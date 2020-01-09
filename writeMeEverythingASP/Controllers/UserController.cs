using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using writeMeEverythingASP.ApiModel;
using writeMeEverythingASP.Data;
using writeMeEverythingASP.Helper;
using writeMeEverythingASP.Models;
using writeMeEverythingASP.Res;

namespace writeMeEverythingASP.Controllers
{

    [Auth]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly ChatContext _context;

        public UserController()
        {
            _context = new ChatContext();
        }

        [HttpGet, Route("friendList")]
        public IHttpActionResult FriendList()
        {
            string token = Request.Headers.GetValues("token").First().ToString();

            User user = _context.Users.FirstOrDefault(u => u.Token == token);
            if (user == null)
            {
                ModelState.AddModelError("Error", Messages.UsrNotFound);
                return BadRequest(ModelState);
            }
            List<FriendModel> friends = _context.Friends.Include("Sender").Include("Receiver").Where(f => 
            (f.SenderId == user.Id || f.ReceiverId == user.Id) && f.isFriend == true && f.isReceiverBlocked == false && f.isSenderBlocked == false).Select(f=>
            new FriendModel
            {
                FriendId = (f.ReceiverId == user.Id ? f.SenderId : f.ReceiverId),
                CreateAt = f.CreateAt,
                Friend = (f.SenderId==user.Id? new UserModel
                {
                    Name = f.Receiver.Name,
                    Surname = f.Receiver.Surname,
                    Email = f.Receiver.Email,
                    About = f.Receiver.About,
                    Avatar = f.Receiver.Avatar,
                    City = f.Receiver.City,
                    CreateAt = f.Receiver.CreateAt,
                    isOnline = f.Receiver.isOnline,
                    Lastseen = f.Receiver.Lastseen,
                    Phone = f.Receiver.Phone
                }: new UserModel
                {
                    Name = f.Sender.Name,
                    Surname = f.Sender.Surname,
                    Email = f.Sender.Email,
                    About = f.Sender.About,
                    Avatar = f.Sender.Avatar,
                    City = f.Sender.City,
                    CreateAt = f.Sender.CreateAt,
                    isOnline = f.Sender.isOnline,
                    Lastseen = f.Sender.Lastseen,
                    Phone = f.Sender.Phone
                })
            }).ToList();
           

            return Ok(new { friends });

        }


        [HttpGet, Route("chatList")]
        public IHttpActionResult ChatsList()
        {
            string token = Request.Headers.GetValues("token").First().ToString();

            User user = _context.Users.FirstOrDefault(u => u.Token == token);
            if (user == null)
            {
                ModelState.AddModelError("Error", Messages.UsrNotFound);
                return BadRequest(ModelState);
            }

            List<ChatModel> chats = _context.Chats.Include("Sender").Include("Receiver").Where(f =>
            f.SenderId == user.Id || f.ReceiverId == user.Id).Select(f =>
            new ChatModel
            {
                FriendId =( f.ReceiverId==user.Id?f.SenderId:f.ReceiverId),
                CreateAt = f.CreateAt,
                Friend = (f.SenderId == user.Id ? new UserModel
                {
                    Name = f.Receiver.Name,
                    Surname = f.Receiver.Surname,
                    Email = f.Receiver.Email,
                    About = f.Receiver.About,
                    Avatar = f.Receiver.Avatar,
                    City = f.Receiver.City,
                    CreateAt = f.Receiver.CreateAt,
                    isOnline = f.Receiver.isOnline,
                    Lastseen = f.Receiver.Lastseen,
                    Phone = f.Receiver.Phone
                } : new UserModel
                {
                    Name = f.Sender.Name,
                    Surname = f.Sender.Surname,
                    Email = f.Sender.Email,
                    About = f.Sender.About,
                    Avatar = f.Sender.Avatar,
                    City = f.Sender.City,
                    CreateAt = f.Sender.CreateAt,
                    isOnline = f.Sender.isOnline,
                    Lastseen = f.Sender.Lastseen,
                    Phone = f.Sender.Phone
                })
            }).ToList();


            return Ok(new { chats });

        }



