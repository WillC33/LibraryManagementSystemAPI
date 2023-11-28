# LibraryManagementSystemAPI
An ASP Web API with EF Core + PostgreSQL to manage library loans (An apprenticeship project)

## Exercise 7: Perform a code review
The use of EF core provided an easy way to scaffold and create the Db quickly in line with the C# models. However, it does leave more room to think about optimised SQL and stored procedures (though these are unlikely necessary for such a system) This project handles connexion and querying of the database within an injectable class.

Overall, more SRP and abstraction would serve this system well if development were to continue but for the very basics it is functional on member and item CRUD as well as a basic loan functionality that checks users do not have outstanding loans. 

Due to time constraints, there are many improvements that could be made to the code . First and foremost, better checking on the loans functionality. At the moment, books can be loaned multiple times and with a manual frontend this could cause problems. If working with a scanner system this would be alleviated but elegant error handling would improve the robustness of the system. This handling would be vital for a full production system.  

Furthermore, it would be necessary to handle HTTPS, CORS, and auth policies for security as well as creating new tables within the Db to deal with auth if this system were to become useful. 

Ideally, I’d like to take the time to create testing but again, as a short project, such functionality was not possible. 
