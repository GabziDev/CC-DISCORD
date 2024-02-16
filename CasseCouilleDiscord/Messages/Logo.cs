using System;

namespace CasseCouilleDiscord.Messages
{
    internal class Logo
    {
        public static void Token()
        {
            string textToken = @"
 _____      _              
/__   \___ | | _____ _ __  
  / /\/ _ \| |/ / _ \ '_ \ 
 / / | (_) |   <  __/ | | |
 \/   \___/|_|\_\___|_| |_|
";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(textToken);
            Console.ResetColor();
        }

        public static void AppLogo()
        {
            string textTitle = @"
   _____ _____      _____ _____  _____  _____ ____  _____  _____  
  / ____/ ____|    |  __ \_   _|/ ____|/ ____/ __ \|  __ \|  __ \ 
 | |   | |   ______| |  | || | | (___ | |   | |  | | |__) | |  | |
 | |   | |  |______| |  | || |  \___ \| |   | |  | |  _  /| |  | |
 | |___| |____     | |__| || |_ ____) | |___| |__| | | \ \| |__| |
  \_____\_____|    |_____/_____|_____/ \_____\____/|_|  \_\_____/ 

[   https://github.com/GabziDev/CC-DISCORD   ]
[                 V2.0.1 BETA                ]
[              Gabzdev, Souciss              ]";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(textTitle);
            Console.ResetColor();
        }

        public static void SpamMessage()
        {
            string textSpamMessage = @"
 __                                                               
/ _\_ __   __ _ _ __ ___     /\/\   ___  ___ ___  __ _  __ _  ___ 
\ \| '_ \ / _` | '_ ` _ \   /    \ / _ \/ __/ __|/ _` |/ _` |/ _ \
_\ \ |_) | (_| | | | | | | / /\/\ \  __/\__ \__ \ (_| | (_| |  __/
\__/ .__/ \__,_|_| |_| |_| \/    \/\___||___/___/\__,_|\__, |\___|
   |_|                                                 |___/      
";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(textSpamMessage);
            Console.ResetColor();
        }

        public static void SpamReact()
        {
            string textSpamReact = @"
 _____                        ______                _   
/  ___|                       | ___ \              | |  
\ `--. _ __   __ _ _ __ ___   | |_/ /___  __ _  ___| |_ 
 `--. \ '_ \ / _` | '_ ` _ \  |    // _ \/ _` |/ __| __|
/\__/ / |_) | (_| | | | | | | | |\ \  __/ (_| | (__| |_ 
\____/| .__/ \__,_|_| |_| |_| \_| \_\___|\__,_|\___|\__|
      | |                                               
      |_|                                               
";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(textSpamReact);
            Console.ResetColor();
        }
    }
}