using Microsoft.EntityFrameworkCore;

namespace GuitarOnlineShop.Models.Data
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {

                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                        new Product()
                    {
                        Name = "FGN JIL72ASHDER",
                        Brand = "FGN Guitars",
                        Series = "Illiad",
                        Type = "Electric guitar",
                        Price = 2000,
                        Description = "Brand new Fujigen's 7 string telecaster with 2 powerful Fishman Fluence picups and" +
                        "progressive design!",
                        ImgUrls = new List<string>()
						{
                            "/image/prods/fgnbeauty1.jpg",
                            "/image/prods/fgnbeauty2.jpg",
                            "/image/prods/fgnbeauty3.jpg",
                            "/image/prods/fgnbeauty4.jpg"
                        },
                        Specs = new Dictionary<string, string>()
                        {
                            { "Construction", "Bolt-On Neck" },
                            { "Fretboard", "Rosewood" },
                            { "Body", "Ash" },
                            { "Neck", "Maple" },
                            { "Scale", $@"25.5{'"'}" },
                            { "Frets", "Jumbo" },
                            { "Tuners", "GOTOH" },
                            { "Pickup(Neck)", "FISHMAN Fluence Modern" },
                            { "Pickup(Bridge)", "FISHMAN Fluence Modern" }
                        }
                    },
                    new Product()
                    {
                        Name = "JIL2EW2R",
                        Brand = "FGN Guitars",
                        Series = "Illiad",
                        Type = "Electric guitar",
                        Price = 1000,
                        Description = "J-Standard fret edge treatment on compound radius rosewood fingerboard & Seymour" +
                        "Duncan JB & Jazz Humbucking pickups with 3-way switch and push-pull coil tap on tone pot.",
                        ImgUrls = new List<string>()
                        {
                            "/image/prods/fgnred1.jpg",
                            "/image/prods/fgnred2.jpg",
                            "/image/prods/fgnred3.jpg",
                            "/image/prods/fgnred4.jpg"
                        },
                        Specs = new Dictionary<string, string>()
                        {
                            { "Construction", "Bolt-On Neck" },
                            { "Fretboard", "Rosewood" },
                            { "Body", "Acacia Koa on Ash" },
                            { "Neck", "Maple U Shape" },
                            { "Scale", $@"25.5{'"'}" },
                            { "Frets", "Medium" },
                            { "Tuners", "GOTOH" },
                            { "Pickup(Neck)", "Seymour Duncan® Jazz SH-2n" },
                            { "Pickup(Bridge)", "Seymour Duncan® JB™ TB-4" }
                        }
                    },
                    new Product()
                    {
                        Name = "MSA-HP",
                        Brand = "FGN Guitars",
                        Series = "Masterfield",
                        Type = "Acoustic guitar",
                        Price = 1300,
                        Description = "Little pretty cherry semi acoustic guitar. Looks kinda retro, sounds kinda nice :)",
                        ImgUrls = new List<string>()
                        {
                            "/image/prods/masterfild1.jpg",
                            "/image/prods/masterfild2.jpg",
                            "/image/prods/masterfild3.jpg",
                            "/image/prods/masterfild4.jpg"
                        },
                        Specs = new Dictionary<string, string>()
                        {
                            { "Construction", "Set-in Neck" },
                            { "Fretboard", "Rosewood" },
                            { "Body", "Curly Maple Top & Back / Mahogany Sides" },
                            { "Neck", "Mahogany U Shape" },
                            { "Scale", $@"24.8{'"'}" },
                            { "Frets", "Medium Jumbo" },
                            { "Tuners", "GOTOH" },
                            { "Pickup(Neck)", "FGN Alnico 3" },
                            { "Pickup(Bridge)", "FGN Alnico 3" },
                            { "Controls", "2Volume, 2Tone, 3Way Toggle SW" },
                            { "Body Finish", "Top Lacquer Low Gloss" }
                        }
                    },
                    new Product()
                    {
                        Name = "EMJ5-AL-R",
                        Brand = "FGN Guitars",
                        Series = "Mighty jazz",
                        Type = "Bass guitar",
                        Price = 1100,
                        Description = "Rock-solid workhorse basses. Featuring a slightly thicker body, precision bolt-on construction, " +
                        "and solid road-worthy hardware, these basses offer fundamental bass tones with fat lows and clear highs. Is " +
                        "sure to fit any music genre.",
                        ImgUrls = new List<string>()
                        {
                            "/image/prods/mj1.jpg",
                            "/image/prods/mj2.jpg",
                            "/image/prods/mj3.jpg",
                            "/image/prods/mj4.jpg"
                        },
                        Specs = new Dictionary<string, string>()
                        {
                            { "Construction", "Bolt-on Neck" },
                            { "Fretboard", "Rosewood" },
                            { "Body", "2pc Alder" },
                            { "Neck", "Maple U-Shape" },
                            { "Scale", $@"34{'"'}" },
                            { "Frets", "Medium Jumbo" },
                            { "Tuners", "GOTOH" },
                            { "Pickup(Neck)", "FGN 62J5-F" },
                            { "Pickup(Bridge)", "FGN 62J5-R" },
                            { "Controls", "2 Volume, 1 Tone" },
                            { "Body Finish", "Gloss" }
                        }
                    }
                        );
                    context.SaveChanges();
                }

            }
        }
    }
}
