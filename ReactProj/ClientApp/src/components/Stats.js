import React, { useEffect, useState, useMemo } from 'react';

function Stats() {
    const [scores8, setScores8] = useState([]);
    const [scores9, setScores9] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [activeTab, setActiveTab] = useState('9'); // '9' for APA 9, '8' for APA 8

    useEffect(() => {
        async function fetchScores() {
            try {
                const [scores9Response, scores8Response] = await Promise.all([
                    fetch('/api/apa').then(res => res.json()),
                    fetch('/api/apa8').then(res => res.json())]);
                setScores8(scores8Response);
                setScores9(scores9Response);
            } catch (error) {
                console.error('Error fetching scores:', error);
            } finally {
                setIsLoading(false);
                console.log('loading set to false');
            }
        }
        fetchScores();
    }, []);

    const calculate = (data) => {
        console.log(data);
        const nineBallBenchmark = 3.1; // Balls per Inning        
        const eightBallBenchmark = 2.5; // innings per rack
        const counts = data.reduce((acc, item) => {
            acc.totalDefenses += item.defenses;
            acc.totalInnings += item.innings;
            acc.totalPoints += item.points;
            if (item.balls > 0) {
                if ((item.balls / item.innings) > nineBallBenchmark) {
                    acc.shotOverLevel += 1;
                    console.log(item);
                }
            }
            else {
                if ((item.innings / item.games) < eightBallBenchmark) {
                    acc.shotOverLevel += 1;
                    console.log(item);
                }
            }
            if (item.isWin === true) {
                acc.wins += 1;

                if (item.playerSL === item.oppPlayerSL) {
                    acc.winsAtLevel += 1;
                }
                if (item.playerSL > item.oppPlayerSL) {
                    acc.winsPlayingDown += 1;
                }
                if (item.playerSL < item.oppPlayerSL) {
                    acc.winsPlayingUp += 1;
                }
            }
            else if (item.isWin === false) {
                acc.losses += 1
                if (item.playerSL === item.oppPlayerSL) {
                    acc.lossesAtLevel += 1;
                }
                if (item.playerSL > item.oppPlayerSL) {
                    acc.lossesPlayingDown += 1;
                }
                if (item.playerSL < item.oppPlayerSL) {
                    acc.lossesPlayingUp += 1;
                }
            }
            return acc;
        }, {
            wins: 0, losses: 0, winsAtLevel: 0, lossesAtLevel: 0, winsPlayingUp: 0, winsPlayingDown: 0, lossesPlayingDown: 0, shotOverLevel: 0,
            lossesPlayingUp: 0, pointsAverage: 0, defensesAverage: 0, inningsAverage: 0, totalDefenses: 0, totalInnings: 0, totalPoints: 0
        });
        console.log(counts);
        counts.pointsAverage = counts.totalPoints / 20;
        counts.defensesAverage = counts.totalDefenses / 20;
        counts.inningsAverage = counts.totalInnings / 20;
        return counts;
    };
    const currentData = activeTab === '9' ? scores9 : scores8;
    const summaryData = useMemo(() => calculate(currentData), [currentData]);

    if (isLoading) return <div>Loading...</div>;

    return (
        <>
            <div className="btn-group mb-4" role="group">
                <button
                    type="button"
                    className={`btn ${activeTab === '9' ? 'btn-primary' : 'btn-outline-primary'}`}
                    onClick={() => setActiveTab('9')}>9 Ball Stats
                </button>
                <button
                    type="button"
                    className={`btn ${activeTab === '8' ? 'btn-primary' : 'btn-outline-primary'}`}
                    onClick={() => setActiveTab('8')}>8 Ball Stats</button>
            </div>
            <div class="row">
                <div class="col-sm-2">
                    <h3 class="pt-2">Overall</h3>
                </div>
                <div class="col-sm-2">
                    <h1>{summaryData.wins}<label class="ps-3">Wins</label></h1>
                </div>
                <div class="col-auto">
                    <h1>{summaryData.losses}<label class="ps-3">Losses</label></h1>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-2">
                    <h3 class="pt-2">At Level</h3>
                </div>
                <div class="col-sm-2">
                    <h1>{summaryData.winsAtLevel}<label class="ps-3">Wins</label></h1>
                </div>
                <div class="col-auto">
                    <h1>{summaryData.lossesAtLevel}<label class="ps-3">Losses</label></h1>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-2">
                    <h3 class="pt-2">Playing up</h3>
                </div>
                <div class="col-sm-2">
                    <h1>{summaryData.winsPlayingUp}<label class="ps-3">Wins</label></h1>
                </div>
                <div class="col-auto">
                    <h1>{summaryData.lossesPlayingUp}<label class="ps-3">Losses</label></h1>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-2">
                    <h3 class="pt-2">Playing down</h3>
                </div>
                <div class="col-sm-2">
                    <h1>{summaryData.winsPlayingDown}<label class="ps-3">Wins</label></h1>
                </div>
                <div class="col-auto">
                    <h1>{summaryData.lossesPlayingDown}<label class="ps-3">Losses</label></h1>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-2"> <h3 class="pt-2">Avg. Points</h3></div>
                <div class="col-sm-2">
                    <h1>{summaryData.pointsAverage}</h1>
                </div>
                <div class="col-auto">
                    <h1>{summaryData.defensesAverage}<label class="ps-3">Defenses</label></h1>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-2"> <h3 class="pt-2">Avg. Innings</h3></div>
                <div class="col-sm-2">
                    <h1>{summaryData.inningsAverage}</h1>
                </div>
                <div class="col-auto">
                </div>
            </div>
            <div class="row">
                <div class="col-sm-2"> <h3 class="pt-2">Red Marks</h3></div>
                <div class="col-sm-2">
                    <h1>{summaryData.shotOverLevel}</h1>
                </div>
            </div>
        </>
    );
}
export default Stats;