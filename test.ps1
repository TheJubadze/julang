if ($null -eq $env:GOPATH) { $env:GOPATH = 'C:\dev\julang' }
go test lexer