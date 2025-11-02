# Quest System 제작 가이드

퀘스트 시스템을 제작하기 위한 단계별 가이드입니다.  
이 가이드를 따라 Category, TaskAction, Target을 구현한 후 Task를 제작할 수 있습니다.

---

## Table of Contents
1. [Category (카테고리)](#category-카테고리)  
2. [TaskAction](#taskaction)  
3. [Target (타겟)](#target-타겟)  
4. [Task 제작](#task-제작)  

---

## Category (카테고리)

퀘스트의 종류를 구분하기 위한 카테고리입니다.  
Category는 재사용 가능하므로 명확한 이름 설정이 중요합니다.

### 생성 방법
1. 우측 마우스 클릭 → **Create → Category**  
2. 기본 생성명: `category_`  
3. 이름 수정 예시: `category_battle`

<img width="500" height="500" alt="category 생성 예시" src="https://github.com/user-attachments/assets/327443ed-d1dd-419c-b6de-31fe1c0221d2" />

---

## TaskAction

퀘스트 진행 중 특정 조건이 만족되었을 때 실행되는 함수입니다.

**예시:**  
- 퀘스트 목표: 버튼을 2회 클릭  
- 버튼 클릭 시 SuccessCount 증가 후 퀘스트 진행 상태 업데이트  

---

## Target (타겟)

퀘스트 목표 달성 여부를 판별하는 모듈입니다.  

- 오브젝트, 문자열(String), 콜라이더 등 다양한 방식으로 판별 가능  
- 추상 클래스로 Target을 정의하고, 필요한 방식으로 상속하여 구현합니다.  

### 예시: StringTarget 구현

<img width="500" height="500" alt="StringTarget 예시" src="https://github.com/user-attachments/assets/163b1ab6-137e-4dcc-b435-3c7dbedfa4e6" />

> Task의 Target 모듈에 연결하면, 트리거가 발생할 때 QuestSystem으로 전달되어 목표물과 Category가 일치하는지 판별합니다.

---

## Task 제작

위 3가지 요소(Category, TaskAction, Target)가 준비되면 Task를 제작할 수 있습니다.

<img width="500" height="500" alt="Task 제작 예시 1" src="https://github.com/user-attachments/assets/083c612f-15de-4882-afb3-f3dc88221a77" />

<img width="500" height="500" alt="Task 제작 예시 2" src="https://github.com/user-attachments/assets/3a342ffe-745d-4b71-872a-2606cf46d935" />

---

### 💡 Tip
- Category는 재사용 가능하므로, 명확한 이름으로 관리  
- Target은 추상화하여 다양한 목표 조건을 지원  
- TaskAction과 Target이 정확히 연결되어야 퀘스트가 올바르게 작동  
