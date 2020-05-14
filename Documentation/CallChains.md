# Call Chains

- [CRUD an Application](#crud-an-application)
- [Get Application Users](#get-application-users)
- [CRUD a User](#crud-a-user)

## CRUD an Application

```mermaid
graph TD;
Client[WebAPI] --> ApplicationManager[Application Manager]
ApplicationManager[Application Manager] --> ClaimsAccessor[Claims Accessor]
ClaimsAccessor[Claims Accessor] --> ClaimsResource(Claims)
ApplicationManager[Application Manager] --> ApplicationsAccessor[Applications Accessor]
ApplicationsAccessor[Applications Accessor] --> ApplicationsResource(Applications)
ApplicationManager[Application Manager] --> MembershipsAccessor[Memberships Accessor]
MembershipsAccessor[Memberships Accessor] --> MembershipsResource(Memberships)
```

## Get Application Users

```mermaid
graph TD;
Client[WebAPI] --> ApplicationManager[Application Manager]
ApplicationManager[Application Manager] --> ActiveDirectoryAccessor[Active Directory Accessor]
ActiveDirectoryAccessor[Active Directory Accessor]--> AAD(Azure Active Directory)
ApplicationManager[Application Manager] --> ApplicationsAccessor[Applications Accessor]
ApplicationsAccessor[Applications Accessor] --> ApplicationsResource(Applications)
```

## CRUD a User

```mermaid
graph TD;
Client[WebAPI] --> AuthorizationManager[Authorization Manager]
AuthorizationManager[Authorization Manager] --> ActiveDirectoryAccessor[Active Directory Accessor]
ActiveDirectoryAccessor[Active Directory Accessor] --> AAD(Azure Active Directory)
AuthorizationManager[Authorization Manager] --> ClaimsAccessor[Claims Accessor]
ClaimsAccessor[Claims Accessor] --> ClaimsResource(Claims)
AuthorizationManager[Authorization Manager] --> ApplicationsAccessor[Applications Accessor]
ApplicationsAccessor[Applications Accessor] --> ApplicationsResource(Applications)
```
