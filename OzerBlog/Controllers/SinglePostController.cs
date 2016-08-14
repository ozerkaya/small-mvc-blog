﻿using BlogDAL;
using BlogDAL.DAL;
using RepositoryBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RepositoryBL.Interfaces;

namespace OzerBlog.Controllers
{
    public class SinglePostController : Controller
    {
        SimpleRepo<Posts> repo = new SimpleRepo<BlogDAL.DAL.Posts>(new DBContext());
        // GET: SinglePost
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult insertComment(string comment, string email, string name, string postID)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                work.CommentRepository.insert(new Comments
                {
                    Comment = comment,
                    Contact = email,
                    Date = DateTime.Now,
                    Name = name,
                    Post_ID = Convert.ToInt32(postID)

                });
                work.Save();
                return Json(work.CommentRepository.list().OrderByDescending(ok => ok.ID), JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult listComment()
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                return Json(work.CommentRepository.list().OrderByDescending(ok => ok.ID), JsonRequestBehavior.AllowGet);
            }

        }
    }
}