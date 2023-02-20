Imports System
Imports System.IO

Module Program
  Sub Main(args As String())
    Console.Write("Enter File Name:")
        dim fileName = console.readline()
        dim marksFile = new streamwriter(fileName,True)
        marksFile.close()
        dim response = ""
        do
            Console.Write("Enter Number of Students:")
            response = console.readline()
        loop until isnumeric(response)
        dim numStudents = Cint(response)
        dim names(0 to numStudents) as string
        dim marks(0 to numStudents) as decimal
        dim records = readFile(fileName,names,marks)
        console.writeline("{0} records read from file {1}",records,fileName)
        dim finish = False
        do while not finish
            displayMenu()
            dim choice = console.readline().toUpper()
            console.clear()
            select case choice
                case "1" : displayRecords(records,names,marks)
                case "2" : records = addRecords(records,names,marks)
                case "3" : editRecords(names,marks)
                ' case 4
                case "S" : saveRecords(records,names,marks,fileName)
                case "X" : finish = True
                case else: console.writeline("Invalid Choice")
            end select
            if not finish then
                console.write("Press any key to continue...")
                console.readline
            end if
        loop
        console.writeline("Program Finished")
    End Sub

    sub displayMenu()
        console.clear()
        Console.writeline("Menu:")
        Console.writeline("[1] Display All Records")
        Console.writeline("[2] Add Records")
        Console.writeline("[3] Edit Records")
        Console.writeline("[4] Display Highest Result")
        Console.writeLine("[s] Save Data")
        Console.writeline("[x] Exit")
        Console.write("Enter choice:")
    end sub

    function readFile(fileName as string, names() as string, marks() as decimal) as integer
    
        dim marksFile = new streamreader(fileName)
        dim records = 0
        do while not marksFile.endOfStream()           
            dim fields() = marksFile.readline().split(",")
            names(records) = fields(0)
            marks(records) = Cdec(fields(1)) 
            records = records + 1
        loop
        marksFile.close()
        return records
    end function

    sub displayRecords(records as integer,names() as string, marks() as decimal)
        console.writeline("{0} Records",records)
        for i = 0 to records - 1
            console.writeline("{0}: {1} {2}",i+1,names(i),marks(i))
        next i
    end sub

    sub saveRecords(records as integer,names() as string, marks() as decimal, fileName as string)
        dim marksFile = new streamwriter(fileName)
        for i = 0 to records - 1
            marksFile.writeline("{0},{1}",names(i),marks(i))
        next i
        marksFile.close()
        console.writeline("Saved {0} records",records)
    end sub


 
    function addRecords(records as integer,names() as string,marks() as decimal)
        dim moreNames = True
        do while moreNames
            console.Write("Enter student name: ")
            names(records) = console.readline()
            marks(records) = inputMark("Enter student mark:")
            records = records + 1
            console.writeline("Add another student [y|n]? ")
            if console.readline().toUpper() = "N" then
                moreNames = False
            end if
        loop
        return records
    end function   

    sub editRecords(names() as string,marks() as decimal)
        console.write("Enter record number:")
        dim record = cint(console.readline())
        console.Write("Enter student name: ")
        names(record-1) = console.readline()
        marks(record-1) = inputMark("Enter student mark:")
    end sub  

    function inputMark(prompt as string) as decimal
        dim mark = -1
        dim valid = False
        do 
            console.write(prompt)
            dim response = console.readline() 
            if not isnumeric(response) then
                console.write("Input not a number - ")
            else
                mark = cdec(response)
                if mark < 0.0 or mark > 100.0 then
                    console.write("Input must be between 0 and 100 - ")
                else
                    valid = True
                end if
            end if
        loop until valid
        return mark
    end function

    function inputMark2(prompt as string) as decimal
        console.write(prompt)
        dim response = console.readline()
        do while not isnumeric(response)
            console.write("Invalid Input - " & prompt)
            response = console.readline() 
        loop
        return Cdec(response)
    end function


End Module
