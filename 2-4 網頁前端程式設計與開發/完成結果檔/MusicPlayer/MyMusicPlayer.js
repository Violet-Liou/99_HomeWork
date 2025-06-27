var myMusic = document.getElementById("myMusic");

        //播放&暫停
        function play() {
            if (myMusic.paused) {
                myMusic.play();
                document.getElementById("control1").innerHTML = ";";

                
                setTimeInitial();
                console.log("播放音樂");
            } else {
                myMusic.pause();
                document.getElementById("control1").innerHTML = "4";
            }
        }

        //停止
        function stop() {
            myMusic.pause();
            //重製播放時間
            myMusic.currentTime = 0;
            myMusic.pause(); //音樂暫停
            document.getElementById("control1").innerHTML = "4"; //修改播放符號
        }

        //快轉&快退
        function backOrForward(v) {
            myMusic.currentTime += v;
        }

 ////////【音樂進度條】//////////////////////////////////////////////////////////////////////////////////////////////////////
        var title = document.getElementById("title");
        var titlebar = document.getElementById("titlebar");
        var time = document.getElementById("time");
        var text = document.getElementById("status");

        //時間格式
        function formatTime(t) {
            var minutes = parseInt(t.toFixed(0) / 60);
            var seconds = parseInt(t.toFixed(0) % 60);

            minutes = minutes < 10 ? "0" + minutes : minutes;
            seconds = seconds < 10 ? "0" + seconds : seconds;

            return minutes + ":" + seconds;
        }
       
        //點擊音樂進度條
        function setTime(){
            myMusic.currentTime = titlebar.value / 10000; //初始化音樂當前時間
        }
        

        //更新音樂進度條
        function setProcessRange(){
            var currentTime = myMusic.currentTime; //目前播放時間
            var duration = myMusic.duration; //音樂總長度

            time.innerHTML = formatTime(currentTime) + " / " + formatTime(duration); //顯示時間
            titlebar.value = currentTime *10000;

            var percent = (currentTime / duration) * 100; //計算進度條百分比
            //塗音量條的顏色
            titlebar.style.backgroundImage = `linear-gradient(to right, rgb(157, 109, 219) 0%, rgb(109, 111, 219) ${percent}%,white ${percent}%)`;
        }

        //初始化音樂進度條
        function setTimeInitial(){
                titlebar.max = myMusic.duration*10000; //音樂的總長度
                setInterval(setProcessRange,10); //更新音樂進度條
        }

////////【歌曲控制(選曲、模式)】////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        var mode = document.getElementById("mode");

        //選擇歌曲
        function changeMusic(n) {
            var musicList = document.getElementById("musicList");
            var mainTitle = document.getElementById("mainTitle");
            var img = document.getElementById("img").children[0];
            var v = musicList.selectedIndex; //獲取目前選擇的歌曲索引
            var index = v + n; //計算新的歌曲索引
            if(index < 0) {
                index = musicList.length - 1; //如果索引小於0，則選擇最後一首歌
            } else if (index >= musicList.length) {
                index = 0; //如果索引大於歌曲數量，則選擇第一首歌
            }
            myMusic.src = musicList.options[index].value; //更改音樂來源
            mainTitle.innerHTML = musicList.options[index].innerText; //更改音樂標題
            musicList.children[index].selected = true; //更新選擇的歌曲索引
            
            //更改圖片來源
            if((index + 1) % 7 == 0) {
                img.src = `../img/7.jpg`; //如果是第七首歌，則圖片顯示第七張
            }else{
                img.src = `../img/${(index + 1) % 7}.jpg`; 
            }

            console.log("目前選擇的歌曲索引：" + index);
            console.log("v : " + v);
            console.log("n : " + n);


            myMusic.load();
            if(document.getElementById("control1").innerHTML == ";") {
                myMusic.onloadeddata = play; //等歌曲載入完畢後再播放音樂
                
            }
            myMusic.onloadedmetadata = function() {
                setTimeInitial();
    };

        }

        //音樂模式 (循環、隨機、單曲循環)
        function musicPlay(){
            if(text.innerText == "循環播放" && musicList.length == musicList.selectedIndex + 1){
                changeMusic(0 - musicList.selectedIndex);
            } else if (text.innerText == "隨機播放") {
                var n = Math.floor(Math.random() * musicList.length); //隨機在musicList中選擇一首歌
                if(n != musicList.selectedIndex) {
                    changeMusic(n - musicList.selectedIndex);
                } else {
                    changeMusic(1); //如果隨機到的歌曲是目前播放的歌曲，則改為下一首
                }
            } else if (text.innerText == "單曲循環") {
                changeMusic(0);
            } else{
                changeMusic(1);
            }
        }
        function musicMode(){
            if (text.innerText == "循環播放") {
                mode.innerHTML = "s";
                text.innerHTML = "隨機播放";
            } else if (text.innerText == "隨機播放") {
                mode.innerHTML = "`";
                text.innerHTML = "單曲循環";
            }else if (text.innerText == "單曲循環") {
                mode.innerHTML = "q";
                text.innerHTML = "循環播放";
            }
        }
        
///////【音效控制】///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //靜音
        function sound() {
            myMusic.muted = !myMusic.muted;
            event.target.className = event.target.className == "setMute" ? "" : "setMute";
        }

        //音量控制
        var volumeControl = document.getElementById("volumeControl");
        var rangeVolumn = volumeControl.children[2];
        var textVolumn = volumeControl.children[4];

        //微調(融合) - 音量降低&加大
        function volumnChange(v) {
            rangeVolumn.value = parseInt(rangeVolumn.value) + v;
            setVolumnRange();
        }

        //音量條
        function setVolumnRange() {
            textVolumn.value = rangeVolumn.value;
            myMusic.volume = textVolumn.value / 100; //真正寫入音量屬性值

            //塗音量條的顏色
            rangeVolumn.style.backgroundImage = `linear-gradient(to right, pink 0%,rgb(218, 180, 223) ${rangeVolumn.value}%,white ${rangeVolumn.value}%)`;
        }

        setVolumnRange(); //初始化音量