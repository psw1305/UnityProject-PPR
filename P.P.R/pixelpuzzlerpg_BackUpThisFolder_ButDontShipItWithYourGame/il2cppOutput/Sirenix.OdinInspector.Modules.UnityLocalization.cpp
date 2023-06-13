#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>
#include <stdint.h>


template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R, typename T1>
struct VirtualFuncInvoker1
{
	typedef R (*Func)(void*, T1, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename R, typename T1, typename T2>
struct VirtualFuncInvoker2
{
	typedef R (*Func)(void*, T1, T2, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1, T2 p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, p1, p2, invokeData.method);
	}
};
template <typename T1>
struct InterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};

// System.Action`1<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle`1<UnityEngine.Localization.Settings.LocalizedDatabase`2/TableEntryResult<UnityEngine.Localization.Tables.StringTable,UnityEngine.Localization.Tables.StringTableEntry>>>
struct Action_1_tF3394C0737C781F84474280B1455BF16E20A3F9A;
// System.Action`1<UnityEngine.Localization.SmartFormat.PersistentVariables.IVariable>
struct Action_1_t9E9C20173C908A2F03243A56F3CAB1EC014C628A;
// System.Action`1<UnityEngine.Localization.Locale>
struct Action_1_t55C64C85A9D0051A87CCECD2C9B66D12616ECC38;
// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase`1<UnityEngine.Localization.Settings.LocalizedDatabase`2/TableEntryResult<UnityEngine.Localization.Tables.StringTable,UnityEngine.Localization.Tables.StringTableEntry>>
struct AsyncOperationBase_1_t0FB87C32EB2974CE880C821A0C5BD15A2093BA61;
// System.Collections.Generic.Dictionary`2<System.Guid,System.String>
struct Dictionary_2_t1237B68284D10128976C2AEC3CB7849642DCBF0C;
// System.Collections.Generic.Dictionary`2<System.String,System.Guid>
struct Dictionary_2_tA80F509096B4D8D9900583577EAA0036147FD60E;
// System.Collections.Generic.Dictionary`2<System.String,UnityEngine.Localization.SmartFormat.PersistentVariables.VariableNameValuePair>
struct Dictionary_2_t6C4B079B538B85DC604C0B3C2C22192BADFA0394;
// System.Collections.Generic.IList`1<System.Object>
struct IList_1_t6EE90D273EFCF5E7E4C37FAB712E70BB6F1B4BFF;
// System.Collections.Generic.List`1<UnityEngine.Localization.SmartFormat.PersistentVariables.IVariableValueChanged>
struct List_1_t5AAF96CEA347C0E29364E69185F89FD157DFBB35;
// System.Collections.Generic.List`1<UnityEngine.Localization.SmartFormat.PersistentVariables.VariableNameValuePair>
struct List_1_t7D38835D1755C7A479CBFD472D9248D626D68F5A;
// System.Collections.Generic.List`1<UnityEngine.Localization.LocalizedString/ChangeHandler>
struct List_1_tD6ECCE8B36BDE07CE54AE73E73C04F763A952824;
// Sirenix.Serialization.ReflectionFormatter`1<UnityEngine.Localization.LocalizedString>
struct ReflectionFormatter_1_t4E881E1C919D16D979FA6083E0C3F6FF17A15397;
// Sirenix.Serialization.ReflectionFormatter`1<System.Object>
struct ReflectionFormatter_1_tB6DAF53F6F410C9664F5960209ABDD85E507E2D8;
// Sirenix.Serialization.ReflectionOrEmittedBaseFormatter`1<UnityEngine.Localization.LocalizedString>
struct ReflectionOrEmittedBaseFormatter_1_t9B220475B0028EB129D93E7C216E7C9C4F058793;
// Sirenix.Serialization.ReflectionOrEmittedBaseFormatter`1<System.Object>
struct ReflectionOrEmittedBaseFormatter_1_t82DA4CE30AC6813C536F4AE75A7C280B454BF7BB;
// Sirenix.Serialization.BaseFormatter`1/SerializationCallback<UnityEngine.Localization.LocalizedString>[]
struct SerializationCallbackU5BU5D_t9A1707F6726FC4BF6BF66C9B8A09C0F46B450866;
// System.Type[]
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
// UnityEngine.Localization.LocalizedString/ChangeHandler[]
struct ChangeHandlerU5BU5D_t67D12FDA1DDDE79050C70400D49899739E73CAB3;
// System.Reflection.Binder
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
// System.Reflection.FieldInfo
struct FieldInfo_t;
// Sirenix.Serialization.IDataReader
struct IDataReader_t387D8EF56B60D4587344CF62EFED4C811EC91816;
// Sirenix.Serialization.ILogger
struct ILogger_t264A82F87DDF77784E6FBFF7D05A2D64E60CD036;
// Sirenix.Serialization.ISerializationPolicy
struct ISerializationPolicy_t0C47BF78FD9B96AE6371585EAE63593988C2FA43;
// UnityEngine.Localization.Locale
struct Locale_t7BD9FB50247883E73B4CC92360943B0455CF02B9;
// UnityEngine.Localization.LocalizedString
struct LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28;
// Sirenix.OdinInspector.Modules.Localization.LocalizedStringFormatter
struct LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047;
// System.Reflection.MemberFilter
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
// System.String
struct String_t;
// System.Type
struct Type_t;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
// UnityEngine.Localization.LocalizedString/ChangeHandler
struct ChangeHandler_t6B90664E6C6149517967E9101DCB19DABA8180F7;

