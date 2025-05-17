namespace Metamon.UI
{
    public static class FightDrawer
    {
        public static void Init()
        {
            Console.Title = "Metamon";
            Console.CursorVisible = false;
            // TODO Fun initialization sequence
            Console.Clear();
            ConsoleUtils.DrawImageAt(title, 1, 1);
            ConsoleUtils.DrawImageAt(frog, 1, 12);
            ConsoleUtils.DrawImageAt("Frog", 1, 31);
            ConsoleUtils.DrawImageAt(frog, 56, 12, 0.1f, ConsoleColor.Red, ConsoleColor.Gray);
            ConsoleUtils.DrawImageAt("Evil frog", 56, 31);
            ConsoleUtils.DrawWordWrappedText("<<<       Battle log       >>>", 107, 12, 34, 60);

            var abilityUpText = ConsoleUtils.GenerateBorderedTextBox("Fireball", 25);
            ConsoleUtils.DrawImageAt(abilityUpText, 41, 33, 0.6f, ConsoleColor.Cyan, ConsoleColor.Gray);
            var abilityDownText = ConsoleUtils.GenerateBorderedTextBox("Hydropump", 25);
            ConsoleUtils.DrawImageAt(abilityDownText, 41, 39, 1.0f, ConsoleColor.Cyan, ConsoleColor.Gray);
            var abilityLeftText = ConsoleUtils.GenerateBorderedTextBox("Evil slash", 25);
            ConsoleUtils.DrawImageAt(abilityLeftText, 21, 36, 0.2f, ConsoleColor.Cyan, ConsoleColor.Gray);
            var abilityRightText = ConsoleUtils.GenerateBorderedTextBox("Nap time", 25);
            ConsoleUtils.DrawImageAt(abilityRightText, 61, 36, 1.0f, ConsoleColor.Cyan, ConsoleColor.Gray);


        }

        public static void Update()
        {
            Console.Clear();
            // String for header

        }


        private static readonly string title = @"
&&&&&&&&&&    &&&&&&&&&&&&&&&&&&&&&&&&&& &&&&&&&&&&&&&&&&&&    &&&&&&&      &&&&&&&&&&&   &&&&&&&&&&&     &&&&&&&&&&&     &&&&&&&&& &&&&&&&&
&&&&&&&&&&&  &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&    &&&&&&&&     &&&&&&&&&&&  $&&&&&&&&&&&   &&&&&&&&&&&&&&&   &&&&&&&&& &&&&&&&&
 &&&&&&&&&&& &&&&&&&&&$    &&&&&&  &&&&&&&&&&& &&&&&& &&&&&   &&&&&&&&&       &&&&&&&&&& &&&&&&&&&&&  &&&&&&&    &&&&&&&   &&&&&&&&& $&&&&& 
 &&&&&&&&&&&&&&&&&&&&&X    &&&&&&        &&&&  &&&&&&  &&&&   &&&&&&&&&&      &&&&&&&&&&&&&&&&&&&&&&  &&&&&&&     &&&&&&&  &&&&&&&&&&X&&&&& 
 &&&&&&&&&&&&&&&&&&&&&X    &&&&&&&&&&&         &&&&&&        &&&&$ &&&&&&     &&&&& &&&&&&&&&&&&&&&& &&&&&&&      &&&&&&&  &&&&&&&&&&&&&&&& 
 &&&&&& &&&&&&&& &&&&&X    &&&&&&&&&&&         &&&&&&       &&&&&&&&&&&&&$    &&&&& &&&&&&&& &&&&&&& $&&&&&&      &&&&&&&  &&&&&X&&&&&&&&&& 
 &&&&&& &&&&&&&  &&&&&X    &&&&&&              &&&&&&      &&&&&&&&&&&&&&&    &&&&& &&&&&&&& &&&&&&&  &&&&&&      &&&&&&&  &&&&&X &&&&&&&&& 
 &&&&&&  &&&&&&  &&&&&$    &&&&&&  $&&&&       &&&&&&      &&&&&&   &&&&&&$   &&&&&  &&&&&&  &&&&&&&  &&&&&&&    &&&&&&&   &&&&&X  &&&&&&&& 
&&&&&&&& &&&&& &&&&&&&&&&&&&&&&&&&&&&&&&     &&&&&&&&&&  &&&&&&&&  &&&&&&&&&&&&&&&&&& &&&&& &&&&&&&&&   &&&&&&&&&&&&&&&   &&&&&&&&  &&&&&&& 
&&&&&&&&       &&&&&&&&&&&&&&&&&&&&&&&&&     &&&&&&&&&&  &&&&&&&&  &&&&&&&&&&&&&&&&&&       &&&&&&&&&     &&&&&&&&&&&     &&&&&&&&  &&&&&&& ".Trim();

        private static readonly string frog = @"
--------------------------------------------------
--------------------------------------------------
--------------------------------==----------------
------------------------------*******=------------
---------------------------=+***@%***+**#*=-------
----------------------=******++*##**********=-----
-----------------=+*****##******************=-----
--------------*#++*%#%*%%+**************##*-------
-----------+*********+*##*******=======+=---------
---------**#######%#**#%##*#*+======*=------------
-------#******+********#++========+=--------------
-----*#*#**###***#*+=============+=---------------
----=#**********++**+========*===#----------------
-----+#*#*#*++++++++++++*+===#===*+++++-----------
-----=******+++++++*+***+++*=-+===++**+=----------
-----=*************#*+*+#+----====+++**=----------
-------+**#***#*#****#*--------+====**+=----------
-------------#**##***+*+=--------*==+**=----------
-------------=++****+++*=----------==++=----------
--------------------------------------------------".Trim();
    }
}