        [HttpGet, Route("MessagesList")]
        public IHttpActionResult MessageList([FromUri] int friendId,[FromUri] int take, [FromUri] int skip  )
        {
            string token = Request.Headers.GetValues("token").First().ToString();
            

            User user = _context.Users.FirstOrDefault(u => u.Token == token);

            if (user == null)
            {
                ModelState.AddModelError("Error", Messages.UsrNotFound);
                return BadRequest(ModelState);
            }

            if (!_context.Friends.Any(u => ((u.SenderId == user.Id && u.ReceiverId==friendId)||(u.SenderId==friendId && u.ReceiverId==user.Id)) && u.isReceiverBlocked==false && u.isSenderBlocked==false && u.isFriend==true))
            {
                return NotFound();
            }

            List<MessageModel> Message = _context.Messages.Select(m =>
            new MessageModel
            {
                SenderId = user.Id,
                ReceiverId = (m.SenderId == user.Id ? m.ReceiverId : m.SenderId),
                Content=m.Content,
                isDeletedFromReceiver = (m.SenderId == user.Id ? m.isDeletedFromReceiver : m.isDeletedFromSender),
                isDeletedFromSender = (m.SenderId == user.Id ? m.isDeletedFromSender:m.isDeletedFromReceiver),
                CreateAt = m.CreateAt
            }).OrderBy(f => f.CreateAt).Skip(skip).Take(take).ToList();

            return Ok(new { Message });


        }


        [HttpGet, Route("blockFriend")]
        public IHttpActionResult BlockFriend([FromUri] int friendId )
        {
            string token = Request.Headers.GetValues("token").First().ToString();


            User user = _context.Users.FirstOrDefault(u => u.Token == token);

            if (user == null)
            {
                ModelState.AddModelError("Error", Messages.UsrNotFound);
                return BadRequest(ModelState);
            }


            Friend friend = _context.Friends.FirstOrDefault
                (f => 
                    (
                        ((f.SenderId == user.Id && f.ReceiverId == friendId) && f.isSenderBlocked == false) ||
                        ((f.SenderId == friendId && f.ReceiverId == user.Id) && f.isReceiverBlocked == false)
                    ) && f.isFriend==true
                );

            if (friend == null) 
            {
                return BadRequest(Messages.UsrNotFound);
            }

            if (friend.SenderId == user.Id)
            {
                friend.isSenderBlocked = true;
            }
            else
            {
                friend.isReceiverBlocked = true;
            }
            _context.SaveChanges();

            string Message = Messages.UsrBlocked;


            return Ok(new { Message });
        }


        [HttpGet, Route("unblockFriend")]
        public IHttpActionResult UnblockFriend([FromUri] int friendId)
        {
            string token = Request.Headers.GetValues("token").First().ToString();


            User user = _context.Users.FirstOrDefault(u => u.Token == token);

            if (user == null)
            {
                ModelState.AddModelError("Error", Messages.UsrNotFound);
                return BadRequest(ModelState);
            }

            Friend friend = _context.Friends.FirstOrDefault
               (f =>
                   (
                       ((f.SenderId == user.Id && f.ReceiverId == friendId) && f.isSenderBlocked == true) ||
                       ((f.SenderId == friendId && f.ReceiverId == user.Id) && f.isReceiverBlocked == true)
                   ) && f.isFriend == true
               );

            if (friend == null)
            {
                return BadRequest(Messages.UsrNotFound);
            }

            if (friend.SenderId == user.Id)
            {
                friend.isSenderBlocked = false;
            }
            else
            {
                friend.isReceiverBlocked = false;
            }
            _context.SaveChanges();

            string Message = Messages.UsrUnblocked;


            return Ok(new { Message });
        }

