using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTrackerCF.Helpers;
using BugTrackerCF.Models;
using Microsoft.AspNet.Identity;
using BugTrackerCF.Hub;
using Microsoft.AspNet.SignalR;
using hbehr.recaptcha;

namespace BugTrackerCF.Controllers
{
    [System.Web.Mvc.Authorize]
    public class TicketsController : Controller
    { 
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tickets
        public ActionResult Index()
        {
            //MyHub myhub = new MyHub();
            //var userid = User.Identity.GetUserName();
            //myhub.SendNote(userid, "Hello");

            var tickets = db.Tickets.Include(t => t.AssignedToUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType);
            
            return View(tickets);
        }

        //public JsonResult GetTickets([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest request, bool myTickets,
        //    DateTimeOffset? date, string type, string priority, string status)
        //{
        //    string userId = User.Identity.GetUserId();
        //    var user = db.Users.Find(userId); //check navigational properties - find looks in memory first for user if not, takes from database
        //    IQueryable<Ticket> tickets;
        //    tickets = db.Tickets.Where(t => t.OwnerUserId == userId);

        //    if (User.IsInRole("Developer") && myTickets == true)
        //    {
        //        ViewBag.Dev = "dev";
        //        tickets = db.Tickets.Where(t => t.AssignedToUserId == userId);
        //    }
        //    else if (User.IsInRole("Admin"))
        //    {
        //        tickets = db.Tickets;
        //    }
        //    else if (User.IsInRole("Project Manager") || User.IsInRole("Developer"))
        //    {
        //        //tickets=(List<Ticket>)user.ListTicketsForUser().Where(u=>u.Id == u.Id);
        //        tickets = user.Projects.SelectMany(p => p.Tickets).AsQueryable();

        //    }
        //    else if (User.IsInRole("Submitter"))
        //    {
        //        tickets = db.Tickets.Where(t => t.OwnerUserId == userId);
        //    }
        //    //db.Tickets.Include(t => t.AssignedToUser)

        //    if (date != null)
        //    {
        //        tickets = tickets.Where(t => t.Created > date);
        //    }
        //    if (type != null && type != "All")
        //    {
        //        tickets = tickets.Where(t => t.TicketType.Name == type);
        //    }
        //    if (priority != null && priority != "All")
        //    {
        //        tickets = tickets.Where(t => t.TicketPriority.Name == priority);
        //    }
        //    if (status != null && status != "All")
        //    {
        //        tickets = tickets.Where(t => t.TicketStatus.Name == status);
        //    }

        //    var totalCount = tickets.Count();
        //    var search = request.Search.Value;


        //    if (!string.IsNullOrWhiteSpace(search))
        //    {
        //        tickets = tickets.Where(t => t.Title.Contains(search) || t.Description.Contains(search)
        //            || (t.AssignedToUserId != "" && t.AssignedToUserId != null && t.AssignedToUser.DisplayName.Contains(search))
        //            || t.Project.Name.Contains(search)
        //            || t.OwnerUser.DisplayName.Contains(search) || t.TicketStatus.Name.Contains(search)
        //            || t.TicketPriority.Name.Contains(search) || t.TicketType.Name.Contains(search));

        //    }


        //    tickets = tickets.OrderByDescending(t => t.Created);

        //    var column = request.Columns.FirstOrDefault(r => r.IsOrdered == true);
        //    if (column != null)
        //    {
        //        if (column.SortDirection == Column.OrderDirection.Descendant)
        //        {
        //            switch (column.Data)
        //            {
        //                case "Title":
        //                    tickets = tickets.OrderByDescending(t => t.Title);
        //                    break;
        //                case "Description":
        //                    tickets = tickets.OrderByDescending(t => t.Description);
        //                    break;
        //                case "Created":
        //                    tickets = tickets.OrderByDescending(t => t.Created);
        //                    break;
        //                case "Updated":
        //                    tickets = tickets.OrderByDescending(t => t.Updated);
        //                    break;
        //                case "Project":
        //                    tickets = tickets.OrderByDescending(t => t.Project.Name);
        //                    break;
        //                case "AssignedUser":
        //                    tickets = tickets.OrderByDescending(t => t.AssignedToUser.DisplayName);
        //                    break;
        //                case "OwnerUser":
        //                    tickets = tickets.OrderByDescending(t => t.OwnerUser.DisplayName);
        //                    break;
        //                case "TicketStatus":
        //                    tickets = tickets.OrderByDescending(t => t.TicketStatus.Name);
        //                    break;
        //                case "TicketType":
        //                    tickets = tickets.OrderByDescending(t => t.TicketType.Name);
        //                    break;
        //                case "TicketPriority":
        //                    tickets = tickets.OrderByDescending(t => t.TicketPriority.Name);
        //                    break;

