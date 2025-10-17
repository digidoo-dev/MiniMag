using Microsoft.EntityFrameworkCore;
using MiniMag.Data;

namespace MiniMag.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MiniMagContext(
                serviceProvider.GetRequiredService<DbContextOptions<MiniMagContext>>()
                ))
            {

                if (!context.Product.Any())
                {
                    context.Product.AddRange(
                    new Product { Name = "Soccer Ball", Quantity = 20, Price = 49.99M, Location = "A1" },
                    new Product { Name = "Basketball", Quantity = 15, Price = 59.99M, Location = "A2" },
                    new Product { Name = "Volleyball", Quantity = 18, Price = 39.99M, Location = "A3" },
                    new Product { Name = "Fitness Ball", Quantity = 20, Price = 24.99M, Location = "A4" },
                    new Product { Name = "Medicine Ball 5kg", Quantity = 15, Price = 34.99M, Location = "A5" },
                    new Product { Name = "Medicine Ball 10kg", Quantity = 10, Price = 59.99M, Location = "A6" },

                    // Section B – Dumbbells & Kettlebells
                    new Product { Name = "Dumbbell 5kg", Quantity = 25, Price = 29.99M, Location = "B1" },
                    new Product { Name = "Dumbbell 10kg", Quantity = 20, Price = 49.99M, Location = "B2" },
                    new Product { Name = "Kettlebell 8kg", Quantity = 15, Price = 34.99M, Location = "B3" },
                    new Product { Name = "Kettlebell 16kg", Quantity = 10, Price = 59.99M, Location = "B4" },

                    // Section C – Mats, Bands & Small Equipment
                    new Product { Name = "Exercise Mat", Quantity = 30, Price = 19.99M, Location = "C1" },
                    new Product { Name = "Jump Rope", Quantity = 50, Price = 9.99M, Location = "C2" },
                    new Product { Name = "Resistance Bands Set", Quantity = 40, Price = 14.99M, Location = "C3" },
                    new Product { Name = "Yoga Block", Quantity = 35, Price = 7.99M, Location = "C4" },
                    new Product { Name = "Yoga Strap", Quantity = 30, Price = 5.99M, Location = "C5" },
                    new Product { Name = "Foam Roller", Quantity = 25, Price = 19.99M, Location = "C6" },
                    new Product { Name = "Resistance Tube", Quantity = 30, Price = 12.99M, Location = "C7" },
                    new Product { Name = "Mini Exercise Ball", Quantity = 30, Price = 9.99M, Location = "C8" },
                    new Product { Name = "Resistance Band Handles", Quantity = 25, Price = 7.99M, Location = "C9" },

                    // Section D – Cardio Machines
                    new Product { Name = "Stationary Bike", Quantity = 5, Price = 999.99M, Location = "D1" },
                    new Product { Name = "Treadmill", Quantity = 3, Price = 1999.99M, Location = "D2" },
                    new Product { Name = "Elliptical Trainer", Quantity = 4, Price = 1499.99M, Location = "D3" },

                    // Section E – Pull-up & Strength Accessories
                    new Product { Name = "Pull-up Bar", Quantity = 10, Price = 79.99M, Location = "E1" },
                    new Product { Name = "Pull-up Assist Bands", Quantity = 20, Price = 14.99M, Location = "E2" },
                    new Product { Name = "Gymnastic Rings", Quantity = 10, Price = 59.99M, Location = "E3" },
                    new Product { Name = "Pull-up Bar Doorway", Quantity = 12, Price = 39.99M, Location = "E4" },

                    // Section F – Boxing & Martial Arts
                    new Product { Name = "Boxing Gloves", Quantity = 15, Price = 49.99M, Location = "F1" },
                    new Product { Name = "Punching Bag", Quantity = 5, Price = 149.99M, Location = "F2" },
                    new Product { Name = "Punch Mitts", Quantity = 10, Price = 29.99M, Location = "F3" },

                    // Section G – Agility & Speed Training
                    new Product { Name = "Speed Ladder", Quantity = 20, Price = 29.99M, Location = "G1" },
                    new Product { Name = "Agility Cones", Quantity = 50, Price = 19.99M, Location = "G2" },
                    new Product { Name = "Speed Parachute", Quantity = 7, Price = 34.99M, Location = "G3" },
                    new Product { Name = "Jump Box", Quantity = 6, Price = 59.99M, Location = "G4" },
                    new Product { Name = "Foam Plyo Box", Quantity = 5, Price = 89.99M, Location = "G5" },
                    new Product { Name = "Agility Hurdles", Quantity = 20, Price = 24.99M, Location = "G6" },

                    // Section H – Functional Training
                    new Product { Name = "Balance Board", Quantity = 10, Price = 49.99M, Location = "H1" },
                    new Product { Name = "Climbing Rope", Quantity = 5, Price = 99.99M, Location = "H2" },
                    new Product { Name = "Weighted Balls Set", Quantity = 12, Price = 44.99M, Location = "H3" },

                    // Section I – Small Accessories
                    new Product { Name = "Weighted Vest", Quantity = 8, Price = 79.99M, Location = "I1" },
                    new Product { Name = "Hand Grippers", Quantity = 40, Price = 4.99M, Location = "I2" }
                    );

                    context.SaveChanges();
                }

                

                if (!context.Supplier.Any())
                {
                    context.Supplier.AddRange(
                    new Supplier { Name = "SportPro Supplies", Address = "123 Fitness St, New York, NY", Phone = "212-555-0101" },
                    new Supplier { Name = "ActiveGear Co.", Address = "456 Workout Ave, Los Angeles, CA", Phone = "310-555-0202" },
                    new Supplier { Name = "FitNation Distributors", Address = "789 Gym Rd, Chicago, IL", Phone = "773-555-0303" },
                    new Supplier { Name = "Peak Performance Inc.", Address = "321 Athletic Blvd, Houston, TX", Phone = "713-555-0404" },
                    new Supplier { Name = "AllSport Equipment", Address = "654 Endurance Ln, Miami, FL", Phone = "305-555-0505" },
                    new Supplier { Name = "Champion Sports Supply", Address = "987 Power Dr, Seattle, WA", Phone = "206-555-0606" },
                    new Supplier { Name = "Ultimate Fitness Co.", Address = "135 Strength St, Boston, MA", Phone = "617-555-0707" },
                    new Supplier { Name = "Elite Sports Gear", Address = "246 Agility Rd, Denver, CO", Phone = "303-555-0808" }
                    );

                    context.SaveChanges();
                }

                
            }
        }
    }
}