IL2CPP_EXTERN_C RuntimeClass* DefaultLoggers_t7B5C368633568D7990EF1EF8497E117680CF9C89_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ILogger_t264A82F87DDF77784E6FBFF7D05A2D64E60CD036_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Type_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteralE90E3031BCCE9F7E5A89229E40B0FD00EE6A8F5E;
IL2CPP_EXTERN_C String_t* _stringLiteralE9BC2D4314B73DB648242D9AA3A4814DA755DD4C;
IL2CPP_EXTERN_C const RuntimeMethod* ReflectionFormatter_1_DeserializeImplementation_m42FBE86EE5E3F5DAF402D30FB37819EA4F5BA377_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* ReflectionOrEmittedBaseFormatter_1__ctor_m6E69E0336017194AA62F893A7DFBFAD1F0841F52_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeType* LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28_0_0_0_var;


IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// <Module>
struct U3CModuleU3E_tE2EB4C3EC1CD5B86F62A4B56739DF0843EE83BCC 
{
};

// Sirenix.Serialization.BaseFormatter`1<UnityEngine.Localization.LocalizedString>
struct BaseFormatter_1_tE9BFDCA1F73AD457C69CFDD5BEA12E0E94907202  : public RuntimeObject
{
};

struct BaseFormatter_1_tE9BFDCA1F73AD457C69CFDD5BEA12E0E94907202_StaticFields
{
	// Sirenix.Serialization.BaseFormatter`1/SerializationCallback<T>[] Sirenix.Serialization.BaseFormatter`1::OnSerializingCallbacks
	SerializationCallbackU5BU5D_t9A1707F6726FC4BF6BF66C9B8A09C0F46B450866* ___OnSerializingCallbacks_0;
	// Sirenix.Serialization.BaseFormatter`1/SerializationCallback<T>[] Sirenix.Serialization.BaseFormatter`1::OnSerializedCallbacks
	SerializationCallbackU5BU5D_t9A1707F6726FC4BF6BF66C9B8A09C0F46B450866* ___OnSerializedCallbacks_1;
	// Sirenix.Serialization.BaseFormatter`1/SerializationCallback<T>[] Sirenix.Serialization.BaseFormatter`1::OnDeserializingCallbacks
	SerializationCallbackU5BU5D_t9A1707F6726FC4BF6BF66C9B8A09C0F46B450866* ___OnDeserializingCallbacks_2;
	// Sirenix.Serialization.BaseFormatter`1/SerializationCallback<T>[] Sirenix.Serialization.BaseFormatter`1::OnDeserializedCallbacks
	SerializationCallbackU5BU5D_t9A1707F6726FC4BF6BF66C9B8A09C0F46B450866* ___OnDeserializedCallbacks_3;
	// System.Boolean Sirenix.Serialization.BaseFormatter`1::IsValueType
	bool ___IsValueType_4;
	// System.Boolean Sirenix.Serialization.BaseFormatter`1::ImplementsISerializationCallbackReceiver
	bool ___ImplementsISerializationCallbackReceiver_5;
	// System.Boolean Sirenix.Serialization.BaseFormatter`1::ImplementsIDeserializationCallback
	bool ___ImplementsIDeserializationCallback_6;
	// System.Boolean Sirenix.Serialization.BaseFormatter`1::ImplementsIObjectReference
	bool ___ImplementsIObjectReference_7;
};
struct Il2CppArrayBounds;

// System.Reflection.MemberInfo
struct MemberInfo_t  : public RuntimeObject
{
};

// System.String
struct String_t  : public RuntimeObject
{
	// System.Int32 System.String::_stringLength
	int32_t ____stringLength_4;
	// System.Char System.String::_firstChar
	Il2CppChar ____firstChar_5;
};

struct String_t_StaticFields
{
	// System.String System.String::Empty
	String_t* ___Empty_6;
};

// System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};

// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle`1<UnityEngine.Localization.Settings.LocalizedDatabase`2/TableEntryResult<UnityEngine.Localization.Tables.StringTable,UnityEngine.Localization.Tables.StringTableEntry>>
struct AsyncOperationHandle_1_t3B992A8DC6CF513772D0CA06B01F226629E98117 
{
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase`1<TObject> UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle`1::m_InternalOp
	AsyncOperationBase_1_t0FB87C32EB2974CE880C821A0C5BD15A2093BA61* ___m_InternalOp_0;
	// System.Int32 UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle`1::m_Version
	int32_t ___m_Version_1;
	// System.String UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle`1::m_LocationName
	String_t* ___m_LocationName_2;
	// System.Boolean UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle`1::m_UnloadSceneOpExcludeReleaseCallback
	bool ___m_UnloadSceneOpExcludeReleaseCallback_3;
};

