import { usePlayers } from '../context/PlayerContext';
import { useFormHandler } from '../hooks/formHandler';
import { Navigate } from 'react-router-dom';
function APA8() {

    const { playerList, isLoading, user } = usePlayers();  
    let playerNo = user ? user.result.playerNumber : 0;
    let sl8 = user ? user.result.sl8 : 0;
    const { formData: data, handleChange, handleSubmit, isSubmitting, error, success } = useFormHandler({
        date: '', innings: 0, defenses: 0, playerId: playerNo, games: 0, points: 0, sl: sl8, oppsl: 0
    }, '/api/apa8');

    if (!user) {
        return <Navigate to="/login" replace />;
    }
    if (isLoading) {
        return (
            <div style={{ textAlign: 'center', marginTop: '50px' }}>
                <h3>Loading data from database...</h3>
            </div>
        );
    }
    if (error) {
        return <div style={{ color: 'red' }}>Error: {error}</div>;
    }
    return (
        <form onSubmit={handleSubmit}>
            {success && (
                <div className="alert alert-success p-2 mb-2">Score added successfully</div>
            )}
            {error && (
                <div className="alert alert-danger mb-2 p-2">{error}</div>
            )}
            <h3>8 Ball Scores</h3>
            <div className="row g-3 mb-3">
                <label for="dateInput" className="col-sm-2 col-form-label">Score Date</label>
                <div className="col-auto">
                    <input type="date" className=" col-sm-5 form-control" id="dateInput" name="date" value={data.date} onChange={handleChange} />
                </div>
            </div>
            <div className="row g-3 mb-3">
                <label for="inningsInput" className="col-sm-2 col-form-label">Innings</label>
                <div className="col-auto">
                    <input type="number" className=" col-sm-5 form-control" id="inningsInput" name="innings" value={data.innings} onChange={handleChange} />
                </div>
            </div>
            <div className="row g-3 mb-3">
                <label for="defensesInput" className="col-sm-2 col-form-label">Defenses</label>
                <div className="col-auto">
                    <input type="number" className="form-control" id="defensesInput" name="defenses" value={data.defenses} onChange={handleChange} />
                </div>
            </div>
            <div className="row g-3 mb-3">
                <label for="ballsInput" className="col-sm-2 col-form-label">Points</label>
                <div className="col-auto">
                    <input type="number" className="form-control" name="points" value={data.points} onChange={handleChange} />
                </div>
            </div>
            <div className="row g-3 mb-3">
                <label for="ballsInput" className="col-sm-2 col-form-label">Games</label>
                <div className="col-auto">
                    <input type="number" className="form-control" name="games" value={data.games} onChange={handleChange} />
                </div>
            </div>
            <div className="row g-3 mb-3">
                <label for="slInput" className="col-sm-2 col-form-label">SL</label>
                <div className="col-auto">
                    <input type="number" className="form-control" id="slInput" name="sl" value={data.sl} onChange={handleChange} />
                </div>
            </div>
            <div className="row g-3 mb-3">
                <label for="oppSlInput" className="col-sm-2 col-form-label">Opponent SL</label>
                <div className="col-auto">
                    <input type="number" className="form-control" id="oppSlInput" name="oppsl" value={data.oppsl} onChange={handleChange} />
                </div>
            </div>
            <div className="row g-3 mb-3">
                <label for="playerInput" className="col-sm-2 col-form-label">Opponent</label>
                <div className="col-auto">
                    <select className="form-select" name="oppPlayerId" value={data.oppPlayerId} onChange={handleChange} required >
                        <option value="0">-- Select --</option>
                        {playerList.map((item) => (
                            <option key={item.playerNumber} value={item.playerNumber}>
                                {item.fullName}
                            </option>
                        ))}
                    </select>
                </div>
            </div >
            <div className="row g-3 mb-3">
                <div className="col-sm-2 "></div>
                <div className="col-auto">
                    <button type="submit" disabled={isSubmitting} className="btn btn-info" >
                        {isSubmitting ? 'Saving...' : 'Enter Score'}</button>

                </div>
            </div>
        </form >
    );
}
export default APA8;