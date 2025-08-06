import react, { useState } from 'react'
import { login } from '../Api/auth';
import { useNavigate } from 'react-router-dom';

const Login:react.FC=()=>{
    const [email ,setEmail]=useState<string>('');
    const [password ,setPassword]=useState<string>('');
    const [error ,setError]=useState<string|null >('');
    const navigate=useNavigate();
    const handleSubmit=async(e:React.FormEvent<HTMLFormElement>)=>{
        e.preventDefault();
        try{
            const response= await login(email, password);
            localStorage.setItem('token',response.token);
            localStorage.setItem('userName',JSON.stringify(response.userName));
            navigate('./admin/Dashboard');
        }
        catch(error: any){
            setError(error.message || "Login failed");
        
    } 

    };

    return (
        <div className="flex items-center justify-center h-screen bg-gray-100">
            <form onSubmit={handleSubmit} className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
                <h2 className="text-2xl font-bold mb-6">Login</h2>
                {error && <p className="text-red-500 mb-4">{error}</p>}
                <div className="mb-4">
                    <input
                        type="email"
                        placeholder="Email"
                        className='w-full px-3 py-2 border border-gray-300 rounded focus:outline-none focus:border-indigo-500
                        '
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>
                 <div className="mb-4">
                    <input
                        type="password"
                        placeholder="password"
                        className='w-full px-3 py-2 border border-gray-300 rounded focus:outline-none focus:border-indigo-500
                        '
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <button type="submit" className="bg-indigo-500 text-white px-4 py-2 rounded hover:bg-indigo-600">Login</button>
       </form>
        </div>
    )
}
export default Login