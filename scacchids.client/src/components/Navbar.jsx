import { Link } from 'react-router-dom';

const Navbar = () => {
    return (
        <nav style={{ padding: '10px', backgroundColor: '#333', color: '#fff' }}>
            <Link to="/" style={{ marginRight: '15px', color: '#fff', textDecoration: 'none' }}>Home</Link>
            <Link to="/game" style={{ marginRight: '15px', color: '#fff', textDecoration: 'none' }}>Chessboard</Link>
            <Link to="/about" style={{ color: '#fff', textDecoration: 'none' }}>About</Link>
        </nav>
    );
};

export default Navbar;