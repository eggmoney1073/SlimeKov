using Facebook.Unity;
using System;
using UnityEngine;



// 실제 구현 클래스
public sealed class FacebookLoginService : IFacebookLoginService
{
    // 페이스북 SDK 초기화
    public void InitializeFacebook()
    {
        // SDK가 초기화되었는지 확인
        if (!FB.IsInitialized)
        {
            // SDK 초기화 호출
            FB.Init(this.OnInitComplete, this.OnHideUnity);
        }
        else
        {
            // 이미 초기화되었다면 바로 활성화
            FB.ActivateApp();
        }
    }

    private void OnInitComplete()
    {
        if (FB.IsInitialized)
        {
            // 앱 이벤트 활성화 (필수)
            FB.ActivateApp();
            Debug.Log("Facebook SDK 초기화 성공!");
        }
        else
        {
            Debug.LogError("Facebook SDK 초기화 실패!");
        }
    }

    private void OnHideUnity(Boolean isGameShown)
    {
        // 게임 화면이 가려지거나 다시 나타날 때의 로직 처리
        Debug.Log("유니티 앱 가시성: " + isGameShown);
    }

    // 페이스북 로그인 처리
    public void Login(Action<string> onLoggedIn, Action<string> onError)
    {
        // 필요한 권한 목록 정의 (기본 권한)
        System.Collections.Generic.List<String> permissions = new System.Collections.Generic.List<String>() { "public_profile", "email" };

        FB.LogInWithReadPermissions(permissions, (ILoginResult result) =>
        {
            if (result == null)
            {
                onError?.Invoke("로그인 결과가 NULL입니다.");
                return;
            }

            // 오류 처리
            if (!String.IsNullOrEmpty(result.Error))
            {
                onError?.Invoke($"로그인 오류: {result.Error}");
                return;
            }

            // 로그인 성공 및 토큰 확보
            if (FB.IsLoggedIn)
            {
                string accessToken = AccessToken.CurrentAccessToken.TokenString;
                Debug.Log($"로그인 성공. 액세스 토큰: {accessToken}");

                // 액세스 토큰을 사용하여 데이터 저장 로직 수행 가능
                onLoggedIn?.Invoke(accessToken);
            }
            else
            {
                onError?.Invoke("로그인 취소 또는 실패.");
            }
        });
    }

    // 데이터 저장/로드 예시: 사용자 이름 가져오기
    public void FetchUserName(Action<string> onDataFetched, Action<string> onError)
    {
        if (FB.IsLoggedIn)
        {
            // Graph API 호출을 통해 사용자 정보 요청
            FB.API("/me?fields=name", HttpMethod.GET, (IGraphResult result) =>
            {
                if (!String.IsNullOrEmpty(result.Error))
                {
                    onError?.Invoke($"Graph API 오류: {result.Error}");
                }
                else
                {
                    // JSON 데이터 파싱 (System.Text.Json 사용 권장)
                    // 예시로 간단히 보여줍니다. 실제로는 안정적인 JSON 파싱 라이브러리 사용
                    String userName = JsonUtility.FromJson<GraphApiNameResponse>(result.RawResult).name;
                    onDataFetched?.Invoke(userName);
                }
            });
        }
        else
        {
            onError?.Invoke("로그인되어 있지 않습니다.");
        }
    }

    [Serializable]
    private sealed class GraphApiNameResponse
    {
        // 필드명은 페이스북 API 응답과 정확히 일치해야 함
        public String name = String.Empty;
    }
}