package com.example.androidclient.network;

import com.example.androidclient.models.TblUserMst;

import io.reactivex.Observable;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface IMyApi {
    //http://localhost:5086/api/Register
    @POST("api/Register")
    Observable<String> registerUser(@Body TblUserMst user);

    //http://localhost:5086/api/Login
    @POST("api/Login")
    Observable<String> loginUser(@Body TblUserMst user);

}
