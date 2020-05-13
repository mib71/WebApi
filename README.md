# WebApi
My exam in WebAPI at EC Education Helsingborg 2020-05-15

### Prerequisites
Add Migration and Update-Database in Packet Manager Console
```
Add-Migration "InitialMigration"
Update-Database
```
## Running the tests
```
https://localhost:5001/swagger
```
Auth "POST" hit **"Try it out"**
```
{
  "userName": "johndoe",
  "password": "Password#123"
}
```
**"Execute"**  
Copy the Response body "token" and paste it after pressing the **Authorize** button dont forget to type in "Bearer" under Value  
Exampel:
```
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiJhMThiZTljMC1hYTY1LTRhZjgtYmQxNy0wMGJkOTM0NGU1NzUiLCJVc2VyTmFtZSI6ImpvaG5kb2UiLCJBZG1pbiI6InRydWUiLCJuYmYiOjE1ODkzNTk4ODQsImV4cCI6MTU4OTM2MDE4NCwiaWF0IjoxNTg5MzU5ODg0fQ.Ht0sx7nlOu8Qbp07aqNmk6qFDXsLWkdJWibgsJAYbTs
```
Knock your self out