        [HttpPost, Route("friendRequest")]
        public IHttpActionResult AddFriend(AddFriendModel addFriend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string token = Request.Headers.GetValues("token").First().ToString();


            User user = _context.Users.FirstOrDefault(u => u.Token == token);

            if (user == null)
            {
                ModelState.AddModelError("Error", Messages.UsrNotFound);
                return BadRequest(ModelState);
            }

            User friend = _context.Users.FirstOrDefault(f => f.Email == addFriend.Email);

            if (friend == null || friend.Email==user.Email) 
            {
                return BadRequest(Messages.UsrNotFound);
            }
            string Message;

            Friend checkFriend = _context.Friends.FirstOrDefault(f => (f.SenderId == user.Id && f.ReceiverId == friend.Id) || (f.SenderId == friend.Id  && f.ReceiverId == user.Id));

            if (checkFriend != null) 
            {
                if (checkFriend.isFriend == false)
                {
                    Message = Messages.AlrdReqFriend;
                }
                else 
                {
                    Message = Messages.AlrdFriends;
                }
               
                return Ok(new { Message });
            }

            Friend addFriendInDb = new Friend()
            {
                SenderId = user.Id,
                ReceiverId = friend.Id,
                isFriend = false,
                isSenderBlocked=false,
                isReceiverBlocked=false,
                CreateAt=DateTime.Now
            };

            _context.Friends.Add(addFriendInDb);

            _context.SaveChanges();


            Message = Messages.UsrReq;


            return Ok(new { Message });
        }


        [HttpGet, Route("friendRequestList")]
        public IHttpActionResult FriendRequestList()
        {
            string token = Request.Headers.GetValues("token").First().ToString();


            User user = _context.Users.FirstOrDefault(u => u.Token == token);

            if (user == null)
            {
                ModelState.AddModelError("Error", Messages.UsrNotFound);
                return BadRequest(ModelState);
            }

          
            List<FriendModel> friendsRequest = _context.Friends.Include("Sender").Include("Receiver").Where(f =>
            (f.SenderId == user.Id || f.ReceiverId == user.Id) && f.isFriend == false && f.isReceiverBlocked == false && f.isSenderBlocked == false).Select(f =>
            new FriendModel
            {
                FriendId = (f.ReceiverId == user.Id ? f.SenderId : f.ReceiverId),
                CreateAt = f.CreateAt,
                Friend = (f.SenderId == user.Id ? new UserModel
                {
                    Name = f.Receiver.Name,
                    Surname = f.Receiver.Surname,
                    Email = f.Receiver.Email,
                    About = f.Receiver.About,
                    Avatar = f.Receiver.Avatar,
                    City = f.Receiver.City,
                    CreateAt = f.Receiver.CreateAt,
                    isOnline = f.Receiver.isOnline,
                    Lastseen = f.Receiver.Lastseen,
                    Phone = f.Receiver.Phone
                } : new UserModel
                {
                    Name = f.Sender.Name,
                    Surname = f.Sender.Surname,
                    Email = f.Sender.Email,
                    About = f.Sender.About,
                    Avatar = f.Sender.Avatar,
                    City = f.Sender.City,
                    CreateAt = f.Sender.CreateAt,
                    isOnline = f.Sender.isOnline,
                    Lastseen = f.Sender.Lastseen,
                    Phone = f.Sender.Phone
                })
            }).ToList();


            return Ok(new { friendsRequest });
        }


        [HttpPost, Route("replyFriendRequest")]
        public IHttpActionResult ReplyFriendRequest(ReplyFriendRequest reply)
        {
            string token = Request.Headers.GetValues("token").First().ToString();

            User user = _context.Users.FirstOrDefault(u => u.Token == token);

            if (user == null)
            {
                ModelState.AddModelError("Error", Messages.UsrNotFound);
                return BadRequest(ModelState);
            }

            Friend friend = _context.Friends.FirstOrDefault(f=>f.SenderId==user.Id && f.ReceiverId==reply.FriendId);

            if (friend==null) 
            {
                ModelState.AddModelError("Error", Messages.UsrNotFound);
                return BadRequest(ModelState);
            }

            if (friend.isFriend == true) 
            {
                return Ok(new { Messages.AlrdFriends});
            }
            string Message;
            if (reply.ReplyRequest==true) 
            {
                friend.isFriend = true;
                Message = Messages.AccFriends;
            }
            else 
            {
                _context.Friends.Remove(friend);
                Message = Messages.DelFriends;

            }
            _context.SaveChanges();

            return Ok(new { Message });
        }

    }
}
