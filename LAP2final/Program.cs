// See https://aka.ms/new-console-template for more information
using System;
using System.Transactions;

class levelConstructor
{
    private int width;
    private int height;
    private char floor = '\u25A0';
    private int x = 0;                              //player location x
    private int y = 0;                              //player location y
    private int[,,] boss1 = new int[2, 7, 1];       //boss 1 location [x, y, alive]
    private int[,,] boss2 = new int[13, 13, 1];     //boss 2 location [x, y, alive]
    private int[,,] boss3 = new int[5, 18, 1];      //boss 3 location [x, y, alive]
    private char mc = '\u25A1';
    private char boss = '\u25A2';

    //char[,] playground = new char[0, 0];

    //public char[,] playground;

    public levelConstructor(int width, int height)
    {
        this.width = width;
        this.height = height;
        char[,] playground = new char[width, height];

        if(width < 16)                                  //make boss 3 dead if size less than large
        {
            this.boss3 = new int[5, 18, 0];
        }
        if(width < 11)                                  //make boss 2 dead if size less than medium
        {
            this.boss2 = new int[13, 13, 0];
        }
        
}
    public char[,] buildPlayground()
    {
        char[,] playground = new char[width,height];
        for(int i = 0; i < playground.GetLength(0); i++)
{
            for (int j = 0; j < playground.GetLength(1); j++)
            {
                playground[i, j]= this.floor;
                //Console.WriteLine("hi");
            }
            //Console.WriteLine();
        }
        return playground;
    }
    public void display(char [,] playground)
    {

        for (int i = 0; i < playground.GetLength(0); i++)
        {
            for (int j = 0; j < playground.GetLength(1); j++)
            {
                Console.Write(playground[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
    public char getmc()         //mc getter
    {
        return this.mc;
    }

    public int getX()           //player x getter
    {
        return this.x;
    }

    public int getY()           //player y getter
    {
        return this.y;
    }

    public void setX(int x)     //player x setter
    {
        this.x = x;
    }

    public void setY(int y)     //player y setter
    {
        this.y = y;
    }

    public int[,,] getBoss1()   //boss 1 getter
    {
        return this.boss1;
    }

    public int[,,] getBoss2()   //boss 2 getter
    {
        return this.boss2;
    }

    public int[,,] getBoss3()   //boss 3 getter
    {
        return this.boss3;
    }

    public void play(levelConstructor field, char[,] playground)    //Play Loop
    {
        while(true)
        {
            if(this.getBoss1().GetLength(2) == 0 && this.getBoss2().GetLength(2) == 0 && this.getBoss3().GetLength(2) == 0) //game over check
            {
                Console.WriteLine("Congratulations! You won the game!");
                break;
            }
            else if (this.x == this.getBoss1().GetLength(0) && this.y == this.getBoss1().GetLength(1) && this.getBoss1().GetLength(2)==1)   //boss 1 check
            {
                //boss 1 quiz
                field.display(playground);
                Console.WriteLine("Boss 1 Quiz.");
                Console.WriteLine("Polymorphism means one function can be used in different ways.");
                Console.WriteLine("1. True");
                Console.WriteLine("2. False");
                Console.WriteLine("your move now ");
                Nullable<int> choice = int.Parse(Console.ReadLine());

                if(choice == 1)         //Correct answer
                {
                    Console.Clear();
                    Console.WriteLine("Correct! Boss 1 is defeated.");
                    this.boss1 = new int[2, 7, 0];     //kill boss
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Wrong! Try again.");
                }
            }
            else if (this.x == this.getBoss2().GetLength(0) && this.y == this.getBoss2().GetLength(1) && this.getBoss2().GetLength(2) == 1) //boss 2 check
            {
                //boss 2 quiz
                field.display(playground);
                Console.WriteLine("Boss 2 Quiz.");
                Console.WriteLine("Encapsulation is a process of making a property or method private so that it is unable to be accessed directly.");
                Console.WriteLine("1. True");
                Console.WriteLine("2. False");
                Console.WriteLine("your move now ");
                Nullable<int> choice = int.Parse(s: Console.ReadLine());

                if (choice == 1)         //Correct answer
                {
                    Console.Clear();
                    Console.WriteLine("Correct! Boss 2 is defeated.");
                    this.boss2 = new int[13, 13, 0];     //kill boss
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Wrong! Try again.");
                    playground[field.getX(), field.getY() - 1] = this.mc;
                }
            }
            else if (this.x == this.getBoss3().GetLength(0) && this.y == this.getBoss3().GetLength(1) && this.getBoss3().GetLength(2) == 1) //boss 3 check
            {
                //boss 3 quiz
                field.display(playground);
                Console.WriteLine("Boss 3 Quiz.");
                Console.WriteLine("Abstraction is a process of inheriting all properties of parent class into itself.");
                Console.WriteLine("1. True");
                Console.WriteLine("2. False");
                Console.WriteLine("your move now ");
                Nullable<int> choice = int.Parse(s: Console.ReadLine());

                if (choice == 2)         //Correct answer
                {
                    Console.Clear();
                    Console.WriteLine("Correct! Boss 3 is defeated.");
                    this.boss3 = new int[5, 18, 0];     //kill boss
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Wrong! Try again.");
                }
            }
            else            //no alive boss on player x and y
            {
                //display bosses
                playground[field.getBoss1().GetLength(0), field.getBoss1().GetLength(1)] = this.boss;
                if (this.width > 10)
                {
                    playground[field.getBoss2().GetLength(0), field.getBoss2().GetLength(1)] = this.boss;
                }

                if (this.width > 15)
                {
                    playground[field.getBoss3().GetLength(0), field.getBoss3().GetLength(1)] = this.boss;
                }

                //display playground to user
                field.display(playground);

                Console.WriteLine("your move now ");

                //movement check
                char movementInput = char.Parse(Console.ReadLine());
                int[,] currentLocation = this.Movement(movementInput, field.getX(), field.getY());
                playground = field.buildPlayground();
                field.setX(currentLocation.GetLength(0));
                field.setY(currentLocation.GetLength(1));
                playground[field.getX(), field.getY()] = this.mc;
            }
        }
    }

    private int[,] Movement(char movementInput, int x, int y)       //movement handler
    {
        int[,] newLocation = new int[x, y];
        Console.Clear();

        switch (movementInput)
        {
            case 'w':
                Console.WriteLine("You moved upward");
                newLocation = new int[x - 1, y];
                break;
            case 's':
                Console.WriteLine("You moved downward");
                newLocation = new int[x + 1, y];
                break;
            case 'a':
                Console.WriteLine("You moved to the left");
                newLocation = new int[x, y - 1];
                break;
            case 'd':
                Console.WriteLine("You moved to the right");
                newLocation = new int[x, y + 1];
                break;
            default:
                Console.WriteLine("Invalid input.");
                break;

        }

        return newLocation;
    }
}
class program
{

    static void Main()
    {
        //main menu
        Console.WriteLine("Hello World");
        Console.WriteLine("There are 3 levels:");
        Console.WriteLine("1. Small ");
        Console.WriteLine("2. Medium ");
        Console.WriteLine("3. Large");
        Console.WriteLine("Input what flavours you most! (1,2,3?)");

        Nullable<int> level = int.Parse(s: Console.ReadLine());
        int size= 10;
        
        switch (level)
        {
            case 1:
                size=10;
                Console.WriteLine("You chose Small");
                break;
            case 2:
                size = 15;
                Console.WriteLine("You chose Medium");
                break;
            case 3:
                size = 20;
                Console.WriteLine("You chose Large");
                break;

        }

        //building the playground
        levelConstructor field = new levelConstructor(size, size);
        char[,] playground = field.buildPlayground();

        //Setting Character in the playground
        playground[field.getX(), field.getY()] = field.getmc();

        //the play loop starts
        field.play(field, playground);
    }
}