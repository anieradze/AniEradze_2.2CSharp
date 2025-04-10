using HigherorLower.Models;
using HigherorLower.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HigherorLower.Controllers
{
    public class LowerOrHigherController : Controller
    {
        private static List<Card> cards = new List<Card>
        {
            new Card { Name = "Nepal Earthquake", Result = 9800000, ImageUrl = "NepalEarthquake.jpg" },
            new Card { Name = "Minecraft", Result = 30000000, ImageUrl = "minecraft.jpg" },
            new Card { Name = "Ariana Grande", Result = 20000000, ImageUrl = "ariana.jpg" },
        };

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Score") == null)
            {
                HttpContext.Session.SetInt32("Score", 0);
            }

            var rand = new Random();
            var leftCardIndex = rand.Next(cards.Count);
            var rightCardIndex = rand.Next(cards.Count);

            while (leftCardIndex == rightCardIndex)
            {
                rightCardIndex = rand.Next(cards.Count);
            }

            var leftCard = cards[leftCardIndex];
            var rightCard = cards[rightCardIndex];

            var model = new GameViewModel
            {
                LeftCard = leftCard,
                RightCard = rightCard,
                Score = HttpContext.Session.GetInt32("Score").Value
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Compare(string selectedCard, int leftCardResult, int rightCardResult)
        {
            int score = HttpContext.Session.GetInt32("Score").Value;
            if ((selectedCard == "Higher" && leftCardResult < rightCardResult) ||
                (selectedCard == "Lower" && leftCardResult > rightCardResult))
            {
                score++;
            }
            else
            {
                score--;
            }

            HttpContext.Session.SetInt32("Score", score);

            var rand = new Random();
            var leftCardIndex = rand.Next(cards.Count);
            var rightCardIndex = rand.Next(cards.Count);

            while (leftCardIndex == rightCardIndex)
            {
                rightCardIndex = rand.Next(cards.Count);
            }

            var leftCard = cards[leftCardIndex];
            var rightCard = cards[rightCardIndex];

            var model = new GameViewModel
            {
                LeftCard = leftCard,
                RightCard = rightCard,
                Score = score
            };

            return View("Index", model);
        }
    }
}
