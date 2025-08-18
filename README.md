# 🐉 몬스터 개발 툴 (MonsterMaker)

## 📖 개요
`MonsterMaker`는 몬스터 제작 과정을 자동화하여, **데이터 생성 → 프리팹 제작 → 설정 → 편집**을 한 번에 관리할 수 있도록 돕는 Unity 에디터 툴입니다.  
직접 일일이 데이터를 만들고 프리팹을 설정하던 과정을 단축하여, **재사용성과 생산성을 높이는 것**이 목적입니다.  

---

## 1. 메뉴 확인
Unity 실행 후 상단 메뉴 바에 **MonsterMaker** 항목이 생성됩니다.  

![menu](https://github.com/user-attachments/assets/c198240c-9055-461a-a4b1-18dd160aa9ee)

현재는 **Forest** 카테고리만 제공됩니다.  
- `MonsterMaker → Forest → Forest Monster Create` 선택  



## 2. 몬스터 생성
버튼 클릭 시 생성 창이 열리며, 필요한 값을 입력 후 **[generate]** 버튼을 누르면 몬스터 데이터가 자동으로 생성됩니다.  

![create_1](https://github.com/user-attachments/assets/eec942e6-e529-48a9-bc67-b9ec1dcf5346)  
![create_2](https://github.com/user-attachments/assets/6f1c21e3-4211-43af-b944-4d7247c9c05c)

📂 생성 경로:   Assets/ForestMonster/CreateData/
- `Data` 폴더 → 몬스터 정보 저장  
- `Prefab` 폴더 → 몬스터 프리팹 저장  

---

## 3. 프리팹 설정
생성된 프리팹을 **더블클릭**하여 편집 모드로 들어갑니다.  

![prefab_edit](https://github.com/user-attachments/assets/c4e410f8-147e-4936-b03a-8965ac98e5f9)

### (1) MonsterHandler
- 자식 오브젝트 **MonsterHandler** 선택  
- **Layer** → *Monster* 로 변경  
- **Box Collider** → 피격 판정 영역으로 설정  
  - `Animaction` 오브젝트의 `SpriteRenderer → FlipX`가 체크된 상태에서 맞춰야 정확합니다  

![hitbox](https://github.com/user-attachments/assets/a0623304-5941-4cf9-8335-fdef9b209c63)



### (2) Monster Controller
- `States List` → 몬스터 상태 데이터 연결  
  - `MonsterStates<T>` 상속 후 상태 패턴 구현 필요  
- `MonsterAniManager` → 애니메이션 데이터 연결  
  - 생성 경로: `Project → Create → MonsterAnimaction → AnimactionData`  
  - `PlayName` = Animator 파라미터 이름  

⚠️ **FlipX 보정**  
1. 게임 실행 후 Flip 체크/해제하면서 **OffsetX** 값 조정  
2. 올바른 위치 확인 후 값 복사  
3. 실행 종료 후 프리팹에 값 적용  

---

### (3) Animaction 오브젝트
- 몬스터의 **Animator Controller** 연결  
---
![animator](https://github.com/user-attachments/assets/664680d3-550f-4e78-a862-c72f11d100c6)

 

### (4) DetectionRange
- 플레이어 **탐지 범위** 설정  
- 인지범위 버튼 활성화 하며 기즈모 생성됨 `Size`, `Offset` 값 보고 조정  

---

### (5) FootCollider
- 바닥 충돌 감지를 위한 **발 부분 콜라이더** 설정  

---

## 4. 몬스터 데이터 편집
- 메뉴: `MonsterMaker → Forest → Forest Monster Edit`  

![edit_1](https://github.com/user-attachments/assets/c152afd1-6c25-423c-8043-3c11a3b9d566)

좌측에서 수정할 데이터를 선택하면 상세 정보가 표시됩니다.  

![edit_2](https://github.com/user-attachments/assets/42d42063-2b7d-435f-9ea1-42571fc33a6f)

모든 값을 수정한 뒤 반드시 **[Save Changes]** 버튼을 눌러야 저장됩니다.  

---
# 몬스터 사용설명서
기본적으로 몬스터 씬에 소환하면 알아서 작동합니다.  


