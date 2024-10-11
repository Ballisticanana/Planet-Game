extern "C" void RegisterStaticallyLinkedModulesGranular()
{
	void RegisterModule_SharedInternals();
	RegisterModule_SharedInternals();

	void RegisterModule_Core();
	RegisterModule_Core();

	void RegisterModule_Audio();
	RegisterModule_Audio();

	void RegisterModule_JSONSerialize();
	RegisterModule_JSONSerialize();

	void RegisterModule_Physics();
	RegisterModule_Physics();

	void RegisterModule_RuntimeInitializeOnLoadManagerInitializer();
	RegisterModule_RuntimeInitializeOnLoadManagerInitializer();

	void RegisterModule_WebGL();
	RegisterModule_WebGL();

}

template <typename T> void RegisterUnityClass(const char*);
template <typename T> void RegisterStrippedType(int, const char*, const char*);

void InvokeRegisterStaticallyLinkedModuleClasses()
{
	// Do nothing (we're in stripping mode)
}

class AudioBehaviour; template <> void RegisterUnityClass<AudioBehaviour>(const char*);
class AudioClip; template <> void RegisterUnityClass<AudioClip>(const char*);
class AudioListener; template <> void RegisterUnityClass<AudioListener>(const char*);
class AudioManager; template <> void RegisterUnityClass<AudioManager>(const char*);
class SampleClip; template <> void RegisterUnityClass<SampleClip>(const char*);
class Behaviour; template <> void RegisterUnityClass<Behaviour>(const char*);
class BuildSettings; template <> void RegisterUnityClass<BuildSettings>(const char*);
class Camera; template <> void RegisterUnityClass<Camera>(const char*);
namespace Unity { class Component; } template <> void RegisterUnityClass<Unity::Component>(const char*);
class ComputeShader; template <> void RegisterUnityClass<ComputeShader>(const char*);
class Cubemap; template <> void RegisterUnityClass<Cubemap>(const char*);
class CubemapArray; template <> void RegisterUnityClass<CubemapArray>(const char*);
class DelayedCallManager; template <> void RegisterUnityClass<DelayedCallManager>(const char*);
class EditorExtension; template <> void RegisterUnityClass<EditorExtension>(const char*);
class GameManager; template <> void RegisterUnityClass<GameManager>(const char*);
class GameObject; template <> void RegisterUnityClass<GameObject>(const char*);
class GlobalGameManager; template <> void RegisterUnityClass<GlobalGameManager>(const char*);
class GraphicsSettings; template <> void RegisterUnityClass<GraphicsSettings>(const char*);
class InputManager; template <> void RegisterUnityClass<InputManager>(const char*);
class LevelGameManager; template <> void RegisterUnityClass<LevelGameManager>(const char*);
class Light; template <> void RegisterUnityClass<Light>(const char*);
class LightProbes; template <> void RegisterUnityClass<LightProbes>(const char*);
class LightingSettings; template <> void RegisterUnityClass<LightingSettings>(const char*);
class LightmapSettings; template <> void RegisterUnityClass<LightmapSettings>(const char*);
class LowerResBlitTexture; template <> void RegisterUnityClass<LowerResBlitTexture>(const char*);
class Material; template <> void RegisterUnityClass<Material>(const char*);
class Mesh; template <> void RegisterUnityClass<Mesh>(const char*);
class MeshFilter; template <> void RegisterUnityClass<MeshFilter>(const char*);
class MeshRenderer; template <> void RegisterUnityClass<MeshRenderer>(const char*);
class MonoBehaviour; template <> void RegisterUnityClass<MonoBehaviour>(const char*);
class MonoManager; template <> void RegisterUnityClass<MonoManager>(const char*);
class NamedObject; template <> void RegisterUnityClass<NamedObject>(const char*);
class Object; template <> void RegisterUnityClass<Object>(const char*);
class PlayerSettings; template <> void RegisterUnityClass<PlayerSettings>(const char*);
class PreloadData; template <> void RegisterUnityClass<PreloadData>(const char*);
class QualitySettings; template <> void RegisterUnityClass<QualitySettings>(const char*);
namespace UI { class RectTransform; } template <> void RegisterUnityClass<UI::RectTransform>(const char*);
class ReflectionProbe; template <> void RegisterUnityClass<ReflectionProbe>(const char*);
class RenderSettings; template <> void RegisterUnityClass<RenderSettings>(const char*);
class RenderTexture; template <> void RegisterUnityClass<RenderTexture>(const char*);
class Renderer; template <> void RegisterUnityClass<Renderer>(const char*);
class ResourceManager; template <> void RegisterUnityClass<ResourceManager>(const char*);
class RuntimeInitializeOnLoadManager; template <> void RegisterUnityClass<RuntimeInitializeOnLoadManager>(const char*);
class Shader; template <> void RegisterUnityClass<Shader>(const char*);
class ShaderNameRegistry; template <> void RegisterUnityClass<ShaderNameRegistry>(const char*);
class Sprite; template <> void RegisterUnityClass<Sprite>(const char*);
class SpriteRenderer; template <> void RegisterUnityClass<SpriteRenderer>(const char*);
class TagManager; template <> void RegisterUnityClass<TagManager>(const char*);
class Texture; template <> void RegisterUnityClass<Texture>(const char*);
class Texture2D; template <> void RegisterUnityClass<Texture2D>(const char*);
class Texture2DArray; template <> void RegisterUnityClass<Texture2DArray>(const char*);
class Texture3D; template <> void RegisterUnityClass<Texture3D>(const char*);
class TimeManager; template <> void RegisterUnityClass<TimeManager>(const char*);
class Transform; template <> void RegisterUnityClass<Transform>(const char*);
class BoxCollider; template <> void RegisterUnityClass<BoxCollider>(const char*);
class Collider; template <> void RegisterUnityClass<Collider>(const char*);
class PhysicsManager; template <> void RegisterUnityClass<PhysicsManager>(const char*);
class Rigidbody; template <> void RegisterUnityClass<Rigidbody>(const char*);

