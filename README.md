# Introduction
Welcome to Sportz Battle Project! This game is actually created for people who always want to compete with friends, newbies and also with themselves. Just login from your loved social media and give access permission for the application and you are ready to start.

# Getting Started
In this section I will show to you how the current project appears on the Unity3d Game Engine. First let’s have a deal about the version. You must use only the latest Unity3d version. Some cowardly developers can always say that you should use only LTS versions (Long Term Support versions). If you are one of them - please wake up. The LTS version was created to support very old and very big projects. It means if just updating content for your project costs more then $100k - transferring this game and repeating the full QA process will be mega expensive. Also 3rd party SDKs like Facebook will not wait on your project.

You must keep the project on last Unity available at the day that you read this file.

Because:
1. New Unity version keeps fresh native templates for Xcode and for Android Studio, even if you see just the executable file - middle step of Unity - built through the generated project template. Without a fresh version you can find problems from CPU supporting to physical file size increasing.
2. Keeping the last 3rd Party SDKs - because they are not LTS - they just shut down after some time.
3. Support last Unity improvements - application will live in new OS environments and updates and will be much stable.
4. Easy track and fixing coming errors from migration. Much easier to track and fix errors in one version than in 5 or 10.

**Little more about Unity version:**
Using currently for build: **2021.1.11f1** (please update this version after successfully migration).

About subversions. If you will move from **2021.1.11f1** to **2021.1.10f1** - this from the start will appear problems. 
Because project manifest and different files updated by internal script updater. For downshifting your project - just use the Assets folder in the newly created blank project.

**Installation process:**
1. Go to https://unity3d.com/get-unity/download
2. Select Download Unity Hub and install it.
3. Open and login in your unity account (if you don’t have it visit https://id.unity.com)
4. Then select license
5. Go to installs and select Add button
6. Select last version (WARNING: NOT LTS - THIS OPTION SELECTED BY DEFAULT)
7. Select platforms that you need (DON’T UNCHECK ANDROID SDK TOOLS THIS IMPORTANT EVEN IF YOU USE FEW UNITY VERSIONS THEY USE DIFFERENT JDKs).
8. Install

After Unity installation go to the **Projects Tip**. Now you have 2 options. Or open a project or create a new project and import the project inside. These 2 ways are important if auto update scripts will be totally losing references inside the project (this can happen if you jump on the 3 or 4 version later).

Let's see this 2 ways in detail.

**Option 1. Clean opening
**
Download and unarchive project in place where you store it. The in Project Tip find Add button:
![](https://dev.azure.com/dewmakerdesign/80a7211d-fc70-4611-a6c3-92b1f0257007/_apis/git/repositories/d58d891c-e6e0-4ef5-b11b-40f596f20219/items?path=%2FReadmeImages%2Fimage8.png&versionDescriptor%5BversionOptions%5D=0&versionDescriptor%5BversionType%5D=0&versionDescriptor%5Bversion%5D=main&resolveLfs=true&%24format=octetStream&api-version=5.0)


Navigate to your project. You will see this folders:
![](https://dev.azure.com/dewmakerdesign/80a7211d-fc70-4611-a6c3-92b1f0257007/_apis/git/repositories/d58d891c-e6e0-4ef5-b11b-40f596f20219/items?path=%2FReadmeImages%2Fimage5.png&versionDescriptor%5BversionOptions%5D=0&versionDescriptor%5BversionType%5D=0&versionDescriptor%5Bversion%5D=main&resolveLfs=true&%24format=octetStream&api-version=5.0)

Then click open. After some time Unity will appear the project.



**Option 2. New Project creating
**

For this select **New Button**:
![](https://dev.azure.com/dewmakerdesign/80a7211d-fc70-4611-a6c3-92b1f0257007/_apis/git/repositories/d58d891c-e6e0-4ef5-b11b-40f596f20219/items?path=%2FReadmeImages%2Fimage9.png&versionDescriptor%5BversionOptions%5D=0&versionDescriptor%5BversionType%5D=0&versionDescriptor%5Bversion%5D=main&resolveLfs=true&%24format=octetStream&api-version=5.0)

Then select Render Template (this is important or your release file will be physically big and also you will have a lack of performance. Select Mobile 2D (you don’t need advanced render for Sprites)

![](https://dev.azure.com/dewmakerdesign/80a7211d-fc70-4611-a6c3-92b1f0257007/_apis/git/repositories/d58d891c-e6e0-4ef5-b11b-40f596f20219/items?path=%2FReadmeImages%2Fimage2.png&versionDescriptor%5BversionOptions%5D=0&versionDescriptor%5BversionType%5D=0&versionDescriptor%5Bversion%5D=main&resolveLfs=true&%24format=octetStream&api-version=5.0)

Then find project inspector:
![](https://dev.azure.com/dewmakerdesign/80a7211d-fc70-4611-a6c3-92b1f0257007/_apis/git/repositories/d58d891c-e6e0-4ef5-b11b-40f596f20219/items?path=%2FReadmeImages%2Fimage6.png&versionDescriptor%5BversionOptions%5D=0&versionDescriptor%5BversionType%5D=0&versionDescriptor%5Bversion%5D=main&resolveLfs=true&%24format=octetStream&api-version=5.0)

Select the Assets folder by right mouse click and select Show in Explorer (Finder if you are on the Mac). In the opened folder put the unarchived Assets folder of your old project (Warning: don't copy Assets folder in Assets folder, just copy content of old Assets folder and put in new Assets folder).

Back to the Unity engine. Project processing, validation and etc start.

# Build and Test
After passing the Getting Started section to test your project you can use Android Build for the test project.
(Test on real device is very important).
Go to main menu **File ->  Build Settings -> Select Android and click Switch Platform button.**

If you see Unity sign on the platform it means that you switched on this platform successfully.
![](https://dev.azure.com/dewmakerdesign/80a7211d-fc70-4611-a6c3-92b1f0257007/_apis/git/repositories/d58d891c-e6e0-4ef5-b11b-40f596f20219/items?path=%2FReadmeImages%2Fimage3.png&versionDescriptor%5BversionOptions%5D=0&versionDescriptor%5BversionType%5D=0&versionDescriptor%5Bversion%5D=main&resolveLfs=true&%24format=octetStream&api-version=5.0)

Choose Build and select the folder where you want apk builded.

After compilation, install the apk on your Android phone.

**Done!**