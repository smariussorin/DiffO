using DiffO.Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiffO;

namespace DiffO.Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var bucharestZoo = new ZooModel
            {
                Bear = new BearModel
                {
                    Color = "Brown",
                    Name = "The Brown Bear",
                    Uid = 123
                },
                Lion = new LionModel
                {
                    Name = "The Lion King",
                    Child = new LionModel
                    {
                        Name = "The Silly Lion King"
                    }
                },
                Birds = new List<BirdModel>
                {
                    new BirdModel
                    {
                        Color = "Blue",
                        Name = "The Blue Bird",
                        Nest = new NestModel
                        {
                            Type = "Simple nest"
                        }
                    },
                    new BirdModel
                    {
                        Color = "Green",
                        Name = "The Great Bird",
                        Nest = new NestModel
                        {
                            Type = "Complex nest"
                        }
                    }
                }
            };

            var clujZoo = new ZooModel
            {
                Bear = new BearModel
                {
                    Name = "The Polar Bear",
                    Uid = 123
                },
                Lion = new LionModel
                {
                    Name = "The Lion King",
                    Child = new LionModel
                    {
                        Name = "The Little Lion King"
                    }
                },
                Birds = new List<BirdModel>
                {
                    new BirdModel
                    {
                        Color = "Red",
                        Name = "The Red Bird",
                        Nest = new NestModel
                        {
                            Type = "Simple nest"
                        }
                    },
                    new BirdModel
                    {
                        Color = "Red",
                        Name = "The Kiler Bird",
                        Nest = new NestModel
                        {
                            Type = "Simple nest"
                        }
                    },
                    new BirdModel
                    {
                        Color = "Green",
                        Name = "The Great Bird",
                        Nest = new NestModel
                        {
                            Type = "Complex nest"
                        }
                    }
                }
            };

            bucharestZoo.CompareTo(clujZoo);

            return View(new DemoViewModel
                            {
                                Bucharest = bucharestZoo,
                                Cluj = clujZoo
                            });
        }

    }
}
