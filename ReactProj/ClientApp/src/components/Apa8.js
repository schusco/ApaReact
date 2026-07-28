import { usePlayers } from '../context/PlayerContext';
import { useFormHandler } from '../hooks/formHandler';

function APA8() {

    const { playerList, isLoading } = usePlayers();
    const { formData: data, handleChange, handleSubmit, isSubmitting, error, success } = useFormHandler({ date: '', innings: 0, defenses: 0, games: 0, points: 0, sl: 6, oppsl: 0 }, '/api/apa8');

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
            <div class="row g-3 mb-3">
                <label for="dateInput" class="col-sm-2 col-form-label">Score Date</label>
                <div class="col-auto">
                    <input type="date" class=" col-sm-5 form-control" id="dateInput" name="date" value={data.date} onChange={handleChange} />
                </div>
            </div>
            <div class="row g-3 mb-3">
                <label for="inningsInput" class="col-sm-2 col-form-label">Innings</label>
                <div class="col-auto">
                    <input type="number" class=" col-sm-5 form-control" id="inningsInput" name="innings" value={data.innings} onChange={handleChange} />
                </div>
            </div>
            <div class="row g-3 mb-3">
                <label for="defensesInput" class="col-sm-2 col-form-label">Defenses</label>
                <div class="col-auto">
                    <input type="number" class="form-control" id="defensesInput" name="defenses" value={data.defenses} onChange={handleChange} />
                </div>
            </div>
            <div class="row g-3 mb-3">
                <label for="ballsInput" class="col-sm-2 col-form-label">Points</label>
                <div class="col-auto">
                    <input type="number" class="form-control" name="points" value={data.points} onChange={handleChange} />
                </div>
            </div>
            <div class="row g-3 mb-3">
                <label for="ballsInput" class="col-sm-2 col-form-label">Games</label>
                <div class="col-auto">
                    <input type="number" class="form-control" name="points" value={data.games} onChange={handleChange} />
                </div>
            </div>
            <div class="row g-3 mb-3">
                <label for="slInput" class="col-sm-2 col-form-label">SL</label>
                <div class="col-auto">
                    <input type="number" class="form-control" id="slInput" name="sl" value={data.sl} onChange={handleChange} />
                </div>
            </div>
            <div class="row g-3 mb-3">
                <label for="oppSlInput" class="col-sm-2 col-form-label">Opponent SL</label>
                <div class="col-auto">
                    <input type="number" class="form-control" id="oppSlInput" name="oppsl" value={data.oppsl} onChange={handleChange} />
                </div>
            </div>
            <div class="row g-3 mb-3">
                <label for="playerInput" class="col-sm-2 col-form-label">Opponent</label>
                <div class="col-auto">
                    <select class="form-select" name="oppPlayerId" value={data.oppPlayerId} onChange={handleChange} required >
                        <option value="0">-- Select --</option>
                        {playerList.map((item) => (
                            <option key={item.playerNumber} value={item.playerNumber}>
                                {item.fullName}
                            </option>
                        ))}
                    </select>
                </div>
            </div >
            <div class="row g-3 mb-3">
                <div class="col-sm-2 "></div>
                <div class="col-auto">
                    <button type="submit" disabled={isSubmitting} class="btn btn-info" >
                        {isSubmitting ? 'Saving...' : 'Enter Score'}</button>

                </div>
            </div>
        </form >
    );
}
export default APA8;