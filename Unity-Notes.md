### General Notes

- Every game runs on what is called a Game Engine. The Engine tells the components of a computer stuff like:
    1. how to render graphcis
    2. play audio
    3. how to perform collisions 
    4. scripting 

- The first decision a game developer has to make: what engine should I choose?

### Information I used when doing my project

- 720x1280 is the most common mobile screen resolution - add it to the resolution list in the Game tab.
- to switch between screens using buttons: https://youtu.be/7KR5IKi8m8g
- remove background of image: https://www.youtube.com/watch?v=aNIHO3eNcqw
- making round corners of rectangle in inkscape to make a rounded corner button: https://superuser.com/questions/640954/inkscape-rounding-corners-of-shapes
- remove background to make png image: www.remove.bg
- load a scene after another after a certain delay: https://www.youtube.com/watch?v=Oe9BZVnoedE
- building project/producing apk for mobile: https://www.youtube.com/watch?time_continue=151&v=Ska81xpB-Po&feature=emb_logo
- how to set up version control for Unity projects: https://www.youtube.com/watch?time_continue=300&v=TbdWXbRlPo0&feature=emb_logo
- how to push to my own remote branch and always push to it instead of master:
    1. `git push -u origin master:Sarah` 
    *explanation:* (where origin is the nickname I gave for the remote repo when I first set up the connection between the local and the remote, master is my local, and Sarah is my branch in the remote repo)
    2. `git config push.default upstream` 
    *explanation:* (this configures the push command to always push to the "upstream" that I have predefined in the previous command by saying `-u origin master:Sarah`; in which `-u` is for upstream. Upstream is like a pipeline from the local `master` to the remote `Sarah`. 
    **Why did we need this command?** 
    The thing is, the previous command defines the pipeline for the `pull` command only, so next time we do a `pull` it will pull from the upstream defined, while `push` by default pushes to the branch with a matching name.
    Since my local branch is called `master` and the remote branch I want to push to is called `Sarah`, they will not match, so Git will automatically go looking for a branch called `master` in the remote repo, and it will push to it. To change this behaviour, we used this command, which basically changed the default setting for the `push` command. Now the `push` will by default push to the upstream that I have set, not to the branch with the same name as my local.
    ref: https://stackoverflow.com/questions/8170558/git-push-set-target-for-branch
- how to use Visual Studio Code with Unity: https://www.youtube.com/watch?v=_v9mc3dcwYE
### Unity Tips and Tricks

- click 'W' on keyboard to move a game object
- click 'R' on keyboard to resize a game object
- *size* property of the Main Camera is the range which any object inside the scene will be able to move vertically (up and down) if it wants to be viewed by the camera.
for eg: if the size is 3, this means that any game object inside the main camera view will be able to have a value of y from 0 to 3 (up) and from 0 to -3 (down). If you give it a y value that is higher than 3 or lower than -3, it can not be seen by the main camera and hence will not be visible in the game.
What about the horizontal size of the camera? 
It depends on the resolution you choose for the camera. This can be configured from the "Game" tab window, which is usually beside the "Scene" tab window.
- simulating a custom button click (3D button being pressed): https://www.youtube.com/watch?v=CJ8FKjYtrT4