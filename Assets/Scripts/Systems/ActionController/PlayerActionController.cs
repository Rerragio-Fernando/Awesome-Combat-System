public class PlayerActionController : ActionController
{
    protected override void OnEnable() {
        PlayerInputHandler.BasicAttackEvent += (phase) => SetNextStateTap(phase, PlayerCombatState.PLAYER_BASIC_ATTACK);
        PlayerInputHandler.StrongAttackEvent += (phase) => SetNextStateTap(phase, PlayerCombatState.PLAYER_STRONG_ATTACK);

        PlayerInputHandler.GuardEvent += (phase) => SetNextStateHold(phase, PlayerCombatState.PLAYER_GUARD);

        PlayerInputHandler.CycleWeaponEvent += (phase) => SetNextStateTap(phase, PlayerCombatState.PLAYER_WEAPON_CYCLE);
    }

    protected override void OnDisable() {
        PlayerInputHandler.BasicAttackEvent -= (phase) => SetNextStateTap(phase, PlayerCombatState.PLAYER_BASIC_ATTACK);
        PlayerInputHandler.StrongAttackEvent -= (phase) => SetNextStateTap(phase, PlayerCombatState.PLAYER_STRONG_ATTACK);

        PlayerInputHandler.GuardEvent -= (phase) => SetNextStateHold(phase, PlayerCombatState.PLAYER_GUARD);

        PlayerInputHandler.CycleWeaponEvent -= (phase) => SetNextStateTap(phase, PlayerCombatState.PLAYER_WEAPON_CYCLE);
    }
}