﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YWSDotNetCore.ConsoleApp.Models;

namespace YWSDotNetCore.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbConnect db = new AppDbConnect();
            var lst = db.Blogs.Where(x => x.DeleteFlag == false).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);

            }
        }

        public void Create(string title, string author,string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            AppDbConnect db = new AppDbConnect();
            db.Blogs.Add(blog);
            var result = db.SaveChanges();

            Console.WriteLine(result == 1 ? "Saving Successful." : "Saving Failed.");

        }

        public void Edit(int id) 
        {
            AppDbConnect db = new AppDbConnect();
            // db.Blogs.Where(x => x.BlogId == id).FirstOrDefault();
            var item =  db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No Dada Found.");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);

        }

        public void Update(int id,string title, string author, string content)
        {
            AppDbConnect db = new AppDbConnect();
            // db.Blogs.Where(x => x.BlogId == id).FirstOrDefault();
            var item = db.Blogs
                .AsNoTracking()
                .FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No Dada Found.");
                return;
            }

            if(!string.IsNullOrEmpty(title))
            {
                item.BlogTitle = title;
            }
            if (!string.IsNullOrEmpty(author))
            {
                item.BlogAuthor = author;
            }
            if (!string.IsNullOrEmpty(content))
            {
                item.BlogContent = content;
            }

            db.Entry(item).State = EntityState.Modified;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Updating Successful." : "Updating Failed.");

        }

        public void Delete(int id)
        {
            AppDbConnect db = new AppDbConnect();
            // db.Blogs.Where(x => x.BlogId == id).FirstOrDefault();
            var item = db.Blogs
                .AsNoTracking()
                .FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No Dada Found.");
                return;
            }
            db.Entry(item).State = EntityState.Deleted;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Deleting Successful." : "Deleting Failed.");

        }

    }
}
