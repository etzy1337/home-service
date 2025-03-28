import React from 'react';
import * as Yup from "yup";
import { useAuth } from '../../Context/useAuth';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import "./LoginPage.css";

type Props = {};

type LoginFormInput = {
    email: string;
    password: string;
};

const validation = Yup.object().shape({
    email: Yup.string().required("Username is required"),
    password: Yup.string().required("Password is required"),
});

const LoginPage = (props: Props) => {
    const { loginUser } = useAuth();
    const { register, handleSubmit, formState: { errors } } = useForm<LoginFormInput>({
        resolver: yupResolver(validation)
    });

    const handleLogin = (form: LoginFormInput) => {
        loginUser(form.email, form.password);
    };

    return (
        <div className="login-page">
            <h2>Login</h2>

         
            <form onSubmit={handleSubmit(handleLogin)}>
                <div className="form-group">
                    <label htmlFor="email">Email</label>
                    <input
                        type="email"
                        id="email"
                        placeholder="Enter your email"
                        {...register("email")} // Rejestracja pola w formularzu
                    />
                    {errors.email ? <p>{errors.email.message}</p> : ""}
                </div>

                <div className="form-group">
                    <label htmlFor="password">Password</label>
                    <input
                        type="password"
                        id="password"
                        placeholder="Enter your password"
                        {...register("password")} // Rejestracja pola w formularzu
                    />
                    {errors.password ? <p>{errors.password.message}</p> : ""}
                </div>

                <button type="submit">Login</button>
            </form>
        </div>
    );
};

export default LoginPage;
