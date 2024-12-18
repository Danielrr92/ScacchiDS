import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';

const Login = () => {
    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError(''); // Reset error before new attempt.

        const loginData = {
            email,
            password,
        };

        try {
            const response = await fetch('https://localhost:7225/api/auth/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(loginData),
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Login fallito');
            }

            const result = await response.json();
            console.log('Login riuscito:', result);
            setTimeout(() => navigate('/'), 3000);
        } catch (error) {
            console.error('Errore durante login:', error.message);
            setError(error.message);
        }
    };



    return (
        <div>
            <h2>Login</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Email:</label>
                    <input
                        type="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Password:</label>
                    <input
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <button type="submit">Login</button>
            </form>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <div>
                <p>Don&#39;t have an account?</p>
                <Link to="/register">
                    <button>Register</button>
                </Link>
            </div>
        </div>
    );
};

export default Login;