// UnityEngine.Localization.CallbackArray`1<UnityEngine.Localization.LocalizedString/ChangeHandler>
struct CallbackArray_1_t395725C7A2BC41DCB9AB3523E35FDAF319C39DD6 
{
	// TDelegate UnityEngine.Localization.CallbackArray`1::m_SingleDelegate
	ChangeHandler_t6B90664E6C6149517967E9101DCB19DABA8180F7* ___m_SingleDelegate_1;
	// TDelegate[] UnityEngine.Localization.CallbackArray`1::m_MultipleDelegates
	ChangeHandlerU5BU5D_t67D12FDA1DDDE79050C70400D49899739E73CAB3* ___m_MultipleDelegates_2;
	// System.Collections.Generic.List`1<TDelegate> UnityEngine.Localization.CallbackArray`1::m_AddCallbacks
	List_1_tD6ECCE8B36BDE07CE54AE73E73C04F763A952824* ___m_AddCallbacks_3;
	// System.Collections.Generic.List`1<TDelegate> UnityEngine.Localization.CallbackArray`1::m_RemoveCallbacks
	List_1_tD6ECCE8B36BDE07CE54AE73E73C04F763A952824* ___m_RemoveCallbacks_4;
	// System.Int32 UnityEngine.Localization.CallbackArray`1::m_Length
	int32_t ___m_Length_5;
	// System.Boolean UnityEngine.Localization.CallbackArray`1::m_CannotMutateCallbacksArray
	bool ___m_CannotMutateCallbacksArray_6;
	// System.Boolean UnityEngine.Localization.CallbackArray`1::m_MutatedDuringCallback
	bool ___m_MutatedDuringCallback_7;
};

// Sirenix.Serialization.ReflectionFormatter`1<UnityEngine.Localization.LocalizedString>
struct ReflectionFormatter_1_t4E881E1C919D16D979FA6083E0C3F6FF17A15397  : public BaseFormatter_1_tE9BFDCA1F73AD457C69CFDD5BEA12E0E94907202
{
	// Sirenix.Serialization.ISerializationPolicy Sirenix.Serialization.ReflectionFormatter`1::<OverridePolicy>k__BackingField
	RuntimeObject* ___U3COverridePolicyU3Ek__BackingField_8;
};

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	// System.Boolean System.Boolean::m_value
	bool ___m_value_0;
};

struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	// System.String System.Boolean::TrueString
	String_t* ___TrueString_5;
	// System.String System.Boolean::FalseString
	String_t* ___FalseString_6;
};

// System.Reflection.FieldInfo
struct FieldInfo_t  : public MemberInfo_t
{
};

// System.Guid
struct Guid_t 
{
	// System.Int32 System.Guid::_a
	int32_t ____a_1;
	// System.Int16 System.Guid::_b
	int16_t ____b_2;
	// System.Int16 System.Guid::_c
	int16_t ____c_3;
	// System.Byte System.Guid::_d
	uint8_t ____d_4;
	// System.Byte System.Guid::_e
	uint8_t ____e_5;
	// System.Byte System.Guid::_f
	uint8_t ____f_6;
	// System.Byte System.Guid::_g
	uint8_t ____g_7;
	// System.Byte System.Guid::_h
	uint8_t ____h_8;
	// System.Byte System.Guid::_i
	uint8_t ____i_9;
	// System.Byte System.Guid::_j
	uint8_t ____j_10;
	// System.Byte System.Guid::_k
	uint8_t ____k_11;
};

struct Guid_t_StaticFields
{
	// System.Guid System.Guid::Empty
	Guid_t ___Empty_0;
};

// System.IntPtr
struct IntPtr_t 
{
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;
};

struct IntPtr_t_StaticFields
{
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;
};

