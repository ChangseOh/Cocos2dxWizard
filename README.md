# Cocos2dxWizard
It's a simple tool that creates options for cocos.bat.<br>
It can create for cocos2dx project from version 3.4 to 4.0 available.<br>
<br>
<img src="https://private-user-images.githubusercontent.com/8163689/517968136-6451434c-17a4-4654-84b9-6529f3199007.jpg?jwt=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3NjM5NjQ0MTQsIm5iZiI6MTc2Mzk2NDExNCwicGF0aCI6Ii84MTYzNjg5LzUxNzk2ODEzNi02NDUxNDM0Yy0xN2E0LTQ2NTQtODRiOS02NTI5ZjMxOTkwMDcuanBnP1gtQW16LUFsZ29yaXRobT1BV1M0LUhNQUMtU0hBMjU2JlgtQW16LUNyZWRlbnRpYWw9QUtJQVZDT0RZTFNBNTNQUUs0WkElMkYyMDI1MTEyNCUyRnVzLWVhc3QtMSUyRnMzJTJGYXdzNF9yZXF1ZXN0JlgtQW16LURhdGU9MjAyNTExMjRUMDYwMTU0WiZYLUFtei1FeHBpcmVzPTMwMCZYLUFtei1TaWduYXR1cmU9ODI4YzVkZjBjYWVhNDU3ZjcyNGUzNmZkNTJlNzA4ZTM4NWEwYjkyNDY3ZjBlYTQ2YWY2NTM0MGVmNjI5YzExNSZYLUFtei1TaWduZWRIZWFkZXJzPWhvc3QifQ.uC461kwtUByTn-DARQaU7HOJ-5eXR2tRKjcQ8HIesMU"><br>
<br>
＊Tools-Console folder : [cocospath]/tools/cocos2d-conseol/bin/coco.bat<br>
＊Project Name : It'll create project folder with same name.<br>
＊Package Name : as its name suggests.<br>
＊Language : can select c++, JS, Lua. but I use only c++ so I cannot guarantee that they will work.<br>
＊Dest Folder : Project's parent folder<br>
＊Force : Device's direction when work on Android or iOS.<br>
＊Log : You can see final log after created project.<br>
＊Create Project : run cocos.bat<br>
＊Reset : delete all option<br>
<br>
<br>
2022-08-06 update<br>
＊You can set up win32 projects automatically with auto cmake checkbox.<br>
＊auto edit CmakeLists.txt for include all cpp/h files in Classes folder<br>
<br>
<br>
ex) auto edit CmakeLists.txt<br>
<img src="https://private-user-images.githubusercontent.com/8163689/517968134-35ed1095-6dda-42bf-b8a8-a8505e5458c3.jpg?jwt=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3NjM5NjQ0MTQsIm5iZiI6MTc2Mzk2NDExNCwicGF0aCI6Ii84MTYzNjg5LzUxNzk2ODEzNC0zNWVkMTA5NS02ZGRhLTQyYmYtYjhhOC1hODUwNWU1NDU4YzMuanBnP1gtQW16LUFsZ29yaXRobT1BV1M0LUhNQUMtU0hBMjU2JlgtQW16LUNyZWRlbnRpYWw9QUtJQVZDT0RZTFNBNTNQUUs0WkElMkYyMDI1MTEyNCUyRnVzLWVhc3QtMSUyRnMzJTJGYXdzNF9yZXF1ZXN0JlgtQW16LURhdGU9MjAyNTExMjRUMDYwMTU0WiZYLUFtei1FeHBpcmVzPTMwMCZYLUFtei1TaWduYXR1cmU9OWZlNDVjMjJlNjFhMWM5OWRlZjUxYWMxY2YxZTA2Yzg5MTczYmU1NGVhY2UzYWZjMzFkM2U4YmM5N2JjY2ZlZSZYLUFtei1TaWduZWRIZWFkZXJzPWhvc3QifQ.j7DwYcrnV8-7_gR1eFaQHG7gBDeNXyFMd0Azjq10i9E"><br>
<br>
<br>
ex) auto cmake win32 project<br><br>
<img src="https://private-user-images.githubusercontent.com/8163689/517968133-8122ab8d-658e-4267-8ae4-bd2b9b56578e.jpg?jwt=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJnaXRodWIuY29tIiwiYXVkIjoicmF3LmdpdGh1YnVzZXJjb250ZW50LmNvbSIsImtleSI6ImtleTUiLCJleHAiOjE3NjM5NjQ0MTQsIm5iZiI6MTc2Mzk2NDExNCwicGF0aCI6Ii84MTYzNjg5LzUxNzk2ODEzMy04MTIyYWI4ZC02NThlLTQyNjctOGFlNC1iZDJiOWI1NjU3OGUuanBnP1gtQW16LUFsZ29yaXRobT1BV1M0LUhNQUMtU0hBMjU2JlgtQW16LUNyZWRlbnRpYWw9QUtJQVZDT0RZTFNBNTNQUUs0WkElMkYyMDI1MTEyNCUyRnVzLWVhc3QtMSUyRnMzJTJGYXdzNF9yZXF1ZXN0JlgtQW16LURhdGU9MjAyNTExMjRUMDYwMTU0WiZYLUFtei1FeHBpcmVzPTMwMCZYLUFtei1TaWduYXR1cmU9MWE1NTgxNTU4NDVmNDEwZDI2MjgxOTNmYjZkMDc0MzUyYWU0Yzg4NjE0NGI1NTk2OTIyNTBlYmNjYTRlYWEyZiZYLUFtei1TaWduZWRIZWFkZXJzPWhvc3QifQ.PFRCE5EyFvUEUH8bsr32GzOCZfcp3ZisKcC9NRjksFw"><br>
<br>
<br>
CAUTION : available cocos2d-x 3.4~4.0, but cmake function recommend for 4.0.<br>
