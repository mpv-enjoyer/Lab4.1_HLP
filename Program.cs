string? input = " ";
CustomIntStack stack = new();
while (true)
{
    Console.Write("> ");
    input = Console.ReadLine();
    if (input == null || input == "exit") continue;
    string[] split = input.Split();
    string command = split[0];
    int argument = split.Length > 1 ? int.Parse(split[1]) : 0;
    switch (command)
    {
        case "push":
            stack.Push(argument);
            Console.WriteLine("ok");
            break;
        case "pop":
            Console.WriteLine(stack.Pop());
            break;
        case "back":
            Console.WriteLine(stack.Back());
            break;
        case "size":
            Console.WriteLine(stack.Size());
            break;
        case "clear":
            stack.Clear();
            Console.WriteLine("ok");
            break;
        case "exit":
            Console.WriteLine("bye");
            return;
    }
}