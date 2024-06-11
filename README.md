# com.coverfroglibrary.core
>> Step 0
>> Moudle 추가

[ 명령어 ]
git submodule add https://github.com/coverfrog/com.coverfroglibrary.core Assets/com.coverfroglibrary.core
git submodule add [A] [B]

[ 명령어 정리]
[A] = 저장할 저장소 ( 미리 생성이 되어 있어야 하고 패키지 배포를 위한 것이므로 Public 설정 )
[B] = .git 을 기준으로 어디에 저장 할것인지? ( 미리 생성되어 있으면 안되고 미리 생성시 에러 )


>> Step 1
>> Runtime 폴더 추가
   1. 모든 폴더 표시 ( 솔루션 탐색기 문서 여러개 있는 아이콘 )
   2. Runtme 폴더 우클릭 
   3. 프로젝트에 포함

-------------------------------------------------------------------------------------

>> Step 2
>> if ( 이미 Dll 추가가 되어 있다면)
>>      Dll 활성화
>> else
>>      어셈블리 추가
   1. 참조 (우클릭)
   2. 참조 추가
   3. 참조할 Dll 찾아서 추가 
   4. 찾아보기탭 클릭
   5. 참조 활성화 (체크 표시) 

[ 유니티를 기본 경로로 설치한 경우 Dll 주소 ]
[ 버전에 맞게 수정 ]

C:\Program Files\Unity\Hub\Editor\2022.3.21f1\Editor\Data\Managed\UnityEditor.dll
C:\Program Files\Unity\Hub\Editor\2022.3.21f1\Editor\Data\Managed\UnityEngine.dll

-------------------------------------------------------------------------------------

>> Step 3
>> 어셈블리 정보 수정
   1. 프로젝트 우클릭
   2. 애플리케이션 
   3. 이름, 네임스페이스 수정 ( CoverFrog )
   4. 어셈블리 정보 수정

-------------------------------------------------------------------------------------

>> Step 4
>> 출력 경로 설정
   1. 프로젝트 우클릭 
   2. 속성
   3. 빌드 출력 경로 수정 ( Runtime\ )

-------------------------------------------------------------------------------------

>> Step 5
>> 빌드 이벤트 수정
   1. 프로젝트 우클릭
   2. 속성
   3. 빌드 이벤트 클릭
   4. 빌드 후 이벤트 명령줄에 다음과 같이 추가

[ 명령어 ]
[ 에러 1 : 파일 못찾음 -> Step 3 확인 ]
[ 에러 2 : 빌드 실패 -> Step 2 확인. 추가로 필요한 Dll 있는지 검토]

del "$(TargetDir)\UnityEngine.dll"
del "$(TargetDir)\UnityEngine.xml"


del "$(TargetDir)\UnityEditor.dll"
del "$(TargetDir)\UnityEditor.xml"


del "$(TargetDir)\Bee.BeeDriver2.dll"
del "$(TargetDir)\Bee.BinLog.dll"
del "$(TargetDir)\Bee.TinyProfiler2.dll"
del "$(TargetDir)\BeeBuildProgramCommon.Data.dll"
del "$(TargetDir)\ExCSS.Unity.dll"
del "$(TargetDir)\PlayerBuildProgramLibrary.Data.dll"
del "$(TargetDir)\ScriptCompilationBuildProgram.Data.dll"
del "$(TargetDir)\System.CodeDom.dll"
del "$(TargetDir)\Unity.Cecil.dll"
del "$(TargetDir)\Unity.CompilationPipeline.Common.dll"

del "$(TargetDir)\CoverFrog.pdb"