void RegisterAllClasses()
{
void RegisterBuiltinTypes();
RegisterBuiltinTypes();
	//Total: 58 non stripped classes
	//0. AudioBehaviour
	RegisterUnityClass<AudioBehaviour>("Audio");
	//1. AudioClip
	RegisterUnityClass<AudioClip>("Audio");
	//2. AudioListener
	RegisterUnityClass<AudioListener>("Audio");
	//3. AudioManager
	RegisterUnityClass<AudioManager>("Audio");
	//4. SampleClip
	RegisterUnityClass<SampleClip>("Audio");
	//5. Behaviour
	RegisterUnityClass<Behaviour>("Core");
	//6. BuildSettings
	RegisterUnityClass<BuildSettings>("Core");
	//7. Camera
	RegisterUnityClass<Camera>("Core");
	//8. Component
	RegisterUnityClass<Unity::Component>("Core");
	//9. ComputeShader
	RegisterUnityClass<ComputeShader>("Core");
	//10. Cubemap
	RegisterUnityClass<Cubemap>("Core");
	//11. CubemapArray
	RegisterUnityClass<CubemapArray>("Core");
	//12. DelayedCallManager
	RegisterUnityClass<DelayedCallManager>("Core");
	//13. EditorExtension
	RegisterUnityClass<EditorExtension>("Core");
	//14. GameManager
	RegisterUnityClass<GameManager>("Core");
	//15. GameObject
	RegisterUnityClass<GameObject>("Core");
	//16. GlobalGameManager
	RegisterUnityClass<GlobalGameManager>("Core");
	//17. GraphicsSettings
	RegisterUnityClass<GraphicsSettings>("Core");
	//18. InputManager
	RegisterUnityClass<InputManager>("Core");
	//19. LevelGameManager
	RegisterUnityClass<LevelGameManager>("Core");
	//20. Light
	RegisterUnityClass<Light>("Core");
	//21. LightProbes
	RegisterUnityClass<LightProbes>("Core");
	//22. LightingSettings
	RegisterUnityClass<LightingSettings>("Core");
	//23. LightmapSettings
	RegisterUnityClass<LightmapSettings>("Core");
	//24. LowerResBlitTexture
	RegisterUnityClass<LowerResBlitTexture>("Core");
	//25. Material
	RegisterUnityClass<Material>("Core");
	//26. Mesh
	RegisterUnityClass<Mesh>("Core");
	//27. MeshFilter
	RegisterUnityClass<MeshFilter>("Core");
	//28. MeshRenderer
	RegisterUnityClass<MeshRenderer>("Core");
	//29. MonoBehaviour
	RegisterUnityClass<MonoBehaviour>("Core");
	//30. MonoManager
	RegisterUnityClass<MonoManager>("Core");
	//31. NamedObject
	RegisterUnityClass<NamedObject>("Core");
	//32. Object
	//Skipping Object
	//33. PlayerSettings
	RegisterUnityClass<PlayerSettings>("Core");
	//34. PreloadData
	RegisterUnityClass<PreloadData>("Core");
	//35. QualitySettings
	RegisterUnityClass<QualitySettings>("Core");
	//36. RectTransform
	RegisterUnityClass<UI::RectTransform>("Core");
	//37. ReflectionProbe
	RegisterUnityClass<ReflectionProbe>("Core");
	//38. RenderSettings
	RegisterUnityClass<RenderSettings>("Core");
	//39. RenderTexture
	RegisterUnityClass<RenderTexture>("Core");
	//40. Renderer
	RegisterUnityClass<Renderer>("Core");
	//41. ResourceManager
	RegisterUnityClass<ResourceManager>("Core");
	//42. RuntimeInitializeOnLoadManager
	RegisterUnityClass<RuntimeInitializeOnLoadManager>("Core");
	//43. Shader
	RegisterUnityClass<Shader>("Core");
	//44. ShaderNameRegistry
	RegisterUnityClass<ShaderNameRegistry>("Core");
	//45. Sprite
	RegisterUnityClass<Sprite>("Core");
	//46. SpriteRenderer
	RegisterUnityClass<SpriteRenderer>("Core");
	//47. TagManager
	RegisterUnityClass<TagManager>("Core");
	//48. Texture
	RegisterUnityClass<Texture>("Core");
	//49. Texture2D
	RegisterUnityClass<Texture2D>("Core");
	//50. Texture2DArray
	RegisterUnityClass<Texture2DArray>("Core");
	//51. Texture3D
	RegisterUnityClass<Texture3D>("Core");
	//52. TimeManager
	RegisterUnityClass<TimeManager>("Core");
	//53. Transform
	RegisterUnityClass<Transform>("Core");
	//54. BoxCollider
	RegisterUnityClass<BoxCollider>("Physics");
	//55. Collider
	RegisterUnityClass<Collider>("Physics");
	//56. PhysicsManager
	RegisterUnityClass<PhysicsManager>("Physics");
	//57. Rigidbody
	RegisterUnityClass<Rigidbody>("Physics");

}
