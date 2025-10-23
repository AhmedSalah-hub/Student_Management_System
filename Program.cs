namespace Student_Management_System
{
    class Student
    {
        public int StudentId;
        public string Name;
        public int Age;
        public List<Course> Courses = new List<Course>();

        public bool Enroll(Course course)
        {
            if (course == null)
                return false;

            for (int i = 0; i < Courses.Count; i++)
            {
                if (Courses[i].CourseId == course.CourseId)
                    return false;
            }

            Courses.Add(course);
            return true;
        }

        public string PrintDetails()
        {
            if (Courses.Count == 0)
                return $"ID: {StudentId}, Name: {Name}, Age: {Age}, Courses: None";

            string allCourses = "";
            for (int i = 0; i < Courses.Count; i++)
            {
                allCourses += Courses[i].Title;
                if (i < Courses.Count - 1) allCourses += ", ";
            }

            return $"ID: {StudentId}, Name: {Name}, Age: {Age}, Courses: {allCourses}";
        }
    }

    class Instructor
    {
        public int InstructorId;
        public string Name;
        public string Specialization;

        public string PrintDetails()
        {
            return $"ID: {InstructorId}, Name: {Name}, Specialization: {Specialization}";
        }
    }

    class Course
    {
        public int CourseId;
        public string Title;
        public Instructor Instructor;

        public string PrintDetails()
        {
            string insName = Instructor != null ? Instructor.Name : "No Instructor";
            return $"ID: {CourseId}, Title: {Title}, Instructor: {insName}";
        }
    }

    class SchoolStudentManager
    {
        public List<Student> Students = new List<Student>();
        public List<Course> Courses = new List<Course>();
        public List<Instructor> Instructors = new List<Instructor>();

        public bool AddStudent(Student s)
        {
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].StudentId == s.StudentId)
                    return false;
            }
            Students.Add(s);
            return true;
        }

        public bool AddInstructor(Instructor i)
        {
            for (int j = 0; j < Instructors.Count; j++)
            {
                if (Instructors[j].InstructorId == i.InstructorId)
                    return false;
            }
            Instructors.Add(i);
            return true;
        }

        public bool AddCourse(Course c)
        {
            for (int k = 0; k < Courses.Count; k++)
            {
                if (Courses[k].CourseId == c.CourseId)
                    return false;
            }
            Courses.Add(c);
            return true;
        }

        public Student FindStudent(int id)
        {
            for (int i = 0; i < Students.Count; i++)
            {
                if (Students[i].StudentId == id)
                    return Students[i];
            }
            return null;
        }

        public Course FindCourse(int id)
        {
            for (int i = 0; i < Courses.Count; i++)
            {
                if (Courses[i].CourseId == id)
                    return Courses[i];
            }
            return null;
        }

        public Instructor FindInstructor(int id)
        {
            for (int i = 0; i < Instructors.Count; i++)
            {
                if (Instructors[i].InstructorId == id)
                    return Instructors[i];
            }
            return null;
        }

        public bool EnrollStudentInCourse(int studentId, int courseId)
        {
            Student s = FindStudent(studentId);
            Course c = FindCourse(courseId);

            if (s == null || c == null)
                return false;

            return s.Enroll(c);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            SchoolStudentManager manager = new SchoolStudentManager();
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("===== Student Management System =====");
                Console.WriteLine("1 - Add Student");
                Console.WriteLine("2 - Add Instructor");
                Console.WriteLine("3 - Add Course");
                Console.WriteLine("4 - Enroll Student in Course");
                Console.WriteLine("5 - Show All Students");
                Console.WriteLine("6 - Show All Courses");
                Console.WriteLine("7 - Show All Instructors");
                Console.WriteLine("8 - Find Student by ID or Name");
                Console.WriteLine("9 - Find Course by ID or Name");
                Console.WriteLine("10 - Check if Student Enrolled in Specific Course");
                Console.WriteLine("11 - Return Instructor Name by Course Name");
                Console.WriteLine("12 - Exit");
                Console.Write("\n==> ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "1")
                {
                    Student s = new Student();
                    Console.Write("Enter Student ID: ");
                    s.StudentId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Name: ");
                    s.Name = Console.ReadLine();
                    Console.Write("Enter Age: ");
                    s.Age = Convert.ToInt32(Console.ReadLine());

                    if (manager.AddStudent(s))
                        Console.WriteLine("Student added successfully!");
                    else
                        Console.WriteLine("Student ID already exists!");
                }
                else if (choice == "2")
                {
                    Instructor i = new Instructor();
                    Console.Write("Enter Instructor ID: ");
                    i.InstructorId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Name: ");
                    i.Name = Console.ReadLine();
                    Console.Write("Enter Specialization: ");
                    i.Specialization = Console.ReadLine();

                    if (manager.AddInstructor(i))
                        Console.WriteLine("Instructor added successfully!");
                    else
                        Console.WriteLine("Instructor ID already exists!");
                }
                else if (choice == "3")
                {
                    Course c = new Course();
                    Console.Write("Enter Course ID: ");
                    c.CourseId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Course Title: ");
                    c.Title = Console.ReadLine();
                    Console.Write("Enter Instructor ID: ");
                    int insId = Convert.ToInt32(Console.ReadLine());

                    c.Instructor = manager.FindInstructor(insId);
                    if (c.Instructor == null)
                        Console.WriteLine("Instructor not found!");
                    else if (manager.AddCourse(c))
                        Console.WriteLine("Course added successfully!");
                    else
                        Console.WriteLine("Course ID already exists!");
                }
                else if (choice == "4")
                {
                    Console.Write("Enter Student ID: ");
                    int sid = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Course ID: ");
                    int cid = Convert.ToInt32(Console.ReadLine());

                    if (manager.EnrollStudentInCourse(sid, cid))
                        Console.WriteLine("Student enrolled successfully!");
                    else
                        Console.WriteLine("Failed to enroll student.");
                }
                else if (choice == "5")
                {
                    Console.WriteLine("\nAll Students:");
                    if (manager.Students.Count == 0)
                        Console.WriteLine("No students found.");
                    else
                        for (int i = 0; i < manager.Students.Count; i++)
                            Console.WriteLine(manager.Students[i].PrintDetails());
                }
                else if (choice == "6")
                {
                    Console.WriteLine("\nAll Courses:");
                    if (manager.Courses.Count == 0)
                        Console.WriteLine("No courses found.");
                    else
                        for (int i = 0; i < manager.Courses.Count; i++)
                            Console.WriteLine(manager.Courses[i].PrintDetails());
                }
                else if (choice == "7")
                {
                    Console.WriteLine("\nAll Instructors:");
                    if (manager.Instructors.Count == 0)
                        Console.WriteLine("No instructors found.");
                    else
                        for (int i = 0; i < manager.Instructors.Count; i++)
                            Console.WriteLine(manager.Instructors[i].PrintDetails());
                }
                else if (choice == "8")
                {
                    Console.Write("Enter Student ID or Name: ");
                    string input = Console.ReadLine();
                    int id;
                    Student found = null;

                    bool isId = int.TryParse(input, out id);
                    if (isId)
                        found = manager.FindStudent(id);
                    else
                    {
                        for (int i = 0; i < manager.Students.Count; i++)
                        {
                            if (manager.Students[i].Name.ToLower() == input.ToLower())
                                found = manager.Students[i];
                        }
                    }

                    if (found != null)
                        Console.WriteLine(found.PrintDetails());
                    else
                        Console.WriteLine("Student not found.");
                }
                else if (choice == "9")
                {
                    Console.Write("Enter Course ID or Name: ");
                    string input = Console.ReadLine();
                    int id;
                    Course found = null;

                    bool isId = int.TryParse(input, out id);
                    if (isId)
                        found = manager.FindCourse(id);
                    else
                    {
                        for (int i = 0; i < manager.Courses.Count; i++)
                        {
                            if (manager.Courses[i].Title.ToLower() == input.ToLower())
                                found = manager.Courses[i];
                        }
                    }

                    if (found != null)
                        Console.WriteLine(found.PrintDetails());
                    else
                        Console.WriteLine("Course not found.");
                }
                else if (choice == "10")
                {
                    Console.Write("Enter Student ID: ");
                    int sid = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Course Name: ");
                    string cname = Console.ReadLine();

                    Student s = manager.FindStudent(sid);
                    if (s != null)
                    {
                        bool enrolled = false;
                        for (int i = 0; i < s.Courses.Count; i++)
                        {
                            if (s.Courses[i].Title.ToLower() == cname.ToLower())
                                enrolled = true;
                        }

                        if (enrolled)
                            Console.WriteLine($"{s.Name} is enrolled in {cname}");
                        else
                            Console.WriteLine($"{s.Name} is not enrolled in {cname}");
                    }
                    else
                        Console.WriteLine("Student not found.");
                }
                else if (choice == "11")
                {
                    Console.Write("Enter Course Name: ");
                    string cname = Console.ReadLine();

                    Course c = null;
                    for (int i = 0; i < manager.Courses.Count; i++)
                    {
                        if (manager.Courses[i].Title.ToLower() == cname.ToLower())
                            c = manager.Courses[i];
                    }

                    if (c != null && c.Instructor != null)
                        Console.WriteLine($"Instructor for {c.Title} is {c.Instructor.Name}");
                    else
                        Console.WriteLine("Course or instructor not found.");
                }
                else if (choice == "12")
                {
                    running = false;
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("Goodbye!");
        }
    }
}
