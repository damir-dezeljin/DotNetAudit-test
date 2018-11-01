# `Audit.NET` and EF
This is a demo program prepared to facilitate debugging of a multi table to single table audit - see also the [Invalid cast exception ...](https://stackoverflow.com/questions/53055428/invalid-cast-exception-when-audit-logging-from-multiple-tables-to-a-single-ef-au) thread on Stackoverflow.

## List of existing GIT tags
* `singleTblIssue`    -> demonstrates the issue described in on Stackoverflow.
* `View_WorkAround`   -> a workaround using a `DbQuery` on a database view which aggregates audit events from two sepparate tables using an `UNION ALL` SQL statement.
* `Fixed_Lib`         -> an updat library Audit.EntityFramework.Core (v13.2.0) fixed the problem.
