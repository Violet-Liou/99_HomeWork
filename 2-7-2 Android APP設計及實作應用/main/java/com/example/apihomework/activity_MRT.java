package com.example.apihomework;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

public class activity_MRT extends AppCompatActivity {

    TextView textview_01, textview_02;
    Button button_01,button_02;
    ListView Listview_01;
    ArrayList<String> MRT_arrayListOfString;
    ArrayAdapter<String> MRT_arrayAdapter;

    String strAPI_URL = "https://api.kcg.gov.tw/api/service/Get/4278fc6a-c3ea-4192-8ce0-40f00cdb40dd";

    String strMRT = "高雄捷運站資料";

    String strMRTID, strMRTName, strLongitude, strLatitude;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
//        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_mrt);
//        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
//            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
//            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
//            return insets;
//        });

        textview_01 = findViewById(R.id.textview_01);
        textview_02 = findViewById(R.id.textview_02);
        button_01 = findViewById(R.id.button_01);
        button_02 = findViewById(R.id.button_02);
        Listview_01 = findViewById(R.id.Listview_01);

        MRT_arrayListOfString = new ArrayList<String>();

        button_02.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if((MRT_arrayListOfString != null) &&(MRT_arrayAdapter != null)){
                    MRT_arrayListOfString.clear();
                    MRT_arrayAdapter.notifyDataSetChanged();
                    textview_01.setText(strMRT);
                    textview_02.setText("\nAPI資料已清空......");
                }
            }
        });

        button_01.setOnClickListener(new View.OnClickListener() {
           @Override
           public void onClick(View v) {
               try{
                   JsonObjectRequest request = new JsonObjectRequest(Request.Method.GET,strAPI_URL,null, new Response.Listener<JSONObject>() {
                       @Override
                       public void onResponse(JSONObject response) {
                           int i;

                           try {
//                               JSONObject jsonObject_result = response.getJSONObject("data");
//                               JSONArray jsonArray_data = jsonObject_result.getJSONArray("data");
                               JSONArray jsonArray_data = response.getJSONArray("data");

                               for (i = 0; i < jsonArray_data.length(); i++) {
                                   JSONObject jsonObject_MRT = jsonArray_data.getJSONObject(i);
                                   strMRTID = jsonObject_MRT.getString("車站編號");
                                   strMRTName = jsonObject_MRT.getString("車站中文名稱");
//                                   strCenterAddress = jsonObject_sport_center.getString("地址");
                                   strLongitude = jsonObject_MRT.getString("車站經度");
                                   strLatitude = jsonObject_MRT.getString("車站緯度");

                                   MRT_arrayListOfString.add((i+1) + ".\n車站編號：" + strMRTID + "\n車站中文名稱：" + strMRTName + "\n座標 : " +
                                           strLatitude + "," + strLongitude);
                               }

                               MRT_arrayAdapter = new ArrayAdapter<String>(activity_MRT.this, android.R.layout.simple_list_item_1 ,MRT_arrayListOfString);

                               Listview_01.setAdapter(MRT_arrayAdapter);
                               textview_01.setText(strMRT);
                               textview_02.setText("\nAPI資料讀取完成，解析成功!!");

                               Listview_01.setOnItemClickListener(new AdapterView.OnItemClickListener(){
                                   @Override
                                   public void onItemClick(AdapterView<?> parent, View view, int position, long id){
                                       String MRT_ItemString = MRT_arrayListOfString.get(position);
                                       int intStartOfGeo = MRT_ItemString.indexOf("座標");
                                       String strGeoPosition = MRT_ItemString.substring(intStartOfGeo + 4);
                                       String strArrayLatTng[] = strGeoPosition.split(",");
                                       int name = MRT_ItemString.indexOf("車站中文名稱");
                                       String strLabel = strArrayLatTng[0] +"," + strArrayLatTng[1]+"("+MRT_ItemString.substring(name +7, intStartOfGeo)+")";
                                       String strQuery = Uri.decode(strLabel);
                                       Intent intentGeo = new Intent(Intent.ACTION_VIEW, Uri.parse("geo:" + strGeoPosition + "?q=" + strQuery +"Z = 32"));
                                       startActivity(intentGeo);
                                   }
                               });

                           } catch (JSONException ex) {
                               textview_01.setText(strMRT);
                               textview_02.setText("\n向伺服器取回資料時發生VolleyError，錯誤訊息:\n" + ex.getMessage());
                           }
                       }
                   }, new Response.ErrorListener() {
                       @Override
                       public void onErrorResponse(VolleyError error) {
                           textview_01.setText(strMRT);
                           textview_02.setText("\n向伺服器取回資料時發生VolleyError，錯誤訊息 :\n" + error.getMessage());
                       }
                   });
                   Volley.newRequestQueue(activity_MRT.this).add(request);
                   textview_01.setText(strMRT);
                   textview_02.setText("\nAPI資料正在讀取中......");

               }catch (Exception ex){

               }
           }
        });

    }
}