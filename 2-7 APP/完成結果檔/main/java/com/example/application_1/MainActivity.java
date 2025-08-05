package com.example.application_1;

import android.os.Bundle;
import android.widget.CheckedTextView;

import androidx.appcompat.app.AppCompatActivity;

public class MainActivity extends AppCompatActivity {

    private CheckedTextView mcheckTextView01, mcheckTextView02;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //EdgeToEdge.enable(this);
        setContentView(R.layout.activity_homework_03); //把某個特定的內容載到畫面上，看要顯示哪個View的畫面
        //ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
        //    Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
        //    v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
        //   return insets;
        //});

//        做CheckTextView的動作設定 (一開始的CheckTextView僅有框框，無法做點選的動作，需要透過城市去設定)
//        mcheckTextView01 = (CheckedTextView) findViewById(R.id.checkTextView01);
////        mcheckTextView01.setChecked(true);
//        mcheckTextView01.setOnClickListener(new View.OnClickListener(){
//            @Override
//            public void onClick(View v){
//                mcheckTextView01.toggle();
//            }
//        });
//
//        mcheckTextView02 = (CheckedTextView) findViewById(R.id.checkTextView02);
////        mcheckTextView02.setChecked(false);
//        mcheckTextView02.setOnClickListener(new View.OnClickListener(){
//            @Override
//            public void onClick(View v){
//                mcheckTextView02.toggle();
//            }
//        });
//
//        mcheckTextView02 = (CheckedTextView) findViewById(R.id.checkTextView02);
//        mcheckTextView02.setOnClickListener(v -> mcheckTextView02.toggle());
    }
}