        //            }
        //        }
        //        else
        //        {
        //            switch (column.Data)
        //            {
        //                case "Title":
        //                    tickets = tickets.OrderBy(t => t.Title);
        //                    break;
        //                case "Description":
        //                    tickets = tickets.OrderBy(t => t.Description);
        //                    break;
        //                case "Created":
        //                    tickets = tickets.OrderBy(t => t.Created);
        //                    break;
        //                case "Updated":
        //                    tickets = tickets.OrderBy(t => t.Updated);
        //                    break;
        //                case "Project":
        //                    tickets = tickets.OrderBy(t => t.Project.Name);
        //                    break;
        //                case "AssignedUser":
        //                    tickets = tickets.OrderBy(t => t.AssignedToUser.DisplayName);
        //                    break;
        //                case "OwnerUser":
        //                    tickets = tickets.OrderBy(t => t.OwnerUser.DisplayName);
        //                    break;
        //                case "TicketStatus":
        //                    tickets = tickets.OrderBy(t => t.TicketStatus.Name);
        //                    break;
        //                case "TicketType":
        //                    tickets = tickets.OrderBy(t => t.TicketType.Name);
        //                    break;
        //                case "TicketPriority":
        //                    tickets = tickets.OrderBy(t => t.TicketPriority.Name);
        //                    break;
        //            }
        //        }
        //    }

        //    var pgd = tickets.Skip(request.Start).Take(request.Length);
        //    var paged = pgd.Select(t => new TicketViewModel
        //    {
        //        Project = t.Project.Name,
        //        TicketStatus = "<span style=\"color:green;\">" + t.TicketStatus.Name + "</span>",
        //        TicketPriority = t.TicketPriority.Name,
        //        TicketType = t.TicketType.Name,
        //        OwnerUser = t.OwnerUser.DisplayName,
        //        AssignedToUser = t.AssignedToUser == null ? "" : "<span style=\"color:green;\">" + t.AssignedToUser.DisplayName + "</span>",
        //        Title = "<span style=\"color:green;\">" + t.Title + "</span>",
        //        Created = t.Created,
        //        Updated = t.Updated,
        //        Id = t.Id,
        //        Description = t.Description,
        //        link = "<a href=\"/Tickets/Details/" + t.Id + "\">Details</a>",

