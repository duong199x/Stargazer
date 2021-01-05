# About

Project name: **Stargazer** \
Description: \
Target Platform: **mobile devide** (android, ios) \
Target Audience: \
Engine: **Unity 2019.4.17ft LTS**

# Technical Note

## Git commmit

- Only commit when project is runnable and no bug detected
- Note every change (add, edit, fixbug) on the comment
- Always write comment when committing. Apply these notation at first of comment lines
  - Add new feature: **_+_** \
    `+ add boss at level 1` \
    `+ add player combat system` \
    `+ add particle effect for magic1, magic4, magic9`
  - Edit: **_-_** \
    `- edit enemy1 behaviour: from walking randomly to chase after player` \
    `- change structure of map1-3`
  - FixBug: _ \
    `_ fixbug #9669: player not die when HP drop to 0`

## Naming Convention

### Files & Folders

Apply prefix to prefab files: pref\__filename_ \
Animator: *SpriteName*Animator (Example: `TinyCatAnimator`)\
Animation: _SpriteName_@_action_ (Example: `Bird@fly`, `TinyCat@sleep`)

### Code

- Capitalize
  - Classname: `CapitalizeFirstLetter` (Example: `PlayerController`, `EnemyAI`)
  - Constant: `CAPITALIZE_ALL_LETTER` (Example: `MAX_ENEMY_ALLOW`)
  - Class property: `prefixCapitalizeFirstLetter` (Example: `iMaxHP`)
  - Class method: `CapitalizeFirstLetter` (Example: `SummonAncientGod()`, `Attack()`)
  - Enum (Example: `enum Direction {UP, DOWN, LEFT, RIGHT};`)
    - Name: `CapitalizeFirstLetter`
    - Value: `CAPITALIZE_ALL_LETTER`
- Variable prefix
  - int: **_i_** \
    `int iNumOfEnemy`, `int iMagicIndex`
  - uint: **_ui_** \
    `uint uiColorHexCode`
  - long: **_l_** \
    `long lBossHP`
  - float, double: no prefix \
    `float damage`, `float positionX`
  - bool: no prefix, but name must be an affirmation, using **_is/isDoing/have_** \
    `bool isImpostor`, `bool isUsingSuperPower`, `bool haveGirlfriend`
  - string: **_str_** \
    `string strGameoverMessage`
  - array: **_a_** \
    `int[] aiPlayerId`, `Item[] aStoreItem`, `bool[] aIsVisitted`, `string[] astrGirlTalks`
- Member vs Local
  - Member of a class: apply prefix **\_m\_\_** \
    `public int m_iCurrentHP`, `private float m_maxMana`
  - Static member of a class: apply prefix **\_s\_\_** \
    `private static string s_strDefaultName`
- Comment: add tag to comment
  - **_TODO_** is used to indicate planned enhancements. \
    `// TODO: add code to create effect here`
  - **_FIXME_** is used to mark potential problematic code that requires special attention and/or review. \
    `// FIXME: this only world when player have ManaPoint > 5`
  - **_NOTE_** is used to document inner workings of code and indicate potential pitfalls. \
    `// NOTE: this medthod must be call after DoSomething() is called`
  - **_BUGFIX #bugId_** is used to indicate that the piece of code is added/modified in order to fix some bug. \
    `// BUGFIX #6996: game crash when player click`

## Folder Structure

References: [Mastering Unity Project Folder Structure: Assets Organization](http://developers.nravo.com/mastering-unity-project-folder-structure-level-2-assets-organization/#.X2rD5mgza00) \
GameDesign: contain concept arts, documents, etc... \
MagicBattlefield\Asset: Unity dev folder.
