using Facebook.Unity;
using System;
using UnityEngine;

// 인터페이스는 기존 코드를 재사용합니다.
public interface IFacebookLoginService
{
    void InitializeFacebook();
    void Login(Action<String> onLoggedIn, Action<String> onError);
    void FetchUserName(Action<String> onDataFetched, Action<String> onError);
}

// 이전에 작성된 FacebookLoginService 코드가 필요합니다.

public sealed class LoginManager : MonoBehaviour
{
    private IFacebookLoginService _facebookService;
    private String _guiMessage = "Facebook SDK 초기화 대기 중...";

    // Start: 유니티 컴포넌트가 활성화될 때 한 번만 실행됩니다.
    private void Start()
    {
        Debug.Log("LoginManager: Facebook 서비스 초기화를 시작합니다.");
        _facebookService = new FacebookLoginService();
        _facebookService.InitializeFacebook();

        // 초기화 완료까지 대기하는 동안 메시지를 설정합니다.
        // 실제 FB.Init은 비동기이므로, FB.IsInitialized를 체크하는 로직이 필요합니다.
        // 여기서는 간단히 시작 메시지를 설정합니다.
        this._guiMessage = "Facebook 로그인 테스트 준비 완료.";
    }

    // OnGUI: 매 프레임 UI를 렌더링합니다. (테스트 목적)
    private void OnGUI()
    {
        // 버튼의 위치와 크기 설정 (화면 중앙 근처)
        Rect buttonRect = new Rect(Screen.width / 2 - 150, Screen.height / 2 - 25, 300, 50);

        // 메시지 표시 (로그인 상태 등)
        GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 100, 300, 50), this._guiMessage);

        // Facebook 로그인 버튼 생성
        if (GUI.Button(buttonRect, "Facebook 로그인 테스트 시작"))
        {
            this.TestLogin();
        }
    }

    // 버튼 클릭 시 호출될 로그인 로직
    private void TestLogin()
    {
        if (_facebookService == null || !FB.IsInitialized)
        {
            this._guiMessage = "SDK가 아직 초기화되지 않았습니다. 잠시 기다려주세요.";
            Debug.LogError("Facebook 서비스가 초기화되지 않았거나 SDK 초기화 대기 중.");
            return;
        }

        this._guiMessage = "로그인 요청 중... (브라우저 또는 앱 전환 대기)";
        Debug.Log("LoginManager: 페이스북 로그인 요청을 전송합니다.");

        _facebookService.Login(
            // --- 로그인 성공 콜백 ---
            (String accessToken) =>
            {
                String displayToken = accessToken.Substring(0, 15) + "...";
                this._guiMessage = $"로그인 성공! 사용자 토큰: {displayToken}";
                Debug.Log($"\n========== 로그인 성공! ==========\n액세스 토큰 (앞 15자리): {displayToken}");

                // 로그인 성공 후 추가 테스트: 사용자 이름 가져오기
                this.TestUserDataFetch();
            },
            // --- 로그인 실패 콜백 ---
            (String errorMessage) =>
            {
                this._guiMessage = $"로그인 실패: {errorMessage}";
                Debug.LogError($"\n========== 로그인 실패! ==========\n오류 메시지: {errorMessage}");
            }
        );
    }

    // 로그인 성공 후 데이터 저장 연동 테스트를 위한 메서드
    private void TestUserDataFetch()
    {
        _facebookService.FetchUserName(
            // --- 데이터 패치 성공 콜백 ---
            (String userName) =>
            {
                this._guiMessage += $"\n데이터 패치 성공! 이름: {userName}";
                Debug.Log($"\n========== 데이터 패치 성공! ==========\n로그인한 사용자 이름: {userName}");
            },
            // --- 데이터 패치 실패 콜백 ---
            (String errorMessage) =>
            {
                this._guiMessage += $"\n데이터 패치 실패: {errorMessage}";
                Debug.LogError($"\n========== 데이터 패치 실패! ==========\n오류 메시지: {errorMessage}");
            }
        );
    }
}