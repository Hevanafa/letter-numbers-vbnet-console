Imports System.IO

Public Class Game
    Dim lives% = 3
    Dim letters% = 0

    Dim question_word$

    Dim done = False
    Dim next_word = True
    Dim correct% = 0

    Dim r As New Random

    ' Source: https://gist.github.com/deekayen/4148741
    Dim words$()


    Sub PrintLives()
        Console.Write("Lives: ")

        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine(StrDup(lives, "♥"))

        Console.ResetColor()
    End Sub


    Sub LoadWordList()
        Using sw As New StreamReader("wordlist.txt")
            Try
                words = sw.ReadToEnd().Split(vbCrLf)
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                Console.Write("Press Enter to exit...")
                Console.ReadLine()
                Environment.Exit(1)
            End Try
        End Using

        Console.WriteLine("Loaded {0} words", words.Length)
        Console.WriteLine()
    End Sub


    Function GetCode$()
        GetCode = String.Join(" ", LCase(question_word).ToCharArray.Select(Function(c) Asc(c) - 96))
    End Function


    Public Sub New()
        Console.ResetColor()

        LoadWordList()

        Console.WriteLine("Welcome to Guess the Word by the Numbers!")
        Console.WriteLine("By Hevanafa, 31-10-2023")
        Console.WriteLine("Try to guess the word by just seeing the index of the letter it belongs to!")
        Console.WriteLine("A is 1, B is 2, and so on until Z, which is 26")
        Console.WriteLine()


        PrintLives()

        Do
            If next_word Then
                question_word = words(r.Next(0, words.Length))
                next_word = False
            End If

            Console.WriteLine(GetCode)

            Dim input$ = Console.ReadLine()

            Console.SetCursorPosition(Len(input) + 2, Console.CursorTop - 1)

            If input = "q" Then
                'done = True
                lives = 0
            ElseIf LCase(Trim(input)) = LCase(question_word) Then  ' .ToCharArray.Select(Function(c) Asc(c)).All(Function(n)
                correct += 1
                letters += Len(question_word)
                next_word = True

                ' give red X or green V to indicate that the player is correct or not
                Console.ForegroundColor = ConsoleColor.Green
                Console.Write("[V]")
            Else
                lives -= 1
                Console.ForegroundColor = ConsoleColor.Red
                Console.Write("[X]")
            End If

            Console.ResetColor()
            Console.SetCursorPosition(0, Console.CursorTop + 1)

            If lives > 0 Then
                PrintLives()
            Else
                Console.WriteLine("Game over!")
                Console.WriteLine("You got {0} correct answers, which have {1} letters in this session", correct, letters)
                done = True
            End If
        Loop Until done
    End Sub


End Class
