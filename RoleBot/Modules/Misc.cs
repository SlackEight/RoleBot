using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace RoleBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        private string[] ProfessionRoles = { "Programmer","Artist","Linguist","Mathematician","Musician","Chef","Sportsman","Engineer","Philosopher","Teacher","Designer"};

        private string[] MBTIRoles = { "INTP", "ENTP", "INTJ", "ENTJ", "INFP", "ENFP", "INFJ", "ENFJ", "ISTP", "ESTP", "ISTJ", "ESTJ", "ISFP", "ESFP", "ISFJ", "ESFJ" };

        private string[] MBTIRolesWithEmoji = { "INTP 🧠", "ENTP 🐺", "INTJ ☠️", "ENTJ 🔥", "INFP 🐣", "ENFP 🐰", "INFJ 🦄", "ENFJ ⭐", "ISTP 👩‍🔧", "ESTP ♦️", "ISTJ 📖", "ESTJ 🎖️", "ISFP 🎨", "ESFP 🙇", "ISFJ ➕", "ESFJ 🌷" };

        private string[] ElementRoles = { "Earth", "Air", "Water", "Fire" };

        private string[] AstrologyRoles = { "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio", "Sagittarius", "Capricorn", "Aquarius", "Pisces" };

        private string[] EnneagramRoles = { "1w9", "1w2", "2w1", "2w3", "3w2", "3w4", "4w3", "4w5", "5w4", "5w6", "6w5", "6w7", "7w6", "7w8", "8w7", "8w9", "9w8", "9w1" };

        private string[] MiscRoles = { "Angry", "Sad", "Needs a hug", "Gives hugs", "Vegetarian", "Wine expert", "Steam", "Sx/so","Sx/sp","Sp/sx","Sp/so","So/sx","So/sp"};

        private SocketRole mbtirole;


        [Command("help")]
        public async Task Help()
        {
            if (Context.Channel.Name != "chat")
            {
                await Context.Channel.SendMessageAsync("Hey there! I'm Role Fish. \nI was created by Morgan to handle all the administrative nit bits, like assigning roles, " +
                    "levelling up users, and showing you what roles you can assign at your level! \nHere are some commands you can use:");
                var embed = new EmbedBuilder();
                embed.WithTitle("?r");
                embed.WithDescription("This command lets you assign a role. First write ?r then the role you want to add! Eg: ?r Wine Expert");
                embed.WithColor(new Color(255, 0, 0));
                await Context.Channel.SendMessageAsync("", false, embed);

                embed.WithTitle("?roles");
                embed.WithDescription("This command gives a list of all the roles.");
                embed.WithColor(new Color(0, 255, 0));
                await Context.Channel.SendMessageAsync("", false, embed);
            }
        }
        [Command("roles")]
        public async Task SayName()
        {
            await Context.Channel.SendMessageAsync("Here are the obtainable roles on Keyboard");
            var embed = new EmbedBuilder();
            embed.WithTitle("MBTI roles (one at a time)");
            embed.WithDescription("INTP 🧠, ENTP 🐺, INTJ ☠️, ENTJ 🔥, INFP 🐣, ENFP 🐰, INFJ 🦄, ENFJ ⭐, ISTP 👩‍🔧, ESTP ♦️, ISTJ 📖, ESTJ 🎖️, ISFP 🎨, ESFP 🙇, ISFJ ➕, ESFJ 🌷, A, T");
            embed.WithColor(new Color(200, 0, 200));
            await Context.Channel.SendMessageAsync("", false, embed);

            embed.WithTitle("Profession roles (three at a time)");
            embed.WithDescription("Programmer, Artist, Linguist, Mathematician, Musician, Chef, Sportsman, Engineer, Philosopher, Teacher, Designer");
            embed.WithColor(new Color(0, 255, 0));
            await Context.Channel.SendMessageAsync("", false, embed);

            embed.WithTitle("Astrology roles (one at a time)");
            embed.WithDescription("Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra, Scorpio, Sagittarius, Capricorn, Aquarius, Pisces");
            embed.WithColor(new Color(0, 200, 200));
            await Context.Channel.SendMessageAsync("", false, embed);

            embed.WithTitle("Student roles (you can be both)");
            embed.WithDescription("Student, Graduate");
            embed.WithColor(new Color(200, 200, 0));
            await Context.Channel.SendMessageAsync("", false, embed);

            embed.WithTitle("Element roles (max two)");
            embed.WithDescription("Air, Fire, Water, Earth");
            embed.WithColor(new Color(100, 255, 100));
            await Context.Channel.SendMessageAsync("", false, embed);

            embed.WithTitle("Misc status (as many as you like)");
            embed.WithDescription("Angry, Sad, Needs a hug, Gives hugs, Vegetarian, Wine expert, Steam, Sx / so, Sx / sp, Sp / sx, Sp / so, So / sx, So / sp");
            embed.WithColor(new Color(100, 100, 100));
            await Context.Channel.SendMessageAsync("", false, embed);

        }
        [Command("r")]
        public async Task AddRole([Remainder]string r)
        {
            int professionRoles = 0;
            int elementRoles = 0;
            bool mbtiRole = false;
            bool enneagramRole = false;
            bool astrologyRole = false;
            var user = Context.User;
            string output = "";
            bool tagRemoval = false;
         
            List<string> rolenames = new List<string>();

            // Note that if you already had the tag, only the removed output will be tag
            // Checks if it is an MBTI role, removes old role and sets output accordingly
            if (MBTIRoles.Contains(r.ToUpper()))
            {
                mbtiRole = true;
                r = MBTIRolesWithEmoji[Array.IndexOf(MBTIRoles, r.ToUpper())];
            }

            else if (EnneagramRoles.Contains(r))
            {             
                enneagramRole = true;
                output = "➕ Setting enneagram to: " + r;
            }

            else if (AstrologyRoles.Contains(r.Substring(0, 1).ToUpper() + r.ToLower().Substring(1)))
            {
                r = r.Substring(0, 1).ToUpper() + r.ToLower().Substring(1);
                astrologyRole = true;
                output = "➕ Setting star sign to: " + r;
            }

            else
            {
                r = r.Substring(0, 1).ToUpper() + r.ToLower().Substring(1);
            }

            // Checks if it's an A/T role, removes old role and sets output accordingly
            if (r.Equals("A"))
            {
                var ro = Context.Guild.Roles.FirstOrDefault(x => x.Name == "T");
                await (user as IGuildUser).RemoveRoleAsync(ro);
                output = "➕ Setting identity to A (Assertive)";
            }
            else if (r.Equals("T"))
            {
                var ro = Context.Guild.Roles.FirstOrDefault(x => x.Name == "A");
                await (user as IGuildUser).RemoveRoleAsync(ro);
                output = "➕ Setting identity to T (Turbulent)";
            }

            // Checks if it's an Element role and sets output accordingly
            if (ElementRoles.Contains(r))
            {
                output = "➕ Adding element role: " + r;
            }

            // Gets an instance of the role you want to add
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == r);
            // Goes through all the users roles and sets the number of each type of role 
            // he has (eg 2 element roles) so that users are limited by role capacity.
            foreach (SocketRole rololo in ((SocketGuildUser)Context.User).Roles)
            {
                if (!rololo.Name.Equals("@everyone"))
                {
                    rolenames.Add(rololo.Name);
                    if (ProfessionRoles.Contains(rololo.Name))
                    {
                        professionRoles++;
                    }
                    if (ElementRoles.Contains(rololo.Name))
                    {
                        elementRoles++;
                    }
                    if (MBTIRolesWithEmoji.Contains(rololo.Name))
                    {
                        mbtirole = rololo;
                        Console.WriteLine("ohno");
                    }
                    if (enneagramRole && EnneagramRoles.Contains(rololo.Name))
                    {
                        await (user as IGuildUser).RemoveRoleAsync(rololo);
                    }
                    if (astrologyRole && AstrologyRoles.Contains(rololo.Name))
                    {
                        await (user as IGuildUser).RemoveRoleAsync(rololo);
                    }
                }
            }

            // Checks if you have the tag, if you do - remove it.
            if (rolenames.Contains(r))
            {
                Console.WriteLine("REEEEE");
                if (Context.Channel.Name != "chat")
                {
                    await Context.Channel.SendMessageAsync(":heavy_minus_sign: Removing tag: " + r);
                    tagRemoval = true;
                }
                await (user as IGuildUser).RemoveRoleAsync(role);
            }
            else
            {
                if (mbtiRole)
                {
                    if (mbtirole != null)
                        await (user as IGuildUser).RemoveRoleAsync(mbtirole);
                    Console.WriteLine("ohno");
                    output = "Setting your MBTI type to " + r;
                    if (Context.Channel.Name != "chat")
                    {
                        await Context.Channel.SendMessageAsync(output);
                        Console.WriteLine("ususus");
                    }
                    Console.WriteLine("akakaka");
                    await (user as IGuildUser).AddRoleAsync(role);
                    Console.WriteLine("popopop");
                }

                // Checks if you're trying to add a profession role with full capacity
                else if (ProfessionRoles.Contains(r) && professionRoles >= 3)
                {
                    if (Context.Channel.Name != "chat")
                    {
                        await Context.Channel.SendMessageAsync(":x: Sorry, you can only have 3 profession roles at one time. Pick your top 3!");
                    }
                }

                // Checks if you're trying to add an element role with full capacity
                else if (ElementRoles.Contains(r) && elementRoles >= 2)
                {
                    Console.WriteLine("sPECtacUlar11");
                    if (Context.Channel.Name != "chat")
                    {
                        await Context.Channel.SendMessageAsync(":x: Sorry, you can only have 2 element roles at one time. Pick your top 2!");
                    }
                }

                else if (role != null)
                {
                    if (output.Equals(""))
                    {
                        if (Context.Channel.Name != "chat")
                        {
                            await Context.Channel.SendMessageAsync("➕ Adding role: " + r);
                        }
                    }
                    else
                    {
                        if (Context.Channel.Name != "chat")
                        {
                            await Context.Channel.SendMessageAsync(output);
                        }
                    }
                    await (user as IGuildUser).AddRoleAsync(role);
                }
            }
            
            //Console.WriteLine(output);
            //await (user as IGuildUser).AddRoleAsync(role);
        }
        [Command("data")]
        public async Task GetData()
        {
            await Context.Channel.SendMessageAsync("Data Has " + DataStorage.GetPairsCount() + " pairs.");
            DataStorage.AddPair(""+DataStorage.GetPairsCount(), "Morgan!");
        }
    }
}