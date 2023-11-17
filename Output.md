Starting MongoDB repository tests...

1. CheckConnection_DbAvailable_ConnectionEstablished
   - Connection established: PASS

2. CheckConnection_DbNotAvailable_ConnectionFailed
   - Connection failed: PASS

3. GetAllUsers_ReadAllUsers_CountIsExpected
   - Users retrieved: PASS

4. GetUserByField_GetUserByNameAndUserExists_UserReturned
   - User found by name: PASS

5. GetUserByField_GetUserByBlogAndUserExists_UserReturned
   - User found by blog: PASS

6. GetUserByField_GetUserByNameAndUserDoesntExist_UserNotReturned
   - User not found by name: PASS

7. GetUserByField_WrongField_UserNotReturned
   - User not found by wrong field: PASS

8. GetUserCount_JustFirstElement_Success
   - Users count as expected: PASS

9. InsertUser_UserInserted_CountIsExpected
   - User inserted: PASS

10. DeleteUserById_UserDeleted_GoodReturnValue
    - User deleted: PASS

11. DeleteUserById_UserDoesntExist_NothingIsDeleted
    - No user deleted for non-existing ID: PASS

12. DeleteAllUsers_DelitingEverything_Sucess
    - All users deleted: PASS

13. UpdateUser_UpdateTopLevelField_UserModified
    - User top-level field updated: PASS

14. UpdateUser_UpdateTopLevelField_GoodReturnValue
    - Update operation returned true: PASS

15. UpdateUser_TryingToUpdateNonExistingUser_GoodReturnValue
    - Update operation returned false for non-existing user: PASS

16. UpdateUser_ExtendingWithNewField_GoodReturnValue
    - Update operation returned true for extending with new field: PASS

All tests passed successfully.

Finished MongoDB repository tests.
