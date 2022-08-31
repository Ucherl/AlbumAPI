# AlbumAPI

### What this project do
Task Description
> Design and create a RESTful API in ASP.NET Core. 
> The API should call, combine, and return 2 API endpoints into a single HTTP response.
> The response should show the combined collection so that each Album contains a collection of its Photos.
>
> Your API should consist of 2 operations; 
> one to return all the data from the above endpoints, 
> and one to return data relating to a single User Id.

### How to run this project

- [x] Install Git
- [x] Install Dotnet CLI
- [x] Open terminal and follow the pic below
![terminal](https://i.imgur.com/VA0kYhp.png)

### API Endpoint
- .../api/Albums/UserCollection/{userId}
![Postman](https://i.imgur.com/AIZX2QX.png)

### TODO
- [ ] refine logger
- [ ] create API endpoint to retrieve specified album/photo resource for internal usage
      -> the UserCollections API retrieve all the dataset then join for the user's result
      -> unnecessary IO


