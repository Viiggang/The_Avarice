# 몬스터 개발 툴  
【사용법】유니티 들어가면 상단 툴메뉴 바에 『MonsterMaker』라고 하나 생겼을 것이다.  
<img width="288" height="68" alt="화면 캡처 2025-08-17 232733" src="https://github.com/user-attachments/assets/c198240c-9055-461a-a4b1-18dd160aa9ee" />  
클릭 후
지금은 숲 관련 몬스터 밖에 없어서 Forest 카테고리밖에 없다.  
forest monster create를 클릭하면   
<img width="340" height="353" alt="화면 캡처 2025-08-17 232357" src="https://github.com/user-attachments/assets/eec942e6-e529-48a9-bc67-b9ec1dcf5346" />  
<img width="331" height="345" alt="화면 캡처 2025-08-17 232538" src="https://github.com/user-attachments/assets/6f1c21e3-4211-43af-b944-4d7247c9c05c" />  
위에 『첫 번째 사진』 같이 나타나고 클릭을 하면 『두 번째 사진』과 같이 나타남  
그다음 필요한 값들을 넣고 생성을 눌러주면 데이터가 생성이된다.  


Assets/ForestMonster/CreateData/ 경로에 몬스터가 사용할 『데이터 폴더』와  『프리팹 폴더』가 생길것이다.    
그러면 프리팹 폴에서 들어가서 먼저 해야할 것들을 알려주겠다.  

우선 프리팹을 더블클릭 하여 프리팹 수정을# 몬스터 개발 툴  
【사용법】유니티 들어가면 상단 툴메뉴 바에 『MonsterMaker』라고 하나 생겼을 것이다.  
<img width="288" height="68" alt="화면 캡처 2025-08-17 232733" src="https://github.com/user-attachments/assets/c198240c-9055-461a-a4b1-18dd160aa9ee" />  
클릭 후
지금은 숲 관련 몬스터 밖에 없어서 Forest 카테고리밖에 없다.  
forest monster create를 클릭하면   
<img width="340" height="353" alt="화면 캡처 2025-08-17 232357" src="https://github.com/user-attachments/assets/eec942e6-e529-48a9-bc67-b9ec1dcf5346" />  
<img width="331" height="345" alt="화면 캡처 2025-08-17 232538" src="https://github.com/user-attachments/assets/6f1c21e3-4211-43af-b944-4d7247c9c05c" />  
위에 『첫 번째 사진』 같이 나타나고 클릭을 하면 『두 번째 사진』과 같이 나타남  
그다음 필요한 값들을 넣고 생성을 눌러주면 데이터가 생성이된다.  


Assets/ForestMonster/CreateData/ 경로에 몬스터가 사용할 『데이터 폴더』와  『프리팹 폴더』가 생길 것 이다.    
그러면 프리팹 폴에서 들어가서 먼저 해야할 것들을 알려주겠다.  

우선 프리팹을 더블클릭 하여 프리팹 수정을 할수있게 해준다.  


1.프리팹 자식 오브젝트의 MonsterHandler 클릭 후 피격 판정 영역 설정 및 몬스터 레이어 설정을 해줘야한다.  
Layer가 몬스터인지 확인 후 박스 콜라이더 설정 해주세요 박스 콜라이더는 피격판정 범위 이기 때문에 잘 설정해주시길 바랍니다.  

그 다음  『monster controller』스크립트 영역의 아랫부분에 states list부분에 몬스터의 상태 데이터를 넣어주면된다.  
상태 데이터는 MonsterStates<T> 상속 후 상태패턴 구현 후 적당한 폴더에 데이터를 생성 하면 된다.  

그 다음 밑에 MonsterAniManager 컴포넌트가 보일건데 애니메이션 데이터를 넣으면 된다.  
데이터 생성은 프로젝트 폴더 영역에서 우클릭 -> Create -> MonsterAnimaction -> AnimactionData를 생성 하면 된다.  
생성된 데이터를 보면  『playname』 이라고 나와있는데 애니메이션 파라미터 값을 넣어주면 된다.  

2. Animaction 오브젝트는 만들려고하는 몬스터의 애니메이터 컨트롤을 연결해주면 된다.

3. DetectionRange 이거는 몬스터가 플레이어를 탐지하는 영역을 만들어주는 부분이다.  
인지범위 활성화 버튼을 눌러 활성화상태에서 size와 offset을 설정 해주면된다.

4.FootCollider 이건 몬스터가 바닥에 닿을수 있게 해주려는 콜라이더 설정이다 발쪽으로  콜라이더 설정 해주면 된다.  

그 프리팹 몬스터 데이터값 변경인데  
『MonsterMaker』->  『Forest』-> 『forest monster edit』 눌러주면 
<img width="999" height="523" alt="image" src="https://github.com/user-attachments/assets/c152afd1-6c25-423c-8043-3c11a3b9d566" />  
영역이 나오는데 우측 만들어진 데이터를 클릭 하면  

<img width="1010" height="516" alt="image" src="https://github.com/user-attachments/assets/42d42063-2b7d-435f-9ea1-42571fc33a6f" />  
요렇게 데이터를 수정할수 있게 변한다. 값을 다 수정 후 『save Changes』 눌러줘야 한다.