// UnityEngine.Localization.Tables.TableEntryReference
struct TableEntryReference_tF899CF4E8EF8DC219E8DDB13CFB1B5ED86077224 
{
	// System.Int64 UnityEngine.Localization.Tables.TableEntryReference::m_KeyId
	int64_t ___m_KeyId_0;
	// System.String UnityEngine.Localization.Tables.TableEntryReference::m_Key
	String_t* ___m_Key_1;
	// System.Boolean UnityEngine.Localization.Tables.TableEntryReference::m_Valid
	bool ___m_Valid_2;
	// UnityEngine.Localization.Tables.TableEntryReference/Type UnityEngine.Localization.Tables.TableEntryReference::<ReferenceType>k__BackingField
	int32_t ___U3CReferenceTypeU3Ek__BackingField_3;
};
// Native definition for P/Invoke marshalling of UnityEngine.Localization.Tables.TableEntryReference
struct TableEntryReference_tF899CF4E8EF8DC219E8DDB13CFB1B5ED86077224_marshaled_pinvoke
{
	int64_t ___m_KeyId_0;
	char* ___m_Key_1;
	int32_t ___m_Valid_2;
	int32_t ___U3CReferenceTypeU3Ek__BackingField_3;
};
// Native definition for COM marshalling of UnityEngine.Localization.Tables.TableEntryReference
struct TableEntryReference_tF899CF4E8EF8DC219E8DDB13CFB1B5ED86077224_marshaled_com
{
	int64_t ___m_KeyId_0;
	Il2CppChar* ___m_Key_1;
	int32_t ___m_Valid_2;
	int32_t ___U3CReferenceTypeU3Ek__BackingField_3;
};

// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};

// Sirenix.Serialization.ReflectionOrEmittedBaseFormatter`1<UnityEngine.Localization.LocalizedString>
struct ReflectionOrEmittedBaseFormatter_1_t9B220475B0028EB129D93E7C216E7C9C4F058793  : public ReflectionFormatter_1_t4E881E1C919D16D979FA6083E0C3F6FF17A15397
{
};

// System.RuntimeTypeHandle
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	// System.IntPtr System.RuntimeTypeHandle::value
	intptr_t ___value_0;
};

// UnityEngine.Localization.Tables.TableReference
struct TableReference_t96A5F0161FDA37E187436FC17A3B6D7AB676A147 
{
	// System.String UnityEngine.Localization.Tables.TableReference::m_TableCollectionName
	String_t* ___m_TableCollectionName_2;
	// System.Boolean UnityEngine.Localization.Tables.TableReference::m_Valid
	bool ___m_Valid_3;
	// UnityEngine.Localization.Tables.TableReference/Type UnityEngine.Localization.Tables.TableReference::<ReferenceType>k__BackingField
	int32_t ___U3CReferenceTypeU3Ek__BackingField_5;
	// System.Guid UnityEngine.Localization.Tables.TableReference::<TableCollectionNameGuid>k__BackingField
	Guid_t ___U3CTableCollectionNameGuidU3Ek__BackingField_6;
};

struct TableReference_t96A5F0161FDA37E187436FC17A3B6D7AB676A147_StaticFields
{
	// System.Collections.Generic.Dictionary`2<System.Guid,System.String> UnityEngine.Localization.Tables.TableReference::s_GuidToStringCache
	Dictionary_2_t1237B68284D10128976C2AEC3CB7849642DCBF0C* ___s_GuidToStringCache_0;
	// System.Collections.Generic.Dictionary`2<System.String,System.Guid> UnityEngine.Localization.Tables.TableReference::s_StringToGuidCache
	Dictionary_2_tA80F509096B4D8D9900583577EAA0036147FD60E* ___s_StringToGuidCache_1;
};
// Native definition for P/Invoke marshalling of UnityEngine.Localization.Tables.TableReference
struct TableReference_t96A5F0161FDA37E187436FC17A3B6D7AB676A147_marshaled_pinvoke
{
	char* ___m_TableCollectionName_2;
	int32_t ___m_Valid_3;
	int32_t ___U3CReferenceTypeU3Ek__BackingField_5;
	Guid_t ___U3CTableCollectionNameGuidU3Ek__BackingField_6;
};
// Native definition for COM marshalling of UnityEngine.Localization.Tables.TableReference
struct TableReference_t96A5F0161FDA37E187436FC17A3B6D7AB676A147_marshaled_com
{
	Il2CppChar* ___m_TableCollectionName_2;
	int32_t ___m_Valid_3;
	int32_t ___U3CReferenceTypeU3Ek__BackingField_5;
	Guid_t ___U3CTableCollectionNameGuidU3Ek__BackingField_6;
};

// UnityEngine.Localization.LocalizedReference
struct LocalizedReference_t7345BAA2758915B7F5BE701F2B89C5038CE93BDC  : public RuntimeObject
{
	// UnityEngine.Localization.Tables.TableReference UnityEngine.Localization.LocalizedReference::m_TableReference
	TableReference_t96A5F0161FDA37E187436FC17A3B6D7AB676A147 ___m_TableReference_0;
	// UnityEngine.Localization.Tables.TableEntryReference UnityEngine.Localization.LocalizedReference::m_TableEntryReference
	TableEntryReference_tF899CF4E8EF8DC219E8DDB13CFB1B5ED86077224 ___m_TableEntryReference_1;
	// UnityEngine.Localization.Settings.FallbackBehavior UnityEngine.Localization.LocalizedReference::m_FallbackState
	int32_t ___m_FallbackState_2;
	// System.Boolean UnityEngine.Localization.LocalizedReference::m_WaitForCompletion
	bool ___m_WaitForCompletion_3;
	// UnityEngine.Localization.Locale UnityEngine.Localization.LocalizedReference::m_LocaleOverride
	Locale_t7BD9FB50247883E73B4CC92360943B0455CF02B9* ___m_LocaleOverride_4;
};

