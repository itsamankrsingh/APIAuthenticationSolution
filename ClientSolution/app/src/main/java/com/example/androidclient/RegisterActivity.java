package com.example.androidclient;

import android.app.AlertDialog;
import android.os.Bundle;

import com.example.androidclient.models.TblUserMst;
import com.example.androidclient.network.IMyApi;
import com.example.androidclient.network.RetrofitClient;
import com.google.android.material.snackbar.Snackbar;

import androidx.appcompat.app.AppCompatActivity;

import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import androidx.navigation.NavController;
import androidx.navigation.Navigation;
import androidx.navigation.ui.AppBarConfiguration;
import androidx.navigation.ui.NavigationUI;

import com.example.androidclient.databinding.ActivityRegisterBinding;

import dmax.dialog.SpotsDialog;
import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.disposables.CompositeDisposable;
import io.reactivex.functions.Consumer;
import io.reactivex.schedulers.Schedulers;

public class RegisterActivity extends AppCompatActivity {

    IMyApi iMyApi;
    CompositeDisposable compositeDisposable = new CompositeDisposable();

    EditText edt_username, edt_password;
    Button btn_sign_up;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        //init api
        iMyApi = RetrofitClient.getInstance().create(IMyApi.class);

        //Views
        edt_username = (EditText) findViewById(R.id.edt_new_account_username);
        edt_password = (EditText) findViewById(R.id.edt_new_account_password);
        btn_sign_up = (Button) findViewById(R.id.btn_sign_up);

        //Events
        btn_sign_up.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                final AlertDialog dialog =new SpotsDialog.Builder()
                        .setContext(RegisterActivity.this)
                        .build();
                dialog.show();


                //create user to login
                TblUserMst user = new TblUserMst(
                        edt_username.getText().toString().trim(),
                        edt_password.getText().toString().trim(),
                        ""
                );

                compositeDisposable.add(iMyApi.registerUser(user)
                        .subscribeOn(Schedulers.io())
                        .observeOn(AndroidSchedulers.mainThread())
                        .subscribe(new Consumer<String>() {
                            @Override
                            public void accept(String s) throws Exception {
                                if(s.equals("User Registered Sucessfully")){

                                    finish();
                                }
                                Toast.makeText(RegisterActivity.this, s, Toast.LENGTH_SHORT).show();
                                dialog.dismiss();

                            }
                        }, new Consumer<Throwable>() {
                            @Override
                            public void accept(Throwable throwable) throws Exception {
                                dialog.dismiss();
                                Toast.makeText(RegisterActivity.this, throwable.getMessage(), Toast.LENGTH_SHORT).show();
                            }
                        })
                );

            }
        });

    }
    @Override
    protected void onStop() {
        compositeDisposable.clear();
        super.onStop();
    }
}