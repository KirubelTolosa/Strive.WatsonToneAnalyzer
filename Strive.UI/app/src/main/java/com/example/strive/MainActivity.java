package com.example.strive;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;

import com.google.android.material.bottomnavigation.BottomNavigationView;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class MainActivity extends AppCompatActivity {

    String input;
    EditText userInput;
    Button btnCallAPI;
    private static final String TAG = "StriveApp";
    private Object String;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        BottomNavigationView navView = findViewById(R.id.nav_view);
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        AppBarConfiguration appBarConfiguration = new AppBarConfiguration.Builder(
                R.id.navigation_home, R.id.navigation_dashboard, R.id.navigation_notifications)
                .build();
        NavController navController = Navigation.findNavController(this, R.id.nav_host_fragment);
        NavigationUI.setupActionBarWithNavController(this, navController, appBarConfiguration);
        NavigationUI.setupWithNavController(navView, navController);

        userInput = (EditText) findViewById(R.id.userInput);

        btnCallAPI = (Button) findViewById(R.id.btnCallAPI);

        btnCallAPI.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Log.i(TAG, "This is a log message from the On Click Listener of the API button. Input = " + input);
                Toast.makeText(getApplicationContext(), "Thanks for sharing, opening next page... ( SEE COMMENTS IN MAINACTIVITY.JAVA)", Toast.LENGTH_LONG)
                        .show();

                input = userInput.getText().toString();

                //openNewActivity(); This will open a new activity which will display the results of the analytics from the API pull
            }
        });

        //public void openNewActivity(){
        //    Intent intent = new Intent(this, NewActivity.class);
       //     startActivity(intent);
       // }
    }

}