// Sirenix.OdinInspector.Modules.Localization.LocalizedStringFormatter
struct LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047  : public ReflectionOrEmittedBaseFormatter_1_t9B220475B0028EB129D93E7C216E7C9C4F058793
{
};

struct LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_StaticFields
{
	// System.Reflection.FieldInfo Sirenix.OdinInspector.Modules.Localization.LocalizedStringFormatter::m_LocalVariables_Field
	FieldInfo_t* ___m_LocalVariables_Field_9;
};

// System.Type
struct Type_t  : public MemberInfo_t
{
	// System.RuntimeTypeHandle System.Type::_impl
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ____impl_8;
};

struct Type_t_StaticFields
{
	// System.Reflection.Binder modreq(System.Runtime.CompilerServices.IsVolatile) System.Type::s_defaultBinder
	Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235* ___s_defaultBinder_0;
	// System.Char System.Type::Delimiter
	Il2CppChar ___Delimiter_1;
	// System.Type[] System.Type::EmptyTypes
	TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* ___EmptyTypes_2;
	// System.Object System.Type::Missing
	RuntimeObject* ___Missing_3;
	// System.Reflection.MemberFilter System.Type::FilterAttribute
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterAttribute_4;
	// System.Reflection.MemberFilter System.Type::FilterName
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterName_5;
	// System.Reflection.MemberFilter System.Type::FilterNameIgnoreCase
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterNameIgnoreCase_6;
};

