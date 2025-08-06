import axios from "axios";
import { use } from "react";

const Api='https://localhost:7264/api/Account';


interface authResponse {
    token: string;
    userName: string;
    roles: string[];
}
export const login= async (email:string ,password :string):Promise<authResponse>=>{
    try{
        const response= await axios.post<authResponse>(`${Api}/Login`,{
            email,
            password
        });
        return response.data;//retourne la reponse au api
    }
        catch(error: any){
            throw error.response?.data || 'login failed';
        }
    };

    export const register =async (name:string,email:string,password:string,confirmPassword:string,role:string):Promise<authResponse>=>{
        try{
            const response= await axios.post<authResponse>(`${Api}/Register`,{
                name,
                email,
                password,
                confirmPassword,
                role});
                return response.data;//retourne la reponse au api
        }
        catch(error: any){
            throw error.response?.data || 'Registration failed';
        }

    } ;
    export const logout =():void=>{//logout a pas besoin d une promise
        localStorage.removeItem('token');
        localStorage.removeItem('userName');

    } ;