        //    });
        //    return Json(new DataTablesResponse(request.Draw, paged, tickets.Count(), totalCount), JsonRequestBehavior.AllowGet);
        //}

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,ProjectId,TicketStatusId,TicketPriorityId,TicketTypeId,AssignedToUserId")] Ticket ticket)
        {
            string userResponse = HttpContext.Request.Params["g-recaptcha-response"];
            bool validCaptcha = ReCaptcha.ValidateCaptcha(userResponse);
            if (validCaptcha)
            {
                if (ModelState.IsValid)
                {
                    ticket.Created = System.DateTimeOffset.Now;
                    ticket.OwnerUserId = User.Identity.GetUserId();
                    db.Tickets.Add(ticket);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // Bot Attack, non validated !
                return RedirectToAction("YouAreARobot", "Home");
            }
            

            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Created,Updated,ProjectId,TicketStatusId,TicketPriorityId,TicketTypeId,OwnerUserId,AssignedToUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var userid = User.Identity.GetUserId();
                var changed = System.DateTime.Now;
                var editId = Guid.NewGuid().ToString();
                var oldTic = db.Tickets.AsNoTracking().FirstOrDefault(t => t.Id == ticket.Id);
                MyHub myhub = new MyHub();
                var assignedUser = db.Users.Find(ticket.AssignedToUserId);
                //myhub.SendNote(userid, "Hello");
                if (oldTic?.Title != ticket.Title)
                {
                    TicketHistory th1 = new TicketHistory
                    {
                        TicketId = ticket.Id,
                        Property = "Title",
                        OldValue = oldTic?.Title,
                        NewValue = ticket.Title,
                        EditId = editId,
                        Changed = changed,
                        UserId = userid
                    };
                    db.TicketHistories.Add(th1);
                }
                if (oldTic?.Description != ticket.Description)
                {
                    TicketHistory th2 = new TicketHistory
                    {
                        TicketId = ticket.Id,
                        Property = "Description",
                        OldValue = oldTic?.Description,
                        NewValue = ticket.Description,
                        Changed = changed, // system date
                        UserId = userid  // current userId
                    };
                    db.TicketHistories.Add(th2);
                }

                if (oldTic?.ProjectId != ticket.ProjectId)
                {

                    TicketHistory th3 = new TicketHistory
                    {
                        TicketId = ticket.Id,
                        Property = "Project",
                        OldValue = db.Projects.Find(oldTic?.ProjectId).Name,
                        NewValue = db.Projects.Find(ticket.ProjectId).Name,
                        EditId = editId,
                        Changed = changed,
                        UserId = userid
                    };
                    db.TicketHistories.Add(th3);
                }

                if (oldTic?.TicketStatusId != ticket.TicketStatusId)
                {
                    TicketHistory th4 = new TicketHistory
                    {
                        TicketId = ticket.Id,
                        Property = "TicketStatus",
                        OldValue = db.TicketStatuses.Find(oldTic?.TicketStatusId).Name,
                        NewValue = db.TicketStatuses.Find(ticket.TicketStatusId).Name,
                        EditId = editId,
                        Changed = changed,
                        UserId = userid
                    };
                    db.TicketHistories.Add(th4);
                }

                if (oldTic?.TicketPriorityId != ticket.TicketPriorityId)
                {
                    //ApplicationUser user = db.Users.Find(ticket.AssignedToUserId);
                    TicketHistory th5 = new TicketHistory
                    {
                        TicketId = ticket.Id,
                        Property = "TicketPriority",
                        OldValue = db.TicketPriorities.Find(oldTic?.TicketPriorityId).Name,
                        NewValue = db.TicketPriorities.Find(ticket.TicketPriorityId).Name,
                        EditId = editId,
                        Changed = changed,
                        UserId = userid
                    };
                    Notification note = new Notification
                    {
                        TicketId = ticket.Id,
                        UserId = assignedUser.Id,
                        Change = "Priority",
                        Details = th5.NewValue,
                        DateNotified = changed,
                    };
                    db.Notifications.Add(note);
                    myhub.SendNote(assignedUser.UserName, "The priority for ticket number : " + note.TicketId 
                        + " has changed to " + note.Details + " on " + note.DateNotified.DateTime.ToLongDateString());
                    //user.SendNotification(note);
                    db.TicketHistories.Add(th5);
                }

                if (oldTic?.TicketTypeId != ticket.TicketTypeId)
                {
                    TicketHistory th6 = new TicketHistory
                    {
                        TicketId = ticket.Id,
                        Property = "TicketType",
                        OldValue = db.TicketTypes.Find(oldTic?.TicketTypeId).Name,
                        NewValue = db.TicketTypes.Find(ticket.TicketTypeId).Name,
                        EditId = editId,
                        Changed = changed,
                        UserId = userid
                    };
                    db.TicketHistories.Add(th6);
                }

                if (oldTic?.AssignedToUserId != ticket.AssignedToUserId)
                {
                    ApplicationUser user = db.Users.Find(ticket.AssignedToUserId);
                    TicketHistory th7 = new TicketHistory
                    {
                        TicketId = ticket.Id,
                        Property = "AssignedToUser",
                        OldValue = oldTic?.AssignedToUserId == null ? "" : db.Users.Find(oldTic.AssignedToUserId).DisplayName,
                        NewValue = user.DisplayName,
                        EditId = editId,
                        Changed = changed,
                        UserId = userid
                    };

                    Notification note = new Notification
                    {
                        TicketId = ticket.Id,
                        UserId = user.Id,
                        Change = "Assigned",
                        Details = user.DisplayName,
                        DateNotified = changed
                    };
                    db.Notifications.Add(note);
                    myhub.SendNote(assignedUser.UserName, "The ticket number : " + note.TicketId 
                        + " has been assigned to you on " + note.DateNotified.DateTime.ToLongDateString());
                    //user.SendNotification(note);
                    db.TicketHistories.Add(th7);
                }

                ticket.Updated = changed;
                
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedToUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
