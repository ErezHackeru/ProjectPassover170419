SELECT S.UserName, S.PassWord, S.CompanyName, P.ProductName
FROM Supplier S
INNER JOIN PRODUCTS P
ON S.ID = P.SupplierNumber
WHERE ProductName = 'Signature'