// UnityEngine.Localization.LocalizedString
struct LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28  : public LocalizedReference_t7345BAA2758915B7F5BE701F2B89C5038CE93BDC
{
	// System.Collections.Generic.List`1<UnityEngine.Localization.SmartFormat.PersistentVariables.VariableNameValuePair> UnityEngine.Localization.LocalizedString::m_LocalVariables
	List_1_t7D38835D1755C7A479CBFD472D9248D626D68F5A* ___m_LocalVariables_5;
	// UnityEngine.Localization.CallbackArray`1<UnityEngine.Localization.LocalizedString/ChangeHandler> UnityEngine.Localization.LocalizedString::m_ChangeHandler
	CallbackArray_1_t395725C7A2BC41DCB9AB3523E35FDAF319C39DD6 ___m_ChangeHandler_6;
	// System.String UnityEngine.Localization.LocalizedString::m_CurrentStringChangedValue
	String_t* ___m_CurrentStringChangedValue_7;
	// System.Collections.Generic.Dictionary`2<System.String,UnityEngine.Localization.SmartFormat.PersistentVariables.VariableNameValuePair> UnityEngine.Localization.LocalizedString::m_VariableLookup
	Dictionary_2_t6C4B079B538B85DC604C0B3C2C22192BADFA0394* ___m_VariableLookup_8;
	// System.Collections.Generic.List`1<UnityEngine.Localization.SmartFormat.PersistentVariables.IVariableValueChanged> UnityEngine.Localization.LocalizedString::m_UsedVariables
	List_1_t5AAF96CEA347C0E29364E69185F89FD157DFBB35* ___m_UsedVariables_9;
	// System.Action`1<UnityEngine.Localization.SmartFormat.PersistentVariables.IVariable> UnityEngine.Localization.LocalizedString::m_OnVariableChanged
	Action_1_t9E9C20173C908A2F03243A56F3CAB1EC014C628A* ___m_OnVariableChanged_10;
	// System.Action`1<UnityEngine.Localization.Locale> UnityEngine.Localization.LocalizedString::m_SelectedLocaleChanged
	Action_1_t55C64C85A9D0051A87CCECD2C9B66D12616ECC38* ___m_SelectedLocaleChanged_11;
	// System.Action`1<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle`1<UnityEngine.Localization.Settings.LocalizedDatabase`2/TableEntryResult<UnityEngine.Localization.Tables.StringTable,UnityEngine.Localization.Tables.StringTableEntry>>> UnityEngine.Localization.LocalizedString::m_AutomaticLoadingCompleted
	Action_1_tF3394C0737C781F84474280B1455BF16E20A3F9A* ___m_AutomaticLoadingCompleted_12;
	// System.Action`1<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle`1<UnityEngine.Localization.Settings.LocalizedDatabase`2/TableEntryResult<UnityEngine.Localization.Tables.StringTable,UnityEngine.Localization.Tables.StringTableEntry>>> UnityEngine.Localization.LocalizedString::m_CompletedSourceValue
	Action_1_tF3394C0737C781F84474280B1455BF16E20A3F9A* ___m_CompletedSourceValue_13;
	// System.Boolean UnityEngine.Localization.LocalizedString::m_WaitingForVariablesEndUpdate
	bool ___m_WaitingForVariablesEndUpdate_14;
	// System.Action`1<UnityEngine.Localization.SmartFormat.PersistentVariables.IVariable> UnityEngine.Localization.LocalizedString::ValueChanged
	Action_1_t9E9C20173C908A2F03243A56F3CAB1EC014C628A* ___ValueChanged_15;
	// System.Collections.Generic.IList`1<System.Object> UnityEngine.Localization.LocalizedString::<Arguments>k__BackingField
	RuntimeObject* ___U3CArgumentsU3Ek__BackingField_16;
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle`1<UnityEngine.Localization.Settings.LocalizedDatabase`2/TableEntryResult<UnityEngine.Localization.Tables.StringTable,UnityEngine.Localization.Tables.StringTableEntry>> UnityEngine.Localization.LocalizedString::<CurrentLoadingOperationHandle>k__BackingField
	AsyncOperationHandle_1_t3B992A8DC6CF513772D0CA06B01F226629E98117 ___U3CCurrentLoadingOperationHandleU3Ek__BackingField_17;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif


// System.Void Sirenix.Serialization.ReflectionFormatter`1<System.Object>::DeserializeImplementation(T&,Sirenix.Serialization.IDataReader)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReflectionFormatter_1_DeserializeImplementation_m16A4BC7E05572EAED41FF87F8D2A40324B9EADCF_gshared (ReflectionFormatter_1_tB6DAF53F6F410C9664F5960209ABDD85E507E2D8* __this, RuntimeObject** ___value0, RuntimeObject* ___reader1, const RuntimeMethod* method) ;
// System.Void Sirenix.Serialization.ReflectionOrEmittedBaseFormatter`1<System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ReflectionOrEmittedBaseFormatter_1__ctor_m4A7E77420E4400DFB3D118363F67075731A45B85_gshared (ReflectionOrEmittedBaseFormatter_1_t82DA4CE30AC6813C536F4AE75A7C280B454BF7BB* __this, const RuntimeMethod* method) ;

// System.Type System.Type::GetTypeFromHandle(System.RuntimeTypeHandle)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57 (RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ___handle0, const RuntimeMethod* method) ;
// System.Boolean System.Reflection.FieldInfo::op_Equality(System.Reflection.FieldInfo,System.Reflection.FieldInfo)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool FieldInfo_op_Equality_mA38D84E1D9AA016F414CF2265C4B0DB1FEBBAB74 (FieldInfo_t* ___left0, FieldInfo_t* ___right1, const RuntimeMethod* method) ;
// Sirenix.Serialization.ILogger Sirenix.Serialization.DefaultLoggers::get_DefaultLogger()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* DefaultLoggers_get_DefaultLogger_mA3CC5060E6E8E0229A24386D671C5E0166697888 (const RuntimeMethod* method) ;
// System.Void UnityEngine.Localization.LocalizedString::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LocalizedString__ctor_m0AB3C98B488085AFCEC6604AA1D7D4E354C92874 (LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28* __this, const RuntimeMethod* method) ;
// System.Void Sirenix.Serialization.ReflectionFormatter`1<UnityEngine.Localization.LocalizedString>::DeserializeImplementation(T&,Sirenix.Serialization.IDataReader)
inline void ReflectionFormatter_1_DeserializeImplementation_m42FBE86EE5E3F5DAF402D30FB37819EA4F5BA377 (ReflectionFormatter_1_t4E881E1C919D16D979FA6083E0C3F6FF17A15397* __this, LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28** ___value0, RuntimeObject* ___reader1, const RuntimeMethod* method)
{
	((  void (*) (ReflectionFormatter_1_t4E881E1C919D16D979FA6083E0C3F6FF17A15397*, LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28**, RuntimeObject*, const RuntimeMethod*))ReflectionFormatter_1_DeserializeImplementation_m16A4BC7E05572EAED41FF87F8D2A40324B9EADCF_gshared)(__this, ___value0, ___reader1, method);
}
// System.Boolean System.Reflection.FieldInfo::op_Inequality(System.Reflection.FieldInfo,System.Reflection.FieldInfo)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool FieldInfo_op_Inequality_m95789A98E646494987E66A9E4188DCA86185066B (FieldInfo_t* ___left0, FieldInfo_t* ___right1, const RuntimeMethod* method) ;
// System.Object System.Activator::CreateInstance(System.Type)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Activator_CreateInstance_mFF030428C64FDDFACC74DFAC97388A1C628BFBCF (Type_t* ___type0, const RuntimeMethod* method) ;
// System.Void System.Reflection.FieldInfo::SetValue(System.Object,System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FieldInfo_SetValue_mD8C0DA3A1A0CFF073F971622BBDBAAB6688B4B6C (FieldInfo_t* __this, RuntimeObject* ___obj0, RuntimeObject* ___value1, const RuntimeMethod* method) ;
// System.Void Sirenix.Serialization.ReflectionOrEmittedBaseFormatter`1<UnityEngine.Localization.LocalizedString>::.ctor()
inline void ReflectionOrEmittedBaseFormatter_1__ctor_m6E69E0336017194AA62F893A7DFBFAD1F0841F52 (ReflectionOrEmittedBaseFormatter_1_t9B220475B0028EB129D93E7C216E7C9C4F058793* __this, const RuntimeMethod* method)
{
	((  void (*) (ReflectionOrEmittedBaseFormatter_1_t9B220475B0028EB129D93E7C216E7C9C4F058793*, const RuntimeMethod*))ReflectionOrEmittedBaseFormatter_1__ctor_m4A7E77420E4400DFB3D118363F67075731A45B85_gshared)(__this, method);
}
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void Sirenix.OdinInspector.Modules.Localization.LocalizedStringFormatter::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LocalizedStringFormatter__cctor_m6924025BB133FC1C18FFF9AC3CAA8089574E27EB (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DefaultLoggers_t7B5C368633568D7990EF1EF8497E117680CF9C89_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ILogger_t264A82F87DDF77784E6FBFF7D05A2D64E60CD036_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Type_t_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE90E3031BCCE9F7E5A89229E40B0FD00EE6A8F5E);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE9BC2D4314B73DB648242D9AA3A4814DA755DD4C);
		s_Il2CppMethodInitialized = true;
	}
	{
		// m_LocalVariables_Field = typeof(LocalizedString).GetField("m_LocalVariables", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_0 = { reinterpret_cast<intptr_t> (LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28_0_0_0_var) };
		il2cpp_codegen_runtime_class_init_inline(Type_t_il2cpp_TypeInfo_var);
		Type_t* L_1;
		L_1 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_0, NULL);
		NullCheck(L_1);
		FieldInfo_t* L_2;
		L_2 = VirtualFuncInvoker2< FieldInfo_t*, String_t*, int32_t >::Invoke(89 /* System.Reflection.FieldInfo System.Type::GetField(System.String,System.Reflection.BindingFlags) */, L_1, _stringLiteralE90E3031BCCE9F7E5A89229E40B0FD00EE6A8F5E, ((int32_t)52));
		((LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_StaticFields*)il2cpp_codegen_static_fields_for(LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_il2cpp_TypeInfo_var))->___m_LocalVariables_Field_9 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&((LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_StaticFields*)il2cpp_codegen_static_fields_for(LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_il2cpp_TypeInfo_var))->___m_LocalVariables_Field_9), (void*)L_2);
		// if (m_LocalVariables_Field == null)
		FieldInfo_t* L_3 = ((LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_StaticFields*)il2cpp_codegen_static_fields_for(LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_il2cpp_TypeInfo_var))->___m_LocalVariables_Field_9;
		bool L_4;
		L_4 = FieldInfo_op_Equality_mA38D84E1D9AA016F414CF2265C4B0DB1FEBBAB74(L_3, (FieldInfo_t*)NULL, NULL);
		if (!L_4)
		{
			goto IL_0037;
		}
	}
	{
		// DefaultLoggers.DefaultLogger.LogError("Could not find field 'UnityEngine.LocalizedString.m_LocalVariables'" +
		//     " - the internals of the Localization package have changed, and deserialization of Odin-serialized" +
		//     " LocalizedString instances may be broken in some cases.");
		il2cpp_codegen_runtime_class_init_inline(DefaultLoggers_t7B5C368633568D7990EF1EF8497E117680CF9C89_il2cpp_TypeInfo_var);
		RuntimeObject* L_5;
		L_5 = DefaultLoggers_get_DefaultLogger_mA3CC5060E6E8E0229A24386D671C5E0166697888(NULL);
		NullCheck(L_5);
		InterfaceActionInvoker1< String_t* >::Invoke(1 /* System.Void Sirenix.Serialization.ILogger::LogError(System.String) */, ILogger_t264A82F87DDF77784E6FBFF7D05A2D64E60CD036_il2cpp_TypeInfo_var, L_5, _stringLiteralE9BC2D4314B73DB648242D9AA3A4814DA755DD4C);
	}

