# ModelPoseFixer  
一般的HumanoidのAスタンス&lt;->TスタンスをInspector上で一括変更できるScript,VRMモデルのポース直しに  
# How to Use  
[![Image from Gyazo](https://i.gyazo.com/f22c214f8645c41f3d75a7a4389a58c9.png)](https://gyazo.com/f22c214f8645c41f3d75a7a4389a58c9)  
適当なGameObjectにModelPoseFixer.csをアタッチして両腕の根本のGameObjectをLeft / Right Arm Rootにアタッチする  
"Object Getter"ボタンを押して指定したGameObject以下の小要素全取得  
設定したいポーズを決める(IsForcedTとAngleを使って)
"Model pose Fixer"ボタンを押してお好きなポース（Tスタンス/Aスタンス)に変更する  
あとは好きにする  
[応用]ExcludedBoneに角度変更したくないボーンの親を指定するとその親から下の角度は変更されなくなります、アクセサリ等にご使用ください  
# Screen Shot  
T to A pose  
[![Image from Gyazo](https://i.gyazo.com/dee83a4a063653beb2eaf4cb2e9afc41.gif)](https://gyazo.com/dee83a4a063653beb2eaf4cb2e9afc41)  
A to T pose  
[![Image from Gyazo](https://i.gyazo.com/98c5fef75285d121b4d14b92470996b9.gif)](https://gyazo.com/98c5fef75285d121b4d14b92470996b9)
# Environments  
WIndows 10  
Unity 2018.2.15f1  
UniVRM 0.51  
# License
[MIT License](https://github.com/yuzuka4573/ModelPoseFixer/blob/master/LICENSE)  
