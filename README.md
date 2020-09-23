VIKKE Mobile Application
===

## Download Application
[<img src="https://play.google.com/intl/en_us/badges/static/images/badges/en_badge_web_generic.png"  alt="Google Play Badge" width="200" />](https://play.google.com/store/apps/details?id=fi.HAMKSmart.VIKKE)

[<img src="https://developer.apple.com/app-store/marketing/guidelines/images/badge-example-preferred.png" alt="App Store Badge" />](https://apps.apple.com/us/app/vikke/id1481186013?ls=1)

## Project Contributors

[<img src="https://www.rakennerahastot.fi/documents/10179/54846/EU_EAKR_FI_vertical_20mm_rgb.png" alt="ERDF and ESF EU flag logo in Finnish" width="200" />](https://www.rakennerahastot.fi)

[<img src="https://www.rakennerahastot.fi/documents/10179/55439/VipuvoimaaEU_2014_2020_rgb.png/2a05d239-e940-4301-928f-82078c2959bf?t=1404911191222" alt="Vipuvoimaa EU:lta logo" width="200"/>](https://www.rakennerahastot.fi)

[<img src="https://www.hameenliitto.fi/wp-content/uploads/2020/02/2020-02-hameenliitto-logo-rgb-vaaka.jpg" alt="Regional Council of Häme logo" width="350"/>](https://www.hameenliitto.fi)

[<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/6/6d/HAMK_yhdistelma_vari_wikipedia.svg/1280px-HAMK_yhdistelma_vari_wikipedia.svg.png" alt="Häme University of Applied Sciences logo" width="200"/>](https://hamk.fi)

## About

VIKKE mobile application for Android and iOS was created as a part of the Virtual Cultural Tourism in Tavastia Region (VIKKE) -project. The application takes the user to exciting journeys though the region with stories revolving around variety of themes, such as the middle ages, Jean Sibelius and The Ox Road of Häme. The user is encouraged to visit various placed and cultural destinations and solve mysterious puzzles at each location in order to complete a story.

| :information_source: | For more information about the VIKKE project, please visit https://www.hamk.fi/vikke |
| -------- | -------- |

### Used Tools :hammer_and_wrench: 

* Unity 2019.3.3f1
    * Android Target Support
        * Android SDK 29
        * Android NDK 19.0.5232133
    * iOS Target Support

For iOS development:
* Xcode 11.4.1

### CI/CD Integration :rocket: 

* Jenkins 2.242
* MacOS 10.15.4
* Xcode 11.4.1

The repository contains a Jenkinsfile that can be used to automate the build process. Please refer to the [documentation](#Jenkins-Configuration) at the end of the Readme for configuring Jenkins.

## Application Features

### MAXST Image Tracker :camera: 

Application utilizes the MAXST AR SDK 5.0.1, specifically the Image Tracker. Some puzzles require using the mobile device's camera to scan a certain image trigger to view AR content over the camera feed.

Puzzles containing AR content are:
* Mierola Bridge (Hattula)
* Girl giving milk to a calf -statue (Hämeenlinna Art Walk)
* Statue of Paavo Cajander (Hämeenlinnä Art Walk)
* War and Love -statue (Hämeenlinna Art Walk)
*removed from the public repository as requested by the Lotta Svärd Foundation*

The AR portion is implemented in a separate Scene that is loaded when the AR scanner button is pressed in the Task view.

### Localization :finland: :gb: 

Localization is implemented using [Lean Localization](https://assetstore.unity.com/packages/tools/localization/lean-localization-28504), a free third party tool from Unity Asset Store. There is a `LeanLocalization` GameObject at the root of each Scene that contains all the available translations. Each GameObject that has localized content contains a Lean Localized child component.

### Content Containers :package:

All content related to the stories are stored in content sciptables stored as assets. In Unity they can be found under`Assets/Resources/Content`.

Each Story can contain multiple Locations; each Location in turn can hold one Task.

The original design was that one Location could be used under multiple Stories. However, it was later found out that while a certain physical location could be shared by multiple stories, small amount of content would change between the stories. This resulted in multiple Location containers sharing a name having almost identical content, aside from i.e the story sprite changing. This is why some Location containers have (brackets) to identify the story it is part of.

There are some Location containers that do get shared identically across different stories. They are stored under `Content/_Shared Locations`. These Locations use the same Task container across stories; these are stored under `Content/_Shared Tasks`.

### Unity Assets :file_cabinet:

| Filepath                              | Comment                                                                                                      |
| ------------------------------------- | ------------------------------------------------------------------------------------------------------------ |
| Animations                            | Animations for AR content                                                                                    |
| Editor/MaxstAR                        | Imported with MAXST SDK                                                                                      |
| Editor/MaxstAR/Textures               | Imported with MAXST Target Manager unitypackage                                                              |
| Editor/Testing                        | Folder for test suite                                                                                        |
| Editor/Testing/PlayTests.asmdef       | Assembly definition, references other assemblies to access scripts and NSubstitute to utilize it for testing |
| Editor/IPlistEditor.cs                | Imported with MAXST SDK                                                                                      |
| Editor/VIKKEColours.colors            | Swatch library for application colours                                                                       |
| Fonts/                                | Imported fonts                                                                                               |
| Graphics/AR/                          | Graphics used by AR content                                                                                  |
| Graphics/Icons                        | Icons used in app                                                                                            |
| Graphics/Icons/App icons/             | Android and iOS icons                                                                                        |
| Sprites/Background/                   | Sprites used as i.e view backgrouns                                                                          |
| Sprites/Content/                      | Sprites used by stories, locations and tasks                                                                 |
| Sprites/Logo/                         | VIKKE logo                                                                                                   |
| Sprites/Maps/                         | Map sprites for LocationlistView                                                                             |
| Sprites/Splash/                       | Sprites used in splashscreen                                                                                 |
| Lean/                                 | Imported with LeanLocalization                                                                               |
| Lean/Lean.asmdef                      | Assembly definition, referenced by GameScriptsAssembly to access LeanLocalization scripts                    |
| Materials/                            | Materials used in app                                                                                        |
| MaxstAR/                              | Imported with MAXST SDK                                                                                      |
| MaxstAR/MAXST.asmdef                  | Assembly definition, referenced by GameScriptsAssembly to access MAXST SDK scripts                           |
| Models/                               | Models used in the game                                                                                      |
| Plugins/                              | Imported with MAXST SDK                                                                                      |
| Plugins/NSubstitute.dll               | Imported to utilize NSubstitute in testing, referenced by PlayTests                                          |
| Resources/Content/                    | Contains content scriptables; stories, locations and tasks                                                   |
| Resources/MaxstAR/                    | Imported with MAXST SDK                                                                                      |
| Resources/MaxstAR/Configuration.asset | Asset used to configure the MAXST SDK                                                                        |
| Resources/Prefabs/AR/                 | AR related prefabs                                                                                           |
| Resources/Prefabs/UI elements/        | Prefabs used within view prefabs                                                                             |
| Resources/Prefabs/Views/              | Prefabs used as application views                                                                            |
| Resources/VersionCodes.json           | Used for version management by VersionCode.cs, stores version number, Android bundle code and iOS build code |
| Scenes/ARScene.unity                  | Scene used for the AR content                                                                                |
| Scenes/MainScene.unity                | Main scene used in the application                                                                           |
| Scripts/                              | Scripts used by the application                                                                              |
| Scripts/GameScriptsAssembly.asmdef    | Assembly definition, referenced by PlayTests to access game scripts                                          |
| Shaders/VertexColorUnlit              | Imported with the Farm Animals assets                                                                        |
| StreamingAssets/                      | Imported with MAXST Target Manager unitypackage                                                              |
| TextMeshPro                           | Imported with TextMesh Pro                                                                                   |

## Scripting

Please check the source code for more detailed documentation on the scripts used in the application.

### MVP Architecture :diamond_shape_with_a_dot_inside: 

MVP stands for Model - View - Presenter. Each View prefab has a View script attached to it, which is backed by a Presenter script. The View scripts are responsible for registering user interactions and changing the view, while Presenter scripts handling user interactions and passing data to and from the view. You could say the Views are the 'eyes' and the Presenters are the 'brains'. Model scripts on the other hand are responsible for data related tasks.

Views and Models talk only to Presenters, but not directly to each others.

### Navigation :compass:

#### NavigationManager

The `NavigationManager` script is the centralized script that handles navigation between the different views within the application. It has a list of paths that can be taken within the application, each path having a starting view, a direction or a state i.e 'FORWARD' and a destination view.

A button or other interactive object in a view would have a script attached to it that would have the direction or state defined in it. This would get passed on to the Presenter script along with the View that registered the interaction. The base Presenter script would then be used to call the NavigationManager using the direction and the originating View as parameters in order to switch to a new View.

#### List of UI Paths

| Origin           | Direction | Destination      |
| ---------------- | --------- | ---------------- |
| StartView        | FORWARD   | StorylistView    |
| StorylistView    | BACK      | StartView        |
| StorylistView    | FORWARD   | StorytellingView |
| LocationlistView | BACK      | StorylistView    |
| LocationlistView | FORWARD   | StorytellingView |
| TaskView         | BACK      | LocationlistView |
| TaskView         | STORYEND  | StorytellingView |
| TaskView         | FORWARD   | LocationlistView |
| StartView        | INFO      | CreditsView      |
| CreditsView      | BACK      | StartView        |

## Jenkins Configuration

### Used Software

* Jenkins 2.242
* [install-unity](https://github.com/sttz/install-unity) tool
Installed with Homebrew
* OpenJDK8
Installed with Homebrew
* Android SDK 29
Installed with Unity via Unity Hub, located under user home /Library/Android/sdk
* Android NDK 19.0.5232133
Installed manually with sdkmanager, located under user home /Library/Android/sdk/ndk

| :warning: | Installing an Unity Editor with Install-Unity tool **does not** install the Android SDK or NDK  (as of v2.4.0) |
| -------- | -------- |

* Jenkins plugins
    * Pipeline
    * Credentials
    * NUnit

### Global Configuration

#### Environment Variables
* Apple ID
    * Key: APPLEID
    * Value: Your Apple ID e-mail address

Used in the pipeline to upload the iOS application package with Application Loader to App Store / Testflight
	
* Modified PATH
    * Key: PATH+EXTRA
    * Value: /usr/local/bin

Used to modify system environment variable so that install-unity command can be called inside the Pipeline. 

#### Credentials

* ALTOOLPW
	* Application Loader password, used to upload iOS application package to App Store / TestFlight
* EXPORTPLIST
	* Export Options properties list used when creating the iOS application package
* VIKKEKEYSTORE
	* Java keystore for signing Android app package
* VIKKEKSPW
	* Password for keystore
* VIKKEKEY
    * Password for the key in the keystore

### Setup Shenanigans

#### Git LFS

While Homebrew installed Git LFS to `/usr/local/opt/git-lfs/bin/git-lfs`, Jenkins insisted on trying to find the binary in `/Applications/Xcode.app/Contents/Developer/usr/libexec/git-core`. This was resolved by simply copying the binary installed with Homebrew to the location Jenkins wanted to use.

#### Xcode Signing

Xcode and signing the iOS application package caused a few headaches when setting up Jenkins.

First attempt to sign the application package in Jenkins was with manual signing, using the following steps:
1. Sign in to Apple Developer account in Xcode
2.  Download Developer Cert from Apple Developer website and add it to MacOS keychain
3.  Download Provisioning Profile from Apple Developer website and install it
4.  Add Developer Team ID and Provisioning Profile to Unity project in project settings
	*  Player -> Settings for iOS tab -> Other settings -> Identification
5.  Create exportOptions.plist file with export options
	*  method: app-store
	*  teamID: can be found in Apple Developer page
	*  teamName: can be found in Apple Developer page
	*  signingStyle: manual
	*  signingCertificate: cert name, can be seen in keychain access
	*  ProvisioningProfiles: dictionary, key is bundle ID and value profile name
6.  Add Plist file to Jenkins Credentials
7.  User "codesign" might ask for user password during build, Allow always
	*  This can happen again if new cert is added to user keychain

After issues with manual signing, signing was switched to automatic:

1.  Check "Automatic Signing" in Unity iOS project settings
	*  Player -> Settings for iOS tab -> Other settings -> Identification
2.  Create exportOptions.plist file with export options
	*  method: app-store
	*  teamID: can be found in Apple Developer page
	*  teamName: can be found in Apple Developer page
	*  signingStyle: manual
3.  Add Plist file to Jenkins Credentials

| :warning: | If you attempt to use automatic signing straight ahead, you may still have to do steps 1 to 3 listed under the manual signing steps.  |
| -------- | -------- |

***

| :information_source: | View the README on [Hackmd.io](https://hackmd.io/qbeJGJBHQcCn3F21SbR55A) |
| -------- | -------- |