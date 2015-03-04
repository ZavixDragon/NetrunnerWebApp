using NetrunnerWebApp.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetrunnerWebApp.Services
{
    public class RandomPasswordGenerator : RandomPasswordGeneratorService
    {
        static Random rnd = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
        private int generatedPasswordLength = 8;

        public Task<string> GeneratePassword()
        {
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return Task.FromResult(new string(Enumerable.Repeat(chars, generatedPasswordLength)
                .Select(s => s[rnd.Next(s.Length)]).ToArray()));
        }
    }
}