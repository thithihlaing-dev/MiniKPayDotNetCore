# MiniKPayDotNetCore

## Kpay
Mobile No

Me - Another One

Id FullName MobileNo Balance Pin => 000000

Bank => Deposit / Withdraw

### Deposit
Deposit API => MobileNo, Balance (+) => 1000 + (-1000)

### Withdraw
Withdraw API => MobileNo, Balance (+) => 1000 - (-1000) at least 10,000 MMK

### Transfer
Transfer API =>

FromMobileNo
ToMobileNo 
Amount 
Pin 
Notes

- FromMobile check
- ToMobileNo check
- FromMobileNo != ToMobileNo
- Pin ==
- Balance
- FromMobileNo Balance -
- ToMobileNo Balance +
- Message (Complete)
- Transaction History

- Balance
- Create Wallet User
- Login
- Change Pin
- Phone No Change
- Forget Password
- Reset Password
- First Time Login


-------------------------------

dotnet ef dbcontext scaffold "Server=.;Database=DotNetTrainingBatch5;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -f (wtite in vs cmd)
OR
dotnet ef dbcontext scaffold "Server=.;Database=DotNetTrainingBatch5;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -t Tbl-Name -f (wtite in vs cmd)