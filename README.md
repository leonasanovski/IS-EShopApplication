How to push and update the repository:
First thing you need to see if the git navigation is in the repository with *git remote -v*
If not, just navigate to the repo *git remote add origin https://github.com/your-username/your-repo.git*
1. You need to add all the new changes *git add -- . ':!.vs/'*. We add it like this because it makes problem with the .vs while adding.
2. You need to make the commit *git commit -m "Message"*
3. You need to push to the main branch *git push -u origin master*