IL_0037:
	{
		// }
		return;
	}
}
// UnityEngine.Localization.LocalizedString Sirenix.OdinInspector.Modules.Localization.LocalizedStringFormatter::GetUninitializedObject()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28* LocalizedStringFormatter_GetUninitializedObject_m1B76058B6DC29C30255D8539FB276F88286B83D6 (LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return new LocalizedString();
		LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28* L_0 = (LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28*)il2cpp_codegen_object_new(LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		LocalizedString__ctor_m0AB3C98B488085AFCEC6604AA1D7D4E354C92874(L_0, NULL);
		return L_0;
	}
}
// System.Void Sirenix.OdinInspector.Modules.Localization.LocalizedStringFormatter::DeserializeImplementation(UnityEngine.Localization.LocalizedString&,Sirenix.Serialization.IDataReader)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LocalizedStringFormatter_DeserializeImplementation_m0BB6539141104D3F15C3773DC2FA79BD79646B10 (LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047* __this, LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28** ___value0, RuntimeObject* ___reader1, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ReflectionFormatter_1_DeserializeImplementation_m42FBE86EE5E3F5DAF402D30FB37819EA4F5BA377_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	{
		// base.DeserializeImplementation(ref value, reader);
		LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28** L_0 = ___value0;
		RuntimeObject* L_1 = ___reader1;
		ReflectionFormatter_1_DeserializeImplementation_m42FBE86EE5E3F5DAF402D30FB37819EA4F5BA377(__this, L_0, L_1, ReflectionFormatter_1_DeserializeImplementation_m42FBE86EE5E3F5DAF402D30FB37819EA4F5BA377_RuntimeMethod_var);
		// if (m_LocalVariables_Field != null && value != null)
		il2cpp_codegen_runtime_class_init_inline(LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_il2cpp_TypeInfo_var);
		FieldInfo_t* L_2 = ((LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_StaticFields*)il2cpp_codegen_static_fields_for(LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_il2cpp_TypeInfo_var))->___m_LocalVariables_Field_9;
		bool L_3;
		L_3 = FieldInfo_op_Inequality_m95789A98E646494987E66A9E4188DCA86185066B(L_2, (FieldInfo_t*)NULL, NULL);
		if (!L_3)
		{
			goto IL_0046;
		}
	}
	{
		LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28** L_4 = ___value0;
		LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28* L_5 = *((LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28**)L_4);
		if (!L_5)
		{
			goto IL_0046;
		}
	}
	{
		// var localVariablesList = m_LocalVariables_Field.GetValue(value);
		il2cpp_codegen_runtime_class_init_inline(LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_il2cpp_TypeInfo_var);
		FieldInfo_t* L_6 = ((LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_StaticFields*)il2cpp_codegen_static_fields_for(LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_il2cpp_TypeInfo_var))->___m_LocalVariables_Field_9;
		LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28** L_7 = ___value0;
		LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28* L_8 = *((LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28**)L_7);
		NullCheck(L_6);
		RuntimeObject* L_9;
		L_9 = VirtualFuncInvoker1< RuntimeObject*, RuntimeObject* >::Invoke(26 /* System.Object System.Reflection.FieldInfo::GetValue(System.Object) */, L_6, L_8);
		V_0 = L_9;
		// if (localVariablesList == null)
		RuntimeObject* L_10 = V_0;
		if (L_10)
		{
			goto IL_0046;
		}
	}
	{
		// localVariablesList = Activator.CreateInstance(m_LocalVariables_Field.FieldType);
		il2cpp_codegen_runtime_class_init_inline(LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_il2cpp_TypeInfo_var);
		FieldInfo_t* L_11 = ((LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_StaticFields*)il2cpp_codegen_static_fields_for(LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_il2cpp_TypeInfo_var))->___m_LocalVariables_Field_9;
		NullCheck(L_11);
		Type_t* L_12;
		L_12 = VirtualFuncInvoker0< Type_t* >::Invoke(17 /* System.Type System.Reflection.FieldInfo::get_FieldType() */, L_11);
		RuntimeObject* L_13;
		L_13 = Activator_CreateInstance_mFF030428C64FDDFACC74DFAC97388A1C628BFBCF(L_12, NULL);
		V_0 = L_13;
		// m_LocalVariables_Field.SetValue(value, localVariablesList);
		FieldInfo_t* L_14 = ((LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_StaticFields*)il2cpp_codegen_static_fields_for(LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047_il2cpp_TypeInfo_var))->___m_LocalVariables_Field_9;
		LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28** L_15 = ___value0;
		LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28* L_16 = *((LocalizedString_tFC63FF1AB38D2E097E3876DF9AA01CD473FF0F28**)L_15);
		RuntimeObject* L_17 = V_0;
		NullCheck(L_14);
		FieldInfo_SetValue_mD8C0DA3A1A0CFF073F971622BBDBAAB6688B4B6C(L_14, L_16, L_17, NULL);
	}

IL_0046:
	{
		// }
		return;
	}
}
// System.Void Sirenix.OdinInspector.Modules.Localization.LocalizedStringFormatter::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void LocalizedStringFormatter__ctor_mDC1D54F5D276E65962964272BBA015066FEB84EB (LocalizedStringFormatter_t5A750CC193621F647EA5741D0B23A6D00F8D2047* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ReflectionOrEmittedBaseFormatter_1__ctor_m6E69E0336017194AA62F893A7DFBFAD1F0841F52_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		ReflectionOrEmittedBaseFormatter_1__ctor_m6E69E0336017194AA62F893A7DFBFAD1F0841F52(__this, ReflectionOrEmittedBaseFormatter_1__ctor_m6E69E0336017194AA62F893A7DFBFAD1F0841F52_RuntimeMethod_var);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
