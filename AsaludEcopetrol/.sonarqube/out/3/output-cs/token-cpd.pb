ê
?D:\proyectos\asalud ecopetrol\Facede\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 

AssemblyTitle 
( 
$str !
)! "
]" #
[		 
assembly		 	
:			 

AssemblyDescription		 
(		 
$str		 !
)		! "
]		" #
[

 
assembly

 	
:

	 
!
AssemblyConfiguration

  
(

  !
$str

! #
)

# $
]

$ %
[ 
assembly 	
:	 

AssemblyCompany 
( 
$str 
) 
] 
[ 
assembly 	
:	 

AssemblyProduct 
( 
$str #
)# $
]$ %
[ 
assembly 	
:	 

AssemblyCopyright 
( 
$str 0
)0 1
]1 2
[ 
assembly 	
:	 

AssemblyTrademark 
( 
$str 
)  
]  !
[ 
assembly 	
:	 

AssemblyCulture 
( 
$str 
) 
] 
[ 
assembly 	
:	 


ComVisible 
( 
false 
) 
] 
[ 
assembly 	
:	 

Guid 
( 
$str 6
)6 7
]7 8
[## 
assembly## 	
:##	 

AssemblyVersion## 
(## 
$str## $
)##$ %
]##% &
[$$ 
assembly$$ 	
:$$	 

AssemblyFileVersion$$ 
($$ 
$str$$ (
)$$( )
]$$) *Ö‘
/D:\proyectos\asalud ecopetrol\Facede\IFacade.cs
	namespace		 	
Facede		
 
{

 
public 

	interface 
IFacade 
{ 
void  
ActualizaContraseÃ±a  
(  !
sis_usuario! ,

ObjUsuario- 7
,7 8
ref9 <
MessageResponseOBJ= O
MsgResP V
)V W
;W X
List 
< 
sis_usuario 
> 
ValidaIngreso '
(' (
sis_usuario( 3

ObjUsuario4 >
,> ?
ref@ C
MessageResponseOBJD V
MsgResW ]
)] ^
;^ _
List 
< 
ManagmentMenuResult  
>  !
ManagmentMenu" /
(/ 0
String0 6

Strusuario7 A
,A B
refC F
MessageResponseOBJG Y
MsgResZ `
)` a
;a b
List 
< 
sis_usuario 
> 
BuscaAuditorUsu )
() *
String* 0

strUsuario1 ;
,; <
ref= @
MessageResponseOBJA S
MsgResT Z
)Z [
;[ \
List 
< 
sis_usuario 
> 
BuscaAuditorNom )
() *
String* 0
	strNombre1 :
,: ;
ref< ?
MessageResponseOBJ@ R
MsgResS Y
)Y Z
;Z [
void 
GestionUsuarios 
( 
sis_usuario (

ObjUsuario) 3
,3 4
ref5 8
MessageResponseOBJ9 K
MsgResL R
)R S
;S T
void 
CrearUsuairo 
( 
sis_usuario %

ObjUsuario& 0
,0 1
ref2 5
MessageResponseOBJ6 H
MsgResI O
)O P
;P Q
DateTime 
ManagmentHora 
( 
)  
;  !
List%% 
<%% 
vw_datos_censo%% 
>%% 
CensoDocumento%% +
(%%+ ,
String%%, 2
	Documento%%3 <
,%%< =
ref%%> A
MessageResponseOBJ%%B T
MsgRes%%U [
)%%[ \
;%%\ ]
List'' 
<'' 
vw_datos_censo'' 
>'' 
CensoFacturas'' *
(''* +
ref''+ .
MessageResponseOBJ''/ A
MsgRes''B H
)''H I
;''I J
Int32)) 
InsertarCenso)) 
()) 

ecop_censo)) &
OBJ))' *
,))* +
ref)), /
MessageResponseOBJ))0 B
MsgRes))C I
)))I J
;))J K
List++ 
<++ 
vw_datos_censo++ 
>++ 
CensoId++ $
(++$ %
Int32++% *
Id+++ -
,++- .
ref++/ 2
MessageResponseOBJ++3 E
MsgRes++F L
)++L M
;++M N
List-- 
<-- 

ecop_censo-- 
>-- 

GetCensoId-- #
(--# $
Int32--$ )
Id--* ,
,--, -
ref--. 1
MessageResponseOBJ--2 D
MsgRes--E K
)--K L
;--L M
void// 
ActualizarCenso// 
(// 

ecop_censo// '
ActualizaSiniestro//( :
,//: ;
ref//< ?
MessageResponseOBJ//@ R
MsgRes//S Y
)//Y Z
;//Z [
void11 !
ActualizarCensoEgreso11 "
(11" #

ecop_censo11# -
ActualizaSiniestro11. @
,11@ A
ref11B E
MessageResponseOBJ11F X
MsgRes11Y _
)11_ `
;11` a
void33 #
ActualizarCensoEgresoOK33 $
(33$ %

ecop_censo33% /
ActualizaSiniestro330 B
,33B C
ref33D G
MessageResponseOBJ33H Z
MsgRes33[ a
)33a b
;33b c
void55 )
ActualizaEgresoConcurrenciaOk55 *
(55* +
ecop_concurrencia55+ <
ObjConcurrencia55= L
,55L M
ref55N Q
MessageResponseOBJ55R d
MsgRes55e k
)55k l
;55l m
List77 
<77 
base_beneficiarios77 
>77  "
BeneficiariosDocumento77! 7
(777 8
Decimal778 ?
	Documento77@ I
,77I J
ref77K N
MessageResponseOBJ77O a
MsgRes77b h
)77h i
;77i j
List99 
<99 .
"vw_concurrencia_evolucion_Contrato99 /
>99/ 0*
ConsultaIdConcurreniaEvolucion991 O
(99O P.
"vw_concurrencia_evolucion_Contrato99P r
ObjAfiliado99s ~
,99~ 
ref
99€ ƒ 
MessageResponseOBJ
99„ –
MsgRes
99— 
)
99 
;
99 Ÿ
List;; 
<;; 
vw_consulta_censo;; 
>;; 
ConsultaCenso;;  -
(;;- .
ref;;. 1
MessageResponseOBJ;;2 D
MsgRes;;E K
);;K L
;;;L M
void== 
CensoEgreso== 
(== 

ecop_censo== #
ActualizaSiniestro==$ 6
,==6 7
ref==8 ;
MessageResponseOBJ==< N
MsgRes==O U
)==U V
;==V W
ListDD 
<DD 
Ref_tipo_documentalDD  
>DD  !
GetTipoDocumnetalDD" 3
(DD3 4
)DD4 5
;DD5 6
ListFF 
<FF 
vw_ips_ciudadFF 
>FF 
GetIPSFF "
(FF" #
)FF# $
;FF$ %
ListHH 
<HH 
Ref_ips_cuentasHH 
>HH 
GetPrstadorCuentasHH 0
(HH0 1
)HH1 2
;HH2 3
ListJJ 
<JJ 
Ref_ips_cuentasJJ 
>JJ 
GetPrstadorJJ )
(JJ) *
)JJ* +
;JJ+ ,
ListLL 
<LL 
vw_ciudad_auditorLL 
>LL 
GetCiudad_auditorLL  1
(LL1 2
)LL2 3
;LL3 4
ListNN 
<NN 

vw_auditorNN 
>NN 
Get_auditorNN $
(NN$ %
)NN% &
;NN& '
ListPP 
<PP 
Ref_origen_eventoPP 
>PP 
GetOrigenEventoPP  /
(PP/ 0
)PP0 1
;PP1 2
ListRR 
<RR 
Ref_regionalRR 
>RR 
GetRefRegionRR '
(RR' (
)RR( )
;RR) *
ListTT 
<TT 
Ref_tipo_habitacionTT  
>TT  !
GetTipoHabitacionTT" 3
(TT3 4
)TT4 5
;TT5 6
ListVV 
<VV 
Ref_tipo_ingresoVV 
>VV 
GetTipoIngresoVV -
(VV- .
)VV. /
;VV/ 0
ListXX 
<XX $
Ref_condicion_alta_censoXX %
>XX% &
GetCondicionAltaXX' 7
(XX7 8
)XX8 9
;XX9 :
ListZZ 
<ZZ 
	Ref_cie10ZZ 
>ZZ 
GetCie10ZZ  
(ZZ  !
)ZZ! "
;ZZ" #
List\\ 
<\\ 
Ref_cups\\ 
>\\ 
GetCups\\ 
(\\ 
)\\  
;\\  !
List^^ 
<^^ 
Ref_cuentas_glosa^^ 
>^^ 
GetCuentaGlosa^^  .
(^^. /
)^^/ 0
;^^0 1
List`` 
<`` !
Ref_responsable_glosa`` "
>``" #
GetResGlosa``$ /
(``/ 0
)``0 1
;``1 2
Listbb 
<bb $
Ref_condicion_del_egresobb %
>bb% &
GetCondicionEgresobb' 9
(bb9 :
)bb: ;
;bb; <
Listdd 
<dd !
Ref_servicio_tratantedd "
>dd" #
GetServiciotratantedd$ 7
(dd7 8
)dd8 9
;dd9 :
Listff 
<ff 
Ref_salud_publicaff 
>ff 
GetSaludPublicaff  /
(ff/ 0
)ff0 1
;ff1 2
Listhh 
<hh -
!Ref_lesiones_severas_y_alto_costohh .
>hh. /
GetAltoCostohh0 <
(hh< =
)hh= >
;hh> ?
Listjj 
<jj 
vw_tablero_censojj 
>jj 
GetTableroCensojj .
(jj. /
)jj/ 0
;jj0 1
Listll 
<ll #
vw_tablero_concurrenciall $
>ll$ %"
GetTableroConcurrenciall& <
(ll< =
)ll= >
;ll> ?
Listnn 
<nn 
Ref_ciudadesnn 
>nn 
GetCiudadesnn &
(nn& '
)nn' (
;nn( )
Listpp 
<pp 
vw_cie10pp 
>pp 
GetCie10Unidopp $
(pp$ %
)pp% &
;pp& '
Listrr 
<rr 
Ref_causal_egresorr 
>rr 
GetCausaEgresorr  .
(rr. /
)rr/ 0
;rr0 1
Listtt 
<tt 
vw_consulta_alertastt  
>tt  !
GetConsultaAlertastt" 4
(tt4 5
)tt5 6
;tt6 7
Listvv 
<vv 
Total_ciudadesvv 
>vv 
GetTotalCiudadesvv -
(vv- .
)vv. /
;vv/ 0
Listxx 
<xx %
Ref_motivo_devolucion_facxx &
>xx& '"
GetMotivoDevolucionFacxx( >
(xx> ?
)xx? @
;xx@ A
Int32zz &
InsertarDevolucionFacturaszz (
(zz( )
factura_devolucionzz) ;
OBJzz< ?
,zz? @
refzzA D
MessageResponseOBJzzE W
MsgReszzX ^
)zz^ _
;zz_ `
Int32|| -
!InsertarDevolucionFacturasDetalle|| /
(||/ 0&
factura_devolucion_detalle||0 J
OBJ||K N
,||N O
ref||P S
MessageResponseOBJ||T f
MsgRes||g m
)||m n
;||n o
List~~ 
<~~ !
vw_sis_auditor_ciudad~~ "
>~~" #
GetCiudadesAuditor~~$ 6
(~~6 7
)~~7 8
;~~8 9
List
€€ 
<
€€  
Ref_hallazgos_RIPS
€€ 
>
€€  
GetRefHallazgos
€€! 0
(
€€0 1
)
€€1 2
;
€€2 3
List
‚‚ 
<
‚‚ (
Ref_categorias_eventos_adv
‚‚ '
>
‚‚' (!
GetRefCategoriaEvad
‚‚) <
(
‚‚< =
)
‚‚= >
;
‚‚> ?
List
„„ 
<
„„ "
sis_auditor_regional
„„ !
>
„„! " 
GetRegionalAuditor
„„# 5
(
„„5 6
)
„„6 7
;
„„7 8
List
†† 
<
†† "
Ref_motivo_reingreso
†† !
>
††! " 
GetRefMotivoRiesgo
††# 5
(
††5 6
)
††6 7
;
††7 8
List
ˆˆ 
<
ˆˆ 3
%Ref_categorias_situaciones_de_calidad
ˆˆ 2
>
ˆˆ2 3&
GetRefCategoriaSituacion
ˆˆ4 L
(
ˆˆL M
)
ˆˆM N
;
ˆˆN O
List
ŠŠ 
<
ŠŠ 
vw_cie10_alertas
ŠŠ 
>
ŠŠ  
GetRefcie10Alertas
ŠŠ 1
(
ŠŠ1 2
)
ŠŠ2 3
;
ŠŠ3 4
List
ŒŒ 
<
ŒŒ (
Ref_enfermedades_Huerfanas
ŒŒ '
>
ŒŒ' (
GetRefHuerfanas
ŒŒ) 8
(
ŒŒ8 9
)
ŒŒ9 :
;
ŒŒ: ;
List
 
<
 
Ref_tipo_ahorro
 
>
 
GetRefTipoAhorro
 .
(
. /
)
/ 0
;
0 1
void
”” #
ActualizaConcurrencia
”” "
(
””" #
ecop_concurrencia
””# 4
ObjConcurrencia
””5 D
,
””D E
String
””F L
User
””M Q
,
””Q R
String
””S Y
	IPAddress
””Z c
,
””c d
ref
””e h 
MessageResponseOBJ
””i {
MsgRes””| ‚
)””‚ ƒ
;””ƒ „
void
–– )
ActualizaEgresoConcurrencia
–– (
(
––( )
ecop_concurrencia
––) :
ObjConcurrencia
––; J
,
––J K
String
––L R
User
––S W
,
––W X
String
––Y _
	IPAddress
––` i
,
––i j
ref
––k n!
MessageResponseOBJ––o 
MsgRes––‚ ˆ
)––ˆ ‰
;––‰ Š
List
˜˜ 
<
˜˜ 
ecop_concurrencia
˜˜ 
>
˜˜ 0
"ConsultaCriterioIngresoActualizado
˜˜  B
(
˜˜B C
Int32
˜˜C H
IdConcu
˜˜I P
,
˜˜P Q
ref
˜˜R U 
MessageResponseOBJ
˜˜V h
MsgRes
˜˜i o
)
˜˜o p
;
˜˜p q
List
šš 
<
šš 4
&ecop_concurrencia_encuesta_satisfacion
šš 3
>
šš3 4%
ConsultaEncuestaCargada
šš5 L
(
ššL M
Int32
ššM R
IdConcu
ššS Z
,
ššZ [
ref
šš\ _ 
MessageResponseOBJ
šš` r
MsgRes
ššs y
)
ššy z
;
ššz {
void
œœ 
InsertaEgreso
œœ 
(
œœ +
egreso_auditoria_Hospitalaria
œœ 8
Egreso
œœ9 ?
,
œœ? @
String
œœA G
UserName
œœH P
,
œœP Q
String
œœR X
	IPAddress
œœY b
,
œœb c
ref
œœd g 
MessageResponseOBJ
œœh z
MsgResœœ{ 
)œœ ‚
;œœ‚ ƒ
List
 
<
 
vw_ciudad_ips
 
>
 )
ConsultaIdConcurreniaciudad
 7
(
7 8
vw_ciudad_ips
8 E
ObjAfiliado
F Q
,
Q R
ref
S V 
MessageResponseOBJ
W i
MsgRes
j p
)
p q
;
q r
List
   
<
   &
vw_consulta_concurrencia
   %
>
  % &"
ConsultaConcurrencia
  ' ;
(
  ; <
ref
  < ? 
MessageResponseOBJ
  @ R
MsgRes
  S Y
)
  Y Z
;
  Z [
List
¢¢ 
<
¢¢  
vw_consulta_egreso
¢¢ 
>
¢¢  
ConsultaEgreso
¢¢! /
(
¢¢/ 0
ref
¢¢0 3 
MessageResponseOBJ
¢¢4 F
MsgRes
¢¢G M
)
¢¢M N
;
¢¢N O
List
¤¤ 
<
¤¤ *
vw_consulta_eventos_adversos
¤¤ )
>
¤¤) *
ConsultaEventosAd
¤¤+ <
(
¤¤< =
ref
¤¤= @ 
MessageResponseOBJ
¤¤A S
MsgRes
¤¤T Z
)
¤¤Z [
;
¤¤[ \
List
¦¦ 
<
¦¦ +
vw_consulta_situacion_calidad
¦¦ *
>
¦¦* +"
ConsultaSituacionCal
¦¦, @
(
¦¦@ A
ref
¦¦A D 
MessageResponseOBJ
¦¦E W
MsgRes
¦¦X ^
)
¦¦^ _
;
¦¦_ `
List
¨¨ 
<
¨¨ 
vw_gestantes
¨¨ 
>
¨¨ 
ConsultaGestantes
¨¨ ,
(
¨¨, -
ref
¨¨- 0 
MessageResponseOBJ
¨¨1 C
MsgRes
¨¨D J
)
¨¨J K
;
¨¨K L
List
ªª 
<
ªª 
vw_gestantes_sin
ªª 
>
ªª "
ConsultaGestantesSin
ªª 3
(
ªª3 4
ref
ªª4 7 
MessageResponseOBJ
ªª8 J
MsgRes
ªªK Q
)
ªªQ R
;
ªªR S
List
«« 
<
«« 
vw_Mortalidad
«« 
>
««  
ConsultaMortalidad
«« .
(
««. /
ref
««/ 2 
MessageResponseOBJ
««3 E
MsgRes
««F L
)
««L M
;
««M N
List
­­ 
<
­­ 
vw_Mortalidad_sin
­­ 
>
­­ #
ConsultaMortalidadSin
­­  5
(
­­5 6
ref
­­6 9 
MessageResponseOBJ
­­: L
MsgRes
­­M S
)
­­S T
;
­­T U
List
¯¯ 
<
¯¯ &
vw_tipo_habitacion_censo
¯¯ %
>
¯¯% &$
ConsultaTipoHabitacion
¯¯' =
(
¯¯= >&
vw_tipo_habitacion_censo
¯¯> V
ObjAfiliado
¯¯W b
,
¯¯b c
ref
¯¯d g 
MessageResponseOBJ
¯¯h z
MsgRes¯¯{ 
)¯¯ ‚
;¯¯‚ ƒ
void
±± *
InsertarEncuestaConcurrencia
±± )
(
±±) *(
ecop_concurrencia_encuesta
±±* D
Encuesta
±±E M
,
±±M N
ref
±±O R 
MessageResponseOBJ
±±S e
MsgRes
±±f l
)
±±l m
;
±±m n
void
¶¶ *
InsertaConcurrenciaEvolucion
¶¶ )
(
¶¶) *)
ecop_concurrencia_evolucion
¶¶* E
	Evolucion
¶¶F O
,
¶¶O P
String
¶¶Q W
UserName
¶¶X `
,
¶¶` a
String
¶¶b h
	IPAddress
¶¶i r
,
¶¶r s
ref
¶¶t w!
MessageResponseOBJ¶¶x Š
MsgRes¶¶‹ ‘
)¶¶‘ ’
;¶¶’ “
List
¸¸ 
<
¸¸ )
ecop_concurrencia_evolucion
¸¸ (
>
¸¸( )!
ConsultaEvoluciones
¸¸* =
(
¸¸= >)
ecop_concurrencia_evolucion
¸¸> Y
ObjEvolu
¸¸Z b
,
¸¸b c
ref
¸¸d g 
MessageResponseOBJ
¸¸h z
MsgRes¸¸{ 
)¸¸ ‚
;¸¸‚ ƒ
void
ºº +
EliminarConcurrenciaEvolucion
ºº *
(
ºº* +)
ecop_concurrencia_evolucion
ºº+ F
ObjEvolucion
ººG S
,
ººS T
String
ººU [
UserName
ºº\ d
,
ººd e
String
ººf l
	IPAddress
ººm v
,
ººv w
ref
ººx {!
MessageResponseOBJºº| 
MsgResºº •
)ºº• –
;ºº– —
List
¼¼ 
<
¼¼ 2
$ecop_concurrencia_evolucion_diag_def
¼¼ 1
>
¼¼1 2+
ConsultaDiagnosticoDefinitivo
¼¼3 P
(
¼¼P Q2
$ecop_concurrencia_evolucion_diag_def
¼¼Q u

Objdiagdef¼¼v €
,¼¼€ 
ref¼¼‚ …"
MessageResponseOBJ¼¼† ˜
MsgRes¼¼™ Ÿ
)¼¼Ÿ  
;¼¼  ¡
void
¾¾ *
InsertaDiagnosticoDefinitivo
¾¾ )
(
¾¾) *2
$ecop_concurrencia_evolucion_diag_def
¾¾* N4
&Concurrencia_Diagnostico_Definitivo_id
¾¾O u
,
¾¾u v
String
¾¾w }
UserName¾¾~ †
,¾¾† ‡
String¾¾ˆ 
	IPAddress¾¾ ˜
,¾¾˜ ™
ref¾¾š "
MessageResponseOBJ¾¾ °
MsgRes¾¾± ·
)¾¾· ¸
;¾¾¸ ¹
void
ÀÀ 
InsertaGlosa
ÀÀ 
(
ÀÀ %
ecop_concurrencia_glosa
ÀÀ 1
ObjGlosa
ÀÀ2 :
,
ÀÀ: ;
String
ÀÀ< B
UserName
ÀÀC K
,
ÀÀK L
String
ÀÀM S
	IPAddress
ÀÀT ]
,
ÀÀ] ^
ref
ÀÀ_ b 
MessageResponseOBJ
ÀÀc u
MsgRes
ÀÀv |
)
ÀÀ| }
;
ÀÀ} ~
List
ÂÂ 
<
ÂÂ %
ecop_concurrencia_glosa
ÂÂ $
>
ÂÂ$ %
ConsultaGlosa
ÂÂ& 3
(
ÂÂ3 4%
ecop_concurrencia_glosa
ÂÂ4 K
ObjGlosa
ÂÂL T
,
ÂÂT U
ref
ÂÂV Y 
MessageResponseOBJ
ÂÂZ l
MsgRes
ÂÂm s
)
ÂÂs t
;
ÂÂt u
List
ÅÅ 
<
ÅÅ "
Ref_eventos_adversos
ÅÅ !
>
ÅÅ! " 
GetEventosAdversos
ÅÅ# 5
(
ÅÅ5 6
)
ÅÅ6 7
;
ÅÅ7 8
List
ÇÇ 
<
ÇÇ 
Ref_grado_lesion
ÇÇ 
>
ÇÇ 
GetGradoLesion
ÇÇ -
(
ÇÇ- .
)
ÇÇ. /
;
ÇÇ/ 0
List
ÉÉ 
<
ÉÉ )
Ref_factores_contribuyentes
ÉÉ (
>
ÉÉ( )'
GetFactoresContribuyentes
ÉÉ* C
(
ÉÉC D
)
ÉÉD E
;
ÉÉE F
List
ËË 
<
ËË $
Ref_barreras_seguridad
ËË #
>
ËË# $$
GetBarrerasDeSeguridad
ËË% ;
(
ËË; <
)
ËË< =
;
ËË= >
List
ÍÍ 
<
ÍÍ $
Ref_acciones_inseguras
ÍÍ #
>
ÍÍ# $"
GetAccionesInseguras
ÍÍ% 9
(
ÍÍ9 :
)
ÍÍ: ;
;
ÍÍ; <
List
ÏÏ 
<
ÏÏ  
Ref_plan_de_manejo
ÏÏ 
>
ÏÏ  
GetPlanDeManejo
ÏÏ! 0
(
ÏÏ0 1
)
ÏÏ1 2
;
ÏÏ2 3
void
ÑÑ "
InsertaEventoAdverso
ÑÑ !
(
ÑÑ! "0
"ecop_concurrencia_eventos_adversos
ÑÑ" D
ObjEventoAdv
ÑÑE Q
,
ÑÑQ R
String
ÑÑS Y
UserName
ÑÑZ b
,
ÑÑb c
String
ÑÑd j
	IPAddress
ÑÑk t
,
ÑÑt u
ref
ÑÑv y!
MessageResponseOBJÑÑz Œ
MsgResÑÑ “
)ÑÑ“ ”
;ÑÑ” •
List
ÓÓ 
<
ÓÓ 0
"ecop_concurrencia_eventos_adversos
ÓÓ /
>
ÓÓ/ 0#
ConsultaEventoAdverso
ÓÓ1 F
(
ÓÓF G0
"ecop_concurrencia_eventos_adversos
ÓÓG i
ObjEventoAdverso
ÓÓj z
,
ÓÓz {
ref
ÓÓ| "
MessageResponseOBJÓÓ€ ’
MsgResÓÓ“ ™
)ÓÓ™ š
;ÓÓš ›
List
ÕÕ 
<
ÕÕ (
Ref_situaciones_de_calidad
ÕÕ '
>
ÕÕ' (%
GetSituacionesDeCalidad
ÕÕ) @
(
ÕÕ@ A
)
ÕÕA B
;
ÕÕB C
void
×× '
InsertaSituacionesCalidad
×× &
(
××& '6
(ecop_concurrencia_situaciones_de_calidad
××' O
ObjSituacionCalid
××P a
,
××a b
String
××c i
UserName
××j r
,
××r s
String
××t z
	IPAddress××{ „
,××„ …
ref××† ‰"
MessageResponseOBJ××Š œ
MsgRes×× £
)××£ ¤
;××¤ ¥
List
ÙÙ 
<
ÙÙ 6
(ecop_concurrencia_situaciones_de_calidad
ÙÙ 5
>
ÙÙ5 6(
ConsultaSituacionesCalidad
ÙÙ7 Q
(
ÙÙQ R6
(ecop_concurrencia_situaciones_de_calidad
ÙÙR z
ObjSituCaliÙÙ{ †
,ÙÙ† ‡
refÙÙˆ ‹"
MessageResponseOBJÙÙŒ 
MsgResÙÙŸ ¥
)ÙÙ¥ ¦
;ÙÙ¦ §
List
ÛÛ 
<
ÛÛ 2
$Ref_motivo_cancelacion_procedimiento
ÛÛ 1
>
ÛÛ1 2"
GetMotivoCancelacion
ÛÛ3 G
(
ÛÛG H
)
ÛÛH I
;
ÛÛI J
void
İİ 4
&InsertaProcedimientoQuirugicoCancelado
İİ 3
(
İİ3 4E
7ecop_concurrencia_procedimientos_quirurgicos_cancelados
İİ4 k%
ProcedimientoQuirCanceİİl ‚
,İİ‚ ƒ
Stringİİ„ Š
UserNameİİ‹ “
,İİ“ ”
Stringİİ• ›
	IPAddressİİœ ¥
,İİ¥ ¦
refİİ§ ª"
MessageResponseOBJİİ« ½
MsgResİİ¾ Ä
)İİÄ Å
;İİÅ Æ
List
ßß 
<
ßß E
7ecop_concurrencia_procedimientos_quirurgicos_cancelados
ßß D
>
ßßD E*
ConsultaProcQuirurgicosCance
ßßF b
(
ßßb cF
7ecop_concurrencia_procedimientos_quirurgicos_canceladosßßc š
ObjProcQuirßß› ¦
,ßß¦ §
refßß¨ «"
MessageResponseOBJßß¬ ¾
MsgResßß¿ Å
)ßßÅ Æ
;ßßÆ Ç
void
áá 
InsertarNatalidad
áá 
(
áá (
natalidad_sin_concurrencia
áá 9
	Natalidad
áá: C
,
ááC D
ref
ááE H 
MessageResponseOBJ
ááI [
MsgRes
áá\ b
)
ááb c
;
áác d
void
ãã  
InsertarMortalidad
ãã 
(
ãã  )
mortalidad_sin_concurrencia
ãã  ;

Mortalidad
ãã< F
,
ããF G
ref
ããH K 
MessageResponseOBJ
ããL ^
MsgRes
ãã_ e
)
ããe f
;
ããf g
List
åå 
<
åå )
vw_tablero_eventos_adversos
åå (
>
åå( )#
ReportesEventoAdverso
åå* ?
(
åå? @
)
åå@ A
;
ååA B
void
çç )
InsertarAlertasConcurrencia
çç (
(
çç( ),
alertas_generadas_concurrencia
çç) G
Alertas
ççH O
,
ççO P
ref
ççQ T 
MessageResponseOBJ
ççU g
MsgRes
ççh n
)
ççn o
;
çço p
void
éé (
InsertarConcurrenciaAhorro
éé '
(
éé' (&
ecop_concurrencia_ahorro
éé( @
Ahorro
ééA G
,
ééG H
ref
ééI L 
MessageResponseOBJ
ééM _
MsgRes
éé` f
)
ééf g
;
éég h
List
ëë 
<
ëë &
ecop_concurrencia_ahorro
ëë %
>
ëë% &
ConsultaAhorro
ëë' 5
(
ëë5 6&
ecop_concurrencia_ahorro
ëë6 N
	ObjAhorro
ëëO X
,
ëëX Y
ref
ëëZ ] 
MessageResponseOBJ
ëë^ p
MsgRes
ëëq w
)
ëëw x
;
ëëx y
List
íí 
<
íí 
Ref_causal_glosa
íí 
>
íí !
ConsultaCausalGlosa
íí 2
(
íí2 3
int
íí3 6!
id_respnsable_glosa
íí7 J
,
ííJ K
ref
ííL O 
MessageResponseOBJ
ííP b
MsgRes
ííc i
)
ííi j
;
ííj k
List
òò 
<
òò +
ManagmentAlertasCalidadResult
òò *
>
òò* +
CuentaFechaCargue
òò, =
(
òò= >
Int32
òò> C
Opc
òòD G
,
òòG H
DateTime
òòI Q
FechaInicial
òòR ^
,
òò^ _
DateTime
òò` h
FechaFin
òòi q
,
òòq r
String
òòs y
strProveedoròòz †
,òò† ‡
Stringòòˆ 
	strEstadoòò ˜
,òò˜ ™
refòòš "
MessageResponseOBJòò °
MsgResòò± ·
)òò· ¸
;òò¸ ¹
List
ôô 
<
ôô 0
"ManagmentReportDevolucionFacResult
ôô /
>
ôô/ 0*
ConsultaReporteDevolucionFac
ôô1 M
(
ôôM N
Int32
ôôN S#
id_devolucion_factura
ôôT i
)
ôôi j
;
ôôj k
List
öö 
<
öö +
vw_Devoluciones_sin_gestionar
öö *
>
öö* +$
DevolucionesSinGestion
öö, B
(
ööB C
)
ööC D
;
ööD E
Int32
øø +
InsertarDevolucionGestionadas
øø +
(
øø+ ,,
factura_devolucion_gestionadas
øø, J
OBJ
øøK N
,
øøN O
ref
øøP S 
MessageResponseOBJ
øøT f
MsgRes
øøg m
)
øøm n
;
øøn o
Int32
úú %
InsertarFacturaSinCenso
úú %
(
úú% &
factura_sin_censo
úú& 7
OBJ
úú8 ;
,
úú; <
ref
úú= @ 
MessageResponseOBJ
úúA S
MsgRes
úúT Z
)
úúZ [
;
úú[ \
List
üü 
<
üü 
vw_hallazgos_RIPS
üü 
>
üü %
HallazgosRipsSinGestion
üü  7
(
üü7 8
)
üü8 9
;
üü9 :
List
şş 
<
şş %
vw_facturas_sin_auditar
şş $
>
şş$ % 
FacturasporAuditar
şş& 8
(
şş8 9
)
şş9 :
;
şş: ;
List
€€ 
<
€€ 
vw_costo_evitado
€€ 
>
€€ 
CostoEvitado
€€ +
(
€€+ ,
Int32
€€, 1
Id
€€2 4
,
€€4 5
ref
€€6 9 
MessageResponseOBJ
€€: L
MsgRes
€€M S
)
€€S T
;
€€T U
List
‚‚ 
<
‚‚ &
vw_facturas_diagnosticos
‚‚ %
>
‚‚% &!
DiagnosticosCuentas
‚‚' :
(
‚‚: ;
Int32
‚‚; @
Id
‚‚A C
,
‚‚C D
ref
‚‚E H 
MessageResponseOBJ
‚‚I [
MsgRes
‚‚\ b
)
‚‚b c
;
‚‚c d
List
„„ 
<
„„ )
vw_ECOPETROL_DEVOLUCION_FAC
„„ (
>
„„( )
VwDevoluciones
„„* 8
(
„„8 9
)
„„9 :
;
„„: ;
List
†† 
<
†† )
vw_ECOPETROL_HALLAZGOS_RIPS
†† (
>
††( )
VwHallazgosRIPS
††* 9
(
††9 :
)
††: ;
;
††; <
List
ˆˆ 
<
ˆˆ *
ECOPETROL_RECEPCION_FACTURAS
ˆˆ )
>
ˆˆ) *!
VwRecepcionFacturas
ˆˆ+ >
(
ˆˆ> ?
)
ˆˆ? @
;
ˆˆ@ A
Int32
 
InsertarHallazgos
 
(
  
hallazgo_RIPS
  -
OBJ
. 1
,
1 2
ref
3 6 
MessageResponseOBJ
7 I
MsgRes
J P
)
P Q
;
Q R
Int32
‘‘ &
InsertarHallazgosDetalle
‘‘ &
(
‘‘& '#
hallazgo_RIPS_detalle
‘‘' <
OBJ
‘‘= @
,
‘‘@ A
ref
‘‘B E 
MessageResponseOBJ
‘‘F X
MsgRes
‘‘Y _
)
‘‘_ `
;
‘‘` a
List
““ 
<
““ /
!ManagmentReportHallazgosRipResult
““ .
>
““. /*
ConsultaReporteHallazgosRips
““0 L
(
““L M
Int32
““M R
id_hallazgo_RIPS
““S c
)
““c d
;
““d e
void
•• $
ActualizaHallazgosRips
•• #
(
••# $
hallazgo_RIPS
••$ 1
Objrips
••2 9
,
••9 :
ref
••; > 
MessageResponseOBJ
••? Q
MsgRes
••R X
)
••X Y
;
••Y Z
List
—— 
<
—— 
factura_sin_censo
—— 
>
—— %
ConsultaFacturasSinAudi
——  7
(
——7 8
Int32
——8 ="
id_factura_sin_censo
——> R
)
——R S
;
——S T
Int32
™™ "
InsertarCostoEvitado
™™ "
(
™™" #+
factura_sin_censo_cos_evitado
™™# @
Obj
™™A D
,
™™D E
ref
™™F I 
MessageResponseOBJ
™™J \
MsgRes
™™] c
)
™™c d
;
™™d e
Int32
›› (
InsertarDiagnosticoCuentas
›› (
(
››( ),
factura_sin_censo_diagnosticos
››) G
Obj
››H K
,
››K L
ref
››M P 
MessageResponseOBJ
››Q c
MsgRes
››d j
)
››j k
;
››k l
void
 &
ActualizaFacturaAuditada
 %
(
% &
factura_sin_censo
& 7
ObjAudi
8 ?
,
? @
ref
A D 
MessageResponseOBJ
E W
MsgRes
X ^
)
^ _
;
_ `
List
ŸŸ 
<
ŸŸ  
factura_devolucion
ŸŸ 
>
ŸŸ  )
ConsultaDevolucionesFactura
ŸŸ! <
(
ŸŸ< =
String
ŸŸ= C
Numero_factura
ŸŸD R
)
ŸŸR S
;
ŸŸS T
List
¡¡ 
<
¡¡ 
factura_sin_censo
¡¡ 
>
¡¡ #
ConsultaFacturaNumero
¡¡  5
(
¡¡5 6
String
¡¡6 <
Numero_factura
¡¡= K
)
¡¡K L
;
¡¡L M
List
££ 
<
££  
factura_devolucion
££ 
>
££  +
ConsultaDevolucionesFacturaId
££! >
(
££> ?
Int32
££? D
Id_devolucion
££E R
)
££R S
;
££S T
List
¥¥ 
<
¥¥ 
hallazgo_RIPS
¥¥ 
>
¥¥ !
ConsultaHallazgosId
¥¥ /
(
¥¥/ 0
Int32
¥¥0 5
Id_rips
¥¥6 =
)
¥¥= >
;
¥¥> ?
Int32
°° 
InsertarRips
°° 
(
°° 
RIPS
°° 
Objrips
°°  '
,
°°' (
ref
°°* - 
MessageResponseOBJ
°°. @
MsgRes
°°A G
)
°°G H
;
°°H I
List
²² 
<
²² 
RIPS
²² 
>
²² 
ConsultaRips
²² 
(
²²  
Int32
²²  %
IdRips
²²& ,
,
²², -
ref
²². 1 
MessageResponseOBJ
²²2 D
MsgRes
²²E K
)
²²K L
;
²²L M
bool
´´ 
ActualizaRips
´´ 
(
´´ 
RIPS
´´ 
ObjRips
´´  '
,
´´' (
ref
´´) , 
MessageResponseOBJ
´´- ?
MsgRes
´´@ F
)
´´F G
;
´´G H
Int32
»» 
InsertarRipsAC
»» 
(
»» 
RIPS_AC
»» $
	ObjripsAc
»»% .
,
»». /
ref
»»0 3 
MessageResponseOBJ
»»4 F
MsgRes
»»G M
)
»»M N
;
»»N O
List
¼¼ 
<
¼¼ 
RIPS_AC
¼¼ 
>
¼¼ 
ConsultaRipsAC
¼¼ $
(
¼¼$ %
Int32
¼¼% *
IdRips
¼¼+ 1
,
¼¼1 2
ref
¼¼3 6 
MessageResponseOBJ
¼¼7 I
MsgRes
¼¼J P
)
¼¼P Q
;
¼¼Q R
Int32
ÄÄ 
InsertarRipsAD
ÄÄ 
(
ÄÄ 
List
ÄÄ !
<
ÄÄ! "
RIPS_AD
ÄÄ" )
>
ÄÄ) *
	ObjripsAD
ÄÄ+ 4
,
ÄÄ4 5
ref
ÄÄ6 9 
MessageResponseOBJ
ÄÄ: L
MsgRes
ÄÄM S
)
ÄÄS T
;
ÄÄT U
Int32
ÌÌ 
InsertarRipsAF
ÌÌ 
(
ÌÌ 
RIPS_AF
ÌÌ $
	ObjripsAF
ÌÌ% .
,
ÌÌ. /
ref
ÌÌ0 3 
MessageResponseOBJ
ÌÌ4 F
MsgRes
ÌÌG M
)
ÌÌM N
;
ÌÌN O
Int32
ÔÔ 
InsertarRipsAH
ÔÔ 
(
ÔÔ 
RIPS_AH
ÔÔ $
	ObjripsAH
ÔÔ% .
,
ÔÔ. /
ref
ÔÔ0 3 
MessageResponseOBJ
ÔÔ4 F
MsgRes
ÔÔG M
)
ÔÔM N
;
ÔÔN O
Int32
ÜÜ 
InsertarRipsAM
ÜÜ 
(
ÜÜ 
RIPS_AM
ÜÜ $
	ObjripsAM
ÜÜ% .
,
ÜÜ. /
ref
ÜÜ0 3 
MessageResponseOBJ
ÜÜ4 F
MsgRes
ÜÜG M
)
ÜÜM N
;
ÜÜN O
Int32
ää 
InsertarRipsAN
ää 
(
ää 
RIPS_AN
ää $
	ObjripsAN
ää% .
,
ää. /
ref
ää0 3 
MessageResponseOBJ
ää4 F
MsgRes
ääG M
)
ääM N
;
ääN O
Int32
ææ 
InsertarRipsAP
ææ 
(
ææ 
RIPS_AP
ææ $
	ObjripsAP
ææ% .
,
ææ. /
ref
ææ0 3 
MessageResponseOBJ
ææ4 F
MsgRes
ææG M
)
ææM N
;
ææN O
Int32
èè 
InsertarRipsAT
èè 
(
èè 
RIPS_AT
èè $
	ObjripsAT
èè% .
,
èè. /
ref
èè0 3 
MessageResponseOBJ
èè4 F
MsgRes
èèG M
)
èèM N
;
èèN O
Int32
êê 
InsertarRipsAU
êê 
(
êê 
RIPS_AU
êê $
	ObjripsAU
êê% .
,
êê. /
ref
êê0 3 
MessageResponseOBJ
êê4 F
MsgRes
êêG M
)
êêM N
;
êêN O
Int32
ìì 
InsertarRipsCT
ìì 
(
ìì 
RIPS_CT
ìì $
	ObjripsCT
ìì% .
,
ìì. /
ref
ìì0 3 
MessageResponseOBJ
ìì4 F
MsgRes
ììG M
)
ììM N
;
ììN O
Int32
îî 
InsertarRipsUS
îî 
(
îî 
RIPS_US
îî $
	ObjripsUS
îî% .
,
îî. /
ref
îî0 3 
MessageResponseOBJ
îî4 F
MsgRes
îîG M
)
îîM N
;
îîN O
List
ğğ 
<
ğğ "
Ref_RIPS_Prestadores
ğğ !
>
ğğ! "!
ConsultaPrestadores
ğğ# 6
(
ğğ6 7
string
ğğ7 =
codhabilitacion
ğğ> M
,
ğğN O
ref
ğğP S 
MessageResponseOBJ
ğğT f
MsgRes
ğğg m
)
ğğm n
;
ğğn o
}
ôô 
}õõ ²è4
.D:\proyectos\asalud ecopetrol\Facede\Facade.cs
	namespace 	
Facede
 
{ 
public 

class 
Facade 
{ 
private 
ConsultasDac 
_DACConsulta )
;) *
public 
ConsultasDac 
DACConsulta '
{ 	
get 
{ 
if 
( 
_DACConsulta  
!=! #
null$ (
)( )
{ 
return 
_DACConsulta '
;' (
} 
else 
{ 
return 
_DACConsulta '
=( )
new* -
ConsultasDac. :
(: ;
); <
;< =
} 
}!! 
set"" 
{"" 
_DACConsulta"" 
=""  
value""! &
;""& '
}""( )
}## 	
private%% 
ActualizarDac%% 
_DACActualiza%% +
;%%+ ,
public&& 
ActualizarDac&& 
DACActualiza&& )
{'' 	
get(( 
{)) 
if** 
(** 
_DACActualiza** !
!=**" $
null**% )
)**) *
{++ 
return,, 
_DACActualiza,, (
;,,( )
}-- 
else.. 
{// 
return00 
_DACActualiza00 (
=00) *
new00+ .
ActualizarDac00/ <
(00< =
)00= >
;00> ?
}11 
}33 
set44 
{44 
_DACActualiza44 
=44  !
value44" '
;44' (
}44) *
}55 	
private77 

ComonClass77 
_DACComonClass77 )
;77) *
public88 

ComonClass88 
DACComonClass88 '
{99 	
get:: 
{;; 
if<< 
(<< 
_DACComonClass<< "
!=<<# %
null<<& *
)<<* +
{== 
return>> 
_DACComonClass>> )
;>>) *
}?? 
else@@ 
{AA 
returnBB 
_DACComonClassBB )
=BB* +
newBB, /

ComonClassBB0 :
(BB: ;
)BB; <
;BB< =
}CC 
}EE 
setFF 
{FF 
_DACComonClassFF  
=FF! "
valueFF# (
;FF( )
}FF* +
}GG 	
privateII 

InsertaDacII 
_DACInsertaII &
;II& '
publicJJ 

InsertaDacJJ 

DACInsertaJJ $
{KK 	
getLL 
{MM 
ifNN 
(NN 
_DACInsertaNN 
!=NN  "
nullNN# '
)NN' (
{OO 
returnPP 
_DACInsertaPP &
;PP& '
}QQ 
elseRR 
{SS 
returnTT 
_DACInsertaTT &
=TT' (
newTT) ,

InsertaDacTT- 7
(TT7 8
)TT8 9
;TT9 :
}UU 
}WW 
setXX 
{XX 
_DACInsertaXX 
=XX 
valueXX  %
;XX% &
}XX' (
}YY 	
private[[ 

EliminaDac[[ 
_DACElimina[[ &
;[[& '
public\\ 

EliminaDac\\ 

DACElimina\\ $
{]] 	
get^^ 
{__ 
if`` 
(`` 
_DACElimina`` 
!=``  "
null``# '
)``' (
{aa 
returnbb 
_DACEliminabb &
;bb& '
}cc 
elsedd 
{ee 
returnff 
_DACEliminaff &
=ff' (
newff) ,

EliminaDacff- 7
(ff7 8
)ff8 9
;ff9 :
}gg 
}ii 
setjj 
{jj 
_DACEliminajj 
=jj 
valuejj  %
;jj% &
}jj' (
}kk 	
publicpp 
Listpp 
<pp 
sis_usuariopp 
>pp  
GetSisUsuariopp! .
(pp. /
)pp/ 0
{qq 	
returnrr 
DACComonClassrr  
.rr  !
GetSisUsuariorr! .
(rr. /
)rr/ 0
;rr0 1
}ss 	
publicuu 
Listuu 
<uu 
sis_usuariouu 
>uu  
GetSisUsuarioactivouu! 4
(uu4 5
)uu5 6
{vv 	
returnww 
DACComonClassww  
.ww  !
GetSisUsuarioactivoww! 4
(ww4 5
)ww5 6
;ww6 7
}xx 	
publiczz 
Listzz 
<zz 
sis_usuariozz 
>zz  
GetSisUsuarioMdzz! 0
(zz0 1
)zz1 2
{{{ 	
return|| 
DACComonClass||  
.||  !
GetSisUsuarioMd||! 0
(||0 1
)||1 2
;||2 3
}}} 	
public 
List 
< 
sis_usuario 
>  
GetSisUsuarioOdont! 3
(3 4
)4 5
{
€€ 	
return
 
DACComonClass
  
.
  ! 
GetSisUsuarioOdont
! 3
(
3 4
)
4 5
;
5 6
}
‚‚ 	
public
„„ 
void
„„ $
ActualizaCodigoIngreso
„„ *
(
„„* +
string
„„+ 1
usuario
„„2 9
,
„„9 :
string
„„; A
codigo
„„B H
,
„„H I
ref
„„J M 
MessageResponseOBJ
„„N `
MsgRes
„„a g
)
„„g h
{
…… 	
DACActualiza
†† 
.
†† $
ActualizaCodigoIngreso
†† /
(
††/ 0
usuario
††0 7
,
††7 8
codigo
††9 ?
,
††? @
ref
††A D
MsgRes
††E K
)
††K L
;
††L M
}
‡‡ 	
public
‰‰ 
List
‰‰ 
<
‰‰ !
Ref_tipo_documental
‰‰ '
>
‰‰' (
GetTipoDocumnetal
‰‰) :
(
‰‰: ;
)
‰‰; <
{
ŠŠ 	
return
‹‹ 
DACComonClass
‹‹  
.
‹‹  !
GetTipoDocumnetal
‹‹! 2
(
‹‹2 3
)
‹‹3 4
;
‹‹4 5
}
 	
public
 
List
 
<
 
vw_ips_ciudad
 !
>
! "
GetIPS
# )
(
) *
)
* +
{
 	
return
‘‘ 
DACComonClass
‘‘  
.
‘‘  !
GetIPS
‘‘! '
(
‘‘' (
)
‘‘( )
;
‘‘) *
}
’’ 	
public
”” 
List
”” 
<
”” 
Ref_ips
”” 
>
”” 
GetPrstador
”” (
(
””( )
)
””) *
{
•• 	
return
–– 
DACComonClass
––  
.
––  !
GetPrstador
––! ,
(
––, -
)
––- .
;
––. /
}
—— 	
public
™™ 
List
™™ 
<
™™ 5
'management_censo_tableroDetalladoResult
™™ ;
>
™™; <
GetCensoDetallado
™™= N
(
™™N O
DateTime
™™O W
?
™™W X
fechaInicio
™™Y d
,
™™d e
DateTime
™™f n
?
™™n o
fechaFin
™™p x
,
™™x y
string™™z €
	documento™™ Š
)™™Š ‹
{
šš 	
return
›› 
DACConsulta
›› 
.
›› 
GetCensoDetallado
›› 0
(
››0 1
fechaInicio
››1 <
,
››< =
fechaFin
››> F
,
››F G
	documento
››H Q
)
››Q R
;
››R S
}
œœ 	
public
 
List
 
<
 
Ref_ips_cuentas
 #
>
# $ 
GetPrstadorCuentas
% 7
(
7 8
)
8 9
{
ŸŸ 	
return
   
DACComonClass
    
.
    ! 
GetPrstadorCuentas
  ! 3
(
  3 4
)
  4 5
;
  5 6
}
¡¡ 	
public
¢¢ 
List
¢¢ 
<
¢¢ 
vw_ciudad_auditor
¢¢ %
>
¢¢% &
GetCiudad_auditor
¢¢' 8
(
¢¢8 9
)
¢¢9 :
{
££ 	
return
¤¤ 
DACComonClass
¤¤  
.
¤¤  !
GetCiudad_auditor
¤¤! 2
(
¤¤2 3
)
¤¤3 4
;
¤¤4 5
}
¥¥ 	
public
§§ 
List
§§ 
<
§§ 

vw_auditor
§§ 
>
§§ 
Get_auditor
§§  +
(
§§+ ,
)
§§, -
{
¨¨ 	
return
©© 
DACComonClass
©©  
.
©©  !
Get_auditor
©©! ,
(
©©, -
)
©©- .
;
©©. /
}
ªª 	
public
«« 
List
«« 
<
«« 
Ref_origen_evento
«« %
>
««% &
GetOrigenEvento
««' 6
(
««6 7
)
««7 8
{
¬¬ 	
return
­­ 
DACComonClass
­­  
.
­­  !
GetOrigenEvento
­­! 0
(
­­0 1
)
­­1 2
;
­­2 3
}
®® 	
public
±± 
List
±± 
<
±± 
Ref_regional
±±  
>
±±  !
GetRefRegion
±±" .
(
±±. /
)
±±/ 0
{
²² 	
return
³³ 
DACComonClass
³³  
.
³³  !
GetRefRegion
³³! -
(
³³- .
)
³³. /
;
³³/ 0
}
´´ 	
public
¶¶ 
Ref_regional
¶¶ 
GetRefRegionId
¶¶ *
(
¶¶* +
int
¶¶+ .
id
¶¶/ 1
)
¶¶1 2
{
·· 	
return
¸¸ 
DACComonClass
¸¸  
.
¸¸  !
GetRefRegionId
¸¸! /
(
¸¸/ 0
id
¸¸0 2
)
¸¸2 3
;
¸¸3 4
}
¹¹ 	
public
ºº 
List
ºº 
<
ºº !
Ref_tipo_habitacion
ºº '
>
ºº' (
GetTipoHabitacion
ºº) :
(
ºº: ;
)
ºº; <
{
»» 	
return
¼¼ 
DACComonClass
¼¼  
.
¼¼  !
GetTipoHabitacion
¼¼! 2
(
¼¼2 3
)
¼¼3 4
;
¼¼4 5
}
½½ 	
public
¿¿ 
List
¿¿ 
<
¿¿ 
Ref_tipo_ingreso
¿¿ $
>
¿¿$ %
GetTipoIngreso
¿¿& 4
(
¿¿4 5
)
¿¿5 6
{
ÀÀ 	
return
ÁÁ 
DACComonClass
ÁÁ  
.
ÁÁ  !
GetTipoIngreso
ÁÁ! /
(
ÁÁ/ 0
)
ÁÁ0 1
;
ÁÁ1 2
}
ÂÂ 	
public
ÄÄ 
List
ÄÄ 
<
ÄÄ &
Ref_condicion_alta_censo
ÄÄ ,
>
ÄÄ, -
GetCondicionAlta
ÄÄ. >
(
ÄÄ> ?
)
ÄÄ? @
{
ÅÅ 	
return
ÆÆ 
DACComonClass
ÆÆ  
.
ÆÆ  !
GetCondicionAlta
ÆÆ! 1
(
ÆÆ1 2
)
ÆÆ2 3
;
ÆÆ3 4
}
ÇÇ 	
public
ÉÉ 
List
ÉÉ 
<
ÉÉ 
	Ref_cie10
ÉÉ 
>
ÉÉ 
GetCie10
ÉÉ '
(
ÉÉ' (
)
ÉÉ( )
{
ÊÊ 	
return
ËË 
DACComonClass
ËË  
.
ËË  !
GetCie10
ËË! )
(
ËË) *
)
ËË* +
;
ËË+ ,
}
ÌÌ 	
public
ÎÎ 
List
ÎÎ 
<
ÎÎ 
vw_ref_cups
ÎÎ 
>
ÎÎ  
GetCups
ÎÎ! (
(
ÎÎ( )
)
ÎÎ) *
{
ÏÏ 	
return
ĞĞ 
DACComonClass
ĞĞ  
.
ĞĞ  !
GetCups
ĞĞ! (
(
ĞĞ( )
)
ĞĞ) *
;
ĞĞ* +
}
ÑÑ 	
public
ÓÓ 
List
ÓÓ 
<
ÓÓ 
Ref_cuentas_glosa
ÓÓ %
>
ÓÓ% &
GetCuentaGlosa
ÓÓ' 5
(
ÓÓ5 6
)
ÓÓ6 7
{
ÔÔ 	
return
ÕÕ 
DACComonClass
ÕÕ  
.
ÕÕ  !
GetCuentaGlosa
ÕÕ! /
(
ÕÕ/ 0
)
ÕÕ0 1
;
ÕÕ1 2
}
ÖÖ 	
public
ØØ 
List
ØØ 
<
ØØ 
Ref_causal_glosa
ØØ $
>
ØØ$ %
GetCausalGlosa
ØØ& 4
(
ØØ4 5
)
ØØ5 6
{
ÙÙ 	
return
ÚÚ 
DACComonClass
ÚÚ  
.
ÚÚ  !
GetCausalGlosa
ÚÚ! /
(
ÚÚ/ 0
)
ÚÚ0 1
;
ÚÚ1 2
}
ÛÛ 	
public
İİ 
List
İİ 
<
İİ #
Ref_responsable_glosa
İİ )
>
İİ) *
GetResGlosa
İİ+ 6
(
İİ6 7
)
İİ7 8
{
ŞŞ 	
return
ßß 
DACComonClass
ßß  
.
ßß  !
GetResGlosa
ßß! ,
(
ßß, -
)
ßß- .
;
ßß. /
}
àà 	
public
ââ 
List
ââ 
<
ââ &
Ref_condicion_del_egreso
ââ ,
>
ââ, - 
GetCondicionEgreso
ââ. @
(
ââ@ A
)
ââA B
{
ãã 	
return
ää 
DACComonClass
ää  
.
ää  ! 
GetCondicionEgreso
ää! 3
(
ää3 4
)
ää4 5
;
ää5 6
}
åå 	
public
çç 
List
çç 
<
çç #
Ref_servicio_tratante
çç )
>
çç) *!
GetServiciotratante
çç+ >
(
çç> ?
)
çç? @
{
èè 	
return
éé 
DACComonClass
éé  
.
éé  !!
GetServiciotratante
éé! 4
(
éé4 5
)
éé5 6
;
éé6 7
}
êê 	
public
ìì 
List
ìì 
<
ìì 
Ref_salud_publica
ìì %
>
ìì% &
GetSaludPublica
ìì' 6
(
ìì6 7
)
ìì7 8
{
íí 	
return
îî 
DACComonClass
îî  
.
îî  !
GetSaludPublica
îî! 0
(
îî0 1
)
îî1 2
;
îî2 3
}
ïï 	
public
òò 
List
òò 
<
òò /
!Ref_lesiones_severas_y_alto_costo
òò 5
>
òò5 6
GetAltoCosto
òò7 C
(
òòC D
)
òòD E
{
óó 	
return
ôô 
DACComonClass
ôô  
.
ôô  !
GetAltoCosto
ôô! -
(
ôô- .
)
ôô. /
;
ôô/ 0
}
õõ 	
public
÷÷ 
List
÷÷ 
<
÷÷ 
vw_tablero_censo
÷÷ $
>
÷÷$ %
GetTableroCenso
÷÷& 5
(
÷÷5 6
)
÷÷6 7
{
øø 	
return
ùù 
DACComonClass
ùù  
.
ùù  !
GetTableroCenso
ùù! 0
(
ùù0 1
)
ùù1 2
;
ùù2 3
}
úú 	
public
üü 
List
üü 
<
üü /
!management_vw_tablero_censoResult
üü 5
>
üü5 6%
GetTableroCensoCompleto
üü7 N
(
üüN O
)
üüO P
{
ıı 	
return
şş 
DACComonClass
şş  
.
şş  !%
GetTableroCensoCompleto
şş! 8
(
şş8 9
)
şş9 :
;
şş: ;
}
ÿÿ 	
public
 
List
 
<
 
vw_tablero_censo2
 %
>
% &
GetTableroCenso2
' 7
(
7 8
)
8 9
{
‚‚ 	
return
ƒƒ 
DACComonClass
ƒƒ  
.
ƒƒ  !
GetTableroCenso2
ƒƒ! 1
(
ƒƒ1 2
)
ƒƒ2 3
;
ƒƒ3 4
}
„„ 	
public
†† 
List
†† 
<
†† %
vw_tablero_concurrencia
†† +
>
††+ ,$
GetTableroConcurrencia
††- C
(
††C D
)
††D E
{
‡‡ 	
return
ˆˆ 
DACComonClass
ˆˆ  
.
ˆˆ  !$
GetTableroConcurrencia
ˆˆ! 7
(
ˆˆ7 8
)
ˆˆ8 9
;
ˆˆ9 :
}
‰‰ 	
public
‹‹ 
List
‹‹ 
<
‹‹ 1
#management_egresosEvolucionesResult
‹‹ 7
>
‹‹7 8
ConsultaEgresoId
‹‹9 I
(
‹‹I J
int
‹‹J M
idEgreso
‹‹N V
)
‹‹V W
{
ŒŒ 	
return
 
DACConsulta
 
.
 
ConsultaEgresoId
 /
(
/ 0
idEgreso
0 8
)
8 9
;
9 :
}
 	
public
 
List
 
<
 3
%management_concurrencia_alertasResult
 9
>
9 :2
$ConsultaConcurrenciaAlertasEvolucion
; _
(
_ `
)
` a
{
‘‘ 	
return
’’ 
DACConsulta
’’ 
.
’’ 2
$ConsultaConcurrenciaAlertasEvolucion
’’ C
(
’’C D
)
’’D E
;
’’E F
}
““ 	
public
•• 
List
•• 
<
•• :
,management_concurrencia_alerta_ReporteResult
•• @
>
••@ A9
+ConsultaConcurrenciaAlertasEvolucionReporte
••B m
(
••m n
)
••n o
{
–– 	
return
—— 
DACConsulta
—— 
.
—— 9
+ConsultaConcurrenciaAlertasEvolucionReporte
—— J
(
——J K
)
——K L
;
——L M
}
˜˜ 	
public
™™ 
int
™™ 8
*ConsultaConcurrenciaAlertasEvolucionConteo
™™ =
(
™™= >
)
™™> ?
{
šš 	
return
›› 
DACConsulta
›› 
.
›› 8
*ConsultaConcurrenciaAlertasEvolucionConteo
›› I
(
››I J
)
››J K
;
››K L
}
œœ 	
public
 
List
 
<
 
Ref_ciudades
  
>
  !
GetCiudades
" -
(
- .
)
. /
{
ŸŸ 	
return
   
DACComonClass
    
.
    !
GetCiudades
  ! ,
(
  , -
)
  - .
;
  . /
}
¡¡ 	
public
££ 
List
££ 
<
££ 
Ref_odont_unis
££ "
>
££" #
unisRegional
££$ 0
(
££0 1
int
££1 4
?
££4 5

idRegional
££6 @
)
££@ A
{
¤¤ 	
return
¥¥ 
DACConsulta
¥¥ 
.
¥¥ 
unisRegional
¥¥ +
(
¥¥+ ,

idRegional
¥¥, 6
)
¥¥6 7
;
¥¥7 8
}
¦¦ 	
public
¨¨ 
List
¨¨ 
<
¨¨ 
Ref_ciudades
¨¨  
>
¨¨  !"
GetCiudadesXRegional
¨¨" 6
(
¨¨6 7
int
¨¨7 :
?
¨¨: ;

idRegional
¨¨< F
)
¨¨F G
{
©© 	
return
ªª 
DACComonClass
ªª  
.
ªª  !"
GetCiudadesXRegional
ªª! 5
(
ªª5 6

idRegional
ªª6 @
)
ªª@ A
;
ªªA B
}
«« 	
public
­­ 
List
­­ 
<
­­ 
Ref_ciudades
­­  
>
­­  !
GetCiudadesXUnis
­­" 2
(
­­2 3
int
­­3 6
?
­­6 7
idUnis
­­8 >
)
­­> ?
{
®® 	
return
¯¯ 
DACComonClass
¯¯  
.
¯¯  !
GetCiudadesXUnis
¯¯! 1
(
¯¯1 2
idUnis
¯¯2 8
)
¯¯8 9
;
¯¯9 :
}
°° 	
public
²² 
List
²² 
<
²² 
vw_cie10
²² 
>
²² 
GetCie10Unido
²² +
(
²²+ ,
)
²², -
{
³³ 	
return
´´ 
DACComonClass
´´  
.
´´  !
GetCie10Unido
´´! .
(
´´. /
)
´´/ 0
;
´´0 1
}
µµ 	
public
¶¶ 
List
¶¶ 
<
¶¶ 
vw_cie10
¶¶ 
>
¶¶ "
GetCie10UnidoDetalle
¶¶ 2
(
¶¶2 3
)
¶¶3 4
{
·· 	
return
¸¸ 
DACComonClass
¸¸  
.
¸¸  !"
GetCie10UnidoDetalle
¸¸! 5
(
¸¸5 6
)
¸¸6 7
;
¸¸7 8
}
ºº 	
public
¼¼ 
List
¼¼ 
<
¼¼ 
Ref_causal_egreso
¼¼ %
>
¼¼% &
GetCausaEgreso
¼¼' 5
(
¼¼5 6
)
¼¼6 7
{
½½ 	
return
¾¾ 
DACComonClass
¾¾  
.
¾¾  !
GetCausaEgreso
¾¾! /
(
¾¾/ 0
)
¾¾0 1
;
¾¾1 2
}
¿¿ 	
public
ÀÀ 
List
ÀÀ 
<
ÀÀ !
vw_consulta_alertas
ÀÀ '
>
ÀÀ' ( 
GetConsultaAlertas
ÀÀ) ;
(
ÀÀ; <
)
ÀÀ< =
{
ÁÁ 	
return
ÂÂ 
DACComonClass
ÂÂ  
.
ÂÂ  ! 
GetConsultaAlertas
ÂÂ! 3
(
ÂÂ3 4
)
ÂÂ4 5
;
ÂÂ5 6
}
ÃÃ 	
public
ÅÅ 
List
ÅÅ 
<
ÅÅ 
Total_ciudades
ÅÅ "
>
ÅÅ" #
GetTotalCiudades
ÅÅ$ 4
(
ÅÅ4 5
)
ÅÅ5 6
{
ÆÆ 	
return
ÇÇ 
DACComonClass
ÇÇ  
.
ÇÇ  !
GetTotalCiudades
ÇÇ! 1
(
ÇÇ1 2
)
ÇÇ2 3
;
ÇÇ3 4
}
ÈÈ 	
public
ÊÊ 
List
ÊÊ 
<
ÊÊ '
Ref_motivo_devolucion_fac
ÊÊ -
>
ÊÊ- .$
GetMotivoDevolucionFac
ÊÊ/ E
(
ÊÊE F
)
ÊÊF G
{
ËË 	
return
ÌÌ 
DACComonClass
ÌÌ  
.
ÌÌ  !$
GetMotivoDevolucionFac
ÌÌ! 7
(
ÌÌ7 8
)
ÌÌ8 9
;
ÌÌ9 :
}
ÍÍ 	
public
ÏÏ 
List
ÏÏ 
<
ÏÏ #
vw_sis_auditor_ciudad
ÏÏ )
>
ÏÏ) * 
GetCiudadesAuditor
ÏÏ+ =
(
ÏÏ= >
)
ÏÏ> ?
{
ĞĞ 	
return
ÑÑ 
DACComonClass
ÑÑ  
.
ÑÑ  ! 
GetCiudadesAuditor
ÑÑ! 3
(
ÑÑ3 4
)
ÑÑ4 5
;
ÑÑ5 6
}
ÒÒ 	
public
ÔÔ 
List
ÔÔ 
<
ÔÔ "
sis_auditor_regional
ÔÔ (
>
ÔÔ( ) 
GetRegionalAuditor
ÔÔ* <
(
ÔÔ< =
)
ÔÔ= >
{
ÕÕ 	
return
ÖÖ 
DACComonClass
ÖÖ  
.
ÖÖ  ! 
GetRegionalAuditor
ÖÖ! 3
(
ÖÖ3 4
)
ÖÖ4 5
;
ÖÖ5 6
}
×× 	
public
ØØ 
List
ØØ 
<
ØØ "
sis_auditor_regional
ØØ (
>
ØØ( )&
listadoRegionalesUsuario
ØØ* B
(
ØØB C
int
ØØC F
	idUsuario
ØØG P
)
ØØP Q
{
ÙÙ 	
return
ÚÚ 
DACConsulta
ÚÚ 
.
ÚÚ &
listadoRegionalesUsuario
ÚÚ 7
(
ÚÚ7 8
	idUsuario
ÚÚ8 A
)
ÚÚA B
;
ÚÚB C
}
ÛÛ 	
public
İİ "
sis_auditor_regional
İİ # 
GetRegionalAuditor
İİ$ 6
(
İİ6 7
int
İİ7 :
?
İİ: ;
	idUsuario
İİ< E
)
İİE F
{
ŞŞ 	
return
ßß 
DACConsulta
ßß 
.
ßß  
GetRegionalAuditor
ßß 1
(
ßß1 2
	idUsuario
ßß2 ;
)
ßß; <
;
ßß< =
}
àà 	
public
ââ 
List
ââ 
<
ââ %
vw_sis_auditor_regional
ââ +
>
ââ+ ,"
GetVWRegionalAuditor
ââ- A
(
ââA B
)
ââB C
{
ãã 	
return
ää 
DACComonClass
ää  
.
ää  !"
GetVWRegionalAuditor
ää! 5
(
ää5 6
)
ää6 7
;
ää7 8
}
åå 	
public
çç 
List
çç 
<
çç  
Ref_hallazgos_RIPS
çç &
>
çç& '
GetRefHallazgos
çç( 7
(
çç7 8
)
çç8 9
{
èè 	
return
éé 
DACComonClass
éé  
.
éé  !
GetRefHallazgos
éé! 0
(
éé0 1
)
éé1 2
;
éé2 3
}
êê 	
public
ìì 
List
ìì 
<
ìì (
Ref_categorias_eventos_adv
ìì .
>
ìì. /!
GetRefCategoriaEvad
ìì0 C
(
ììC D
)
ììD E
{
íí 	
return
îî 
DACComonClass
îî  
.
îî  !!
GetRefCategoriaEvad
îî! 4
(
îî4 5
)
îî5 6
;
îî6 7
}
ïï 	
public
ññ 
List
ññ 
<
ññ "
Ref_motivo_reingreso
ññ (
>
ññ( ) 
GetRefMotivoRiesgo
ññ* <
(
ññ< =
)
ññ= >
{
òò 	
return
óó 
DACComonClass
óó  
.
óó  ! 
GetRefMotivoRiesgo
óó! 3
(
óó3 4
)
óó4 5
;
óó5 6
}
ôô 	
public
öö 
List
öö 
<
öö 3
%Ref_categorias_situaciones_de_calidad
öö 9
>
öö9 :&
GetRefCategoriaSituacion
öö; S
(
ööS T
)
ööT U
{
÷÷ 	
return
øø 
DACComonClass
øø  
.
øø  !&
GetRefCategoriaSituacion
øø! 9
(
øø9 :
)
øø: ;
;
øø; <
}
ùù 	
public
ûû 
List
ûû 
<
ûû 
vw_cie10_alertas
ûû $
>
ûû$ % 
GetRefcie10Alertas
ûû& 8
(
ûû8 9
)
ûû9 :
{
üü 	
return
ıı 
DACComonClass
ıı  
.
ıı  ! 
GetRefcie10Alertas
ıı! 3
(
ıı3 4
)
ıı4 5
;
ıı5 6
}
şş 	
public
ÿÿ 
List
ÿÿ 
<
ÿÿ &
vw_cie10_alertas_detalle
ÿÿ ,
>
ÿÿ, -%
GetRefcie10AlertasNuevo
ÿÿ. E
(
ÿÿE F
)
ÿÿF G
{
€€ 	
return
 
DACComonClass
  
.
  !%
GetRefcie10AlertasNuevo
! 8
(
8 9
)
9 :
;
: ;
}
‚‚ 	
public
„„ 
List
„„ 
<
„„ (
Ref_enfermedades_Huerfanas
„„ .
>
„„. /
GetRefHuerfanas
„„0 ?
(
„„? @
)
„„@ A
{
…… 	
return
†† 
DACComonClass
††  
.
††  !
GetRefHuerfanas
††! 0
(
††0 1
)
††1 2
;
††2 3
}
‡‡ 	
public
‰‰ 
List
‰‰ 
<
‰‰ 
Ref_tipo_ahorro
‰‰ #
>
‰‰# $
GetRefTipoAhorro
‰‰% 5
(
‰‰5 6
)
‰‰6 7
{
ŠŠ 	
return
‹‹ 
DACComonClass
‹‹  
.
‹‹  !
GetRefTipoAhorro
‹‹! 1
(
‹‹1 2
)
‹‹2 3
;
‹‹3 4
}
ŒŒ 	
public
 
List
 
<
 
Ref_PQRS_Atributo
 %
>
% & 
GetRefPqrsAtributo
' 9
(
9 :
)
: ;
{
 	
return
‘‘ 
DACComonClass
‘‘  
.
‘‘  ! 
GetRefPqrsAtributo
‘‘! 3
(
‘‘3 4
)
‘‘4 5
;
‘‘5 6
;
‘‘7 8
}
’’ 	
public
•• 
List
•• 
<
•• %
Ref_PQRS_estado_Gestion
•• +
>
••+ ,
GetRefPqrsGestion
••- >
(
••> ?
)
••? @
{
–– 	
return
—— 
DACComonClass
——  
.
——  !
GetRefPqrsGestion
——! 2
(
——2 3
)
——3 4
;
——4 5
}
˜˜ 	
public
›› 
List
›› 
<
›› 
Ref_PQRS_llamada
›› $
>
››$ %
GetRefPqrsLLamada
››& 7
(
››7 8
)
››8 9
{
œœ 	
return
 
DACComonClass
  
.
  !
GetRefPqrsLLamada
! 2
(
2 3
)
3 4
;
4 5
}
 	
public
   
List
   
<
   "
Ref_PQRS_Subtematica
   (
>
  ( )#
GetRefPqrsSubtematica
  * ?
(
  ? @
)
  @ A
{
¡¡ 	
return
¢¢ 
DACComonClass
¢¢  
.
¢¢  !#
GetRefPqrsSubtematica
¢¢! 6
(
¢¢6 7
)
¢¢7 8
;
¢¢8 9
}
££ 	
public
¥¥ 
List
¥¥ 
<
¥¥ %
Ref_PQRS_tipo_solicitud
¥¥ +
>
¥¥+ ,!
GetRefPqrsSolicitud
¥¥- @
(
¥¥@ A
)
¥¥A B
{
¦¦ 	
return
§§ 
DACComonClass
§§  
.
§§  !!
GetRefPqrsSolicitud
§§! 4
(
§§4 5
)
§§5 6
;
§§6 7
}
¨¨ 	
public
ªª 
List
ªª 
<
ªª 
vw_PQRS_usuarios
ªª $
>
ªª$ % 
GetRefPqrsUsuarios
ªª& 8
(
ªª8 9
)
ªª9 :
{
«« 	
return
¬¬ 
DACComonClass
¬¬  
.
¬¬  ! 
GetRefPqrsUsuarios
¬¬! 3
(
¬¬3 4
)
¬¬4 5
;
¬¬5 6
}
­­ 	
public
¯¯ 
void
¯¯ $
EliminarPQRSEnrevision
¯¯ *
(
¯¯* +
int
¯¯+ .
id_ecop_PQRS
¯¯/ ;
,
¯¯; <
ref
¯¯= @ 
MessageResponseOBJ
¯¯A S
MsgRes
¯¯T Z
)
¯¯Z [
{
°° 	

DACElimina
±± 
.
±± $
EliminarPQRSEnrevision
±± -
(
±±- .
id_ecop_PQRS
±±. :
,
±±: ;
ref
±±< ?
MsgRes
±±@ F
)
±±F G
;
±±G H
}
²² 	
public
³³ 
List
³³ 
<
³³ #
vw_md_crono_auditores
³³ )
>
³³) * 
GetRefCronoAuditor
³³+ =
(
³³= >
)
³³> ?
{
´´ 	
return
µµ 
DACComonClass
µµ  
.
µµ  ! 
GetRefCronoAuditor
µµ! 3
(
µµ3 4
)
µµ4 5
;
µµ5 6
}
¶¶ 	
public
¹¹ 
List
¹¹ 
<
¹¹ '
Ref_analaisis_caso_ambito
¹¹ -
>
¹¹- .
	Getambito
¹¹/ 8
(
¹¹8 9
)
¹¹9 :
{
ºº 	
return
»» 
DACComonClass
»»  
.
»»  !
	Getambito
»»! *
(
»»* +
)
»»+ ,
;
»», -
}
¼¼ 	
public
¾¾ 
List
¾¾ 
<
¾¾ "
Ref_eventos_decision
¾¾ (
>
¾¾( )
GetEventoDecision
¾¾* ;
(
¾¾; <
)
¾¾< =
{
¿¿ 	
return
ÀÀ 
DACComonClass
ÀÀ  
.
ÀÀ  !
GetEventoDecision
ÀÀ! 2
(
ÀÀ2 3
)
ÀÀ3 4
;
ÀÀ4 5
}
ÁÁ 	
public
ÃÃ 
List
ÃÃ 
<
ÃÃ #
Ref_eventos_habilidad
ÃÃ )
>
ÃÃ) *!
GetEventosHabilidad
ÃÃ+ >
(
ÃÃ> ?
)
ÃÃ? @
{
ÄÄ 	
return
ÅÅ 
DACComonClass
ÅÅ  
.
ÅÅ  !!
GetEventosHabilidad
ÅÅ! 4
(
ÅÅ4 5
)
ÅÅ5 6
;
ÅÅ6 7
}
ÆÆ 	
public
ÈÈ 
List
ÈÈ 
<
ÈÈ $
Ref_eventos_percepcion
ÈÈ *
>
ÈÈ* +"
GetEventosPercepcion
ÈÈ, @
(
ÈÈ@ A
)
ÈÈA B
{
ÉÉ 	
return
ÊÊ 
DACComonClass
ÊÊ  
.
ÊÊ  !"
GetEventosPercepcion
ÊÊ! 5
(
ÊÊ5 6
)
ÊÊ6 7
;
ÊÊ7 8
}
ËË 	
public
ÎÎ 
List
ÎÎ 
<
ÎÎ #
Ref_eventos_rutinaria
ÎÎ )
>
ÎÎ) *!
GetEventosRutinaria
ÎÎ+ >
(
ÎÎ> ?
)
ÎÎ? @
{
ÏÏ 	
return
ĞĞ 
DACComonClass
ĞĞ  
.
ĞĞ  !!
GetEventosRutinaria
ĞĞ! 4
(
ĞĞ4 5
)
ĞĞ5 6
;
ĞĞ6 7
}
ÑÑ 	
public
ÓÓ 
List
ÓÓ 
<
ÓÓ '
Ref_eventos_excepcionales
ÓÓ -
>
ÓÓ- .%
GetEventosexcepcionales
ÓÓ/ F
(
ÓÓF G
)
ÓÓG H
{
ÔÔ 	
return
ÕÕ 
DACComonClass
ÕÕ  
.
ÕÕ  !%
GetEventosexcepcionales
ÕÕ! 8
(
ÕÕ8 9
)
ÕÕ9 :
;
ÕÕ: ;
}
ÖÖ 	
public
ÙÙ 
List
ÙÙ 
<
ÙÙ "
Ref_eventos_paciente
ÙÙ (
>
ÙÙ( ) 
GetEventosPaciente
ÙÙ* <
(
ÙÙ< =
)
ÙÙ= >
{
ÚÚ 	
return
ÛÛ 
DACComonClass
ÛÛ  
.
ÛÛ  ! 
GetEventosPaciente
ÛÛ! 3
(
ÛÛ3 4
)
ÛÛ4 5
;
ÛÛ5 6
}
ÜÜ 	
public
ŞŞ 
List
ŞŞ 
<
ŞŞ #
Ref_eventos_tarea_tec
ŞŞ )
>
ŞŞ) *
GetEventostarea
ŞŞ+ :
(
ŞŞ: ;
)
ŞŞ; <
{
ßß 	
return
àà 
DACComonClass
àà  
.
àà  !
GetEventostarea
àà! 0
(
àà0 1
)
àà1 2
;
àà2 3
}
áá 	
public
ãã 
List
ãã 
<
ãã  
Ref_eventos_equipo
ãã &
>
ãã& '
GetEventosEquipoT
ãã( 9
(
ãã9 :
)
ãã: ;
{
ää 	
return
åå 
DACComonClass
åå  
.
åå  !
GetEventosEquipoT
åå! 2
(
åå2 3
)
åå3 4
;
åå4 5
}
ææ 	
public
éé 
List
éé 
<
éé #
Ref_eventos_individuo
éé )
>
éé) *!
GetEventosIndividuo
éé+ >
(
éé> ?
)
éé? @
{
êê 	
return
ëë 
DACComonClass
ëë  
.
ëë  !!
GetEventosIndividuo
ëë! 4
(
ëë4 5
)
ëë5 6
;
ëë6 7
}
ìì 	
public
îî 
List
îî 
<
îî &
Ref_eventos_ambiente_tra
îî ,
>
îî, -'
GetEventosambienteTrabajo
îî. G
(
îîG H
)
îîH I
{
ïï 	
return
ğğ 
DACComonClass
ğğ  
.
ğğ  !'
GetEventosambienteTrabajo
ğğ! :
(
ğğ: ;
)
ğğ; <
;
ğğ< =
}
ññ 	
public
óó 
List
óó 
<
óó &
Ref_eventos_organizacion
óó ,
>
óó, -$
GetEventosOrganizacion
óó. D
(
óóD E
)
óóE F
{
ôô 	
return
õõ 
DACComonClass
õõ  
.
õõ  !$
GetEventosOrganizacion
õõ! 7
(
õõ7 8
)
õõ8 9
;
õõ9 :
}
öö 	
public
øø 
List
øø 
<
øø "
Ref_eventos_contexto
øø (
>
øø( ) 
GetEventosContexto
øø* <
(
øø< =
)
øø= >
{
ùù 	
return
úú 
DACComonClass
úú  
.
úú  ! 
GetEventosContexto
úú! 3
(
úú3 4
)
úú4 5
;
úú5 6
}
ûû 	
public
ıı 
List
ıı 
<
ıı #
Ref_eventos_severidad
ıı )
>
ıı) *!
GetEventosSeveridad
ıı+ >
(
ıı> ?
)
ıı? @
{
şş 	
return
ÿÿ 
DACComonClass
ÿÿ  
.
ÿÿ  !!
GetEventosSeveridad
ÿÿ! 4
(
ÿÿ4 5
)
ÿÿ5 6
;
ÿÿ6 7
}
€€ 	
public
‚‚ 
List
‚‚ 
<
‚‚ $
Ref_eventos_repeticion
‚‚ *
>
‚‚* +%
GetEventosProbavilidadR
‚‚, C
(
‚‚C D
)
‚‚D E
{
ƒƒ 	
return
„„ 
DACComonClass
„„  
.
„„  !%
GetEventosProbavilidadR
„„! 8
(
„„8 9
)
„„9 :
;
„„: ;
}
…… 	
public
‡‡ 
List
‡‡ 
<
‡‡ %
Ref_eventos_tipo_evento
‡‡ +
>
‡‡+ ,"
GetEventosTipoEvento
‡‡- A
(
‡‡A B
)
‡‡B C
{
ˆˆ 	
return
‰‰ 
DACComonClass
‰‰  
.
‰‰  !"
GetEventosTipoEvento
‰‰! 5
(
‰‰5 6
)
‰‰6 7
;
‰‰7 8
}
ŠŠ 	
public
ŒŒ 
List
ŒŒ 
<
ŒŒ 
vw_md_ref_glosa
ŒŒ #
>
ŒŒ# $
GetMedglosa
ŒŒ% 0
(
ŒŒ0 1
)
ŒŒ1 2
{
 	
return
 
DACComonClass
  
.
  !
GetMedglosa
! ,
(
, -
)
- .
;
. /
}
 	
public
‘‘ 
List
‘‘ 
<
‘‘ !
vw_md_Ref_indicador
‘‘ '
>
‘‘' (
GetMDIndicadores
‘‘) 9
(
‘‘9 :
)
‘‘: ;
{
’’ 	
return
““ 
DACComonClass
““  
.
““  !
GetMDIndicadores
““! 1
(
““1 2
)
““2 3
;
““3 4
}
”” 	
public
–– 
List
–– 
<
––  
ref_meses_del_aÃ±o
–– %
>
––% &
meses
––' ,
(
––, -
)
––- .
{
—— 	
return
˜˜ 
DACComonClass
˜˜  
.
˜˜  !
meses
˜˜! &
(
˜˜& '
)
˜˜' (
;
˜˜( )
}
™™ 	
public
›› 
List
›› 
<
›› 
ref_tipo_cohorte
›› $
>
››$ %
tipoCohortes
››& 2
(
››2 3
)
››3 4
{
œœ 	
return
 
DACComonClass
  
.
  !
tipoCohortes
! -
(
- .
)
. /
;
/ 0
}
 	
public
   
List
   
<
   (
Ref_origen_hospitalizacion
   .
>
  . /&
GetOrigenHospitalizacion
  0 H
(
  H I
)
  I J
{
¡¡ 	
return
¢¢ 
DACComonClass
¢¢  
.
¢¢  !&
GetOrigenHospitalizacion
¢¢! 9
(
¢¢9 :
)
¢¢: ;
;
¢¢; <
}
££ 	
public
¥¥ 
List
¥¥ 
<
¥¥ +
vw_ref_enfermedades_huerfanas
¥¥ 1
>
¥¥1 2&
GetEnfermedadesHuerfanas
¥¥3 K
(
¥¥K L
)
¥¥L M
{
¦¦ 	
return
§§ 
DACComonClass
§§  
.
§§  !&
GetEnfermedadesHuerfanas
§§! 9
(
§§9 :
)
§§: ;
;
§§; <
}
¨¨ 	
public
ªª 
List
ªª 
<
ªª &
vw_evo_ecop_concurrencia
ªª ,
>
ªª, -&
ConsultaIdConcurreniaEvo
ªª. F
(
ªªF G&
vw_evo_ecop_concurrencia
ªªG _
ObjAfiliado
ªª` k
,
ªªk l
ref
ªªm p!
MessageResponseOBJªªq ƒ
MsgResªª„ Š
)ªªŠ ‹
{
«« 	
return
¬¬ 
DACConsulta
¬¬ 
.
¬¬ &
ConsultaIdConcurreniaEvo
¬¬ 7
(
¬¬7 8
ObjAfiliado
¬¬8 C
,
¬¬C D
ref
¬¬E H
MsgRes
¬¬I O
)
¬¬O P
;
¬¬P Q
}
­­ 	
public
¯¯ 
List
¯¯ 
<
¯¯ )
ecop_concurrencia_evolucion
¯¯ /
>
¯¯/ 0'
ConsultaNumeroEvoluciones
¯¯1 J
(
¯¯J K)
ecop_concurrencia_evolucion
¯¯K f
ObjAfiliado
¯¯g r
,
¯¯r s
ref
¯¯t w!
MessageResponseOBJ¯¯x Š
MsgRes¯¯‹ ‘
)¯¯‘ ’
{
°° 	
return
±± 
DACConsulta
±± 
.
±± '
ConsultaNumeroEvoluciones
±± 8
(
±±8 9
ObjAfiliado
±±9 D
,
±±D E
ref
±±F I
MsgRes
±±J P
)
±±P Q
;
±±Q R
}
²² 	
public
´´ 
List
´´ 
<
´´ 
Ref_rol_cargo
´´ !
>
´´! "
RolCargo
´´# +
(
´´+ ,
)
´´, -
{
µµ 	
return
¶¶ 
DACComonClass
¶¶  
.
¶¶  !
RolCargo
¶¶! )
(
¶¶) *
)
¶¶* +
;
¶¶+ ,
}
·· 	
public
¹¹ 
List
¹¹ 
<
¹¹ 
Ref_odont_unis
¹¹ "
>
¹¹" #

Odont_unis
¹¹$ .
(
¹¹. /
)
¹¹/ 0
{
ºº 	
return
»» 
DACComonClass
»»  
.
»»  !

Odont_unis
»»! +
(
»»+ ,
)
»», -
;
»»- .
}
¼¼ 	
public
¾¾ 
List
¾¾ 
<
¾¾ 
Ref_odont_unis
¾¾ "
>
¾¾" #"
Odont_unisIdRegional
¾¾$ 8
(
¾¾8 9
int
¾¾9 <
?
¾¾< =

idRegional
¾¾> H
)
¾¾H I
{
¿¿ 	
return
ÀÀ 
DACComonClass
ÀÀ  
.
ÀÀ  !"
Odont_unisIdRegional
ÀÀ! 5
(
ÀÀ5 6

idRegional
ÀÀ6 @
)
ÀÀ@ A
;
ÀÀA B
}
ÁÁ 	
public
ÂÂ 
List
ÂÂ 
<
ÂÂ 2
$ref_odontologia_protesisfija_dientes
ÂÂ 8
>
ÂÂ8 9'
OdontoProtesisFijaDientes
ÂÂ: S
(
ÂÂS T
int
ÂÂT W
?
ÂÂW X
tipo
ÂÂY ]
)
ÂÂ] ^
{
ÃÃ 	
return
ÄÄ 
DACConsulta
ÄÄ 
.
ÄÄ '
OdontoProtesisFijaDientes
ÄÄ 8
(
ÄÄ8 9
tipo
ÄÄ9 =
)
ÄÄ= >
;
ÄÄ> ?
}
ÅÅ 	
public
ÇÇ 
List
ÇÇ 
<
ÇÇ 2
$ref_odontologia_protesisfija_dientes
ÇÇ 8
>
ÇÇ8 9,
OdontoProtesisFijaDientesTotal
ÇÇ: X
(
ÇÇX Y
)
ÇÇY Z
{
ÈÈ 	
return
ÉÉ 
DACConsulta
ÉÉ 
.
ÉÉ ,
OdontoProtesisFijaDientesTotal
ÉÉ =
(
ÉÉ= >
)
ÉÉ> ?
;
ÉÉ? @
}
ÊÊ 	
public
ÌÌ 2
$ref_odontologia_protesisfija_dientes
ÌÌ 3
TraerDienteId
ÌÌ4 A
(
ÌÌA B
int
ÌÌB E
?
ÌÌE F
id
ÌÌG I
)
ÌÌI J
{
ÍÍ 	
return
ÎÎ 
DACConsulta
ÎÎ 
.
ÎÎ 
TraerDienteId
ÎÎ ,
(
ÎÎ, -
id
ÎÎ- /
)
ÎÎ/ 0
;
ÎÎ0 1
}
ÏÏ 	
public
ÑÑ 
List
ÑÑ 
<
ÑÑ 
	Ref_si_no
ÑÑ 
>
ÑÑ 
Ref_sino
ÑÑ '
(
ÑÑ' (
)
ÑÑ( )
{
ÒÒ 	
return
ÓÓ 
DACComonClass
ÓÓ  
.
ÓÓ  !
Ref_sino
ÓÓ! )
(
ÓÓ) *
)
ÓÓ* +
;
ÓÓ+ ,
}
ÕÕ 	
public
×× 
List
×× 
<
×× #
vw_ref_regiona_ciudad
×× )
>
××) *!
Ref_regional_ciudad
××+ >
(
××> ?
)
××? @
{
ØØ 	
return
ÙÙ 
DACComonClass
ÙÙ  
.
ÙÙ  !!
Ref_regional_ciudad
ÙÙ! 4
(
ÙÙ4 5
)
ÙÙ5 6
;
ÙÙ6 7
}
ÚÚ 	
public
ÛÛ 
List
ÛÛ 
<
ÛÛ .
 management_regional_ciudadResult
ÛÛ 4
>
ÛÛ4 5+
Ref_regional_ciudad_detallado
ÛÛ6 S
(
ÛÛS T
)
ÛÛT U
{
ÜÜ 	
return
İİ 
DACComonClass
İİ  
.
İİ  !+
Ref_regional_ciudad_detallado
İİ! >
(
İİ> ?
)
İİ? @
;
İİ@ A
}
ŞŞ 	
public
ßß 
List
ßß 
<
ßß *
Ref_plan_mejora_estado_tarea
ßß 0
>
ßß0 1
estadoTarea
ßß2 =
(
ßß= >
)
ßß> ?
{
àà 	
return
áá 
DACComonClass
áá  
.
áá  !
estadoTarea
áá! ,
(
áá, -
)
áá- .
;
áá. /
}
ââ 	
public
ää 
List
ää 
<
ää 5
'ManagementPrestadoresAlertasFechaResult
ää ;
>
ää; <
ManagmentAlertas
ää= M
(
ääM N
ref
ääN Q 
MessageResponseOBJ
ääR d
MsgRes
ääe k
)
ääk l
{
åå 	
return
ææ 
DACComonClass
ææ  
.
ææ  !
ManagmentAlertas
ææ! 1
(
ææ1 2
ref
ææ2 5
MsgRes
ææ6 <
)
ææ< =
;
ææ= >
}
çç 	
public
éé 
List
éé 
<
éé $
ref_consulta_ecopetrol
éé *
>
éé* +$
Ref_ConsultasEcopetrol
éé, B
(
ééB C
)
ééC D
{
êê 	
return
ëë 
DACComonClass
ëë  
.
ëë  !$
Ref_ConsultasEcopetrol
ëë! 7
(
ëë7 8
)
ëë8 9
;
ëë9 :
}
ìì 	
public
õõ 
void
õõ '
InsertarActividadReciente
õõ -
(
õõ- .$
sis_actividad_reciente
õõ. D
obj
õõE H
,
õõH I
ref
õõJ M 
MessageResponseOBJ
õõN `
MsgRes
õõa g
)
õõg h
{
öö 	
DACComonClass
÷÷ 
.
÷÷ '
InsertarActividadReciente
÷÷ 3
(
÷÷3 4
obj
÷÷4 7
,
÷÷7 8
ref
÷÷9 <
MsgRes
÷÷= C
)
÷÷C D
;
÷÷D E
}
øø 	
public
€€ 
List
€€ 
<
€€ 5
'Management_sis_actividad_recienteResult
€€ ;
>
€€; <&
GetListActividadReciente
€€= U
(
€€U V
)
€€V W
{
 	
return
‚‚ 
DACComonClass
‚‚  
.
‚‚  !&
GetListActividadReciente
‚‚! 9
(
‚‚9 :
)
‚‚: ;
;
‚‚; <
}
ƒƒ 	
public
‡‡ 
List
‡‡ 
<
‡‡ 
Ref_ffmm_glosas
‡‡ #
>
‡‡# $
FFMM_glosas
‡‡% 0
(
‡‡0 1
)
‡‡1 2
{
ˆˆ 	
return
‰‰ 
DACComonClass
‰‰  
.
‰‰  !
FFMM_glosas
‰‰! ,
(
‰‰, -
)
‰‰- .
;
‰‰. /
}
ŠŠ 	
public
ŒŒ 
List
ŒŒ 
<
ŒŒ !
Ref_ffmm_alto_costo
ŒŒ '
>
ŒŒ' (
FFMM_altocosto
ŒŒ) 7
(
ŒŒ7 8
)
ŒŒ8 9
{
 	
return
 
DACComonClass
  
.
  !
FFMM_altocosto
! /
(
/ 0
)
0 1
;
1 2
}
 	
public
‘‘ 
List
‘‘ 
<
‘‘ *
Ref_ffmm_especialidad_remite
‘‘ 0
>
‘‘0 1&
FFMM_especialidad_remite
‘‘2 J
(
‘‘J K
)
‘‘K L
{
’’ 	
return
““ 
DACComonClass
““  
.
““  !&
FFMM_especialidad_remite
““! 9
(
““9 :
)
““: ;
;
““; <
}
”” 	
public
–– 
List
–– 
<
–– +
Ref_ffmm_especialidad_remitio
–– 1
>
––1 2'
FFMM_especialidad_remitio
––3 L
(
––L M
)
––M N
{
—— 	
return
˜˜ 
DACComonClass
˜˜  
.
˜˜  !'
FFMM_especialidad_remitio
˜˜! :
(
˜˜: ;
)
˜˜; <
;
˜˜< =
}
™™ 	
public
›› 
List
›› 
<
›› 
Ref_ffmm_estado
›› #
>
››# $
FFMM_estado
››% 0
(
››0 1
)
››1 2
{
œœ 	
return
 
DACComonClass
  
.
  !
FFMM_estado
! ,
(
, -
)
- .
;
. /
}
 	
public
   
List
   
<
   
Ref_ffmm_fuerza
   #
>
  # $
FFMM_fuerza
  % 0
(
  0 1
)
  1 2
{
¡¡ 	
return
¢¢ 
DACComonClass
¢¢  
.
¢¢  !
FFMM_fuerza
¢¢! ,
(
¢¢, -
)
¢¢- .
;
¢¢. /
}
££ 	
public
¥¥ 
List
¥¥ 
<
¥¥ "
Ref_ffmm_imagen_proc
¥¥ (
>
¥¥( )
FFMM_imagen_proc
¥¥* :
(
¥¥: ;
)
¥¥; <
{
¦¦ 	
return
§§ 
DACComonClass
§§  
.
§§  !
FFMM_imagen_proc
§§! 1
(
§§1 2
)
§§2 3
;
§§3 4
}
¨¨ 	
public
ªª 
List
ªª 
<
ªª %
Ref_ffmm_modalidad_pago
ªª +
>
ªª+ ,!
FFMM_modalidad_pago
ªª- @
(
ªª@ A
)
ªªA B
{
«« 	
return
¬¬ 
DACComonClass
¬¬  
.
¬¬  !!
FFMM_modalidad_pago
¬¬! 4
(
¬¬4 5
)
¬¬5 6
;
¬¬6 7
}
­­ 	
public
®® 
List
®® 
<
®® %
Ref_ffmm_nivel_atencion
®® +
>
®®+ ,!
FFMM_nivel_atencion
®®- @
(
®®@ A
)
®®A B
{
¯¯ 	
return
°° 
DACComonClass
°°  
.
°°  !!
FFMM_nivel_atencion
°°! 4
(
°°4 5
)
°°5 6
;
°°6 7
}
±± 	
public
³³ 
List
³³ 
<
³³ (
Ref_ffmm_nivel_complejidad
³³ .
>
³³. /$
FFMM_nivel_complejidad
³³0 F
(
³³F G
)
³³G H
{
´´ 	
return
µµ 
DACComonClass
µµ  
.
µµ  !$
FFMM_nivel_complejidad
µµ! 7
(
µµ7 8
)
µµ8 9
;
µµ9 :
}
¶¶ 	
public
¸¸ 
List
¸¸ 
<
¸¸ &
Ref_ffmm_origen_servicio
¸¸ ,
>
¸¸, -"
FFMM_origen_servicio
¸¸. B
(
¸¸B C
)
¸¸C D
{
¹¹ 	
return
ºº 
DACComonClass
ºº  
.
ºº  !"
FFMM_origen_servicio
ºº! 5
(
ºº5 6
)
ºº6 7
;
ºº7 8
}
»» 	
public
½½ 
List
½½ 
<
½½ "
Ref_ffmm_prestadores
½½ (
>
½½( )
FFMM_prestadores
½½* :
(
½½: ;
)
½½; <
{
¾¾ 	
return
¿¿ 
DACComonClass
¿¿  
.
¿¿  !
FFMM_prestadores
¿¿! 1
(
¿¿1 2
)
¿¿2 3
;
¿¿3 4
}
ÀÀ 	
public
ÂÂ 
List
ÂÂ 
<
ÂÂ 
vw_ffmm_ips
ÂÂ 
>
ÂÂ  !
FFMM_prestadores_vw
ÂÂ! 4
(
ÂÂ4 5
)
ÂÂ5 6
{
ÃÃ 	
return
ÄÄ 
DACComonClass
ÄÄ  
.
ÄÄ  !!
FFMM_prestadores_vw
ÄÄ! 4
(
ÄÄ4 5
)
ÄÄ5 6
;
ÄÄ6 7
}
ÅÅ 	
public
ÆÆ 
List
ÆÆ 
<
ÆÆ 
Ref_ffmm_proceso
ÆÆ $
>
ÆÆ$ %
FFMM_proceso
ÆÆ& 2
(
ÆÆ2 3
)
ÆÆ3 4
{
ÇÇ 	
return
ÈÈ 
DACComonClass
ÈÈ  
.
ÈÈ  !
FFMM_proceso
ÈÈ! -
(
ÈÈ- .
)
ÈÈ. /
;
ÈÈ/ 0
}
ÉÉ 	
public
ËË 
List
ËË 
<
ËË 
Ref_ffmm_servicio
ËË %
>
ËË% &
FFMM_servicio
ËË' 4
(
ËË4 5
)
ËË5 6
{
ÌÌ 	
return
ÍÍ 
DACComonClass
ÍÍ  
.
ÍÍ  !
FFMM_servicio
ÍÍ! .
(
ÍÍ. /
)
ÍÍ/ 0
;
ÍÍ0 1
}
ÎÎ 	
public
ĞĞ 
List
ĞĞ 
<
ĞĞ 
Ref_ffmm_sexo
ĞĞ !
>
ĞĞ! "
	FFMM_sexo
ĞĞ# ,
(
ĞĞ, -
)
ĞĞ- .
{
ÑÑ 	
return
ÒÒ 
DACComonClass
ÒÒ  
.
ÒÒ  !
	FFMM_sexo
ÒÒ! *
(
ÒÒ* +
)
ÒÒ+ ,
;
ÒÒ, -
}
ÓÓ 	
public
ÕÕ 
List
ÕÕ 
<
ÕÕ 
Ref_ffmm_sino
ÕÕ !
>
ÕÕ! "
	FFMM_sino
ÕÕ# ,
(
ÕÕ, -
)
ÕÕ- .
{
ÖÖ 	
return
×× 
DACComonClass
××  
.
××  !
	FFMM_sino
××! *
(
××* +
)
××+ ,
;
××, -
}
ÙÙ 	
public
ÛÛ 
List
ÛÛ 
<
ÛÛ &
Ref_ffmm_tipo_afiliacion
ÛÛ ,
>
ÛÛ, -"
FFMM_tipo_afiliacion
ÛÛ. B
(
ÛÛB C
)
ÛÛC D
{
ÜÜ 	
return
İİ 
DACComonClass
İİ  
.
İİ  !"
FFMM_tipo_afiliacion
İİ! 5
(
İİ5 6
)
İİ6 7
;
İİ7 8
}
ßß 	
public
áá 
List
áá 
<
áá $
Ref_ffmm_tipo_servicio
áá *
>
áá* + 
FFMM_tipo_servicio
áá, >
(
áá> ?
)
áá? @
{
ââ 	
return
ãã 
DACComonClass
ãã  
.
ãã  ! 
FFMM_tipo_servicio
ãã! 3
(
ãã3 4
)
ãã4 5
;
ãã5 6
}
åå 	
public
çç 
List
çç 
<
çç &
Ref_ffmm_unidad_satelite
çç ,
>
çç, -"
FFMM_unidad_satelite
çç. B
(
ççB C
)
ççC D
{
èè 	
return
éé 
DACComonClass
éé  
.
éé  !"
FFMM_unidad_satelite
éé! 5
(
éé5 6
)
éé6 7
;
éé7 8
}
ëë 	
public
íí 
List
íí 
<
íí 
Ref_ffmm_unudadR
íí $
>
íí$ %
FFMM_unudadR
íí& 2
(
íí2 3
)
íí3 4
{
îî 	
return
ïï 
DACComonClass
ïï  
.
ïï  !
FFMM_unudadR
ïï! -
(
ïï- .
)
ïï. /
;
ïï/ 0
}
ğğ 	
public
óó 
List
óó 
<
óó "
vw_ffmm_departamento
óó (
>
óó( )
FFMM_departamento
óó* ;
(
óó; <
)
óó< =
{
ôô 	
return
õõ 
DACComonClass
õõ  
.
õõ  !
FFMM_departamento
õõ! 2
(
õõ2 3
)
õõ3 4
;
õõ4 5
}
öö 	
public
øø 
List
øø 
<
øø 
vw_ffmm_municipio
øø %
>
øø% &
FFMM_municipio
øø' 5
(
øø5 6
)
øø6 7
{
ùù 	
return
úú 
DACComonClass
úú  
.
úú  !
FFMM_municipio
úú! /
(
úú/ 0
)
úú0 1
;
úú1 2
}
ûû 	
public
ıı 
List
ıı 
<
ıı 
vw_FMM_RefGeneral
ıı %
>
ıı% &
FFMM_Reg_General
ıı' 7
(
ıı7 8
)
ıı8 9
{
şş 	
return
ÿÿ 
DACComonClass
ÿÿ  
.
ÿÿ  !
FFMM_Reg_General
ÿÿ! 1
(
ÿÿ1 2
)
ÿÿ2 3
;
ÿÿ3 4
}
€€ 	
public
‚‚ 
List
‚‚ 
<
‚‚ ,
Ref_ffmm_prestadores_proveedor
‚‚ 2
>
‚‚2 3(
FFMM_prestadores_Proveedor
‚‚4 N
(
‚‚N O
)
‚‚O P
{
ƒƒ 	
return
„„ 
DACComonClass
„„  
.
„„  !(
FFMM_prestadores_Proveedor
„„! ;
(
„„; <
)
„„< =
;
„„= >
}
…… 	
public
‡‡ 
List
‡‡ 
<
‡‡ %
Ref_ffmm_tipo_proveedor
‡‡ +
>
‡‡+ ,!
FFMM_tipo_proveedor
‡‡- @
(
‡‡@ A
)
‡‡A B
{
ˆˆ 	
return
‰‰ 
DACComonClass
‰‰  
.
‰‰  !!
FFMM_tipo_proveedor
‰‰! 4
(
‰‰4 5
)
‰‰5 6
;
‰‰6 7
}
ŠŠ 	
public
ŒŒ 
List
ŒŒ 
<
ŒŒ '
Ref_ffmm_glosas_respuesta
ŒŒ -
>
ŒŒ- .$
FFMM_respuestas_glosas
ŒŒ/ E
(
ŒŒE F
)
ŒŒF G
{
 	
return
 
DACComonClass
  
.
  !$
FFMM_respuestas_glosas
! 7
(
7 8
)
8 9
;
9 :
}
 	
public
‘‘ 
List
‘‘ 
<
‘‘  
Ref_ffmm_unidad_cp
‘‘ &
>
‘‘& '
FFMM_Unidad_CP
‘‘( 6
(
‘‘6 7
)
‘‘7 8
{
’’ 	
return
““ 
DACComonClass
““  
.
““  !
FFMM_Unidad_CP
““! /
(
““/ 0
)
““0 1
;
““1 2
}
”” 	
public
•• 
List
•• 
<
•• %
Ref_ffmm_tipo_visita_cp
•• +
>
••+ ,
FFMM_TipoV_CP
••- :
(
••: ;
)
••; <
{
–– 	
return
—— 
DACComonClass
——  
.
——  !
FFMM_TipoV_CP
——! .
(
——. /
)
——/ 0
;
——0 1
}
˜˜ 	
public
šš 
Int32
šš #
InsertarFFMMAuditoria
šš *
(
šš* +
ffmm_auditoria
šš+ 9
OBJ
šš: =
,
šš= >
ref
šš? B 
MessageResponseOBJ
ššC U
MsgRes
ššV \
)
šš\ ]
{
›› 	
return
œœ 

DACInserta
œœ 
.
œœ #
InsertarFFMMAuditoria
œœ 3
(
œœ3 4
OBJ
œœ4 7
,
œœ7 8
ref
œœ9 <
MsgRes
œœ= C
)
œœC D
;
œœD E
}
 	
public
ŸŸ 
List
ŸŸ 
<
ŸŸ +
ref_ffmm_ips_prestadores_tipo
ŸŸ 1
>
ŸŸ1 2
tipoIpsPrestador
ŸŸ3 C
(
ŸŸC D
)
ŸŸD E
{
   	
return
¡¡ 
DACComonClass
¡¡  
.
¡¡  !
tipoIpsPrestador
¡¡! 1
(
¡¡1 2
)
¡¡2 3
;
¡¡3 4
}
¢¢ 	
public
¤¤ 
List
¤¤ 
<
¤¤ 3
%management_traerIpsoPrestadoresResult
¤¤ 9
>
¤¤9 : 
traerPrestadoresId
¤¤; M
(
¤¤M N
int
¤¤N Q
tipo
¤¤R V
,
¤¤V W
int
¤¤X [
nit
¤¤\ _
)
¤¤_ `
{
¥¥ 	
return
¦¦ 
DACConsulta
¦¦ 
.
¦¦  
traerPrestadoresId
¦¦ 1
(
¦¦1 2
tipo
¦¦2 6
,
¦¦6 7
nit
¦¦8 ;
)
¦¦; <
;
¦¦< =
}
§§ 	
public
¨¨ 
int
¨¨ "
InsertarIpsPrestador
¨¨ '
(
¨¨' (&
ref_ffmm_ips_prestadores
¨¨( @
obj
¨¨A D
)
¨¨D E
{
©© 	
return
ªª 

DACInserta
ªª 
.
ªª "
InsertarIpsPrestador
ªª 2
(
ªª2 3
obj
ªª3 6
)
ªª6 7
;
ªª7 8
}
«« 	
public
¬¬ 
List
¬¬ 
<
¬¬ &
ref_ffmm_ips_prestadores
¬¬ ,
>
¬¬, -&
existenciaIpsPrestadores
¬¬. F
(
¬¬F G
int
¬¬G J
nit
¬¬K N
)
¬¬N O
{
­­ 	
return
®® 
DACConsulta
®® 
.
®® &
existenciaIpsPrestadores
®® 7
(
®®7 8
nit
®®8 ;
)
®®; <
;
®®< =
}
¯¯ 	
public
°° 
List
°° 
<
°° &
ref_ffmm_ips_prestadores
°° ,
>
°°, -,
existenciaIpsPrestadoresNombre
°°. L
(
°°L M
String
°°M S
nombre
°°T Z
)
°°Z [
{
±± 	
return
²² 
DACConsulta
²² 
.
²² ,
existenciaIpsPrestadoresNombre
²² =
(
²²= >
nombre
²²> D
)
²²D E
;
²²E F
}
³³ 	
public
´´ 
int
´´ &
ActualizarIpsPrestadores
´´ +
(
´´+ ,&
ref_ffmm_ips_prestadores
´´, D
obj
´´E H
)
´´H I
{
µµ 	
return
¶¶ 
DACActualiza
¶¶ 
.
¶¶  &
ActualizarIpsPrestadores
¶¶  8
(
¶¶8 9
obj
¶¶9 <
)
¶¶< =
;
¶¶= >
}
·· 	
public
¹¹ 
List
¹¹ 
<
¹¹ &
ref_ffmm_ips_prestadores
¹¹ ,
>
¹¹, -!
ObtenerIpsPrestador
¹¹. A
(
¹¹A B
int
¹¹B E
idCiudad
¹¹F N
,
¹¹N O
int
¹¹P S
tipo
¹¹T X
)
¹¹X Y
{
ºº 	
return
»» 
DACConsulta
»» 
.
»» !
ObtenerIpsPrestador
»» 2
(
»»2 3
idCiudad
»»3 ;
,
»»; <
tipo
»»= A
)
»»A B
;
»»B C
}
¼¼ 	
public
¾¾ 
List
¾¾ 
<
¾¾ &
ref_ffmm_ips_prestadores
¾¾ ,
>
¾¾, -(
ObtenerIpsPrestadorSinTipo
¾¾. H
(
¾¾H I
int
¾¾I L
idCiudad
¾¾M U
)
¾¾U V
{
¿¿ 	
return
ÀÀ 
DACConsulta
ÀÀ 
.
ÀÀ (
ObtenerIpsPrestadorSinTipo
ÀÀ 9
(
ÀÀ9 :
idCiudad
ÀÀ: B
)
ÀÀB C
;
ÀÀC D
}
ÁÁ 	
public
ÂÂ 
List
ÂÂ 
<
ÂÂ &
ref_ffmm_ips_prestadores
ÂÂ ,
>
ÂÂ, -)
ObtenerIpsPrestadorCompleto
ÂÂ. I
(
ÂÂI J
)
ÂÂJ K
{
ÃÃ 	
return
ÄÄ 
DACConsulta
ÄÄ 
.
ÄÄ )
ObtenerIpsPrestadorCompleto
ÄÄ :
(
ÄÄ: ;
)
ÄÄ; <
;
ÄÄ< =
}
ÅÅ 	
public
ÆÆ 
List
ÆÆ 
<
ÆÆ 4
&management_contratos_prestadoresResult
ÆÆ :
>
ÆÆ: ;(
ObtenerIpsPrestadorTablero
ÆÆ< V
(
ÆÆV W
)
ÆÆW X
{
ÇÇ 	
return
ÈÈ 
DACConsulta
ÈÈ 
.
ÈÈ (
ObtenerIpsPrestadorTablero
ÈÈ 9
(
ÈÈ9 :
)
ÈÈ: ;
;
ÈÈ; <
}
ÉÉ 	
public
ÌÌ 
List
ÌÌ 
<
ÌÌ 6
(managmentFFMMfacturasRadicadasLoteResult
ÌÌ <
>
ÌÌ< =&
GetRecepcionFacturasFFMM
ÌÌ> V
(
ÌÌV W
ref
ÌÌW Z 
MessageResponseOBJ
ÌÌ[ m
MsgRes
ÌÌn t
)
ÌÌt u
{
ÍÍ 	
return
ÎÎ 
DACConsulta
ÎÎ 
.
ÎÎ &
GetRecepcionFacturasFFMM
ÎÎ 7
(
ÎÎ7 8
ref
ÎÎ8 ;
MsgRes
ÎÎ< B
)
ÎÎB C
;
ÎÎC D
}
ÏÏ 	
public
ÑÑ 
List
ÑÑ 
<
ÑÑ 6
(Management_FFMM_Glosas_PrestadoresResult
ÑÑ <
>
ÑÑ< =&
GetFFMMGlosasPrestadores
ÑÑ> V
(
ÑÑV W
ref
ÑÑW Z 
MessageResponseOBJ
ÑÑ[ m
MsgRes
ÑÑn t
)
ÑÑt u
{
ÒÒ 	
return
ÓÓ 
DACConsulta
ÓÓ 
.
ÓÓ &
GetFFMMGlosasPrestadores
ÓÓ 7
(
ÓÓ7 8
ref
ÓÓ8 ;
MsgRes
ÓÓ< B
)
ÓÓB C
;
ÓÓC D
}
ÔÔ 	
public
ÕÕ 
List
ÕÕ 
<
ÕÕ 6
(managmentFFMMfacturasRadicadasdtllResult
ÕÕ <
>
ÕÕ< =*
GetRecepcionFacturasDTLLFFMM
ÕÕ> Z
(
ÕÕZ [
Int32
ÕÕ[ `
id_cargue_base
ÕÕa o
,
ÕÕo p
ref
ÕÕq t!
MessageResponseOBJÕÕu ‡
MsgResÕÕˆ 
)ÕÕ 
{
ÖÖ 	
return
×× 
DACConsulta
×× 
.
×× *
GetRecepcionFacturasDTLLFFMM
×× ;
(
××; <
id_cargue_base
××< J
,
××J K
ref
××L O
MsgRes
××P V
)
××V W
;
××W X
}
ØØ 	
public
ÚÚ 
List
ÚÚ 
<
ÚÚ 9
+managmentFFMMfacturasRadicadasid_dtllResult
ÚÚ ?
>
ÚÚ? @,
GetRecepcionFacturasDTLLFFMMId
ÚÚA _
(
ÚÚ_ `
Int32
ÚÚ` e
id_cargue_dtll
ÚÚf t
,
ÚÚt u
ref
ÚÚv y!
MessageResponseOBJÚÚz Œ
MsgResÚÚ “
)ÚÚ“ ”
{
ÛÛ 	
return
ÜÜ 
DACConsulta
ÜÜ 
.
ÜÜ ,
GetRecepcionFacturasDTLLFFMMId
ÜÜ =
(
ÜÜ= >
id_cargue_dtll
ÜÜ> L
,
ÜÜL M
ref
ÜÜN Q
MsgRes
ÜÜR X
)
ÜÜX Y
.
ÜÜY Z
ToList
ÜÜZ `
(
ÜÜ` a
)
ÜÜa b
;
ÜÜb c
}
İİ 	
public
ßß 
List
ßß 
<
ßß 4
&Management_actualizar_FacturaDigResult
ßß :
>
ßß: ;$
ActualizarFFMMFacturas
ßß< R
(
ßßR S
Int32
ßßS X

Id_factura
ßßY c
,
ßßc d
Int32
ßße j
estado
ßßk q
,
ßßq r
ref
ßßs v!
MessageResponseOBJßßw ‰
MsgResßßŠ 
)ßß ‘
{
àà 	
return
áá 
DACActualiza
áá 
.
áá  $
ActualizarFFMMFacturas
áá  6
(
áá6 7

Id_factura
áá7 A
,
ááA B
estado
ááC I
,
ááI J
ref
ááK N
MsgRes
ááO U
)
ááU V
;
ááV W
}
ââ 	
public
åå 
List
åå 
<
åå 5
'Management_FFMM_Consultas_cuentasResult
åå ;
>
åå; <$
GetConsultaFFMMCuentas
åå= S
(
ååS T
ref
ååT W 
MessageResponseOBJ
ååX j
MsgRes
ååk q
)
ååq r
{
ææ 	
return
çç 
DACConsulta
çç 
.
çç $
GetConsultaFFMMCuentas
çç 5
(
çç5 6
ref
çç6 9
MsgRes
çç: @
)
çç@ A
;
ççA B
}
éé 	
public
ëë 
List
ëë 
<
ëë 9
+Management_FFMM_Consultas_ConcurreniaResult
ëë ?
>
ëë? @)
GetConsultaFFMMConcurrencia
ëëA \
(
ëë\ ]
ref
ëë] ` 
MessageResponseOBJ
ëëa s
MsgRes
ëët z
)
ëëz {
{
ìì 	
return
íí 
DACConsulta
íí 
.
íí )
GetConsultaFFMMConcurrencia
íí :
(
íí: ;
ref
íí; >
MsgRes
íí? E
)
ííE F
;
ííF G
}
îî 	
public
ğğ 
List
ğğ 
<
ğğ 1
#Management_FFMM_Consultas_PADResult
ğğ 7
>
ğğ7 8 
GetConsultaFFMMPad
ğğ9 K
(
ğğK L
ref
ğğL O 
MessageResponseOBJ
ğğP b
MsgRes
ğğc i
)
ğği j
{
ññ 	
return
òò 
DACConsulta
òò 
.
òò  
GetConsultaFFMMPad
òò 1
(
òò1 2
ref
òò2 5
MsgRes
òò6 <
)
òò< =
;
òò= >
}
óó 	
public
õõ 
List
õõ 
<
õõ <
.Management_FFMM_consulta_cuentas_primeraResult
õõ B
>
õõB C'
GetConsultaFFMMCuentasUno
õõD ]
(
õõ] ^
DateTime
õõ^ f
fecha_inicial
õõg t
,
õõt u
DateTime
õõv ~
fecha_finalõõ Š
,õõŠ ‹
refõõŒ "
MessageResponseOBJõõ ¢
MsgResõõ£ ©
)õõ© ª
{
öö 	
return
÷÷ 
DACConsulta
÷÷ 
.
÷÷ '
GetConsultaFFMMCuentasUno
÷÷ 8
(
÷÷8 9
fecha_inicial
÷÷9 F
,
÷÷F G
fecha_final
÷÷H S
,
÷÷S T
ref
÷÷U X
MsgRes
÷÷Y _
)
÷÷_ `
;
÷÷` a
}
øø 	
public
ùù 
List
ùù 
<
ùù <
.Management_FFMM_consulta_cuentas_segundaResult
ùù B
>
ùùB C'
GetConsultaFFMMCuentasDos
ùùD ]
(
ùù] ^
DateTime
ùù^ f
fecha_inicial
ùùg t
,
ùùt u
DateTime
ùùv ~
fecha_finalùù Š
,ùùŠ ‹
refùùŒ "
MessageResponseOBJùù ¢
MsgResùù£ ©
)ùù© ª
{
úú 	
return
ûû 
DACConsulta
ûû 
.
ûû '
GetConsultaFFMMCuentasDos
ûû 8
(
ûû8 9
fecha_inicial
ûû9 F
,
ûûF G
fecha_final
ûûH S
,
ûûS T
ref
ûûU X
MsgRes
ûûY _
)
ûû_ `
;
ûû` a
}
üü 	
public
ıı 
List
ıı 
<
ıı <
.Management_FFMM_consulta_cuentas_terceraResult
ıı B
>
ııB C(
GetConsultaFFMMCuentasTres
ııD ^
(
ıı^ _
DateTime
ıı_ g
fecha_inicial
ııh u
,
ııu v
DateTime
ııw 
fecha_finalıı€ ‹
,ıı‹ Œ
refıı "
MessageResponseOBJıı‘ £
MsgResıı¤ ª
)ııª «
{
şş 	
return
ÿÿ 
DACConsulta
ÿÿ 
.
ÿÿ (
GetConsultaFFMMCuentasTres
ÿÿ 9
(
ÿÿ9 :
fecha_inicial
ÿÿ: G
,
ÿÿG H
fecha_final
ÿÿI T
,
ÿÿT U
ref
ÿÿV Y
MsgRes
ÿÿZ `
)
ÿÿ` a
;
ÿÿa b
}
€€ 	
public
 
List
 
<
 ;
-Management_FFMM_consulta_cuentas_cuartaResult
 A
>
A B*
GetConsultaFFMMCuentasCuatro
C _
(
_ `
DateTime
` h
fecha_inicial
i v
,
v w
DateTimex €
fecha_final Œ
,Œ 
ref ‘"
MessageResponseOBJ’ ¤
MsgRes¥ «
)« ¬
{
‚‚ 	
return
ƒƒ 
DACConsulta
ƒƒ 
.
ƒƒ *
GetConsultaFFMMCuentasCuatro
ƒƒ ;
(
ƒƒ; <
fecha_inicial
ƒƒ< I
,
ƒƒI J
fecha_final
ƒƒK V
,
ƒƒV W
ref
ƒƒX [
MsgRes
ƒƒ\ b
)
ƒƒb c
;
ƒƒc d
}
„„ 	
public
…… 
List
…… 
<
…… ;
-Management_FFMM_consulta_cuentas_quintaResult
…… A
>
……A B)
GetConsultaFFMMCuentasCinco
……C ^
(
……^ _
DateTime
……_ g
fecha_inicial
……h u
,
……u v
DateTime
……w 
fecha_final……€ ‹
,……‹ Œ
ref…… "
MessageResponseOBJ……‘ £
MsgRes……¤ ª
)……ª «
{
†† 	
return
‡‡ 
DACConsulta
‡‡ 
.
‡‡ )
GetConsultaFFMMCuentasCinco
‡‡ :
(
‡‡: ;
fecha_inicial
‡‡; H
,
‡‡H I
fecha_final
‡‡J U
,
‡‡U V
ref
‡‡W Z
MsgRes
‡‡[ a
)
‡‡a b
;
‡‡b c
}
ˆˆ 	
public
ŠŠ 
List
ŠŠ 
<
ŠŠ 
ref_auditor
ŠŠ 
>
ŠŠ  
listAuditor
ŠŠ! ,
(
ŠŠ, -
)
ŠŠ- .
{
‹‹ 	
return
ŒŒ 
DACConsulta
ŒŒ 
.
ŒŒ 
listAuditor
ŒŒ *
(
ŒŒ* +
)
ŒŒ+ ,
;
ŒŒ, -
}
 	
public
 
List
 
<
 A
3Management_FFMM_consulta_concurrencia_primeraResult
 G
>
G H,
GetConsultaFFMMConcurrenciaUno
I g
(
g h
DateTime
h p
fecha_inicial
q ~
,
~ 
DateTime€ ˆ
fecha_final‰ ”
,” •
ref– ™"
MessageResponseOBJš ¬
MsgRes­ ³
)³ ´
{
‘‘ 	
return
’’ 
DACConsulta
’’ 
.
’’ ,
GetConsultaFFMMConcurrenciaUno
’’ =
(
’’= >
fecha_inicial
’’> K
,
’’K L
fecha_final
’’M X
,
’’X Y
ref
’’Z ]
MsgRes
’’^ d
)
’’d e
;
’’e f
}
““ 	
public
”” 
List
”” 
<
”” A
3Management_FFMM_consulta_concurrencia_segundaResult
”” G
>
””G H,
GetConsultaFFMMConcurrenciaDos
””I g
(
””g h
DateTime
””h p
fecha_inicial
””q ~
,
””~ 
DateTime””€ ˆ
fecha_final””‰ ”
,””” •
ref””– ™"
MessageResponseOBJ””š ¬
MsgRes””­ ³
)””³ ´
{
•• 	
return
–– 
DACConsulta
–– 
.
–– ,
GetConsultaFFMMConcurrenciaDos
–– =
(
––= >
fecha_inicial
––> K
,
––K L
fecha_final
––M X
,
––X Y
ref
––Z ]
MsgRes
––^ d
)
––d e
;
––e f
}
—— 	
public
˜˜ 
List
˜˜ 
<
˜˜ A
3Management_FFMM_consulta_concurrencia_terceroResult
˜˜ G
>
˜˜G H0
"GetConsultaFFMMConcurrenciaTercero
˜˜I k
(
˜˜k l
DateTime
˜˜l t
fecha_inicial˜˜u ‚
,˜˜‚ ƒ
DateTime˜˜„ Œ
fecha_final˜˜ ˜
,˜˜˜ ™
ref˜˜š "
MessageResponseOBJ˜˜ °
MsgRes˜˜± ·
)˜˜· ¸
{
™™ 	
return
šš 
DACConsulta
šš 
.
šš 0
"GetConsultaFFMMConcurrenciaTercero
šš A
(
ššA B
fecha_inicial
ššB O
,
ššO P
fecha_final
ššQ \
,
šš\ ]
ref
šš^ a
MsgRes
ššb h
)
ššh i
;
šši j
}
›› 	
public
œœ 
List
œœ 
<
œœ @
2Management_FFMM_consulta_concurrencia_cuartoResult
œœ F
>
œœF G/
!GetConsultaFFMMConcurrenciaCuarto
œœH i
(
œœi j
DateTime
œœj r
fecha_inicialœœs €
,œœ€ 
DateTimeœœ‚ Š
fecha_finalœœ‹ –
,œœ– —
refœœ˜ ›"
MessageResponseOBJœœœ ®
MsgResœœ¯ µ
)œœµ ¶
{
 	
return
 
DACConsulta
 
.
 /
!GetConsultaFFMMConcurrenciaCuarto
 @
(
@ A
fecha_inicial
A N
,
N O
fecha_final
P [
,
[ \
ref
] `
MsgRes
a g
)
g h
;
h i
}
ŸŸ 	
public
§§ 
Int32
§§ 
CrearUsuairo
§§ !
(
§§! "
sis_usuario
§§" -

ObjUsuario
§§. 8
,
§§8 9
ref
§§: = 
MessageResponseOBJ
§§> P
MsgRes
§§Q W
)
§§W X
{
¨¨ 	
return
©© 

DACInserta
©© 
.
©© 
CrearUsuairo
©© *
(
©©* +

ObjUsuario
©©+ 5
,
©©5 6
ref
©©7 :
MsgRes
©©; A
)
©©A B
;
©©B C
}
ªª 	
public
¬¬ 
List
¬¬ 
<
¬¬ 
sis_usuario
¬¬ 
>
¬¬  
ValidaIngreso
¬¬! .
(
¬¬. /
sis_usuario
¬¬/ :

ObjUsuario
¬¬; E
,
¬¬E F
ref
¬¬G J 
MessageResponseOBJ
¬¬K ]
MsgRes
¬¬^ d
)
¬¬d e
{
­­ 	
return
®® 
DACConsulta
®® 
.
®® 
ValidaIngreso
®® ,
(
®®, -

ObjUsuario
®®- 7
,
®®7 8
ref
®®9 <
MsgRes
®®= C
)
®®C D
;
®®D E
}
¯¯ 	
public
±± 
sis_usuario
±±  
ValidaIngresoClave
±± -
(
±±- .
sis_usuario
±±. 9

ObjUsuario
±±: D
,
±±D E
ref
±±F I 
MessageResponseOBJ
±±J \
MsgRes
±±] c
)
±±c d
{
²² 	
return
³³ 
DACConsulta
³³ 
.
³³  
ValidaIngresoClave
³³ 1
(
³³1 2

ObjUsuario
³³2 <
,
³³< =
ref
³³> A
MsgRes
³³B H
)
³³H I
;
³³I J
}
´´ 	
public
¶¶ 
List
¶¶ 
<
¶¶ !
ManagmentMenuResult
¶¶ '
>
¶¶' (
ManagmentMenu
¶¶) 6
(
¶¶6 7
String
¶¶7 =

Strusuario
¶¶> H
,
¶¶H I
ref
¶¶J M 
MessageResponseOBJ
¶¶N `
MsgRes
¶¶a g
)
¶¶g h
{
·· 	
return
¸¸ 
DACConsulta
¸¸ 
.
¸¸ 
ManagmentMenu
¸¸ ,
(
¸¸, -

Strusuario
¸¸- 7
,
¸¸7 8
ref
¸¸9 <
MsgRes
¸¸= C
)
¸¸C D
;
¸¸D E
}
¹¹ 	
public
»» 
void
»» "
ActualizaContraseÃ±a
»» '
(
»»' (
sis_usuario
»»( 3

ObjUsuario
»»4 >
,
»»> ?
ref
»»@ C 
MessageResponseOBJ
»»D V
MsgRes
»»W ]
)
»»] ^
{
¼¼ 	
DACActualiza
½½ 
.
½½ "
ActualizaContraseÃ±a
½½ ,
(
½½, -

ObjUsuario
½½- 7
,
½½7 8
ref
½½9 <
MsgRes
½½= C
)
½½C D
;
½½D E
}
¾¾ 	
public
¿¿ 
void
¿¿ (
ActualizaContraseÃ±aOlvido
¿¿ -
(
¿¿- .
sis_usuario
¿¿. 9
Usuario
¿¿: A
,
¿¿A B
ref
¿¿C F 
MessageResponseOBJ
¿¿G Y
MsgRes
¿¿Z `
)
¿¿` a
{
ÀÀ 	
DACActualiza
ÁÁ 
.
ÁÁ (
ActualizaContraseÃ±aOlvido
ÁÁ 2
(
ÁÁ2 3
Usuario
ÁÁ3 :
,
ÁÁ: ;
ref
ÁÁ< ?
MsgRes
ÁÁ@ F
)
ÁÁF G
;
ÁÁG H
}
ÂÂ 	
public
ÃÃ 
void
ÃÃ $
ActualizaEstadoUsuario
ÃÃ *
(
ÃÃ* +
sis_usuario
ÃÃ+ 6

ObjUsuario
ÃÃ7 A
,
ÃÃA B
ref
ÃÃC F 
MessageResponseOBJ
ÃÃG Y
MsgRes
ÃÃZ `
)
ÃÃ` a
{
ÄÄ 	
DACActualiza
ÅÅ 
.
ÅÅ $
ActualizaEstadoUsuario
ÅÅ /
(
ÅÅ/ 0

ObjUsuario
ÅÅ0 :
,
ÅÅ: ;
ref
ÅÅ< ?
MsgRes
ÅÅ@ F
)
ÅÅF G
;
ÅÅG H
}
ÆÆ 	
public
ÈÈ 
List
ÈÈ 
<
ÈÈ 
sis_usuario
ÈÈ 
>
ÈÈ  
BuscaAuditorUsu
ÈÈ! 0
(
ÈÈ0 1
String
ÈÈ1 7

strUsuario
ÈÈ8 B
,
ÈÈB C
ref
ÈÈD G 
MessageResponseOBJ
ÈÈH Z
MsgRes
ÈÈ[ a
)
ÈÈa b
{
ÉÉ 	
return
ÊÊ 
DACConsulta
ÊÊ 
.
ÊÊ 
BuscaAuditorUsu
ÊÊ .
(
ÊÊ. /

strUsuario
ÊÊ/ 9
,
ÊÊ9 :
ref
ÊÊ; >
MsgRes
ÊÊ? E
)
ÊÊE F
;
ÊÊF G
}
ËË 	
public
ÍÍ 
List
ÍÍ 
<
ÍÍ 
sis_usuario
ÍÍ 
>
ÍÍ  
BuscaAuditorNom
ÍÍ! 0
(
ÍÍ0 1
String
ÍÍ1 7
	strNombre
ÍÍ8 A
,
ÍÍA B
ref
ÍÍC F 
MessageResponseOBJ
ÍÍG Y
MsgRes
ÍÍZ `
)
ÍÍ` a
{
ÎÎ 	
return
ÏÏ 
DACConsulta
ÏÏ 
.
ÏÏ 
BuscaAuditorNom
ÏÏ .
(
ÏÏ. /
	strNombre
ÏÏ/ 8
,
ÏÏ8 9
ref
ÏÏ: =
MsgRes
ÏÏ> D
)
ÏÏD E
;
ÏÏE F
}
ĞĞ 	
public
ÒÒ 
void
ÒÒ 
GestionUsuarios
ÒÒ #
(
ÒÒ# $
sis_usuario
ÒÒ$ /

ObjUsuario
ÒÒ0 :
,
ÒÒ: ;
ref
ÒÒ< ? 
MessageResponseOBJ
ÒÒ@ R
MsgRes
ÒÒS Y
)
ÒÒY Z
{
ÓÓ 	
DACConsulta
ÔÔ 
.
ÔÔ 
GestionUsuarios
ÔÔ '
(
ÔÔ' (

ObjUsuario
ÔÔ( 2
,
ÔÔ2 3
ref
ÔÔ4 7
MsgRes
ÔÔ8 >
)
ÔÔ> ?
;
ÔÔ? @
}
ÕÕ 	
public
×× 
DateTime
×× 
ManagmentHora
×× %
(
××% &
)
××& '
{
ØØ 	
return
ÙÙ 
DACConsulta
ÙÙ 
.
ÙÙ 
ManagmentHora
ÙÙ ,
(
ÙÙ, -
)
ÙÙ- .
;
ÙÙ. /
}
ÚÚ 	
public
ÜÜ 
List
ÜÜ 
<
ÜÜ 
vw_rol_usuarios
ÜÜ #
>
ÜÜ# $
Ref_rol_Usuario
ÜÜ% 4
(
ÜÜ4 5
)
ÜÜ5 6
{
İİ 	
return
ŞŞ 
DACComonClass
ŞŞ  
.
ŞŞ  !
Ref_rol_Usuario
ŞŞ! 0
(
ŞŞ0 1
)
ŞŞ1 2
;
ŞŞ2 3
}
ßß 	
public
àà 
List
àà 
<
àà 
vw_cargo_usuario
àà $
>
àà$ %
Ref_cargo_Usuario
àà& 7
(
àà7 8
)
àà8 9
{
áá 	
return
ââ 
DACComonClass
ââ  
.
ââ  !
Ref_cargo_Usuario
ââ! 2
(
ââ2 3
)
ââ3 4
;
ââ4 5
}
ää 	
public
åå 
List
åå 
<
åå 
sis_usuario
åå 
>
åå  
BuscaAuditorDoc
åå! 0
(
åå0 1
String
åå1 7

strUsuario
åå8 B
,
ååB C
ref
ååD G 
MessageResponseOBJ
ååH Z
MsgRes
åå[ a
)
ååa b
{
ææ 	
return
çç 
DACConsulta
çç 
.
çç 
BuscaAuditorDoc
çç .
(
çç. /

strUsuario
çç/ 9
,
çç9 :
ref
çç; >
MsgRes
çç? E
)
ççE F
;
ççF G
}
èè 	
public
éé 
List
éé 
<
éé %
vw_pruebas_img_usuarios
éé +
>
éé+ ,
BuscaAuditorImg
éé- <
(
éé< =
String
éé= C

strUsuario
ééD N
,
ééN O
ref
ééP S 
MessageResponseOBJ
ééT f
MsgRes
éég m
)
éém n
{
êê 	
return
ëë 
DACConsulta
ëë 
.
ëë 
BuscaAuditorImg
ëë .
(
ëë. /

strUsuario
ëë/ 9
,
ëë9 :
ref
ëë; >
MsgRes
ëë? E
)
ëëE F
;
ëëF G
}
ìì 	
public
îî 
List
îî 
<
îî 
sis_usuario
îî 
>
îî  !
BuscaAuditorUsuSami
îî! 4
(
îî4 5
String
îî5 ;

strUsuario
îî< F
,
îîF G
ref
îîH K 
MessageResponseOBJ
îîL ^
MsgRes
îî_ e
)
îîe f
{
ïï 	
return
ğğ 
DACConsulta
ğğ 
.
ğğ !
BuscaAuditorUsuSami
ğğ 2
(
ğğ2 3

strUsuario
ğğ3 =
,
ğğ= >
ref
ğğ? B
MsgRes
ğğC I
)
ğğI J
;
ğğJ K
}
ññ 	
public
óó 
List
óó 
<
óó 
sis_usuario
óó 
>
óó  
GetUsuarios
óó! ,
(
óó, -
)
óó- .
{
ôô 	
return
õõ 
DACConsulta
õõ 
.
õõ 
GetUsuarios
õõ *
(
õõ* +
)
õõ+ ,
;
õõ, -
}
öö 	
public
øø 
List
øø 
<
øø C
5management_sis_usuarios_controlActividadesCensoResult
øø I
>
øøI J
GetUsuariosCenso
øøK [
(
øø[ \
)
øø\ ]
{
ùù 	
return
úú 
DACConsulta
úú 
.
úú 
GetUsuariosCenso
úú /
(
úú/ 0
)
úú0 1
;
úú1 2
}
ûû 	
public
ıı 
Int32
ıı '
InsertarLogGestionUusario
ıı .
(
ıı. /"
log_gestion_usuarios
ıı/ C
log
ııD G
,
ııG H
ref
ııI L 
MessageResponseOBJ
ııM _
MsgRes
ıı` f
)
ııf g
{
şş 	
return
ÿÿ 

DACInserta
ÿÿ 
.
ÿÿ '
InsertarLogGestionUusario
ÿÿ 7
(
ÿÿ7 8
log
ÿÿ8 ;
,
ÿÿ; <
ref
ÿÿ= @
MsgRes
ÿÿA G
)
ÿÿG H
;
ÿÿH I
}
€€ 	
public
ƒƒ 
void
ƒƒ #
ActualizaClaveUsuario
ƒƒ )
(
ƒƒ) *
sis_usuario
ƒƒ* 5
OBJ
ƒƒ6 9
,
ƒƒ9 :
ref
ƒƒ; > 
MessageResponseOBJ
ƒƒ? Q
MsgRes
ƒƒR X
)
ƒƒX Y
{
„„ 	
DACActualiza
…… 
.
…… #
ActualizaClaveUsuario
…… .
(
……. /
OBJ
……/ 2
,
……2 3
ref
……4 7
MsgRes
……8 >
)
……> ?
;
……? @
}
†† 	
public
 
void
 %
InsertarLogInicioSesion
 +
(
+ ,
Log_InicioSession
, =
obj
> A
,
A B
ref
C F 
MessageResponseOBJ
G Y
MsgRes
Z `
)
` a
{
 	

DACInserta
‘‘ 
.
‘‘ %
InsertarLogInicioSesion
‘‘ .
(
‘‘. /
obj
‘‘/ 2
,
‘‘2 3
ref
‘‘4 7
MsgRes
‘‘8 >
)
‘‘> ?
;
‘‘? @
}
’’ 	
public
˜˜ 
List
˜˜ 
<
˜˜ 
vw_datos_censo
˜˜ "
>
˜˜" #
CensoDocumento
˜˜$ 2
(
˜˜2 3
String
˜˜3 9
	Documento
˜˜: C
,
˜˜C D
ref
˜˜E H 
MessageResponseOBJ
˜˜I [
MsgRes
˜˜\ b
)
˜˜b c
{
™™ 	
return
šš 
DACConsulta
šš 
.
šš 
CensoDocumento
šš -
(
šš- .
	Documento
šš. 7
,
šš7 8
ref
šš9 <
MsgRes
šš= C
)
ššC D
;
ššD E
}
›› 	
public
œœ 

ecop_censo
œœ ,
ConsultaCensoIdentificacionPac
œœ 8
(
œœ8 9
string
œœ9 ?

idPaciente
œœ@ J
)
œœJ K
{
 	
return
 
DACConsulta
 
.
 ,
ConsultaCensoIdentificacionPac
 =
(
= >

idPaciente
> H
)
H I
;
I J
}
ŸŸ 	
public
   4
&management_datosPaciente_alertasResult
   5
DatosPaciente
  6 C
(
  C D
int
  D G
idConcurrencia
  H V
)
  V W
{
¡¡ 	
return
¢¢ 
DACConsulta
¢¢ 
.
¢¢ 
DatosPaciente
¢¢ ,
(
¢¢, -
idConcurrencia
¢¢- ;
)
¢¢; <
;
¢¢< =
}
££ 	
public
¤¤ 
List
¤¤ 
<
¤¤ 
vw_datos_censo
¤¤ "
>
¤¤" #
CensoFacturas
¤¤$ 1
(
¤¤1 2
ref
¤¤2 5 
MessageResponseOBJ
¤¤6 H
MsgRes
¤¤I O
)
¤¤O P
{
¥¥ 	
return
¦¦ 
DACConsulta
¦¦ 
.
¦¦ 
CensoFacturas
¦¦ ,
(
¦¦, -
ref
¦¦- 0
MsgRes
¦¦1 7
)
¦¦7 8
;
¦¦8 9
}
§§ 	
public
©© 
List
©© 
<
©© *
management_datos_censoResult
©© 0
>
©©0 1$
CensoFacturasDetallado
©©2 H
(
©©H I
string
©©I O
	documento
©©P Y
,
©©Y Z
string
©©[ a
nombre
©©b h
,
©©h i
DateTime
©©j r
?
©©r s
fechaEgresoConcu©©t „
,©©„ …
DateTime©©† 
?©©  
fechaEgresoCenso©©  
,©©  ¡
ref©©¢ ¥"
MessageResponseOBJ©©¦ ¸
MsgRes©©¹ ¿
)©©¿ À
{
ªª 	
return
«« 
DACConsulta
«« 
.
«« $
CensoFacturasDetallado
«« 5
(
««5 6
	documento
««6 ?
,
««? @
nombre
««A G
,
««G H
fechaEgresoConcu
««I Y
,
««Y Z
fechaEgresoCenso
««[ k
,
««k l
ref
««m p
MsgRes
««q w
)
««w x
;
««x y
}
¬¬ 	
public
®® 
Int32
®® 
InsertarCenso
®® "
(
®®" #

ecop_censo
®®# -
OBJ
®®. 1
,
®®1 2
ref
®®3 6 
MessageResponseOBJ
®®7 I
MsgRes
®®J P
)
®®P Q
{
¯¯ 	
return
°° 

DACInserta
°° 
.
°° 
InsertarCenso
°° +
(
°°+ ,
OBJ
°°, /
,
°°/ 0
ref
°°1 4
MsgRes
°°5 ;
)
°°; <
;
°°< =
}
±± 	
public
³³ 
List
³³ 
<
³³ 
vw_datos_censo
³³ "
>
³³" #
CensoId
³³$ +
(
³³+ ,
Int32
³³, 1
Id
³³2 4
,
³³4 5
ref
³³6 9 
MessageResponseOBJ
³³: L
MsgRes
³³M S
)
³³S T
{
´´ 	
return
µµ 
DACConsulta
µµ 
.
µµ 
CensoId
µµ &
(
µµ& '
Id
µµ' )
,
µµ) *
ref
µµ+ .
MsgRes
µµ/ 5
)
µµ5 6
;
µµ6 7
}
¶¶ 	
public
¸¸ 
List
¸¸ 
<
¸¸ 

ecop_censo
¸¸ 
>
¸¸ 

GetCensoId
¸¸  *
(
¸¸* +
Int32
¸¸+ 0
Id
¸¸1 3
,
¸¸3 4
ref
¸¸5 8 
MessageResponseOBJ
¸¸9 K
MsgRes
¸¸L R
)
¸¸R S
{
¹¹ 	
return
ºº 
DACConsulta
ºº 
.
ºº 

GetCensoId
ºº )
(
ºº) *
Id
ºº* ,
,
ºº, -
ref
ºº. 1
MsgRes
ºº2 8
)
ºº8 9
;
ºº9 :
}
»» 	
public
½½ 
void
½½ 
ActualizarCenso
½½ #
(
½½# $

ecop_censo
½½$ . 
ActualizaSiniestro
½½/ A
,
½½A B
ref
½½C F 
MessageResponseOBJ
½½G Y
MsgRes
½½Z `
)
½½` a
{
¾¾ 	
DACActualiza
¿¿ 
.
¿¿ 
ActualizarCenso
¿¿ (
(
¿¿( ) 
ActualizaSiniestro
¿¿) ;
,
¿¿; <
ref
¿¿= @
MsgRes
¿¿A G
)
¿¿G H
;
¿¿H I
}
ÀÀ 	
public
ÂÂ 

ecop_censo
ÂÂ "
ConsultaCensoIdCenso
ÂÂ .
(
ÂÂ. /
int
ÂÂ/ 2
?
ÂÂ2 3
idCenso
ÂÂ4 ;
)
ÂÂ; <
{
ÃÃ 	
return
ÄÄ 
DACConsulta
ÄÄ 
.
ÄÄ "
ConsultaCensoIdCenso
ÄÄ 3
(
ÄÄ3 4
idCenso
ÄÄ4 ;
)
ÄÄ; <
;
ÄÄ< =
}
ÅÅ 	
public
ÇÇ 
List
ÇÇ 
<
ÇÇ +
vw_censo_control_concurrencia
ÇÇ 1
>
ÇÇ1 2'
CensoConcurrenciasTotales
ÇÇ3 L
(
ÇÇL M
)
ÇÇM N
{
ÈÈ 	
return
ÉÉ 
DACConsulta
ÉÉ 
.
ÉÉ '
CensoConcurrenciasTotales
ÉÉ 8
(
ÉÉ8 9
)
ÉÉ9 :
;
ÉÉ: ;
}
ÊÊ 	
public
ÌÌ 
List
ÌÌ 
<
ÌÌ D
6management_censo_control_concurrencia_optimizadaResult
ÌÌ J
>
ÌÌJ K1
#CensoConcurrenciasTotalesOptimizada
ÌÌL o
(
ÌÌo p
int
ÌÌp s
?
ÌÌs t
regional
ÌÌu }
,
ÌÌ} ~
stringÌÌ …
	documentoÌÌ† 
,ÌÌ 
stringÌÌ‘ —
nombreÌÌ˜ 
)ÌÌ Ÿ
{
ÍÍ 	
return
ÎÎ 
DACConsulta
ÎÎ 
.
ÎÎ 1
#CensoConcurrenciasTotalesOptimizada
ÎÎ B
(
ÎÎB C
regional
ÎÎC K
,
ÎÎK L
	documento
ÎÎM V
,
ÎÎV W
nombre
ÎÎX ^
)
ÎÎ^ _
;
ÎÎ_ `
}
ÏÏ 	
public
ÑÑ 
List
ÑÑ 
<
ÑÑ -
vw_censo_control_cuentasMedicas
ÑÑ 3
>
ÑÑ3 4(
CensoCuentasMedicasTotales
ÑÑ5 O
(
ÑÑO P
)
ÑÑP Q
{
ÒÒ 	
return
ÓÓ 
DACConsulta
ÓÓ 
.
ÓÓ (
CensoCuentasMedicasTotales
ÓÓ 9
(
ÓÓ9 :
)
ÓÓ: ;
;
ÓÓ; <
}
ÔÔ 	
public
ÕÕ 
List
ÕÕ 
<
ÕÕ F
8management_censo_control_cuentasMedicas_optimizadaResult
ÕÕ L
>
ÕÕL M2
$CensoCuentasMedicasTotalesOptimizada
ÕÕN r
(
ÕÕr s
int
ÕÕs v
?
ÕÕv w
regionalÕÕx €
,ÕÕ€ 
stringÕÕ‚ ˆ
	documentoÕÕ‰ ’
,ÕÕ’ “
stringÕÕ” š
nombreÕÕ› ¡
)ÕÕ¡ ¢
{
ÖÖ 	
return
×× 
DACConsulta
×× 
.
×× 2
$CensoCuentasMedicasTotalesOptimizada
×× C
(
××C D
regional
××D L
,
××L M
	documento
××N W
,
××W X
nombre
××Y _
)
××_ `
;
××` a
}
ØØ 	
public
ÚÚ 
List
ÚÚ 
<
ÚÚ &
vw_censo_control_visitas
ÚÚ ,
>
ÚÚ, -!
CensoVisitasTotales
ÚÚ. A
(
ÚÚA B
)
ÚÚB C
{
ÛÛ 	
return
ÜÜ 
DACConsulta
ÜÜ 
.
ÜÜ !
CensoVisitasTotales
ÜÜ 2
(
ÜÜ2 3
)
ÜÜ3 4
;
ÜÜ4 5
}
İİ 	
public
ßß 
List
ßß 
<
ßß ?
1management_censo_control_visitas_optimizadaResult
ßß E
>
ßßE F+
CensoVisitasTotalesOptimizada
ßßG d
(
ßßd e
int
ßße h
?
ßßh i
regional
ßßj r
,
ßßr s
string
ßßt z
	documentoßß{ „
,ßß„ …
stringßß† Œ
nombreßß “
)ßß“ ”
{
àà 	
return
áá 
DACConsulta
áá 
.
áá +
CensoVisitasTotalesOptimizada
áá <
(
áá< =
regional
áá= E
,
ááE F
	documento
ááG P
,
ááP Q
nombre
ááR X
)
ááX Y
;
ááY Z
}
ââ 	
public
ää 
List
ää 
<
ää N
@management_sis_usuarios_controlActividadesCenso_optimizadaResult
ää T
>
ääT U(
GetUsuariosCensoOptimizada
ääV p
(
ääp q
int
ääq t
?
äät u
regional
ääv ~
,
ää~ 
stringää€ †
	documentoää‡ 
,ää ‘
stringää’ ˜
nombreää™ Ÿ
)ääŸ  
{
åå 	
return
ææ 
DACConsulta
ææ 
.
ææ (
GetUsuariosCensoOptimizada
ææ 9
(
ææ9 :
regional
ææ: B
,
ææB C
	documento
ææD M
,
ææM N
nombre
ææO U
)
ææU V
;
ææV W
}
çç 	
public
ëë 
List
ëë 
<
ëë *
ref_ecop_censo_tiposConsulta
ëë 0
>
ëë0 1.
 CensoConsultaReportesActividades
ëë2 R
(
ëëR S
)
ëëS T
{
ìì 	
return
íí 
DACConsulta
íí 
.
íí .
 CensoConsultaReportesActividades
íí ?
(
íí? @
)
íí@ A
;
ííA B
}
îî 	
public
ğğ 
Int32
ğğ '
InsertarLogCensoReingreso
ğğ .
(
ğğ. /"
log_censo_reingresos
ğğ/ C
OBJ
ğğD G
,
ğğG H
ref
ğğI L 
MessageResponseOBJ
ğğM _
MsgRes
ğğ` f
)
ğğf g
{
ññ 	
return
òò 

DACInserta
òò 
.
òò '
InsertarLogCensoReingreso
òò 7
(
òò7 8
OBJ
òò8 ;
,
òò; <
ref
òò= @
MsgRes
òòA G
)
òòG H
;
òòH I
}
óó 	
public
õõ 
void
õõ (
ActualizarFechaEgresoCenso
õõ .
(
õõ. /

ecop_censo
õõ/ 9
OBJ
õõ: =
,
õõ= >
ref
õõ? B 
MessageResponseOBJ
õõC U
MsgRes
õõV \
)
õõ\ ]
{
öö 	
DACActualiza
÷÷ 
.
÷÷ (
ActualizarFechaEgresoCenso
÷÷ 3
(
÷÷3 4
OBJ
÷÷4 7
,
÷÷7 8
ref
÷÷9 <
MsgRes
÷÷= C
)
÷÷C D
;
÷÷D E
}
øø 	
public
úú 
int
úú ,
ActualizarCensoSacarDelTablero
úú 1
(
úú1 2

ecop_censo
úú2 <
censo
úú= B
)
úúB C
{
ûû 	
return
üü 
DACActualiza
üü 
.
üü  ,
ActualizarCensoSacarDelTablero
üü  >
(
üü> ?
censo
üü? D
)
üüD E
;
üüE F
}
ıı 	
public
ÿÿ 
void
ÿÿ %
ActualizarLeEgresoCenso
ÿÿ +
(
ÿÿ+ ,

ecop_censo
ÿÿ, 6
OBJ
ÿÿ7 :
,
ÿÿ: ;
ref
ÿÿ< ? 
MessageResponseOBJ
ÿÿ@ R
MsgRes
ÿÿS Y
)
ÿÿY Z
{
€	€	 	
DACActualiza
		 
.
		 %
ActualizarLeEgresoCenso
		 0
(
		0 1
OBJ
		1 4
,
		4 5
ref
		6 9
MsgRes
		: @
)
		@ A
;
		A B
}
‚	‚	 	
public
„	„	 
void
„	„	 #
ActualizarEgresoConcu
„	„	 )
(
„	„	) *
ecop_concurrencia
„	„	* ;
OBJ
„	„	< ?
,
„	„	? @
ref
„	„	A D 
MessageResponseOBJ
„	„	E W
MsgRes
„	„	X ^
)
„	„	^ _
{
…	…	 	
DACActualiza
†	†	 
.
†	†	 #
ActualizarEgresoConcu
†	†	 .
(
†	†	. /
OBJ
†	†	/ 2
,
†	†	2 3
ref
†	†	4 7
MsgRes
†	†	8 >
)
†	†	> ?
;
†	†	? @
}
‡	‡	 	
public
‰	‰	 
void
‰	‰	 #
ActualizarCensoEgreso
‰	‰	 )
(
‰	‰	) *

ecop_censo
‰	‰	* 4 
ActualizaSiniestro
‰	‰	5 G
,
‰	‰	G H
ref
‰	‰	I L 
MessageResponseOBJ
‰	‰	M _
MsgRes
‰	‰	` f
)
‰	‰	f g
{
Š	Š	 	
DACActualiza
‹	‹	 
.
‹	‹	 #
ActualizarCensoEgreso
‹	‹	 .
(
‹	‹	. / 
ActualizaSiniestro
‹	‹	/ A
,
‹	‹	A B
ref
‹	‹	C F
MsgRes
‹	‹	G M
)
‹	‹	M N
;
‹	‹	N O
}
Œ	Œ	 	
public
		 
void
		 %
ActualizarCensoEgresoOK
		 +
(
		+ ,

ecop_censo
		, 6 
ActualizaSiniestro
		7 I
,
		I J
ref
		K N 
MessageResponseOBJ
		O a
MsgRes
		b h
)
		h i
{
		 	
DACActualiza
		 
.
		 %
ActualizarCensoEgresoOK
		 0
(
		0 1 
ActualizaSiniestro
		1 C
,
		C D
ref
		E H
MsgRes
		I O
)
		O P
;
		P Q
}
‘	‘	 	
public
“	“	 
void
“	“	 +
ActualizaEgresoConcurrenciaOk
“	“	 1
(
“	“	1 2
ecop_concurrencia
“	“	2 C
ObjConcurrencia
“	“	D S
,
“	“	S T
ref
“	“	U X 
MessageResponseOBJ
“	“	Y k
MsgRes
“	“	l r
)
“	“	r s
{
”	”	 	
DACActualiza
•	•	 
.
•	•	 +
ActualizaEgresoConcurrenciaOk
•	•	 6
(
•	•	6 7
ObjConcurrencia
•	•	7 F
,
•	•	F G
ref
•	•	H K
MsgRes
•	•	L R
)
•	•	R S
;
•	•	S T
}
–	–	 	
public
˜	˜	 
List
˜	˜	 
<
˜	˜	 #
VW_base_beneficiarios
˜	˜	 )
>
˜	˜	) *$
BeneficiariosDocumento
˜	˜	+ A
(
˜	˜	A B
String
˜	˜	B H
	Documento
˜	˜	I R
,
˜	˜	R S
ref
˜	˜	T W 
MessageResponseOBJ
˜	˜	X j
MsgRes
˜	˜	k q
)
˜	˜	q r
{
™	™	 	
return
š	š	 
DACConsulta
š	š	 
.
š	š	 $
BeneficiariosDocumento
š	š	 5
(
š	š	5 6
	Documento
š	š	6 ?
,
š	š	? @
ref
š	š	A D
MsgRes
š	š	E K
)
š	š	K L
;
š	š	L M
}
›	›	 	
public
		 
List
		 
<
		 '
vw_tablero_levante_egreso
		 -
>
		- .
GetlevanteEgreso
		/ ?
(
		? @
String
		@ F
	Documento
		G P
,
		P Q
ref
		R U 
MessageResponseOBJ
		V h
MsgRes
		i o
)
		o p
{
		 	
return
Ÿ	Ÿ	 
DACConsulta
Ÿ	Ÿ	 
.
Ÿ	Ÿ	 
GetlevanteEgreso
Ÿ	Ÿ	 /
(
Ÿ	Ÿ	/ 0
	Documento
Ÿ	Ÿ	0 9
,
Ÿ	Ÿ	9 :
ref
Ÿ	Ÿ	; >
MsgRes
Ÿ	Ÿ	? E
)
Ÿ	Ÿ	E F
;
Ÿ	Ÿ	F G
}
 	 	 	
public
£	£	 
List
£	£	 
<
£	£	 #
VW_base_beneficiarios
£	£	 )
>
£	£	) *
GetBeneficiarios
£	£	+ ;
(
£	£	; <
string
£	£	< B
term
£	£	C G
,
£	£	G H
ref
£	£	I L 
MessageResponseOBJ
£	£	M _
MsgRes
£	£	` f
)
£	£	f g
{
¤	¤	 	
return
¥	¥	 
DACConsulta
¥	¥	 
.
¥	¥	 
GetBeneficiarios
¥	¥	 /
(
¥	¥	/ 0
term
¥	¥	0 4
,
¥	¥	4 5
ref
¥	¥	6 9
MsgRes
¥	¥	: @
)
¥	¥	@ A
;
¥	¥	A B
}
¦	¦	 	
public
¨	¨	 
List
¨	¨	 
<
¨	¨	  
base_beneficiarios
¨	¨	 &
>
¨	¨	& '$
GetUltimoBeneficiarios
¨	¨	( >
(
¨	¨	> ?
string
¨	¨	? E
	documento
¨	¨	F O
,
¨	¨	O P
string
¨	¨	Q W
tipo
¨	¨	X \
,
¨	¨	\ ]
ref
¨	¨	^ a 
MessageResponseOBJ
¨	¨	b t
MsgRes
¨	¨	u {
)
¨	¨	{ |
{
©	©	 	
return
ª	ª	 
DACConsulta
ª	ª	 
.
ª	ª	 $
GetUltimoBeneficiarios
ª	ª	 5
(
ª	ª	5 6
	documento
ª	ª	6 ?
,
ª	ª	? @
tipo
ª	ª	A E
,
ª	ª	E F
ref
ª	ª	G J
MsgRes
ª	ª	K Q
)
ª	ª	Q R
;
ª	ª	R S
}
«	«	 	
public
®	®	 
List
®	®	 
<
®	®	 
vw_consulta_censo
®	®	 %
>
®	®	% &
ConsultaCenso
®	®	' 4
(
®	®	4 5
ref
®	®	5 8 
MessageResponseOBJ
®	®	9 K
MsgRes
®	®	L R
)
®	®	R S
{
¯	¯	 	
return
°	°	 
DACConsulta
°	°	 
.
°	°	 
ConsultaCenso
°	°	 ,
(
°	°	, -
ref
°	°	- 0
MsgRes
°	°	1 7
)
°	°	7 8
;
°	°	8 9
}
±	±	 	
public
²	²	 
List
²	²	 
<
²	²	 
vw_consulta_ecop
²	²	 $
>
²	²	$ %'
ConsultaCensoConcurrencia
²	²	& ?
(
²	²	? @
ref
²	²	@ C 
MessageResponseOBJ
²	²	D V
MsgRes
²	²	W ]
)
²	²	] ^
{
³	³	 	
return
´	´	 
DACConsulta
´	´	 
.
´	´	 '
ConsultaCensoConcurrencia
´	´	 8
(
´	´	8 9
ref
´	´	9 <
MsgRes
´	´	= C
)
´	´	C D
;
´	´	D E
}
µ	µ	 	
public
¿	¿	 
List
¿	¿	 
<
¿	¿	 ,
Management_Consulta_EcopResult
¿	¿	 2
>
¿	¿	2 3)
ConsultaCensoConcurrenciaII
¿	¿	4 O
(
¿	¿	O P
int
¿	¿	P S
tipo
¿	¿	T X
,
¿	¿	X Y
int
¿	¿	Z ]
?
¿	¿	] ^
regional
¿	¿	_ g
,
¿	¿	g h
string
¿	¿	i o
	documento
¿	¿	p y
,
¿	¿	y z
DateTime¿	¿	{ ƒ
?¿	¿	ƒ „
fechaInicio¿	¿	… 
,¿	¿	 ‘
DateTime¿	¿	’ š
?¿	¿	š ›

fechaFinal¿	¿	œ ¦
,¿	¿	¦ §
ref¿	¿	¨ «"
MessageResponseOBJ¿	¿	¬ ¾
MsgRes¿	¿	¿ Å
)¿	¿	Å Æ
{
À	À	 	
return
Á	Á	 
DACConsulta
Á	Á	 
.
Á	Á	 )
ConsultaCensoConcurrenciaII
Á	Á	 :
(
Á	Á	: ;
tipo
Á	Á	; ?
,
Á	Á	? @
regional
Á	Á	A I
,
Á	Á	I J
	documento
Á	Á	K T
,
Á	Á	T U
fechaInicio
Á	Á	V a
,
Á	Á	a b

fechaFinal
Á	Á	c m
,
Á	Á	m n
ref
Á	Á	o r
MsgRes
Á	Á	s y
)
Á	Á	y z
;
Á	Á	z {
}
Â	Â	 	
public
Ä	Ä	 
List
Ä	Ä	 
<
Ä	Ä	 -
Management_Consulta_Ecop2Result
Ä	Ä	 3
>
Ä	Ä	3 4*
ConsultaCensoConcurrenciaII2
Ä	Ä	5 Q
(
Ä	Ä	Q R
int
Ä	Ä	R U
tipo
Ä	Ä	V Z
,
Ä	Ä	Z [
int
Ä	Ä	\ _
?
Ä	Ä	_ `
regional
Ä	Ä	a i
,
Ä	Ä	i j
string
Ä	Ä	k q
	documento
Ä	Ä	r {
,
Ä	Ä	{ |
DateTimeÄ	Ä	} …
?Ä	Ä	… †
fechaInicioÄ	Ä	‡ ’
,Ä	Ä	’ “
DateTimeÄ	Ä	” œ
?Ä	Ä	œ 

fechaFinalÄ	Ä	 ¨
,Ä	Ä	¨ ©
refÄ	Ä	ª ­"
MessageResponseOBJÄ	Ä	® À
MsgResÄ	Ä	Á Ç
)Ä	Ä	Ç È
{
Å	Å	 	
return
Æ	Æ	 
DACConsulta
Æ	Æ	 
.
Æ	Æ	 *
ConsultaCensoConcurrenciaII2
Æ	Æ	 ;
(
Æ	Æ	; <
tipo
Æ	Æ	< @
,
Æ	Æ	@ A
regional
Æ	Æ	B J
,
Æ	Æ	J K
	documento
Æ	Æ	L U
,
Æ	Æ	U V
fechaInicio
Æ	Æ	W b
,
Æ	Æ	b c

fechaFinal
Æ	Æ	d n
,
Æ	Æ	n o
ref
Æ	Æ	p s
MsgRes
Æ	Æ	t z
)
Æ	Æ	z {
;
Æ	Æ	{ |
}
Ç	Ç	 	
public
É	É	 
List
É	É	 
<
É	É	 *
vw_consulta_pacientesActivos
É	É	 0
>
É	É	0 1&
ConsultaPacientesActivos
É	É	2 J
(
É	É	J K
)
É	É	K L
{
Ê	Ê	 	
return
Ë	Ë	 
DACConsulta
Ë	Ë	 
.
Ë	Ë	 &
ConsultaPacientesActivos
Ë	Ë	 7
(
Ë	Ë	7 8
)
Ë	Ë	8 9
;
Ë	Ë	9 :
}
Ì	Ì	 	
public
Î	Î	 
void
Î	Î	 
CensoEgreso
Î	Î	 
(
Î	Î	  

ecop_censo
Î	Î	  * 
ActualizaSiniestro
Î	Î	+ =
,
Î	Î	= >
ref
Î	Î	? B 
MessageResponseOBJ
Î	Î	C U
MsgRes
Î	Î	V \
)
Î	Î	\ ]
{
Ï	Ï	 	
DACActualiza
Ğ	Ğ	 
.
Ğ	Ğ	 
CensoEgreso
Ğ	Ğ	 $
(
Ğ	Ğ	$ % 
ActualizaSiniestro
Ğ	Ğ	% 7
,
Ğ	Ğ	7 8
ref
Ğ	Ğ	9 <
MsgRes
Ğ	Ğ	= C
)
Ğ	Ğ	C D
;
Ğ	Ğ	D E
}
Ñ	Ñ	 	
public
Ó	Ó	 
void
Ó	Ó	 #
ActualizarEgresoCenso
Ó	Ó	 )
(
Ó	Ó	) *

ecop_censo
Ó	Ó	* 4 
ActualizaSiniestro
Ó	Ó	5 G
,
Ó	Ó	G H
ref
Ó	Ó	I L 
MessageResponseOBJ
Ó	Ó	M _
MsgRes
Ó	Ó	` f
)
Ó	Ó	f g
{
Ô	Ô	 	
DACActualiza
Õ	Õ	 
.
Õ	Õ	 #
ActualizarEgresoCenso
Õ	Õ	 .
(
Õ	Õ	. / 
ActualizaSiniestro
Õ	Õ	/ A
,
Õ	Õ	A B
ref
Õ	Õ	C F
MsgRes
Õ	Õ	G M
)
Õ	Õ	M N
;
Õ	Õ	N O
}
Ö	Ö	 	
public
Ø	Ø	 
void
Ø	Ø	 -
ActualizarFechaEgresoConcucenso
Ø	Ø	 3
(
Ø	Ø	3 4

ecop_censo
Ø	Ø	4 >
censo
Ø	Ø	? D
,
Ø	Ø	D E
int
Ø	Ø	F I
idconcu
Ø	Ø	J Q
,
Ø	Ø	Q R
ref
Ø	Ø	S V 
MessageResponseOBJ
Ø	Ø	W i
MsgRes
Ø	Ø	j p
)
Ø	Ø	p q
{
Ù	Ù	 	
DACActualiza
Ú	Ú	 
.
Ú	Ú	 -
ActualizarFechaEgresoConcucenso
Ú	Ú	 8
(
Ú	Ú	8 9
censo
Ú	Ú	9 >
,
Ú	Ú	> ?
idconcu
Ú	Ú	@ G
,
Ú	Ú	G H
ref
Ú	Ú	I L
MsgRes
Ú	Ú	M S
)
Ú	Ú	S T
;
Ú	Ú	T U
}
Û	Û	 	
public
İ	İ	 
List
İ	İ	 
<
İ	İ	 /
!management_egresBuscar_megaResult
İ	İ	 5
>
İ	İ	5 6
BuscarMegaEgreso
İ	İ	7 G
(
İ	İ	G H
string
İ	İ	H N
	documento
İ	İ	O X
)
İ	İ	X Y
{
Ş	Ş	 	
return
ß	ß	 
DACConsulta
ß	ß	 
.
ß	ß	 
BuscarMegaEgreso
ß	ß	 /
(
ß	ß	/ 0
	documento
ß	ß	0 9
)
ß	ß	9 :
;
ß	ß	: ;
}
à	à	 	
public
â	â	 
List
â	â	 
<
â	â	 %
ref_censo_caso_especial
â	â	 +
>
â	â	+ ,'
listaCensoCasosEspeciales
â	â	- F
(
â	â	F G
)
â	â	G H
{
ã	ã	 	
return
ä	ä	 
DACConsulta
ä	ä	 
.
ä	ä	 '
listaCensoCasosEspeciales
ä	ä	 8
(
ä	ä	8 9
)
ä	ä	9 :
;
ä	ä	: ;
}
å	å	 	
public
è	è	 
List
è	è	 
<
è	è	 "
ref_censo_linea_pago
è	è	 (
>
è	è	( )"
listaCensoLineasPago
è	è	* >
(
è	è	> ?
)
è	è	? @
{
é	é	 	
return
ê	ê	 
DACConsulta
ê	ê	 
.
ê	ê	 "
listaCensoLineasPago
ê	ê	 3
(
ê	ê	3 4
)
ê	ê	4 5
;
ê	ê	5 6
}
ë	ë	 	
public
í	í	 5
'management_censo_ultimaHabitacionResult
í	í	 6
datosEgreso
í	í	7 B
(
í	í	B C
int
í	í	C F
?
í	í	F G
idCenso
í	í	H O
)
í	í	O P
{
î	î	 	
return
ï	ï	 
DACConsulta
ï	ï	 
.
ï	ï	 
datosEgreso
ï	ï	 *
(
ï	ï	* +
idCenso
ï	ï	+ 2
)
ï	ï	2 3
;
ï	ï	3 4
}
ğ	ğ	 	
public
õ	õ	 
List
õ	õ	 
<
õ	õ	 
ecop_concurrencia
õ	õ	 %
>
õ	õ	% &)
ConsultaAfiliadoConcurrenia
õ	õ	' B
(
õ	õ	B C
ecop_concurrencia
õ	õ	C T
ObjAfiliado
õ	õ	U `
,
õ	õ	` a
ref
õ	õ	b e 
MessageResponseOBJ
õ	õ	f x
MsgRes
õ	õ	y 
)õ	õ	 €
{
ö	ö	 	
return
÷	÷	 
DACConsulta
÷	÷	 
.
÷	÷	 )
ConsultaAfiliadoConcurrenia
÷	÷	 :
(
÷	÷	: ;
ObjAfiliado
÷	÷	; F
,
÷	÷	F G
ref
÷	÷	H K
MsgRes
÷	÷	L R
)
÷	÷	R S
;
÷	÷	S T
}
ø	ø	 	
public
ú	ú	 
List
ú	ú	 
<
ú	ú	 
ecop_concurrencia
ú	ú	 %
>
ú	ú	% &#
ConsultaIdConcurrenia
ú	ú	' <
(
ú	ú	< =
ecop_concurrencia
ú	ú	= N
ObjAfiliado
ú	ú	O Z
,
ú	ú	Z [
ref
ú	ú	\ _ 
MessageResponseOBJ
ú	ú	` r
MsgRes
ú	ú	s y
)
ú	ú	y z
{
û	û	 	
return
ü	ü	 
DACConsulta
ü	ü	 
.
ü	ü	 #
ConsultaIdConcurrenia
ü	ü	 4
(
ü	ü	4 5
ObjAfiliado
ü	ü	5 @
,
ü	ü	@ A
ref
ü	ü	B E
MsgRes
ü	ü	F L
)
ü	ü	L M
;
ü	ü	M N
}
ı	ı	 	
public
ÿ	ÿ	 
ecop_concurrencia
ÿ	ÿ	  $
ConsultaConcurrenciaId
ÿ	ÿ	! 7
(
ÿ	ÿ	7 8
int
ÿ	ÿ	8 ;
idconcurrencia
ÿ	ÿ	< J
)
ÿ	ÿ	J K
{
€
€
 	
return


 
DACConsulta


 
.


 $
ConsultaConcurrenciaId


 5
(


5 6
idconcurrencia


6 D
)


D E
;


E F
}
‚
‚
 	
public
ƒ
ƒ
 
List
ƒ
ƒ
 
<
ƒ
ƒ
 
ecop_concurrencia
ƒ
ƒ
 %
>
ƒ
ƒ
% &)
ConsultaConcurrenciaIdLista
ƒ
ƒ
' B
(
ƒ
ƒ
B C
Int32
ƒ
ƒ
C H
IdConcu
ƒ
ƒ
I P
,
ƒ
ƒ
P Q
ref
ƒ
ƒ
R U 
MessageResponseOBJ
ƒ
ƒ
V h
MsgRes
ƒ
ƒ
i o
)
ƒ
ƒ
o p
{
„
„
 	
return
…
…
 
DACConsulta
…
…
 
.
…
…
 )
ConsultaConcurrenciaIdLista
…
…
 :
(
…
…
: ;
IdConcu
…
…
; B
,
…
…
B C
ref
…
…
D G
MsgRes
…
…
H N
)
…
…
N O
;
…
…
O P
}
†
†
 	
public
ˆ
ˆ
 
List
ˆ
ˆ
 
<
ˆ
ˆ
 

ecop_censo
ˆ
ˆ
 
>
ˆ
ˆ
 "
ConsultaCensoIdLista
ˆ
ˆ
  4
(
ˆ
ˆ
4 5
Int32
ˆ
ˆ
5 :
IdCenso
ˆ
ˆ
; B
,
ˆ
ˆ
B C
ref
ˆ
ˆ
D G 
MessageResponseOBJ
ˆ
ˆ
H Z
MsgRes
ˆ
ˆ
[ a
)
ˆ
ˆ
a b
{
‰
‰
 	
return
Š
Š
 
DACConsulta
Š
Š
 
.
Š
Š
 "
ConsultaCensoIdLista
Š
Š
 3
(
Š
Š
3 4
IdCenso
Š
Š
4 ;
,
Š
Š
; <
ref
Š
Š
= @
MsgRes
Š
Š
A G
)
Š
Š
G H
;
Š
Š
H I
}
‹
‹
 	
public


 
List


 
<


 (
vw_ecop_cohortes_evolucion


 .
>


. /
ConsultaCohortes


0 @
(


@ A(
vw_ecop_cohortes_evolucion


A [
ObjAfiliado


\ g
,


g h
ref


i l 
MessageResponseOBJ


m 
MsgRes

€ †
)

† ‡
{


 	
return


 
DACConsulta


 
.


 
ConsultaCohortes


 /
(


/ 0
ObjAfiliado


0 ;
,


; <
ref


= @
MsgRes


A G
)


G H
;


H I
}


 	
public
’
’
 
List
’
’
 
<
’
’
 &
vw_tipo_habitacion_censo
’
’
 ,
>
’
’
, -$
ConsultaTipoHabitacion
’
’
. D
(
’
’
D E&
vw_tipo_habitacion_censo
’
’
E ]
ObjAfiliado
’
’
^ i
,
’
’
i j
ref
’
’
k n!
MessageResponseOBJ’
’
o 
MsgRes’
’
‚ ˆ
)’
’
ˆ ‰
{
“
“
 	
return
”
”
 
DACConsulta
”
”
 
.
”
”
 $
ConsultaTipoHabitacion
”
”
 5
(
”
”
5 6
ObjAfiliado
”
”
6 A
,
”
”
A B
ref
”
”
C F
MsgRes
”
”
G M
)
”
”
M N
;
”
”
N O
}
•
•
 	
public
–
–
 
List
–
–
 
<
–
–
 
vw_ciudad_ips
–
–
 !
>
–
–
! ")
ConsultaIdConcurreniaciudad
–
–
# >
(
–
–
> ?
vw_ciudad_ips
–
–
? L
ObjAfiliado
–
–
M X
,
–
–
X Y
ref
–
–
Z ] 
MessageResponseOBJ
–
–
^ p
MsgRes
–
–
q w
)
–
–
w x
{
—
—
 	
return
˜
˜
 
DACConsulta
˜
˜
 
.
˜
˜
 )
ConsultaIdConcurreniaciudad
˜
˜
 :
(
˜
˜
: ;
ObjAfiliado
˜
˜
; F
,
˜
˜
F G
ref
˜
˜
H K
MsgRes
˜
˜
L R
)
˜
˜
R S
;
˜
˜
S T
}
™
™
 	
public
œ
œ
 
void
œ
œ
 #
ActualizaConcurrencia
œ
œ
 )
(
œ
œ
) *
ecop_concurrencia
œ
œ
* ;
ObjConcurrencia
œ
œ
< K
,
œ
œ
K L
String
œ
œ
M S
User
œ
œ
T X
,
œ
œ
X Y
String
œ
œ
Z `
	IPAddress
œ
œ
a j
,
œ
œ
j k
ref
œ
œ
l o!
MessageResponseOBJœ
œ
p ‚
MsgResœ
œ
ƒ ‰
)œ
œ
‰ Š
{


 	
DACActualiza


 
.


 #
ActualizaConcurrencia


 .
(


. /
ObjConcurrencia


/ >
,


> ?
User


@ D
,


D E
	IPAddress


F O
,


O P
ref


Q T
MsgRes


U [
)


[ \
;


\ ]
}
Ÿ
Ÿ
 	
public
¡
¡
 
List
¡
¡
 
<
¡
¡
 
ecop_concurrencia
¡
¡
 %
>
¡
¡
% &&
GetconcurrenciaAfiliados
¡
¡
' ?
(
¡
¡
? @
string
¡
¡
@ F
numidafiliado
¡
¡
G T
)
¡
¡
T U
{
¢
¢
 	
return
£
£
 
DACConsulta
£
£
 
.
£
£
 &
GetconcurrenciaAfiliados
£
£
 7
(
£
£
7 8
numidafiliado
£
£
8 E
)
£
£
E F
;
£
£
F G
}
¤
¤
 	
public
¦
¦
 
List
¦
¦
 
<
¦
¦
 
Ref_ips
¦
¦
 
>
¦
¦
 
	GetRefIps
¦
¦
 &
(
¦
¦
& '
)
¦
¦
' (
{
§
§
 	
return
¨
¨
 
DACConsulta
¨
¨
 
.
¨
¨
 
	GetRefIps
¨
¨
 (
(
¨
¨
( )
)
¨
¨
) *
;
¨
¨
* +
}
©
©
 	
public
«
«
 
List
«
«
 
<
«
«
 
ref_eps
«
«
 
>
«
«
 
	GetRefEps
«
«
 &
(
«
«
& '
)
«
«
' (
{
¬
¬
 	
return
­
­
 
DACConsulta
­
­
 
.
­
­
 
	GetRefEps
­
­
 (
(
­
­
( )
)
­
­
) *
;
­
­
* +
}
®
®
 	
public
°
°
 
void
°
°
 )
ActualizaEgresoConcurrencia
°
°
 /
(
°
°
/ 0
ecop_concurrencia
°
°
0 A
ObjConcurrencia
°
°
B Q
,
°
°
Q R
String
°
°
S Y
User
°
°
Z ^
,
°
°
^ _
String
°
°
` f
	IPAddress
°
°
g p
,
°
°
p q
ref
°
°
r u!
MessageResponseOBJ°
°
v ˆ
MsgRes°
°
‰ 
)°
°
 
{
±
±
 	
DACActualiza
²
²
 
.
²
²
 )
ActualizaEgresoConcurrencia
²
²
 4
(
²
²
4 5
ObjConcurrencia
²
²
5 D
,
²
²
D E
User
²
²
F J
,
²
²
J K
	IPAddress
²
²
L U
,
²
²
U V
ref
²
²
W Z
MsgRes
²
²
[ a
)
²
²
a b
;
²
²
b c
}
³
³
 	
public
µ
µ
 
List
µ
µ
 
<
µ
µ
 
ecop_concurrencia
µ
µ
 %
>
µ
µ
% &0
"ConsultaCriterioIngresoActualizado
µ
µ
' I
(
µ
µ
I J
Int32
µ
µ
J O
IdConcu
µ
µ
P W
,
µ
µ
W X
ref
µ
µ
Y \ 
MessageResponseOBJ
µ
µ
] o
MsgRes
µ
µ
p v
)
µ
µ
v w
{
¶
¶
 	
return
·
·
 
DACConsulta
·
·
 
.
·
·
 0
"ConsultaCriterioIngresoActualizado
·
·
 A
(
·
·
A B
IdConcu
·
·
B I
,
·
·
I J
ref
·
·
K N
MsgRes
·
·
O U
)
·
·
U V
;
·
·
V W
}
¸
¸
 	
public
º
º
 
List
º
º
 
<
º
º
 4
&ecop_concurrencia_encuesta_satisfacion
º
º
 :
>
º
º
: ;%
ConsultaEncuestaCargada
º
º
< S
(
º
º
S T
Int32
º
º
T Y
IdConcu
º
º
Z a
,
º
º
a b
ref
º
º
c f 
MessageResponseOBJ
º
º
g y
MsgResº
º
z €
)º
º
€ 
{
»
»
 	
return
¼
¼
 
DACConsulta
¼
¼
 
.
¼
¼
 %
ConsultaEncuestaCargada
¼
¼
 6
(
¼
¼
6 7
IdConcu
¼
¼
7 >
,
¼
¼
> ?
ref
¼
¼
@ C
MsgRes
¼
¼
D J
)
¼
¼
J K
;
¼
¼
K L
}
½
½
 	
public
¿
¿
 
int
¿
¿
 
InsertaEgreso
¿
¿
  
(
¿
¿
  !+
egreso_auditoria_Hospitalaria
¿
¿
! >
Egreso
¿
¿
? E
,
¿
¿
E F
String
¿
¿
G M
UserName
¿
¿
N V
,
¿
¿
V W
String
¿
¿
X ^
	IPAddress
¿
¿
_ h
,
¿
¿
h i
ref
¿
¿
j m!
MessageResponseOBJ¿
¿
n €
MsgRes¿
¿
 ‡
)¿
¿
‡ ˆ
{
À
À
 	
return
Á
Á
 

DACInserta
Á
Á
 
.
Á
Á
 
InsertaEgreso
Á
Á
 +
(
Á
Á
+ ,
Egreso
Á
Á
, 2
,
Á
Á
2 3
UserName
Á
Á
4 <
,
Á
Á
< =
	IPAddress
Á
Á
> G
,
Á
Á
G H
ref
Á
Á
I L
MsgRes
Á
Á
M S
)
Á
Á
S T
;
Á
Á
T U
}
Â
Â
 	
public
Ä
Ä
 
List
Ä
Ä
 
<
Ä
Ä
 0
"vw_concurrencia_evolucion_Contrato
Ä
Ä
 6
>
Ä
Ä
6 7,
ConsultaIdConcurreniaEvolucion
Ä
Ä
8 V
(
Ä
Ä
V W
ecop_concurrencia
Ä
Ä
W h
ObjAfiliado
Ä
Ä
i t
,
Ä
Ä
t u
ref
Ä
Ä
v y!
MessageResponseOBJÄ
Ä
z Œ
MsgResÄ
Ä
 “
)Ä
Ä
“ ”
{
Å
Å
 	
return
Æ
Æ
 
DACConsulta
Æ
Æ
 
.
Æ
Æ
 ,
ConsultaIdConcurreniaEvolucion
Æ
Æ
 =
(
Æ
Æ
= >
ObjAfiliado
Æ
Æ
> I
,
Æ
Æ
I J
ref
Æ
Æ
K N
MsgRes
Æ
Æ
O U
)
Æ
Æ
U V
;
Æ
Æ
V W
}
Ç
Ç
 	
public
Ê
Ê
 
List
Ê
Ê
 
<
Ê
Ê
 &
vw_consulta_concurrencia
Ê
Ê
 ,
>
Ê
Ê
, -"
ConsultaConcurrencia
Ê
Ê
. B
(
Ê
Ê
B C
ref
Ê
Ê
C F 
MessageResponseOBJ
Ê
Ê
G Y
MsgRes
Ê
Ê
Z `
)
Ê
Ê
` a
{
Ë
Ë
 	
return
Ì
Ì
 
DACConsulta
Ì
Ì
 
.
Ì
Ì
 "
ConsultaConcurrencia
Ì
Ì
 3
(
Ì
Ì
3 4
ref
Ì
Ì
4 7
MsgRes
Ì
Ì
8 >
)
Ì
Ì
> ?
;
Ì
Ì
? @
}
Í
Í
 	
public
Ï
Ï
 
List
Ï
Ï
 
<
Ï
Ï
  
vw_consulta_egreso
Ï
Ï
 &
>
Ï
Ï
& '
ConsultaEgreso
Ï
Ï
( 6
(
Ï
Ï
6 7
ref
Ï
Ï
7 : 
MessageResponseOBJ
Ï
Ï
; M
MsgRes
Ï
Ï
N T
)
Ï
Ï
T U
{
Ğ
Ğ
 	
return
Ñ
Ñ
 
DACConsulta
Ñ
Ñ
 
.
Ñ
Ñ
 
ConsultaEgreso
Ñ
Ñ
 -
(
Ñ
Ñ
- .
ref
Ñ
Ñ
. 1
MsgRes
Ñ
Ñ
2 8
)
Ñ
Ñ
8 9
;
Ñ
Ñ
9 :
}
Ò
Ò
 	
public
Ô
Ô
 
List
Ô
Ô
 
<
Ô
Ô
 0
"managment_vw_consulta_egresoResult
Ô
Ô
 6
>
Ô
Ô
6 7
ConsultaEgreso2
Ô
Ô
8 G
(
Ô
Ô
G H
ref
Ô
Ô
H K 
MessageResponseOBJ
Ô
Ô
L ^
MsgRes
Ô
Ô
_ e
)
Ô
Ô
e f
{
Õ
Õ
 	
return
Ö
Ö
 
DACConsulta
Ö
Ö
 
.
Ö
Ö
 
ConsultaEgreso2
Ö
Ö
 .
(
Ö
Ö
. /
ref
Ö
Ö
/ 2
MsgRes
Ö
Ö
3 9
)
Ö
Ö
9 :
;
Ö
Ö
: ;
}
×
×
 	
public
Ù
Ù
 
List
Ù
Ù
 
<
Ù
Ù
 *
vw_consulta_eventos_adversos
Ù
Ù
 0
>
Ù
Ù
0 1
ConsultaEventosAd
Ù
Ù
2 C
(
Ù
Ù
C D
ref
Ù
Ù
D G 
MessageResponseOBJ
Ù
Ù
H Z
MsgRes
Ù
Ù
[ a
)
Ù
Ù
a b
{
Ú
Ú
 	
return
Û
Û
 
DACConsulta
Û
Û
 
.
Û
Û
 
ConsultaEventosAd
Û
Û
 0
(
Û
Û
0 1
ref
Û
Û
1 4
MsgRes
Û
Û
5 ;
)
Û
Û
; <
;
Û
Û
< =
}
Ü
Ü
 	
public
Ş
Ş
 
List
Ş
Ş
 
<
Ş
Ş
 +
vw_consulta_situacion_calidad
Ş
Ş
 1
>
Ş
Ş
1 2"
ConsultaSituacionCal
Ş
Ş
3 G
(
Ş
Ş
G H
ref
Ş
Ş
H K 
MessageResponseOBJ
Ş
Ş
L ^
MsgRes
Ş
Ş
_ e
)
Ş
Ş
e f
{
ß
ß
 	
return
à
à
 
DACConsulta
à
à
 
.
à
à
 "
ConsultaSituacionCal
à
à
 3
(
à
à
3 4
ref
à
à
4 7
MsgRes
à
à
8 >
)
à
à
> ?
;
à
à
? @
}
á
á
 	
public
ã
ã
 
List
ã
ã
 
<
ã
ã
 
vw_gestantes
ã
ã
  
>
ã
ã
  !
ConsultaGestantes
ã
ã
" 3
(
ã
ã
3 4
ref
ã
ã
4 7 
MessageResponseOBJ
ã
ã
8 J
MsgRes
ã
ã
K Q
)
ã
ã
Q R
{
ä
ä
 	
return
å
å
 
DACConsulta
å
å
 
.
å
å
 
ConsultaGestantes
å
å
 0
(
å
å
0 1
ref
å
å
1 4
MsgRes
å
å
5 ;
)
å
å
; <
;
å
å
< =
}
æ
æ
 	
public
ç
ç
 
List
ç
ç
 
<
ç
ç
 /
!management_controlNatalidadResult
ç
ç
 5
>
ç
ç
5 6$
ConsultaGestantesNuevo
ç
ç
7 M
(
ç
ç
M N
ref
ç
ç
N Q 
MessageResponseOBJ
ç
ç
R d
MsgRes
ç
ç
e k
)
ç
ç
k l
{
è
è
 	
return
é
é
 
DACConsulta
é
é
 
.
é
é
 $
ConsultaGestantesNuevo
é
é
 5
(
é
é
5 6
ref
é
é
6 9
MsgRes
é
é
: @
)
é
é
@ A
;
é
é
A B
}
ê
ê
 	
public
ë
ë
 
List
ë
ë
 
<
ë
ë
 
vw_gestantes_sin
ë
ë
 $
>
ë
ë
$ %"
ConsultaGestantesSin
ë
ë
& :
(
ë
ë
: ;
ref
ë
ë
; > 
MessageResponseOBJ
ë
ë
? Q
MsgRes
ë
ë
R X
)
ë
ë
X Y
{
ì
ì
 	
return
í
í
 
DACConsulta
í
í
 
.
í
í
 "
ConsultaGestantesSin
í
í
 3
(
í
í
3 4
ref
í
í
4 7
MsgRes
í
í
8 >
)
í
í
> ?
;
í
í
? @
}
î
î
 	
public
ñ
ñ
 
List
ñ
ñ
 
<
ñ
ñ
 
vw_Mortalidad
ñ
ñ
 !
>
ñ
ñ
! " 
ConsultaMortalidad
ñ
ñ
# 5
(
ñ
ñ
5 6
ref
ñ
ñ
6 9 
MessageResponseOBJ
ñ
ñ
: L
MsgRes
ñ
ñ
M S
)
ñ
ñ
S T
{
ò
ò
 	
return
ó
ó
 
DACConsulta
ó
ó
 
.
ó
ó
  
ConsultaMortalidad
ó
ó
 1
(
ó
ó
1 2
ref
ó
ó
2 5
MsgRes
ó
ó
6 <
)
ó
ó
< =
;
ó
ó
= >
}
ô
ô
 	
public
ö
ö
 
List
ö
ö
 
<
ö
ö
 <
.ManagementConsultaConcuMortalidadCon_SinResult
ö
ö
 B
>
ö
ö
B C"
ConsultaMortalidadV2
ö
ö
D X
(
ö
ö
X Y
int
ö
ö
Y \
tipoconsulta
ö
ö
] i
,
ö
ö
i j
ref
ö
ö
k n!
MessageResponseOBJö
ö
o 
MsgResö
ö
‚ ˆ
)ö
ö
ˆ ‰
{
÷
÷
 	
return
ø
ø
 
DACConsulta
ø
ø
 
.
ø
ø
 "
ConsultaMortalidadV2
ø
ø
 3
(
ø
ø
3 4
tipoconsulta
ø
ø
4 @
,
ø
ø
@ A
ref
ø
ø
B E
MsgRes
ø
ø
F L
)
ø
ø
L M
;
ø
ø
M N
}
ù
ù
 	
public
û
û
 
List
û
û
 
<
û
û
 
vw_Mortalidad_sin
û
û
 %
>
û
û
% &#
ConsultaMortalidadSin
û
û
' <
(
û
û
< =
ref
û
û
= @ 
MessageResponseOBJ
û
û
A S
MsgRes
û
û
T Z
)
û
û
Z [
{
ü
ü
 	
return
ı
ı
 
DACConsulta
ı
ı
 
.
ı
ı
 #
ConsultaMortalidadSin
ı
ı
 4
(
ı
ı
4 5
ref
ı
ı
5 8
MsgRes
ı
ı
9 ?
)
ı
ı
? @
;
ı
ı
@ A
}
ş
ş
 	
public
€€ 
void
€€ *
InsertarEncuestaConcurrencia
€€ 0
(
€€0 1(
ecop_concurrencia_encuesta
€€1 K
Encuesta
€€L T
,
€€T U
ref
€€V Y 
MessageResponseOBJ
€€Z l
MsgRes
€€m s
)
€€s t
{
 	

DACInserta
‚‚ 
.
‚‚ *
InsertarEncuestaConcurrencia
‚‚ 3
(
‚‚3 4
Encuesta
‚‚4 <
,
‚‚< =
ref
‚‚> A
MsgRes
‚‚B H
)
‚‚H I
;
‚‚I J
}
ƒƒ 	
public
†† 
List
†† 
<
†† +
egreso_auditoria_Hospitalaria
†† 1
>
††1 2
ConsultaAgresoH
††3 B
(
††B C
Int32
††C H
IdConcu
††I P
,
††P Q
ref
††R U 
MessageResponseOBJ
††V h
MsgRes
††i o
)
††o p
{
‡‡ 	
return
ˆˆ 
DACConsulta
ˆˆ 
.
ˆˆ 
ConsultaAgresoH
ˆˆ .
(
ˆˆ. /
IdConcu
ˆˆ/ 6
,
ˆˆ6 7
ref
ˆˆ8 ;
MsgRes
ˆˆ< B
)
ˆˆB C
;
ˆˆC D
}
ŠŠ 	
public
ŒŒ 
void
ŒŒ "
Actualizarprevenible
ŒŒ (
(
ŒŒ( )
ecop_concurrencia
ŒŒ) :
Objconcurrencia
ŒŒ; J
,
ŒŒJ K
ref
ŒŒL O 
MessageResponseOBJ
ŒŒP b
MsgRes
ŒŒc i
)
ŒŒi j
{
 	
DACActualiza
 
.
 "
Actualizarprevenible
 -
(
- .
Objconcurrencia
. =
,
= >
ref
? B
MsgRes
C I
)
I J
;
J K
}
 	
public
‘‘ 
List
‘‘ 
<
‘‘ /
!vw_max_concurrencia_por_documento
‘‘ 5
>
‘‘5 6.
 ConsultaMaxConcurrenciaDocumento
‘‘7 W
(
‘‘W X
String
‘‘X ^
	Documento
‘‘_ h
,
‘‘h i
ref
‘‘j m!
MessageResponseOBJ‘‘n €
MsgRes‘‘ ‡
)‘‘‡ ˆ
{
’’ 	
return
““ 
DACConsulta
““ 
.
““ .
 ConsultaMaxConcurrenciaDocumento
““ ?
(
““? @
	Documento
““@ I
,
““I J
ref
““K N
MsgRes
““O U
)
““U V
;
““V W
}
”” 	
public
–– 
void
–– 
ActualizarEgreso
–– $
(
––$ %+
egreso_auditoria_Hospitalaria
––% B
	Objegreso
––C L
,
––L M
ref
––N Q 
MessageResponseOBJ
––R d
MsgRes
––e k
)
––k l
{
—— 	
DACActualiza
˜˜ 
.
˜˜ 
ActualizarEgreso
˜˜ )
(
˜˜) *
	Objegreso
˜˜* 3
,
˜˜3 4
ref
˜˜5 8
MsgRes
˜˜9 ?
)
˜˜? @
;
˜˜@ A
}
™™ 	
public
›› 
void
›› %
InsertarEgresoGestantes
›› +
(
››+ ,
egreso_gestantes
››, <
	Gestantes
››= F
,
››F G
ref
››H K 
MessageResponseOBJ
››L ^
MsgRes
››_ e
)
››e f
{
œœ 	

DACInserta
 
.
 %
InsertarEgresoGestantes
 .
(
. /
	Gestantes
/ 8
,
8 9
ref
: =
MsgRes
> D
)
D E
;
E F
}
 	
public
   
List
   
<
   
egreso_gestantes
   $
>
  $ %&
ConsultasEgresoGestantes
  & >
(
  > ?
Int32
  ? D
id_concurrencia
  E T
,
  T U
ref
  V Y 
MessageResponseOBJ
  Z l
MsgRes
  m s
)
  s t
{
¡¡ 	
return
¢¢ 
DACConsulta
¢¢ 
.
¢¢ &
ConsultasEgresoGestantes
¢¢ 7
(
¢¢7 8
id_concurrencia
¢¢8 G
,
¢¢G H
ref
¢¢I L
MsgRes
¢¢M S
)
¢¢S T
;
¢¢T U
}
££ 	
public
¥¥ 
List
¥¥ 
<
¥¥ +
vw_ecop_egresos_hospitalarios
¥¥ 1
>
¥¥1 2
GetListaEgresos
¥¥3 B
(
¥¥B C
DateTime
¥¥C K
?
¥¥K L
fechainicial
¥¥M Y
,
¥¥Y Z
DateTime
¥¥[ c
?
¥¥c d

fechafinal
¥¥e o
)
¥¥o p
{
¦¦ 	
return
§§ 
DACConsulta
§§ 
.
§§ 
GetListaEgresos
§§ .
(
§§. /
fechainicial
§§/ ;
,
§§; <

fechafinal
§§= G
)
§§G H
;
§§H I
}
¨¨ 	
public
©© 
List
©© 
<
©© 9
+management_ecop_egresos_hospitalariosResult
©© ?
>
©©? @)
listadoEgresosHospitalarios
©©A \
(
©©\ ]
DateTime
©©] e
?
©©e f
fechainicial
©©g s
,
©©s t
DateTime
©©u }
?
©©} ~

fechafinal©© ‰
)©©‰ Š
{
ªª 	
return
«« 
DACConsulta
«« 
.
«« )
listadoEgresosHospitalarios
«« :
(
««: ;
fechainicial
««; G
,
««G H

fechafinal
««I S
)
««S T
;
««T U
}
¬¬ 	
public
­­ 
ecop_concurrencia
­­  0
"traerDatosBeneficiarioConcurrencia
­­! C
(
­­C D
string
­­D J
documentoBen
­­K W
)
­­W X
{
®® 	
return
¯¯ 
DACConsulta
¯¯ 
.
¯¯ 0
"traerDatosBeneficiarioConcurrencia
¯¯ A
(
¯¯A B
documentoBen
¯¯B N
)
¯¯N O
;
¯¯O P
}
°° 	
public
±± 
List
±± 
<
±± 8
*ecop_concurrencia_evolucion_procedimientos
±± >
>
±±> ?/
!traerDatosEvolucionProcedimientos
±±@ a
(
±±a b
int
±±b e
id_evolucion
±±f r
)
±±r s
{
²² 	
return
³³ 
DACConsulta
³³ 
.
³³ /
!traerDatosEvolucionProcedimientos
³³ @
(
³³@ A
id_evolucion
³³A M
)
³³M N
;
³³N O
}
´´ 	
public
µµ 
List
µµ 
<
µµ &
ref_tipo_hospitalizacion
µµ ,
>
µµ, -'
GetRefTipoHospitalizacion
µµ. G
(
µµG H
)
µµH I
{
¶¶ 	
return
·· 
DACConsulta
·· 
.
·· '
GetRefTipoHospitalizacion
·· 8
(
··8 9
)
··9 :
;
··: ;
}
¸¸ 	
public
¹¹ 
List
¹¹ 
<
¹¹ -
ref_tipo_patologia_catastrofica
¹¹ 3
>
¹¹3 4-
GetRefTipoPatologiaCatastrofica
¹¹5 T
(
¹¹T U
)
¹¹U V
{
ºº 	
return
»» 
DACConsulta
»» 
.
»» -
GetRefTipoPatologiaCatastrofica
»» >
(
»»> ?
)
»»? @
;
»»@ A
}
¼¼ 	
public
½½ 
List
½½ 
<
½½ 1
#ref_pertinencia_estancia_prolongada
½½ 7
>
½½7 8)
GetRefPertinenciaProlongada
½½9 T
(
½½T U
)
½½U V
{
¾¾ 	
return
¿¿ 
DACConsulta
¿¿ 
.
¿¿ )
GetRefPertinenciaProlongada
¿¿ :
(
¿¿: ;
)
¿¿; <
;
¿¿< =
}
ÀÀ 	
public
ÁÁ 
List
ÁÁ 
<
ÁÁ /
!ref_condicion_estancia_prolongada
ÁÁ 5
>
ÁÁ5 6'
GetRefCondicionProlongada
ÁÁ7 P
(
ÁÁP Q
)
ÁÁQ R
{
ÂÂ 	
return
ÃÃ 
DACConsulta
ÃÃ 
.
ÃÃ '
GetRefCondicionProlongada
ÃÃ 8
(
ÃÃ8 9
)
ÃÃ9 :
;
ÃÃ: ;
}
ÄÄ 	
public
ÆÆ 0
"categorizacion_egreso_hospitalario
ÆÆ 1
getcatbyegreso
ÆÆ2 @
(
ÆÆ@ A
int
ÆÆA D
idegreso
ÆÆE M
)
ÆÆM N
{
ÇÇ 	
return
ÈÈ 
DACConsulta
ÈÈ 
.
ÈÈ 
getcatbyegreso
ÈÈ -
(
ÈÈ- .
idegreso
ÈÈ. 6
)
ÈÈ6 7
;
ÈÈ7 8
}
ÉÉ 	
public
ËË 
void
ËË $
insertarcategorizacion
ËË *
(
ËË* +0
"categorizacion_egreso_hospitalario
ËË+ M
obj
ËËN Q
,
ËËQ R
ref
ËËS V 
MessageResponseOBJ
ËËW i
MsgRes
ËËj p
)
ËËp q
{
ÌÌ 	

DACInserta
ÍÍ 
.
ÍÍ $
insertarcategorizacion
ÍÍ -
(
ÍÍ- .
obj
ÍÍ. 1
,
ÍÍ1 2
ref
ÍÍ3 6
MsgRes
ÍÍ7 =
)
ÍÍ= >
;
ÍÍ> ?
}
ÎÎ 	
public
ÏÏ 
List
ÏÏ 
<
ÏÏ 8
*management_egresos_verCategorizacionResult
ÏÏ >
>
ÏÏ> ?,
traerDatosCategorizacionEgreso
ÏÏ@ ^
(
ÏÏ^ _
int
ÏÏ_ b
idEgreso
ÏÏc k
)
ÏÏk l
{
ĞĞ 	
return
ÑÑ 
DACConsulta
ÑÑ 
.
ÑÑ ,
traerDatosCategorizacionEgreso
ÑÑ =
(
ÑÑ= >
idEgreso
ÑÑ> F
)
ÑÑF G
;
ÑÑG H
}
ÒÒ 	
public
ÔÔ 
void
ÔÔ &
actualizarcategorizacion
ÔÔ ,
(
ÔÔ, -0
"categorizacion_egreso_hospitalario
ÔÔ- O
obj
ÔÔP S
,
ÔÔS T
ref
ÔÔU X 
MessageResponseOBJ
ÔÔY k
MsgRes
ÔÔl r
)
ÔÔr s
{
ÕÕ 	
DACActualiza
ÖÖ 
.
ÖÖ &
actualizarcategorizacion
ÖÖ 1
(
ÖÖ1 2
obj
ÖÖ2 5
,
ÖÖ5 6
ref
ÖÖ7 :
MsgRes
ÖÖ; A
)
ÖÖA B
;
ÖÖB C
}
×× 	
public
ÙÙ 
void
ÙÙ 
ActualizarIps
ÙÙ !
(
ÙÙ! "
int
ÙÙ" %
idconcurrencia
ÙÙ& 4
,
ÙÙ4 5
int
ÙÙ6 9
idips
ÙÙ: ?
,
ÙÙ? @
ref
ÙÙA D 
MessageResponseOBJ
ÙÙE W
Msg
ÙÙX [
)
ÙÙ[ \
{
ÚÚ 	
DACActualiza
ÛÛ 
.
ÛÛ 
ActualizarIps
ÛÛ &
(
ÛÛ& '
idconcurrencia
ÛÛ' 5
,
ÛÛ5 6
idips
ÛÛ7 <
,
ÛÛ< =
ref
ÛÛ> A
Msg
ÛÛB E
)
ÛÛE F
;
ÛÛF G
}
ÜÜ 	
public
ŞŞ 
List
ŞŞ 
<
ŞŞ 
ref_trimeste
ŞŞ  
>
ŞŞ  !
GetRefTrimestre
ŞŞ" 1
(
ŞŞ1 2
)
ŞŞ2 3
{
ßß 	
return
àà 
DACConsulta
àà 
.
àà 
GetRefTrimestre
àà .
(
àà. /
)
àà/ 0
;
àà0 1
}
áá 	
public
ââ 
List
ââ 
<
ââ '
Ref_plan_mejora_categoria
ââ -
>
ââ- .)
GetRefplan_mejora_categoria
ââ/ J
(
ââJ K
)
ââK L
{
ãã 	
return
ää 
DACConsulta
ää 
.
ää )
GetRefplan_mejora_categoria
ää :
(
ää: ;
)
ää; <
;
ää< =
}
åå 	
public
ææ 
List
ææ 
<
ææ "
Ref_plan_mejora_foco
ææ (
>
ææ( )%
GetRef_plan_mejora_foco
ææ* A
(
ææA B
)
ææB C
{
çç 	
return
èè 
DACConsulta
èè 
.
èè %
GetRef_plan_mejora_foco
èè 6
(
èè6 7
)
èè7 8
;
èè8 9
}
éé 	
public
ëë 
List
ëë 
<
ëë "
Ref_proceso_auditado
ëë (
>
ëë( )%
GetRef_proceso_auditado
ëë* A
(
ëëA B
)
ëëB C
{
ìì 	
return
íí 
DACConsulta
íí 
.
íí %
GetRef_proceso_auditado
íí 6
(
íí6 7
)
íí7 8
;
íí8 9
}
îî 	
public
ğğ 
List
ğğ 
<
ğğ .
 management_planmejora_focoResult
ğğ 4
>
ğğ4 5
Cuentadetallefoco
ğğ6 G
(
ğğG H
Int32
ğğH M
id_plan_de_mejora
ğğN _
,
ğğ_ `
ref
ğğa d 
MessageResponseOBJ
ğğe w
MsgRes
ğğx ~
)
ğğ~ 
{
ññ 	
return
òò 
DACConsulta
òò 
.
òò 
Cuentadetallefoco
òò 0
(
òò0 1
id_plan_de_mejora
òò1 B
,
òòB C
ref
òòD G
MsgRes
òòH N
)
òòN O
;
òòO P
}
óó 	
public
õõ 
Int32
õõ  
InsertarPlanMejora
õõ '
(
õõ' (!
ecop_plan_de_mejora
õõ( ;
obj
õõ< ?
,
õõ? @
ref
õõA D 
MessageResponseOBJ
õõE W
MsgRes
õõX ^
)
õõ^ _
{
öö 	
return
÷÷ 

DACInserta
÷÷ 
.
÷÷  
InsertarPlanMejora
÷÷ 0
(
÷÷0 1
obj
÷÷1 4
,
÷÷4 5
ref
÷÷6 9
MsgRes
÷÷: @
)
÷÷@ A
;
÷÷A B
}
øø 	
public
úú 
Int32
úú $
InsertarPlanMejorafoco
úú +
(
úú+ ,0
"ecop_plan_mejora_foco_intervencion
úú, N
obj
úúO R
,
úúR S
ref
úúT W 
MessageResponseOBJ
úúX j
MsgRes
úúk q
)
úúq r
{
ûû 	
return
üü 

DACInserta
üü 
.
üü $
InsertarPlanMejorafoco
üü 4
(
üü4 5
obj
üü5 8
,
üü8 9
ref
üü: =
MsgRes
üü> D
)
üüD E
;
üüE F
}
ıı 	
public
ÿÿ 
void
ÿÿ !
EliminarDetallePlan
ÿÿ '
(
ÿÿ' (
int
ÿÿ( +.
 id_plan_mejora_foco_intervencion
ÿÿ, L
,
ÿÿL M
ref
ÿÿN Q 
MessageResponseOBJ
ÿÿR d
MsgRes
ÿÿe k
)
ÿÿk l
{
€€ 	

DACElimina
 
.
 !
EliminarDetallePlan
 *
(
* +.
 id_plan_mejora_foco_intervencion
+ K
,
K L
ref
M P
MsgRes
Q W
)
W X
;
X Y
}
‚‚ 	
public
„„ 
List
„„ 
<
„„ /
!management_planmejora_tareaResult
„„ 5
>
„„5 6 
CuentadetalleTarea
„„7 I
(
„„I J
Int32
„„J O.
 id_plan_mejora_foco_intervencion
„„P p
,
„„p q
ref
„„r u!
MessageResponseOBJ„„v ˆ
MsgRes„„‰ 
)„„ 
{
…… 	
return
†† 
DACConsulta
†† 
.
††  
CuentadetalleTarea
†† 1
(
††1 2.
 id_plan_mejora_foco_intervencion
††2 R
,
††R S
ref
††T W
MsgRes
††X ^
)
††^ _
;
††_ `
}
‡‡ 	
public
‰‰ 
void
‰‰ &
EliminarDetallePlanTarea
‰‰ ,
(
‰‰, -
int
‰‰- 0#
id_plan_mejora_tareas
‰‰1 F
,
‰‰F G
ref
‰‰H K 
MessageResponseOBJ
‰‰L ^
MsgRes
‰‰_ e
)
‰‰e f
{
ŠŠ 	

DACElimina
‹‹ 
.
‹‹ &
EliminarDetallePlanTarea
‹‹ /
(
‹‹/ 0#
id_plan_mejora_tareas
‹‹0 E
,
‹‹E F
ref
‹‹G J
MsgRes
‹‹K Q
)
‹‹Q R
;
‹‹R S
}
ŒŒ 	
public
 
Int32
 %
InsertarPlanMejoratarea
 ,
(
, -%
ecop_plan_mejora_tareas
- D
obj
E H
,
H I
ref
J M 
MessageResponseOBJ
N `
MsgRes
a g
)
g h
{
 	
return
 

DACInserta
 
.
 %
InsertarPlanMejoratarea
 5
(
5 6
obj
6 9
,
9 :
ref
; >
MsgRes
? E
)
E F
;
F G
}
‘‘ 	
public
”” 
void
”” "
ActualizarPlanEgreso
”” (
(
””( )
int
””) ,
id_plan_mejora
””- ;
,
””; <
Int32
””= B
estado
””C I
,
””I J
ref
””K N 
MessageResponseOBJ
””O a
MsgRes
””b h
)
””h i
{
•• 	
DACActualiza
–– 
.
–– "
ActualizarPlanEgreso
–– -
(
––- .
id_plan_mejora
––. <
,
––< =
estado
––> D
,
––D E
ref
––F I
MsgRes
––J P
)
––P Q
;
––Q R
}
—— 	
public
™™ 
List
™™ 
<
™™ 7
)management_planmejora_tarea_controlResult
™™ =
>
™™= >'
CuentadetalleTareacontrol
™™? X
(
™™X Y
Int32
™™Y ^
id_plan_mejora
™™_ m
,
™™m n
ref
™™o r!
MessageResponseOBJ™™s …
MsgRes™™† Œ
)™™Œ 
{
šš 	
return
›› 
DACConsulta
›› 
.
›› '
CuentadetalleTareacontrol
›› 8
(
››8 9
id_plan_mejora
››9 G
,
››G H
ref
››I L
MsgRes
››M S
)
››S T
;
››T U
}
œœ 	
public
 
List
 
<
 2
$management_plan_mejora_tableroResult
 8
>
8 9 
PlanTableroGeneral
: L
(
L M
)
M N
{
ŸŸ 	
return
   
DACConsulta
   
.
    
PlanTableroGeneral
   1
(
  1 2
)
  2 3
;
  3 4
}
¡¡ 	
public
££ 
List
££ 
<
££ 1
#management_planMejora_reporteResult
££ 7
>
££7 8
DatosPMReporte
££9 G
(
££G H
int
££H K
?
££K L
idPlan
££M S
)
££S T
{
¤¤ 	
return
¥¥ 
DACConsulta
¥¥ 
.
¥¥ 
DatosPMReporte
¥¥ -
(
¥¥- .
idPlan
¥¥. 4
)
¥¥4 5
;
¥¥5 6
}
¦¦ 	
public
¨¨ 
List
¨¨ 
<
¨¨ @
2management_planMejora_reporte_detalleCeacionResult
¨¨ F
>
¨¨F G+
DatosPMReporteDetalleCreacion
¨¨H e
(
¨¨e f
int
¨¨f i
?
¨¨i j
idPlan
¨¨k q
)
¨¨q r
{
©© 	
return
ªª 
DACConsulta
ªª 
.
ªª +
DatosPMReporteDetalleCreacion
ªª <
(
ªª< =
idPlan
ªª= C
)
ªªC D
;
ªªD E
}
«« 	
public
­­ 
List
­­ 
<
­­ ?
1management_planMejora_reporte_detalleCierreResult
­­ E
>
­­E F)
DatosPMReporteDetalleCierre
­­G b
(
­­b c
int
­­c f
?
­­f g
idPlan
­­h n
)
­­n o
{
®® 	
return
¯¯ 
DACConsulta
¯¯ 
.
¯¯ )
DatosPMReporteDetalleCierre
¯¯ :
(
¯¯: ;
idPlan
¯¯; A
)
¯¯A B
;
¯¯B C
}
°° 	
public
²² 
List
²² 
<
²² ;
-management_planMejora_tableroDocumentalResult
²² A
>
²²A B&
DatosPMgestionDocumental
²²C [
(
²²[ \
int
²²\ _
?
²²_ `
regional
²²a i
,
²²i j
DateTime
²²k s
?
²²s t
fechaInicio²²u €
,²²€ 
DateTime²²‚ Š
?²²Š ‹
fechaFin²²Œ ”
)²²” •
{
³³ 	
return
´´ 
DACConsulta
´´ 
.
´´ &
DatosPMgestionDocumental
´´ 7
(
´´7 8
regional
´´8 @
,
´´@ A
fechaInicio
´´B M
,
´´M N
fechaFin
´´O W
)
´´W X
;
´´X Y
}
µµ 	
public
·· 
List
·· 
<
·· >
0management_planesMejora_reporteSeguimientoResult
·· D
>
··D E-
DatosPMgestionDocumentalReporte
··F e
(
··e f
int
··f i
?
··i j
regional
··k s
,
··s t
DateTime
··u }
?
··} ~
fechaInicio·· Š
,··Š ‹
DateTime··Œ ”
?··” •
fechaFin··– 
)·· Ÿ
{
¸¸ 	
return
¹¹ 
DACConsulta
¹¹ 
.
¹¹ -
DatosPMgestionDocumentalReporte
¹¹ >
(
¹¹> ?
regional
¹¹? G
,
¹¹G H
fechaInicio
¹¹I T
,
¹¹T U
fechaFin
¹¹V ^
)
¹¹^ _
;
¹¹_ `
}
ºº 	
public
¼¼ 
List
¼¼ 
<
¼¼ &
ref_planMejora_prioridad
¼¼ ,
>
¼¼, -
listaPrioridadPM
¼¼. >
(
¼¼> ?
)
¼¼? @
{
½½ 	
return
¾¾ 
DACConsulta
¾¾ 
.
¾¾ 
listaPrioridadPM
¾¾ /
(
¾¾/ 0
)
¾¾0 1
;
¾¾1 2
}
¿¿ 	
public
ÀÀ 
List
ÀÀ 
<
ÀÀ 7
)management_plan_mejora_tablero_dtllResult
ÀÀ =
>
ÀÀ= >$
PlanTableroGeneralDtll
ÀÀ? U
(
ÀÀU V
Int32
ÀÀV [
id_plan_de_mejora
ÀÀ\ m
,
ÀÀm n
ref
ÀÀo r!
MessageResponseOBJÀÀs …
MsgResÀÀ† Œ
)ÀÀŒ 
{
ÁÁ 	
return
ÂÂ 
DACConsulta
ÂÂ 
.
ÂÂ $
PlanTableroGeneralDtll
ÂÂ 5
(
ÂÂ5 6
id_plan_de_mejora
ÂÂ6 G
,
ÂÂG H
ref
ÂÂI L
MsgRes
ÂÂM S
)
ÂÂS T
;
ÂÂT U
}
ÃÃ 	
public
ÅÅ 
List
ÅÅ 
<
ÅÅ 6
(management_planMejora_ampliacionesResult
ÅÅ <
>
ÅÅ< =$
PlanMejoraAmpliaciones
ÅÅ> T
(
ÅÅT U
int
ÅÅU X
?
ÅÅX Y
idPlan
ÅÅZ `
)
ÅÅ` a
{
ÆÆ 	
return
ÇÇ 
DACConsulta
ÇÇ 
.
ÇÇ $
PlanMejoraAmpliaciones
ÇÇ 5
(
ÇÇ5 6
idPlan
ÇÇ6 <
)
ÇÇ< =
;
ÇÇ= >
}
ÈÈ 	
public
ÊÊ 
List
ÊÊ 
<
ÊÊ 8
*management_planMejora_DocumentosPlanResult
ÊÊ >
>
ÊÊ> ?&
PlanMejoraArchivoporTipo
ÊÊ@ X
(
ÊÊX Y
int
ÊÊY \
?
ÊÊ\ ]
idPlan
ÊÊ^ d
,
ÊÊd e
int
ÊÊf i
?
ÊÊi j
tipo
ÊÊk o
)
ÊÊo p
{
ËË 	
return
ÌÌ 
DACConsulta
ÌÌ 
.
ÌÌ &
PlanMejoraArchivoporTipo
ÌÌ 7
(
ÌÌ7 8
idPlan
ÌÌ8 >
,
ÌÌ> ?
tipo
ÌÌ@ D
)
ÌÌD E
;
ÌÌE F
}
ÍÍ 	
public
ÏÏ 
int
ÏÏ *
InsertarAmpliacionPlanMejora
ÏÏ /
(
ÏÏ/ 0!
log_PM_ampliaciones
ÏÏ0 C
obj
ÏÏD G
)
ÏÏG H
{
ĞĞ 	
return
ÑÑ 

DACInserta
ÑÑ 
.
ÑÑ *
InsertarAmpliacionPlanMejora
ÑÑ :
(
ÑÑ: ;
obj
ÑÑ; >
)
ÑÑ> ?
;
ÑÑ? @
}
ÒÒ 	
public
ÔÔ 
int
ÔÔ ,
ActualizarPlanEgresoAmpliacion
ÔÔ 1
(
ÔÔ1 2
DateTime
ÔÔ2 :
?
ÔÔ: ;
fechaAmpliacion
ÔÔ< K
,
ÔÔK L
string
ÔÔM S
obsAmpliacion
ÔÔT a
,
ÔÔa b
int
ÔÔc f
?
ÔÔf g
idPlan
ÔÔh n
)
ÔÔn o
{
ÕÕ 	
return
ÖÖ 
DACActualiza
ÖÖ 
.
ÖÖ  ,
ActualizarPlanEgresoAmpliacion
ÖÖ  >
(
ÖÖ> ?
fechaAmpliacion
ÖÖ? N
,
ÖÖN O
obsAmpliacion
ÖÖP ]
,
ÖÖ] ^
idPlan
ÖÖ_ e
)
ÖÖe f
;
ÖÖf g
}
×× 	
public
ÙÙ ,
ecop_plan_de_mejora_documental
ÙÙ -+
PlanMejoraGestionDocumentalId
ÙÙ. K
(
ÙÙK L
int
ÙÙL O
?
ÙÙO P
idPlan
ÙÙQ W
,
ÙÙW X
int
ÙÙY \
?
ÙÙ\ ]
tipo
ÙÙ^ b
)
ÙÙb c
{
ÚÚ 	
return
ÛÛ 
DACConsulta
ÛÛ 
.
ÛÛ +
PlanMejoraGestionDocumentalId
ÛÛ <
(
ÛÛ< =
idPlan
ÛÛ= C
,
ÛÛC D
tipo
ÛÛE I
)
ÛÛI J
;
ÛÛJ K
}
ÜÜ 	
public
ŞŞ ,
ecop_plan_de_mejora_documental
ŞŞ -2
$PlanMejoraGestionDocumentalIdArchivo
ŞŞ. R
(
ŞŞR S
int
ŞŞS V
?
ŞŞV W
	idArchivo
ŞŞX a
)
ŞŞa b
{
ßß 	
return
àà 
DACConsulta
àà 
.
àà 2
$PlanMejoraGestionDocumentalIdArchivo
àà C
(
ààC D
	idArchivo
ààD M
)
ààM N
;
ààN O
}
áá 	
public
ãã 
void
ãã 
EliminarArchivoPM
ãã %
(
ãã% &
int
ãã& )
?
ãã) *
	idArchivo
ãã+ 4
,
ãã4 5
ref
ãã6 9 
MessageResponseOBJ
ãã: L
MsgRes
ããM S
)
ããS T
{
ää 	

DACElimina
åå 
.
åå 
EliminarArchivoPM
åå (
(
åå( )
	idArchivo
åå) 2
,
åå2 3
ref
åå4 7
MsgRes
åå8 >
)
åå> ?
;
åå? @
}
ææ 	
public
èè !
ecop_plan_de_mejora
èè "
PlanMejoraId
èè# /
(
èè/ 0
int
èè0 3
?
èè3 4
idPlan
èè5 ;
)
èè; <
{
éé 	
return
êê 
DACConsulta
êê 
.
êê 
PlanMejoraId
êê +
(
êê+ ,
idPlan
êê, 2
)
êê2 3
;
êê3 4
}
ëë 	
public
íí 
List
íí 
<
íí !
ref_medicion_riesgo
íí '
>
íí' (&
PlanMejoraMedicionRiesgo
íí) A
(
ííA B
)
ííB C
{
îî 	
return
ïï 
DACConsulta
ïï 
.
ïï &
PlanMejoraMedicionRiesgo
ïï 7
(
ïï7 8
)
ïï8 9
;
ïï9 :
}
ğğ 	
public
òò 
List
òò 
<
òò "
ref_costos_noCalidad
òò (
>
òò( )'
PlanMejoraCostosNoCalidad
òò* C
(
òòC D
)
òòD E
{
óó 	
return
ôô 
DACConsulta
ôô 
.
ôô '
PlanMejoraCostosNoCalidad
ôô 8
(
ôô8 9
)
ôô9 :
;
ôô: ;
}
õõ 	
public
÷÷ 
List
÷÷ 
<
÷÷ 
ref_cobertura
÷÷ !
>
÷÷! "!
PlanMejoraCobertura
÷÷# 6
(
÷÷6 7
)
÷÷7 8
{
øø 	
return
ùù 
DACConsulta
ùù 
.
ùù !
PlanMejoraCobertura
ùù 2
(
ùù2 3
)
ùù3 4
;
ùù4 5
}
úú 	
public
üü 
List
üü 
<
üü 3
%management_planmejora_tarea_obsResult
üü 9
>
üü9 :
GetobsTareas
üü; G
(
üüG H
Int32
üüH M#
id_plan_mejora_tareas
üüN c
,
üüc d
ref
üüe h 
MessageResponseOBJ
üüi {
MsgResüü| ‚
)üü‚ ƒ
{
ıı 	
return
şş 
DACConsulta
şş 
.
şş 
GetobsTareas
şş +
(
şş+ ,#
id_plan_mejora_tareas
şş, A
,
şşA B
ref
şşC F
MsgRes
şşG M
)
şşM N
;
şşN O
}
ÿÿ 	
public
 
Int32
 $
InsertarPlanMejora_obs
 +
(
+ ,)
ecop_plan_mejora_obs_tareas
, G
obj
H K
,
K L
ref
M P 
MessageResponseOBJ
Q c
MsgRes
d j
)
j k
{
‚‚ 	
return
ƒƒ 

DACInserta
ƒƒ 
.
ƒƒ $
InsertarPlanMejora_obs
ƒƒ 4
(
ƒƒ4 5
obj
ƒƒ5 8
,
ƒƒ8 9
ref
ƒƒ: =
MsgRes
ƒƒ> D
)
ƒƒD E
;
ƒƒE F
}
„„ 	
public
†† 
void
†† )
Actualizarplan_estado_tarea
†† /
(
††/ 0
int
††0 3#
id_plan_mejora_tareas
††4 I
,
††I J
Int32
††K P
estado
††Q W
,
††W X
ref
††Y \ 
MessageResponseOBJ
††] o
MsgRes
††p v
)
††v w
{
‡‡ 	
DACActualiza
ˆˆ 
.
ˆˆ )
Actualizarplan_estado_tarea
ˆˆ 4
(
ˆˆ4 5#
id_plan_mejora_tareas
ˆˆ5 J
,
ˆˆJ K
estado
ˆˆL R
,
ˆˆR S
ref
ˆˆT W
MsgRes
ˆˆX ^
)
ˆˆ^ _
;
ˆˆ_ `
}
‰‰ 	
public
‹‹ 
List
‹‹ 
<
‹‹ >
0management_planmejora_tarea_control_estadoResult
‹‹ D
>
‹‹D E-
CuentadetalleTareacontrolEstado
‹‹F e
(
‹‹e f
Int32
‹‹f k
id_plan_mejora
‹‹l z
,
‹‹z {
ref
‹‹| "
MessageResponseOBJ‹‹€ ’
MsgRes‹‹“ ™
)‹‹™ š
{
ŒŒ 	
return
 
DACConsulta
 
.
 -
CuentadetalleTareacontrolEstado
 >
(
> ?
id_plan_mejora
? M
,
M N
ref
O R
MsgRes
S Y
)
Y Z
;
Z [
}
 	
public
 
Int32
 +
InsertarPlanMejora_documentos
 2
(
2 3,
ecop_plan_de_mejora_documental
3 Q
obj
R U
)
U V
{
‘‘ 	
return
’’ 

DACInserta
’’ 
.
’’ +
InsertarPlanMejora_documentos
’’ ;
(
’’; <
obj
’’< ?
)
’’? @
;
’’@ A
}
““ 	
public
•• 
List
•• 
<
•• 8
*management_planMejora_tableroVisitasResult
•• >
>
••> ?
DatosPMVisitas
••@ N
(
••N O
string
••O U
auditor
••V ]
)
••] ^
{
–– 	
return
—— 
DACConsulta
—— 
.
—— 
DatosPMVisitas
—— -
(
——- .
auditor
——. 5
)
——5 6
;
——6 7
}
˜˜ 	
public
›› 
List
›› 
<
›› *
vw_planMejora_tableroVisitas
›› 0
>
››0 1!
DatosPMVisitasRoles
››2 E
(
››E F
)
››F G
{
œœ 	
return
 
DACConsulta
 
.
 !
DatosPMVisitasRoles
 2
(
2 3
)
3 4
;
4 5
}
 	
public
¤¤ 
List
¤¤ 
<
¤¤ 
Ref_valor_ahorro
¤¤ $
>
¤¤$ %
GetRefValorAhorro
¤¤& 7
(
¤¤7 8
)
¤¤8 9
{
¥¥ 	
return
¦¦ 
DACComonClass
¦¦  
.
¦¦  !
GetRefValorAhorro
¦¦! 2
(
¦¦2 3
)
¦¦3 4
;
¦¦4 5
}
§§ 	
public
©© 
void
©© *
InsertaConcurrenciaEvolucion
©© 0
(
©©0 1)
ecop_concurrencia_evolucion
©©1 L
	Evolucion
©©M V
,
©©V W
List
©©X \
<
©©\ ]9
*ecop_concurrencia_evolucion_procedimientos©©] ‡
>©©‡ ˆ
lista©©‰ 
,©© 
String©© –
UserName©©— Ÿ
,©©Ÿ  
String©©¡ §
	IPAddress©©¨ ±
,©©± ²
ref©©³ ¶"
MessageResponseOBJ©©· É
MsgRes©©Ê Ğ
)©©Ğ Ñ
{
ªª 	

DACInserta
«« 
.
«« *
InsertaConcurrenciaEvolucion
«« 3
(
««3 4
	Evolucion
««4 =
,
««= >
lista
««? D
,
««D E
UserName
««F N
,
««N O
	IPAddress
««P Y
,
««Y Z
ref
««[ ^
MsgRes
««_ e
)
««e f
;
««f g
}
¬¬ 	
public
®® 
List
®® 
<
®® )
ecop_concurrencia_evolucion
®® /
>
®®/ 0!
ConsultaEvoluciones
®®1 D
(
®®D E)
ecop_concurrencia_evolucion
®®E `
ObjEvolu
®®a i
,
®®i j
ref
®®k n!
MessageResponseOBJ®®o 
MsgRes®®‚ ˆ
)®®ˆ ‰
{
¯¯ 	
return
°° 
DACConsulta
°° 
.
°° !
ConsultaEvoluciones
°° 2
(
°°2 3
ObjEvolu
°°3 ;
,
°°; <
ref
°°= @
MsgRes
°°A G
)
°°G H
;
°°H I
}
±± 	
public
³³ 
List
³³ 
<
³³ 2
$vw_evo_ecop_concurrencia_evoluciones
³³ 8
>
³³8 9$
ConsultaEvolucionesIps
³³: P
(
³³P Q2
$vw_evo_ecop_concurrencia_evoluciones
³³Q u
ObjEvolu
³³v ~
,
³³~ 
ref³³€ ƒ"
MessageResponseOBJ³³„ –
MsgRes³³— 
)³³ 
{
´´ 	
return
µµ 
DACConsulta
µµ 
.
µµ $
ConsultaEvolucionesIps
µµ 5
(
µµ5 6
ObjEvolu
µµ6 >
,
µµ> ?
ref
µµ@ C
MsgRes
µµD J
)
µµJ K
;
µµK L
}
¶¶ 	
public
¸¸ 
void
¸¸ +
EliminarConcurrenciaEvolucion
¸¸ 1
(
¸¸1 2)
ecop_concurrencia_evolucion
¸¸2 M
ObjEvolucion
¸¸N Z
,
¸¸Z [
String
¸¸\ b
UserName
¸¸c k
,
¸¸k l
String
¸¸m s
	IPAddress
¸¸t }
,
¸¸} ~
ref¸¸ ‚"
MessageResponseOBJ¸¸ƒ •
MsgRes¸¸– œ
)¸¸œ 
{
¹¹ 	

DACElimina
ºº 
.
ºº +
EliminarConcurrenciaEvolucion
ºº 4
(
ºº4 5
ObjEvolucion
ºº5 A
,
ººA B
UserName
ººC K
,
ººK L
	IPAddress
ººM V
,
ººV W
ref
ººX [
MsgRes
ºº\ b
)
ººb c
;
ººc d
}
»» 	
public
½½ 
void
½½  
EliminarPlanAccion
½½ &
(
½½& '8
*ecop_concurrencia_eventos_en_salud_detalle
½½' Q
OBJ
½½R U
,
½½U V
ref
½½W Z 
MessageResponseOBJ
½½[ m
MsgRes
½½n t
)
½½t u
{
¾¾ 	

DACElimina
¿¿ 
.
¿¿  
EliminarPlanAccion
¿¿ )
(
¿¿) *
OBJ
¿¿* -
,
¿¿- .
ref
¿¿/ 2
MsgRes
¿¿3 9
)
¿¿9 :
;
¿¿: ;
}
ÀÀ 	
public
ÂÂ 
List
ÂÂ 
<
ÂÂ 2
$ecop_concurrencia_evolucion_diag_def
ÂÂ 8
>
ÂÂ8 9+
ConsultaDiagnosticoDefinitivo
ÂÂ: W
(
ÂÂW X2
$ecop_concurrencia_evolucion_diag_def
ÂÂX |

ObjdiagdefÂÂ} ‡
,ÂÂ‡ ˆ
refÂÂ‰ Œ"
MessageResponseOBJÂÂ Ÿ
MsgResÂÂ  ¦
)ÂÂ¦ §
{
ÃÃ 	
return
ÄÄ 
DACConsulta
ÄÄ 
.
ÄÄ +
ConsultaDiagnosticoDefinitivo
ÄÄ <
(
ÄÄ< =

Objdiagdef
ÄÄ= G
,
ÄÄG H
ref
ÄÄI L
MsgRes
ÄÄM S
)
ÄÄS T
;
ÄÄT U
}
ÅÅ 	
public
ÇÇ 
List
ÇÇ 
<
ÇÇ '
ecop_concurrencia_cohorte
ÇÇ -
>
ÇÇ- .
ConsultaCohorte
ÇÇ/ >
(
ÇÇ> ?'
ecop_concurrencia_cohorte
ÇÇ? X

ObjCohorte
ÇÇY c
,
ÇÇc d
ref
ÇÇe h 
MessageResponseOBJ
ÇÇi {
MsgResÇÇ| ‚
)ÇÇ‚ ƒ
{
ÈÈ 	
return
ÉÉ 
DACConsulta
ÉÉ 
.
ÉÉ 
ConsultaCohorte
ÉÉ .
(
ÉÉ. /

ObjCohorte
ÉÉ/ 9
,
ÉÉ9 :
ref
ÉÉ; >
MsgRes
ÉÉ? E
)
ÉÉE F
;
ÉÉF G
}
ÊÊ 	
public
ÌÌ 
void
ÌÌ *
InsertaDiagnosticoDefinitivo
ÌÌ 0
(
ÌÌ0 12
$ecop_concurrencia_evolucion_diag_def
ÌÌ1 U4
&Concurrencia_Diagnostico_Definitivo_id
ÌÌV |
,
ÌÌ| }
StringÌÌ~ „
UserNameÌÌ… 
,ÌÌ 
StringÌÌ •
	IPAddressÌÌ– Ÿ
,ÌÌŸ  
refÌÌ¡ ¤"
MessageResponseOBJÌÌ¥ ·
MsgResÌÌ¸ ¾
)ÌÌ¾ ¿
{
ÍÍ 	

DACInserta
ÎÎ 
.
ÎÎ *
InsertaDiagnosticoDefinitivo
ÎÎ 3
(
ÎÎ3 44
&Concurrencia_Diagnostico_Definitivo_id
ÎÎ4 Z
,
ÎÎZ [
UserName
ÎÎ\ d
,
ÎÎd e
	IPAddress
ÎÎf o
,
ÎÎo p
ref
ÎÎq t
MsgRes
ÎÎu {
)
ÎÎ{ |
;
ÎÎ| }
}
ÏÏ 	
public
ÑÑ 
void
ÑÑ 
InsertaGlosa
ÑÑ  
(
ÑÑ  !%
ecop_concurrencia_glosa
ÑÑ! 8
ObjGlosa
ÑÑ9 A
,
ÑÑA B
String
ÑÑC I
UserName
ÑÑJ R
,
ÑÑR S
String
ÑÑT Z
	IPAddress
ÑÑ[ d
,
ÑÑd e
ref
ÑÑf i 
MessageResponseOBJ
ÑÑj |
MsgResÑÑ} ƒ
)ÑÑƒ „
{
ÒÒ 	

DACInserta
ÓÓ 
.
ÓÓ 
InsertaGlosa
ÓÓ #
(
ÓÓ# $
ObjGlosa
ÓÓ$ ,
,
ÓÓ, -
UserName
ÓÓ. 6
,
ÓÓ6 7
	IPAddress
ÓÓ8 A
,
ÓÓA B
ref
ÓÓC F
MsgRes
ÓÓG M
)
ÓÓM N
;
ÓÓN O
}
ÔÔ 	
public
ÖÖ 
List
ÖÖ 
<
ÖÖ %
ecop_concurrencia_glosa
ÖÖ +
>
ÖÖ+ ,
ConsultaGlosa
ÖÖ- :
(
ÖÖ: ;%
ecop_concurrencia_glosa
ÖÖ; R
ObjGlosa
ÖÖS [
,
ÖÖ[ \
ref
ÖÖ] ` 
MessageResponseOBJ
ÖÖa s
MsgRes
ÖÖt z
)
ÖÖz {
{
×× 	
return
ØØ 
DACConsulta
ØØ 
.
ØØ 
ConsultaGlosa
ØØ ,
(
ØØ, -
ObjGlosa
ØØ- 5
,
ØØ5 6
ref
ØØ7 :
MsgRes
ØØ; A
)
ØØA B
;
ØØB C
}
ÙÙ 	
public
ÛÛ 
List
ÛÛ 
<
ÛÛ %
ecop_concurrencia_glosa
ÛÛ +
>
ÛÛ+ ,+
ConsultaGlosabyidconcurrencia
ÛÛ- J
(
ÛÛJ K
int
ÛÛK N
idconcurrencia
ÛÛO ]
,
ÛÛ] ^
ref
ÛÛ_ b 
MessageResponseOBJ
ÛÛc u
MsgRes
ÛÛv |
)
ÛÛ| }
{
ÜÜ 	
return
İİ 
DACConsulta
İİ 
.
İİ +
ConsultaGlosabyidconcurrencia
İİ <
(
İİ< =
idconcurrencia
İİ= K
,
İİK L
ref
İİM P
MsgRes
İİQ W
)
İİW X
;
İİX Y
}
ŞŞ 	
public
àà 
List
àà 
<
àà &
ecop_concurrencia_ahorro
àà ,
>
àà, -
ConsultaAhorro
àà. <
(
àà< =&
ecop_concurrencia_ahorro
àà= U
	ObjAhorro
ààV _
,
àà_ `
ref
ààa d 
MessageResponseOBJ
ààe w
MsgRes
ààx ~
)
àà~ 
{
áá 	
return
ââ 
DACConsulta
ââ 
.
ââ 
ConsultaAhorro
ââ -
(
ââ- .
	ObjAhorro
ââ. 7
,
ââ7 8
ref
ââ9 <
MsgRes
ââ= C
)
ââC D
;
ââD E
}
ãã 	
public
åå 
List
åå 
<
åå )
vw_ecop_concurrencia_ahorro
åå /
>
åå/ 0 
ConsultaAhorroOtro
åå1 C
(
ååC D)
vw_ecop_concurrencia_ahorro
ååD _
	ObjAhorro
åå` i
,
ååi j
ref
ååk n!
MessageResponseOBJååo 
MsgResåå‚ ˆ
)ååˆ ‰
{
ææ 	
return
çç 
DACConsulta
çç 
.
çç  
ConsultaAhorroOtro
çç 1
(
çç1 2
	ObjAhorro
çç2 ;
,
çç; <
ref
çç= @
MsgRes
ççA G
)
ççG H
;
ççH I
}
èè 	
public
êê 
List
êê 
<
êê *
vw_ecop_concurrencia_cohorte
êê 0
>
êê0 1
ConsultaCohorte
êê2 A
(
êêA B*
vw_ecop_concurrencia_cohorte
êêB ^

ObjCohorte
êê_ i
,
êêi j
ref
êêk n!
MessageResponseOBJêêo 
MsgResêê‚ ˆ
)êêˆ ‰
{
ëë 	
return
ìì 
DACConsulta
ìì 
.
ìì 
ConsultaCohorte
ìì .
(
ìì. /

ObjCohorte
ìì/ 9
,
ìì9 :
ref
ìì; >
MsgRes
ìì? E
)
ììE F
;
ììF G
}
íí 	
public
ğğ 
List
ğğ 
<
ğğ "
Ref_eventos_adversos
ğğ (
>
ğğ( ) 
GetEventosAdversos
ğğ* <
(
ğğ< =
)
ğğ= >
{
ññ 	
return
òò 
DACComonClass
òò  
.
òò  ! 
GetEventosAdversos
òò! 3
(
òò3 4
)
òò4 5
;
òò5 6
}
óó 	
public
ôô 
List
ôô 
<
ôô 
Ref_grado_lesion
ôô $
>
ôô$ %
GetGradoLesion
ôô& 4
(
ôô4 5
)
ôô5 6
{
õõ 	
return
öö 
DACComonClass
öö  
.
öö  !
GetGradoLesion
öö! /
(
öö/ 0
)
öö0 1
;
öö1 2
}
÷÷ 	
public
øø 
List
øø 
<
øø )
Ref_factores_contribuyentes
øø /
>
øø/ 0'
GetFactoresContribuyentes
øø1 J
(
øøJ K
)
øøK L
{
ùù 	
return
úú 
DACComonClass
úú  
.
úú  !'
GetFactoresContribuyentes
úú! :
(
úú: ;
)
úú; <
;
úú< =
}
ûû 	
public
üü 
List
üü 
<
üü $
Ref_barreras_seguridad
üü *
>
üü* +$
GetBarrerasDeSeguridad
üü, B
(
üüB C
)
üüC D
{
ıı 	
return
şş 
DACComonClass
şş  
.
şş  !$
GetBarrerasDeSeguridad
şş! 7
(
şş7 8
)
şş8 9
;
şş9 :
}
ÿÿ 	
public
€€ 
List
€€ 
<
€€ $
Ref_acciones_inseguras
€€ *
>
€€* +"
GetAccionesInseguras
€€, @
(
€€@ A
)
€€A B
{
 	
return
‚‚ 
DACComonClass
‚‚  
.
‚‚  !"
GetAccionesInseguras
‚‚! 5
(
‚‚5 6
)
‚‚6 7
;
‚‚7 8
}
ƒƒ 	
public
„„ 
List
„„ 
<
„„  
Ref_plan_de_manejo
„„ &
>
„„& '
GetPlanDeManejo
„„( 7
(
„„7 8
)
„„8 9
{
…… 	
return
†† 
DACComonClass
††  
.
††  !
GetPlanDeManejo
††! 0
(
††0 1
)
††1 2
;
††2 3
}
‡‡ 	
public
‰‰ 
void
‰‰ "
InsertaEventoAdverso
‰‰ (
(
‰‰( )0
"ecop_concurrencia_eventos_adversos
‰‰) K
ObjEventoAdv
‰‰L X
,
‰‰X Y
String
‰‰Z `
UserName
‰‰a i
,
‰‰i j
String
‰‰k q
	IPAddress
‰‰r {
,
‰‰{ |
ref‰‰} €"
MessageResponseOBJ‰‰ “
MsgRes‰‰” š
)‰‰š ›
{
ŠŠ 	

DACInserta
‹‹ 
.
‹‹ "
InsertaEventoAdverso
‹‹ +
(
‹‹+ ,
ObjEventoAdv
‹‹, 8
,
‹‹8 9
UserName
‹‹: B
,
‹‹B C
	IPAddress
‹‹D M
,
‹‹M N
ref
‹‹O R
MsgRes
‹‹S Y
)
‹‹Y Z
;
‹‹Z [
}
ŒŒ 	
public
 
List
 
<
 0
"ecop_concurrencia_eventos_adversos
 6
>
6 7#
ConsultaEventoAdverso
8 M
(
M N0
"ecop_concurrencia_eventos_adversos
N p
ObjEventoAdversoq 
, ‚
refƒ †"
MessageResponseOBJ‡ ™
MsgResš  
)  ¡
{
 	
return
 
DACConsulta
 
.
 #
ConsultaEventoAdverso
 4
(
4 5
ObjEventoAdverso
5 E
,
E F
ref
G J
MsgRes
K Q
)
Q R
;
R S
}
‘‘ 	
public
““ 
List
““ 
<
““ (
Ref_situaciones_de_calidad
““ .
>
““. /%
GetSituacionesDeCalidad
““0 G
(
““G H
)
““H I
{
”” 	
return
•• 
DACComonClass
••  
.
••  !%
GetSituacionesDeCalidad
••! 8
(
••8 9
)
••9 :
;
••: ;
}
–– 	
public
˜˜ 
void
˜˜ '
InsertaSituacionesCalidad
˜˜ -
(
˜˜- .6
(ecop_concurrencia_situaciones_de_calidad
˜˜. V
ObjSituacionCalid
˜˜W h
,
˜˜h i
String
˜˜j p
UserName
˜˜q y
,
˜˜y z
String˜˜{ 
	IPAddress˜˜‚ ‹
,˜˜‹ Œ
ref˜˜ "
MessageResponseOBJ˜˜‘ £
MsgRes˜˜¤ ª
)˜˜ª «
{
™™ 	

DACInserta
šš 
.
šš '
InsertaSituacionesCalidad
šš 0
(
šš0 1
ObjSituacionCalid
šš1 B
,
ššB C
UserName
ššD L
,
ššL M
	IPAddress
ššN W
,
ššW X
ref
ššY \
MsgRes
šš] c
)
ššc d
;
ššd e
}
›› 	
public
 
List
 
<
 6
(ecop_concurrencia_situaciones_de_calidad
 <
>
< =(
ConsultaSituacionesCalidad
> X
(
X Y7
(ecop_concurrencia_situaciones_de_calidadY 
ObjSituCali‚ 
, 
ref ’"
MessageResponseOBJ“ ¥
MsgRes¦ ¬
)¬ ­
{
 	
return
ŸŸ 
DACConsulta
ŸŸ 
.
ŸŸ (
ConsultaSituacionesCalidad
ŸŸ 9
(
ŸŸ9 :
ObjSituCali
ŸŸ: E
,
ŸŸE F
ref
ŸŸG J
MsgRes
ŸŸK Q
)
ŸŸQ R
;
ŸŸR S
}
   	
public
¡¡ 
List
¡¡ 
<
¡¡ 2
$Ref_motivo_cancelacion_procedimiento
¡¡ 8
>
¡¡8 9"
GetMotivoCancelacion
¡¡: N
(
¡¡N O
)
¡¡O P
{
¢¢ 	
return
££ 
DACComonClass
££  
.
££  !"
GetMotivoCancelacion
££! 5
(
££5 6
)
££6 7
;
££7 8
}
¤¤ 	
public
¦¦ 
void
¦¦ 4
&InsertaProcedimientoQuirugicoCancelado
¦¦ :
(
¦¦: ;E
7ecop_concurrencia_procedimientos_quirurgicos_cancelados
¦¦; r%
ProcedimientoQuirCance¦¦s ‰
,¦¦‰ Š
String¦¦‹ ‘
UserName¦¦’ š
,¦¦š ›
String¦¦œ ¢
	IPAddress¦¦£ ¬
,¦¦¬ ­
ref¦¦® ±"
MessageResponseOBJ¦¦² Ä
MsgRes¦¦Å Ë
)¦¦Ë Ì
{
§§ 	

DACInserta
¨¨ 
.
¨¨ 4
&InsertaProcedimientoQuirugicoCancelado
¨¨ =
(
¨¨= >$
ProcedimientoQuirCance
¨¨> T
,
¨¨T U
UserName
¨¨V ^
,
¨¨^ _
	IPAddress
¨¨` i
,
¨¨i j
ref
¨¨k n
MsgRes
¨¨o u
)
¨¨u v
;
¨¨v w
}
©© 	
public
ªª 
List
ªª 
<
ªª E
7ecop_concurrencia_procedimientos_quirurgicos_cancelados
ªª K
>
ªªK L*
ConsultaProcQuirurgicosCance
ªªM i
(
ªªi jF
7ecop_concurrencia_procedimientos_quirurgicos_canceladosªªj ¡
ObjProcQuirªª¢ ­
,ªª­ ®
refªª¯ ²"
MessageResponseOBJªª³ Å
MsgResªªÆ Ì
)ªªÌ Í
{
«« 	
return
¬¬ 
DACConsulta
¬¬ 
.
¬¬ *
ConsultaProcQuirurgicosCance
¬¬ ;
(
¬¬; <
ObjProcQuir
¬¬< G
,
¬¬G H
ref
¬¬I L
MsgRes
¬¬M S
)
¬¬S T
;
¬¬T U
}
­­ 	
public
¯¯ 
void
¯¯ 
InsertarNatalidad
¯¯ %
(
¯¯% &(
natalidad_sin_concurrencia
¯¯& @
	Natalidad
¯¯A J
,
¯¯J K
ref
¯¯L O 
MessageResponseOBJ
¯¯P b
MsgRes
¯¯c i
)
¯¯i j
{
°° 	

DACInserta
±± 
.
±± 
InsertarNatalidad
±± (
(
±±( )
	Natalidad
±±) 2
,
±±2 3
ref
±±4 7
MsgRes
±±8 >
)
±±> ?
;
±±? @
}
²² 	
public
´´ 
void
´´  
InsertarMortalidad
´´ &
(
´´& ')
mortalidad_sin_concurrencia
´´' B

Mortalidad
´´C M
,
´´M N
ref
´´O R 
MessageResponseOBJ
´´S e
MsgRes
´´f l
)
´´l m
{
µµ 	

DACInserta
¶¶ 
.
¶¶  
InsertarMortalidad
¶¶ )
(
¶¶) *

Mortalidad
¶¶* 4
,
¶¶4 5
ref
¶¶6 9
MsgRes
¶¶: @
)
¶¶@ A
;
¶¶A B
}
·· 	
public
¸¸ 
List
¸¸ 
<
¸¸ )
vw_tablero_eventos_adversos
¸¸ /
>
¸¸/ 0#
ReportesEventoAdverso
¸¸1 F
(
¸¸F G
)
¸¸G H
{
¹¹ 	
return
ºº 
DACConsulta
ºº 
.
ºº #
ReportesEventoAdverso
ºº 4
(
ºº4 5
)
ºº5 6
;
ºº6 7
}
»» 	
public
½½ 
void
½½ )
InsertarAlertasConcurrencia
½½ /
(
½½/ 0,
alertas_generadas_concurrencia
½½0 N
Alertas
½½O V
,
½½V W
ref
½½X [ 
MessageResponseOBJ
½½\ n
MsgRes
½½o u
)
½½u v
{
¾¾ 	

DACInserta
¿¿ 
.
¿¿ )
InsertarAlertasConcurrencia
¿¿ 2
(
¿¿2 3
Alertas
¿¿3 :
,
¿¿: ;
ref
¿¿< ?
MsgRes
¿¿@ F
)
¿¿F G
;
¿¿G H
}
ÀÀ 	
public
ÂÂ 
void
ÂÂ (
InsertarConcurrenciaAhorro
ÂÂ .
(
ÂÂ. /&
ecop_concurrencia_ahorro
ÂÂ/ G
Ahorro
ÂÂH N
,
ÂÂN O
ref
ÂÂP S 
MessageResponseOBJ
ÂÂT f
MsgRes
ÂÂg m
)
ÂÂm n
{
ÃÃ 	

DACInserta
ÄÄ 
.
ÄÄ (
InsertarConcurrenciaAhorro
ÄÄ 1
(
ÄÄ1 2
Ahorro
ÄÄ2 8
,
ÄÄ8 9
ref
ÄÄ: =
MsgRes
ÄÄ> D
)
ÄÄD E
;
ÄÄE F
}
ÅÅ 	
public
ÇÇ 
void
ÇÇ )
InsertarConcurrenciaCohorte
ÇÇ /
(
ÇÇ/ 0'
ecop_concurrencia_cohorte
ÇÇ0 I
Cohorte
ÇÇJ Q
,
ÇÇQ R
ref
ÇÇS V 
MessageResponseOBJ
ÇÇW i
MsgRes
ÇÇj p
)
ÇÇp q
{
ÈÈ 	

DACInserta
ÉÉ 
.
ÉÉ )
InsertarConcurrenciaCohorte
ÉÉ 2
(
ÉÉ2 3
Cohorte
ÉÉ3 :
,
ÉÉ: ;
ref
ÉÉ< ?
MsgRes
ÉÉ@ F
)
ÉÉF G
;
ÉÉG H
}
ÊÊ 	
public
ÌÌ 
List
ÌÌ 
<
ÌÌ 
Ref_causal_glosa
ÌÌ $
>
ÌÌ$ %!
ConsultaCausalGlosa
ÌÌ& 9
(
ÌÌ9 :
int
ÌÌ: =!
id_respnsable_glosa
ÌÌ> Q
,
ÌÌQ R
ref
ÌÌS V 
MessageResponseOBJ
ÌÌW i
MsgRes
ÌÌj p
)
ÌÌp q
{
ÍÍ 	
return
ÎÎ 
DACConsulta
ÎÎ 
.
ÎÎ !
ConsultaCausalGlosa
ÎÎ 2
(
ÎÎ2 3!
id_respnsable_glosa
ÎÎ3 F
,
ÎÎF G
ref
ÎÎH K
MsgRes
ÎÎL R
)
ÎÎR S
;
ÎÎS T
}
ÏÏ 	
public
ÓÓ 
List
ÓÓ 
<
ÓÓ ,
alertas_generadas_concurrencia
ÓÓ 2
>
ÓÓ2 3)
ConsultaAlertasConcurrencia
ÓÓ4 O
(
ÓÓO P
Int32
ÓÓP U
Idalerta
ÓÓV ^
,
ÓÓ^ _
string
ÓÓ` f
idcie10
ÓÓg n
,
ÓÓn o
ref
ÓÓp s!
MessageResponseOBJÓÓt †
MsgResÓÓ‡ 
)ÓÓ 
{
ÔÔ 	
return
ÕÕ 
DACConsulta
ÕÕ 
.
ÕÕ )
ConsultaAlertasConcurrencia
ÕÕ :
(
ÕÕ: ;
Idalerta
ÕÕ; C
,
ÕÕC D
idcie10
ÕÕE L
,
ÕÕL M
ref
ÕÕN Q
MsgRes
ÕÕR X
)
ÕÕX Y
;
ÕÕY Z
}
ÖÖ 	
public
ØØ 
vw_cie10_alertas
ØØ !
ConsultaAlertaCie10
ØØ  3
(
ØØ3 4
String
ØØ4 :
idcie10
ØØ; B
,
ØØB C
ref
ØØD G 
MessageResponseOBJ
ØØH Z
MsgRes
ØØ[ a
)
ØØa b
{
ÙÙ 	
return
ÚÚ 
DACConsulta
ÚÚ 
.
ÚÚ !
ConsultaAlertaCie10
ÚÚ 2
(
ÚÚ2 3
idcie10
ÚÚ3 :
,
ÚÚ: ;
ref
ÚÚ< ?
MsgRes
ÚÚ@ F
)
ÚÚF G
;
ÚÚG H
}
ÛÛ 	
public
ÜÜ 
ref_cie10_detalle
ÜÜ  (
ConsultaAlertaCie10Detalle
ÜÜ! ;
(
ÜÜ; <
String
ÜÜ< B
idcie10
ÜÜC J
,
ÜÜJ K
ref
ÜÜL O 
MessageResponseOBJ
ÜÜP b
MsgRes
ÜÜc i
)
ÜÜi j
{
İİ 	
return
ŞŞ 
DACConsulta
ŞŞ 
.
ŞŞ (
ConsultaAlertaCie10Detalle
ŞŞ 9
(
ŞŞ9 :
idcie10
ŞŞ: A
,
ŞŞA B
ref
ŞŞC F
MsgRes
ŞŞG M
)
ŞŞM N
;
ŞŞN O
}
ßß 	
public
àà 
List
àà 
<
àà $
analisis_caso_original
àà *
>
àà* +3
%ConsultaEvolucionAnalisisCasoOriginal
àà, Q
(
ààQ R
Int32
ààR W
?
ààW X
idconcurrencia
ààY g
,
ààg h
Int32
àài n
?
ààn o
idanalisiscaso
ààp ~
,
àà~ 
refàà€ ƒ"
MessageResponseOBJàà„ –
MsgResàà— 
)àà 
{
áá 	
return
ââ 
DACConsulta
ââ 
.
ââ 3
%ConsultaEvolucionAnalisisCasoOriginal
ââ D
(
ââD E
idconcurrencia
ââE S
,
ââS T
idanalisiscaso
ââU c
,
ââc d
ref
ââe h
MsgRes
ââi o
)
ââo p
;
ââp q
}
ãã 	
public
åå 
List
åå 
<
åå 
Ref_valor_ahorro
åå $
>
åå$ %
ValorAhorro
åå& 1
(
åå1 2
)
åå2 3
{
ææ 	
return
çç 
DACConsulta
çç 
.
çç 
ValorAhorro
çç *
(
çç* +
)
çç+ ,
;
çç, -
}
èè 	
public
êê 
List
êê 
<
êê 2
$vw_evo_ecop_concurrencia_evoluciones
êê 8
>
êê8 9*
GetConcurrenciaEvolucionById
êê: V
(
êêV W
int
êêW Z
id_evolucion
êê[ g
)
êêg h
{
ëë 	
return
ìì 
DACConsulta
ìì 
.
ìì *
GetConcurrenciaEvolucionById
ìì ;
(
ìì; <
id_evolucion
ìì< H
)
ììH I
;
ììI J
}
íí 	
public
ïï 
void
ïï -
MandarConcurrenciaContactCenter
ïï 3
(
ïï3 4
List
ïï4 8
<
ïï8 9
int
ïï9 <
>
ïï< =
listado
ïï> E
,
ïïE F
ref
ïïG J 
MessageResponseOBJ
ïïK ]
MsgRes
ïï^ d
)
ïïd e
{
ğğ 	
DACActualiza
ññ 
.
ññ -
MandarConcurrenciaContactCenter
ññ 8
(
ññ8 9
listado
ññ9 @
,
ññ@ A
ref
ññB E
MsgRes
ññF L
)
ññL M
;
ññM N
}
òò 	
public
ôô 
void
ôô 7
)MandarindividualConcurrenciaContactCenter
ôô =
(
ôô= >
int
ôô> A
?
ôôA B
idConcu
ôôC J
,
ôôJ K
string
ôôL R
observacion
ôôS ^
,
ôô^ _
ref
ôô` c 
MessageResponseOBJ
ôôd v
MsgRes
ôôw }
)
ôô} ~
{
õõ 	
DACActualiza
öö 
.
öö 7
)MandarindividualConcurrenciaContactCenter
öö B
(
ööB C
idConcu
ööC J
,
ööJ K
observacion
ööL W
,
ööW X
ref
ööY \
MsgRes
öö] c
)
ööc d
;
ööd e
}
÷÷ 	
public
øø 0
"vw_ecop_evo_evaluacion_pertinencia
øø 1*
GetEvaluacionPertinenciaById
øø2 N
(
øøN O
int
øøO R
idevolucion
øøS ^
)
øø^ _
{
ùù 	
return
úú 
DACConsulta
úú 
.
úú *
GetEvaluacionPertinenciaById
úú ;
(
úú; <
idevolucion
úú< G
)
úúG H
;
úúH I
}
ûû 	
public
ıı 
List
ıı 
<
ıı 4
&management_evolucionEgresosListaResult
ıı :
>
ıı: ;(
GetEvolucionesConcurrencia
ıı< V
(
ııV W
int
ııW Z
idConcu
ıı[ b
)
ııb c
{
şş 	
return
ÿÿ 
DACConsulta
ÿÿ 
.
ÿÿ (
GetEvolucionesConcurrencia
ÿÿ 9
(
ÿÿ9 :
idConcu
ÿÿ: A
)
ÿÿA B
;
ÿÿB C
}
€€ 	
public
‚‚ 
List
‚‚ 
<
‚‚ 7
)Management_evolucion_procedimientosResult
‚‚ =
>
‚‚= >0
"ConsultaProcedimientosConcurrencia
‚‚? a
(
‚‚a b
ref
‚‚b e 
MessageResponseOBJ
‚‚f x
MsgRes
‚‚y 
)‚‚ €
{
ƒƒ 	
return
„„ 
DACConsulta
„„ 
.
„„ 0
"ConsultaProcedimientosConcurrencia
„„ A
(
„„A B
ref
„„B E
MsgRes
„„F L
)
„„L M
;
„„M N
}
…… 	
public
ŠŠ 
List
ŠŠ 
<
ŠŠ +
ManagmentAlertasCalidadResult
ŠŠ 1
>
ŠŠ1 2
CuentaFechaCargue
ŠŠ3 D
(
ŠŠD E
Int32
ŠŠE J
Opc
ŠŠK N
,
ŠŠN O
DateTime
ŠŠP X
FechaInicial
ŠŠY e
,
ŠŠe f
DateTime
ŠŠg o
FechaFin
ŠŠp x
,
ŠŠx y
StringŠŠz €
strProveedorŠŠ 
,ŠŠ 
StringŠŠ •
	strEstadoŠŠ– Ÿ
,ŠŠŸ  
refŠŠ¡ ¤"
MessageResponseOBJŠŠ¥ ·
MsgResŠŠ¸ ¾
)ŠŠ¾ ¿
{
‹‹ 	
return
ŒŒ 
DACConsulta
ŒŒ 
.
ŒŒ 
CuentaFechaCargue
ŒŒ 0
(
ŒŒ0 1
Opc
ŒŒ1 4
,
ŒŒ4 5
FechaInicial
ŒŒ6 B
,
ŒŒB C
FechaFin
ŒŒD L
,
ŒŒL M
strProveedor
ŒŒN Z
,
ŒŒZ [
	strEstado
ŒŒ\ e
,
ŒŒe f
ref
ŒŒg j
MsgRes
ŒŒk q
)
ŒŒq r
;
ŒŒr s
}
 	
public
 
List
 
<
 +
vw_Devoluciones_sin_gestionar
 1
>
1 2$
DevolucionesSinGestion
3 I
(
I J
)
J K
{
 	
return
‘‘ 
DACConsulta
‘‘ 
.
‘‘ $
DevolucionesSinGestion
‘‘ 5
(
‘‘5 6
)
‘‘6 7
;
‘‘7 8
}
’’ 	
public
”” 
Int32
”” +
InsertarDevolucionGestionadas
”” 2
(
””2 3,
factura_devolucion_gestionadas
””3 Q
OBJ
””R U
,
””U V
ref
””W Z 
MessageResponseOBJ
””[ m
MsgRes
””n t
)
””t u
{
•• 	
return
–– 

DACInserta
–– 
.
–– +
InsertarDevolucionGestionadas
–– ;
(
––; <
OBJ
––< ?
,
––? @
ref
––A D
MsgRes
––E K
)
––K L
;
––L M
}
—— 	
public
™™ 
List
™™ 
<
™™ 
vw_hallazgos_RIPS
™™ %
>
™™% &%
HallazgosRipsSinGestion
™™' >
(
™™> ?
)
™™? @
{
šš 	
return
›› 
DACConsulta
›› 
.
›› %
HallazgosRipsSinGestion
›› 6
(
››6 7
)
››7 8
;
››8 9
}
œœ 	
public
 
List
 
<
 %
vw_facturas_sin_auditar
 +
>
+ , 
FacturasporAuditar
- ?
(
? @
)
@ A
{
ŸŸ 	
return
   
DACConsulta
   
.
    
FacturasporAuditar
   1
(
  1 2
)
  2 3
;
  3 4
}
¡¡ 	
public
££ 
List
££ 
<
££ 
vw_costo_evitado
££ $
>
££$ %
CostoEvitado
££& 2
(
££2 3
Int32
££3 8
Id
££9 ;
,
££; <
ref
££= @ 
MessageResponseOBJ
££A S
MsgRes
££T Z
)
££Z [
{
¤¤ 	
return
¥¥ 
DACConsulta
¥¥ 
.
¥¥ 
CostoEvitado
¥¥ +
(
¥¥+ ,
Id
¥¥, .
,
¥¥. /
ref
¥¥0 3
MsgRes
¥¥4 :
)
¥¥: ;
;
¥¥; <
}
¦¦ 	
public
¨¨ 
List
¨¨ 
<
¨¨ &
vw_facturas_diagnosticos
¨¨ ,
>
¨¨, -!
DiagnosticosCuentas
¨¨. A
(
¨¨A B
Int32
¨¨B G
Id
¨¨H J
,
¨¨J K
ref
¨¨L O 
MessageResponseOBJ
¨¨P b
MsgRes
¨¨c i
)
¨¨i j
{
©© 	
return
ªª 
DACConsulta
ªª 
.
ªª !
DiagnosticosCuentas
ªª 2
(
ªª2 3
Id
ªª3 5
,
ªª5 6
ref
ªª7 :
MsgRes
ªª; A
)
ªªA B
;
ªªB C
}
«« 	
public
­­ 
List
­­ 
<
­­ )
vw_ECOPETROL_DEVOLUCION_FAC
­­ /
>
­­/ 0
VwDevoluciones
­­1 ?
(
­­? @
)
­­@ A
{
®® 	
return
¯¯ 
DACConsulta
¯¯ 
.
¯¯ 
VwDevoluciones
¯¯ -
(
¯¯- .
)
¯¯. /
;
¯¯/ 0
}
°° 	
public
²² 
List
²² 
<
²² )
vw_ECOPETROL_HALLAZGOS_RIPS
²² /
>
²²/ 0
VwHallazgosRIPS
²²1 @
(
²²@ A
)
²²A B
{
³³ 	
return
´´ 
DACConsulta
´´ 
.
´´ 
VwHallazgosRIPS
´´ .
(
´´. /
)
´´/ 0
;
´´0 1
}
µµ 	
public
·· 
List
·· 
<
·· *
ECOPETROL_RECEPCION_FACTURAS
·· 0
>
··0 1!
VwRecepcionFacturas
··2 E
(
··E F
)
··F G
{
¸¸ 	
return
¹¹ 
DACConsulta
¹¹ 
.
¹¹ !
VwRecepcionFacturas
¹¹ 2
(
¹¹2 3
)
¹¹3 4
;
¹¹4 5
}
ºº 	
public
¼¼ 
void
¼¼ 
InsertarMega
¼¼  
(
¼¼  !
List
¼¼! %
<
¼¼% &
megas_cargue_base
¼¼& 7
>
¼¼7 8
ListMega
¼¼9 A
,
¼¼A B
ref
¼¼C F 
MessageResponseOBJ
¼¼G Y
MsgRes
¼¼Z `
)
¼¼` a
{
½½ 	

DACInserta
¾¾ 
.
¾¾ 
InsertarMega
¾¾ #
(
¾¾# $
ListMega
¾¾$ ,
,
¾¾, -
ref
¾¾. 1
MsgRes
¾¾2 8
)
¾¾8 9
;
¾¾9 :
}
¿¿ 	
public
ÂÂ 
List
ÂÂ 
<
ÂÂ 9
+ManagmentClinicosCensoConConcurrenciaResult
ÂÂ ?
>
ÂÂ? @(
CensoConcurrenciaEcopetrol
ÂÂA [
(
ÂÂ[ \
DateTime
ÂÂ\ d
	fecha_ini
ÂÂe n
,
ÂÂn o
DateTime
ÂÂp x
fecha_finalÂÂy „
,ÂÂ„ …
refÂÂ† ‰"
MessageResponseOBJÂÂŠ œ
MsgResÂÂ £
)ÂÂ£ ¤
{
ÃÃ 	
return
ÄÄ 
DACConsulta
ÄÄ 
.
ÄÄ (
CensoConcurrenciaEcopetrol
ÄÄ 9
(
ÄÄ9 :
	fecha_ini
ÄÄ: C
,
ÄÄC D
fecha_final
ÄÄE P
,
ÄÄP Q
ref
ÄÄR U
MsgRes
ÄÄV \
)
ÄÄ\ ]
;
ÄÄ] ^
}
ÅÅ 	
public
ĞĞ 
	DataTable
ĞĞ *
CensoConcurrenciaEcopetrolII
ĞĞ 5
(
ĞĞ5 6
DateTime
ĞĞ6 >
	fecha_ini
ĞĞ? H
,
ĞĞH I
DateTime
ĞĞJ R
fecha_final
ĞĞS ^
,
ĞĞ^ _
String
ĞĞ` f
Conexion
ĞĞg o
,
ĞĞo p
ref
ĞĞq t!
MessageResponseOBJĞĞu ‡
MsgResĞĞˆ 
)ĞĞ 
{
ÑÑ 	
return
ÒÒ 
DACConsulta
ÒÒ 
.
ÒÒ *
CensoConcurrenciaEcopetrolII
ÒÒ ;
(
ÒÒ; <
	fecha_ini
ÒÒ< E
,
ÒÒE F
fecha_final
ÒÒG R
,
ÒÒR S
Conexion
ÒÒT \
,
ÒÒ\ ]
ref
ÒÒ^ a
MsgRes
ÒÒb h
)
ÒÒh i
;
ÒÒi j
}
ÓÓ 	
public
ÕÕ 
List
ÕÕ 
<
ÕÕ *
ManagmentClinicosCensoResult
ÕÕ 0
>
ÕÕ0 1
CensoEcopetrol
ÕÕ2 @
(
ÕÕ@ A
DateTime
ÕÕA I
	fecha_ini
ÕÕJ S
,
ÕÕS T
DateTime
ÕÕU ]
fecha_final
ÕÕ^ i
,
ÕÕi j
ref
ÕÕk n!
MessageResponseOBJÕÕo 
MsgResÕÕ‚ ˆ
)ÕÕˆ ‰
{
ÖÖ 	
return
×× 
DACConsulta
×× 
.
×× 
CensoEcopetrol
×× -
(
××- .
	fecha_ini
××. 7
,
××7 8
fecha_final
××9 D
,
××D E
ref
××F I
MsgRes
××J P
)
××P Q
;
××Q R
}
ØØ 	
public
ÚÚ 
List
ÚÚ 
<
ÚÚ 5
'ManagmentClinicosConsultasAlertasResult
ÚÚ ;
>
ÚÚ; <
AlertasEcopetrol
ÚÚ= M
(
ÚÚM N
DateTime
ÚÚN V
	fecha_ini
ÚÚW `
,
ÚÚ` a
DateTime
ÚÚb j
fecha_final
ÚÚk v
,
ÚÚv w
ref
ÚÚx {!
MessageResponseOBJÚÚ| 
MsgResÚÚ •
)ÚÚ• –
{
ÛÛ 	
return
ÜÜ 
DACConsulta
ÜÜ 
.
ÜÜ 
AlertasEcopetrol
ÜÜ /
(
ÜÜ/ 0
	fecha_ini
ÜÜ0 9
,
ÜÜ9 :
fecha_final
ÜÜ; F
,
ÜÜF G
ref
ÜÜH K
MsgRes
ÜÜL R
)
ÜÜR S
;
ÜÜS T
}
İİ 	
public
ää 
Int32
ää (
InsertarDevolucionFacturas
ää /
(
ää/ 0 
factura_devolucion
ää0 B
OBJ
ääC F
,
ääF G
ref
ääH K 
MessageResponseOBJ
ääL ^
MsgRes
ää_ e
)
ääe f
{
åå 	
return
ææ 

DACInserta
ææ 
.
ææ (
InsertarDevolucionFacturas
ææ 8
(
ææ8 9
OBJ
ææ9 <
,
ææ< =
ref
ææ> A
MsgRes
ææB H
)
ææH I
;
ææI J
}
çç 	
public
éé 
Int32
éé /
!InsertarDevolucionFacturasDetalle
éé 6
(
éé6 7(
factura_devolucion_detalle
éé7 Q
OBJ
ééR U
,
ééU V
ref
ééW Z 
MessageResponseOBJ
éé[ m
MsgRes
één t
)
éét u
{
êê 	
return
ëë 

DACInserta
ëë 
.
ëë /
!InsertarDevolucionFacturasDetalle
ëë ?
(
ëë? @
OBJ
ëë@ C
,
ëëC D
ref
ëëE H
MsgRes
ëëI O
)
ëëO P
;
ëëP Q
}
ìì 	
public
îî 
List
îî 
<
îî 0
"ManagmentReportDevolucionFacResult
îî 6
>
îî6 7*
ConsultaReporteDevolucionFac
îî8 T
(
îîT U
Int32
îîU Z#
id_devolucion_factura
îî[ p
)
îîp q
{
ïï 	
return
ğğ 
DACConsulta
ğğ 
.
ğğ *
ConsultaReporteDevolucionFac
ğğ ;
(
ğğ; <#
id_devolucion_factura
ğğ< Q
)
ğğQ R
;
ğğR S
}
ññ 	
public
óó 
Int32
óó %
InsertarFacturaSinCenso
óó ,
(
óó, -
factura_sin_censo
óó- >
OBJ
óó? B
,
óóB C
ref
óóD G 
MessageResponseOBJ
óóH Z
MsgRes
óó[ a
)
óóa b
{
ôô 	
return
õõ 

DACInserta
õõ 
.
õõ %
InsertarFacturaSinCenso
õõ 5
(
õõ5 6
OBJ
õõ6 9
,
õõ9 :
ref
õõ; >
MsgRes
õõ? E
)
õõE F
;
õõF G
}
öö 	
public
øø 
Int32
øø 
InsertarHallazgos
øø &
(
øø& '
hallazgo_RIPS
øø' 4
OBJ
øø5 8
,
øø8 9
ref
øø: = 
MessageResponseOBJ
øø> P
MsgRes
øøQ W
)
øøW X
{
ùù 	
return
úú 

DACInserta
úú 
.
úú 
InsertarHallazgos
úú /
(
úú/ 0
OBJ
úú0 3
,
úú3 4
ref
úú5 8
MsgRes
úú9 ?
)
úú? @
;
úú@ A
}
ûû 	
public
ıı 
Int32
ıı &
InsertarHallazgosDetalle
ıı -
(
ıı- .#
hallazgo_RIPS_detalle
ıı. C
OBJ
ııD G
,
ııG H
ref
ııI L 
MessageResponseOBJ
ııM _
MsgRes
ıı` f
)
ııf g
{
şş 	
return
ÿÿ 

DACInserta
ÿÿ 
.
ÿÿ &
InsertarHallazgosDetalle
ÿÿ 6
(
ÿÿ6 7
OBJ
ÿÿ7 :
,
ÿÿ: ;
ref
ÿÿ< ?
MsgRes
ÿÿ@ F
)
ÿÿF G
;
ÿÿG H
}
€€ 	
public
‚‚ 
List
‚‚ 
<
‚‚ /
!ManagmentReportHallazgosRipResult
‚‚ 5
>
‚‚5 6*
ConsultaReporteHallazgosRips
‚‚7 S
(
‚‚S T
Int32
‚‚T Y
id_hallazgo_RIPS
‚‚Z j
)
‚‚j k
{
ƒƒ 	
return
„„ 
DACConsulta
„„ 
.
„„ *
ConsultaReporteHallazgosRips
„„ ;
(
„„; <
id_hallazgo_RIPS
„„< L
)
„„L M
;
„„M N
}
…… 	
public
‡‡ 
void
‡‡ $
ActualizaHallazgosRips
‡‡ *
(
‡‡* +
hallazgo_RIPS
‡‡+ 8
Objrips
‡‡9 @
,
‡‡@ A
ref
‡‡B E 
MessageResponseOBJ
‡‡F X
MsgRes
‡‡Y _
)
‡‡_ `
{
ˆˆ 	
DACActualiza
‰‰ 
.
‰‰ $
ActualizaHallazgosRips
‰‰ /
(
‰‰/ 0
Objrips
‰‰0 7
,
‰‰7 8
ref
‰‰9 <
MsgRes
‰‰= C
)
‰‰C D
;
‰‰D E
}
ŠŠ 	
public
ŒŒ 
List
ŒŒ 
<
ŒŒ 
factura_sin_censo
ŒŒ %
>
ŒŒ% &%
ConsultaFacturasSinAudi
ŒŒ' >
(
ŒŒ> ?
Int32
ŒŒ? D"
id_factura_sin_censo
ŒŒE Y
)
ŒŒY Z
{
 	
return
 
DACConsulta
 
.
 %
ConsultaFacturasSinAudi
 6
(
6 7"
id_factura_sin_censo
7 K
)
K L
;
L M
}
 	
public
’’ 
Int32
’’ "
InsertarCostoEvitado
’’ )
(
’’) *+
factura_sin_censo_cos_evitado
’’* G
Obj
’’H K
,
’’K L
ref
’’M P 
MessageResponseOBJ
’’Q c
MsgRes
’’d j
)
’’j k
{
““ 	
return
”” 

DACInserta
”” 
.
”” "
InsertarCostoEvitado
”” 2
(
””2 3
Obj
””3 6
,
””6 7
ref
””8 ;
MsgRes
””< B
)
””B C
;
””C D
}
•• 	
public
–– 
Int32
–– (
InsertarDiagnosticoCuentas
–– /
(
––/ 0,
factura_sin_censo_diagnosticos
––0 N
Obj
––O R
,
––R S
ref
––T W 
MessageResponseOBJ
––X j
MsgRes
––k q
)
––q r
{
—— 	
return
˜˜ 

DACInserta
˜˜ 
.
˜˜ (
InsertarDiagnosticoCuentas
˜˜ 8
(
˜˜8 9
Obj
˜˜9 <
,
˜˜< =
ref
˜˜> A
MsgRes
˜˜B H
)
˜˜H I
;
˜˜I J
}
™™ 	
public
›› 
void
›› &
ActualizaFacturaAuditada
›› ,
(
››, -
factura_sin_censo
››- >
ObjAudi
››? F
,
››F G
ref
››H K 
MessageResponseOBJ
››L ^
MsgRes
››_ e
)
››e f
{
œœ 	
DACActualiza
 
.
 &
ActualizaFacturaAuditada
 1
(
1 2
ObjAudi
2 9
,
9 :
ref
; >
MsgRes
? E
)
E F
;
F G
}
 	
public
   
List
   
<
    
factura_devolucion
   &
>
  & ')
ConsultaDevolucionesFactura
  ( C
(
  C D
String
  D J
Numero_factura
  K Y
)
  Y Z
{
¡¡ 	
return
¢¢ 
DACConsulta
¢¢ 
.
¢¢ )
ConsultaDevolucionesFactura
¢¢ :
(
¢¢: ;
Numero_factura
¢¢; I
)
¢¢I J
;
¢¢J K
}
££ 	
public
¥¥ 
List
¥¥ 
<
¥¥ 
factura_sin_censo
¥¥ %
>
¥¥% &#
ConsultaFacturaNumero
¥¥' <
(
¥¥< =
String
¥¥= C
Numero_factura
¥¥D R
)
¥¥R S
{
¦¦ 	
return
§§ 
DACConsulta
§§ 
.
§§ #
ConsultaFacturaNumero
§§ 4
(
§§4 5
Numero_factura
§§5 C
)
§§C D
;
§§D E
}
¨¨ 	
public
ªª 
List
ªª 
<
ªª  
factura_devolucion
ªª &
>
ªª& '+
ConsultaDevolucionesFacturaId
ªª( E
(
ªªE F
Int32
ªªF K
Id_devolucion
ªªL Y
)
ªªY Z
{
«« 	
return
¬¬ 
DACConsulta
¬¬ 
.
¬¬ +
ConsultaDevolucionesFacturaId
¬¬ <
(
¬¬< =
Id_devolucion
¬¬= J
)
¬¬J K
;
¬¬K L
}
­­ 	
public
¯¯ 
List
¯¯ 
<
¯¯ 
hallazgo_RIPS
¯¯ !
>
¯¯! "!
ConsultaHallazgosId
¯¯# 6
(
¯¯6 7
Int32
¯¯7 <
Id_rips
¯¯= D
)
¯¯D E
{
°° 	
return
±± 
DACConsulta
±± 
.
±± !
ConsultaHallazgosId
±± 2
(
±±2 3
Id_rips
±±3 :
)
±±: ;
;
±±; <
}
²² 	
public
µµ 
List
µµ 
<
µµ /
!management_rips_busqueda_acResult
µµ 5
>
µµ5 6!
TraerConsultaRIPSAC
µµ7 J
(
µµJ K
DateTime
µµK S
?
µµS T
fechaInicio
µµU `
,
µµ` a
DateTime
µµb j
?
µµj k
fechaFin
µµl t
,
µµt u
string
µµv |
codCupsµµ} „
,µµ„ …
stringµµ† Œ
diagnosticoµµ ˜
,µµ˜ ™
stringµµš  
cedulaµµ¡ §
)µµ§ ¨
{
¶¶ 	
return
·· 
DACConsulta
·· 
.
·· !
TraerConsultaRIPSAC
·· 2
(
··2 3
fechaInicio
··3 >
,
··> ?
fechaFin
··@ H
,
··H I
codCups
··J Q
,
··Q R
diagnostico
··S ^
,
··^ _
cedula
··` f
)
··f g
;
··g h
}
¸¸ 	
public
ºº 
List
ºº 
<
ºº /
!management_rips_busqueda_apResult
ºº 5
>
ºº5 6!
TraerConsultaRIPSAP
ºº7 J
(
ººJ K
DateTime
ººK S
?
ººS T
fechaInicio
ººU `
,
ºº` a
DateTime
ººb j
?
ººj k
fechaFin
ººl t
,
ººt u
string
ººv |
codCupsºº} „
,ºº„ …
stringºº† Œ
diagnosticoºº ˜
,ºº˜ ™
stringººš  
cedulaºº¡ §
)ºº§ ¨
{
»» 	
return
¼¼ 
DACConsulta
¼¼ 
.
¼¼ !
TraerConsultaRIPSAP
¼¼ 2
(
¼¼2 3
fechaInicio
¼¼3 >
,
¼¼> ?
fechaFin
¼¼@ H
,
¼¼H I
codCups
¼¼J Q
,
¼¼Q R
diagnostico
¼¼S ^
,
¼¼^ _
cedula
¼¼` f
)
¼¼f g
;
¼¼g h
}
½½ 	
public
ÀÀ 
Int32
ÀÀ '
InsertarloteContabilizado
ÀÀ .
(
ÀÀ. />
0ecop_gestion_factura_digital_contabilizados_lote
ÀÀ/ _
OBJ
ÀÀ` c
,
ÀÀc d
ref
ÀÀe h 
MessageResponseOBJ
ÀÀi {
MsgResÀÀ| ‚
)ÀÀ‚ ƒ
{
ÁÁ 	
return
ÂÂ 

DACInserta
ÂÂ 
.
ÂÂ '
InsertarloteContabilizado
ÂÂ 7
(
ÂÂ7 8
OBJ
ÂÂ8 ;
,
ÂÂ; <
ref
ÂÂ= @
MsgRes
ÂÂA G
)
ÂÂG H
;
ÂÂH I
}
ÃÃ 	
public
ÅÅ 
List
ÅÅ 
<
ÅÅ 7
)management_reportelotecontabilizadoResult
ÅÅ =
>
ÅÅ= >!
ConsultaReporteLote
ÅÅ? R
(
ÅÅR S
Int32
ÅÅS X
ID
ÅÅY [
)
ÅÅ[ \
{
ÆÆ 	
return
ÇÇ 
DACConsulta
ÇÇ 
.
ÇÇ !
ConsultaReporteLote
ÇÇ 2
(
ÇÇ2 3
ID
ÇÇ3 5
)
ÇÇ5 6
;
ÇÇ6 7
}
ÈÈ 	
public
ÊÊ 
List
ÊÊ 
<
ÊÊ $
facturacion_sap_cargue
ÊÊ *
>
ÊÊ* +%
validarCargueFacturaSap
ÊÊ, C
(
ÊÊC D
int
ÊÊD G
?
ÊÊG H
mes
ÊÊI L
,
ÊÊL M
int
ÊÊN Q
?
ÊÊQ R
aÃ±o
ÊÊS V
)
ÊÊV W
{
ËË 	
return
ÌÌ 
DACConsulta
ÌÌ 
.
ÌÌ %
validarCargueFacturaSap
ÌÌ 6
(
ÌÌ6 7
mes
ÌÌ7 :
,
ÌÌ: ;
aÃ±o
ÌÌ< ?
)
ÌÌ? @
;
ÌÌ@ A
}
ÍÍ 	
public
ÎÎ 
int
ÎÎ $
InsertarFacturacionSAP
ÎÎ )
(
ÎÎ) *
List
ÎÎ* .
<
ÎÎ. /"
facturacion_sap_dtll
ÎÎ/ C
>
ÎÎC D
List
ÎÎE I
,
ÎÎI J$
facturacion_sap_cargue
ÎÎK a
objbase
ÎÎb i
,
ÎÎi j
ref
ÎÎk n!
MessageResponseOBJÎÎo 
MsgResÎÎ‚ ˆ
)ÎÎˆ ‰
{
ÏÏ 	
return
ĞĞ 

DACInserta
ĞĞ 
.
ĞĞ $
InsertarFacturacionSAP
ĞĞ 4
(
ĞĞ4 5
List
ĞĞ5 9
,
ĞĞ9 :
objbase
ĞĞ; B
,
ĞĞB C
ref
ĞĞD G
MsgRes
ĞĞH N
)
ĞĞN O
;
ĞĞO P
}
ÑÑ 	
public
ÒÒ 
List
ÒÒ 
<
ÒÒ 3
%management_facturacionSAP_listaResult
ÒÒ 9
>
ÒÒ9 :"
ListarFacturacionSAP
ÒÒ; O
(
ÒÒO P
)
ÒÒP Q
{
ÓÓ 	
return
ÔÔ 
DACConsulta
ÔÔ 
.
ÔÔ "
ListarFacturacionSAP
ÔÔ 3
(
ÔÔ3 4
)
ÔÔ4 5
;
ÔÔ5 6
}
ÕÕ 	
public
ÖÖ 
List
ÖÖ 
<
ÖÖ :
,management_facturacionSAP_listaDetalleResult
ÖÖ @
>
ÖÖ@ A)
ListarFacturacionSAPDetalle
ÖÖB ]
(
ÖÖ] ^
int
ÖÖ^ a
aÃ±o
ÖÖb e
,
ÖÖe f
string
ÖÖg m
mes
ÖÖn q
)
ÖÖq r
{
×× 	
return
ØØ 
DACConsulta
ØØ 
.
ØØ )
ListarFacturacionSAPDetalle
ØØ :
(
ØØ: ;
aÃ±o
ØØ; >
,
ØØ> ?
mes
ØØ@ C
)
ØØC D
;
ØØD E
}
ÙÙ 	
public
ÚÚ 
List
ÚÚ 
<
ÚÚ :
,management_facturacionSAP_listaREPORTEResult
ÚÚ @
>
ÚÚ@ A)
ListarFacturacionSAPReporte
ÚÚB ]
(
ÚÚ] ^
DateTime
ÚÚ^ f
fechaIni
ÚÚg o
,
ÚÚo p
DateTime
ÚÚq y
fechaFinÚÚz ‚
)ÚÚ‚ ƒ
{
ÛÛ 	
return
ÜÜ 
DACConsulta
ÜÜ 
.
ÜÜ )
ListarFacturacionSAPReporte
ÜÜ :
(
ÜÜ: ;
fechaIni
ÜÜ; C
,
ÜÜC D
fechaFin
ÜÜE M
)
ÜÜM N
;
ÜÜN O
}
İİ 	
public
ââ 
Int32
ââ 
InsertarRips
ââ !
(
ââ! "
RIPS
ââ" &
Objrips
ââ' .
,
ââ. /
ref
ââ0 3 
MessageResponseOBJ
ââ4 F
MsgRes
ââG M
)
ââM N
{
ãã 	
return
ää 

DACInserta
ää 
.
ää 
InsertarRips
ää *
(
ää* +
Objrips
ää+ 2
,
ää2 3
ref
ää4 7
MsgRes
ää8 >
)
ää> ?
;
ää? @
}
åå 	
public
çç 
List
çç 
<
çç 
RIPS
çç 
>
çç 
ConsultaRips
çç &
(
çç& '
Int32
çç' ,
IdRips
çç- 3
,
çç3 4
ref
çç5 8 
MessageResponseOBJ
çç9 K
MsgRes
ççL R
)
ççR S
{
èè 	
return
éé 
DACConsulta
éé 
.
éé 
ConsultaRips
éé +
(
éé+ ,
IdRips
éé, 2
,
éé2 3
ref
éé4 7
MsgRes
éé8 >
)
éé> ?
;
éé? @
}
êê 	
public
ìì 
bool
ìì 
ActualizaRips
ìì !
(
ìì! "
RIPS
ìì" &
ObjRips
ìì' .
,
ìì. /
ref
ìì0 3 
MessageResponseOBJ
ìì4 F
MsgRes
ììG M
)
ììM N
{
íí 	
return
îî 
DACActualiza
îî 
.
îî  
ActualizaRips
îî  -
(
îî- .
ObjRips
îî. 5
,
îî5 6
ref
îî7 :
MsgRes
îî; A
)
îîA B
;
îîB C
}
ïï 	
public
òò 
Int32
òò 
InsertarRipsAC
òò #
(
òò# $
List
òò$ (
<
òò( )
RIPS_AC
òò) 0
>
òò0 1
	ObjripsAc
òò2 ;
,
òò; <
ref
òò= @ 
MessageResponseOBJ
òòA S
MsgRes
òòT Z
)
òòZ [
{
óó 	
return
ôô 

DACInserta
ôô 
.
ôô 
InsertarRipsAC
ôô ,
(
ôô, -
	ObjripsAc
ôô- 6
,
ôô6 7
ref
ôô8 ;
MsgRes
ôô< B
)
ôôB C
;
ôôC D
}
õõ 	
public
÷÷ 
Int32
÷÷ 
InsertarRipsAD
÷÷ #
(
÷÷# $
List
÷÷$ (
<
÷÷( )
RIPS_AD
÷÷) 0
>
÷÷0 1
	ObjripsAD
÷÷2 ;
,
÷÷; <
ref
÷÷= @ 
MessageResponseOBJ
÷÷A S
MsgRes
÷÷T Z
)
÷÷Z [
{
øø 	
return
ùù 

DACInserta
ùù 
.
ùù 
InsertarRipsAD
ùù ,
(
ùù, -
	ObjripsAD
ùù- 6
,
ùù6 7
ref
ùù8 ;
MsgRes
ùù< B
)
ùùB C
;
ùùC D
}
úú 	
public
üü 
Int32
üü 
InsertarRipsAF
üü #
(
üü# $
List
üü$ (
<
üü( )
RIPS_AF
üü) 0
>
üü0 1
	ObjripsAF
üü2 ;
,
üü; <
ref
üü= @ 
MessageResponseOBJ
üüA S
MsgRes
üüT Z
)
üüZ [
{
ıı 	
return
şş 

DACInserta
şş 
.
şş 
InsertarRipsAF
şş ,
(
şş, -
	ObjripsAF
şş- 6
,
şş6 7
ref
şş8 ;
MsgRes
şş< B
)
şşB C
;
şşC D
}
ÿÿ 	
public
 
Int32
 
InsertarRipsAH
 #
(
# $
List
$ (
<
( )
RIPS_AH
) 0
>
0 1
	ObjripsAH
2 ;
,
; <
ref
= @ 
MessageResponseOBJ
A S
MsgRes
T Z
)
Z [
{
‚‚ 	
return
ƒƒ 

DACInserta
ƒƒ 
.
ƒƒ 
InsertarRipsAH
ƒƒ ,
(
ƒƒ, -
	ObjripsAH
ƒƒ- 6
,
ƒƒ6 7
ref
ƒƒ8 ;
MsgRes
ƒƒ< B
)
ƒƒB C
;
ƒƒC D
}
„„ 	
public
†† 
Int32
†† 
InsertarRipsAM
†† #
(
††# $
List
††$ (
<
††( )
RIPS_AM
††) 0
>
††0 1
	ObjripsAM
††2 ;
,
††; <
ref
††= @ 
MessageResponseOBJ
††A S
MsgRes
††T Z
)
††Z [
{
‡‡ 	
return
ˆˆ 

DACInserta
ˆˆ 
.
ˆˆ 
InsertarRipsAM
ˆˆ ,
(
ˆˆ, -
	ObjripsAM
ˆˆ- 6
,
ˆˆ6 7
ref
ˆˆ8 ;
MsgRes
ˆˆ< B
)
ˆˆB C
;
ˆˆC D
}
‰‰ 	
public
‹‹ 
Int32
‹‹ 
InsertarRipsAN
‹‹ #
(
‹‹# $
List
‹‹$ (
<
‹‹( )
RIPS_AN
‹‹) 0
>
‹‹0 1
	ObjripsAN
‹‹2 ;
,
‹‹; <
ref
‹‹= @ 
MessageResponseOBJ
‹‹A S
MsgRes
‹‹T Z
)
‹‹Z [
{
ŒŒ 	
return
 

DACInserta
 
.
 
InsertarRipsAN
 ,
(
, -
	ObjripsAN
- 6
,
6 7
ref
8 ;
MsgRes
< B
)
B C
;
C D
}
 	
public
 
Int32
 
InsertarRipsAP
 #
(
# $
List
$ (
<
( )
RIPS_AP
) 0
>
0 1
	ObjripsAP
2 ;
,
; <
ref
= @ 
MessageResponseOBJ
A S
MsgRes
T Z
)
Z [
{
‘‘ 	
return
’’ 

DACInserta
’’ 
.
’’ 
InsertarRipsAP
’’ ,
(
’’, -
	ObjripsAP
’’- 6
,
’’6 7
ref
’’8 ;
MsgRes
’’< B
)
’’B C
;
’’C D
}
““ 	
public
•• 
Int32
•• 
InsertarRipsAT
•• #
(
••# $
List
••$ (
<
••( )
RIPS_AT
••) 0
>
••0 1
	ObjripsAT
••2 ;
,
••; <
ref
••= @ 
MessageResponseOBJ
••A S
MsgRes
••T Z
)
••Z [
{
–– 	
return
—— 

DACInserta
—— 
.
—— 
InsertarRipsAT
—— ,
(
——, -
	ObjripsAT
——- 6
,
——6 7
ref
——8 ;
MsgRes
——< B
)
——B C
;
——C D
}
˜˜ 	
public
šš 
Int32
šš 
InsertarRipsAU
šš #
(
šš# $
List
šš$ (
<
šš( )
RIPS_AU
šš) 0
>
šš0 1
	ObjripsAU
šš2 ;
,
šš; <
ref
šš= @ 
MessageResponseOBJ
ššA S
MsgRes
ššT Z
)
ššZ [
{
›› 	
return
œœ 

DACInserta
œœ 
.
œœ 
InsertarRipsAU
œœ ,
(
œœ, -
	ObjripsAU
œœ- 6
,
œœ6 7
ref
œœ8 ;
MsgRes
œœ< B
)
œœB C
;
œœC D
}
 	
public
ŸŸ 
Int32
ŸŸ 
InsertarRipsCT
ŸŸ #
(
ŸŸ# $
List
ŸŸ$ (
<
ŸŸ( )
RIPS_CT
ŸŸ) 0
>
ŸŸ0 1
	ObjripsCT
ŸŸ2 ;
,
ŸŸ; <
ref
ŸŸ= @ 
MessageResponseOBJ
ŸŸA S
MsgRes
ŸŸT Z
)
ŸŸZ [
{
   	
return
¡¡ 

DACInserta
¡¡ 
.
¡¡ 
InsertarRipsCT
¡¡ ,
(
¡¡, -
	ObjripsCT
¡¡- 6
,
¡¡6 7
ref
¡¡8 ;
MsgRes
¡¡< B
)
¡¡B C
;
¡¡C D
}
¢¢ 	
public
¤¤ 
Int32
¤¤ 
InsertarRipsUS
¤¤ #
(
¤¤# $
List
¤¤$ (
<
¤¤( )
RIPS_US
¤¤) 0
>
¤¤0 1
	ObjripsUS
¤¤2 ;
,
¤¤; <
ref
¤¤= @ 
MessageResponseOBJ
¤¤A S
MsgRes
¤¤T Z
)
¤¤Z [
{
¥¥ 	
return
¦¦ 

DACInserta
¦¦ 
.
¦¦ 
InsertarRipsUS
¦¦ ,
(
¦¦, -
	ObjripsUS
¦¦- 6
,
¦¦6 7
ref
¦¦8 ;
MsgRes
¦¦< B
)
¦¦B C
;
¦¦C D
}
§§ 	
public
©© 
List
©© 
<
©© 
ECOPETROL_COMMON
©© $
.
©©$ %
ENUM
©©% )
.
©©) *
reporterips
©©* 5
>
©©5 6$
ConsultaRipsEvaluacion
©©7 M
(
©©M N
Int32
©©N S
IdRips
©©T Z
,
©©Z [
string
©©\ b
conexion
©©c k
,
©©k l
ref
©©m p!
MessageResponseOBJ©©q ƒ
MsgRes©©„ Š
)©©Š ‹
{
ªª 	
return
«« 
DACConsulta
«« 
.
«« $
ConsultaRipsEvaluacion
«« 5
(
««5 6
IdRips
««6 <
,
««< =
conexion
««> F
,
««F G
ref
««H K
MsgRes
««L R
)
««R S
;
««S T
}
¬¬ 	
public
®® 
List
®® 
<
®® ;
-managmentReportePrestadoresNoExistentesResult
®® A
>
®®A B(
getprestadoresnoexistentes
®®C ]
(
®®] ^
Int32
®®^ c
IdRips
®®d j
,
®®j k
ref
®®l o!
MessageResponseOBJ®®p ‚
MsgRes®®ƒ ‰
)®®‰ Š
{
¯¯ 	
return
°° 
DACConsulta
°° 
.
°° (
getprestadoresnoexistentes
°° 9
(
°°9 :
IdRips
°°: @
,
°°@ A
ref
°°B E
MsgRes
°°F L
)
°°L M
;
°°M N
}
±± 	
public
³³ 
List
³³ 
<
³³ &
Logerroresevaluacionrips
³³ ,
>
³³, -'
ConsultaLogRipsEvaluacion
³³. G
(
³³G H
Int32
³³H M
IdRips
³³N T
,
³³T U
ref
³³V Y 
MessageResponseOBJ
³³Z l
MsgRes
³³m s
)
³³s t
{
´´ 	
return
µµ 
DACConsulta
µµ 
.
µµ '
ConsultaLogRipsEvaluacion
µµ 8
(
µµ8 9
IdRips
µµ9 ?
,
µµ? @
ref
µµA D
MsgRes
µµE K
)
µµK L
;
µµL M
}
¶¶ 	
public
¸¸ 
List
¸¸ 
<
¸¸ 
RIPS
¸¸ 
>
¸¸ %
GetListaRipsPorMesYAÃ±o
¸¸ 0
(
¸¸0 1
int
¸¸1 4
mes
¸¸5 8
,
¸¸8 9
int
¸¸: =
aÃ±o
¸¸> A
,
¸¸A B
int
¸¸C F
?
¸¸F G
regional
¸¸H P
)
¸¸P Q
{
¹¹ 	
return
ºº 
DACConsulta
ºº 
.
ºº %
GetListaRipsPorMesYAÃ±o
ºº 5
(
ºº5 6
mes
ºº6 9
,
ºº9 :
aÃ±o
ºº; >
,
ºº> ?
regional
ºº@ H
)
ººH I
;
ººI J
}
»» 	
public
½½ 
List
½½ 
<
½½ <
.ManagmentErroresRipsEvaluacion_historicoResult
½½ B
>
½½B C0
"ConsultaLogRipsEvaluacionHistorico
½½D f
(
½½f g
Int32
½½g l
IdRips
½½m s
,
½½s t
ref
½½u x!
MessageResponseOBJ½½y ‹
MsgRes½½Œ ’
)½½’ “
{
¾¾ 	
return
¿¿ 
DACConsulta
¿¿ 
.
¿¿ 0
"ConsultaLogRipsEvaluacionHistorico
¿¿ A
(
¿¿A B
IdRips
¿¿B H
,
¿¿H I
ref
¿¿J M
MsgRes
¿¿N T
)
¿¿T U
;
¿¿U V
}
ÀÀ 	
public
ÂÂ 
List
ÂÂ 
<
ÂÂ *
LogerroresevaluacionripsDtll
ÂÂ 0
>
ÂÂ0 1+
ConsultaLogRipsEvaluacionDtll
ÂÂ2 O
(
ÂÂO P
Int32
ÂÂP U
IdRips
ÂÂV \
,
ÂÂ\ ]
ref
ÂÂ^ a 
MessageResponseOBJ
ÂÂb t
MsgRes
ÂÂu {
)
ÂÂ{ |
{
ÃÃ 	
return
ÄÄ 
DACConsulta
ÄÄ 
.
ÄÄ +
ConsultaLogRipsEvaluacionDtll
ÄÄ <
(
ÄÄ< =
IdRips
ÄÄ= C
,
ÄÄC D
ref
ÄÄE H
MsgRes
ÄÄI O
)
ÄÄO P
;
ÄÄP Q
}
ÅÅ 	
public
ÇÇ 
List
ÇÇ 
<
ÇÇ A
3ManagmentErroresRipsEvaluacion_Dtll_historicoResult
ÇÇ G
>
ÇÇG H4
&ConsultaLogRipsEvaluacionDtllHistorico
ÇÇI o
(
ÇÇo p
Int32
ÇÇp u
IdRips
ÇÇv |
,
ÇÇ| }
refÇÇ~ "
MessageResponseOBJÇÇ‚ ”
MsgResÇÇ• ›
)ÇÇ› œ
{
ÈÈ 	
return
ÉÉ 
DACConsulta
ÉÉ 
.
ÉÉ 4
&ConsultaLogRipsEvaluacionDtllHistorico
ÉÉ E
(
ÉÉE F
IdRips
ÉÉF L
,
ÉÉL M
ref
ÉÉN Q
MsgRes
ÉÉR X
)
ÉÉX Y
;
ÉÉY Z
}
ÊÊ 	
public
ÌÌ (
vw_totales_rips_evaluacion
ÌÌ )+
ConsultaTotalesRipsEvaluacion
ÌÌ* G
(
ÌÌG H
Int32
ÌÌH M
IdRips
ÌÌN T
,
ÌÌT U
ref
ÌÌV Y 
MessageResponseOBJ
ÌÌZ l
MsgRes
ÌÌm s
)
ÌÌs t
{
ÍÍ 	
return
ÎÎ 
DACConsulta
ÎÎ 
.
ÎÎ +
ConsultaTotalesRipsEvaluacion
ÎÎ <
(
ÎÎ< =
IdRips
ÎÎ= C
,
ÎÎC D
ref
ÎÎE H
MsgRes
ÎÎI O
)
ÎÎO P
;
ÎÎP Q
}
ÏÏ 	
public
ÑÑ 
RIPS_AC
ÑÑ 
GetRipsAcById
ÑÑ $
(
ÑÑ$ %
int
ÑÑ% (
idripsac
ÑÑ) 1
)
ÑÑ1 2
{
ÒÒ 	
return
ÓÓ 
DACConsulta
ÓÓ 
.
ÓÓ 
GetRipsAcById
ÓÓ ,
(
ÓÓ, -
idripsac
ÓÓ- 5
)
ÓÓ5 6
;
ÓÓ6 7
}
ÔÔ 	
public
ÖÖ 
RIPS_AP
ÖÖ 
GetRipsApById
ÖÖ $
(
ÖÖ$ %
int
ÖÖ% (
idripsap
ÖÖ) 1
)
ÖÖ1 2
{
×× 	
return
ØØ 
DACConsulta
ØØ 
.
ØØ 
GetRipsApById
ØØ ,
(
ØØ, -
idripsap
ØØ- 5
)
ØØ5 6
;
ØØ6 7
}
ÙÙ 	
public
ÛÛ 
RIPS_AU
ÛÛ 
GetRipsAuById
ÛÛ $
(
ÛÛ$ %
int
ÛÛ% (
id
ÛÛ) +
)
ÛÛ+ ,
{
ÜÜ 	
return
İİ 
DACConsulta
İİ 
.
İİ 
GetRipsAuById
İİ ,
(
İİ, -
id
İİ- /
)
İİ/ 0
;
İİ0 1
}
ŞŞ 	
public
àà 
RIPS_AH
àà 
GetRipsAhById
àà $
(
àà$ %
int
àà% (
id
àà) +
)
àà+ ,
{
áá 	
return
ââ 
DACConsulta
ââ 
.
ââ 
GetRipsAhById
ââ ,
(
ââ, -
id
ââ- /
)
ââ/ 0
;
ââ0 1
}
ãã 	
public
åå 
RIPS_AN
åå 
GetRipsAnById
åå $
(
åå$ %
int
åå% (
id
åå) +
)
åå+ ,
{
ææ 	
return
çç 
DACConsulta
çç 
.
çç 
GetRipsAnById
çç ,
(
çç, -
id
çç- /
)
çç/ 0
;
çç0 1
}
èè 	
public
êê 
List
êê 
<
êê 
RIPS_AC_HISTORICO
êê %
>
êê% &$
GetRipsAcHistoricoById
êê' =
(
êê= >
int
êê> A
id
êêB D
,
êêD E
int
êêF I
modo
êêJ N
)
êêN O
{
ëë 	
return
ìì 
DACConsulta
ìì 
.
ìì $
GetRipsAcHistoricoById
ìì 5
(
ìì5 6
id
ìì6 8
,
ìì8 9
modo
ìì: >
)
ìì> ?
;
ìì? @
}
íí 	
public
ïï 
List
ïï 
<
ïï 
RIPS_AP_HISTORICO
ïï %
>
ïï% &$
GetRipsApHistoricoById
ïï' =
(
ïï= >
int
ïï> A
id
ïïB D
,
ïïD E
int
ïïF I
modo
ïïJ N
)
ïïN O
{
ğğ 	
return
ññ 
DACConsulta
ññ 
.
ññ $
GetRipsApHistoricoById
ññ 5
(
ññ5 6
id
ññ6 8
,
ññ8 9
modo
ññ: >
)
ññ> ?
;
ññ? @
}
òò 	
public
ôô 
List
ôô 
<
ôô 
RIPS_AU_HISTORICO
ôô %
>
ôô% &$
GetRipsAuHistoricoById
ôô' =
(
ôô= >
int
ôô> A
id
ôôB D
,
ôôD E
int
ôôF I
modo
ôôJ N
)
ôôN O
{
õõ 	
return
öö 
DACConsulta
öö 
.
öö $
GetRipsAuHistoricoById
öö 5
(
öö5 6
id
öö6 8
,
öö8 9
modo
öö: >
)
öö> ?
;
öö? @
}
÷÷ 	
public
ùù 
List
ùù 
<
ùù 
RIPS_AH_HISTORICO
ùù %
>
ùù% &$
GetRipsAhHistoricoById
ùù' =
(
ùù= >
int
ùù> A
id
ùùB D
,
ùùD E
int
ùùF I
modo
ùùJ N
)
ùùN O
{
úú 	
return
ûû 
DACConsulta
ûû 
.
ûû $
GetRipsAhHistoricoById
ûû 5
(
ûû5 6
id
ûû6 8
,
ûû8 9
modo
ûû: >
)
ûû> ?
;
ûû? @
}
üü 	
public
şş 
List
şş 
<
şş 
RIPS_AN_HISTORICO
şş %
>
şş% &$
GetRipsAnHistoricoById
şş' =
(
şş= >
int
şş> A
id
şşB D
,
şşD E
int
şşF I
modo
şşJ N
)
şşN O
{
ÿÿ 	
return
€€ 
DACConsulta
€€ 
.
€€ $
GetRipsAnHistoricoById
€€ 5
(
€€5 6
id
€€6 8
,
€€8 9
modo
€€: >
)
€€> ?
;
€€? @
}
 	
public
ƒƒ 
List
ƒƒ 
<
ƒƒ 
RIPS_AF_HISTORICO
ƒƒ %
>
ƒƒ% &$
GetRipsAfHistoricoById
ƒƒ' =
(
ƒƒ= >
int
ƒƒ> A
id
ƒƒB D
)
ƒƒD E
{
„„ 	
return
…… 
DACConsulta
…… 
.
…… $
GetRipsAfHistoricoById
…… 5
(
……5 6
id
……6 8
)
……8 9
;
……9 :
}
†† 	
public
ˆˆ 
List
ˆˆ 
<
ˆˆ 
RIPS_US_HISTORICO
ˆˆ %
>
ˆˆ% &$
GetRipsUsHistoricoById
ˆˆ' =
(
ˆˆ= >
int
ˆˆ> A
id
ˆˆB D
)
ˆˆD E
{
‰‰ 	
return
ŠŠ 
DACConsulta
ŠŠ 
.
ŠŠ $
GetRipsUsHistoricoById
ŠŠ 5
(
ŠŠ5 6
id
ŠŠ6 8
)
ŠŠ8 9
;
ŠŠ9 :
}
‹‹ 	
public
 
List
 
<
 
RIPS_AC_HISTORICO
 %
>
% &&
GetRipsAcOportunidadById
' ?
(
? @
int
@ C
id
D F
,
F G
int
H K
modo
L P
)
P Q
{
 	
return
 
DACConsulta
 
.
 &
GetRipsAcOportunidadById
 7
(
7 8
id
8 :
,
: ;
modo
< @
)
@ A
;
A B
}
‘‘ 	
public
““ 
List
““ 
<
““ 
RIPS_AP_HISTORICO
““ %
>
““% &&
GetRipsApOportunidadById
““' ?
(
““? @
int
““@ C
id
““D F
,
““F G
int
““H K
modo
““L P
)
““P Q
{
”” 	
return
•• 
DACConsulta
•• 
.
•• &
GetRipsApOportunidadById
•• 7
(
••7 8
id
••8 :
,
••: ;
modo
••< @
)
••@ A
;
••A B
}
–– 	
public
—— 
List
—— 
<
—— ?
1Managment_ReportePrefacturas_total_abiertasResult
—— E
>
——E F)
GetPrefacturasTotalAbiertas
——G b
(
——b c
)
——c d
{
˜˜ 	
return
™™ 
DACConsulta
™™ 
.
™™ )
GetPrefacturasTotalAbiertas
™™ :
(
™™: ;
)
™™; <
;
™™< =
}
šš 	
public
›› 
List
›› 
<
›› ?
1Managment_ReportePrefacturas_total_cerradasResult
›› E
>
››E F)
GetPrefacturasTotalCerradas
››G b
(
››b c
)
››c d
{
œœ 	
return
 
DACConsulta
 
.
 )
GetPrefacturasTotalCerradas
 :
(
: ;
)
; <
;
< =
}
 	
public
¡¡ 
List
¡¡ 
<
¡¡ 
RIPS_AU_HISTORICO
¡¡ %
>
¡¡% &&
GetRipsAuoportunidadById
¡¡' ?
(
¡¡? @
int
¡¡@ C
id
¡¡D F
,
¡¡F G
int
¡¡H K
modo
¡¡L P
)
¡¡P Q
{
¢¢ 	
return
££ 
DACConsulta
££ 
.
££ &
GetRipsAuoportunidadById
££ 7
(
££7 8
id
££8 :
,
££: ;
modo
££< @
)
££@ A
;
££A B
}
¤¤ 	
public
¦¦ 
List
¦¦ 
<
¦¦ 
RIPS_AH_HISTORICO
¦¦ %
>
¦¦% &&
GetRipsAhOportunidadById
¦¦' ?
(
¦¦? @
int
¦¦@ C
id
¦¦D F
,
¦¦F G
int
¦¦H K
modo
¦¦L P
)
¦¦P Q
{
§§ 	
return
¨¨ 
DACConsulta
¨¨ 
.
¨¨ &
GetRipsAhOportunidadById
¨¨ 7
(
¨¨7 8
id
¨¨8 :
,
¨¨: ;
modo
¨¨< @
)
¨¨@ A
;
¨¨A B
}
©© 	
public
«« 
List
«« 
<
«« 
RIPS_AN_HISTORICO
«« %
>
««% &&
GetRipsAnOportunidadById
««' ?
(
««? @
int
««@ C
id
««D F
,
««F G
int
««H K
modo
««L P
)
««P Q
{
¬¬ 	
return
­­ 
DACConsulta
­­ 
.
­­ &
GetRipsAnOportunidadById
­­ 7
(
­­7 8
id
­­8 :
,
­­: ;
modo
­­< @
)
­­@ A
;
­­A B
}
®® 	
public
²² 
List
²² 
<
²² 2
$managmentRips_AC_FechaconsultaResult
²² 8
>
²²8 9'
ConsultaRipsFechaConsulta
²²: S
(
²²S T
DateTime
²²T \
FechaInicio
²²] h
,
²²h i
DateTime
²²j r

FechaFinal
²²s }
,
²²} ~
ref²² ‚"
MessageResponseOBJ²²ƒ •
MsgRes²²– œ
)²²œ 
{
³³ 	
return
´´ 
DACConsulta
´´ 
.
´´ '
ConsultaRipsFechaConsulta
´´ 8
(
´´8 9
FechaInicio
´´9 D
,
´´D E

FechaFinal
´´F P
,
´´P Q
ref
´´R U
MsgRes
´´V \
)
´´\ ]
;
´´] ^
}
µµ 	
public
·· 
List
·· 
<
·· 7
)managmentRips_AP_FechaProcedimientoResult
·· =
>
··= >,
ConsultaRipsFechaProcedimeinto
··? ]
(
··] ^
int
··^ a
?
··a b
regional
··c k
,
··k l
DateTime
··m u
FechaInicio··v 
,·· ‚
DateTime··ƒ ‹

FechaFinal··Œ –
,··– —
ref··˜ ›"
MessageResponseOBJ··œ ®
MsgRes··¯ µ
)··µ ¶
{
¸¸ 	
return
¹¹ 
DACConsulta
¹¹ 
.
¹¹ ,
ConsultaRipsFechaProcedimiento
¹¹ =
(
¹¹= >
regional
¹¹> F
,
¹¹F G
FechaInicio
¹¹H S
,
¹¹S T

FechaFinal
¹¹U _
,
¹¹_ `
ref
¹¹a d
MsgRes
¹¹e k
)
¹¹k l
;
¹¹l m
}
ºº 	
public
¼¼ 
List
¼¼ 
<
¼¼ 1
#vw_consulta_rips_an_fechanacimiento
¼¼ 7
>
¼¼7 8(
GetListRipsFechaNacimiento
¼¼9 S
(
¼¼S T
DateTime
¼¼T \
FechaInicio
¼¼] h
,
¼¼h i
DateTime
¼¼j r

FechaFinal
¼¼s }
,
¼¼} ~
ref¼¼ ‚"
MessageResponseOBJ¼¼ƒ •
MsgRes¼¼– œ
)¼¼œ 
{
½½ 	
return
¾¾ 
DACConsulta
¾¾ 
.
¾¾ )
ConsultaRipsFechaNacimiento
¾¾ :
(
¾¾: ;
FechaInicio
¾¾; F
,
¾¾F G

FechaFinal
¾¾H R
,
¾¾R S
ref
¾¾T W
MsgRes
¾¾X ^
)
¾¾^ _
;
¾¾_ `
}
¿¿ 	
public
ÁÁ 
List
ÁÁ 
<
ÁÁ 
Ref_tipo_rips
ÁÁ !
>
ÁÁ! "
ConsultaTipoRips
ÁÁ# 3
(
ÁÁ3 4
ref
ÁÁ4 7 
MessageResponseOBJ
ÁÁ8 J
MsgRes
ÁÁK Q
)
ÁÁQ R
{
ÂÂ 	
return
ÃÃ 
DACConsulta
ÃÃ 
.
ÃÃ 
ConsultaTipoRips
ÃÃ /
(
ÃÃ/ 0
ref
ÃÃ0 3
MsgRes
ÃÃ4 :
)
ÃÃ: ;
;
ÃÃ; <
}
ÄÄ 	
public
ÆÆ 
List
ÆÆ 
<
ÆÆ ,
vw_consulta_rips_ah_mortalidad
ÆÆ 2
>
ÆÆ2 3%
GetListRipsMortalidadAH
ÆÆ4 K
(
ÆÆK L
DateTime
ÆÆL T
?
ÆÆT U
FechaInicial
ÆÆV b
,
ÆÆb c
DateTime
ÆÆd l
?
ÆÆl m

FechaFinal
ÆÆn x
,
ÆÆx y
ref
ÆÆz }!
MessageResponseOBJÆÆ~ 
MsgResÆÆ‘ —
)ÆÆ— ˜
{
ÇÇ 	
return
ÈÈ 
DACConsulta
ÈÈ 
.
ÈÈ %
GetListRipsMortalidadAH
ÈÈ 6
(
ÈÈ6 7
FechaInicial
ÈÈ7 C
,
ÈÈC D

FechaFinal
ÈÈE O
,
ÈÈO P
ref
ÈÈQ T
MsgRes
ÈÈU [
)
ÈÈ[ \
;
ÈÈ\ ]
}
ÉÉ 	
public
ÌÌ 
List
ÌÌ 
<
ÌÌ ,
vw_consulta_rips_au_mortalidad
ÌÌ 2
>
ÌÌ2 3%
GetListRipsMortalidadAU
ÌÌ4 K
(
ÌÌK L
DateTime
ÌÌL T
?
ÌÌT U
FechaInicial
ÌÌV b
,
ÌÌb c
DateTime
ÌÌd l
?
ÌÌl m

FechaFinal
ÌÌn x
,
ÌÌx y
ref
ÌÌz }!
MessageResponseOBJÌÌ~ 
MsgResÌÌ‘ —
)ÌÌ— ˜
{
ÍÍ 	
return
ÎÎ 
DACConsulta
ÎÎ 
.
ÎÎ %
GetListRipsMortalidadAU
ÎÎ 6
(
ÎÎ6 7
FechaInicial
ÎÎ7 C
,
ÎÎC D

FechaFinal
ÎÎE O
,
ÎÎO P
ref
ÎÎQ T
MsgRes
ÎÎU [
)
ÎÎ[ \
;
ÎÎ\ ]
}
ÏÏ 	
public
ÑÑ 
RIPS
ÑÑ "
ValidacionCargueRips
ÑÑ (
(
ÑÑ( )
int
ÑÑ) ,

idregional
ÑÑ- 7
,
ÑÑ7 8
int
ÑÑ9 <
mes
ÑÑ= @
,
ÑÑ@ A
int
ÑÑB E
aÃ±o
ÑÑF I
)
ÑÑI J
{
ÒÒ 	
return
ÓÓ 
DACConsulta
ÓÓ 
.
ÓÓ "
ValidacionCargueRips
ÓÓ 3
(
ÓÓ3 4

idregional
ÓÓ4 >
,
ÓÓ> ?
mes
ÓÓ@ C
,
ÓÓC D
aÃ±o
ÓÓE H
)
ÓÓH I
;
ÓÓI J
}
ÔÔ 	
public
ÙÙ 
void
ÙÙ 
ActualizarPQRS
ÙÙ "
(
ÙÙ" #
	ecop_PQRS
ÙÙ# ,
ObjPqrs
ÙÙ- 4
,
ÙÙ4 5
ref
ÙÙ6 9 
MessageResponseOBJ
ÙÙ: L
MsgRes
ÙÙM S
)
ÙÙS T
{
ÚÚ 	
DACActualiza
ÛÛ 
.
ÛÛ 
ActualizarPQRS
ÛÛ '
(
ÛÛ' (
ObjPqrs
ÛÛ( /
,
ÛÛ/ 0
ref
ÛÛ1 4
MsgRes
ÛÛ5 ;
)
ÛÛ; <
;
ÛÛ< =
}
ÜÜ 	
public
İİ 
	ecop_PQRS
İİ 
LlamarPqrsById
İİ '
(
İİ' (
int
İİ( +
id
İİ, .
)
İİ. /
{
ŞŞ 	
return
ßß 
DACConsulta
ßß 
.
ßß 
LlamarPqrsById
ßß -
(
ßß- .
id
ßß. 0
)
ßß0 1
;
ßß1 2
}
àà 	
public
ââ 
int
ââ *
eliminarArchivoPqrsidArchivo
ââ /
(
ââ/ 0
int
ââ0 3
id
ââ4 6
)
ââ6 7
{
ãã 	
return
ää 

DACElimina
ää 
.
ää *
eliminarArchivoPqrsidArchivo
ää :
(
ää: ;
id
ää; =
)
ää= >
;
ää> ?
}
åå 	
public
çç 
int
çç *
GuardarLogEliminarArchivoPqr
çç /
(
çç/ 0,
log_ecop_pqrs_eliminarArchivos
çç0 N
obj
ççO R
)
ççR S
{
èè 	
return
éé 

DACInserta
éé 
.
éé *
GuardarLogEliminarArchivoPqr
éé :
(
éé: ;
obj
éé; >
)
éé> ?
;
éé? @
}
êê 	
public
ëë 
void
ëë 1
#ActualizarEstadoEnrevisionpryectada
ëë 7
(
ëë7 8"
ecop_PQRS_enrevision
ëë8 L
OBJ
ëëM P
,
ëëP Q
ref
ëëR U 
MessageResponseOBJ
ëëV h
MsgRes
ëëi o
)
ëëo p
{
ìì 	
DACActualiza
íí 
.
íí 1
#ActualizarEstadoEnrevisionpryectada
íí <
(
íí< =
OBJ
íí= @
,
íí@ A
ref
ííB E
MsgRes
ííF L
)
ííL M
;
ííM N
}
îî 	
public
ğğ 
List
ğğ 
<
ğğ 
vw_ecop_PQRS
ğğ  
>
ğğ  !
ConsultaPQRS
ğğ" .
(
ğğ. /
)
ğğ/ 0
{
ññ 	
return
òò 
DACConsulta
òò 
.
òò 
ConsultaPQRS
òò +
(
òò+ ,
)
òò, -
;
òò- .
}
óó 	
public
õõ 
List
õõ 
<
õõ 2
$management_pqrs_tableroAuditorResult
õõ 8
>
õõ8 9$
ConsultaTableroAuditor
õõ: P
(
õõP Q
)
õõQ R
{
öö 	
return
÷÷ 
DACConsulta
÷÷ 
.
÷÷ $
ConsultaTableroAuditor
÷÷ 5
(
÷÷5 6
)
÷÷6 7
;
÷÷7 8
}
øø 	
public
ùù 
List
ùù 
<
ùù 2
$management_pqrsAuditor_reporteResult
ùù 8
>
ùù8 9 
ReporteAuditorPqrs
ùù: L
(
ùùL M
int
ùùM P
idPqrs
ùùQ W
)
ùùW X
{
úú 	
return
ûû 
DACConsulta
ûû 
.
ûû  
ReporteAuditorPqrs
ûû 1
(
ûû1 2
idPqrs
ûû2 8
)
ûû8 9
;
ûû9 :
}
üü 	
public
şş 
List
şş 
<
şş %
vw_ecop_PQRS_enrevision
şş +
>
şş+ ,$
ConsultaPQRSEnRevision
şş- C
(
şşC D
)
şşD E
{
ÿÿ 	
return
€€ 
DACConsulta
€€ 
.
€€ $
ConsultaPQRSEnRevision
€€ 5
(
€€5 6
)
€€6 7
;
€€7 8
}
 	
public
„„ 
List
„„ 
<
„„ 
Ref_PQRS_usuarios
„„ %
>
„„% &
GetusuariosPQRS
„„' 6
(
„„6 7
)
„„7 8
{
…… 	
return
†† 
DACConsulta
†† 
.
†† 
GetusuariosPQRS
†† .
(
††. /
)
††/ 0
;
††0 1
}
‡‡ 	
public
ŠŠ 
List
ŠŠ 
<
ŠŠ 
	ecop_PQRS
ŠŠ 
>
ŠŠ 
	GetPQRSId
ŠŠ (
(
ŠŠ( )
int
ŠŠ) ,
id
ŠŠ- /
)
ŠŠ/ 0
{
‹‹ 	
return
ŒŒ 
DACConsulta
ŒŒ 
.
ŒŒ 
	GetPQRSId
ŒŒ (
(
ŒŒ( )
id
ŒŒ) +
)
ŒŒ+ ,
;
ŒŒ, -
}
 	
public
 
List
 
<
 "
ecop_PQRS_enrevision
 (
>
( )!
GetPQRSIdEnrevision
* =
(
= >
int
> A
id
B D
)
D E
{
 	
return
‘‘ 
DACConsulta
‘‘ 
.
‘‘ !
GetPQRSIdEnrevision
‘‘ 2
(
‘‘2 3
id
‘‘3 5
)
‘‘5 6
;
‘‘6 7
}
’’ 	
public
”” 
List
”” 
<
”” "
ecop_PQRS_enrevision
”” (
>
””( )!
GetIdPQRSEnrevision
””* =
(
””= >
int
””> A
id
””B D
)
””D E
{
•• 	
return
–– 
DACConsulta
–– 
.
–– !
GetIdPQRSEnrevision
–– 2
(
––2 3
id
––3 5
)
––5 6
;
––6 7
}
—— 	
public
šš 
List
šš 
<
šš 
vw_ecop_PQRS2
šš !
>
šš! "
ConsultaPQRS2
šš# 0
(
šš0 1
)
šš1 2
{
›› 	
return
œœ 
DACConsulta
œœ 
.
œœ 
ConsultaPQRS2
œœ ,
(
œœ, -
)
œœ- .
;
œœ. /
}
 	
public
ŸŸ 
Int32
ŸŸ 
InsertarPQRS
ŸŸ !
(
ŸŸ! "
	ecop_PQRS
ŸŸ" +
OBJ
ŸŸ, /
,
ŸŸ/ 0
ref
ŸŸ1 4 
MessageResponseOBJ
ŸŸ5 G
MsgRes
ŸŸH N
)
ŸŸN O
{
   	
return
¡¡ 

DACInserta
¡¡ 
.
¡¡ 
InsertarPQRS
¡¡ *
(
¡¡* +
OBJ
¡¡+ .
,
¡¡. /
ref
¡¡0 3
MsgRes
¡¡4 :
)
¡¡: ;
;
¡¡; <
}
¢¢ 	
public
¤¤ 
List
¤¤ 
<
¤¤ #
vw_ecop_PQRS_DetalleG
¤¤ )
>
¤¤) *!
ConsultaPQRSDetalle
¤¤+ >
(
¤¤> ?
Int32
¤¤? D
Id_pqrs
¤¤E L
)
¤¤L M
{
¥¥ 	
return
§§ 
DACConsulta
§§ 
.
§§ !
ConsultaPQRSDetalle
§§ 2
(
§§2 3
Id_pqrs
§§3 :
)
§§: ;
;
§§; <
}
¨¨ 	
public
ªª 9
+log_pqrs_reinicioConteo_asignacionAnalistas
ªª :&
BuscarReinicioConteoPqrs
ªª; S
(
ªªS T
int
ªªT W
?
ªªW X
mes
ªªY \
,
ªª\ ]
int
ªª^ a
?
ªªa b
aÃ±o
ªªc f
)
ªªf g
{
¬¬ 	
return
­­ 
DACConsulta
­­ 
.
­­ &
BuscarReinicioConteoPqrs
­­ 7
(
­­7 8
mes
­­8 ;
,
­­; <
aÃ±o
­­= @
)
­­@ A
;
­­A B
}
®® 	
public
°° 
int
°° +
InsertarLogReinicioConteoPqrs
°° 0
(
°°0 19
+log_pqrs_reinicioConteo_asignacionAnalistas
°°1 \
obj
°°] `
)
°°` a
{
±± 	
return
²² 

DACInserta
²² 
.
²² +
InsertarLogReinicioConteoPqrs
²² ;
(
²²; <
obj
²²< ?
)
²²? @
;
²²@ A
}
³³ 	
public
µµ 
int
µµ *
ActualizaConteoPqrsAnalistas
µµ /
(
µµ/ 0
)
µµ0 1
{
¶¶ 	
return
·· 
DACActualiza
·· 
.
··  *
ActualizaConteoPqrsAnalistas
··  <
(
··< =
)
··= >
;
··> ?
}
¸¸ 	
public
ºº 
List
ºº 
<
ºº 1
#management_buscarAnalistaPqrsResult
ºº 7
>
ºº7 8
AnalistaPqr
ºº9 D
(
ººD E
int
ººE H
ciudad
ººI O
,
ººO P
int
ººQ T
regional
ººU ]
)
ºº] ^
{
»» 	
return
¼¼ 
DACConsulta
¼¼ 
.
¼¼ 
AnalistaPqr
¼¼ *
(
¼¼* +
ciudad
¼¼+ 1
,
¼¼1 2
regional
¼¼3 ;
)
¼¼; <
;
¼¼< =
}
½½ 	
public
¾¾ 
Ref_PQRS_usuarios
¾¾  
GetUsuarioPqrs
¾¾! /
(
¾¾/ 0
string
¾¾0 6
usuario
¾¾7 >
)
¾¾> ?
{
¿¿ 	
return
ÀÀ 
DACConsulta
ÀÀ 
.
ÀÀ 
GetUsuarioPqrs
ÀÀ -
(
ÀÀ- .
usuario
ÀÀ. 5
)
ÀÀ5 6
;
ÀÀ6 7
}
ÁÁ 	
public
ÃÃ 
Ref_PQRS_usuarios
ÃÃ  
GetUsuarioPqrsId
ÃÃ! 1
(
ÃÃ1 2
int
ÃÃ2 5
?
ÃÃ5 6
	idUsuario
ÃÃ7 @
)
ÃÃ@ A
{
ÄÄ 	
return
ÅÅ 
DACConsulta
ÅÅ 
.
ÅÅ 
GetUsuarioPqrsId
ÅÅ /
(
ÅÅ/ 0
	idUsuario
ÅÅ0 9
)
ÅÅ9 :
;
ÅÅ: ;
}
ÆÆ 	
public
ÈÈ 
List
ÈÈ 
<
ÈÈ 
Ref_PQRS_Atributo
ÈÈ %
>
ÈÈ% & 
listaAtributosPqrs
ÈÈ' 9
(
ÈÈ9 :
)
ÈÈ: ;
{
ÉÉ 	
return
ÊÊ 
DACConsulta
ÊÊ 
.
ÊÊ  
listaAtributosPqrs
ÊÊ 1
(
ÊÊ1 2
)
ÊÊ2 3
;
ÊÊ3 4
}
ËË 	
public
ÍÍ 
List
ÍÍ 
<
ÍÍ 
sis_usuario
ÍÍ 
>
ÍÍ  "
GetListAuditorCiudad
ÍÍ! 5
(
ÍÍ5 6
Int32
ÍÍ6 ;
?
ÍÍ; <
regional
ÍÍ= E
,
ÍÍE F
Int32
ÍÍG L
?
ÍÍL M
ciudad
ÍÍN T
,
ÍÍT U
ref
ÍÍV Y 
MessageResponseOBJ
ÍÍZ l
MsgRes
ÍÍm s
)
ÍÍs t
{
ÎÎ 	
return
ÏÏ 
DACConsulta
ÏÏ 
.
ÏÏ "
GetListAuditorCiudad
ÏÏ 3
(
ÏÏ3 4
regional
ÏÏ4 <
,
ÏÏ< =
ciudad
ÏÏ> D
,
ÏÏD E
ref
ÏÏF I
MsgRes
ÏÏJ P
)
ÏÏP Q
;
ÏÏQ R
}
ĞĞ 	
public
ÒÒ 
Int32
ÒÒ 
?
ÒÒ 
Getidauditor
ÒÒ "
(
ÒÒ" #
string
ÒÒ# )
nombre
ÒÒ* 0
)
ÒÒ0 1
{
ÓÓ 	
return
ÔÔ 
DACConsulta
ÔÔ 
.
ÔÔ 
Getidauditor
ÔÔ +
(
ÔÔ+ ,
nombre
ÔÔ, 2
)
ÔÔ2 3
;
ÔÔ3 4
}
ÕÕ 	
public
×× 
List
×× 
<
×× 
vw_ecop_PQRS
××  
>
××  !
ConsultaPQRSId
××" 0
(
××0 1
Int32
××1 6
id_ecop_PQRS
××7 C
)
××C D
{
ØØ 	
return
ÙÙ 
DACConsulta
ÙÙ 
.
ÙÙ 
ConsultaPQRSId
ÙÙ -
(
ÙÙ- .
id_ecop_PQRS
ÙÙ. :
)
ÙÙ: ;
;
ÙÙ; <
}
ÚÚ 	
public
İİ 
List
İİ 
<
İİ 
vw_ecop_PQRS2
İİ !
>
İİ! "
ConsultaPQRSId2
İİ# 2
(
İİ2 3
Int32
İİ3 8
id_ecop_PQRS
İİ9 E
)
İİE F
{
ŞŞ 	
return
ßß 
DACConsulta
ßß 
.
ßß 
ConsultaPQRSId2
ßß .
(
ßß. /
id_ecop_PQRS
ßß/ ;
)
ßß; <
;
ßß< =
}
àà 	
public
ââ 
Int32
ââ !
InsertarPQRSAuditor
ââ (
(
ââ( )
ecop_PQRS_Auditor
ââ) :
OBJ
ââ; >
,
ââ> ?
ref
ââ@ C 
MessageResponseOBJ
ââD V
MsgRes
ââW ]
)
ââ] ^
{
ãã 	
return
ää 

DACInserta
ää 
.
ää !
InsertarPQRSAuditor
ää 1
(
ää1 2
OBJ
ää2 5
,
ää5 6
ref
ää7 :
MsgRes
ää; A
)
ääA B
;
ääB C
}
åå 	
public
çç 
Int32
çç $
InsertarPQRSProyeccion
çç +
(
çç+ ,$
GestionDocumentalPQRS2
çç, B
OBJ
ççC F
,
ççF G
ref
ççH K 
MessageResponseOBJ
ççL ^
MsgRes
çç_ e
)
ççe f
{
èè 	
return
éé 

DACInserta
éé 
.
éé $
InsertarPQRSProyeccion
éé 4
(
éé4 5
OBJ
éé5 8
,
éé8 9
ref
éé: =
MsgRes
éé> D
)
ééD E
;
ééE F
}
êê 	
public
ëë 
Int32
ëë 3
%InsertarArchivoPQRRespuestaProyectada
ëë :
(
ëë: ;$
GestionDocumentalPQRS2
ëë; Q
OBJ
ëëR U
,
ëëU V
ref
ëëW Z 
MessageResponseOBJ
ëë[ m
MsgRes
ëën t
)
ëët u
{
ìì 	
return
íí 

DACInserta
íí 
.
íí 3
%InsertarArchivoPQRRespuestaProyectada
íí C
(
ííC D
OBJ
ííD G
,
ííG H
ref
ííI L
MsgRes
ííM S
)
ííS T
;
ííT U
}
îî 	
public
ïï 
Int32
ïï 1
#PqrInsertarArchivoRepositorioCierre
ïï 8
(
ïï8 9$
GestionDocumentalPQRS2
ïï9 O
OBJ
ïïP S
,
ïïS T
ref
ïïU X 
MessageResponseOBJ
ïïY k
MsgRes
ïïl r
)
ïïr s
{
ğğ 	
return
ññ 

DACInserta
ññ 
.
ññ 1
#PqrInsertarArchivoRepositorioCierre
ññ A
(
ññA B
OBJ
ññB E
,
ññE F
ref
ññG J
MsgRes
ññK Q
)
ññQ R
;
ññR S
}
òò 	
public
óó 
int
óó *
InsertarArchivoReaperturaPQR
óó /
(
óó/ 0$
GestionDocumentalPQRS2
óó0 F
OBJ
óóG J
)
óóJ K
{
ôô 	
return
õõ 

DACInserta
õõ 
.
õõ *
InsertarArchivoReaperturaPQR
õõ :
(
õõ: ;
OBJ
õõ; >
)
õõ> ?
;
õõ? @
}
öö 	
public
øø 
Int32
øø $
InsertarPQRSEnrevision
øø +
(
øø+ ,"
ecop_PQRS_enrevision
øø, @
OBJ
øøA D
,
øøD E
ref
øøF I 
MessageResponseOBJ
øøJ \
MsgRes
øø] c
)
øøc d
{
ùù 	
return
úú 

DACInserta
úú 
.
úú $
InsertarPQRSEnrevision
úú 4
(
úú4 5
OBJ
úú5 8
,
úú8 9
ref
úú: =
MsgRes
úú> D
)
úúD E
;
úúE F
}
ûû 	
public
şş 
List
şş 
<
şş 
ecop_PQRS_Auditor
şş %
>
şş% &!
ConsultaPQRSAuditor
şş' :
(
şş: ;
Int32
şş; @
Id_pqrs
şşA H
)
şşH I
{
ÿÿ 	
return
€€ 
DACConsulta
€€ 
.
€€ !
ConsultaPQRSAuditor
€€ 2
(
€€2 3
Id_pqrs
€€3 :
)
€€: ;
;
€€; <
}
 	
public
‚‚ 
List
‚‚ 
<
‚‚ 0
"management_pqrs_auditorListaResult
‚‚ 6
>
‚‚6 7
ListaPqrsAuditor
‚‚8 H
(
‚‚H I
int
‚‚I L
idPqrs
‚‚M S
)
‚‚S T
{
ƒƒ 	
return
„„ 
DACConsulta
„„ 
.
„„ 
ListaPqrsAuditor
„„ /
(
„„/ 0
idPqrs
„„0 6
)
„„6 7
;
„„7 8
}
…… 	
public
‡‡ 
List
‡‡ 
<
‡‡ $
GestionDocumentalPQRS2
‡‡ *
>
‡‡* +
GetUrlProyeccion
‡‡, <
(
‡‡< =
Int32
‡‡= B
Id
‡‡C E
,
‡‡E F
ref
‡‡G J 
MessageResponseOBJ
‡‡K ]
MsgRes
‡‡^ d
)
‡‡d e
{
ˆˆ 	
return
‰‰ 
DACConsulta
‰‰ 
.
‰‰ 
GetUrlProyeccion
‰‰ /
(
‰‰/ 0
Id
‰‰0 2
,
‰‰2 3
ref
‰‰4 7
MsgRes
‰‰8 >
)
‰‰> ?
;
‰‰? @
}
ŠŠ 	
public
‹‹ 
List
‹‹ 
<
‹‹ 1
#management_pqrs_mirarArchivosResult
‹‹ 7
>
‹‹7 8
ArchivosPqrs
‹‹9 E
(
‹‹E F
Int32
‹‹F K
idPqr
‹‹L Q
)
‹‹Q R
{
ŒŒ 	
return
 
DACConsulta
 
.
 
ArchivosPqrs
 +
(
+ ,
idPqr
, 1
)
1 2
;
2 3
}
 	
public
 $
GestionDocumentalPQRS2
 %
traerArchivoPqr
& 5
(
5 6
int
6 9
	idArchivo
: C
)
C D
{
 	
return
‘‘ 
DACConsulta
‘‘ 
.
‘‘ 
traerArchivoPqr
‘‘ .
(
‘‘. /
	idArchivo
‘‘/ 8
)
‘‘8 9
;
‘‘9 :
}
’’ 	
public
““ $
GestionDocumentalPQRS2
““ %
traerArchivoPqrId
““& 7
(
““7 8
int
““8 ;
idPqr
““< A
)
““A B
{
”” 	
return
•• 
DACConsulta
•• 
.
•• 
traerArchivoPqrId
•• 0
(
••0 1
idPqr
••1 6
)
••6 7
;
••7 8
}
–– 	
public
˜˜ 
List
˜˜ 
<
˜˜ #
GestionDocumentalPQRS
˜˜ )
>
˜˜) *"
GetUrlDocumentosPqrs
˜˜+ ?
(
˜˜? @
Int32
˜˜@ E
Id
˜˜F H
,
˜˜H I
ref
˜˜J M 
MessageResponseOBJ
˜˜N `
MsgRes
˜˜a g
)
˜˜g h
{
™™ 	
return
šš 
DACConsulta
šš 
.
šš "
GetUrlDocumentosPqrs
šš 3
(
šš3 4
Id
šš4 6
,
šš6 7
ref
šš8 ;
MsgRes
šš< B
)
ššB C
;
ššC D
}
›› 	
public
ŸŸ 
void
ŸŸ !
ActualizarFechaPQRS
ŸŸ '
(
ŸŸ' (
Int32
ŸŸ( -
id_ecop_PQRS
ŸŸ. :
,
ŸŸ: ;
ref
ŸŸ< ? 
MessageResponseOBJ
ŸŸ@ R
MsgRes
ŸŸS Y
)
ŸŸY Z
{
   	
DACActualiza
¡¡ 
.
¡¡ !
ActualizarFechaPQRS
¡¡ ,
(
¡¡, -
id_ecop_PQRS
¡¡- 9
,
¡¡9 :
ref
¡¡; >
MsgRes
¡¡? E
)
¡¡E F
;
¡¡F G
}
¢¢ 	
public
¤¤ 
void
¤¤ +
ActualizaestadoPQRSEnrevision
¤¤ 1
(
¤¤1 2"
ecop_PQRS_enrevision
¤¤2 F
obj
¤¤G J
,
¤¤J K
ref
¤¤L O 
MessageResponseOBJ
¤¤P b
MsgRes
¤¤c i
)
¤¤i j
{
¥¥ 	
DACActualiza
¦¦ 
.
¦¦ +
ActualizaestadoPQRSEnrevision
¦¦ 6
(
¦¦6 7
obj
¦¦7 :
,
¦¦: ;
ref
¦¦< ?
MsgRes
¦¦@ F
)
¦¦F G
;
¦¦G H
}
§§ 	
public
©© 
void
©© -
ActualizarGestionPQRSEnrevision
©© 3
(
©©3 4"
ecop_PQRS_enrevision
©©4 H
obj
©©I L
,
©©L M
ref
©©N Q 
MessageResponseOBJ
©©R d
MsgRes
©©e k
)
©©k l
{
ªª 	
DACActualiza
«« 
.
«« -
ActualizarGestionPQRSEnrevision
«« 8
(
««8 9
obj
««9 <
,
««< =
ref
««> A
MsgRes
««B H
)
««H I
;
««I J
}
¬¬ 	
public
¯¯ 
void
¯¯ "
ActualizaReabrirPQRS
¯¯ (
(
¯¯( )
	ecop_PQRS
¯¯) 2
obj
¯¯3 6
,
¯¯6 7
ref
¯¯8 ; 
MessageResponseOBJ
¯¯< N
MsgRes
¯¯O U
)
¯¯U V
{
°° 	
DACActualiza
±± 
.
±± "
ActualizaReabrirPQRS
±± -
(
±±- .
obj
±±. 1
,
±±1 2
ref
±±3 6
MsgRes
±±7 =
)
±±= >
;
±±> ?
}
²² 	
public
´´ 
void
´´ &
ActualizarFechaPQRSDirec
´´ ,
(
´´, -
Int32
´´- 2
id_ecop_PQRS
´´3 ?
,
´´? @
ref
´´A D 
MessageResponseOBJ
´´E W
MsgRes
´´X ^
)
´´^ _
{
µµ 	
DACActualiza
¶¶ 
.
¶¶ &
ActualizarFechaPQRSDirec
¶¶ 1
(
¶¶1 2
id_ecop_PQRS
¶¶2 >
,
¶¶> ?
ref
¶¶@ C
MsgRes
¶¶D J
)
¶¶J K
;
¶¶K L
}
·· 	
public
¹¹ 
int
¹¹ "
ActualizarPqrsEstado
¹¹ '
(
¹¹' (
	ecop_PQRS
¹¹( 1
obj
¹¹2 5
,
¹¹5 6
ref
¹¹7 : 
MessageResponseOBJ
¹¹; M
MsgRes
¹¹N T
)
¹¹T U
{
ºº 	
return
»» 
DACActualiza
»» 
.
»»  "
ActualizarPqrsEstado
»»  4
(
»»4 5
obj
»»5 8
,
»»8 9
ref
»»: =
MsgRes
»»> D
)
»»D E
;
»»E F
}
¼¼ 	
public
ÀÀ 
List
ÀÀ 
<
ÀÀ '
vw_ecop_PQRS_correo_direc
ÀÀ -
>
ÀÀ- . 
ConsultaPQRSCorreo
ÀÀ/ A
(
ÀÀA B
)
ÀÀB C
{
ÁÁ 	
return
ÂÂ 
DACConsulta
ÂÂ 
.
ÂÂ  
ConsultaPQRSCorreo
ÂÂ 1
(
ÂÂ1 2
)
ÂÂ2 3
;
ÂÂ3 4
}
ÃÃ 	
public
ÆÆ 
void
ÆÆ 
EliminarPQRS
ÆÆ  
(
ÆÆ  !
int
ÆÆ! $
id_ecop_PQRS
ÆÆ% 1
,
ÆÆ1 2
ref
ÆÆ3 6 
MessageResponseOBJ
ÆÆ7 I
MsgRes
ÆÆJ P
)
ÆÆP Q
{
ÇÇ 	

DACElimina
ÈÈ 
.
ÈÈ 
EliminarPQRS
ÈÈ #
(
ÈÈ# $
id_ecop_PQRS
ÈÈ$ 0
,
ÈÈ0 1
ref
ÈÈ2 5
MsgRes
ÈÈ6 <
)
ÈÈ< =
;
ÈÈ= >
}
ÉÉ 	
public
ËË 
Int32
ËË "
InsertarPQRSEliminar
ËË )
(
ËË) *"
Log_eliminacion_pqrs
ËË* >
OBJ
ËË? B
,
ËËB C
ref
ËËD G 
MessageResponseOBJ
ËËH Z
MsgRes
ËË[ a
)
ËËa b
{
ÌÌ 	
return
ÍÍ 

DACInserta
ÍÍ 
.
ÍÍ "
InsertarPQRSEliminar
ÍÍ 2
(
ÍÍ2 3
OBJ
ÍÍ3 6
,
ÍÍ6 7
ref
ÍÍ8 ;
MsgRes
ÍÍ< B
)
ÍÍB C
;
ÍÍC D
}
ÎÎ 	
public
ĞĞ 
List
ĞĞ 
<
ĞĞ "
vw_prestadores_lotes
ĞĞ (
>
ĞĞ( )"
GetRecepcionFacturas
ĞĞ* >
(
ĞĞ> ?
ref
ĞĞ? B 
MessageResponseOBJ
ĞĞC U
MsgRes
ĞĞV \
)
ĞĞ\ ]
{
ÑÑ 	
return
ÒÒ 
DACConsulta
ÒÒ 
.
ÒÒ "
GetRecepcionFacturas
ÒÒ 3
(
ÒÒ3 4
ref
ÒÒ4 7
MsgRes
ÒÒ8 >
)
ÒÒ> ?
;
ÒÒ? @
}
ÓÓ 	
public
ÕÕ 
List
ÕÕ 
<
ÕÕ  
vw_analistas_lotes
ÕÕ &
>
ÕÕ& '+
GetRecepcionFacturasAnalistas
ÕÕ( E
(
ÕÕE F
ref
ÕÕF I 
MessageResponseOBJ
ÕÕJ \
MsgRes
ÕÕ] c
)
ÕÕc d
{
ÖÖ 	
return
×× 
DACConsulta
×× 
.
×× +
GetRecepcionFacturasAnalistas
×× <
(
××< =
ref
××= @
MsgRes
××A G
)
××G H
;
××H I
}
ØØ 	
public
ÙÙ 
List
ÙÙ 
<
ÙÙ "
vw_prestadores_lotes
ÙÙ (
>
ÙÙ( )#
GetRecepcionFacturas2
ÙÙ* ?
(
ÙÙ? @
ref
ÙÙ@ C 
MessageResponseOBJ
ÙÙD V
MsgRes
ÙÙW ]
)
ÙÙ] ^
{
ÚÚ 	
return
ÛÛ 
DACConsulta
ÛÛ 
.
ÛÛ #
GetRecepcionFacturas2
ÛÛ 4
(
ÛÛ4 5
ref
ÛÛ5 8
MsgRes
ÛÛ9 ?
)
ÛÛ? @
;
ÛÛ@ A
}
ÜÜ 	
public
ŞŞ 
List
ŞŞ 
<
ŞŞ 2
$managment_prestadores_facturasResult
ŞŞ 8
>
ŞŞ8 9&
GetFacturasByIdRecepcion
ŞŞ: R
(
ŞŞR S
int
ŞŞS V
idrecepcion
ŞŞW b
,
ŞŞb c
ref
ŞŞd g 
MessageResponseOBJ
ŞŞh z
MsgResŞŞ{ 
)ŞŞ ‚
{
ßß 	
return
àà 
DACConsulta
àà 
.
àà &
GetFacturasByIdRecepcion
àà 7
(
àà7 8
idrecepcion
àà8 C
,
ààC D
ref
ààE H
MsgRes
ààI O
)
ààO P
;
ààP Q
}
áá 	
public
ãã 
List
ãã 
<
ãã 2
$managment_prestadores_facturasResult
ãã 8
>
ãã8 9

GetFactura
ãã: D
(
ããD E
int
ããE H
idrecepcion
ããI T
,
ããT U
int
ããV Y
	iddetalle
ããZ c
,
ããc d
ref
ããe h 
MessageResponseOBJ
ããi {
MsgResãã| ‚
)ãã‚ ƒ
{
ää 	
return
åå 
DACConsulta
åå 
.
åå 

GetFactura
åå )
(
åå) *
idrecepcion
åå* 5
,
åå5 6
	iddetalle
åå7 @
,
åå@ A
ref
ååB E
MsgRes
ååF L
)
ååL M
;
ååM N
}
ææ 	
public
çç 5
'managment_prestadores_facturas_GDResult
çç 6
GetFacturaGD
çç7 C
(
ççC D
int
ççD G
	iddetalle
ççH Q
,
ççQ R
ref
ççS V 
MessageResponseOBJ
ççW i
MsgRes
ççj p
)
ççp q
{
èè 	
return
éé 
DACConsulta
éé 
.
éé 
GetFacturaGD
éé +
(
éé+ ,
	iddetalle
éé, 5
,
éé5 6
ref
éé7 :
MsgRes
éé; A
)
ééA B
;
ééB C
}
êê 	
public
ëë 9
+managment_prestadores_facturas_GD_zipResult
ëë :
GetFacturaGD2
ëë; H
(
ëëH I
int
ëëI L
	iddetalle
ëëM V
,
ëëV W
ref
ëëX [ 
MessageResponseOBJ
ëë\ n
MsgRes
ëëo u
)
ëëu v
{
ìì 	
return
íí 
DACConsulta
íí 
.
íí 
GetFacturaGD2
íí ,
(
íí, -
	iddetalle
íí- 6
,
íí6 7
ref
íí8 ;
MsgRes
íí< B
)
ííB C
;
ííC D
}
îî 	
public
ğğ 
List
ğğ 
<
ğğ 6
(managmentprestadoresfacturasestadoResult
ğğ <
>
ğğ< =!
GetFacturasByEstado
ğğ> Q
(
ğğQ R
int
ğğR U
idestado
ğğV ^
,
ğğ^ _
ref
ğğ` c 
MessageResponseOBJ
ğğd v
MsgRes
ğğw }
)
ğğ} ~
{
ññ 	
return
òò 
DACConsulta
òò 
.
òò !
GetFacturasByEstado
òò 2
(
òò2 3
idestado
òò3 ;
,
òò; <
ref
òò= @
MsgRes
òòA G
)
òòG H
;
òòH I
}
óó 	
public
ôô 
List
ôô 
<
ôô 9
+managmentprestadoresfacturasaceptadasResult
ôô ?
>
ôô? @"
GetFacturasAceptadas
ôôA U
(
ôôU V
int
ôôV Y
idestado
ôôZ b
,
ôôb c
int
ôôd g

id_usuario
ôôh r
,
ôôr s
ref
ôôt w!
MessageResponseOBJôôx Š
MsgResôô‹ ‘
)ôô‘ ’
{
õõ 	
return
öö 
DACConsulta
öö 
.
öö "
GetFacturasAceptadas
öö 3
(
öö3 4
idestado
öö4 <
,
öö< =

id_usuario
öö> H
,
ööH I
ref
ööJ M
MsgRes
ööN T
)
ööT U
;
ööU V
}
÷÷ 	
public
øø 
List
øø 
<
øø ;
-managmentprestadoresfacturasaceptadasOKResult
øø A
>
øøA B#
GetFacturasAceptadas2
øøC X
(
øøX Y
ref
øøY \ 
MessageResponseOBJ
øø] o
MsgRes
øøp v
)
øøv w
{
ùù 	
return
úú 
DACConsulta
úú 
.
úú #
GetFacturasAceptadas2
úú 4
(
úú4 5
ref
úú5 8
MsgRes
úú9 ?
)
úú? @
;
úú@ A
}
ûû 	
public
üü 
List
üü 
<
üü ;
-managmentprestadoresfacturasgestionadasResult
üü A
>
üüA B 
GetGestionFacturas
üüC U
(
üüU V
)
üüV W
{
ıı 	
return
şş 
DACConsulta
şş 
.
şş  
GetGestionFacturas
şş 1
(
şş1 2
)
şş2 3
;
şş3 4
}
ÿÿ 	
public
‚‚ 
List
‚‚ 
<
‚‚ A
3managmentprestadoresfacturasgestionadasFechasResult
‚‚ G
>
‚‚G H&
GetGestionFacturasFechas
‚‚I a
(
‚‚a b
DateTime
‚‚b j
fechaIni
‚‚k s
,
‚‚s t
DateTime
‚‚u }
fechaFin‚‚~ †
)‚‚† ‡
{
ƒƒ 	
return
„„ 
DACConsulta
„„ 
.
„„ &
GetGestionFacturasFechas
„„ 7
(
„„7 8
fechaIni
„„8 @
,
„„@ A
fechaFin
„„B J
)
„„J K
;
„„K L
}
…… 	
public
‡‡ 
List
‡‡ 
<
‡‡ ;
-managmentprestadoresfacturasgestionadasResult
‡‡ A
>
‡‡A B"
GetGestionFacturasv2
‡‡C W
(
‡‡W X
int
‡‡X [
?
‡‡[ \
	idDetalle
‡‡] f
,
‡‡f g
DateTime
‡‡h p
?
‡‡p q
fechainicial
‡‡r ~
,
‡‡~ 
DateTime‡‡€ ˆ
?‡‡ˆ ‰

fechafinal‡‡Š ”
,‡‡” •
String‡‡– œ
estado‡‡ £
,‡‡£ ¤
int‡‡¥ ¨
?‡‡¨ ©
regional‡‡ª ²
,‡‡² ³
String‡‡´ º
	prestador‡‡» Ä
,‡‡Ä Å
String‡‡Æ Ì
nit‡‡Í Ğ
,‡‡Ğ Ñ
String‡‡Ò Ø
numFac‡‡Ù ß
)‡‡ß à
{
ˆˆ 	
return
‰‰ 
DACConsulta
‰‰ 
.
‰‰ "
GetGestionFacturasv2
‰‰ 3
(
‰‰3 4
	idDetalle
‰‰4 =
,
‰‰= >
fechainicial
‰‰? K
,
‰‰K L

fechafinal
‰‰M W
,
‰‰W X
estado
‰‰Y _
,
‰‰_ `
regional
‰‰a i
,
‰‰i j
	prestador
‰‰k t
,
‰‰t u
nit
‰‰v y
,
‰‰y z
numFac‰‰{ 
)‰‰ ‚
;‰‰‚ ƒ
}
ŠŠ 	
public
‹‹ 
List
‹‹ 
<
‹‹ C
5managmentprestadoresfacturasgestionadasCompletaResult
‹‹ I
>
‹‹I J"
GetGestionFacturasv3
‹‹K _
(
‹‹_ `
String
‹‹` f
numFac
‹‹g m
,
‹‹m n
String
‹‹o u
nit
‹‹v y
,
‹‹y z
String‹‹{ 
	prestador‹‹‚ ‹
,‹‹‹ Œ
String‹‹ “
sap‹‹” —
,‹‹— ˜
int‹‹™ œ
?‹‹œ 
estado‹‹ ¤
,‹‹¤ ¥
int‹‹¦ ©
?‹‹© ª
	idDetalle‹‹« ´
)‹‹´ µ
{
ŒŒ 	
return
 
DACConsulta
 
.
 "
GetGestionFacturasv3
 3
(
3 4
numFac
4 :
,
: ;
nit
< ?
,
? @
	prestador
A J
,
J K
sap
L O
,
O P
estado
Q W
,
W X
	idDetalle
Y b
)
b c
;
c d
}
 	
public
 
List
 
<
 G
9managmentprestadoresfacturasgestionadasTrazabilidadResult
 M
>
M N,
GetGestionFacturasTrazabilidad
O m
(
m n
)
n o
{
‘‘ 	
return
’’ 
DACConsulta
’’ 
.
’’ ,
GetGestionFacturasTrazabilidad
’’ =
(
’’= >
)
’’> ?
;
’’? @
}
““ 	
public
•• 
List
•• 
<
•• G
9managmentprestadoresfacturasgestionadasTrazabilidadResult
•• M
>
••M N.
 GetGestionFacturasTrazabilidadV2
••O o
(
••o p
DateTime
••p x
?
••x y
fechainicial••z †
,••† ‡
DateTime••ˆ 
?•• ‘

fechafinal••’ œ
,••œ 
String•• ¤
estado••¥ «
,••« ¬
int••­ °
?••° ±
regional••² º
,••º »
String••¼ Â
	prestador••Ã Ì
,••Ì Í
String••Î Ô
nit••Õ Ø
,••Ø Ù
String••Ú à
numFac••á ç
)••ç è
{
–– 	
return
—— 
DACConsulta
—— 
.
—— .
 GetGestionFacturasTrazabilidadV2
—— ?
(
——? @
fechainicial
——@ L
,
——L M

fechafinal
——N X
,
——X Y
estado
——Z `
,
——` a
regional
——b j
,
——j k
	prestador
——l u
,
——u v
nit
——w z
,
——z {
numFac——| ‚
)——‚ ƒ
;——ƒ „
}
˜˜ 	
public
›› 
List
›› 
<
›› >
0managmentprestadores_estados_factura_totalResult
›› D
>
››D E
GetTotalFacturas
››F V
(
››V W
)
››W X
{
œœ 	
return
 
DACConsulta
 
.
 
GetTotalFacturas
 /
(
/ 0
)
0 1
;
1 2
}
 	
public
   
List
   
<
   /
!vw_ref_estado_factura_total_rango
   5
>
  5 6'
GetRecepcionFacturasRango
  7 P
(
  P Q
Int32
  Q V
opc
  W Z
)
  Z [
{
¡¡ 	
return
¢¢ 
DACConsulta
¢¢ 
.
¢¢ '
GetRecepcionFacturasRango
¢¢ 8
(
¢¢8 9
opc
¢¢9 <
)
¢¢< =
;
¢¢= >
}
££ 	
public
¥¥ 
List
¥¥ 
<
¥¥ 0
"managmentprestadoresFacturasResult
¥¥ 6
>
¥¥6 7*
GetFacturasByEstadoAceptadas
¥¥8 T
(
¥¥T U
int
¥¥U X
idestado
¥¥Y a
,
¥¥a b
ref
¥¥c f 
MessageResponseOBJ
¥¥g y
MsgRes¥¥z €
)¥¥€ 
{
¦¦ 	
return
§§ 
DACConsulta
§§ 
.
§§ *
GetFacturasByEstadoAceptadas
§§ ;
(
§§; <
idestado
§§< D
,
§§D E
ref
§§F I
MsgRes
§§J P
)
§§P Q
;
§§Q R
}
¨¨ 	
public
ªª 
List
ªª 
<
ªª :
,managmentprestadoresFacturas_devueltasResult
ªª @
>
ªª@ A*
GetFacturasByEstadoDevueltas
ªªB ^
(
ªª^ _
int
ªª_ b
idestado
ªªc k
,
ªªk l
int
ªªm p
?
ªªp q
id
ªªr t
,
ªªt u
ref
ªªv y!
MessageResponseOBJªªz Œ
MsgResªª “
)ªª“ ”
{
«« 	
return
¬¬ 
DACConsulta
¬¬ 
.
¬¬ *
GetFacturasByEstadoDevueltas
¬¬ ;
(
¬¬; <
idestado
¬¬< D
,
¬¬D E
id
¬¬F H
,
¬¬H I
ref
¬¬J M
MsgRes
¬¬N T
)
¬¬T U
;
¬¬U V
}
­­ 	
public
®® 
List
®® 
<
®® 6
(managmentprestadoresFacturas_rangoResult
®® <
>
®®< =/
!GetFacturasByEstadoAceptadasRango
®®> _
(
®®_ `
int
®®` c
rango
®®d i
,
®®i j
Int32
®®k p
id_regional
®®q |
)
®®| }
{
¯¯ 	
return
°° 
DACConsulta
°° 
.
°° /
!GetFacturasByEstadoAceptadasRango
°° @
(
°°@ A
rango
°°A F
,
°°F G
id_regional
°°H S
)
°°S T
;
°°T U
}
±± 	
public
³³ 
List
³³ 
<
³³ 7
)managmentprestadoresFacturasAuditorResult
³³ =
>
³³= >"
GetFacturasByAuditor
³³? S
(
³³S T
int
³³T W
idestado
³³X `
,
³³` a
int
³³b e

id_usuario
³³f p
,
³³p q
ref
³³r u!
MessageResponseOBJ³³v ˆ
MsgRes³³‰ 
)³³ 
{
´´ 	
return
µµ 
DACConsulta
µµ 
.
µµ "
GetFacturasByAuditor
µµ 3
(
µµ3 4
idestado
µµ4 <
,
µµ< =

id_usuario
µµ> H
,
µµH I
ref
µµJ M
MsgRes
µµN T
)
µµT U
;
µµU V
}
¶¶ 	
public
¸¸ 
List
¸¸ 
<
¸¸ 9
+managmentprestadoresFacturasAuditorOKResult
¸¸ ?
>
¸¸? @#
GetFacturasByAuditor2
¸¸A V
(
¸¸V W
ref
¸¸W Z 
MessageResponseOBJ
¸¸[ m
MsgRes
¸¸n t
)
¸¸t u
{
¹¹ 	
return
ºº 
DACConsulta
ºº 
.
ºº #
GetFacturasByAuditor2
ºº 4
(
ºº4 5
ref
ºº5 8
MsgRes
ºº9 ?
)
ºº? @
;
ºº@ A
}
»» 	
public
¼¼ 
List
¼¼ 
<
¼¼ A
3managmentprestadoresFacturasAuditorOKCompletaResult
¼¼ G
>
¼¼G H#
GetFacturasByAuditor3
¼¼I ^
(
¼¼^ _
int
¼¼_ b
	idAuditor
¼¼c l
)
¼¼l m
{
½½ 	
return
¾¾ 
DACConsulta
¾¾ 
.
¾¾ #
GetFacturasByAuditor3
¾¾ 4
(
¾¾4 5
	idAuditor
¾¾5 >
)
¾¾> ?
;
¾¾? @
}
¿¿ 	
public
ÀÀ 
List
ÀÀ 
<
ÀÀ 9
+managmentprestadoresFacturasAprobadasResult
ÀÀ ?
>
ÀÀ? @"
GetFacturasAprobadas
ÀÀA U
(
ÀÀU V
int
ÀÀV Y
idestado
ÀÀZ b
,
ÀÀb c
ref
ÀÀd g 
MessageResponseOBJ
ÀÀh z
MsgResÀÀ{ 
)ÀÀ ‚
{
ÁÁ 	
return
ÂÂ 
DACConsulta
ÂÂ 
.
ÂÂ "
GetFacturasAprobadas
ÂÂ 3
(
ÂÂ3 4
idestado
ÂÂ4 <
,
ÂÂ< =
ref
ÂÂ> A
MsgRes
ÂÂB H
)
ÂÂH I
;
ÂÂI J
}
ÃÃ 	
public
ÄÄ 
List
ÄÄ 
<
ÄÄ 7
)managmentprestadoresFacturasReporteResult
ÄÄ =
>
ÄÄ= >(
GetFacturasByEstadoReporte
ÄÄ? Y
(
ÄÄY Z
int
ÄÄZ ]
idestado
ÄÄ^ f
,
ÄÄf g
ref
ÄÄh k 
MessageResponseOBJ
ÄÄl ~
MsgResÄÄ …
)ÄÄ… †
{
ÅÅ 	
return
ÆÆ 
DACConsulta
ÆÆ 
.
ÆÆ (
GetFacturasByEstadoReporte
ÆÆ 9
(
ÆÆ9 :
idestado
ÆÆ: B
,
ÆÆB C
ref
ÆÆD G
MsgRes
ÆÆH N
)
ÆÆN O
;
ÆÆO P
}
ÇÇ 	
public
ÉÉ 
List
ÉÉ 
<
ÉÉ 3
%managmentRechazoFacturasReporteResult
ÉÉ 9
>
ÉÉ9 :)
GetFacturasByRechazoReporte
ÉÉ; V
(
ÉÉV W
int
ÉÉW Z
id_dtll
ÉÉ[ b
,
ÉÉb c
ref
ÉÉd g 
MessageResponseOBJ
ÉÉh z
MsgResÉÉ{ 
)ÉÉ ‚
{
ÊÊ 	
return
ËË 
DACConsulta
ËË 
.
ËË )
GetFacturasByRechazoReporte
ËË :
(
ËË: ;
id_dtll
ËË; B
,
ËËB C
ref
ËËD G
MsgRes
ËËH N
)
ËËN O
;
ËËO P
}
ÌÌ 	
public
ÎÎ 
List
ÎÎ 
<
ÎÎ 7
)managmentRechazoLoteFacturasReporteResult
ÎÎ =
>
ÎÎ= >-
GetLoteFacturasByRechazoReporte
ÎÎ? ^
(
ÎÎ^ _
int
ÎÎ_ b
id_lote
ÎÎc j
,
ÎÎj k
ref
ÎÎl o!
MessageResponseOBJÎÎp ‚
MsgResÎÎƒ ‰
)ÎÎ‰ Š
{
ÏÏ 	
return
ĞĞ 
DACConsulta
ĞĞ 
.
ĞĞ -
GetLoteFacturasByRechazoReporte
ĞĞ >
(
ĞĞ> ?
id_lote
ĞĞ? F
,
ĞĞF G
ref
ĞĞH K
MsgRes
ĞĞL R
)
ĞĞR S
;
ĞĞS T
}
ÑÑ 	
public
ÓÓ 
List
ÓÓ 
<
ÓÓ ;
-managmentRechazoLoteDtllFacturasReporteResult
ÓÓ A
>
ÓÓA B1
#GetLoteFacturasdtllByRechazoReporte
ÓÓC f
(
ÓÓf g
int
ÓÓg j
id_lote
ÓÓk r
,
ÓÓr s
ref
ÓÓt w!
MessageResponseOBJÓÓx Š
MsgResÓÓ‹ ‘
)ÓÓ‘ ’
{
ÔÔ 	
return
ÕÕ 
DACConsulta
ÕÕ 
.
ÕÕ 1
#GetLoteFacturasdtllByRechazoReporte
ÕÕ B
(
ÕÕB C
id_lote
ÕÕC J
,
ÕÕJ K
ref
ÕÕL O
MsgRes
ÕÕP V
)
ÕÕV W
;
ÕÕW X
}
ÖÖ 	
public
ØØ 
List
ØØ 
<
ØØ ;
-managment_prestadores_soportes_clinicosResult
ØØ A
>
ØØA B%
GetSoportesClinicosList
ØØC Z
(
ØØZ [
int
ØØ[ ^
idcargue
ØØ_ g
,
ØØg h
int
ØØi l
detalle
ØØm t
)
ØØt u
{
ÙÙ 	
return
ÚÚ 
DACConsulta
ÚÚ 
.
ÚÚ %
GetSoportesClinicosList
ÚÚ 6
(
ÚÚ6 7
idcargue
ÚÚ7 ?
,
ÚÚ? @
detalle
ÚÚA H
)
ÚÚH I
;
ÚÚI J
}
ÛÛ 	
public
İİ 
List
İİ 
<
İİ 4
&managment_prestadores_documentosResult
İİ :
>
İİ: ;
GetSoportesList
İİ< K
(
İİK L
int
İİL O
detalle
İİP W
)
İİW X
{
ŞŞ 	
return
ßß 
DACConsulta
ßß 
.
ßß 
GetSoportesList
ßß .
(
ßß. /
detalle
ßß/ 6
)
ßß6 7
;
ßß7 8
}
àà 	
public
ââ 
List
ââ 
<
ââ -
managment_ffmm_documentosResult
ââ 3
>
ââ3 4!
GetSoportesListFFMM
ââ5 H
(
ââH I
int
ââI L
detalle
ââM T
)
ââT U
{
ãã 	
return
ää 
DACConsulta
ää 
.
ää !
GetSoportesListFFMM
ää 2
(
ää2 3
detalle
ää3 :
)
ää: ;
;
ää; <
}
åå 	
public
ææ 6
(management_prestadores_get_soporteResult
ææ 7
Getsoporteclinico
ææ8 I
(
ææI J
int
ææJ M

idsoportee
ææN X
)
ææX Y
{
çç 	
return
èè 
DACConsulta
èè 
.
èè 
Getsoporteclinico
èè 0
(
èè0 1

idsoportee
èè1 ;
)
èè; <
;
èè< =
}
éé 	
public
ëë 
List
ëë 
<
ëë 
ref_rechazos_Fac
ëë $
>
ëë$ %!
Getref_rechazos_Fac
ëë& 9
(
ëë9 :
ref
ëë: = 
MessageResponseOBJ
ëë> P
MsgRes
ëëQ W
)
ëëW X
{
ìì 	
return
íí 
DACConsulta
íí 
.
íí !
Getref_rechazos_Fac
íí 2
(
íí2 3
ref
íí3 6
MsgRes
íí7 =
)
íí= >
;
íí> ?
}
îî 	
public
ïï 
List
ïï 
<
ïï "
vw_auditores_totales
ïï (
>
ïï( )
GetAuditorTotales
ïï* ;
(
ïï; <
ref
ïï< ? 
MessageResponseOBJ
ïï@ R
MsgRes
ïïS Y
)
ïïY Z
{
ğğ 	
return
ññ 
DACConsulta
ññ 
.
ññ 
GetAuditorTotales
ññ 0
(
ññ0 1
ref
ññ1 4
MsgRes
ññ5 ;
)
ññ; <
;
ññ< =
}
òò 	
public
ôô 
List
ôô 
<
ôô '
vw_auditores_totales_pqrs
ôô -
>
ôô- .#
GetAuditorTotalesPqrs
ôô/ D
(
ôôD E
ref
ôôE H 
MessageResponseOBJ
ôôI [
MsgRes
ôô\ b
)
ôôb c
{
õõ 	
return
öö 
DACConsulta
öö 
.
öö #
GetAuditorTotalesPqrs
öö 4
(
öö4 5
ref
öö5 8
MsgRes
öö9 ?
)
öö? @
;
öö@ A
}
÷÷ 	
public
ùù 
List
ùù 
<
ùù 7
)managment_prestadores_list_rechazosResult
ùù =
>
ùù= >&
GetFacturasByRechazoList
ùù? W
(
ùùW X
int
ùùX [
id_dtll
ùù\ c
,
ùùc d
ref
ùùe h 
MessageResponseOBJ
ùùi {
MsgResùù| ‚
)ùù‚ ƒ
{
úú 	
return
ûû 
DACConsulta
ûû 
.
ûû &
GetFacturasByRechazoList
ûû 7
(
ûû7 8
id_dtll
ûû8 ?
,
ûû? @
ref
ûûA D
MsgRes
ûûE K
)
ûûK L
;
ûûL M
}
üü 	
public
şş 
void
şş !
ActualizarEnvioPQRS
şş '
(
şş' (
Int32
şş( -
id_ecop_PQRS
şş. :
,
şş: ;
String
şş< B
usuario
şşC J
,
şşJ K
ref
şşL O 
MessageResponseOBJ
şşP b
MsgRes
şşc i
)
şşi j
{
ÿÿ 	
DACActualiza
€€ 
.
€€ !
ActualizarEnvioPQRS
€€ ,
(
€€, -
id_ecop_PQRS
€€- 9
,
€€9 :
usuario
€€; B
,
€€B C
ref
€€D G
MsgRes
€€H N
)
€€N O
;
€€O P
}
 	
public
ƒƒ 
ref_solucionador
ƒƒ #
getSolucionadorNombre
ƒƒ  5
(
ƒƒ5 6
string
ƒƒ6 <
nombre
ƒƒ= C
,
ƒƒC D
string
ƒƒE K
auxsolucionador
ƒƒL [
)
ƒƒ[ \
{
„„ 	
return
…… 
DACConsulta
…… 
.
…… #
getSolucionadorNombre
…… 4
(
……4 5
nombre
……5 ;
,
……; <
auxsolucionador
……= L
)
……L M
;
……M N
}
†† 	
public
ˆˆ 
ref_solucionador
ˆˆ "
TraerAuxSolucionador
ˆˆ  4
(
ˆˆ4 5
string
ˆˆ5 ;
	nombreAux
ˆˆ< E
)
ˆˆE F
{
‰‰ 	
return
ŠŠ 
DACConsulta
ŠŠ 
.
ŠŠ "
TraerAuxSolucionador
ŠŠ 3
(
ŠŠ3 4
	nombreAux
ŠŠ4 =
)
ŠŠ= >
;
ŠŠ> ?
}
‹‹ 	
public
 
List
 
<
 #
Ref_PQRS_correo_envio
 )
>
) *$
ConsultaPQRSref_correo
+ A
(
A B
)
B C
{
 	
return
 
DACConsulta
 
.
 $
ConsultaPQRSref_correo
 5
(
5 6
)
6 7
;
7 8
}
 	
public
’’ 
List
’’ 
<
’’ %
Ref_PQRS_categorizacion
’’ +
>
’’+ ,(
ConsultaPQRSCategorizacion
’’- G
(
’’G H
)
’’H I
{
““ 	
return
”” 
DACConsulta
”” 
.
”” (
ConsultaPQRSCategorizacion
”” 9
(
””9 :
)
””: ;
;
””; <
}
•• 	
public
—— 
List
—— 
<
—— 3
%management_pqrs_tablero_controlResult
—— 9
>
——9 : 
GestiontableroPQRS
——; M
(
——M N
)
——N O
{
˜˜ 	
return
™™ 
DACConsulta
™™ 
.
™™  
GestiontableroPQRS
™™ 1
(
™™1 2
)
™™2 3
;
™™3 4
}
šš 	
public
›› 
List
›› 
<
›› F
8management_pqrs_tablero_control_proyectadasFinalesResult
›› L
>
››L M0
"DatosTableroPqrsProyectadasFinales
››N p
(
››p q
)
››q r
{
œœ 	
return
 
DACConsulta
 
.
 0
"DatosTableroPqrsProyectadasFinales
 A
(
A B
)
B C
;
C D
}
 	
public
ŸŸ 
List
ŸŸ 
<
ŸŸ 5
'management_pqrs_proyectadasCierreResult
ŸŸ ;
>
ŸŸ; </
!DatosTableroPqrsProyectadasCierre
ŸŸ= ^
(
ŸŸ^ _
)
ŸŸ_ `
{
   	
return
¡¡ 
DACConsulta
¡¡ 
.
¡¡ /
!DatosTableroPqrsProyectadasCierre
¡¡ @
(
¡¡@ A
)
¡¡A B
;
¡¡B C
}
¢¢ 	
public
££ 
List
££ 
<
££ ?
1management_pqrs_tablero_control_proyectadasResult
££ E
>
££E F+
GestiontableroPQRSProyectadas
££G d
(
££d e
string
££e k
numCaso
££l s
,
££s t
string
££u {
numOpc££| ‚
,££‚ ƒ
string££„ Š
numDocumento££‹ —
,££— ˜
DateTime££™ ¡
?££¡ ¢
fechaInicial£££ ¯
,££¯ °
DateTime££± ¹
?££¹ º

fechaFinal££» Å
,££Å Æ
int££Ç Ê
?££Ê Ë
idPqr££Ì Ñ
)££Ñ Ò
{
¤¤ 	
return
¥¥ 
DACConsulta
¥¥ 
.
¥¥ +
GestiontableroPQRSProyectadas
¥¥ <
(
¥¥< =
numCaso
¥¥= D
,
¥¥D E
numOpc
¥¥F L
,
¥¥L M
numDocumento
¥¥N Z
,
¥¥Z [
fechaInicial
¥¥\ h
,
¥¥h i

fechaFinal
¥¥j t
,
¥¥t u
idPqr
¥¥v {
)
¥¥{ |
;
¥¥| }
}
¦¦ 	
public
§§ 
List
§§ 
<
§§ 6
(management_pqrs_TableroseguimientoResult
§§ <
>
§§< =,
GestiontableeroSeguimientoPqrs
§§> \
(
§§\ ]
string
§§] c
usuario
§§d k
,
§§k l
string
§§m s
solucionador§§t €
)§§€ 
{
¨¨ 	
return
©© 
DACConsulta
©© 
.
©© ,
GestiontableeroSeguimientoPqrs
©© =
(
©©= >
usuario
©©> E
,
©©E F
solucionador
©©G S
)
©©S T
;
©©T U
}
ªª 	
public
«« 
List
«« 
<
«« 
ref_solucionador
«« $
>
««$ %
getSolucionador
««& 5
(
««5 6
int
««6 9
idCiudad
««: B
)
««B C
{
¬¬ 	
return
­­ 
DACConsulta
­­ 
.
­­ 
getSolucionador
­­ .
(
­­. /
idCiudad
­­/ 7
)
­­7 8
;
­­8 9
}
®® 	
public
°° 
List
°° 
<
°° 
ref_solucionador
°° $
>
°°$ % 
getSolucionadorReg
°°& 8
(
°°8 9
int
°°9 <

idRegional
°°= G
)
°°G H
{
±± 	
return
²² 
DACConsulta
²² 
.
²²  
getSolucionadorReg
²² 1
(
²²1 2

idRegional
²²2 <
)
²²< =
;
²²= >
}
³³ 	
public
µµ 
List
µµ 
<
µµ /
!management_ref_solucionadorResult
µµ 5
>
µµ5 6'
getSolucionadorRegActivos
µµ7 P
(
µµP Q
int
µµQ T

idRegional
µµU _
)
µµ_ `
{
¶¶ 	
return
·· 
DACConsulta
·· 
.
·· '
getSolucionadorRegActivos
·· 8
(
··8 9

idRegional
··9 C
)
··C D
;
··D E
}
¸¸ 	
public
ºº 
List
ºº 
<
ºº 
ref_solucionador
ºº $
>
ºº$ %%
getSolucionadorRegional
ºº& =
(
ºº= >
int
ºº> A
?
ººA B

idRegional
ººC M
)
ººM N
{
»» 	
return
¼¼ 
DACConsulta
¼¼ 
.
¼¼ %
getSolucionadorRegional
¼¼ 6
(
¼¼6 7

idRegional
¼¼7 A
)
¼¼A B
;
¼¼B C
}
½½ 	
public
¿¿ 
List
¿¿ 
<
¿¿ 
Ref_ciudades
¿¿  
>
¿¿  !
TotalCiudades
¿¿" /
(
¿¿/ 0
)
¿¿0 1
{
ÀÀ 	
return
ÁÁ 
DACConsulta
ÁÁ 
.
ÁÁ 
TotalCiudades
ÁÁ ,
(
ÁÁ, -
)
ÁÁ- .
;
ÁÁ. /
}
ÂÂ 	
public
ÄÄ 
Ref_ciudades
ÄÄ 

CiudadesId
ÄÄ &
(
ÄÄ& '
int
ÄÄ' *
?
ÄÄ* +
id
ÄÄ, .
)
ÄÄ. /
{
ÅÅ 	
return
ÆÆ 
DACConsulta
ÆÆ 
.
ÆÆ 

CiudadesId
ÆÆ )
(
ÆÆ) *
id
ÆÆ* ,
)
ÆÆ, -
;
ÆÆ- .
}
ÇÇ 	
public
ÉÉ 
List
ÉÉ 
<
ÉÉ 
ref_solucionador
ÉÉ $
>
ÉÉ$ %"
getSolucionadorTotal
ÉÉ& :
(
ÉÉ: ;
)
ÉÉ; <
{
ÊÊ 	
return
ËË 
DACConsulta
ËË 
.
ËË "
getSolucionadorTotal
ËË 3
(
ËË3 4
)
ËË4 5
;
ËË5 6
}
ÌÌ 	
public
ÍÍ 
List
ÍÍ 
<
ÍÍ 2
$Management_PQRS_solucionadoresResult
ÍÍ 8
>
ÍÍ8 9 
getSolucionadorAux
ÍÍ: L
(
ÍÍL M
)
ÍÍM N
{
ÎÎ 	
return
ÏÏ 
DACConsulta
ÏÏ 
.
ÏÏ  
getSolucionadorAux
ÏÏ 1
(
ÏÏ1 2
)
ÏÏ2 3
;
ÏÏ3 4
}
ĞĞ 	
public
ÑÑ 
int
ÑÑ '
ActualizarUsuarioAsignado
ÑÑ ,
(
ÑÑ, -
	ecop_PQRS
ÑÑ- 6
OBJ
ÑÑ7 :
,
ÑÑ: ;
ref
ÑÑ< ? 
MessageResponseOBJ
ÑÑ@ R
MsgRes
ÑÑS Y
)
ÑÑY Z
{
ÒÒ 	
return
ÓÓ 
DACActualiza
ÓÓ 
.
ÓÓ  '
ActualizarUsuarioAsignado
ÓÓ  9
(
ÓÓ9 :
OBJ
ÓÓ: =
,
ÓÓ= >
ref
ÓÓ? B
MsgRes
ÓÓC I
)
ÓÓI J
;
ÓÓJ K
}
ÔÔ 	
public
ÕÕ 
int
ÕÕ )
ActualizarCategorizacionPQR
ÕÕ .
(
ÕÕ. /
	ecop_PQRS
ÕÕ/ 8
OBJ
ÕÕ9 <
,
ÕÕ< =
ref
ÕÕ> A 
MessageResponseOBJ
ÕÕB T
MsgRes
ÕÕU [
)
ÕÕ[ \
{
ÖÖ 	
return
×× 
DACActualiza
×× 
.
××  )
ActualizarCategorizacionPQR
××  ;
(
××; <
OBJ
××< ?
,
××? @
ref
××A D
MsgRes
××E K
)
××K L
;
××L M
}
ØØ 	
public
ÙÙ 
List
ÙÙ 
<
ÙÙ 8
*management_facturas_sinDocumentacionResult
ÙÙ >
>
ÙÙ> ?&
ListaFacturasIncompletas
ÙÙ@ X
(
ÙÙX Y
)
ÙÙY Z
{
ÚÚ 	
return
ÛÛ 
DACConsulta
ÛÛ 
.
ÛÛ &
ListaFacturasIncompletas
ÛÛ 7
(
ÛÛ7 8
)
ÛÛ8 9
;
ÛÛ9 :
}
ÜÜ 	
public
ŞŞ 
int
ŞŞ )
ActualizarAvanzarProyectada
ŞŞ .
(
ŞŞ. /
	ecop_PQRS
ŞŞ/ 8
OBJ
ŞŞ9 <
,
ŞŞ< =
ref
ŞŞ> A 
MessageResponseOBJ
ŞŞB T
MsgRes
ŞŞU [
)
ŞŞ[ \
{
ßß 	
return
àà 
DACActualiza
àà 
.
àà  )
ActualizarAvanzarProyectada
àà  ;
(
àà; <
OBJ
àà< ?
,
àà? @
ref
ààA D
MsgRes
ààE K
)
ààK L
;
ààL M
}
áá 	
public
ââ 
int
ââ (
ActualizarCerrarProyectada
ââ -
(
ââ- .
	ecop_PQRS
ââ. 7
OBJ
ââ8 ;
,
ââ; <
ref
ââ= @ 
MessageResponseOBJ
ââA S
MsgRes
ââT Z
)
ââZ [
{
ãã 	
return
ää 
DACActualiza
ää 
.
ää  (
ActualizarCerrarProyectada
ää  :
(
ää: ;
OBJ
ää; >
,
ää> ?
ref
ää@ C
MsgRes
ääD J
)
ääJ K
;
ääK L
}
åå 	
public
ææ 
int
ææ *
ActualizarDatosReaperturaPQR
ææ /
(
ææ/ 0
	ecop_PQRS
ææ0 9
OBJ
ææ: =
)
ææ= >
{
çç 	
return
èè 
DACActualiza
èè 
.
èè  *
ActualizarDatosReaperturaPQR
èè  <
(
èè< =
OBJ
èè= @
)
èè@ A
;
èèA B
}
éé 	
public
êê 
int
êê '
InsertarLogReaperturaPqrs
êê ,
(
êê, -"
log_pqrs_reaperturas
êê- A
obj
êêB E
)
êêE F
{
ëë 	
return
ìì 

DACInserta
ìì 
.
ìì '
InsertarLogReaperturaPqrs
ìì 7
(
ìì7 8
obj
ìì8 ;
)
ìì; <
;
ìì< =
}
íí 	
public
ïï 
int
ïï /
!InsertarLogCierrePqrsSolucionador
ïï 4
(
ïï4 5+
log_pqrs_cerradasSolucionador
ïï5 R
obj
ïïS V
)
ïïV W
{
ğğ 	
return
ññ 

DACInserta
ññ 
.
ññ /
!InsertarLogCierrePqrsSolucionador
ññ ?
(
ññ? @
obj
ññ@ C
)
ññC D
;
ññD E
}
òò 	
public
ôô 
int
ôô )
CargueMedicamentosRegulados
ôô .
(
ôô. /&
ecop_pqrs_registroMasivo
ôô/ G
obj
ôôH K
,
ôôK L
List
ôôM Q
<
ôôQ R
	ecop_PQRS
ôôR [
>
ôô[ \
detalle
ôô] d
,
ôôd e
ref
ôôf i 
MessageResponseOBJ
ôôj |
MsgResôô} ƒ
)ôôƒ „
{
õõ 	
return
öö 

DACInserta
öö 
.
öö )
CargueMedicamentosRegulados
öö 9
(
öö9 :
obj
öö: =
,
öö= >
detalle
öö? F
,
ööF G
ref
ööH K
MsgRes
ööL R
)
ööR S
;
ööS T
}
÷÷ 	
public
øø 
List
øø 
<
øø 
	ecop_PQRS
øø 
>
øø 
ListadoPqrsMasivo
øø 0
(
øø0 1
int
øø1 4
idMasivo
øø5 =
)
øø= >
{
ùù 	
return
úú 
DACConsulta
úú 
.
úú 
ListadoPqrsMasivo
úú 0
(
úú0 1
idMasivo
úú1 9
)
úú9 :
;
úú: ;
}
ûû 	
public
ıı 
int
ıı +
ActualizarAnalistaAsignadoPqr
ıı 0
(
ıı0 1
	ecop_PQRS
ıı1 :
obj
ıı; >
)
ıı> ?
{
şş 	
return
ÿÿ 
DACActualiza
ÿÿ 
.
ÿÿ  +
ActualizarAnalistaAsignadoPqr
ÿÿ  =
(
ÿÿ= >
obj
ÿÿ> A
)
ÿÿA B
;
ÿÿB C
}
€€ 	
public
 
int
 +
PqrGuardarObservaciopnAuditor
 0
(
0 1,
ecop_pqrs_observacionesAuditor
1 O
obj
P S
)
S T
{
‚‚ 	
return
ƒƒ 

DACInserta
ƒƒ 
.
ƒƒ +
PqrGuardarObservaciopnAuditor
ƒƒ ;
(
ƒƒ; <
obj
ƒƒ< ?
)
ƒƒ? @
;
ƒƒ@ A
}
„„ 	
public
…… 
List
…… 
<
…… 8
*management_pqrs_observacionesAuditorResult
…… >
>
……> ?+
PqrsListaObservacionesAuditor
……@ ]
(
……] ^
int
……^ a
idPqrs
……b h
)
……h i
{
†† 	
return
‡‡ 
DACConsulta
‡‡ 
.
‡‡ +
PqrsListaObservacionesAuditor
‡‡ <
(
‡‡< =
idPqrs
‡‡= C
)
‡‡C D
;
‡‡D E
}
ˆˆ 	
public
‰‰ 
int
‰‰ (
CargueMasivoQuienLlamoPqrs
‰‰ -
(
‰‰- .
List
‰‰. 2
<
‰‰2 3%
ecop_pqrs_a_quien_llamo
‰‰3 J
>
‰‰J K
detalle
‰‰L S
,
‰‰S T
ref
‰‰U X 
MessageResponseOBJ
‰‰Y k
MsgRes
‰‰l r
)
‰‰r s
{
ŠŠ 	
return
‹‹ 

DACInserta
‹‹ 
.
‹‹ (
CargueMasivoQuienLlamoPqrs
‹‹ 8
(
‹‹8 9
detalle
‹‹9 @
,
‹‹@ A
ref
‹‹B E
MsgRes
‹‹F L
)
‹‹L M
;
‹‹M N
}
ŒŒ 	
public
 
List
 
<
 5
'management_pqrs_consolidadoMasivoResult
 ;
>
; <
PqrsListaMasivos
= M
(
M N
int
N Q
?
Q R
	idUsuario
S \
)
\ ]
{
 	
return
 
DACConsulta
 
.
 
PqrsListaMasivos
 /
(
/ 0
	idUsuario
0 9
)
9 :
;
: ;
}
‘‘ 	
public
““ 
List
““ 
<
““ =
/management_pqrs_consolidadoMasivo_detalleResult
““ C
>
““C D%
PqrsListaMasivosDetalle
““E \
(
““\ ]
int
““] `
?
““` a
idMasivo
““b j
,
““j k
int
““l o
?
““o p
	idUsuario
““q z
)
““z {
{
”” 	
return
•• 
DACConsulta
•• 
.
•• %
PqrsListaMasivosDetalle
•• 6
(
••6 7
idMasivo
••7 ?
,
••? @
	idUsuario
••A J
)
••J K
;
••K L
}
–– 	
public
˜˜ 
List
˜˜ 
<
˜˜ 5
'management_pqrs_sinArchivoInicialResult
˜˜ ;
>
˜˜; <*
listadoPqrsInicialSinArchivo
˜˜= Y
(
˜˜Y Z
int
˜˜Z ]
?
˜˜] ^
	idUsuario
˜˜_ h
)
˜˜h i
{
™™ 	
return
šš 
DACConsulta
šš 
.
šš *
listadoPqrsInicialSinArchivo
šš ;
(
šš; <
	idUsuario
šš< E
)
ššE F
;
ššF G
}
›› 	
public
 
int
 "
insertarDatosCorreos
 '
(
' ($
ecop_pqrs_envioCorreos
( >
obj
? B
)
B C
{
 	
return
ŸŸ 

DACInserta
ŸŸ 
.
ŸŸ "
insertarDatosCorreos
ŸŸ 2
(
ŸŸ2 3
obj
ŸŸ3 6
)
ŸŸ6 7
;
ŸŸ7 8
}
   	
public
¡¡ $
ecop_pqrs_envioCorreos
¡¡ %"
LlamarPqrsCorreoById
¡¡& :
(
¡¡: ;
int
¡¡; >
id
¡¡? A
)
¡¡A B
{
¢¢ 	
return
££ 
DACConsulta
££ 
.
££ "
LlamarPqrsCorreoById
££ 3
(
££3 4
id
££4 6
)
££6 7
;
££7 8
}
¤¤ 	
public
¥¥ 
int
¥¥ -
ActualizarPasaArchivoPqrinicial
¥¥ 2
(
¥¥2 3
	ecop_PQRS
¥¥3 <
obj
¥¥= @
)
¥¥@ A
{
¦¦ 	
return
§§ 
DACActualiza
§§ 
.
§§  -
ActualizarPasaArchivoPqrinicial
§§  ?
(
§§? @
obj
§§@ C
)
§§C D
;
§§D E
}
¨¨ 	
public
ªª 
int
ªª '
CerrarCasoPqrSolucionador
ªª ,
(
ªª, -
	ecop_PQRS
ªª- 6
obj
ªª7 :
)
ªª: ;
{
«« 	
return
¬¬ 
DACActualiza
¬¬ 
.
¬¬  '
CerrarCasoPqrSolucionador
¬¬  9
(
¬¬9 :
obj
¬¬: =
)
¬¬= >
;
¬¬> ?
}
­­ 	
public
¯¯ 
	ecop_PQRS
¯¯ "
buscarNumeroCasoPqrs
¯¯ -
(
¯¯- .
string
¯¯. 4
numero_caso
¯¯5 @
)
¯¯@ A
{
°° 	
return
±± 
DACConsulta
±± 
.
±± "
buscarNumeroCasoPqrs
±± 3
(
±±3 4
numero_caso
±±4 ?
)
±±? @
;
±±@ A
}
²² 	
public
µµ 6
(management_devolverFechaHabil_diasResult
µµ 7!
DevolverDiasHabiles
µµ8 K
(
µµK L
DateTime
µµL T
?
µµT U
fecha
µµV [
,
µµ[ \
int
µµ] `
?
µµ` a
tipoSolicitud
µµb o
)
µµo p
{
¶¶ 	
return
·· 
DACConsulta
·· 
.
·· !
DevolverDiasHabiles
·· 2
(
··2 3
fecha
··3 8
,
··8 9
tipoSolicitud
··: G
)
··G H
;
··H I
}
¸¸ 	
public
ºº /
!management_pqrs_detalleCasoResult
ºº 0&
DetallePqrsReporteCorreo
ºº1 I
(
ººI J
int
ººJ M
?
ººM N
idPqr
ººO T
)
ººT U
{
»» 	
return
¼¼ 
DACConsulta
¼¼ 
.
¼¼ &
DetallePqrsReporteCorreo
¼¼ 7
(
¼¼7 8
idPqr
¼¼8 =
)
¼¼= >
;
¼¼> ?
}
½½ 	
public
¿¿ 
List
¿¿ 
<
¿¿ 7
)management_pqrs_PorcentajeAuditoresResult
¿¿ =
>
¿¿= >*
listadoPQRSAuditorPorcentaje
¿¿? [
(
¿¿[ \
string
¿¿\ b
auditor
¿¿c j
)
¿¿j k
{
ÀÀ 	
return
ÁÁ 
DACConsulta
ÁÁ 
.
ÁÁ *
listadoPQRSAuditorPorcentaje
ÁÁ ;
(
ÁÁ; <
auditor
ÁÁ< C
)
ÁÁC D
;
ÁÁD E
}
ÂÂ 	
public
ÄÄ "
vw_auditores_totales
ÄÄ #
GetAuditorNombre
ÄÄ$ 4
(
ÄÄ4 5
string
ÄÄ5 ;
nombre
ÄÄ< B
)
ÄÄB C
{
ÅÅ 	
return
ÆÆ 
DACConsulta
ÆÆ 
.
ÆÆ 
GetAuditorNombre
ÆÆ /
(
ÆÆ/ 0
nombre
ÆÆ0 6
)
ÆÆ6 7
;
ÆÆ7 8
}
ÇÇ 	
public
ÉÉ '
vw_auditores_totales_pqrs
ÉÉ ("
GetAuditorNombrePqrs
ÉÉ) =
(
ÉÉ= >
string
ÉÉ> D
nombre
ÉÉE K
)
ÉÉK L
{
ÊÊ 	
return
ËË 
DACConsulta
ËË 
.
ËË "
GetAuditorNombrePqrs
ËË 3
(
ËË3 4
nombre
ËË4 :
)
ËË: ;
;
ËË; <
}
ÌÌ 	
public
ÒÒ 
List
ÒÒ 
<
ÒÒ 
Ref_procesos
ÒÒ  
>
ÒÒ  !
GetProcesosGD
ÒÒ" /
(
ÒÒ/ 0
)
ÒÒ0 1
{
ÓÓ 	
return
ÔÔ 
DACComonClass
ÔÔ  
.
ÔÔ  !
GetProcesosGD
ÔÔ! .
(
ÔÔ. /
)
ÔÔ/ 0
;
ÔÔ0 1
}
ÕÕ 	
public
×× 
List
×× 
<
×× )
Ref_gestion_tipo_documental
×× /
>
××/ 0+
ConsultaGestionTipoDocumental
××1 N
(
××N O
Int32
××O T
	idproceso
××U ^
)
××^ _
{
ØØ 	
return
ÙÙ 
DACConsulta
ÙÙ 
.
ÙÙ +
ConsultaGestionTipoDocumental
ÙÙ <
(
ÙÙ< =
	idproceso
ÙÙ= F
)
ÙÙF G
;
ÙÙG H
}
ÚÚ 	
public
İİ #
vw_md_consolidado_fac
İİ $
MD_CosolidadofAC
İİ% 5
(
İİ5 6
String
İİ6 <
numero_factura
İİ= K
)
İİK L
{
ŞŞ 	
return
ßß 
DACConsulta
ßß 
.
ßß 
MD_CosolidadofAC
ßß /
(
ßß/ 0
numero_factura
ßß0 >
)
ßß> ?
;
ßß? @
}
àà 	
public
ââ 
List
ââ 
<
ââ #
vw_md_consolidado_fac
ââ )
>
ââ) *%
MD_CosolidadofACDetalle
ââ+ B
(
ââB C
String
ââC I
numero_factura
ââJ X
)
ââX Y
{
ãã 	
return
ää 
DACConsulta
ää 
.
ää %
MD_CosolidadofACDetalle
ää 6
(
ää6 7
numero_factura
ää7 E
)
ääE F
;
ääF G
}
åå 	
public
çç 
List
çç 
<
çç #
vw_md_consolidado_fac
çç )
>
çç) *
MD_CosolidadofAC2
çç+ <
(
çç< =
String
çç= C
factura
ççD K
)
ççK L
{
èè 	
return
éé 
DACConsulta
éé 
.
éé 
MD_CosolidadofAC2
éé 0
(
éé0 1
factura
éé1 8
)
éé8 9
;
éé9 :
}
êê 	
public
ìì 
List
ìì 
<
ìì )
Ref_gestion_tipo_documental
ìì /
>
ìì/ 0
ConsultaCodigoGD
ìì1 A
(
ììA B)
Ref_gestion_tipo_documental
ììB ]
objBusqueda
ìì^ i
,
ììi j
ref
ììk n!
MessageResponseOBJììo 
MsgResìì‚ ˆ
)ììˆ ‰
{
íí 	
return
îî 
DACConsulta
îî 
.
îî 
ConsultaCodigoGD
îî /
(
îî/ 0
objBusqueda
îî0 ;
,
îî; <
ref
îî= @
MsgRes
îîA G
)
îîG H
;
îîH I
}
ïï 	
public
ññ 
Int32
ññ  
InsertarGestionDoc
ññ '
(
ññ' (+
GestionDocumentalMedicamentos
ññ( E
ObjobjGD
ññF N
,
ññN O
ref
ññP S 
MessageResponseOBJ
ññT f
MsgRes
ññg m
)
ññm n
{
òò 	
return
óó 

DACInserta
óó 
.
óó  
InsertarGestionDoc
óó 0
(
óó0 1
ObjobjGD
óó1 9
,
óó9 :
ref
óó; >
MsgRes
óó? E
)
óóE F
;
óóF G
}
ôô 	
public
öö 
Int32
öö *
InsertarGestionDocMedCalidad
öö 1
(
öö1 2.
 GestionDocumentalMedicamentosCad
öö2 R
ObjobjGD
ööS [
,
öö[ \
ref
öö] ` 
MessageResponseOBJ
ööa s
MsgRes
ööt z
)
ööz {
{
÷÷ 	
return
øø 

DACInserta
øø 
.
øø *
InsertarGestionDocMedCalidad
øø :
(
øø: ;
ObjobjGD
øø; C
,
øøC D
ref
øøE H
MsgRes
øøI O
)
øøO P
;
øøP Q
}
ùù 	
public
ûû 
void
ûû $
InsertarGestionDocPQRS
ûû *
(
ûû* +#
GestionDocumentalPQRS
ûû+ @
Obj
ûûA D
,
ûûD E
ref
ûûF I 
MessageResponseOBJ
ûûJ \
MsgRes
ûû] c
)
ûûc d
{
üü 	

DACInserta
ıı 
.
ıı $
InsertarGestionDocPQRS
ıı -
(
ıı- .
Obj
ıı. 1
,
ıı1 2
ref
ıı3 6
MsgRes
ıı7 =
)
ıı= >
;
ıı> ?
}
şş 	
public
€€ 
void
€€ .
 InsertarGestionDocVisitasCalidad
€€ 4
(
€€4 5-
GestionDocumentalVisitasCalidad
€€5 T
Obj
€€U X
,
€€X Y
ref
€€Z ] 
MessageResponseOBJ
€€^ p
MsgRes
€€q w
)
€€w x
{
 	

DACInserta
‚‚ 
.
‚‚ .
 InsertarGestionDocVisitasCalidad
‚‚ 7
(
‚‚7 8
Obj
‚‚8 ;
,
‚‚; <
ref
‚‚= @
MsgRes
‚‚A G
)
‚‚G H
;
‚‚H I
}
ƒƒ 	
public
…… 
List
…… 
<
…… *
vw_g_documental_medicamentos
…… 0
>
……0 1
ConsultaFactura
……2 A
(
……A B
String
……B H
FacMedicamentos
……I X
)
……X Y
{
†† 	
return
‡‡ 
DACConsulta
‡‡ 
.
‡‡ 
ConsultaFactura
‡‡ .
(
‡‡. /
FacMedicamentos
‡‡/ >
)
‡‡> ?
;
‡‡? @
}
ˆˆ 	
public
ŠŠ 
List
ŠŠ 
<
ŠŠ *
vw_g_documental_medicamentos
ŠŠ 0
>
ŠŠ0 1
ConsultaDocumento
ŠŠ2 C
(
ŠŠC D
Decimal
ŠŠD K
DocMedicamentos
ŠŠL [
)
ŠŠ[ \
{
‹‹ 	
return
ŒŒ 
DACConsulta
ŒŒ 
.
ŒŒ 
ConsultaDocumento
ŒŒ 0
(
ŒŒ0 1
DocMedicamentos
ŒŒ1 @
)
ŒŒ@ A
;
ŒŒA B
}
 	
public
 
List
 
<
  
vw_fac_consolidado
 &
>
& '
ConsultaFactura2
( 8
(
8 9
String
9 ?
FacMedicamentos
@ O
)
O P
{
 	
return
‘‘ 
DACConsulta
‘‘ 
.
‘‘ 
ConsultaFactura2
‘‘ /
(
‘‘/ 0
FacMedicamentos
‘‘0 ?
)
‘‘? @
;
‘‘@ A
}
’’ 	
public
”” 
List
”” 
<
””  
vw_fac_consolidado
”” &
>
””& ' 
ConsultaDocumento2
””( :
(
””: ;
String
””; A
DocMedicamentos
””B Q
)
””Q R
{
•• 	
return
–– 
DACConsulta
–– 
.
––  
ConsultaDocumento2
–– 1
(
––1 2
DocMedicamentos
––2 A
)
––A B
;
––B C
}
—— 	
public
™™ *
vw_g_documental_medicamentos
™™ +)
ConsultaIdGestionDocumental
™™, G
(
™™G H
Int32
™™H M#
id_gestion_documental
™™N c
,
™™c d
ref
™™e h 
MessageResponseOBJ
™™i {
MsgRes™™| ‚
)™™‚ ƒ
{
šš 	
return
›› 
DACConsulta
›› 
.
›› )
ConsultaIdGestionDocumental
›› :
(
››: ;#
id_gestion_documental
››; P
,
››P Q
ref
››R U
MsgRes
››V \
)
››\ ]
;
››] ^
}
œœ 	
public
 
List
 
<
 *
vw_g_documental_medicamentos
 0
>
0 1*
ConsultaIdGestionDocumental2
2 N
(
N O
Int32
O T#
id_gestion_documental
U j
,
j k
ref
l o!
MessageResponseOBJp ‚
MsgResƒ ‰
)‰ Š
{
ŸŸ 	
return
   
DACConsulta
   
.
   *
ConsultaIdGestionDocumental2
   ;
(
  ; <#
id_gestion_documental
  < Q
,
  Q R
ref
  S V
MsgRes
  W ]
)
  ] ^
;
  ^ _
}
¡¡ 	
public
££ 
List
££ 
<
££ *
vw_g_documental_medicamentos
££ 0
>
££0 10
"ConsultaIdGestionDocumentalFormula
££2 T
(
££T U
String
££U [
formula
££\ c
,
££c d
ref
££e h 
MessageResponseOBJ
££i {
MsgRes££| ‚
)££‚ ƒ
{
¤¤ 	
return
¥¥ 
DACConsulta
¥¥ 
.
¥¥ 0
"ConsultaIdGestionDocumentalFormula
¥¥ A
(
¥¥A B
formula
¥¥B I
,
¥¥I J
ref
¥¥K N
MsgRes
¥¥O U
)
¥¥U V
;
¥¥V W
}
¦¦ 	
public
§§ 
List
§§ 
<
§§ 0
"vw_gestion_documental_med_cad_dtll
§§ 6
>
§§6 72
$ConsultaIdGestionDocumentalMDCalidad
§§8 \
(
§§\ ]
Int32
§§] b)
id_indicadores_medicamentos
§§c ~
,
§§~ 
ref§§€ ƒ"
MessageResponseOBJ§§„ –
MsgRes§§— 
)§§ 
{
¨¨ 	
return
©© 
DACConsulta
©© 
.
©© 2
$ConsultaIdGestionDocumentalMDCalidad
©© C
(
©©C D)
id_indicadores_medicamentos
©©D _
,
©©_ `
ref
©©a d
MsgRes
©©e k
)
©©k l
;
©©l m
}
ªª 	
public
¬¬ 
	ecop_PQRS
¬¬ #
ConsultaPQRSbyNumCaso
¬¬ .
(
¬¬. /
string
¬¬/ 5
numcaso
¬¬6 =
)
¬¬= >
{
­­ 	
return
®® 
DACConsulta
®® 
.
®® #
ConsultaPQRSbyNumCaso
®® 4
(
®®4 5
numcaso
®®5 <
)
®®< =
;
®®= >
}
¯¯ 	
public
±± #
GestionDocumentalPQRS
±± $$
ConsultaGestorPQRSbyId
±±% ;
(
±±; <
Int32
±±< A
Id
±±B D
)
±±D E
{
²² 	
return
³³ 
DACConsulta
³³ 
.
³³ $
ConsultaGestorPQRSbyId
³³ 5
(
³³5 6
Id
³³6 8
)
³³8 9
;
³³9 :
}
´´ 	
public
·· 
List
·· 
<
·· #
GestionDocumentalPQRS
·· )
>
··) *2
$ConsultanumcasoGestionDocumentalPQRS
··+ O
(
··O P
string
··P V
numcaso
··W ^
)
··^ _
{
¸¸ 	
return
¹¹ 
DACConsulta
¹¹ 
.
¹¹ 2
$ConsultanumcasoGestionDocumentalPQRS
¹¹ C
(
¹¹C D
numcaso
¹¹D K
)
¹¹K L
;
¹¹L M
}
ºº 	
public
¼¼ 
void
¼¼ '
EliminarDocumento_med_cal
¼¼ -
(
¼¼- .
Int32
¼¼. 3
id
¼¼4 6
,
¼¼6 7
ref
¼¼8 ; 
MessageResponseOBJ
¼¼< N
MsgRes
¼¼O U
)
¼¼U V
{
½½ 	

DACElimina
¾¾ 
.
¾¾ '
EliminarDocumento_med_cal
¾¾ 0
(
¾¾0 1
id
¾¾1 3
,
¾¾3 4
ref
¾¾5 8
MsgRes
¾¾9 ?
)
¾¾? @
;
¾¾@ A
}
¿¿ 	
public
ÁÁ 
void
ÁÁ #
EliminarDocumento_med
ÁÁ )
(
ÁÁ) *
Int32
ÁÁ* /
id
ÁÁ0 2
,
ÁÁ2 3
ref
ÁÁ4 7 
MessageResponseOBJ
ÁÁ8 J
MsgRes
ÁÁK Q
)
ÁÁQ R
{
ÂÂ 	

DACElimina
ÃÃ 
.
ÃÃ #
EliminarDocumento_med
ÃÃ ,
(
ÃÃ, -
id
ÃÃ- /
,
ÃÃ/ 0
ref
ÃÃ1 4
MsgRes
ÃÃ5 ;
)
ÃÃ; <
;
ÃÃ< =
}
ÄÄ 	
public
ÆÆ 
bool
ÆÆ 
EliminarDocPQRS
ÆÆ #
(
ÆÆ# $
Int32
ÆÆ$ )
id
ÆÆ* ,
,
ÆÆ, -
ref
ÆÆ. 1 
MessageResponseOBJ
ÆÆ2 D
MsgRes
ÆÆE K
)
ÆÆK L
{
ÇÇ 	
return
ÈÈ 

DACElimina
ÈÈ 
.
ÈÈ 
EliminarDocPQRS
ÈÈ -
(
ÈÈ- .
id
ÈÈ. 0
,
ÈÈ0 1
ref
ÈÈ2 5
MsgRes
ÈÈ6 <
)
ÈÈ< =
;
ÈÈ= >
}
ÉÉ 	
public
ËË 
void
ËË "
InsertarLogActividad
ËË (
(
ËË( )#
Log_GestionDocumental
ËË) >
Log
ËË? B
)
ËËB C
{
ÌÌ 	

DACInserta
ÍÍ 
.
ÍÍ "
InsertarLogActividad
ÍÍ +
(
ÍÍ+ ,
Log
ÍÍ, /
)
ÍÍ/ 0
;
ÍÍ0 1
}
ÎÎ 	
public
ĞĞ 
List
ĞĞ 
<
ĞĞ +
GestionDocumentalMedicamentos
ĞĞ 1
>
ĞĞ1 2
TraerPdf
ĞĞ3 ;
(
ĞĞ; <
)
ĞĞ< =
{
ÑÑ 	
return
ÒÒ 
DACComonClass
ÒÒ  
.
ÒÒ  !
TraerPdf
ÒÒ! )
(
ÒÒ) *
)
ÒÒ* +
;
ÒÒ+ ,
}
ÓÓ 	
public
ÕÕ 
String
ÕÕ #
ActualizarRutaByteMed
ÕÕ +
(
ÕÕ+ ,+
GestionDocumentalMedicamentos
ÕÕ, I
obj
ÕÕJ M
,
ÕÕM N
ref
ÕÕO R 
MessageResponseOBJ
ÕÕS e
MsgRes
ÕÕf l
)
ÕÕl m
{
ÖÖ 	
return
×× 
DACActualiza
×× 
.
××  #
ActualizarRutaByteMed
××  5
(
××5 6
obj
××6 9
,
××9 :
ref
××; >
MsgRes
××? E
)
××E F
;
××F G
}
ØØ 	
public
ÚÚ 
String
ÚÚ (
ActualizarRutasDocsVisitas
ÚÚ 0
(
ÚÚ0 1)
cronograma_visita_documento
ÚÚ1 L
obj
ÚÚM P
,
ÚÚP Q
ref
ÚÚR U 
MessageResponseOBJ
ÚÚV h
MsgRes
ÚÚi o
)
ÚÚo p
{
ÛÛ 	
return
ÜÜ 
DACActualiza
ÜÜ 
.
ÜÜ  (
ActualizarRutasDocsVisitas
ÜÜ  :
(
ÜÜ: ;
obj
ÜÜ; >
,
ÜÜ> ?
ref
ÜÜ@ C
MsgRes
ÜÜD J
)
ÜÜJ K
;
ÜÜK L
}
İİ 	
public
ßß 
void
ßß 6
(ActualizarRutaDocumentoVisitasCronograma
ßß <
(
ßß< =
string
ßß= C
ruta
ßßD H
,
ßßH I
int
ßßJ M
?
ßßM N
idVisita
ßßO W
)
ßßW X
{
àà 	
DACActualiza
áá 
.
áá 6
(ActualizarRutaDocumentoVisitasCronograma
áá A
(
ááA B
ruta
ááB F
,
ááF G
idVisita
ááH P
)
ááP Q
;
ááQ R
}
ââ 	
public
ää 
String
ää $
ActualizarRutaBytePQRS
ää ,
(
ää, -$
GestionDocumentalPQRS2
ää- C
obj
ääD G
,
ääG H
ref
ääI L 
MessageResponseOBJ
ääM _
MsgRes
ää` f
)
ääf g
{
åå 	
return
ææ 
DACActualiza
ææ 
.
ææ  $
ActualizarRutaBytePQRS
ææ  6
(
ææ6 7
obj
ææ7 :
,
ææ: ;
ref
ææ< ?
MsgRes
ææ@ F
)
ææF G
;
ææG H
}
çç 	
public
èè 
int
èè '
insertarConteoAnalistaPQR
èè ,
(
èè, -
int
èè- 0

idAnalista
èè1 ;
,
èè; <
int
èè= @
	idUsuario
èèA J
)
èèJ K
{
éé 	
return
êê 

DACInserta
êê 
.
êê '
insertarConteoAnalistaPQR
êê 7
(
êê7 8

idAnalista
êê8 B
,
êêB C
	idUsuario
êêD M
)
êêM N
;
êêN O
}
ëë 	
public
ìì 
String
ìì *
ActualizarRutaByteMedCalidad
ìì 2
(
ìì2 3.
 GestionDocumentalMedicamentosCad
ìì3 S
obj
ììT W
,
ììW X
ref
ììY \ 
MessageResponseOBJ
ìì] o
MsgRes
ììp v
)
ììv w
{
íí 	
return
îî 
DACActualiza
îî 
.
îî  *
ActualizarRutaByteMedCalidad
îî  <
(
îî< =
obj
îî= @
,
îî@ A
ref
îîB E
MsgRes
îîF L
)
îîL M
;
îîM N
}
ïï 	
public
ññ 
List
ññ 
<
ññ +
GestionDocumentalMedicamentos
ññ 1
>
ññ1 2&
ConsultaGestionMedCargue
ññ3 K
(
ññK L
)
ññL M
{
òò 	
return
óó 
DACConsulta
óó 
.
óó &
ConsultaGestionMedCargue
óó 7
(
óó7 8
)
óó8 9
;
óó9 :
}
ôô 	
public
õõ 
List
õõ 
<
õõ 1
#vw_g_documental_medicamentos_masivo
õõ 7
>
õõ7 8%
GestionDocumentalmasivo
õõ9 P
(
õõP Q
)
õõQ R
{
öö 	
return
÷÷ 
DACConsulta
÷÷ 
.
÷÷ %
GestionDocumentalmasivo
÷÷ 6
(
÷÷6 7
)
÷÷7 8
;
÷÷8 9
}
øø 	
public
ùù 
List
ùù 
<
ùù *
management_masivo_pqrsResult
ùù 0
>
ùù0 1&
GestionDocumentalmasivo2
ùù2 J
(
ùùJ K
)
ùùK L
{
úú 	
return
ûû 
DACConsulta
ûû 
.
ûû &
GestionDocumentalmasivo2
ûû 7
(
ûû7 8
)
ûû8 9
;
ûû9 :
}
üü 	
public
ıı 
List
ıı 
<
ıı !
md_Ref_com_dirigido
ıı '
>
ıı' (
GetDirigido
ıı) 4
(
ıı4 5
)
ıı5 6
{
şş 	
return
ÿÿ 
DACComonClass
ÿÿ  
.
ÿÿ  !
GetDirigido
ÿÿ! ,
(
ÿÿ, -
)
ÿÿ- .
;
ÿÿ. /
}
€€ 	
public
‚‚ 
List
‚‚ 
<
‚‚ 
md_Ref_com_tipo
‚‚ #
>
‚‚# $
	GetMdTipo
‚‚% .
(
‚‚. /
)
‚‚/ 0
{
ƒƒ 	
return
„„ 
DACComonClass
„„  
.
„„  !
	GetMdTipo
„„! *
(
„„* +
)
„„+ ,
;
„„, -
}
…… 	
public
‡‡ 
List
‡‡ 
<
‡‡  
md_ref_tipo_visita
‡‡ &
>
‡‡& '
GetMdTipoVisita
‡‡( 7
(
‡‡7 8
)
‡‡8 9
{
ˆˆ 	
return
‰‰ 
DACComonClass
‰‰  
.
‰‰  !
GetMdTipoVisita
‰‰! 0
(
‰‰0 1
)
‰‰1 2
;
‰‰2 3
}
ŠŠ 	
public
ŒŒ 
void
ŒŒ $
InsertarComunicaciones
ŒŒ *
(
ŒŒ* +
md_comunicaciones
ŒŒ+ <
OBJ
ŒŒ= @
,
ŒŒ@ A
ref
ŒŒB E 
MessageResponseOBJ
ŒŒF X
MsgRes
ŒŒY _
)
ŒŒ_ `
{
 	

DACInserta
 
.
 $
InsertarComunicaciones
 -
(
- .
OBJ
. 1
,
1 2
ref
3 6
MsgRes
7 =
)
= >
;
> ?
}
 	
public
‘‘ 
void
‘‘ "
InsertarCronoVisitas
‘‘ (
(
‘‘( )
md_crono_visita
‘‘) 8
OBJ
‘‘9 <
,
‘‘< =
ref
‘‘> A 
MessageResponseOBJ
‘‘B T
MsgRes
‘‘U [
)
‘‘[ \
{
’’ 	

DACInserta
““ 
.
““ "
InsertarCronoVisitas
““ +
(
““+ ,
OBJ
““, /
,
““/ 0
ref
““1 4
MsgRes
““5 ;
)
““; <
;
““< =
}
”” 	
public
–– 
List
–– 
<
–– 2
$ManagmentRefPuntosDispersacionResult
–– 8
>
––8 9
ConsultaListaPD
––: I
(
––I J
int
––J M
opc
––N Q
,
––Q R
ref
––S V 
MessageResponseOBJ
––W i
MsgRes
––j p
)
––p q
{
—— 	
return
˜˜ 
DACConsulta
˜˜ 
.
˜˜ 
ConsultaListaPD
˜˜ .
(
˜˜. /
opc
˜˜/ 2
,
˜˜2 3
ref
˜˜4 7
MsgRes
˜˜8 >
)
˜˜> ?
;
˜˜? @
}
™™ 	
public
›› 0
"vw_gestion_documental_med_cad_dtll
›› 10
"ConsultaIdGestionDocumentalMDCalId
››2 T
(
››T U
Int32
››U Z6
'id_gestion_documental__medicamentos_cad››[ ‚
,››‚ ƒ
ref››„ ‡"
MessageResponseOBJ››ˆ š
MsgRes››› ¡
)››¡ ¢
{
œœ 	
return
 
DACConsulta
 
.
 0
"ConsultaIdGestionDocumentalMDCalId
 A
(
A B5
'id_gestion_documental__medicamentos_cad
B i
,
i j
ref
k n
MsgRes
o u
)
u v
;
v w
}
 	
public
¤¤ 
List
¤¤ 
<
¤¤ ,
ManagmentFacMedicamentosResult
¤¤ 2
>
¤¤2 3#
CuentaFacMedicamentos
¤¤4 I
(
¤¤I J
String
¤¤J P
factura
¤¤Q X
,
¤¤X Y
String
¤¤Z `
formula
¤¤a h
,
¤¤h i
Int32
¤¤j o
OPC
¤¤p s
,
¤¤s t
ref
¤¤u x!
MessageResponseOBJ¤¤y ‹
MsgRes¤¤Œ ’
)¤¤’ “
{
¥¥ 	
return
¦¦ 
DACConsulta
¦¦ 
.
¦¦ #
CuentaFacMedicamentos
¦¦ 4
(
¦¦4 5
factura
¦¦5 <
,
¦¦< =
formula
¦¦> E
,
¦¦E F
OPC
¦¦G J
,
¦¦J K
ref
¦¦L O
MsgRes
¦¦P V
)
¦¦V W
;
¦¦W X
}
§§ 	
public
©© 
List
©© 
<
©© /
!Managment_md_tablerocontrolResult
©© 5
>
©©5 6%
CuentaFacTableroControl
©©7 N
(
©©N O
DateTime
©©O W
fecha_inicial
©©X e
,
©©e f
DateTime
©©g o
fecha_final
©©p {
,
©©{ |
ref©©} €"
MessageResponseOBJ©© “
MsgRes©©” š
)©©š ›
{
ªª 	
return
«« 
DACConsulta
«« 
.
«« %
CuentaFacTableroControl
«« 6
(
««6 7
fecha_inicial
««7 D
,
««D E
fecha_final
««F Q
,
««Q R
ref
««S V
MsgRes
««W ]
)
««] ^
;
««^ _
}
¬¬ 	
public
®® 
List
®® 
<
®® 7
)Managment_md_tablero_ConciliacionesResult
®® =
>
®®= >3
%CuentaFacTableroControlConciliaciones
®®? d
(
®®d e
ref
®®e h 
MessageResponseOBJ
®®i {
MsgRes®®| ‚
)®®‚ ƒ
{
¯¯ 	
return
°° 
DACConsulta
°° 
.
°° 3
%CuentaFacTableroControlConciliaciones
°° D
(
°°D E
ref
°°E H
MsgRes
°°I O
)
°°O P
;
°°P Q
}
±± 	
public
³³ 
List
³³ 
<
³³ ?
1Managment_md_tablero_Conciliaciones_detalleResult
³³ E
>
³³E F7
)CuentaFacTableroControlConciliacionesdtll
³³G p
(
³³p q
ref
³³q t!
MessageResponseOBJ³³u ‡
MsgRes³³ˆ 
)³³ 
{
´´ 	
return
µµ 
DACConsulta
µµ 
.
µµ 7
)CuentaFacTableroControlConciliacionesdtll
µµ H
(
µµH I
ref
µµI L
MsgRes
µµM S
)
µµS T
;
µµT U
}
¶¶ 	
public
¸¸ 
List
¸¸ 
<
¸¸ 3
%ManagmentFacMedicamentosDetalleResult
¸¸ 9
>
¸¸9 :*
CuentaFacMedicamentosDetalle
¸¸; W
(
¸¸W X
String
¸¸X ^
factura
¸¸_ f
,
¸¸f g
String
¸¸h n
formula
¸¸o v
,
¸¸v w
Int32
¸¸x }
OPC¸¸~ 
,¸¸ ‚
ref¸¸ƒ †"
MessageResponseOBJ¸¸‡ ™
MsgRes¸¸š  
)¸¸  ¡
{
¹¹ 	
return
ºº 
DACConsulta
ºº 
.
ºº *
CuentaFacMedicamentosDetalle
ºº ;
(
ºº; <
factura
ºº< C
,
ººC D
formula
ººE L
,
ººL M
OPC
ººN Q
,
ººQ R
ref
ººS V
MsgRes
ººW ]
)
ºº] ^
;
ºº^ _
}
»» 	
public
½½ 
List
½½ 
<
½½ (
md_Ref_resultado_auditoria
½½ .
>
½½. /
GetResultadoAUD
½½0 ?
(
½½? @
)
½½@ A
{
¾¾ 	
return
¿¿ 
DACComonClass
¿¿  
.
¿¿  !
GetResultadoAUD
¿¿! 0
(
¿¿0 1
)
¿¿1 2
;
¿¿2 3
}
ÀÀ 	
public
ÂÂ 
Int32
ÂÂ 
InsertarGlosaMD
ÂÂ $
(
ÂÂ$ %
md_glosa
ÂÂ% -
OBJGlosa
ÂÂ. 6
,
ÂÂ6 7
ref
ÂÂ8 ; 
MessageResponseOBJ
ÂÂ< N
MsgRes
ÂÂO U
)
ÂÂU V
{
ÃÃ 	
return
ÄÄ 

DACInserta
ÄÄ 
.
ÄÄ 
InsertarGlosaMD
ÄÄ -
(
ÄÄ- .
OBJGlosa
ÄÄ. 6
,
ÄÄ6 7
ref
ÄÄ8 ;
MsgRes
ÄÄ< B
)
ÄÄB C
;
ÄÄC D
}
ÅÅ 	
public
ÇÇ 
Int32
ÇÇ $
InsertarGlosaDetalleMD
ÇÇ +
(
ÇÇ+ ,
md_glosa_detalle
ÇÇ, <
OBJGlosaDetalle
ÇÇ= L
,
ÇÇL M
ref
ÇÇN Q 
MessageResponseOBJ
ÇÇR d
MsgRes
ÇÇe k
)
ÇÇk l
{
ÈÈ 	
return
ÉÉ 

DACInserta
ÉÉ 
.
ÉÉ $
InsertarGlosaDetalleMD
ÉÉ 4
(
ÉÉ4 5
OBJGlosaDetalle
ÉÉ5 D
,
ÉÉD E
ref
ÉÉF I
MsgRes
ÉÉJ P
)
ÉÉP Q
;
ÉÉQ R
}
ÊÊ 	
public
ÌÌ 
List
ÌÌ 
<
ÌÌ #
vw_glosa_medicamentos
ÌÌ )
>
ÌÌ) *
ConsultaGlosa
ÌÌ+ 8
(
ÌÌ8 9
String
ÌÌ9 ?
formula
ÌÌ@ G
)
ÌÌG H
{
ÍÍ 	
return
ÎÎ 
DACConsulta
ÎÎ 
.
ÎÎ 
ConsultaGlosa
ÎÎ ,
(
ÎÎ, -
formula
ÎÎ- 4
)
ÎÎ4 5
;
ÎÎ5 6
}
ÏÏ 	
public
ÑÑ 
void
ÑÑ 
EliminarGlosa
ÑÑ !
(
ÑÑ! "
Int32
ÑÑ" '
id
ÑÑ( *
,
ÑÑ* +
ref
ÑÑ, / 
MessageResponseOBJ
ÑÑ0 B
MsgRes
ÑÑC I
)
ÑÑI J
{
ÒÒ 	

DACElimina
ÓÓ 
.
ÓÓ 
EliminarGlosa
ÓÓ $
(
ÓÓ$ %
id
ÓÓ% '
,
ÓÓ' (
ref
ÓÓ) ,
MsgRes
ÓÓ- 3
)
ÓÓ3 4
;
ÓÓ4 5
}
ÔÔ 	
public
ÖÖ 
Int32
ÖÖ 
InsertarIndicador
ÖÖ &
(
ÖÖ& '
md_indicadores
ÖÖ' 5
OBJIndicadores
ÖÖ6 D
,
ÖÖD E
ref
ÖÖF I 
MessageResponseOBJ
ÖÖJ \
MsgRes
ÖÖ] c
)
ÖÖc d
{
×× 	
return
ØØ 

DACInserta
ØØ 
.
ØØ 
InsertarIndicador
ØØ /
(
ØØ/ 0
OBJIndicadores
ØØ0 >
,
ØØ> ?
ref
ØØ@ C
MsgRes
ØØD J
)
ØØJ K
;
ØØK L
}
ÙÙ 	
public
ÛÛ 
List
ÛÛ 
<
ÛÛ $
md_indicadores_detalle
ÛÛ *
>
ÛÛ* +#
GetIndicadoresDetalle
ÛÛ, A
(
ÛÛA B
Int32
ÛÛB G)
id_indicadores_medicamentos
ÛÛH c
)
ÛÛc d
{
ÜÜ 	
return
İİ 
DACConsulta
İİ 
.
İİ #
GetIndicadoresDetalle
İİ 4
(
İİ4 5)
id_indicadores_medicamentos
İİ5 P
)
İİP Q
;
İİQ R
}
ŞŞ 	
public
àà 
Int32
àà &
InsertarIndicadorDetalle
àà -
(
àà- .$
md_indicadores_detalle
àà. D

OBJDetalle
ààE O
,
ààO P
ref
ààQ T 
MessageResponseOBJ
ààU g
MsgRes
ààh n
)
ààn o
{
áá 	
return
ââ 

DACInserta
ââ 
.
ââ &
InsertarIndicadorDetalle
ââ 6
(
ââ6 7

OBJDetalle
ââ7 A
,
ââA B
ref
ââC F
MsgRes
ââG M
)
ââM N
;
ââN O
}
ãã 	
public
åå 
void
åå /
!ActualizarIndicadoresMedicamentos
åå 5
(
åå5 6$
md_indicadores_detalle
åå6 L
OBJIndicadoresMD
ååM ]
,
åå] ^
ref
åå_ b 
MessageResponseOBJ
ååc u
MsgRes
ååv |
)
åå| }
{
ææ 	
DACActualiza
çç 
.
çç /
!ActualizarIndicadoresMedicamentos
çç :
(
çç: ;
OBJIndicadoresMD
çç; K
,
ççK L
ref
ççM P
MsgRes
ççQ W
)
ççW X
;
ççX Y
}
èè 	
public
êê 
List
êê 
<
êê $
md_indicadores_detalle
êê *
>
êê* +%
GetIndicadoresDetalleID
êê, C
(
êêC D
Int32
êêD I!
id_md_ref_indicador
êêJ ]
,
êê] ^
Int32
êê_ d*
id_indicadores_medicamentosêêe €
)êê€ 
{
ëë 	
return
ìì 
DACConsulta
ìì 
.
ìì %
GetIndicadoresDetalleID
ìì 6
(
ìì6 7!
id_md_ref_indicador
ìì7 J
,
ììJ K)
id_indicadores_medicamentos
ììL g
)
ììg h
;
ììh i
}
íí 	
public
ïï 
List
ïï 
<
ïï .
 vw_indicador_detalle_sin_cumplir
ïï 4
>
ïï4 5&
GetIndicadoresSinCumplir
ïï6 N
(
ïïN O
Int32
ïïO T)
id_indicadores_medicamentos
ïïU p
)
ïïp q
{
ğğ 	
return
ññ 
DACConsulta
ññ 
.
ññ &
GetIndicadoresSinCumplir
ññ 7
(
ññ7 8)
id_indicadores_medicamentos
ññ8 S
)
ññS T
;
ññT U
}
òò 	
public
ôô 
List
ôô 
<
ôô .
 Managment_md_Ref_indicadorResult
ôô 4
>
ôô4 5#
DetalleRefIndicadores
ôô6 K
(
ôôK L
Int32
ôôL Q)
id_indicadores_medicamentos
ôôR m
,
ôôm n
Int32
ôôo t
opc
ôôu x
)
ôôx y
{
õõ 	
return
öö 
DACConsulta
öö 
.
öö #
DetalleRefIndicadores
öö 4
(
öö4 5)
id_indicadores_medicamentos
öö5 P
,
ööP Q
opc
ööR U
)
ööU V
;
ööV W
}
÷÷ 	
public
ùù 
List
ùù 
<
ùù -
ManagmentReporIndicadorMDResult
ùù 3
>
ùù3 4 
ReporteIndicadores
ùù5 G
(
ùùG H
Int32
ùùH M)
id_indicadores_medicamentos
ùùN i
)
ùùi j
{
úú 	
return
ûû 
DACConsulta
ûû 
.
ûû  
ReporteIndicadores
ûû 1
(
ûû1 2)
id_indicadores_medicamentos
ûû2 M
)
ûûM N
;
ûûN O
}
üü 	
public
şş 
void
şş %
ActualizarIndicadoresMD
şş +
(
şş+ ,
md_indicadores
şş, :
OBJIndicadoresMD
şş; K
,
şşK L
ref
şşM P 
MessageResponseOBJ
şşQ c
MsgRes
şşd j
)
şşj k
{
ÿÿ 	
DACActualiza
€€ 
.
€€ %
ActualizarIndicadoresMD
€€ 0
(
€€0 1
OBJIndicadoresMD
€€1 A
,
€€A B
ref
€€C F
MsgRes
€€G M
)
€€M N
;
€€N O
}
 	
public
ƒƒ !
vw_total_md_detalle
ƒƒ " 
Total_DetalleIndMD
ƒƒ# 5
(
ƒƒ5 6
Int32
ƒƒ6 ;)
id_indicadores_medicamentos
ƒƒ< W
)
ƒƒW X
{
„„ 	
return
…… 
DACConsulta
…… 
.
……  
Total_DetalleIndMD
…… 1
(
……1 2)
id_indicadores_medicamentos
……2 M
)
……M N
;
……N O
}
‡‡ 	
public
‰‰ 
List
‰‰ 
<
‰‰ !
vw_table_gestion_MD
‰‰ '
>
‰‰' (
ConsultaGestionMd
‰‰) :
(
‰‰: ;
)
‰‰; <
{
ŠŠ 	
return
‹‹ 
DACConsulta
‹‹ 
.
‹‹ 
ConsultaGestionMd
‹‹ 0
(
‹‹0 1
)
‹‹1 2
;
‹‹2 3
}
ŒŒ 	
public
 
List
 
<
 "
md_Ref_tipo_hallazgo
 (
>
( )
TipoHallazgo
* 6
(
6 7
)
7 8
{
 	
return
 
DACConsulta
 
.
 
TipoHallazgo
 +
(
+ ,
)
, -
;
- .
}
‘‘ 	
public
““ 
Int32
““ &
InsertarIndicadorGestion
““ -
(
““- .$
md_indicadores_gestion
““. D

OBJGestion
““E O
,
““O P
ref
““Q T 
MessageResponseOBJ
““U g
MsgRes
““h n
)
““n o
{
”” 	
return
•• 

DACInserta
•• 
.
•• &
InsertarIndicadorGestion
•• 6
(
••6 7

OBJGestion
••7 A
,
••A B
ref
••C F
MsgRes
••G M
)
••M N
;
••N O
}
–– 	
public
˜˜ 
void
˜˜ +
ActualizarIndicadoresMDEstado
˜˜ 1
(
˜˜1 2
md_indicadores
˜˜2 @
OBJIndicadoresMD
˜˜A Q
,
˜˜Q R
ref
˜˜S V 
MessageResponseOBJ
˜˜W i
MsgRes
˜˜j p
)
˜˜p q
{
™™ 	
DACActualiza
šš 
.
šš +
ActualizarIndicadoresMDEstado
šš 6
(
šš6 7
OBJIndicadoresMD
šš7 G
,
ššG H
ref
ššI L
MsgRes
ššM S
)
ššS T
;
ššT U
}
›› 	
public
 
List
 
<
 
md_Ref_consultas
 $
>
$ %
GetRefConsulta
& 4
(
4 5
)
5 6
{
 	
return
ŸŸ 
DACComonClass
ŸŸ  
.
ŸŸ  !
GetRefConsulta
ŸŸ! /
(
ŸŸ/ 0
)
ŸŸ0 1
;
ŸŸ1 2
}
   	
public
¢¢ 
List
¢¢ 
<
¢¢ *
Managment_md_ConsultasResult
¢¢ 0
>
¢¢0 1)
CuentaConsultasMedicamentos
¢¢2 M
(
¢¢M N
Int32
¢¢N S
opc
¢¢T W
,
¢¢W X
DateTime
¢¢Y a
fecha_inicial
¢¢b o
,
¢¢o p
DateTime
¢¢q y
fecha_final¢¢z …
,¢¢… †
ref¢¢‡ Š"
MessageResponseOBJ¢¢‹ 
MsgRes¢¢ ¤
)¢¢¤ ¥
{
££ 	
return
¤¤ 
DACConsulta
¤¤ 
.
¤¤ )
CuentaConsultasMedicamentos
¤¤ :
(
¤¤: ;
opc
¤¤; >
,
¤¤> ?
fecha_inicial
¤¤@ M
,
¤¤M N
fecha_final
¤¤O Z
,
¤¤Z [
ref
¤¤\ _
MsgRes
¤¤` f
)
¤¤f g
;
¤¤g h
}
¥¥ 	
public
§§ 
List
§§ 
<
§§ 
md_Ref_proveedor
§§ $
>
§§$ %!
GetMD_Ref_proveedor
§§& 9
(
§§9 :
)
§§: ;
{
¨¨ 	
return
©© 
DACComonClass
©©  
.
©©  !!
GetMD_Ref_proveedor
©©! 4
(
©©4 5
)
©©5 6
;
©©6 7
}
ªª 	
public
¬¬ 
IEnumerable
¬¬ 
<
¬¬ )
vw_md_Ref_indicador_datalle
¬¬ 6
>
¬¬6 7%
GetVwIndicadoresDetalle
¬¬8 O
(
¬¬O P
Int32
¬¬P U)
id_indicadores_medicamentos
¬¬V q
)
¬¬q r
{
­­ 	
return
®® 
DACConsulta
®® 
.
®® %
GetVwIndicadoresDetalle
®® 6
(
®®6 7)
id_indicadores_medicamentos
®®7 R
)
®®R S
;
®®S T
}
¯¯ 	
public
±± 
List
±± 
<
±± (
md_ref_puntos_dispensacion
±± .
>
±±. /
PuntosDispercion
±±0 @
(
±±@ A
)
±±A B
{
²² 	
return
³³ 
DACConsulta
³³ 
.
³³ 
PuntosDispercion
³³ /
(
³³/ 0
)
³³0 1
;
³³1 2
}
´´ 	
public
¶¶ 
List
¶¶ 
<
¶¶ )
vw_indicadores_medicamentos
¶¶ /
>
¶¶/ 0"
IndicadoresProvvedor
¶¶1 E
(
¶¶E F
String
¶¶F L

Proveeedor
¶¶M W
)
¶¶W X
{
·· 	
return
¸¸ 
DACConsulta
¸¸ 
.
¸¸ "
IndicadoresProvvedor
¸¸ 3
(
¸¸3 4

Proveeedor
¸¸4 >
)
¸¸> ?
;
¸¸? @
}
¹¹ 	
public
»» 
List
»» 
<
»» +
vw_obligaciones_contractuales
»» 1
>
»»1 2#
ObligacionesProveedor
»»3 H
(
»»H I
String
»»I O
	Proveedor
»»P Y
)
»»Y Z
{
¼¼ 	
return
½½ 
DACConsulta
½½ 
.
½½ #
ObligacionesProveedor
½½ 4
(
½½4 5
	Proveedor
½½5 >
)
½½> ?
;
½½? @
}
¾¾ 	
public
ÀÀ 
Int32
ÀÀ "
InsertarObligaciones
ÀÀ )
(
ÀÀ) *+
md_obligaciones_contractuales
ÀÀ* G*
OBJObligacionesContractuales
ÀÀH d
,
ÀÀd e
ref
ÀÀf i 
MessageResponseOBJ
ÀÀj |
MsgResÀÀ} ƒ
)ÀÀƒ „
{
ÁÁ 	
return
ÂÂ 

DACInserta
ÂÂ 
.
ÂÂ "
InsertarObligaciones
ÂÂ 2
(
ÂÂ2 3*
OBJObligacionesContractuales
ÂÂ3 O
,
ÂÂO P
ref
ÂÂQ T
MsgRes
ÂÂU [
)
ÂÂ[ \
;
ÂÂ\ ]
}
ÃÃ 	
public
ÅÅ 
List
ÅÅ 
<
ÅÅ 1
#Managment_md_Ref_obligacionesResult
ÅÅ 7
>
ÅÅ7 8$
DetalleRefObligaciones
ÅÅ9 O
(
ÅÅO P
Int32
ÅÅP U+
id_obligaciones_contractuales
ÅÅV s
,
ÅÅs t
Int32
ÅÅu z
opc
ÅÅ{ ~
)
ÅÅ~ 
{
ÆÆ 	
return
ÇÇ 
DACConsulta
ÇÇ 
.
ÇÇ $
DetalleRefObligaciones
ÇÇ 5
(
ÇÇ5 6+
id_obligaciones_contractuales
ÇÇ6 S
,
ÇÇS T
opc
ÇÇU X
)
ÇÇX Y
;
ÇÇY Z
}
ÈÈ 	
public
ÊÊ 
Int32
ÊÊ )
InsertarObligacionesDetalle
ÊÊ 0
(
ÊÊ0 13
%md_obligaciones_contractuales_detalle
ÊÊ1 V

OBJDetalle
ÊÊW a
,
ÊÊa b
ref
ÊÊc f 
MessageResponseOBJ
ÊÊg y
MsgResÊÊz €
)ÊÊ€ 
{
ËË 	
return
ÌÌ 

DACInserta
ÌÌ 
.
ÌÌ )
InsertarObligacionesDetalle
ÌÌ 9
(
ÌÌ9 :

OBJDetalle
ÌÌ: D
,
ÌÌD E
ref
ÌÌF I
MsgRes
ÌÌJ P
)
ÌÌP Q
;
ÌÌQ R
}
ÍÍ 	
public
ÏÏ .
 vw_total_md_obligaciones_detalle
ÏÏ /)
Total_DetalleObligacionesMD
ÏÏ0 K
(
ÏÏK L
Int32
ÏÏL Q+
id_obligaciones_contractuales
ÏÏR o
)
ÏÏo p
{
ĞĞ 	
return
ÑÑ 
DACConsulta
ÑÑ 
.
ÑÑ )
Total_DetalleObligacionesMD
ÑÑ :
(
ÑÑ: ;+
id_obligaciones_contractuales
ÑÑ; X
)
ÑÑX Y
;
ÑÑY Z
}
ÓÓ 	
public
ÕÕ 
void
ÕÕ &
ActualizarObligacionesMD
ÕÕ ,
(
ÕÕ, -+
md_obligaciones_contractuales
ÕÕ- J*
OBJObligacionesContractuales
ÕÕK g
,
ÕÕg h
ref
ÕÕi l 
MessageResponseOBJ
ÕÕm 
MsgResÕÕ€ †
)ÕÕ† ‡
{
ÖÖ 	
DACActualiza
×× 
.
×× &
ActualizarObligacionesMD
×× 1
(
××1 2*
OBJObligacionesContractuales
××2 N
,
××N O
ref
××P S
MsgRes
××T Z
)
××Z [
;
××[ \
}
ÙÙ 	
public
ÜÜ 
void
ÜÜ -
ActualizarObligacionesDetalleMD
ÜÜ 3
(
ÜÜ3 43
%md_obligaciones_contractuales_detalle
ÜÜ4 Y1
#OBJObligacionesContractualesDetalle
ÜÜZ }
,
ÜÜ} ~
refÜÜ ‚"
MessageResponseOBJÜÜƒ •
MsgResÜÜ– œ
)ÜÜœ 
{
İİ 	
DACActualiza
ŞŞ 
.
ŞŞ -
ActualizarObligacionesDetalleMD
ŞŞ 8
(
ŞŞ8 91
#OBJObligacionesContractualesDetalle
ŞŞ9 \
,
ŞŞ\ ]
ref
ŞŞ^ a
MsgRes
ŞŞb h
)
ŞŞh i
;
ŞŞi j
}
àà 	
public
ââ 
List
ââ 
<
ââ 3
%md_obligaciones_contractuales_detalle
ââ 9
>
ââ9 :&
GetObligacionesDetalleID
ââ; S
(
ââS T
Int32
ââT Y$
id_md_ref_obligaciones
ââZ p
,
ââp q
Int32
ââr w,
id_obligaciones_contractualesââx •
)ââ• –
{
ãã 	
return
ää 
DACConsulta
ää 
.
ää &
GetObligacionesDetalleID
ää 7
(
ää7 8$
id_md_ref_obligaciones
ää8 N
,
ääN O+
id_obligaciones_contractuales
ääP m
)
ääm n
;
ään o
}
åå 	
public
éé 
Int32
éé ,
InsertarHerramientaTecnologica
éé 3
(
éé3 4 
md_herramienta_tec
éé4 F
OBJHerramienta
ééG U
,
ééU V
ref
ééW Z 
MessageResponseOBJ
éé[ m
MsgRes
één t
)
éét u
{
êê 	
return
ëë 

DACInserta
ëë 
.
ëë ,
InsertarHerramientaTecnologica
ëë <
(
ëë< =
OBJHerramienta
ëë= K
,
ëëK L
ref
ëëM P
MsgRes
ëëQ W
)
ëëW X
;
ëëX Y
}
ìì 	
public
îî 
Int32
îî 
InsertarDetallet1
îî &
(
îî& '
List
îî' +
<
îî+ ,+
md_herramienta_tec_detalle_t1
îî, I
>
îîI J

OBJDetalle
îîK U
,
îîU V
ref
îîW Z 
MessageResponseOBJ
îî[ m
MsgRes
îîn t
)
îît u
{
ïï 	
return
ğğ 

DACInserta
ğğ 
.
ğğ 
InsertarDetallet1
ğğ /
(
ğğ/ 0

OBJDetalle
ğğ0 :
,
ğğ: ;
ref
ğğ< ?
MsgRes
ğğ@ F
)
ğğF G
;
ğğG H
}
òò 	
public
ôô 
Int32
ôô 
InsertarDetallet2
ôô &
(
ôô& '
List
ôô' +
<
ôô+ ,+
md_herramienta_tec_detalle_t2
ôô, I
>
ôôI J

OBJDetalle
ôôK U
,
ôôU V
ref
ôôW Z 
MessageResponseOBJ
ôô[ m
MsgRes
ôôn t
)
ôôt u
{
õõ 	
return
öö 

DACInserta
öö 
.
öö 
InsertarDetallet2
öö /
(
öö/ 0

OBJDetalle
öö0 :
,
öö: ;
ref
öö< ?
MsgRes
öö@ F
)
ööF G
;
ööG H
}
÷÷ 	
public
úú 
Int32
úú 
InsertarDetallet3
úú &
(
úú& '
List
úú' +
<
úú+ ,+
md_herramienta_tec_detalle_t3
úú, I
>
úúI J

OBJDetalle
úúK U
,
úúU V
ref
úúW Z 
MessageResponseOBJ
úú[ m
MsgRes
úún t
)
úút u
{
ûû 	
return
üü 

DACInserta
üü 
.
üü 
InsertarDetallet3
üü /
(
üü/ 0

OBJDetalle
üü0 :
,
üü: ;
ref
üü< ?
MsgRes
üü@ F
)
üüF G
;
üüG H
}
şş 	
public
 
Int32
 
InsertarDetallet4
 &
(
& '
List
' +
<
+ ,+
md_herramienta_tec_detalle_t4
, I
>
I J

OBJDetalle
K U
,
U V
ref
W Z 
MessageResponseOBJ
[ m
MsgRes
n t
)
t u
{
‚‚ 	
return
ƒƒ 

DACInserta
ƒƒ 
.
ƒƒ 
InsertarDetallet4
ƒƒ /
(
ƒƒ/ 0

OBJDetalle
ƒƒ0 :
,
ƒƒ: ;
ref
ƒƒ< ?
MsgRes
ƒƒ@ F
)
ƒƒF G
;
ƒƒG H
}
„„ 	
public
‡‡ 
List
‡‡ 
<
‡‡ *
vw_herramientas_tecnologicas
‡‡ 0
>
‡‡0 1.
 IndicadoresProvvedorHerramientas
‡‡2 R
(
‡‡R S
Int32
‡‡S X

Proveeedor
‡‡Y c
)
‡‡c d
{
ˆˆ 	
return
‰‰ 
DACConsulta
‰‰ 
.
‰‰ .
 IndicadoresProvvedorHerramientas
‰‰ ?
(
‰‰? @

Proveeedor
‰‰@ J
)
‰‰J K
;
‰‰K L
}
ŠŠ 	
public
ŒŒ 
List
ŒŒ 
<
ŒŒ 
md_ref_tabla1
ŒŒ !
>
ŒŒ! "

ref_tabla1
ŒŒ# -
(
ŒŒ- .
)
ŒŒ. /
{
 	
return
 
DACConsulta
 
.
 

ref_tabla1
 )
(
) *
)
* +
;
+ ,
}
 	
public
‘‘ 
List
‘‘ 
<
‘‘ 
vw_md_crono
‘‘ 
>
‘‘   
ConsultaCronograma
‘‘! 3
(
‘‘3 4
)
‘‘4 5
{
’’ 	
return
““ 
DACConsulta
““ 
.
““  
ConsultaCronograma
““ 1
(
““1 2
)
““2 3
;
““3 4
}
”” 	
public
–– 
List
–– 
<
–– #
vw_md_crono_auditores
–– )
>
––) *
GetUsuarioCronoId
––+ <
(
––< =
String
––= C
usuario
––D K
,
––K L
ref
––M P 
MessageResponseOBJ
––Q c
MsgRes
––d j
)
––j k
{
—— 	
return
˜˜ 
DACConsulta
˜˜ 
.
˜˜ 
GetUsuarioCronoId
˜˜ 0
(
˜˜0 1
usuario
˜˜1 8
,
˜˜8 9
ref
˜˜: =
MsgRes
˜˜> D
)
˜˜D E
;
˜˜E F
}
™™ 	
public
›› 
List
›› 
<
›› (
md_ref_puntos_dispensacion
›› .
>
››. /#
GetPuntosDispensacion
››0 E
(
››E F
)
››F G
{
œœ 	
return
 
DACComonClass
  
.
  !#
GetPuntosDispensacion
! 6
(
6 7
)
7 8
;
8 9
}
 	
public
ŸŸ 
List
ŸŸ 
<
ŸŸ #
md_ref_puntos_control
ŸŸ )
>
ŸŸ) *
GetpuntoControl
ŸŸ+ :
(
ŸŸ: ;
)
ŸŸ; <
{
   	
return
¡¡ 
DACConsulta
¡¡ 
.
¡¡ 
GetpuntoControl
¡¡ .
(
¡¡. /
)
¡¡/ 0
;
¡¡0 1
}
¢¢ 	
public
££ 
List
££ 
<
££ 4
&Managment_md_Ref_crono_auditoresResult
££ :
>
££: ;)
ConsultaListaCronoAuditores
££< W
(
££W X
int
££X [
opc1
££\ `
,
££` a
Int32
££b g
?
££g h
opc2
££i m
,
££m n
ref
££o r!
MessageResponseOBJ££s …
MsgRes££† Œ
)££Œ 
{
¤¤ 	
return
¥¥ 
DACConsulta
¥¥ 
.
¥¥ )
ConsultaListaCronoAuditores
¥¥ :
(
¥¥: ;
opc1
¥¥; ?
,
¥¥? @
opc2
¥¥A E
,
¥¥E F
ref
¥¥G J
MsgRes
¥¥K Q
)
¥¥Q R
;
¥¥R S
}
¦¦ 	
public
¨¨ 
Int32
¨¨ *
InsertarInterventoriaGeneral
¨¨ 1
(
¨¨1 2&
md_interventoria_general
¨¨2 J%
OBJInterventoriaGeneral
¨¨K b
,
¨¨b c
ref
¨¨d g 
MessageResponseOBJ
¨¨h z
MsgRes¨¨{ 
)¨¨ ‚
{
©© 	
return
ªª 

DACInserta
ªª 
.
ªª *
InsertarInterventoriaGeneral
ªª :
(
ªª: ;%
OBJInterventoriaGeneral
ªª; R
,
ªªR S
ref
ªªT W
MsgRes
ªªX ^
)
ªª^ _
;
ªª_ `
}
«« 	
public
­­ 
List
­­ 
<
­­ -
Managment_md_Ref_General1Result
­­ 3
>
­­3 4-
DetalleRefInterventoriaGeneral1
­­5 T
(
­­T U
Int32
­­U Z)
id_md_interventoria_general
­­[ v
,
­­v w
Int32
­­x }
opc­­~ 
)­­ ‚
{
®® 	
return
¯¯ 
DACConsulta
¯¯ 
.
¯¯ -
DetalleRefInterventoriaGeneral1
¯¯ >
(
¯¯> ?)
id_md_interventoria_general
¯¯? Z
,
¯¯Z [
opc
¯¯\ _
)
¯¯_ `
;
¯¯` a
}
°° 	
public
²² 
List
²² 
<
²² -
Managment_md_Ref_General2Result
²² 3
>
²²3 4-
DetalleRefInterventoriaGeneral2
²²5 T
(
²²T U
Int32
²²U Z)
id_md_interventoria_general
²²[ v
,
²²v w
Int32
²²x }
opc²²~ 
)²² ‚
{
³³ 	
return
´´ 
DACConsulta
´´ 
.
´´ -
DetalleRefInterventoriaGeneral2
´´ >
(
´´> ?)
id_md_interventoria_general
´´? Z
,
´´Z [
opc
´´\ _
)
´´_ `
;
´´` a
}
µµ 	
public
·· 
List
·· 
<
·· -
Managment_md_Ref_General3Result
·· 3
>
··3 4-
DetalleRefInterventoriaGeneral3
··5 T
(
··T U
Int32
··U Z)
id_md_interventoria_general
··[ v
,
··v w
Int32
··x }
opc··~ 
)·· ‚
{
¸¸ 	
return
¹¹ 
DACConsulta
¹¹ 
.
¹¹ -
DetalleRefInterventoriaGeneral3
¹¹ >
(
¹¹> ?)
id_md_interventoria_general
¹¹? Z
,
¹¹Z [
opc
¹¹\ _
)
¹¹_ `
;
¹¹` a
}
ºº 	
public
¼¼ 
List
¼¼ 
<
¼¼ -
Managment_md_Ref_General4Result
¼¼ 3
>
¼¼3 4-
DetalleRefInterventoriaGeneral4
¼¼5 T
(
¼¼T U
Int32
¼¼U Z)
id_md_interventoria_general
¼¼[ v
,
¼¼v w
Int32
¼¼x }
opc¼¼~ 
)¼¼ ‚
{
½½ 	
return
¾¾ 
DACConsulta
¾¾ 
.
¾¾ -
DetalleRefInterventoriaGeneral4
¾¾ >
(
¾¾> ?)
id_md_interventoria_general
¾¾? Z
,
¾¾Z [
opc
¾¾\ _
)
¾¾_ `
;
¾¾` a
}
¿¿ 	
public
ÁÁ 
Int32
ÁÁ %
InsertarGeneral1Detalle
ÁÁ ,
(
ÁÁ, -/
!md_interventoria_general_detalle1
ÁÁ- N
OBJDetallleG1
ÁÁO \
,
ÁÁ\ ]
ref
ÁÁ^ a 
MessageResponseOBJ
ÁÁb t
MsgRes
ÁÁu {
)
ÁÁ{ |
{
ÂÂ 	
return
ÃÃ 

DACInserta
ÃÃ 
.
ÃÃ %
InsertarGeneral1Detalle
ÃÃ 5
(
ÃÃ5 6
OBJDetallleG1
ÃÃ6 C
,
ÃÃC D
ref
ÃÃE H
MsgRes
ÃÃI O
)
ÃÃO P
;
ÃÃP Q
}
ÄÄ 	
public
ÆÆ 
Int32
ÆÆ %
InsertarGeneral2Detalle
ÆÆ ,
(
ÆÆ, -/
!md_interventoria_general_detalle2
ÆÆ- N
OBJDetallleG2
ÆÆO \
,
ÆÆ\ ]
ref
ÆÆ^ a 
MessageResponseOBJ
ÆÆb t
MsgRes
ÆÆu {
)
ÆÆ{ |
{
ÇÇ 	
return
ÈÈ 

DACInserta
ÈÈ 
.
ÈÈ %
InsertarGeneral2Detalle
ÈÈ 5
(
ÈÈ5 6
OBJDetallleG2
ÈÈ6 C
,
ÈÈC D
ref
ÈÈE H
MsgRes
ÈÈI O
)
ÈÈO P
;
ÈÈP Q
}
ÉÉ 	
public
ÌÌ 
Int32
ÌÌ %
InsertarGeneral3Detalle
ÌÌ ,
(
ÌÌ, -/
!md_interventoria_general_detalle3
ÌÌ- N
OBJDetallleG3
ÌÌO \
,
ÌÌ\ ]
ref
ÌÌ^ a 
MessageResponseOBJ
ÌÌb t
MsgRes
ÌÌu {
)
ÌÌ{ |
{
ÍÍ 	
return
ÎÎ 

DACInserta
ÎÎ 
.
ÎÎ %
InsertarGeneral3Detalle
ÎÎ 5
(
ÎÎ5 6
OBJDetallleG3
ÎÎ6 C
,
ÎÎC D
ref
ÎÎE H
MsgRes
ÎÎI O
)
ÎÎO P
;
ÎÎP Q
}
ÏÏ 	
public
ÑÑ 
Int32
ÑÑ %
InsertarGeneral4Detalle
ÑÑ ,
(
ÑÑ, -/
!md_interventoria_general_detalle4
ÑÑ- N
OBJDetallleG4
ÑÑO \
,
ÑÑ\ ]
ref
ÑÑ^ a 
MessageResponseOBJ
ÑÑb t
MsgRes
ÑÑu {
)
ÑÑ{ |
{
ÒÒ 	
return
ÓÓ 

DACInserta
ÓÓ 
.
ÓÓ %
InsertarGeneral4Detalle
ÓÓ 5
(
ÓÓ5 6
OBJDetallleG4
ÓÓ6 C
,
ÓÓC D
ref
ÓÓE H
MsgRes
ÓÓI O
)
ÓÓO P
;
ÓÓP Q
}
ÔÔ 	
public
ÖÖ 
List
ÖÖ 
<
ÖÖ 
md_ref_tabla2
ÖÖ !
>
ÖÖ! "

ref_tabla2
ÖÖ# -
(
ÖÖ- .
)
ÖÖ. /
{
×× 	
return
ØØ 
DACConsulta
ØØ 
.
ØØ 

ref_tabla2
ØØ )
(
ØØ) *
)
ØØ* +
;
ØØ+ ,
}
ÙÙ 	
public
ÚÚ 
List
ÚÚ 
<
ÚÚ 
md_ref_tabla3
ÚÚ !
>
ÚÚ! "

ref_tabla3
ÚÚ# -
(
ÚÚ- .
)
ÚÚ. /
{
ÛÛ 	
return
ÜÜ 
DACConsulta
ÜÜ 
.
ÜÜ 

ref_tabla3
ÜÜ )
(
ÜÜ) *
)
ÜÜ* +
;
ÜÜ+ ,
}
İİ 	
public
ŞŞ 
List
ŞŞ 
<
ŞŞ 
md_ref_tabla4
ŞŞ !
>
ŞŞ! "

ref_tabla4
ŞŞ# -
(
ŞŞ- .
)
ŞŞ. /
{
ßß 	
return
àà 
DACConsulta
àà 
.
àà 

ref_tabla4
àà )
(
àà) *
)
àà* +
;
àà+ ,
}
áá 	
public
ââ 
List
ââ 
<
ââ 
vw_tabla1_categ
ââ #
>
ââ# $

Tabla1Catg
ââ% /
(
ââ/ 0
)
ââ0 1
{
ãã 	
return
ää 
DACConsulta
ää 
.
ää 

Tabla1Catg
ää )
(
ää) *
)
ää* +
;
ää+ ,
}
åå 	
public
çç 
List
çç 
<
çç 
vw_md_detalle_T1
çç $
>
çç$ %
Tabla1Detalle
çç& 3
(
çç3 4
Int32
çç4 9
id_cat
çç: @
,
çç@ A
Int32
ççB G 
id_herramienta_tec
ççH Z
)
ççZ [
{
èè 	
return
éé 
DACConsulta
éé 
.
éé 
Tabla1Detalle
éé ,
(
éé, -
id_cat
éé- 3
,
éé3 4 
id_herramienta_tec
éé5 G
)
ééG H
;
ééH I
}
êê 	
public
ëë 
List
ëë 
<
ëë 
vw_md_detalle_T2
ëë $
>
ëë$ %
Tabla2Detalle
ëë& 3
(
ëë3 4
Int32
ëë4 9
id_cat
ëë: @
,
ëë@ A
Int32
ëëB G 
id_herramienta_tec
ëëH Z
)
ëëZ [
{
ìì 	
return
íí 
DACConsulta
íí 
.
íí 
Tabla2Detalle
íí ,
(
íí, -
id_cat
íí- 3
,
íí3 4 
id_herramienta_tec
íí5 G
)
ííG H
;
ííH I
}
îî 	
public
ïï 
List
ïï 
<
ïï 
vw_md_detalle_T3
ïï $
>
ïï$ %
Tabla3Detalle
ïï& 3
(
ïï3 4
Int32
ïï4 9
id_cat
ïï: @
,
ïï@ A
Int32
ïïB G 
id_herramienta_tec
ïïH Z
)
ïïZ [
{
ğğ 	
return
ññ 
DACConsulta
ññ 
.
ññ 
Tabla3Detalle
ññ ,
(
ññ, -
id_cat
ññ- 3
,
ññ3 4 
id_herramienta_tec
ññ5 G
)
ññG H
;
ññH I
}
òò 	
public
óó 
List
óó 
<
óó 
vw_md_detalle_T4
óó $
>
óó$ %
Tabla4Detalle
óó& 3
(
óó3 4
Int32
óó4 9
id_cat
óó: @
,
óó@ A
Int32
óóB G 
id_herramienta_tec
óóH Z
)
óóZ [
{
ôô 	
return
õõ 
DACConsulta
õõ 
.
õõ 
Tabla4Detalle
õõ ,
(
õõ, -
id_cat
õõ- 3
,
õõ3 4 
id_herramienta_tec
õõ5 G
)
õõG H
;
õõH I
}
öö 	
public
øø 
vw_md_total_T1
øø 
	totalesT1
øø '
(
øø' (
Int32
øø( -
id
øø. 0
)
øø0 1
{
ùù 	
return
úú 
DACConsulta
úú 
.
úú 
	totalesT1
úú (
(
úú( )
id
úú) +
)
úú+ ,
;
úú, -
}
ûû 	
public
üü 
vw_md_total_T2
üü 
	totalesT2
üü '
(
üü' (
Int32
üü( -
id
üü. 0
)
üü0 1
{
ıı 	
return
şş 
DACConsulta
şş 
.
şş 
	totalesT2
şş (
(
şş( )
id
şş) +
)
şş+ ,
;
şş, -
}
ÿÿ 	
public
€€ 
vw_md_total_T3
€€ 
	totalesT3
€€ '
(
€€' (
Int32
€€( -
id
€€. 0
)
€€0 1
{
 	
return
‚‚ 
DACConsulta
‚‚ 
.
‚‚ 
	totalesT3
‚‚ (
(
‚‚( )
id
‚‚) +
)
‚‚+ ,
;
‚‚, -
}
ƒƒ 	
public
„„ 
vw_md_total_T4
„„ 
	totalesT4
„„ '
(
„„' (
Int32
„„( -
id
„„. 0
)
„„0 1
{
…… 	
return
†† 
DACConsulta
†† 
.
†† 
	totalesT4
†† (
(
††( )
id
††) +
)
††+ ,
;
††, -
}
‡‡ 	
public
ŠŠ 
void
ŠŠ !
ActualizarDetallet1
ŠŠ '
(
ŠŠ' (+
md_herramienta_tec_detalle_t1
ŠŠ( E
OBJDetalleT
ŠŠF Q
,
ŠŠQ R
ref
ŠŠS V 
MessageResponseOBJ
ŠŠW i
MsgRes
ŠŠj p
)
ŠŠp q
{
‹‹ 	
DACActualiza
ŒŒ 
.
ŒŒ !
ActualizarDetallet1
ŒŒ ,
(
ŒŒ, -
OBJDetalleT
ŒŒ- 8
,
ŒŒ8 9
ref
ŒŒ: =
MsgRes
ŒŒ> D
)
ŒŒD E
;
ŒŒE F
}
 	
public
 
void
 !
ActualizarDetallet2
 '
(
' (+
md_herramienta_tec_detalle_t2
( E
OBJDetalleT
F Q
,
Q R
ref
S V 
MessageResponseOBJ
W i
MsgRes
j p
)
p q
{
 	
DACActualiza
 
.
 !
ActualizarDetallet2
 ,
(
, -
OBJDetalleT
- 8
,
8 9
ref
: =
MsgRes
> D
)
D E
;
E F
}
‘‘ 	
public
’’ 
void
’’ !
ActualizarDetallet3
’’ '
(
’’' (+
md_herramienta_tec_detalle_t3
’’( E
OBJDetalleT
’’F Q
,
’’Q R
ref
’’S V 
MessageResponseOBJ
’’W i
MsgRes
’’j p
)
’’p q
{
““ 	
DACActualiza
”” 
.
”” !
ActualizarDetallet3
”” ,
(
””, -
OBJDetalleT
””- 8
,
””8 9
ref
””: =
MsgRes
””> D
)
””D E
;
””E F
}
•• 	
public
–– 
void
–– !
ActualizarDetallet4
–– '
(
––' (+
md_herramienta_tec_detalle_t4
––( E
OBJDetalleT
––F Q
,
––Q R
ref
––S V 
MessageResponseOBJ
––W i
MsgRes
––j p
)
––p q
{
—— 	
DACActualiza
˜˜ 
.
˜˜ !
ActualizarDetallet4
˜˜ ,
(
˜˜, -
OBJDetalleT
˜˜- 8
,
˜˜8 9
ref
˜˜: =
MsgRes
˜˜> D
)
˜˜D E
;
˜˜E F
}
™™ 	
public
œœ 
void
œœ  
ActualizarGeneral1
œœ &
(
œœ& ' 
md_herramienta_tec
œœ' 9
OBJDetalleT
œœ: E
,
œœE F
ref
œœG J 
MessageResponseOBJ
œœK ]
MsgRes
œœ^ d
)
œœd e
{
 	
DACActualiza
 
.
  
ActualizarGeneral1
 +
(
+ ,
OBJDetalleT
, 7
,
7 8
ref
9 <
MsgRes
= C
)
C D
;
D E
}
ŸŸ 	
public
   
void
    
ActualizarGeneral2
   &
(
  & ' 
md_herramienta_tec
  ' 9
OBJDetalleT
  : E
,
  E F
ref
  G J 
MessageResponseOBJ
  K ]
MsgRes
  ^ d
)
  d e
{
¡¡ 	
DACActualiza
¢¢ 
.
¢¢  
ActualizarGeneral2
¢¢ +
(
¢¢+ ,
OBJDetalleT
¢¢, 7
,
¢¢7 8
ref
¢¢9 <
MsgRes
¢¢= C
)
¢¢C D
;
¢¢D E
}
££ 	
public
¤¤ 
void
¤¤  
ActualizarGeneral3
¤¤ &
(
¤¤& ' 
md_herramienta_tec
¤¤' 9
OBJDetalleT
¤¤: E
,
¤¤E F
ref
¤¤G J 
MessageResponseOBJ
¤¤K ]
MsgRes
¤¤^ d
)
¤¤d e
{
¥¥ 	
DACActualiza
¦¦ 
.
¦¦  
ActualizarGeneral3
¦¦ +
(
¦¦+ ,
OBJDetalleT
¦¦, 7
,
¦¦7 8
ref
¦¦9 <
MsgRes
¦¦= C
)
¦¦C D
;
¦¦D E
}
§§ 	
public
¨¨ 
void
¨¨  
ActualizarGeneral4
¨¨ &
(
¨¨& ' 
md_herramienta_tec
¨¨' 9
OBJDetalleT
¨¨: E
,
¨¨E F
ref
¨¨G J 
MessageResponseOBJ
¨¨K ]
MsgRes
¨¨^ d
)
¨¨d e
{
©© 	
DACActualiza
ªª 
.
ªª  
ActualizarGeneral4
ªª +
(
ªª+ ,
OBJDetalleT
ªª, 7
,
ªª7 8
ref
ªª9 <
MsgRes
ªª= C
)
ªªC D
;
ªªD E
}
«« 	
public
®® /
!vw_total_md_interventoria_detalle
®® 01
#Total_DetalleInterventoriaGeneralMD
®®1 T
(
®®T U
Int32
®®U Z)
id_md_interventoria_general
®®[ v
)
®®v w
{
¯¯ 	
return
°° 
DACConsulta
°° 
.
°° 1
#Total_DetalleInterventoriaGeneralMD
°° B
(
°°B C)
id_md_interventoria_general
°°C ^
)
°°^ _
;
°°_ `
}
²² 	
public
´´ 
void
´´ .
 ActualizarInterventoriaGeneralMD
´´ 4
(
´´4 5&
md_interventoria_general
´´5 M%
OBJInterventoriaGeneral
´´N e
,
´´e f
ref
´´g j 
MessageResponseOBJ
´´k }
MsgRes´´~ „
)´´„ …
{
µµ 	
DACActualiza
¶¶ 
.
¶¶ .
 ActualizarInterventoriaGeneralMD
¶¶ 9
(
¶¶9 :%
OBJInterventoriaGeneral
¶¶: Q
,
¶¶Q R
ref
¶¶S V
MsgRes
¶¶W ]
)
¶¶] ^
;
¶¶^ _
}
¸¸ 	
public
ºº 
List
ºº 
<
ºº /
!md_interventoria_general_detalle1
ºº 5
>
ºº5 6(
GetInterventoriaDetalle1ID
ºº7 Q
(
ººQ R
Int32
ººR W%
id_md_ref_inte_general1
ººX o
,
ººo p
Int32
ººq v*
id_md_interventoria_generalººw ’
)ºº’ “
{
»» 	
return
¼¼ 
DACConsulta
¼¼ 
.
¼¼ (
GetInterventoriaDetalle1ID
¼¼ 9
(
¼¼9 :%
id_md_ref_inte_general1
¼¼: Q
,
¼¼Q R)
id_md_interventoria_general
¼¼S n
)
¼¼n o
;
¼¼o p
}
½½ 	
public
¿¿ 
List
¿¿ 
<
¿¿ /
!md_interventoria_general_detalle2
¿¿ 5
>
¿¿5 6(
GetInterventoriaDetalle2ID
¿¿7 Q
(
¿¿Q R
Int32
¿¿R W%
id_md_ref_inte_general2
¿¿X o
,
¿¿o p
Int32
¿¿q v*
id_md_interventoria_general¿¿w ’
)¿¿’ “
{
ÀÀ 	
return
ÁÁ 
DACConsulta
ÁÁ 
.
ÁÁ (
GetInterventoriaDetalle2ID
ÁÁ 9
(
ÁÁ9 :%
id_md_ref_inte_general2
ÁÁ: Q
,
ÁÁQ R)
id_md_interventoria_general
ÁÁS n
)
ÁÁn o
;
ÁÁo p
}
ÂÂ 	
public
ÄÄ 
List
ÄÄ 
<
ÄÄ /
!md_interventoria_general_detalle3
ÄÄ 5
>
ÄÄ5 6(
GetInterventoriaDetalle3ID
ÄÄ7 Q
(
ÄÄQ R
Int32
ÄÄR W%
id_md_ref_inte_general3
ÄÄX o
,
ÄÄo p
Int32
ÄÄq v*
id_md_interventoria_generalÄÄw ’
)ÄÄ’ “
{
ÅÅ 	
return
ÆÆ 
DACConsulta
ÆÆ 
.
ÆÆ (
GetInterventoriaDetalle3ID
ÆÆ 9
(
ÆÆ9 :%
id_md_ref_inte_general3
ÆÆ: Q
,
ÆÆQ R)
id_md_interventoria_general
ÆÆS n
)
ÆÆn o
;
ÆÆo p
}
ÇÇ 	
public
ÉÉ 
List
ÉÉ 
<
ÉÉ /
!md_interventoria_general_detalle4
ÉÉ 5
>
ÉÉ5 6(
GetInterventoriaDetalle4ID
ÉÉ7 Q
(
ÉÉQ R
Int32
ÉÉR W%
id_md_ref_inte_general4
ÉÉX o
,
ÉÉo p
Int32
ÉÉq v*
id_md_interventoria_generalÉÉw ’
)ÉÉ’ “
{
ÊÊ 	
return
ËË 
DACConsulta
ËË 
.
ËË (
GetInterventoriaDetalle4ID
ËË 9
(
ËË9 :%
id_md_ref_inte_general4
ËË: Q
,
ËËQ R)
id_md_interventoria_general
ËËS n
)
ËËn o
;
ËËo p
}
ÌÌ 	
public
ÏÏ 
void
ÏÏ 6
(ActualizarInterventoriaGeneralDetalle1MD
ÏÏ <
(
ÏÏ< =/
!md_interventoria_general_detalle1
ÏÏ= ^
OBJDetallleG1
ÏÏ_ l
,
ÏÏl m
ref
ÏÏn q!
MessageResponseOBJÏÏr „
MsgResÏÏ… ‹
)ÏÏ‹ Œ
{
ĞĞ 	
DACActualiza
ÑÑ 
.
ÑÑ 6
(ActualizarInterventoriaGeneralDetalle1MD
ÑÑ A
(
ÑÑA B
OBJDetallleG1
ÑÑB O
,
ÑÑO P
ref
ÑÑQ T
MsgRes
ÑÑU [
)
ÑÑ[ \
;
ÑÑ\ ]
}
ÓÓ 	
public
ÕÕ 
void
ÕÕ 6
(ActualizarInterventoriaGeneralDetalle2MD
ÕÕ <
(
ÕÕ< =/
!md_interventoria_general_detalle2
ÕÕ= ^
OBJDetallleG2
ÕÕ_ l
,
ÕÕl m
ref
ÕÕn q!
MessageResponseOBJÕÕr „
MsgResÕÕ… ‹
)ÕÕ‹ Œ
{
ÖÖ 	
DACActualiza
×× 
.
×× 6
(ActualizarInterventoriaGeneralDetalle2MD
×× A
(
××A B
OBJDetallleG2
××B O
,
××O P
ref
××Q T
MsgRes
××U [
)
××[ \
;
××\ ]
}
ÙÙ 	
public
ÚÚ 
void
ÚÚ 6
(ActualizarInterventoriaGeneralDetalle3MD
ÚÚ <
(
ÚÚ< =/
!md_interventoria_general_detalle3
ÚÚ= ^
OBJDetallleG3
ÚÚ_ l
,
ÚÚl m
ref
ÚÚn q!
MessageResponseOBJÚÚr „
MsgResÚÚ… ‹
)ÚÚ‹ Œ
{
ÛÛ 	
DACActualiza
ÜÜ 
.
ÜÜ 6
(ActualizarInterventoriaGeneralDetalle3MD
ÜÜ A
(
ÜÜA B
OBJDetallleG3
ÜÜB O
,
ÜÜO P
ref
ÜÜQ T
MsgRes
ÜÜU [
)
ÜÜ[ \
;
ÜÜ\ ]
}
ŞŞ 	
public
ßß 
void
ßß 6
(ActualizarInterventoriaGeneralDetalle4MD
ßß <
(
ßß< =/
!md_interventoria_general_detalle4
ßß= ^
OBJDetallleG4
ßß_ l
,
ßßl m
ref
ßßn q!
MessageResponseOBJßßr „
MsgResßß… ‹
)ßß‹ Œ
{
àà 	
DACActualiza
áá 
.
áá 6
(ActualizarInterventoriaGeneralDetalle4MD
áá A
(
ááA B
OBJDetallleG4
ááB O
,
ááO P
ref
ááQ T
MsgRes
ááU [
)
áá[ \
;
áá\ ]
}
ãã 	
public
åå 
List
åå 
<
åå )
vw_md_interventoria_general
åå /
>
åå/ 0+
InterventoriaGeneralProveedor
åå1 N
(
ååN O
String
ååO U
	Proveedor
ååV _
)
åå_ `
{
ææ 	
return
çç 
DACConsulta
çç 
.
çç +
InterventoriaGeneralProveedor
çç <
(
çç< =
	Proveedor
çç= F
)
ççF G
;
ççG H
}
èè 	
public
éé 
Int32
éé %
InsertarCargueCuentasMd
éé ,
(
éé, -
md_cargue_cuentas
éé- >
OBJCargueCuentas
éé? O
,
ééO P
ref
ééQ T 
MessageResponseOBJ
ééU g
MsgRes
ééh n
)
één o
{
êê 	
return
ëë 

DACInserta
ëë 
.
ëë %
InsertarCargueCuentasMd
ëë 5
(
ëë5 6
OBJCargueCuentas
ëë6 F
,
ëëF G
ref
ëëH K
MsgRes
ëëL R
)
ëëR S
;
ëëS T
}
ìì 	
public
îî 
Int32
îî +
InsertarSeguimientoPendientes
îî 2
(
îî2 3'
md_seguimiento_pendientes
îî3 L&
OBJSeguimientoPendientes
îîM e
,
îîe f
ref
îîg j 
MessageResponseOBJ
îîk }
MsgResîî~ „
)îî„ …
{
ïï 	
return
ğğ 

DACInserta
ğğ 
.
ğğ +
InsertarSeguimientoPendientes
ğğ ;
(
ğğ; <&
OBJSeguimientoPendientes
ğğ< T
,
ğğT U
ref
ğğV Y
MsgRes
ğğZ `
)
ğğ` a
;
ğğa b
}
ññ 	
public
óó 
Int32
óó 
?
óó 2
$InsertarSeguimientoPendientesDetalle
óó :
(
óó: ;/
!md_seguimiento_pendientes_detalle
óó; \-
OBJSeguimientoPendientesDetalle
óó] |
,
óó| }
refóó~ "
MessageResponseOBJóó‚ ”
MsgResóó• ›
)óó› œ
{
ôô 	
return
õõ 

DACInserta
õõ 
.
õõ 2
$InsertarSeguimientoPendientesDetalle
õõ B
(
õõB C-
OBJSeguimientoPendientesDetalle
õõC b
,
õõb c
ref
õõd g
MsgRes
õõh n
)
õõn o
;
õõo p
}
öö 	
public
øø 
List
øø 
<
øø ;
-Managment_md_Ref_seguimiento_pendientesResult
øø A
>
øøA B-
DetalleRefSeguimientoPendientes
øøC b
(
øøb c
Int32
øøc h+
id_md_seguimiento_pendientesøøi …
,øø… †
Int32øø‡ Œ
opcøø 
)øø ‘
{
ùù 	
return
úú 
DACConsulta
úú 
.
úú -
DetalleRefSeguimientoPendientes
úú >
(
úú> ?*
id_md_seguimiento_pendientes
úú? [
,
úú[ \
opc
úú] `
)
úú` a
;
úúa b
}
ûû 	
public
şş -
vw_total_md_seguimiento_detalle
şş .2
$Total_DetalleSeguimientoPendientesMD
şş/ S
(
şşS T
Int32
şşT Y*
id_md_seguimiento_pendientes
şşZ v
)
şşv w
{
ÿÿ 	
return
€€ 
DACConsulta
€€ 
.
€€ 2
$Total_DetalleSeguimientoPendientesMD
€€ C
(
€€C D*
id_md_seguimiento_pendientes
€€D `
)
€€` a
;
€€a b
}
‚‚ 	
public
„„ 
void
„„ /
!ActualizarSeguimientoPendientesMD
„„ 5
(
„„5 6'
md_seguimiento_pendientes
„„6 O&
OBJSeguimientoPendientes
„„P h
,
„„h i
ref
„„j m!
MessageResponseOBJ„„n €
MsgRes„„ ‡
)„„‡ ˆ
{
…… 	
DACActualiza
†† 
.
†† /
!ActualizarSeguimientoPendientesMD
†† :
(
††: ;&
OBJSeguimientoPendientes
††; S
,
††S T
ref
††U X
MsgRes
††Y _
)
††_ `
;
††` a
}
ˆˆ 	
public
ŠŠ 
List
ŠŠ 
<
ŠŠ /
!md_seguimiento_pendientes_detalle
ŠŠ 5
>
ŠŠ5 6/
!GetSeguimientoPendientesDetalleID
ŠŠ7 X
(
ŠŠX Y
Int32
ŠŠY ^.
 id_md_ref_seguimiento_pendientes
ŠŠ_ 
,ŠŠ €
Int32ŠŠ †,
id_md_seguimiento_pendientesŠŠ‡ £
)ŠŠ£ ¤
{
‹‹ 	
return
ŒŒ 
DACConsulta
ŒŒ 
.
ŒŒ /
!GetSeguimientoPendientesDetalleID
ŒŒ @
(
ŒŒ@ A.
 id_md_ref_seguimiento_pendientes
ŒŒA a
,
ŒŒa b*
id_md_seguimiento_pendientes
ŒŒc 
)ŒŒ €
;ŒŒ€ 
}
 	
public
 
void
 6
(ActualizarSeguimientoPendientesDetalleMD
 <
(
< =/
!md_seguimiento_pendientes_detalle
= ^-
OBJSeguimientoPendientesDetalle
_ ~
,
~ 
ref€ ƒ"
MessageResponseOBJ„ –
MsgRes— 
) 
{
 	
DACActualiza
‘‘ 
.
‘‘ 6
(ActualizarSeguimientoPendientesDetalleMD
‘‘ A
(
‘‘A B-
OBJSeguimientoPendientesDetalle
‘‘B a
,
‘‘a b
ref
‘‘c f
MsgRes
‘‘g m
)
‘‘m n
;
‘‘n o
}
““ 	
public
–– 
List
–– 
<
–– *
vw_md_seguimiento_pendientes
–– 0
>
––0 1,
SeguimientoPendientesProveedor
––2 P
(
––P Q
String
––Q W
	Proveedor
––X a
)
––a b
{
—— 	
return
˜˜ 
DACConsulta
˜˜ 
.
˜˜ ,
SeguimientoPendientesProveedor
˜˜ =
(
˜˜= >
	Proveedor
˜˜> G
)
˜˜G H
;
˜˜H I
}
™™ 	
public
›› 
Int32
›› ,
InsertarConsolidadoFacturacion
›› 3
(
››3 4
List
››4 8
<
››8 9(
md_consolidado_facturacion
››9 S
>
››S T

OBJDetalle
››U _
,
››_ `
ref
››a d 
MessageResponseOBJ
››e w
MsgRes
››x ~
)
››~ 
{
œœ 	
return
 

DACInserta
 
.
 ,
InsertarConsolidadoFacturacion
 <
(
< =

OBJDetalle
= G
,
G H
ref
I L
MsgRes
M S
)
S T
;
T U
}
 	
public
   
List
   
<
   %
vw_gestionDocumentalCad
   +
>
  + ,)
GestionDocumentalMedCalidad
  - H
(
  H I
Int32
  I N
id
  O Q
,
  Q R
Int32
  S X
id2
  Y \
)
  \ ]
{
¡¡ 	
return
¢¢ 
DACConsulta
¢¢ 
.
¢¢ )
GestionDocumentalMedCalidad
¢¢ :
(
¢¢: ;
id
¢¢; =
,
¢¢= >
id2
¢¢? B
)
¢¢B C
;
¢¢C D
}
££ 	
public
¥¥ 
Int32
¥¥ (
InsertarHerramientaGestion
¥¥ /
(
¥¥/ 0(
md_herramienta_tec_gestion
¥¥0 J

OBJGestion
¥¥K U
,
¥¥U V
ref
¥¥W Z 
MessageResponseOBJ
¥¥[ m
MsgRes
¥¥n t
)
¥¥t u
{
¦¦ 	
return
§§ 

DACInserta
§§ 
.
§§ (
InsertarHerramientaGestion
§§ 8
(
§§8 9

OBJGestion
§§9 C
,
§§C D
ref
§§E H
MsgRes
§§I O
)
§§O P
;
§§P Q
}
©© 	
public
«« 
List
«« 
<
«« -
vw__md_herramientaT_sin_cumplir
«« 3
>
««3 4'
GetHerramientasSincumplir
««5 N
(
««N O
Int32
««O T$
id_herramienta_tec_med
««U k
,
««k l
Int32
««m r
tabla
««s x
)
««x y
{
¬¬ 	
return
­­ 
DACConsulta
­­ 
.
­­ '
GetHerramientasSincumplir
­­ 8
(
­­8 9$
id_herramienta_tec_med
­­9 O
,
­­O P
tabla
­­Q V
)
­­V W
;
­­W X
}
®® 	
public
±± 
List
±± 
<
±± 2
$ManagmentReportNotifiAuditoriaResult
±± 8
>
±±8 9$
ReportNotificacionAudi
±±: P
(
±±P Q
Int32
±±Q V
id
±±W Y
)
±±Y Z
{
²² 	
return
³³ 
DACConsulta
³³ 
.
³³ $
ReportNotificacionAudi
³³ 5
(
³³5 6
id
³³6 8
)
³³8 9
;
³³9 :
}
µµ 	
public
¸¸ 
Int32
¸¸ .
 Insertardispensacion_ambulatoria
¸¸ 5
(
¸¸5 6)
md_dispensacion_ambulatoria
¸¸6 Q

OBJDetalle
¸¸R \
,
¸¸\ ]
ref
¸¸^ a 
MessageResponseOBJ
¸¸b t
MsgRes
¸¸u {
)
¸¸{ |
{
¹¹ 	
return
ºº 

DACInserta
ºº 
.
ºº .
 Insertardispensacion_ambulatoria
ºº >
(
ºº> ?

OBJDetalle
ºº? I
,
ººI J
ref
ººK N
MsgRes
ººO U
)
ººU V
;
ººV W
}
»» 	
public
½½ 
Int32
½½ /
!Insertardispensacion_Hospitalaria
½½ 6
(
½½6 7*
md_dispensacion_hospitalaria
½½7 S

OBJDetalle
½½T ^
,
½½^ _
ref
½½` c 
MessageResponseOBJ
½½d v
MsgRes
½½w }
)
½½} ~
{
¾¾ 	
return
¿¿ 

DACInserta
¿¿ 
.
¿¿ /
!Insertardispensacion_Hospitalaria
¿¿ ?
(
¿¿? @

OBJDetalle
¿¿@ J
,
¿¿J K
ref
¿¿L O
MsgRes
¿¿P V
)
¿¿V W
;
¿¿W X
}
ÀÀ 	
public
ÂÂ 
Int32
ÂÂ 5
'Insertardispensacion_ambulatoriaDetalle
ÂÂ <
(
ÂÂ< =1
#md_dispensacion_ambulatoria_detalle
ÂÂ= `

OBJDetalle
ÂÂa k
,
ÂÂk l
ref
ÂÂm p!
MessageResponseOBJÂÂq ƒ
MsgResÂÂ„ Š
)ÂÂŠ ‹
{
ÃÃ 	
return
ÄÄ 

DACInserta
ÄÄ 
.
ÄÄ 5
'Insertardispensacion_ambulatoriaDetalle
ÄÄ E
(
ÄÄE F

OBJDetalle
ÄÄF P
,
ÄÄP Q
ref
ÄÄR U
MsgRes
ÄÄV \
)
ÄÄ\ ]
;
ÄÄ] ^
}
ÆÆ 	
public
ÈÈ 
Int32
ÈÈ 6
(Insertardispensacion_HospitalariaDetalle
ÈÈ =
(
ÈÈ= >2
$md_dispensacion_hospitalaria_detalle
ÈÈ> b

OBJDetalle
ÈÈc m
,
ÈÈm n
ref
ÈÈo r!
MessageResponseOBJÈÈs …
MsgResÈÈ† Œ
)ÈÈŒ 
{
ÉÉ 	
return
ÊÊ 

DACInserta
ÊÊ 
.
ÊÊ 6
(Insertardispensacion_HospitalariaDetalle
ÊÊ F
(
ÊÊF G

OBJDetalle
ÊÊG Q
,
ÊÊQ R
ref
ÊÊS V
MsgRes
ÊÊW ]
)
ÊÊ] ^
;
ÊÊ^ _
}
ËË 	
public
ÍÍ 
List
ÍÍ 
<
ÍÍ (
md_ref_dispens_ambulatoria
ÍÍ .
>
ÍÍ. /(
RefDispersacionAmbulatoria
ÍÍ0 J
(
ÍÍJ K
)
ÍÍK L
{
ÎÎ 	
return
ÏÏ 
DACConsulta
ÏÏ 
.
ÏÏ (
RefDispersacionAmbulatoria
ÏÏ 9
(
ÏÏ9 :
)
ÏÏ: ;
;
ÏÏ; <
}
ĞĞ 	
public
ÒÒ 
List
ÒÒ 
<
ÒÒ )
md_ref_dispens_hospitalaria
ÒÒ /
>
ÒÒ/ 0)
RefDispersacionHospitalaria
ÒÒ1 L
(
ÒÒL M
)
ÒÒM N
{
ÓÓ 	
return
ÔÔ 
DACConsulta
ÔÔ 
.
ÔÔ )
RefDispersacionHospitalaria
ÔÔ :
(
ÔÔ: ;
)
ÔÔ; <
;
ÔÔ< =
}
ÕÕ 	
public
ÖÖ 
List
ÖÖ 
<
ÖÖ 0
"Managment_md_Ref_ambulatorioResult
ÖÖ 6
>
ÖÖ6 7#
DetalleRefAmbulatorio
ÖÖ8 M
(
ÖÖM N
Int32
ÖÖN S,
id_md_dispensacion_ambulatoria
ÖÖT r
)
ÖÖr s
{
×× 	
return
ØØ 
DACConsulta
ØØ 
.
ØØ #
DetalleRefAmbulatorio
ØØ 4
(
ØØ4 5,
id_md_dispensacion_ambulatoria
ØØ5 S
)
ØØS T
;
ØØT U
}
ÙÙ 	
public
ÛÛ 
List
ÛÛ 
<
ÛÛ 1
#Managment_md_Ref_hospitalarioResult
ÛÛ 7
>
ÛÛ7 8$
DetalleRefHospitalario
ÛÛ9 O
(
ÛÛO P
Int32
ÛÛP U-
id_md_dispensacion_Hospitalaria
ÛÛV u
)
ÛÛu v
{
ÜÜ 	
return
İİ 
DACConsulta
İİ 
.
İİ $
DetalleRefHospitalario
İİ 5
(
İİ5 6-
id_md_dispensacion_Hospitalaria
İİ6 U
)
İİU V
;
İİV W
}
ŞŞ 	
public
àà 
List
àà 
<
àà 1
#md_dispensacion_ambulatoria_detalle
àà 7
>
àà7 8%
GetAmbulatorioDetalleID
àà9 P
(
ààP Q
Int32
ààQ V+
id_md_ref_dispens_ambulatoria
ààW t
,
ààt u
Int32
ààv {-
id_md_dispensacion_ambulatoriaàà| š
)ààš ›
{
áá 	
return
ââ 
DACConsulta
ââ 
.
ââ %
GetAmbulatorioDetalleID
ââ 6
(
ââ6 7+
id_md_ref_dispens_ambulatoria
ââ7 T
,
ââT U,
id_md_dispensacion_ambulatoria
ââV t
)
âât u
;
ââu v
}
ãã 	
public
åå 
List
åå 
<
åå 2
$md_dispensacion_hospitalaria_detalle
åå 8
>
åå8 9&
GetHospitalarioDetalleID
åå: R
(
ååR S
Int32
ååS X,
id_md_ref_dispens_hospitalaria
ååY w
,
ååw x
Int32
ååy ~.
id_md_dispensacion_hospitalariaåå 
)åå Ÿ
{
ææ 	
return
çç 
DACConsulta
çç 
.
çç &
GetHospitalarioDetalleID
çç 7
(
çç7 8,
id_md_ref_dispens_hospitalaria
çç8 V
,
ççV W-
id_md_dispensacion_hospitalaria
ççX w
)
ççw x
;
ççx y
}
èè 	
public
ëë 
void
ëë /
!ActualizarDispersacionAmbulatorio
ëë 5
(
ëë5 61
#md_dispensacion_ambulatoria_detalle
ëë6 Y
OBJMD
ëëZ _
,
ëë_ `
ref
ëëa d 
MessageResponseOBJ
ëëe w
MsgRes
ëëx ~
)
ëë~ 
{
ìì 	
DACActualiza
íí 
.
íí /
!ActualizarDispersacionAmbulatorio
íí :
(
íí: ;
OBJMD
íí; @
,
íí@ A
ref
ííB E
MsgRes
ííF L
)
ííL M
;
ííM N
}
îî 	
public
ğğ 
void
ğğ 3
%ActualizarDispersacionHospitalizacion
ğğ 9
(
ğğ9 :2
$md_dispensacion_hospitalaria_detalle
ğğ: ^
OBJMD
ğğ_ d
,
ğğd e
ref
ğğf i 
MessageResponseOBJ
ğğj |
MsgResğğ} ƒ
)ğğƒ „
{
ññ 	
DACActualiza
òò 
.
òò 3
%ActualizarDispersacionHospitalizacion
òò >
(
òò> ?
OBJMD
òò? D
,
òòD E
ref
òòF I
MsgRes
òòJ P
)
òòP Q
;
òòQ R
}
óó 	
public
õõ 
List
õõ 
<
õõ )
vw_dispensacion_ambulatoria
õõ /
>
õõ/ 0"
AmbulatorioProvvedor
õõ1 E
(
õõE F
String
õõF L

Proveeedor
õõM W
)
õõW X
{
öö 	
return
÷÷ 
DACConsulta
÷÷ 
.
÷÷ "
AmbulatorioProvvedor
÷÷ 3
(
÷÷3 4

Proveeedor
÷÷4 >
)
÷÷> ?
;
÷÷? @
}
øø 	
public
úú 
List
úú 
<
úú *
vw_dispensacion_hospitalaria
úú 0
>
úú0 1#
hospitalarioProvvedor
úú2 G
(
úúG H
String
úúH N

Proveeedor
úúO Y
)
úúY Z
{
ûû 	
return
üü 
DACConsulta
üü 
.
üü #
hospitalarioProvvedor
üü 4
(
üü4 5

Proveeedor
üü5 ?
)
üü? @
;
üü@ A
}
ıı 	
public
ÿÿ 
void
ÿÿ %
ActualizarAmbulatoriaMD
ÿÿ +
(
ÿÿ+ ,)
md_dispensacion_ambulatoria
ÿÿ, G
OBJMD
ÿÿH M
,
ÿÿM N
ref
ÿÿO R 
MessageResponseOBJ
ÿÿS e
MsgRes
ÿÿf l
)
ÿÿl m
{
€€ 	
DACActualiza
 
.
 %
ActualizarAmbulatoriaMD
 0
(
0 1
OBJMD
1 6
,
6 7
ref
8 ;
MsgRes
< B
)
B C
;
C D
}
‚‚ 	
public
„„ 
void
„„ &
ActualizarHospitalariaMD
„„ ,
(
„„, -*
md_dispensacion_hospitalaria
„„- I
OBJMD
„„J O
,
„„O P
ref
„„Q T 
MessageResponseOBJ
„„U g
MsgRes
„„h n
)
„„n o
{
…… 	
DACActualiza
†† 
.
†† &
ActualizarHospitalariaMD
†† 1
(
††1 2
OBJMD
††2 7
,
††7 8
ref
††9 <
MsgRes
††= C
)
††C D
;
††D E
}
‡‡ 	
public
‰‰ 
md_control_gastos
‰‰  
control_gastosMes
‰‰! 2
(
‰‰2 3
Int32
‰‰3 8
mes
‰‰9 <
,
‰‰< =
String
‰‰> D
aÃ±o
‰‰E H
)
‰‰H I
{
ŠŠ 	
return
‹‹ 
DACConsulta
‹‹ 
.
‹‹ 
control_gastosMes
‹‹ 0
(
‹‹0 1
mes
‹‹1 4
,
‹‹4 5
aÃ±o
‹‹6 9
)
‹‹9 :
;
‹‹: ;
}
 	
public
 
Int32
 #
Insertarcontrol_gasto
 *
(
* +
md_control_gastos
+ <

OBJDetalle
= G
,
G H
ref
I L 
MessageResponseOBJ
M _
MsgRes
` f
)
f g
{
 	
return
 

DACInserta
 
.
 #
Insertarcontrol_gasto
 3
(
3 4

OBJDetalle
4 >
,
> ?
ref
@ C
MsgRes
D J
)
J K
;
K L
}
‘‘ 	
public
““ 
void
““ %
ActualizarControlGastos
““ +
(
““+ ,
md_control_gastos
““, =
OBJMD
““> C
,
““C D
ref
““E H 
MessageResponseOBJ
““I [
MsgRes
““\ b
)
““b c
{
”” 	
DACActualiza
•• 
.
•• %
ActualizarControlGastos
•• 0
(
••0 1
OBJMD
••1 6
,
••6 7
ref
••8 ;
MsgRes
••< B
)
••B C
;
••C D
}
–– 	
public
˜˜ !
vw_md_control_gasto
˜˜ "!
control_gastosTotal
˜˜# 6
(
˜˜6 7
Int32
˜˜7 <
mes
˜˜= @
)
˜˜@ A
{
™™ 	
return
šš 
DACConsulta
šš 
.
šš !
control_gastosTotal
šš 2
(
šš2 3
mes
šš3 6
)
šš6 7
;
šš7 8
}
›› 	
public
 
List
 
<
 7
)Managment_md_control_gastos_tableroResult
 =
>
= >"
tableroControlGastos
? S
(
S T
int
T W
opc
X [
,
[ \
int
] `
aÃ±o
a d
)
d e
{
 	
return
ŸŸ 
DACConsulta
ŸŸ 
.
ŸŸ "
tableroControlGastos
ŸŸ 3
(
ŸŸ3 4
opc
ŸŸ4 7
,
ŸŸ7 8
aÃ±o
ŸŸ9 <
)
ŸŸ< =
;
ŸŸ= >
}
   	
public
¢¢ 
List
¢¢ 
<
¢¢ 8
*Managment_md_control_gastos_tablero2Result
¢¢ >
>
¢¢> ?#
tableroControlGastos2
¢¢@ U
(
¢¢U V
int
¢¢V Y
opc
¢¢Z ]
,
¢¢] ^
int
¢¢_ b
aÃ±o
¢¢c f
)
¢¢f g
{
££ 	
return
¤¤ 
DACConsulta
¤¤ 
.
¤¤ #
tableroControlGastos2
¤¤ 4
(
¤¤4 5
opc
¤¤5 8
,
¤¤8 9
aÃ±o
¤¤: =
)
¤¤= >
;
¤¤> ?
}
¥¥ 	
public
©© %
vw_md_total_ambulatoria
©© &&
Total_DetalleAmbulatoria
©©' ?
(
©©? @
Int32
©©@ E,
id_md_dispensacion_ambulatoria
©©F d
)
©©d e
{
ªª 	
return
«« 
DACConsulta
«« 
.
«« &
Total_DetalleAmbulatoria
«« 7
(
««7 8,
id_md_dispensacion_ambulatoria
««8 V
)
««V W
;
««W X
}
¬¬ 	
public
®® &
vw_md_total_hospitalaria
®® ''
Total_DetalleHospitalaria
®®( A
(
®®A B
Int32
®®B G-
id_md_dispensacion_hospitalaria
®®H g
)
®®g h
{
¯¯ 	
return
°° 
DACConsulta
°° 
.
°° '
Total_DetalleHospitalaria
°° 8
(
°°8 9-
id_md_dispensacion_hospitalaria
°°9 X
)
°°X Y
;
°°Y Z
}
±± 	
public
½½ 
int
½½ "
CarguePrefacturaBase
½½ '
(
½½' ((
md_prefacturas_cargue_base
½½( B

carguebase
½½C M
,
½½M N
List
½½O S
<
½½S T$
md_prefacturas_detalle
½½T j
>
½½j k
listaDetalle
½½l x
)
½½x y
{
¾¾ 	
return
¿¿ 

DACInserta
¿¿ 
.
¿¿ "
CarguePrefacturaBase
¿¿ 2
(
¿¿2 3

carguebase
¿¿3 =
,
¿¿= >
listaDetalle
¿¿? K
)
¿¿K L
;
¿¿L M
}
ÀÀ 	
public
ÁÁ 
int
ÁÁ #
CarguePrefacturaLista
ÁÁ (
(
ÁÁ( )
List
ÁÁ) -
<
ÁÁ- .$
md_prefacturas_detalle
ÁÁ. D
>
ÁÁD E
listadoCargue
ÁÁF S
)
ÁÁS T
{
ÂÂ 	
return
ÃÃ 

DACInserta
ÃÃ 
.
ÃÃ #
CarguePrefacturaLista
ÃÃ 3
(
ÃÃ3 4
listadoCargue
ÃÃ4 A
)
ÃÃA B
;
ÃÃB C
}
ÄÄ 	
public
ÇÇ 
int
ÇÇ 
CargueLUPEBase
ÇÇ !
(
ÇÇ! "!
md_Lupe_cargue_base
ÇÇ" 5

carguebase
ÇÇ6 @
,
ÇÇ@ A
List
ÇÇB F
<
ÇÇF G)
md_lupe_cargue_base_detalle
ÇÇG b
>
ÇÇb c
listadoCargue
ÇÇd q
)
ÇÇq r
{
ÈÈ 	
return
ÉÉ 

DACInserta
ÉÉ 
.
ÉÉ 
CargueLUPEBase
ÉÉ ,
(
ÉÉ, -

carguebase
ÉÉ- 7
,
ÉÉ7 8
listadoCargue
ÉÉ9 F
)
ÉÉF G
;
ÉÉG H
}
ÊÊ 	
public
ËË 
int
ËË )
ActualizarVigenciaHastaLupe
ËË .
(
ËË. /!
md_Lupe_cargue_base
ËË/ B
obj
ËËC F
)
ËËF G
{
ÌÌ 	
return
ÍÍ 
DACActualiza
ÍÍ 
.
ÍÍ  )
ActualizarVigenciaHastaLupe
ÍÍ  ;
(
ÍÍ; <
obj
ÍÍ< ?
)
ÍÍ? @
;
ÍÍ@ A
}
ÎÎ 	
public
ĞĞ 
List
ĞĞ 
<
ĞĞ 7
)management_listadoPrefacturasCruzanResult
ĞĞ =
>
ĞĞ= >,
listadoSiCruzanPrefacturasLupe
ĞĞ? ]
(
ĞĞ] ^
int
ĞĞ^ a
idBase
ĞĞb h
)
ĞĞh i
{
ÑÑ 	
return
ÒÒ 
DACConsulta
ÒÒ 
.
ÒÒ ,
listadoSiCruzanPrefacturasLupe
ÒÒ =
(
ÒÒ= >
idBase
ÒÒ> D
)
ÒÒD E
;
ÒÒE F
}
ÓÓ 	
public
ÕÕ 
List
ÕÕ 
<
ÕÕ 9
+management_listadoPrefacturasNoCruzanResult
ÕÕ ?
>
ÕÕ? @,
listadoNoCruzanPrefacturasLupe
ÕÕA _
(
ÕÕ_ `
int
ÕÕ` c
idBase
ÕÕd j
)
ÕÕj k
{
ÖÖ 	
return
×× 
DACConsulta
×× 
.
×× ,
listadoNoCruzanPrefacturasLupe
×× =
(
××= >
idBase
××> D
)
××D E
;
××E F
}
ØØ 	
public
ÚÚ 
List
ÚÚ 
<
ÚÚ +
management_lupe_carguesResult
ÚÚ 1
>
ÚÚ1 2
listadoCargueLupe
ÚÚ3 D
(
ÚÚD E
)
ÚÚE F
{
ÛÛ 	
return
ÜÜ 
DACConsulta
ÜÜ 
.
ÜÜ 
listadoCargueLupe
ÜÜ 0
(
ÜÜ0 1
)
ÜÜ1 2
;
ÜÜ2 3
}
İİ 	
public
ŞŞ 
List
ŞŞ 
<
ŞŞ <
.management_lupe_cargues_intermediacionesResult
ŞŞ B
>
ŞŞB C/
!listadoCargueLupeIntermediaciones
ŞŞD e
(
ŞŞe f
int
ŞŞf i
idLupe
ŞŞj p
)
ŞŞp q
{
ßß 	
return
àà 
DACConsulta
àà 
.
àà /
!listadoCargueLupeIntermediaciones
àà @
(
àà@ A
idLupe
ààA G
)
ààG H
;
ààH I
}
áá 	
public
ââ 
int
ââ 
EliminarLupe
ââ 
(
ââ  
int
ââ  #
idLupe
ââ$ *
,
ââ* +
string
ââ, 2
usuarioElimina
ââ3 A
)
ââA B
{
ãã 	
return
ää 

DACElimina
ää 
.
ää 
EliminarLupe
ää *
(
ää* +
idLupe
ää+ 1
,
ää1 2
usuarioElimina
ää3 A
)
ääA B
;
ääB C
}
åå 	
public
ææ 
int
ææ +
EliminarMedicamentosRegulados
ææ 0
(
ææ0 1
int
ææ1 4
idMed
ææ5 :
,
ææ: ;
string
ææ< B
usuarioElimina
ææC Q
)
ææQ R
{
çç 	
return
èè 

DACElimina
èè 
.
èè +
EliminarMedicamentosRegulados
èè ;
(
èè; <
idMed
èè< A
,
èèA B
usuarioElimina
èèC Q
)
èèQ R
;
èèR S
}
éé 	
public
êê !
md_Lupe_cargue_base
êê "
UltimoCargueLupe
êê# 3
(
êê3 4
int
êê4 7
?
êê7 8
idPrestador
êê9 D
)
êêD E
{
ëë 	
return
ìì 
DACConsulta
ìì 
.
ìì 
UltimoCargueLupe
ìì /
(
ìì/ 0
idPrestador
ìì0 ;
)
ìì; <
;
ìì< =
}
íí 	
public
îî 
int
îî 
CargueLUPELista
îî "
(
îî" #
List
îî# '
<
îî' ()
md_lupe_cargue_base_detalle
îî( C
>
îîC D
listadoCargue
îîE R
,
îîR S
int
îîT W
id_cargueBase
îîX e
)
îîe f
{
ïï 	
return
ğğ 

DACInserta
ğğ 
.
ğğ 
CargueLUPELista
ğğ -
(
ğğ- .
listadoCargue
ğğ. ;
,
ğğ; <
id_cargueBase
ğğ= J
)
ğğJ K
;
ğğK L
}
ññ 	
public
òò 
int
òò )
CargueLUPEListaReaprobacion
òò .
(
òò. /
List
òò/ 3
<
òò3 46
(md_lupe_cargue_base_detalle_reaprobacion
òò4 \
>
òò\ ]
listadoCargue
òò^ k
,
òòk l
int
òòm p
idCargue
òòq y
,
òòy z
int
òò{ ~
idPrefacturaòò ‹
)òò‹ Œ
{
óó 	
return
ôô 

DACInserta
ôô 
.
ôô )
CargueLUPEListaReaprobacion
ôô 9
(
ôô9 :
listadoCargue
ôô: G
,
ôôG H
idCargue
ôôI Q
,
ôôQ R
idPrefactura
ôôS _
)
ôô_ `
;
ôô` a
}
õõ 	
public
öö 
int
öö ,
InsertarReparobacioPrefacturas
öö 1
(
öö1 2
List
öö2 6
<
öö6 7/
!md_prefacturas_reaprobacionMasiva
öö7 X
>
ööX Y
listadoCargue
ööZ g
,
öög h
int
ööi l
idPrefacturaBase
ööm }
)
öö} ~
{
÷÷ 	
return
øø 

DACInserta
øø 
.
øø ,
InsertarReparobacioPrefacturas
øø <
(
øø< =
listadoCargue
øø= J
,
øøJ K
idPrefacturaBase
øøL \
)
øø\ ]
;
øø] ^
}
ùù 	
public
úú 
int
úú .
 InsertarDesaparobacioPrefacturas
úú 3
(
úú3 4
List
úú4 8
<
úú8 90
"md_prefacturas_desaprobacionMasiva
úú9 [
>
úú[ \
listadoCargue
úú] j
,
úúj k
int
úúl o
idPrefacturaBaseúúp €
)úú€ 
{
ûû 	
return
üü 

DACInserta
üü 
.
üü .
 InsertarDesaparobacioPrefacturas
üü >
(
üü> ?
listadoCargue
üü? L
,
üüL M
idPrefacturaBase
üüN ^
)
üü^ _
;
üü_ `
}
ıı 	
public
ÿÿ 
int
ÿÿ -
InsertarCierrePrefacturasMasivo
ÿÿ 2
(
ÿÿ2 3
List
ÿÿ3 7
<
ÿÿ7 8)
md_prefacturas_cierreMasivo
ÿÿ8 S
>
ÿÿS T
listadoCargue
ÿÿU b
,
ÿÿb c
int
ÿÿd g
idPrefacturaBase
ÿÿh x
)
ÿÿx y
{
€€ 	
return
 

DACInserta
 
.
 -
InsertarCierrePrefacturasMasivo
 =
(
= >
listadoCargue
> K
,
K L
idPrefacturaBase
M ]
)
] ^
;
^ _
}
‚‚ 	
public
‰‰ 
List
‰‰ 
<
‰‰ A
3management_prefacturas_notificacionOPLNoPasanResult
‰‰ G
>
‰‰G H5
'ListaDatoaReportePrefacturasaOPLNoPasan
‰‰I p
(
‰‰p q
int
‰‰q t
?
‰‰t u
idCargue
‰‰v ~
)
‰‰~ 
{
ŠŠ 	
return
‹‹ 
DACConsulta
‹‹ 
.
‹‹ 5
'ListaDatoaReportePrefacturasaOPLNoPasan
‹‹ F
(
‹‹F G
idCargue
‹‹G O
)
‹‹O P
;
‹‹P Q
}
ŒŒ 	
public
 
List
 
<
 ?
1management_prefacturas_notificacionOPLPasanResult
 E
>
E F3
%ListaDatoaReportePrefacturasaOPLPasan
G l
(
l m
int
m p
?
p q
idCargue
r z
)
z {
{
 	
return
 
DACConsulta
 
.
 3
%ListaDatoaReportePrefacturasaOPLPasan
 D
(
D E
idCargue
E M
)
M N
;
N O
}
‘‘ 	
public
’’ 
List
’’ 
<
’’ E
7management_prefacturas_listaMedicamentosReguladosResult
’’ K
>
’’K L(
ListaMedicamentosRegulados
’’M g
(
’’g h
)
’’h i
{
““ 	
return
”” 
DACConsulta
”” 
.
”” (
ListaMedicamentosRegulados
”” 9
(
””9 :
)
””: ;
;
””; <
}
•• 	
public
–– 
int
–– *
CargueLupeIntermediacionBase
–– /
(
––/ 0$
md_lupe_intermediacion
––0 F
obj
––G J
,
––J K
int
––L O
idCargueBase
––P \
)
––\ ]
{
—— 	
return
˜˜ 

DACInserta
˜˜ 
.
˜˜ *
CargueLupeIntermediacionBase
˜˜ :
(
˜˜: ;
obj
˜˜; >
,
˜˜> ?
idCargueBase
˜˜@ L
)
˜˜L M
;
˜˜M N
}
™™ 	
public
›› 
int
›› +
CargueLupeIntermediacionLista
›› 0
(
››0 1
List
››1 5
<
››5 6)
md_lupe_intermediacion_dtll
››6 Q
>
››Q R
listadoCargue
››S `
)
››` a
{
œœ 	
return
 

DACInserta
 
.
 +
CargueLupeIntermediacionLista
 ;
(
; <
listadoCargue
< I
)
I J
;
J K
}
ŸŸ 	
public
¡¡ 
void
¡¡ 

CargueLupe
¡¡ 
(
¡¡ !
md_Lupe_cargue_base
¡¡ 2

carguebase
¡¡3 =
,
¡¡= >
List
¡¡? C
<
¡¡C D)
md_lupe_cargue_base_detalle
¡¡D _
>
¡¡_ `
carguedetalle
¡¡a n
,
¡¡n o
List
¡¡p t
<
¡¡t u*
md_lupe_intermediacion_dtll¡¡u 
>¡¡ ‘ 
Intermediaciones¡¡’ ¢
,¡¡¢ £
ref¡¡¤ §"
MessageResponseOBJ¡¡¨ º
MsgRes¡¡» Á
)¡¡Á Â
{
¢¢ 	

DACInserta
££ 
.
££ 

CargueLupe
££ !
(
££! "

carguebase
££" ,
,
££, -
carguedetalle
££. ;
,
££; <
Intermediaciones
££= M
,
££M N
ref
££O R
MsgRes
££S Y
)
££Y Z
;
££Z [
}
¤¤ 	
public
¥¥ 
int
¥¥ )
CargueMedicamentosRegulados
¥¥ .
(
¥¥. /'
md_medicamentos_regulados
¥¥/ H
obj
¥¥I L
,
¥¥L M
List
¥¥N R
<
¥¥R S/
!md_medicamentos_regulados_detalle
¥¥S t
>
¥¥t u
detalle
¥¥v }
,
¥¥} ~
ref¥¥ ‚"
MessageResponseOBJ¥¥ƒ •
MsgRes¥¥– œ
)¥¥œ 
{
¦¦ 	
return
§§ 

DACInserta
§§ 
.
§§ )
CargueMedicamentosRegulados
§§ 9
(
§§9 :
obj
§§: =
,
§§= >
detalle
§§? F
,
§§F G
ref
§§H K
MsgRes
§§L R
)
§§R S
;
§§S T
}
¨¨ 	
public
ªª 
int
ªª )
CargueMedicamentosDatosOPLS
ªª .
(
ªª. /"
md_medicamentos_OPLS
ªª/ C
obj
ªªD G
,
ªªG H
List
ªªI M
<
ªªM N*
md_medicamentos_OPLS_detalle
ªªN j
>
ªªj k
detalle
ªªl s
,
ªªs t
ref
ªªu x!
MessageResponseOBJªªy ‹
MsgResªªŒ ’
)ªª’ “
{
«« 	
return
¬¬ 

DACInserta
¬¬ 
.
¬¬ )
CargueMedicamentosDatosOPLS
¬¬ 9
(
¬¬9 :
obj
¬¬: =
,
¬¬= >
detalle
¬¬? F
,
¬¬F G
ref
¬¬H K
MsgRes
¬¬L R
)
¬¬R S
;
¬¬S T
}
­­ 	
public
¯¯ 
List
¯¯ 
<
¯¯ (
md_prefacturas_cargue_base
¯¯ .
>
¯¯. / 
GetPrefacturasList
¯¯0 B
(
¯¯B C
)
¯¯C D
{
°° 	
return
±± 
DACConsulta
±± 
.
±±  
GetPrefacturasList
±± 1
(
±±1 2
)
±±2 3
;
±±3 4
}
²² 	
public
³³ 
List
³³ 
<
³³ 3
%management_validadorPrefacturasResult
³³ 9
>
³³9 :#
GetPrefacturasListado
³³; P
(
³³P Q
int
³³Q T
?
³³T U
rol
³³V Y
,
³³Y Z
string
³³[ a
usuario
³³b i
)
³³i j
{
´´ 	
return
µµ 
DACConsulta
µµ 
.
µµ #
GetPrefacturasListado
µµ 4
(
µµ4 5
rol
µµ5 8
,
µµ8 9
usuario
µµ: A
)
µµA B
;
µµB C
}
¶¶ 	
public
¸¸ 
List
¸¸ 
<
¸¸ 9
+management_validadorCarguePrefacturasResult
¸¸ ?
>
¸¸? @)
GetPrefacturasListadoCargue
¸¸A \
(
¸¸\ ]
int
¸¸] `
?
¸¸` a
rol
¸¸b e
,
¸¸e f
string
¸¸g m
usuario
¸¸n u
)
¸¸u v
{
¹¹ 	
return
ºº 
DACConsulta
ºº 
.
ºº )
GetPrefacturasListadoCargue
ºº :
(
ºº: ;
rol
ºº; >
,
ºº> ?
usuario
ºº@ G
)
ººG H
;
ººH I
}
»» 	
public
¼¼ 5
'management_prefacturasDatosCargueResult
¼¼ 6%
DatosPrefacturaIdCargue
¼¼7 N
(
¼¼N O
int
¼¼O R
idCargue
¼¼S [
)
¼¼[ \
{
½½ 	
return
¾¾ 
DACConsulta
¾¾ 
.
¾¾ %
DatosPrefacturaIdCargue
¾¾ 6
(
¾¾6 7
idCargue
¾¾7 ?
)
¾¾? @
;
¾¾@ A
}
¿¿ 	
public
ÀÀ 
List
ÀÀ 
<
ÀÀ <
.management_consolidadoInicialPrefacturasResult
ÀÀ B
>
ÀÀB C5
'GetPrefacturasListadoConsolidadoInicial
ÀÀD k
(
ÀÀk l
int
ÀÀl o
?
ÀÀo p
	idUsuario
ÀÀq z
)
ÀÀz {
{
ÁÁ 	
return
ÂÂ 
DACConsulta
ÂÂ 
.
ÂÂ 5
'GetPrefacturasListadoConsolidadoInicial
ÂÂ F
(
ÂÂF G
	idUsuario
ÂÂG P
)
ÂÂP Q
;
ÂÂQ R
}
ÃÃ 	
public
ÅÅ 
int
ÅÅ 0
"CargarLoteMedicamentosDispensacion
ÅÅ 5
(
ÅÅ5 6&
medicamentos_dispen_lote
ÅÅ6 N
obj
ÅÅO R
)
ÅÅR S
{
ÆÆ 	
return
ÇÇ 

DACInserta
ÇÇ 
.
ÇÇ 0
"CargarLoteMedicamentosDispensacion
ÇÇ @
(
ÇÇ@ A
obj
ÇÇA D
)
ÇÇD E
;
ÇÇE F
}
ÈÈ 	
public
ÊÊ 
int
ÊÊ 2
$eliminarLoteMedicamentosDispensacion
ÊÊ 7
(
ÊÊ7 8
int
ÊÊ8 ;
lote
ÊÊ< @
)
ÊÊ@ A
{
ËË 	
return
ÌÌ 

DACElimina
ÌÌ 
.
ÌÌ 2
$eliminarLoteMedicamentosDispensacion
ÌÌ B
(
ÌÌB C
lote
ÌÌC G
)
ÌÌG H
;
ÌÌH I
}
ÍÍ 	
public
ĞĞ (
md_prefacturas_cargue_base
ĞĞ )
GetPrefacturas
ĞĞ* 8
(
ĞĞ8 9
int
ĞĞ9 <
id
ĞĞ= ?
)
ĞĞ? @
{
ÑÑ 	
return
ÒÒ 
DACConsulta
ÒÒ 
.
ÒÒ 
GetPrefacturas
ÒÒ -
(
ÒÒ- .
id
ÒÒ. 0
)
ÒÒ0 1
;
ÒÒ1 2
}
ÓÓ 	
public
ÕÕ .
 log_prefacturas_estadoValidacion
ÕÕ //
!GetLogEstadoValidacionPrefacturas
ÕÕ0 Q
(
ÕÕQ R
int
ÕÕR U
?
ÕÕU V
id
ÕÕW Y
)
ÕÕY Z
{
ÖÖ 	
return
×× 
DACConsulta
×× 
.
×× /
!GetLogEstadoValidacionPrefacturas
×× @
(
××@ A
id
××A C
)
××C D
;
××D E
}
ØØ 	
public
ÚÚ 
List
ÚÚ 
<
ÚÚ .
 log_prefacturas_estadoValidacion
ÚÚ 4
>
ÚÚ4 54
&GetLogEstadoValidacionPrefacturasFases
ÚÚ6 \
(
ÚÚ\ ]
int
ÚÚ] `
?
ÚÚ` a
id
ÚÚb d
,
ÚÚd e
int
ÚÚf i
?
ÚÚi j
fase
ÚÚk o
)
ÚÚo p
{
ÛÛ 	
return
ÜÜ 
DACConsulta
ÜÜ 
.
ÜÜ 4
&GetLogEstadoValidacionPrefacturasFases
ÜÜ E
(
ÜÜE F
id
ÜÜF H
,
ÜÜH I
fase
ÜÜJ N
)
ÜÜN O
;
ÜÜO P
}
İİ 	
public
ŞŞ 2
$log_control_validaciones_prefacturas
ŞŞ 3
GetLogPrefacturas
ŞŞ4 E
(
ŞŞE F
int
ŞŞF I
?
ŞŞI J
idCargue
ŞŞK S
)
ŞŞS T
{
ßß 	
return
àà 
DACConsulta
àà 
.
àà 
GetLogPrefacturas
àà 0
(
àà0 1
idCargue
àà1 9
)
àà9 :
;
àà: ;
}
áá 	
public
ãã 
int
ãã '
ActualizarFasePrefacturas
ãã ,
(
ãã, -
int
ãã- 0
?
ãã0 1

cargueBase
ãã2 <
,
ãã< =
int
ãã> A
?
ããA B
fase
ããC G
)
ããG H
{
ää 	
return
åå 
DACActualiza
åå 
.
åå  '
ActualizarFasePrefacturas
åå  9
(
åå9 :

cargueBase
åå: D
,
ååD E
fase
ååF J
)
ååJ K
;
ååK L
}
ææ 	
public
èè 
int
èè /
!ActualizarEnValidacionPrefacturas
èè 4
(
èè4 5
int
èè5 8
?
èè8 9

cargueBase
èè: D
,
èèD E
int
èèF I
?
èèI J
estado
èèK Q
)
èèQ R
{
éé 	
return
êê 
DACActualiza
êê 
.
êê  /
!ActualizarEnValidacionPrefacturas
êê  A
(
êêA B

cargueBase
êêB L
,
êêL M
estado
êêN T
)
êêT U
;
êêU V
}
ëë 	
public
íí 
int
íí (
LogErroresFasesPrefacturas
íí -
(
íí- .(
log_prefacturas_errorFases
íí. H
obj
ííI L
)
ííL M
{
îî 	
return
ïï 

DACInserta
ïï 
.
ïï (
LogErroresFasesPrefacturas
ïï 8
(
ïï8 9
obj
ïï9 <
)
ïï< =
;
ïï= >
}
ğğ 	
public
òò 
List
òò 
<
òò $
md_prefacturas_detalle
òò *
>
òò* + 
GetPrefacturasById
òò, >
(
òò> ?
string
òò? E
numeroPrefactura
òòF V
)
òòV W
{
óó 	
return
ôô 
DACConsulta
ôô 
.
ôô  
GetPrefacturasById
ôô 1
(
ôô1 2
numeroPrefactura
ôô2 B
)
ôôB C
;
ôôC D
}
õõ 	
public
÷÷ $
md_prefacturas_detalle
÷÷ %
GetPrefacturasID
÷÷& 6
(
÷÷6 7
int
÷÷7 :
?
÷÷: ;
id_detprefactura
÷÷< L
)
÷÷L M
{
øø 	
return
ùù 
DACConsulta
ùù 
.
ùù 
GetPrefacturasID
ùù /
(
ùù/ 0
id_detprefactura
ùù0 @
)
ùù@ A
;
ùùA B
}
úú 	
public
üü 
List
üü 
<
üü /
!ManagmentReportePrefacturasResult
üü 5
>
üü5 6
GetRptPrefacturas
üü7 H
(
üüH I
int
üüI L
idcargue
üüM U
)
üüU V
{
ıı 	
return
şş 
DACConsulta
şş 
.
şş 
GetRptPrefacturas
şş 0
(
şş0 1
idcargue
şş1 9
)
şş9 :
;
şş: ;
}
ÿÿ 	
public
   
void
   "
ActualizarPrefactura
   (
(
  ( )$
md_prefacturas_detalle
  ) ?
obj
  @ C
)
  C D
{
‚ ‚  	
DACActualiza
ƒ ƒ  
.
ƒ ƒ  "
ActualizarPrefactura
ƒ ƒ  -
(
ƒ ƒ - .
obj
ƒ ƒ . 1
)
ƒ ƒ 1 2
;
ƒ ƒ 2 3
}
„ „  	
public
† †  
int
† †  '
ActualizarPrefacturaLista
† †  ,
(
† † , -
List
† † - 1
<
† † 1 2
int
† † 2 5
>
† † 5 6
ListaIds
† † 7 ?
,
† † ? @
string
† † A G
observaciones
† † H U
,
† † U V
double
† † W ]
nuevo_valor
† † ^ i
)
† † i j
{
‡ ‡  	
return
ˆ ˆ  
DACActualiza
ˆ ˆ  
.
ˆ ˆ   '
ActualizarPrefacturaLista
ˆ ˆ   9
(
ˆ ˆ 9 :
ListaIds
ˆ ˆ : B
,
ˆ ˆ B C
observaciones
ˆ ˆ D Q
,
ˆ ˆ Q R
nuevo_valor
ˆ ˆ S ^
)
ˆ ˆ ^ _
;
ˆ ˆ _ `
}
‰ ‰  	
public
Š Š  
int
Š Š  #
DesaprobarPrefacturas
Š Š  (
(
Š Š ( )
List
Š Š ) -
<
Š Š - .
int
Š Š . 1
>
Š Š 1 2
ListaIds
Š Š 3 ;
,
Š Š ; <
string
Š Š = C&
observacionDesaprobacion
Š Š D \
)
Š Š \ ]
{
‹ ‹  	
return
Œ Œ  
DACActualiza
Œ Œ  
.
Œ Œ   #
DesaprobarPrefacturas
Œ Œ   5
(
Œ Œ 5 6
ListaIds
Œ Œ 6 >
,
Œ Œ > ?&
observacionDesaprobacion
Œ Œ @ X
)
Œ Œ X Y
;
Œ Œ Y Z
}
   	
public
   
int
   0
"guardarLogDesaprobacionPrefacturas
   5
(
  5 6
List
  6 :
<
  : ;+
log_prefacturas_desaprobacion
  ; X
>
  X Y
lista
  Z _
)
  _ `
{
   	
return
‘ ‘  

DACInserta
‘ ‘  
.
‘ ‘  0
"guardarLogDesaprobacionPrefacturas
‘ ‘  @
(
‘ ‘ @ A
lista
‘ ‘ A F
)
‘ ‘ F G
;
‘ ‘ G H
}
’ ’  	
public
” ”  
int
” ”  -
guardarLogAprobacionPrefacturas
” ”  2
(
” ” 2 3
List
” ” 3 7
<
” ” 7 8(
log_prefacturas_aprobacion
” ” 8 R
>
” ” R S
lista
” ” T Y
)
” ” Y Z
{
• •  	
return
– –  

DACInserta
– –  
.
– –  -
guardarLogAprobacionPrefacturas
– –  =
(
– – = >
lista
– – > C
)
– – C D
;
– – D E
}
— —  	
public
™ ™  
int
™ ™  (
GuardarLogAprobacionMasiva
™ ™  -
(
™ ™ - ..
 log_prefacturas_aprobacionMasiva
™ ™ . N
obj
™ ™ O R
)
™ ™ R S
{
š š  	
return
› ›  

DACInserta
› ›  
.
› ›  (
GuardarLogAprobacionMasiva
› ›  8
(
› › 8 9
obj
› › 9 <
)
› › < =
;
› › = >
}
œ œ  	
public
   
int
   7
)GuardarLogControl_validacionesPrefacturas
   <
(
  < =2
$log_control_validaciones_prefacturas
  = a
obj
  b e
)
  e f
{
Ÿ Ÿ  	
return
     

DACInserta
     
.
     7
)GuardarLogControl_validacionesPrefacturas
     G
(
    G H
obj
    H K
)
    K L
;
    L M
}
¡ ¡  	
public
¤ ¤  
int
¤ ¤  +
GuardarLogDesaprobacionMasiva
¤ ¤  0
(
¤ ¤ 0 11
#log_prefacturas_desaprobacionMasiva
¤ ¤ 1 T
obj
¤ ¤ U X
)
¤ ¤ X Y
{
¥ ¥  	
return
¦ ¦  

DACInserta
¦ ¦  
.
¦ ¦  +
GuardarLogDesaprobacionMasiva
¦ ¦  ;
(
¦ ¦ ; <
obj
¦ ¦ < ?
)
¦ ¦ ? @
;
¦ ¦ @ A
}
§ §  	
public
© ©  
int
© ©  ,
TraerUltimoCargueLogAprobacion
© ©  1
(
© © 1 2
)
© © 2 3
{
ª ª  	
return
« «  
DACConsulta
« «  
.
« «  ,
TraerUltimoCargueLogAprobacion
« «  =
(
« « = >
)
« « > ?
;
« « ? @
}
¬ ¬  	
public
® ®  
int
® ®  /
!TraerUltimoCargueLogDesaprobacion
® ®  4
(
® ® 4 5
)
® ® 5 6
{
¯ ¯  	
return
° °  
DACConsulta
° °  
.
° °  /
!TraerUltimoCargueLogDesaprobacion
° °  @
(
° ° @ A
)
° ° A B
;
° ° B C
}
± ±  	
public
³ ³  
int
³ ³  -
GuardarLogDatosAprobacionMasiva
³ ³  2
(
³ ³ 2 3
int
³ ³ 3 6
?
³ ³ 6 7
idCargue
³ ³ 8 @
,
³ ³ @ A
int
³ ³ B E
?
³ ³ E F
idLog
³ ³ G L
,
³ ³ L M
string
³ ³ N T
usuarioDigita
³ ³ U b
)
³ ³ b c
{
´ ´  	
return
µ µ  

DACInserta
µ µ  
.
µ µ  -
GuardarLogDatosAprobacionMasiva
µ µ  =
(
µ µ = >
idCargue
µ µ > F
,
µ µ F G
idLog
µ µ H M
,
µ µ M N
usuarioDigita
µ µ O \
)
µ µ \ ]
;
µ µ ] ^
}
¶ ¶  	
public
¸ ¸  
int
¸ ¸  0
"GuardarLogDatosDesaprobacionMasiva
¸ ¸  5
(
¸ ¸ 5 6
int
¸ ¸ 6 9
?
¸ ¸ 9 :
idCargue
¸ ¸ ; C
,
¸ ¸ C D
int
¸ ¸ E H
?
¸ ¸ H I
idLog
¸ ¸ J O
,
¸ ¸ O P
string
¸ ¸ Q W
usuarioDigita
¸ ¸ X e
)
¸ ¸ e f
{
¹ ¹  	
return
º º  

DACInserta
º º  
.
º º  0
"GuardarLogDatosDesaprobacionMasiva
º º  @
(
º º @ A
idCargue
º º A I
,
º º I J
idLog
º º K P
,
º º P Q
usuarioDigita
º º R _
)
º º _ `
;
º º ` a
}
» »  	
public
¾ ¾  
void
¾ ¾  ,
ActualizarPrefacturaListaTotal
¾ ¾  2
(
¾ ¾ 2 3
int
¾ ¾ 3 6
idCargue
¾ ¾ 7 ?
,
¾ ¾ ? @
string
¾ ¾ A G
observaciones
¾ ¾ H U
,
¾ ¾ U V
double
¾ ¾ W ]
nuevo_valor
¾ ¾ ^ i
)
¾ ¾ i j
{
¿ ¿  	
DACActualiza
À À  
.
À À  ,
ActualizarPrefacturaListaTotal
À À  7
(
À À 7 8
idCargue
À À 8 @
,
À À @ A
observaciones
À À B O
,
À À O P
nuevo_valor
À À Q \
)
À À \ ]
;
À À ] ^
}
Á Á  	
public
Ã Ã  
void
Ã Ã  0
"InsertarCargueMasivoDispensaciones
Ã Ã  6
(
Ã Ã 6 7&
dispensacion_cargue_base
Ã Ã 7 O
obj
Ã Ã P S
,
Ã Ã S T
List
Ã Ã U Y
<
Ã Ã Y Z+
dispensacion_cargue_base_dtll
Ã Ã Z w
>
Ã Ã w x
lista
Ã Ã y ~
,
Ã Ã ~ 
refÃ Ã € ƒ"
MessageResponseOBJÃ Ã „ –
MsgResÃ Ã — 
)Ã Ã  
{
Ä Ä  	

DACInserta
Å Å  
.
Å Å  0
"InsertarCargueMasivoDispensaciones
Å Å  9
(
Å Å 9 :
obj
Å Å : =
,
Å Å = >
lista
Å Å ? D
,
Å Å D E
ref
Å Å F I
MsgRes
Å Å J P
)
Å Å P Q
;
Å Å Q R
}
Æ Æ  	
public
É É  
List
É É  
<
É É  /
!ManagmentocargueconsolidadoResult
É É  5
>
É É 5 6!
CuentaConsolidadoMD
É É 7 J
(
É É J K
String
É É K Q
numero_factura
É É R `
,
É É ` a
String
É É b h
numero_formula
É É i w
,
É É w x
DateTimeÉ É y 
fecha_inicialÉ É ‚ 
,É É  
DateTimeÉ É ‘ ™
fecha_finalÉ É š ¥
,É É ¥ ¦
refÉ É § ª"
MessageResponseOBJÉ É « ½
MsgResÉ É ¾ Ä
)É É Ä Å
{
Ê Ê  	
return
Ë Ë  
DACConsulta
Ë Ë  
.
Ë Ë  !
CuentaConsolidadoMD
Ë Ë  2
(
Ë Ë 2 3
numero_factura
Ë Ë 3 A
,
Ë Ë A B
numero_formula
Ë Ë C Q
,
Ë Ë Q R
fecha_inicial
Ë Ë S `
,
Ë Ë ` a
fecha_final
Ë Ë b m
,
Ë Ë m n
ref
Ë Ë o r
MsgRes
Ë Ë s y
)
Ë Ë y z
;
Ë Ë z {
}
Ì Ì  	
public
Î Î  
Int32
Î Î  +
InsertarFFMMGlosaConciliacion
Î Î  2
(
Î Î 2 3#
md_glosa_conciliacion
Î Î 3 H
OBJ
Î Î I L
,
Î Î L M
ref
Î Î N Q 
MessageResponseOBJ
Î Î R d
MsgRes
Î Î e k
)
Î Î k l
{
Ï Ï  	
return
Ğ Ğ  

DACInserta
Ğ Ğ  
.
Ğ Ğ  +
InsertarFFMMGlosaConciliacion
Ğ Ğ  ;
(
Ğ Ğ ; <
OBJ
Ğ Ğ < ?
,
Ğ Ğ ? @
ref
Ğ Ğ A D
MsgRes
Ğ Ğ E K
)
Ğ Ğ K L
;
Ğ Ğ L M
}
Ñ Ñ  	
public
Ó Ó  &
vw_md_glosa_conciliacion
Ó Ó  '!
ConsultaGlosaDtllId
Ó Ó ( ;
(
Ó Ó ; <
Int32
Ó Ó < A!
id_md_glosa_detalle
Ó Ó B U
)
Ó Ó U V
{
Ô Ô  	
return
Õ Õ  
DACConsulta
Õ Õ  
.
Õ Õ  !
ConsultaGlosaDtllId
Õ Õ  2
(
Õ Õ 2 3!
id_md_glosa_detalle
Õ Õ 3 F
)
Õ Õ F G
;
Õ Õ G H
}
Ö Ö  	
public
Ø Ø  
int
Ø Ø  ,
GuardarMedicamentosFacturacion
Ø Ø  1
(
Ø Ø 1 2&
medicamentos_facturacion
Ø Ø 2 J
Obj
Ø Ø K N
,
Ø Ø N O
List
Ø Ø P T
<
Ø Ø T U+
medicamentos_facturacion_dtll
Ø Ø U r
>
Ø Ø r s
Result
Ø Ø t z
,
Ø Ø z {
ref
Ø Ø | "
MessageResponseOBJØ Ø € ’
MsgResØ Ø “ ™
)Ø Ø ™ š
{
Ù Ù  	
return
Ú Ú  

DACInserta
Ú Ú  
.
Ú Ú  ,
GuardarMedicamentosFacturacion
Ú Ú  <
(
Ú Ú < =
Obj
Ú Ú = @
,
Ú Ú @ A
Result
Ú Ú B H
,
Ú Ú H I
ref
Ú Ú J M
MsgRes
Ú Ú N T
)
Ú Ú T U
;
Ú Ú U V
}
Û Û  	
public
Ş Ş  
List
Ş Ş  
<
Ş Ş  5
'ManagementMedicamentosFacturacionResult
Ş Ş  ;
>
Ş Ş ; <"
GetListMdFacturacion
Ş Ş = Q
(
Ş Ş Q R
)
Ş Ş R S
{
ß ß  	
return
à à  
DACConsulta
à à  
.
à à  "
GetListMdFacturacion
à à  3
(
à à 3 4
)
à à 4 5
;
à à 5 6
}
á á  	
public
ã ã  
List
ã ã  
<
ã ã  8
*managemente_medicamentos_facturacionResult
ã ã  >
>
ã ã > ?)
Getmedicamentos_facturacion
ã ã @ [
(
ã ã [ \
int
ã ã \ _
mes
ã ã ` c
,
ã ã c d
int
ã ã e h
aÃ±o
ã ã i l
,
ã ã l m
int
ã ã n q
regional
ã ã r z
)
ã ã z {
{
ä ä  	
return
å å  
DACConsulta
å å  
.
å å  )
Getmedicamentos_facturacion
å å  :
(
å å : ;
mes
å å ; >
,
å å > ?
aÃ±o
å å @ C
,
å å C D
regional
å å E M
)
å å M N
;
å å N O
}
æ æ  	
public
è è  
List
è è  
<
è è  5
'ManagementMedicamentosFacturacionResult
è è  ;
>
è è ; <,
GetMedicamentosFacturacionList
è è = [
(
è è [ \
int
è è \ _
?
è è _ `
	mesinicio
è è a j
,
è è j k
int
è è l o
?
è è o p

aÃ±oinicio
è è q z
,
è è z {
int
è è | 
?è è  €
mesfinalè è  ‰
,è è ‰ Š
intè è ‹ 
?è è  
aÃ±ofinè è  –
,è è – —
stringè è ˜ 
	Prestadorè è Ÿ ¨
,è è ¨ ©
stringè è ª °
regionalè è ± ¹
)è è ¹ º
{
é é  	
return
ê ê  
DACConsulta
ê ê  
.
ê ê  ,
GetMedicamentosFacturacionList
ê ê  =
(
ê ê = >
	mesinicio
ê ê > G
,
ê ê G H

aÃ±oinicio
ê ê I R
,
ê ê R S
mesfinal
ê ê T \
,
ê ê \ ]
aÃ±ofin
ê ê ^ d
,
ê ê d e
	Prestador
ê ê f o
,
ê ê o p
regional
ê ê q y
)
ê ê y z
;
ê ê z {
}
ë ë  	
public
î î  
List
î î  
<
î î  6
(Managment_ReportePrefacturas_totalResult
î î  <
>
î î < =!
GetPrefacturasTotal
î î > Q
(
î î Q R
)
î î R S
{
ï ï  	
return
ğ ğ  
DACConsulta
ğ ğ  
.
ğ ğ  !
GetPrefacturasTotal
ğ ğ  2
(
ğ ğ 2 3
)
ğ ğ 3 4
;
ğ ğ 4 5
}
ñ ñ  	
public
ò ò  
List
ò ò  
<
ò ò  @
2Managment_ReportePrefacturas_cerrar_abiertasResult
ò ò  F
>
ò ò F G*
GetPrefacturasCerrarAbiertas
ò ò H d
(
ò ò d e
)
ò ò e f
{
ó ó  	
return
ô ô  
DACConsulta
ô ô  
.
ô ô  *
GetPrefacturasCerrarAbiertas
ô ô  ;
(
ô ô ; <
)
ô ô < =
;
ô ô = >
}
õ õ  	
public
÷ ÷  
List
÷ ÷  
<
÷ ÷  ?
1management_prefacturas_consolidado_abiertasResult
÷ ÷  E
>
÷ ÷ E F$
GetPrefacturasAbiertas
÷ ÷ G ]
(
÷ ÷ ] ^
int
÷ ÷ ^ a
?
÷ ÷ a b

cargueBase
÷ ÷ c m
)
÷ ÷ m n
{
ø ø  	
return
ù ù  
DACConsulta
ù ù  
.
ù ù  $
GetPrefacturasAbiertas
ù ù  5
(
ù ù 5 6

cargueBase
ù ù 6 @
)
ù ù @ A
;
ù ù A B
}
ú ú  	
public
û û  
List
û û  
<
û û  ?
1management_prefacturas_consolidado_cerradasResult
û û  E
>
û û E F$
GetPrefacturasCerradas
û û G ]
(
û û ] ^
int
û û ^ a
?
û û a b

cargueBase
û û c m
)
û û m n
{
ü ü  	
return
ı ı  
DACConsulta
ı ı  
.
ı ı  $
GetPrefacturasCerradas
ı ı  5
(
ı ı 5 6

cargueBase
ı ı 6 @
)
ı ı @ A
;
ı ı A B
}
ş ş  	
public
ÿ ÿ  
List
ÿ ÿ  
<
ÿ ÿ  @
2Managment_ReportePrefacturas_cerrar_cerradasResult
ÿ ÿ  F
>
ÿ ÿ F G*
GetPrefacturasCerrarCerradas
ÿ ÿ H d
(
ÿ ÿ d e
)
ÿ ÿ e f
{
€!€! 	
return
!! 
DACConsulta
!! 
.
!! *
GetPrefacturasCerrarCerradas
!! ;
(
!!; <
)
!!< =
;
!!= >
}
‚!‚! 	
public
„!„! 
int
„!„! (
ActualizarPrefacturaCerrar
„!„! -
(
„!„!- .$
md_prefacturas_detalle
„!„!. D
obj
„!„!E H
)
„!„!H I
{
…!…! 	
return
†!†! 
DACActualiza
†!†! 
.
†!†!  (
ActualizarPrefacturaCerrar
†!†!  :
(
†!†!: ;
obj
†!†!; >
)
†!†!> ?
;
†!†!? @
}
‡!‡! 	
public
‰!‰! 
int
‰!‰! &
GuardarPrefacturaCerrada
‰!‰! +
(
‰!‰!+ ,,
md_prefacturas_cargue_cerradas
‰!‰!, J
obj
‰!‰!K N
)
‰!‰!N O
{
Š!Š! 	
return
‹!‹! 

DACInserta
‹!‹! 
.
‹!‹! &
GuardarPrefacturaCerrada
‹!‹! 6
(
‹!‹!6 7
obj
‹!‹!7 :
)
‹!‹!: ;
;
‹!‹!; <
}
Œ!Œ! 	
public
!! 
void
!! &
EliminarCarguePrefactura
!! ,
(
!!, -
int
!!- 0
idCargue
!!1 9
,
!!9 :
ref
!!; > 
MessageResponseOBJ
!!? Q
MsgRes
!!R X
)
!!X Y
{
!! 	

DACElimina
!! 
.
!! &
EliminarCarguePrefactura
!! /
(
!!/ 0
idCargue
!!0 8
,
!!8 9
ref
!!: =
MsgRes
!!> D
)
!!D E
;
!!E F
}
‘!‘! 	
public
“!“! 
void
“!“!  
EliminarCargueLUPE
“!“! &
(
“!“!& '
int
“!“!' *
idCargue
“!“!+ 3
,
“!“!3 4
ref
“!“!5 8 
MessageResponseOBJ
“!“!9 K
MsgRes
“!“!L R
)
“!“!R S
{
”!”! 	

DACElimina
•!•! 
.
•!•!  
EliminarCargueLUPE
•!•! )
(
•!•!) *
idCargue
•!•!* 2
,
•!•!2 3
ref
•!•!4 7
MsgRes
•!•!8 >
)
•!•!> ?
;
•!•!? @
}
–!–! 	
public
˜!˜! 
void
˜!˜! +
EliminarMedicamentosRegulados
˜!˜! 1
(
˜!˜!1 2
int
˜!˜!2 5
idCargue
˜!˜!6 >
,
˜!˜!> ?
ref
˜!˜!@ C 
MessageResponseOBJ
˜!˜!D V
MsgRes
˜!˜!W ]
)
˜!˜!] ^
{
™!™! 	

DACElimina
š!š! 
.
š!š! +
EliminarMedicamentosRegulados
š!š! 4
(
š!š!4 5
idCargue
š!š!5 =
,
š!š!= >
ref
š!š!? B
MsgRes
š!š!C I
)
š!š!I J
;
š!š!J K
}
›!›! 	
public
!! 
List
!! 
<
!! (
md_prefacturas_cargue_base
!! .
>
!!. /*
ConsultaExistenciaPrefactura
!!0 L
(
!!L M
int
!!M P
regional
!!Q Y
,
!!Y Z
string
!![ a
numPrefactura
!!b o
,
!!o p
int
!!q t
idPrestador!!u €
)!!€ 
{
Ÿ!Ÿ! 	
return
 ! ! 
DACConsulta
 ! ! 
.
 ! ! *
ConsultaExistenciaPrefactura
 ! ! ;
(
 ! !; <
regional
 ! !< D
,
 ! !D E
numPrefactura
 ! !F S
,
 ! !S T
idPrestador
 ! !U `
)
 ! !` a
;
 ! !a b
}
¡!¡! 	
public
£!£! 
List
£!£! 
<
£!£! !
ref_referencia_pago
£!£! '
>
£!£!' (#
GetReferenciaPagoList
£!£!) >
(
£!£!> ?
)
£!£!? @
{
¤!¤! 	
return
¥!¥! 
DACConsulta
¥!¥! 
.
¥!¥! #
GetReferenciaPagoList
¥!¥! 4
(
¥!¥!4 5
)
¥!¥!5 6
;
¥!¥!6 7
}
¦!¦! 	
public
­!­! 
List
­!­! 
<
­!­!  
indicador_regional
­!­! &
>
­!­!& ',
ConsultarIndicadorRegionalList
­!­!( F
(
­!­!F G
ref
­!­!G J 
MessageResponseOBJ
­!­!K ]
MsgRes
­!­!^ d
)
­!­!d e
{
®!®! 	
return
¯!¯! 
DACConsulta
¯!¯! 
.
¯!¯! ,
ConsultarIndicadorRegionalList
¯!¯! =
(
¯!¯!= >
ref
¯!¯!> A
MsgRes
¯!¯!B H
)
¯!¯!H I
;
¯!¯!I J
}
±!±! 	
public
³!³! 
List
³!³! 
<
³!³! 

vw_visitas
³!³! 
>
³!³! '
ConsultaCronogramaVisitas
³!³!  9
(
³!³!9 :
Int32
³!³!: ?
?
³!³!? @
idcronograma
³!³!A M
,
³!³!M N
ref
³!³!O R 
MessageResponseOBJ
³!³!S e
MsgRta
³!³!f l
)
³!³!l m
{
´!´! 	
return
µ!µ! 
DACConsulta
µ!µ! 
.
µ!µ! '
ConsultaCronogramaVisitas
µ!µ! 8
(
µ!µ!8 9
idcronograma
µ!µ!9 E
,
µ!µ!E F
ref
µ!µ!G J
MsgRta
µ!µ!K Q
)
µ!µ!Q R
;
µ!µ!R S
}
¶!¶! 	
public
¸!¸! 
List
¸!¸! 
<
¸!¸! :
,Management_Consulta_Cronograma_VisitasResult
¸!¸! @
>
¸!¸!@ A4
&ConsultaCronogramaVisitasProcedimiento
¸!¸!B h
(
¸!¸!h i
int
¸!¸!i l

tipoFiltro
¸!¸!m w
,
¸!¸!w x
string
¸!¸!y 
Auditor¸!¸!€ ‡
)¸!¸!‡ ˆ
{
¹!¹! 	
return
º!º! 
DACConsulta
º!º! 
.
º!º! 4
&ConsultaCronogramaVisitasProcedimiento
º!º! E
(
º!º!E F

tipoFiltro
º!º!F P
,
º!º!P Q
Auditor
º!º!R Y
)
º!º!Y Z
;
º!º!Z [
}
»!»! 	
public
½!½! 
List
½!½! 
<
½!½! '
cronograma_visita_detalle
½!½! -
>
½!½!- .-
ConsultaCronogramaVisitaDetalle
½!½!/ N
(
½!½!N O
int
½!½!O R
idcronograma
½!½!S _
)
½!½!_ `
{
¾!¾! 	
return
¿!¿! 
DACConsulta
¿!¿! 
.
¿!¿! -
ConsultaCronogramaVisitaDetalle
¿!¿! >
(
¿!¿!> ?
idcronograma
¿!¿!? K
)
¿!¿!K L
;
¿!¿!L M
}
À!À! 	
public
Â!Â! 
List
Â!Â! 
<
Â!Â! 1
#cronograma_visita_detalle_indicador
Â!Â! 7
>
Â!Â!7 80
"ConsultaCronogramaVisitaDetalleInd
Â!Â!9 [
(
Â!Â![ \
int
Â!Â!\ _
idcronograma
Â!Â!` l
)
Â!Â!l m
{
Ã!Ã! 	
return
Ä!Ä! 
DACConsulta
Ä!Ä! 
.
Ä!Ä! 0
"ConsultaCronogramaVisitaDetalleInd
Ä!Ä! A
(
Ä!Ä!A B
idcronograma
Ä!Ä!B N
)
Ä!Ä!N O
;
Ä!Ä!O P
}
Å!Å! 	
public
Ç!Ç!  
cronograma_visitas
Ç!Ç! !
getvisitabyid
Ç!Ç!" /
(
Ç!Ç!/ 0
Int32
Ç!Ç!0 5
idvisita
Ç!Ç!6 >
,
Ç!Ç!> ?
ref
Ç!Ç!@ C 
MessageResponseOBJ
Ç!Ç!D V
MsgRta
Ç!Ç!W ]
)
Ç!Ç!] ^
{
È!È! 	
return
É!É! 
DACConsulta
É!É! 
.
É!É! 
getvisitabyid
É!É! ,
(
É!É!, -
idvisita
É!É!- 5
,
É!É!5 6
ref
É!É!7 :
MsgRta
É!É!; A
)
É!É!A B
;
É!É!B C
}
Ê!Ê! 	
public
Ì!Ì! 
void
Ì!Ì! '
InsertarCronogramaVisitas
Ì!Ì! -
(
Ì!Ì!- . 
cronograma_visitas
Ì!Ì!. @
objcronograma
Ì!Ì!A N
,
Ì!Ì!N O
ref
Ì!Ì!P S 
MessageResponseOBJ
Ì!Ì!T f
MsgRes
Ì!Ì!g m
)
Ì!Ì!m n
{
Í!Í! 	

DACInserta
Î!Î! 
.
Î!Î! '
InsertarCronogramaVisitas
Î!Î! 0
(
Î!Î!0 1
objcronograma
Î!Î!1 >
,
Î!Î!> ?
ref
Î!Î!@ C
MsgRes
Î!Î!D J
)
Î!Î!J K
;
Î!Î!K L
}
Ï!Ï! 	
public
Ñ!Ñ! 
void
Ñ!Ñ! )
ActualizarCronogramaVisitas
Ñ!Ñ! /
(
Ñ!Ñ!/ 0 
cronograma_visitas
Ñ!Ñ!0 B
objcronograma
Ñ!Ñ!C P
,
Ñ!Ñ!P Q
ref
Ñ!Ñ!R U 
MessageResponseOBJ
Ñ!Ñ!V h
MsgRes
Ñ!Ñ!i o
)
Ñ!Ñ!o p
{
Ò!Ò! 	
DACActualiza
Ó!Ó! 
.
Ó!Ó! )
ActualizarCronogramaVisitas
Ó!Ó! 4
(
Ó!Ó!4 5
objcronograma
Ó!Ó!5 B
,
Ó!Ó!B C
ref
Ó!Ó!D G
MsgRes
Ó!Ó!H N
)
Ó!Ó!N O
;
Ó!Ó!O P
}
Ô!Ô! 	
public
Ö!Ö! 
void
Ö!Ö! #
insertardetallevisita
Ö!Ö! )
(
Ö!Ö!) *
int
Ö!Ö!* -
id_cronograma
Ö!Ö!. ;
,
Ö!Ö!; <
int
Ö!Ö!= @
id_regional
Ö!Ö!A L
,
Ö!Ö!L M
int
Ö!Ö!N Q
id_indicador
Ö!Ö!R ^
,
Ö!Ö!^ _
List
Ö!Ö!` d
<
Ö!Ö!d e
item_capitulo
Ö!Ö!e r
>
Ö!Ö!r s
listadoitemsÖ!Ö!t €
,Ö!Ö!€ 
refÖ!Ö!‚ …"
MessageResponseOBJÖ!Ö!† ˜
MsgResÖ!Ö!™ Ÿ
)Ö!Ö!Ÿ  
{
×!×! 	

DACInserta
Ø!Ø! 
.
Ø!Ø! #
insertardetallevisita
Ø!Ø! ,
(
Ø!Ø!, -
id_cronograma
Ø!Ø!- :
,
Ø!Ø!: ;
id_regional
Ø!Ø!< G
,
Ø!Ø!G H
id_indicador
Ø!Ø!I U
,
Ø!Ø!U V
listadoitems
Ø!Ø!W c
,
Ø!Ø!c d
ref
Ø!Ø!e h
MsgRes
Ø!Ø!i o
)
Ø!Ø!o p
;
Ø!Ø!p q
}
Ù!Ù! 	
public
Û!Û! 
void
Û!Û! *
insertarcalificacionesvisita
Û!Û! 0
(
Û!Û!0 1
int
Û!Û!1 4
idcronograma
Û!Û!5 A
,
Û!Û!A B
string
Û!Û!C I
[
Û!Û!I J
]
Û!Û!J K
calificaciones
Û!Û!L Z
,
Û!Û!Z [
ref
Û!Û!\ _ 
MessageResponseOBJ
Û!Û!` r
MsgRes
Û!Û!s y
)
Û!Û!y z
{
Ü!Ü! 	

DACInserta
İ!İ! 
.
İ!İ! *
insertarcalificacionesvisita
İ!İ! 3
(
İ!İ!3 4
idcronograma
İ!İ!4 @
,
İ!İ!@ A
calificaciones
İ!İ!B P
,
İ!İ!P Q
ref
İ!İ!R U
MsgRes
İ!İ!V \
)
İ!İ!\ ]
;
İ!İ!] ^
}
Ş!Ş! 	
public
à!à! 
int
à!à! ,
insertardetallevisitaindicador
à!à! 1
(
à!à!1 2
List
à!à!2 6
<
à!à!6 7 
capitulo_indicador
à!à!7 I
>
à!à!I J
	capitulos
à!à!K T
,
à!à!T U
int
à!à!V Y
idcronograma
à!à!Z f
,
à!à!f g
int
à!à!h k
idprestador
à!à!l w
,
à!à!w x
ref
à!à!y |!
MessageResponseOBJà!à!} 
MsgResà!à! –
)à!à!– —
{
á!á! 	
return
â!â! 

DACInserta
â!â! 
.
â!â! ,
insertardetallevisitaindicador
â!â! <
(
â!â!< =
	capitulos
â!â!= F
,
â!â!F G
idcronograma
â!â!H T
,
â!â!T U
idprestador
â!â!V a
,
â!â!a b
ref
â!â!c f
MsgRes
â!â!g m
)
â!â!m n
;
â!â!n o
}
ã!ã! 	
public
å!å! 
List
å!å! 
<
å!å! 
	capitulos
å!å! 
>
å!å! 
GetCapitulosList
å!å! /
(
å!å!/ 0
)
å!å!0 1
{
æ!æ! 	
return
ç!ç! 
DACConsulta
ç!ç! 
.
ç!ç! 
GetCapitulosList
ç!ç! /
(
ç!ç!/ 0
)
ç!ç!0 1
;
ç!ç!1 2
}
è!è! 	
public
ê!ê! 
List
ê!ê! 
<
ê!ê!  
capitulo_indicador
ê!ê! &
>
ê!ê!& '%
GetCapitulosByIndicador
ê!ê!( ?
(
ê!ê!? @
Int32
ê!ê!@ E
?
ê!ê!E F
idindicador
ê!ê!G R
,
ê!ê!R S
Int32
ê!ê!T Y

idregioanl
ê!ê!Z d
,
ê!ê!d e
ref
ê!ê!f i 
MessageResponseOBJ
ê!ê!j |
MsgResê!ê!} ƒ
)ê!ê!ƒ „
{
ë!ë! 	
return
ì!ì! 
DACConsulta
ì!ì! 
.
ì!ì! %
GetCapitulosByIndicador
ì!ì! 6
(
ì!ì!6 7
idindicador
ì!ì!7 B
,
ì!ì!B C

idregioanl
ì!ì!D N
,
ì!ì!N O
ref
ì!ì!P S
MsgRes
ì!ì!T Z
)
ì!ì!Z [
;
ì!ì![ \
}
í!í! 	
public
ï!ï! 
List
ï!ï! 
<
ï!ï! 2
$ManagementCalidadDtllIndicadorResult
ï!ï! 8
>
ï!ï!8 9.
 GetCapitulosEvaluadosByIndicador
ï!ï!: Z
(
ï!ï!Z [
Int32
ï!ï![ `
?
ï!ï!` a
idindicador
ï!ï!b m
,
ï!ï!m n
Int32
ï!ï!o t

idregioanl
ï!ï!u 
,ï!ï! €
refï!ï! „"
MessageResponseOBJï!ï!… —
MsgResï!ï!˜ 
)ï!ï! Ÿ
{
ğ!ğ! 	
return
ñ!ñ! 
DACConsulta
ñ!ñ! 
.
ñ!ñ! .
 GetCapitulosEvaluadosByIndicador
ñ!ñ! ?
(
ñ!ñ!? @
idindicador
ñ!ñ!@ K
,
ñ!ñ!K L

idregioanl
ñ!ñ!M W
,
ñ!ñ!W X
ref
ñ!ñ!Y \
MsgRes
ñ!ñ!] c
)
ñ!ñ!c d
;
ñ!ñ!d e
}
ò!ò! 	
public
ô!ô!  
capitulo_indicador
ô!ô! !"
getcapbyindicadorcap
ô!ô!" 6
(
ô!ô!6 7
int
ô!ô!7 :

idcapitulo
ô!ô!; E
,
ô!ô!E F
int
ô!ô!G J
idindicador
ô!ô!K V
,
ô!ô!V W
int
ô!ô!X [

idregional
ô!ô!\ f
)
ô!ô!f g
{
õ!õ! 	
return
ö!ö! 
DACConsulta
ö!ö! 
.
ö!ö! "
getcapbyindicadorcap
ö!ö! 3
(
ö!ö!3 4

idcapitulo
ö!ö!4 >
,
ö!ö!> ?
idindicador
ö!ö!@ K
,
ö!ö!K L

idregional
ö!ö!M W
)
ö!ö!W X
;
ö!ö!X Y
}
÷!÷! 	
public
ù!ù! 
List
ù!ù! 
<
ù!ù! 
indicadores
ù!ù! 
>
ù!ù!  "
ConsultarIndicadores
ù!ù!! 5
(
ù!ù!5 6
int
ù!ù!6 9
?
ù!ù!9 :
idindicador
ù!ù!; F
,
ù!ù!F G
ref
ù!ù!H K 
MessageResponseOBJ
ù!ù!L ^
MegRes
ù!ù!_ e
)
ù!ù!e f
{
ú!ú! 	
return
û!û! 
DACConsulta
û!û! 
.
û!û! "
ConsultarIndicadores
û!û! 3
(
û!û!3 4
idindicador
û!û!4 ?
,
û!û!? @
ref
û!û!A D
MegRes
û!û!E K
)
û!û!K L
;
û!û!L M
}
ü!ü! 	
public
ş!ş! 
item_capitulo
ş!ş! 
Getitemcapbyid
ş!ş! +
(
ş!ş!+ ,
Int32
ş!ş!, 1
IdItem
ş!ş!2 8
)
ş!ş!8 9
{
ÿ!ÿ! 	
return
€"€" 
DACConsulta
€"€" 
.
€"€" 
Getitemcapbyid
€"€" -
(
€"€"- .
IdItem
€"€". 4
)
€"€"4 5
;
€"€"5 6
}
"" 	
public
ƒ"ƒ" 
List
ƒ"ƒ" 
<
ƒ"ƒ" 
item_capitulo
ƒ"ƒ" !
>
ƒ"ƒ"! " 
Getitemcapbyindcap
ƒ"ƒ"# 5
(
ƒ"ƒ"5 6
Int32
ƒ"ƒ"6 ;

idregional
ƒ"ƒ"< F
,
ƒ"ƒ"F G
Int32
ƒ"ƒ"H M
idindicador
ƒ"ƒ"N Y
,
ƒ"ƒ"Y Z
Int32
ƒ"ƒ"[ `
?
ƒ"ƒ"` a
idcap
ƒ"ƒ"b g
)
ƒ"ƒ"g h
{
„"„" 	
return
…"…" 
DACConsulta
…"…" 
.
…"…"  
Getitemcapbyindcap
…"…" 1
(
…"…"1 2

idregional
…"…"2 <
,
…"…"< =
idindicador
…"…"> I
,
…"…"I J
idcap
…"…"K P
)
…"…"P Q
;
…"…"Q R
}
†"†" 	
public
ˆ"ˆ" 
List
ˆ"ˆ" 
<
ˆ"ˆ" '
cronograma_visita_detalle
ˆ"ˆ" -
>
ˆ"ˆ"- .$
Getdetalllescronograma
ˆ"ˆ"/ E
(
ˆ"ˆ"E F
int
ˆ"ˆ"F I
idcronograma
ˆ"ˆ"J V
)
ˆ"ˆ"V W
{
‰"‰" 	
return
Š"Š" 
DACConsulta
Š"Š" 
.
Š"Š" $
Getdetalllescronograma
Š"Š" 5
(
Š"Š"5 6
idcronograma
Š"Š"6 B
)
Š"Š"B C
;
Š"Š"C D
}
‹"‹" 	
public
"" 
bool
"" $
ActualizarItemCapitulo
"" *
(
""* +
item_capitulo
""+ 8
objitem
""9 @
)
""@ A
{
"" 	
return
"" 
DACActualiza
"" 
.
""  $
ActualizarItemCapitulo
""  6
(
""6 7
objitem
""7 >
)
""> ?
;
""? @
}
"" 	
public
’"’" 
	capitulos
’"’" 
Getcapitulobyid
’"’" (
(
’"’"( )
Int32
’"’") .

idcapitulo
’"’"/ 9
)
’"’"9 :
{
“"“" 	
return
”"”" 
DACConsulta
”"”" 
.
”"”" 
Getcapitulobyid
”"”" .
(
”"”". /

idcapitulo
”"”"/ 9
)
”"”"9 :
;
”"”": ;
}
•"•" 	
public
—"—" 
bool
—"—" "
InsertarItemCapitulo
—"—" (
(
—"—"( )
item_capitulo
—"—") 6
itemcapitulo
—"—"7 C
)
—"—"C D
{
˜"˜" 	
return
™"™" 

DACInserta
™"™" 
.
™"™" "
InsertarItemCapitulo
™"™" 2
(
™"™"2 3
itemcapitulo
™"™"3 ?
)
™"™"? @
;
™"™"@ A
}
š"š" 	
public
œ"œ" 
bool
œ"œ" 
InsertaCapitulo
œ"œ" #
(
œ"œ"# $
	capitulos
œ"œ"$ -
capitulo
œ"œ". 6
)
œ"œ"6 7
{
"" 	
return
"" 

DACInserta
"" 
.
"" 
InsertaCapitulo
"" -
(
""- .
capitulo
"". 6
)
""6 7
;
""7 8
}
Ÿ"Ÿ" 	
public
¡"¡" 
bool
¡"¡" )
ActualizarCapituloIndicador
¡"¡" /
(
¡"¡"/ 0
Int32
¡"¡"0 5!
idcapituloIndicador
¡"¡"6 I
,
¡"¡"I J
int
¡"¡"K N
pesogen
¡"¡"O V
)
¡"¡"V W
{
¢"¢" 	
return
£"£" 
DACActualiza
£"£" 
.
£"£"  )
ActualizarCapituloIndicador
£"£"  ;
(
£"£"; <!
idcapituloIndicador
£"£"< O
,
£"£"O P
pesogen
£"£"Q X
)
£"£"X Y
;
£"£"Y Z
}
¤"¤" 	
public
¦"¦" "
Ref_RIPS_Prestadores
¦"¦" #
getPrestador
¦"¦"$ 0
(
¦"¦"0 1
double
¦"¦"1 7
codprestador
¦"¦"8 D
,
¦"¦"D E
ref
¦"¦"F I 
MessageResponseOBJ
¦"¦"J \
MsgRes
¦"¦"] c
)
¦"¦"c d
{
§"§" 	
return
¨"¨" 
DACConsulta
¨"¨" 
.
¨"¨" 
getPrestador
¨"¨" +
(
¨"¨"+ ,
codprestador
¨"¨", 8
,
¨"¨"8 9
ref
¨"¨": =
MsgRes
¨"¨"> D
)
¨"¨"D E
;
¨"¨"E F
}
©"©" 	
public
«"«" 
List
«"«" 
<
«"«" %
ref_usuario_responsable
«"«" +
>
«"«"+ ,*
ConsultResponsablesbyusuario
«"«"- I
(
«"«"I J
Int32
«"«"J O
	idusuario
«"«"P Y
,
«"«"Y Z
ref
«"«"[ ^ 
MessageResponseOBJ
«"«"_ q
MsgRes
«"«"r x
)
«"«"x y
{
¬"¬" 	
return
­"­" 
DACConsulta
­"­" 
.
­"­" *
ConsultResponsablesbyusuario
­"­" ;
(
­"­"; <
	idusuario
­"­"< E
,
­"­"E F
ref
­"­"G J
MsgRes
­"­"K Q
)
­"­"Q R
;
­"­"R S
}
®"®" 	
public
°"°" 
List
°"°" 
<
°"°" 
sis_usuario
°"°" 
>
°"°"  
LstResponsables
°"°"! 0
(
°"°"0 1
)
°"°"1 2
{
±"±" 	
return
²"²" 
DACConsulta
²"²" 
.
²"²" 
LstResponsables
²"²" .
(
²"²". /
)
²"²"/ 0
;
²"²"0 1
}
³"³" 	
public
µ"µ" 
List
µ"µ" 
<
µ"µ" !
calidad_prestadores
µ"µ" '
>
µ"µ"' ( 
getprestadoresList
µ"µ") ;
(
µ"µ"; <
)
µ"µ"< =
{
¶"¶" 	
return
·"·" 
DACConsulta
·"·" 
.
·"·"  
getprestadoresList
·"·" 1
(
·"·"1 2
)
·"·"2 3
;
·"·"3 4
}
¸"¸" 	
public
º"º" !
calidad_prestadores
º"º" "
getPresadorById
º"º"# 2
(
º"º"2 3
int
º"º"3 6
idprestador
º"º"7 B
)
º"º"B C
{
»"»" 	
return
¼"¼" 
DACConsulta
¼"¼" 
.
¼"¼" 
getPresadorById
¼"¼" .
(
¼"¼". /
idprestador
¼"¼"/ :
)
¼"¼": ;
;
¼"¼"; <
}
½"½" 	
public
¿"¿" 
List
¿"¿" 
<
¿"¿" &
calidad_ref_especialidad
¿"¿" ,
>
¿"¿", -$
GetRefEspecialidadList
¿"¿". D
(
¿"¿"D E
)
¿"¿"E F
{
À"À" 	
return
Á"Á" 
DACConsulta
Á"Á" 
.
Á"Á" $
GetRefEspecialidadList
Á"Á" 5
(
Á"Á"5 6
)
Á"Á"6 7
;
Á"Á"7 8
}
Â"Â" 	
public
Ä"Ä" 
List
Ä"Ä" 
<
Ä"Ä" !
calidad_ref_regimen
Ä"Ä" '
>
Ä"Ä"' ( 
GetRefRegimentList
Ä"Ä") ;
(
Ä"Ä"; <
)
Ä"Ä"< =
{
Å"Å" 	
return
Æ"Æ" 
DACConsulta
Æ"Æ" 
.
Æ"Æ"  
GetRefRegimentList
Æ"Æ" 1
(
Æ"Æ"1 2
)
Æ"Æ"2 3
;
Æ"Æ"3 4
}
Ç"Ç" 	
public
É"É" 
List
É"É" 
<
É"É" 
Ref_clase_persona
É"É" %
>
É"É"% &!
GetClasePersonaList
É"É"' :
(
É"É": ;
)
É"É"; <
{
Ê"Ê" 	
return
Ë"Ë" 
DACConsulta
Ë"Ë" 
.
Ë"Ë" !
GetClasePersonaList
Ë"Ë" 2
(
Ë"Ë"2 3
)
Ë"Ë"3 4
;
Ë"Ë"4 5
}
Ì"Ì" 	
public
Î"Î" 
List
Î"Î" 
<
Î"Î" ,
vw_calidad_prestador_indicador
Î"Î" 2
>
Î"Î"2 3)
GetListIndicadoresPrestador
Î"Î"4 O
(
Î"Î"O P
int
Î"Î"P S
id_prestador
Î"Î"T `
)
Î"Î"` a
{
Ï"Ï" 	
return
Ğ"Ğ" 
DACConsulta
Ğ"Ğ" 
.
Ğ"Ğ" )
GetListIndicadoresPrestador
Ğ"Ğ" :
(
Ğ"Ğ": ;
id_prestador
Ğ"Ğ"; G
)
Ğ"Ğ"G H
;
Ğ"Ğ"H I
}
Ñ"Ñ" 	
public
Ó"Ó" 
void
Ó"Ó" 
InsertarPrestador
Ó"Ó" %
(
Ó"Ó"% &!
calidad_prestadores
Ó"Ó"& 9
Obj
Ó"Ó": =
,
Ó"Ó"= >
ref
Ó"Ó"? B 
MessageResponseOBJ
Ó"Ó"C U
MsgRes
Ó"Ó"V \
)
Ó"Ó"\ ]
{
Ô"Ô" 	

DACInserta
Õ"Õ" 
.
Õ"Õ" 
InsertarPrestador
Õ"Õ" (
(
Õ"Õ"( )
Obj
Õ"Õ") ,
,
Õ"Õ", -
ref
Õ"Õ". 1
MsgRes
Õ"Õ"2 8
)
Õ"Õ"8 9
;
Õ"Õ"9 :
}
Ö"Ö" 	
public
Ø"Ø" 
void
Ø"Ø" 
InsertarVisita
Ø"Ø" "
(
Ø"Ø"" # 
cronograma_visitas
Ø"Ø"# 5
ObjCronocrama
Ø"Ø"6 C
,
Ø"Ø"C D
ref
Ø"Ø"E H 
MessageResponseOBJ
Ø"Ø"I [
MsgRes
Ø"Ø"\ b
)
Ø"Ø"b c
{
Ù"Ù" 	

DACInserta
Ú"Ú" 
.
Ú"Ú" 
InsertarVisita
Ú"Ú" %
(
Ú"Ú"% &
ObjCronocrama
Ú"Ú"& 3
,
Ú"Ú"3 4
ref
Ú"Ú"5 8
MsgRes
Ú"Ú"9 ?
)
Ú"Ú"? @
;
Ú"Ú"@ A
}
Û"Û" 	
public
İ"İ" 
void
İ"İ" (
insertaRegionalPrestadores
İ"İ" .
(
İ"İ". /
Int32
İ"İ"/ 4

idregional
İ"İ"5 ?
,
İ"İ"? @
List
İ"İ"A E
<
İ"İ"E F
int
İ"İ"F I
>
İ"İ"I J
prestadores
İ"İ"K V
)
İ"İ"V W
{
Ş"Ş" 	

DACInserta
ß"ß" 
.
ß"ß" (
insertaRegionalPrestadores
ß"ß" 1
(
ß"ß"1 2

idregional
ß"ß"2 <
,
ß"ß"< =
prestadores
ß"ß"> I
)
ß"ß"I J
;
ß"ß"J K
}
à"à" 	
public
â"â" 
void
â"â" (
InsertarCapitulosPrestador
â"â" .
(
â"â". /
Int32
â"â"/ 4

idregional
â"â"5 ?
,
â"â"? @
Int32
â"â"A F
idindicador
â"â"G R
,
â"â"R S
List
â"â"T X
<
â"â"X Y
int
â"â"Y \
>
â"â"\ ]
	capitulos
â"â"^ g
)
â"â"g h
{
ã"ã" 	

DACInserta
ä"ä" 
.
ä"ä" (
InsertarCapitulosPrestador
ä"ä" 1
(
ä"ä"1 2

idregional
ä"ä"2 <
,
ä"ä"< =
idindicador
ä"ä"> I
,
ä"ä"I J
	capitulos
ä"ä"K T
)
ä"ä"T U
;
ä"ä"U V
}
å"å" 	
public
ç"ç" 
void
ç"ç" 
EliminarCapitulo
ç"ç" $
(
ç"ç"$ %
int
ç"ç"% (

idcapitulo
ç"ç") 3
)
ç"ç"3 4
{
è"è" 	

DACElimina
é"é" 
.
é"é" 
EliminarCapitulo
é"é" '
(
é"é"' (

idcapitulo
é"é"( 2
)
é"é"2 3
;
é"é"3 4
}
ê"ê" 	
public
ì"ì" 
void
ì"ì" 
EliminarVisita
ì"ì" "
(
ì"ì"" #
Int32
ì"ì"# (
idvisita
ì"ì") 1
,
ì"ì"1 2%
log_eliminacion_visitas
ì"ì"3 J
obj
ì"ì"K N
,
ì"ì"N O
ref
ì"ì"P S 
MessageResponseOBJ
ì"ì"T f
MsgRes
ì"ì"g m
)
ì"ì"m n
{
í"í" 	

DACElimina
î"î" 
.
î"î" 
EliminarVisita
î"î" %
(
î"î"% &
idvisita
î"î"& .
,
î"î". /
obj
î"î"0 3
,
î"î"3 4
ref
î"î"5 8
MsgRes
î"î"9 ?
)
î"î"? @
;
î"î"@ A
}
ï"ï" 	
public
ñ"ñ" 
void
ñ"ñ" &
EliminarEvaluacionVisita
ñ"ñ" ,
(
ñ"ñ", -
Int32
ñ"ñ"- 2
idvisita
ñ"ñ"3 ;
,
ñ"ñ"; <%
log_eliminacion_visitas
ñ"ñ"= T
obj
ñ"ñ"U X
,
ñ"ñ"X Y
ref
ñ"ñ"Z ] 
MessageResponseOBJ
ñ"ñ"^ p
MsgRes
ñ"ñ"q w
)
ñ"ñ"w x
{
ò"ò" 	

DACElimina
ó"ó" 
.
ó"ó" &
EliminarEvaluacionVisita
ó"ó" /
(
ó"ó"/ 0
idvisita
ó"ó"0 8
,
ó"ó"8 9
obj
ó"ó": =
,
ó"ó"= >
ref
ó"ó"? B
MsgRes
ó"ó"C I
)
ó"ó"I J
;
ó"ó"J K
}
ô"ô" 	
public
ö"ö" 
void
ö"ö" 
EliminarEgreso
ö"ö" "
(
ö"ö"" #
Int32
ö"ö"# (
id
ö"ö") +
,
ö"ö"+ ,
ref
ö"ö"- 0 
MessageResponseOBJ
ö"ö"1 C
MsgRes
ö"ö"D J
)
ö"ö"J K
{
÷"÷" 	

DACElimina
ø"ø" 
.
ø"ø" 
EliminarEgreso
ø"ø" %
(
ø"ø"% &
id
ø"ø"& (
,
ø"ø"( )
ref
ø"ø"* -
MsgRes
ø"ø". 4
)
ø"ø"4 5
;
ø"ø"5 6
}
ù"ù" 	
public
û"û" 
Int32
û"û" #
InsertarCargueRanking
û"û" *
(
û"û"* +$
calidad_cargue_ranking
û"û"+ A
ranking
û"û"B I
)
û"û"I J
{
ü"ü" 	
return
ı"ı" 

DACInserta
ı"ı" 
.
ı"ı" #
InsertarCargueRanking
ı"ı" 3
(
ı"ı"3 4
ranking
ı"ı"4 ;
)
ı"ı"; <
;
ı"ı"< =
}
ş"ş" 	
public
€#€# 
void
€#€# *
InsertarDetalleCargueRanking
€#€# 0
(
€#€#0 1
List
€#€#1 5
<
€#€#5 6,
calidad_detalle_cargue_ranking
€#€#6 T
>
€#€#T U
detalleranking
€#€#V d
)
€#€#d e
{
## 	

DACInserta
‚#‚# 
.
‚#‚# *
InsertarDetalleCargueRanking
‚#‚# 3
(
‚#‚#3 4
detalleranking
‚#‚#4 B
)
‚#‚#B C
;
‚#‚#C D
}
ƒ#ƒ# 	
public
…#…# 
void
…#…# #
InsertarNovedadVisita
…#…# )
(
…#…#) *)
cronograma_visita_novedades
…#…#* E
obj
…#…#F I
,
…#…#I J
ref
…#…#K N 
MessageResponseOBJ
…#…#O a
MsgRes
…#…#b h
)
…#…#h i
{
†#†# 	

DACInserta
‡#‡# 
.
‡#‡# #
InsertarNovedadVisita
‡#‡# ,
(
‡#‡#, -
obj
‡#‡#- 0
,
‡#‡#0 1
ref
‡#‡#2 5
MsgRes
‡#‡#6 <
)
‡#‡#< =
;
‡#‡#= >
}
ˆ#ˆ# 	
public
Š#Š# 
void
Š#Š# :
,InsertarMasivamenteReportesEvaluacionVisitas
Š#Š# @
(
Š#Š#@ A
List
Š#Š#A E
<
Š#Š#E F?
1cronograma_visitas_reportesDoc_evaluacion_calidad
Š#Š#F w
>
Š#Š#w x
obj
Š#Š#y |
,
Š#Š#| }
refŠ#Š#~ "
MessageResponseOBJŠ#Š#‚ ”
MsgResŠ#Š#• ›
)Š#Š#› œ
{
‹#‹# 	

DACInserta
Œ#Œ# 
.
Œ#Œ# :
,InsertarMasivamenteReportesEvaluacionVisitas
Œ#Œ# C
(
Œ#Œ#C D
obj
Œ#Œ#D G
,
Œ#Œ#G H
ref
Œ#Œ#I L
MsgRes
Œ#Œ#M S
)
Œ#Œ#S T
;
Œ#Œ#T U
}
## 	
public
## 5
'Management_get_info_visita_por_idResult
## 6
GetInfoVisitaById
##7 H
(
##H I
int
##I L
idCronograma
##M Y
)
##Y Z
{
## 	
return
‘#‘# 
DACConsulta
‘#‘# 
.
‘#‘# 
GetInfoVisitaById
‘#‘# 0
(
‘#‘#0 1
idCronograma
‘#‘#1 =
)
‘#‘#= >
;
‘#‘#> ?
}
’#’# 	
public
”#”# 
void
”#”# !
actualizarPrestador
”#”# '
(
”#”#' (!
calidad_prestadores
”#”#( ;
Obj
”#”#< ?
,
”#”#? @
int
”#”#A D
idprestador
”#”#E P
)
”#”#P Q
{
•#•# 	
DACActualiza
–#–# 
.
–#–# !
actualizarPrestador
–#–# ,
(
–#–#, -
Obj
–#–#- 0
,
–#–#0 1
idprestador
–#–#2 =
)
–#–#= >
;
–#–#> ?
}
—#—# 	
public
™#™# 
List
™#™# 
<
™#™# $
Ref_calidad_municipios
™#™# *
>
™#™#* +
GetMunicipiosDane
™#™#, =
(
™#™#= >
)
™#™#> ?
{
š#š# 	
return
›#›# 
DACConsulta
›#›# 
.
›#›# 
GetMunicipiosDane
›#›# 0
(
›#›#0 1
)
›#›#1 2
;
›#›#2 3
}
œ#œ# 	
public
## 
List
## 
<
## 

vw_visitas
## 
>
## 
GetListVisitas
##  .
(
##. /
Int32
##/ 4
?
##4 5
	id_visita
##6 ?
,
##? @
Int32
##A F
?
##F G
id_prestador
##H T
,
##T U
string
##V \
numcontrato
##] h
)
##h i
{
Ÿ#Ÿ# 	
return
 # # 
DACConsulta
 # # 
.
 # # 
GetListVisitas
 # # -
(
 # #- .
	id_visita
 # #. 7
,
 # #7 8
id_prestador
 # #9 E
,
 # #E F
numcontrato
 # #G R
)
 # #R S
;
 # #S T
}
¡#¡# 	
public
£#£# 
List
£#£# 
<
£#£# .
 ref_cronograma_visitas_novedades
£#£# 4
>
£#£#4 5 
GetListTipoNovedad
£#£#6 H
(
£#£#H I
)
£#£#I J
{
¤#¤# 	
return
¥#¥# 
DACConsulta
¥#¥# 
.
¥#¥#  
GetListTipoNovedad
¥#¥# 1
(
¥#¥#1 2
)
¥#¥#2 3
;
¥#¥#3 4
}
¦#¦# 	
public
¨#¨# 
void
¨#¨#  
GuardarActaVisitas
¨#¨# &
(
¨#¨#& ')
cronograma_visita_documento
¨#¨#' B
obj
¨#¨#C F
,
¨#¨#F G
ref
¨#¨#H K 
MessageResponseOBJ
¨#¨#L ^
MsgRes
¨#¨#_ e
)
¨#¨#e f
{
©#©# 	

DACInserta
ª#ª# 
.
ª#ª#  
GuardarActaVisitas
ª#ª# )
(
ª#ª#) *
obj
ª#ª#* -
,
ª#ª#- .
ref
ª#ª#/ 2
MsgRes
ª#ª#3 9
)
ª#ª#9 :
;
ª#ª#: ;
}
«#«# 	
public
´#´# )
cronograma_visita_documento
´#´# *
Getactavisita
´#´#+ 8
(
´#´#8 9
int
´#´#9 <
idvisita
´#´#= E
)
´#´#E F
{
µ#µ# 	
return
¶#¶# 
DACConsulta
¶#¶# 
.
¶#¶# 
Getactavisita
¶#¶# ,
(
¶#¶#, -
idvisita
¶#¶#- 5
)
¶#¶#5 6
;
¶#¶#6 7
}
·#·# 	
public
¹#¹# =
/management_cronograma_visita_documento_idResult
¹#¹# >
Getactavisita2
¹#¹#? M
(
¹#¹#M N
int
¹#¹#N Q
idvisita
¹#¹#R Z
)
¹#¹#Z [
{
º#º# 	
return
»#»# 
DACConsulta
»#»# 
.
»#»# 
Getactavisita2
»#»# -
(
»#»#- .
idvisita
»#»#. 6
)
»#»#6 7
;
»#»#7 8
}
¼#¼# 	
public
¾#¾# 
List
¾#¾# 
<
¾#¾# B
4management_cronograma_visita_documento_sinRutaResult
¾#¾# H
>
¾#¾#H I"
GetactavisitaSinRuta
¾#¾#J ^
(
¾#¾#^ _
)
¾#¾#_ `
{
¿#¿# 	
return
À#À# 
DACConsulta
À#À# 
.
À#À# "
GetactavisitaSinRuta
À#À# 3
(
À#À#3 4
)
À#À#4 5
;
À#À#5 6
}
Á#Á# 	
public
Ã#Ã# 
List
Ã#Ã# 
<
Ã#Ã# #
vw_visitas_documentos
Ã#Ã# )
>
Ã#Ã#) *
GetActasVisitas
Ã#Ã#+ :
(
Ã#Ã#: ;
int
Ã#Ã#; >
regional
Ã#Ã#? G
,
Ã#Ã#G H
int
Ã#Ã#I L
mes
Ã#Ã#M P
,
Ã#Ã#P Q
int
Ã#Ã#R U
aÃ±o
Ã#Ã#V Y
)
Ã#Ã#Y Z
{
Ä#Ä# 	
return
Å#Å# 
DACConsulta
Å#Å# 
.
Å#Å# 
GetActasVisitas
Å#Å# .
(
Å#Å#. /
regional
Å#Å#/ 7
,
Å#Å#7 8
mes
Å#Å#9 <
,
Å#Å#< =
aÃ±o
Å#Å#> A
)
Å#Å#A B
;
Å#Å#B C
}
Æ#Æ# 	
public
È#È# 
List
È#È# 
<
È#È# 2
$ManagementConsultaGnralVisitasResult
È#È# 8
>
È#È#8 9'
GetConsultageneralVisitas
È#È#: S
(
È#È#S T
int
È#È#T W
regional
È#È#X `
,
È#È#` a
int
È#È#b e
	prestador
È#È#f o
,
È#È#o p
DateTime
È#È#q y
fecha
È#È#z 
,È#È# €
stringÈ#È# ‡
nitÈ#È#ˆ ‹
,È#È#‹ Œ
stringÈ#È# “
codsapÈ#È#” š
)È#È#š ›
{
É#É# 	
return
Ê#Ê# 
DACConsulta
Ê#Ê# 
.
Ê#Ê# '
GetConsultageneralVisitas
Ê#Ê# 8
(
Ê#Ê#8 9
regional
Ê#Ê#9 A
,
Ê#Ê#A B
	prestador
Ê#Ê#C L
,
Ê#Ê#L M
fecha
Ê#Ê#N S
,
Ê#Ê#S T
nit
Ê#Ê#U X
,
Ê#Ê#X Y
codsap
Ê#Ê#Z `
)
Ê#Ê#` a
;
Ê#Ê#a b
}
Ë#Ë# 	
public
Í#Í# '
cronograma_visita_detalle
Í#Í# ()
Getresultadovisitaindicador
Í#Í#) D
(
Í#Í#D E
int
Í#Í#E H
idvisita
Í#Í#I Q
,
Í#Í#Q R
int
Í#Í#S V
idindicador
Í#Í#W b
)
Í#Í#b c
{
Î#Î# 	
return
Ï#Ï# 
DACConsulta
Ï#Ï# 
.
Ï#Ï# )
Getresultadovisitaindicador
Ï#Ï# :
(
Ï#Ï#: ;
idvisita
Ï#Ï#; C
,
Ï#Ï#C D
idindicador
Ï#Ï#E P
)
Ï#Ï#P Q
;
Ï#Ï#Q R
}
Ğ#Ğ# 	
public
Ò#Ò# 
List
Ò#Ò# 
<
Ò#Ò# /
!cronograma_visitas_calificaciones
Ò#Ò# 5
>
Ò#Ò#5 6%
GetCalificacionesVisita
Ò#Ò#7 N
(
Ò#Ò#N O
int
Ò#Ò#O R
id_cronograma
Ò#Ò#S `
)
Ò#Ò#` a
{
Ó#Ó# 	
return
Ô#Ô# 
DACConsulta
Ô#Ô# 
.
Ô#Ô# %
GetCalificacionesVisita
Ô#Ô# 6
(
Ô#Ô#6 7
id_cronograma
Ô#Ô#7 D
)
Ô#Ô#D E
;
Ô#Ô#E F
}
Õ#Õ# 	
public
Û#Û# 
List
Û#Û# 
<
Û#Û# )
analisis_caso_salud_publica
Û#Û# /
>
Û#Û#/ 0-
ConsultaEvolucionAnalisisSaludP
Û#Û#1 P
(
Û#Û#P Q
Int32
Û#Û#Q V
idconcurrencia
Û#Û#W e
,
Û#Û#e f
Int32
Û#Û#g l
?
Û#Û#l m

IdAnalisis
Û#Û#n x
,
Û#Û#x y
ref
Û#Û#z }!
MessageResponseOBJÛ#Û#~ 
MsgResÛ#Û#‘ —
)Û#Û#— ˜
{
Ü#Ü# 	
return
İ#İ# 
DACConsulta
İ#İ# 
.
İ#İ# -
ConsultaEvolucionAnalisisSaludP
İ#İ# >
(
İ#İ#> ?
idconcurrencia
İ#İ#? M
,
İ#İ#M N

IdAnalisis
İ#İ#O Y
,
İ#İ#Y Z
ref
İ#İ#[ ^
MsgRes
İ#İ#_ e
)
İ#İ#e f
;
İ#İ#f g
}
Ş#Ş# 	
public
à#à# 
List
à#à# 
<
à#à# #
analisis_caso_alertas
à#à# )
>
à#à#) *)
ConsultaAnalisisCasoAlertas
à#à#+ F
(
à#à#F G
Int32
à#à#G L
?
à#à#L M
idconcurrencia
à#à#N \
,
à#à#\ ]
Int32
à#à#^ c
?
à#à#c d

IdAnalisis
à#à#e o
,
à#à#o p
ref
à#à#q t!
MessageResponseOBJà#à#u ‡
MsgResà#à#ˆ 
)à#à# 
{
á#á# 	
return
â#â# 
DACConsulta
â#â# 
.
â#â# )
ConsultaAnalisisCasoAlertas
â#â# :
(
â#â#: ;
idconcurrencia
â#â#; I
,
â#â#I J

IdAnalisis
â#â#K U
,
â#â#U V
ref
â#â#W Z
MsgRes
â#â#[ a
)
â#â#a b
;
â#â#b c
}
ã#ã# 	
public
å#å# 
List
å#å# 
<
å#å# $
analisis_caso_muestreo
å#å# *
>
å#å#* +*
ConsultaAnalisisCasoMuestreo
å#å#, H
(
å#å#H I
Int32
å#å#I N
?
å#å#N O
idconcurrencia
å#å#P ^
,
å#å#^ _
Int32
å#å#` e
?
å#å#e f

IdAnalisis
å#å#g q
,
å#å#q r
ref
å#å#s v!
MessageResponseOBJå#å#w ‰
MsgReså#å#Š 
)å#å# ‘
{
æ#æ# 	
return
ç#ç# 
DACConsulta
ç#ç# 
.
ç#ç# *
ConsultaAnalisisCasoMuestreo
ç#ç# ;
(
ç#ç#; <
idconcurrencia
ç#ç#< J
,
ç#ç#J K

IdAnalisis
ç#ç#L V
,
ç#ç#V W
ref
ç#ç#X [
MsgRes
ç#ç#\ b
)
ç#ç#b c
;
ç#ç#c d
}
è#è# 	
public
ê#ê# 
List
ê#ê# 
<
ê#ê# 1
#ecop_concurrencia_eventos_en_asalud
ê#ê# 7
>
ê#ê#7 8,
ConsultaAnalisisEventosenSalud
ê#ê#9 W
(
ê#ê#W X
Int32
ê#ê#X ]
idconcurrencia
ê#ê#^ l
,
ê#ê#l m
Int32
ê#ê#n s
?
ê#ê#s t

IdAnalisis
ê#ê#u 
,ê#ê# €
refê#ê# „"
MessageResponseOBJê#ê#… —
MsgResê#ê#˜ 
)ê#ê# Ÿ
{
ë#ë# 	
return
ì#ì# 
DACConsulta
ì#ì# 
.
ì#ì# ,
ConsultaAnalisisEventosenSalud
ì#ì# =
(
ì#ì#= >
idconcurrencia
ì#ì#> L
,
ì#ì#L M

IdAnalisis
ì#ì#N X
,
ì#ì#X Y
ref
ì#ì#Z ]
MsgRes
ì#ì#^ d
)
ì#ì#d e
;
ì#ì#e f
}
í#í# 	
public
ï#ï# 
List
ï#ï# 
<
ï#ï# 8
*ecop_concurrencia_eventos_en_salud_detalle
ï#ï# >
>
ï#ï#> ?3
%ConsultaAnalisisEventosenSaludDetalle
ï#ï#@ e
(
ï#ï#e f9
*ecop_concurrencia_eventos_en_salud_detalleï#ï#f 
OBJï#ï#‘ ”
,ï#ï#” •
refï#ï#– ™"
MessageResponseOBJï#ï#š ¬
MsgResï#ï#­ ³
)ï#ï#³ ´
{
ğ#ğ# 	
return
ñ#ñ# 
DACConsulta
ñ#ñ# 
.
ñ#ñ# 3
%ConsultaAnalisisEventosenSaludDetalle
ñ#ñ# D
(
ñ#ñ#D E
OBJ
ñ#ñ#E H
,
ñ#ñ#H I
ref
ñ#ñ#J M
MsgRes
ñ#ñ#N T
)
ñ#ñ#T U
;
ñ#ñ#U V
}
ò#ò# 	
public
ô#ô# 
List
ô#ô# 
<
ô#ô# 8
*ecop_concurrencia_eventos_en_salud_detalle
ô#ô# >
>
ô#ô#> ?.
 GetAnalisisEventosenSaludDetalle
ô#ô#@ `
(
ô#ô#` a
int
ô#ô#a d

idanalisis
ô#ô#e o
)
ô#ô#o p
{
õ#õ# 	
return
ö#ö# 
DACConsulta
ö#ö# 
.
ö#ö# .
 GetAnalisisEventosenSaludDetalle
ö#ö# ?
(
ö#ö#? @

idanalisis
ö#ö#@ J
)
ö#ö#J K
;
ö#ö#K L
}
÷#÷# 	
public
ù#ù# 
int
ù#ù# #
InsertarAnalisisCasos
ù#ù# (
(
ù#ù#( )$
analisis_caso_original
ù#ù#) ?
Analisis
ù#ù#@ H
,
ù#ù#H I
ref
ù#ù#J M 
MessageResponseOBJ
ù#ù#N `
MsgRes
ù#ù#a g
)
ù#ù#g h
{
ú#ú# 	
return
û#û# 

DACInserta
û#û# 
.
û#û# #
InsertarAnalisisCasos
û#û# 3
(
û#û#3 4
Analisis
û#û#4 <
,
û#û#< =
ref
û#û#> A
MsgRes
û#û#B H
)
û#û#H I
;
û#û#I J
}
ü#ü# 	
public
ş#ş# 
int
ş#ş# &
InsertarAnalisisMuestreo
ş#ş# +
(
ş#ş#+ ,$
analisis_caso_muestreo
ş#ş#, B
AnalisisMuestreo
ş#ş#C S
,
ş#ş#S T
ref
ş#ş#U X 
MessageResponseOBJ
ş#ş#Y k
MsgRes
ş#ş#l r
)
ş#ş#r s
{
ÿ#ÿ# 	
return
€$€$ 

DACInserta
€$€$ 
.
€$€$ &
InsertarAnalisisMuestreo
€$€$ 6
(
€$€$6 7
AnalisisMuestreo
€$€$7 G
,
€$€$G H
ref
€$€$I L
MsgRes
€$€$M S
)
€$€$S T
;
€$€$T U
}
$$ 	
public
ƒ$ƒ$ 
int
ƒ$ƒ$ )
InsertarAnalisisCasosAlerta
ƒ$ƒ$ .
(
ƒ$ƒ$. /#
analisis_caso_alertas
ƒ$ƒ$/ D
AnalisisAlerta
ƒ$ƒ$E S
,
ƒ$ƒ$S T
ref
ƒ$ƒ$U X 
MessageResponseOBJ
ƒ$ƒ$Y k
MsgRes
ƒ$ƒ$l r
)
ƒ$ƒ$r s
{
„$„$ 	
return
…$…$ 

DACInserta
…$…$ 
.
…$…$ )
InsertarAnalisisCasosAlerta
…$…$ 9
(
…$…$9 :
AnalisisAlerta
…$…$: H
,
…$…$H I
ref
…$…$J M
MsgRes
…$…$N T
)
…$…$T U
;
…$…$U V
}
†$†$ 	
public
ˆ$ˆ$ 
void
ˆ$ˆ$ %
ActualizarAnalisisCasos
ˆ$ˆ$ +
(
ˆ$ˆ$+ ,$
analisis_caso_original
ˆ$ˆ$, B
Analisis
ˆ$ˆ$C K
,
ˆ$ˆ$K L
ref
ˆ$ˆ$M P 
MessageResponseOBJ
ˆ$ˆ$Q c
MsgRes
ˆ$ˆ$d j
)
ˆ$ˆ$j k
{
‰$‰$ 	
DACActualiza
Š$Š$ 
.
Š$Š$ %
ActualizarAnalisisCasos
Š$Š$ 0
(
Š$Š$0 1
Analisis
Š$Š$1 9
,
Š$Š$9 :
ref
Š$Š$; >
MsgRes
Š$Š$? E
)
Š$Š$E F
;
Š$Š$F G
}
‹$‹$ 	
public
$$ 
void
$$ (
ActualizarAnalisisMuestreo
$$ .
(
$$. /$
analisis_caso_muestreo
$$/ E
	AnalisisM
$$F O
,
$$O P
ref
$$Q T 
MessageResponseOBJ
$$U g
MsgRes
$$h n
)
$$n o
{
$$ 	
DACActualiza
$$ 
.
$$ (
ActualizarAnalisisMuestreo
$$ 3
(
$$3 4
	AnalisisM
$$4 =
,
$$= >
ref
$$? B
MsgRes
$$C I
)
$$I J
;
$$J K
}
$$ 	
public
’$’$ 
void
’$’$ '
ActualizarAnalisisAlertas
’$’$ -
(
’$’$- .#
analisis_caso_alertas
’$’$. C
	AnalisisA
’$’$D M
,
’$’$M N
ref
’$’$O R 
MessageResponseOBJ
’$’$S e
MsgRes
’$’$f l
)
’$’$l m
{
“$“$ 	
DACActualiza
”$”$ 
.
”$”$ '
ActualizarAnalisisAlertas
”$”$ 2
(
”$”$2 3
	AnalisisA
”$”$3 <
,
”$”$< =
ref
”$”$> A
MsgRes
”$”$B H
)
”$”$H I
;
”$”$I J
}
•$•$ 	
public
—$—$ 
int
—$—$ )
InsertarAnalisisCasosSaludP
—$—$ .
(
—$—$. /)
analisis_caso_salud_publica
—$—$/ J
Analisis
—$—$K S
,
—$—$S T
ref
—$—$U X 
MessageResponseOBJ
—$—$Y k
MsgRes
—$—$l r
)
—$—$r s
{
˜$˜$ 	
return
™$™$ 

DACInserta
™$™$ 
.
™$™$ )
InsertarAnalisisCasosSaludP
™$™$ 9
(
™$™$9 :
Analisis
™$™$: B
,
™$™$B C
ref
™$™$D G
MsgRes
™$™$H N
)
™$™$N O
;
™$™$O P
}
š$š$ 	
public
œ$œ$ 
void
œ$œ$ 0
"ActualizarAnalisisCasoSaludPublica
œ$œ$ 6
(
œ$œ$6 7)
analisis_caso_salud_publica
œ$œ$7 R
analisis
œ$œ$S [
,
œ$œ$[ \
ref
œ$œ$] ` 
MessageResponseOBJ
œ$œ$a s
MsgRes
œ$œ$t z
)
œ$œ$z {
{
$$ 	
DACActualiza
$$ 
.
$$ 0
"ActualizarAnalisisCasoSaludPublica
$$ ;
(
$$; <
analisis
$$< D
,
$$D E
ref
$$F I
MsgRes
$$J P
)
$$P Q
;
$$Q R
}
Ÿ$Ÿ$ 	
public
¡$¡$ 
void
¡$¡$ *
InsertarAnalisisCasosEventos
¡$¡$ 0
(
¡$¡$0 11
#ecop_concurrencia_eventos_en_asalud
¡$¡$1 T
Analisis
¡$¡$U ]
,
¡$¡$] ^
List
¡$¡$_ c
<
¡$¡$c d9
*ecop_concurrencia_eventos_en_salud_detalle¡$¡$d 
>¡$¡$ 

otrosiList¡$¡$ š
,¡$¡$š ›
ref¡$¡$œ Ÿ"
MessageResponseOBJ¡$¡$  ²
MsgRes¡$¡$³ ¹
)¡$¡$¹ º
{
¢$¢$ 	

DACInserta
£$£$ 
.
£$£$ *
InsertarAnalisisCasosEventos
£$£$ 3
(
£$£$3 4
Analisis
£$£$4 <
,
£$£$< =

otrosiList
£$£$> H
,
£$£$H I
ref
£$£$J M
MsgRes
£$£$N T
)
£$£$T U
;
£$£$U V
}
¤$¤$ 	
public
¦$¦$ 
void
¦$¦$ -
InsertarAnalisisCasosEventosDet
¦$¦$ 3
(
¦$¦$3 41
#ecop_concurrencia_eventos_en_asalud
¦$¦$4 W
Analisis
¦$¦$X `
,
¦$¦$` a
List
¦$¦$b f
<
¦$¦$f g9
*ecop_concurrencia_eventos_en_salud_detalle¦$¦$g ‘
>¦$¦$‘ ’

otrosiList¦$¦$“ 
,¦$¦$ 
ref¦$¦$Ÿ ¢"
MessageResponseOBJ¦$¦$£ µ
MsgRes¦$¦$¶ ¼
)¦$¦$¼ ½
{
§$§$ 	

DACInserta
¨$¨$ 
.
¨$¨$ -
InsertarAnalisisCasosEventosDet
¨$¨$ 6
(
¨$¨$6 7
Analisis
¨$¨$7 ?
,
¨$¨$? @

otrosiList
¨$¨$A K
,
¨$¨$K L
ref
¨$¨$M P
MsgRes
¨$¨$Q W
)
¨$¨$W X
;
¨$¨$X Y
}
©$©$ 	
public
«$«$ 
Int32
«$«$ 1
#InsertarAnalisisCasosEventosDetalle
«$«$ 8
(
«$«$8 98
*ecop_concurrencia_eventos_en_salud_detalle
«$«$9 c
OBJ
«$«$d g
,
«$«$g h
ref
«$«$i l 
MessageResponseOBJ
«$«$m 
MsgRes«$«$€ †
)«$«$† ‡
{
¬$¬$ 	
return
­$­$ 

DACInserta
­$­$ 
.
­$­$ 1
#InsertarAnalisisCasosEventosDetalle
­$­$ A
(
­$­$A B
OBJ
­$­$B E
,
­$­$E F
ref
­$­$G J
MsgRes
­$­$K Q
)
­$­$Q R
;
­$­$R S
}
®$®$ 	
public
°$°$ 
void
°$°$ ,
ActualizarAnalisisEventosSalud
°$°$ 2
(
°$°$2 31
#ecop_concurrencia_eventos_en_asalud
°$°$3 V
Analisis
°$°$W _
,
°$°$_ `
ref
°$°$a d 
MessageResponseOBJ
°$°$e w
MsgRes
°$°$x ~
)
°$°$~ 
{
±$±$ 	
DACActualiza
²$²$ 
.
²$²$ ,
ActualizarAnalisisEventosSalud
²$²$ 7
(
²$²$7 8
Analisis
²$²$8 @
,
²$²$@ A
ref
²$²$B E
MsgRes
²$²$F L
)
²$²$L M
;
²$²$M N
}
³$³$ 	
public
µ$µ$ 
List
µ$µ$ 
<
µ$µ$ 2
$ManagmentReporteAnalisisCasoSPResult
µ$µ$ 8
>
µ$µ$8 9#
ReporteAnalisisCasoSP
µ$µ$: O
(
µ$µ$O P
Int32
µ$µ$P U
idconcurrencia
µ$µ$V d
,
µ$µ$d e
Int32
µ$µ$f k

idanalisis
µ$µ$l v
)
µ$µ$v w
{
¶$¶$ 	
return
·$·$ 
DACConsulta
·$·$ 
.
·$·$ #
ReporteAnalisisCasoSP
·$·$ 4
(
·$·$4 5
idconcurrencia
·$·$5 C
,
·$·$C D

idanalisis
·$·$E O
)
·$·$O P
;
·$·$P Q
}
¸$¸$ 	
public
º$º$ 
List
º$º$ 
<
º$º$ 3
%ManagmentReporteAnalisisCasoOrgResult
º$º$ 9
>
º$º$9 :)
ReporteAnalisisCasoOriginal
º$º$; V
(
º$º$V W
Int32
º$º$W \
idConcurrencia
º$º$] k
,
º$º$k l
Int32
º$º$m r

idAnalisis
º$º$s }
,
º$º$} ~
refº$º$ ‚"
MessageResponseOBJº$º$ƒ •
MsgResº$º$– œ
)º$º$œ 
{
»$»$ 	
return
¼$¼$ 
DACConsulta
¼$¼$ 
.
¼$¼$ )
ReporteAnalisisCasoOriginal
¼$¼$ :
(
¼$¼$: ;
idConcurrencia
¼$¼$; I
,
¼$¼$I J

idAnalisis
¼$¼$K U
,
¼$¼$U V
ref
¼$¼$W Z
MsgRes
¼$¼$[ a
)
¼$¼$a b
;
¼$¼$b c
}
½$½$ 	
public
¿$¿$ 
List
¿$¿$ 
<
¿$¿$ .
 ManagmentReporteAnalisisESResult
¿$¿$ 4
>
¿$¿$4 5!
ReporteEventosSalud
¿$¿$6 I
(
¿$¿$I J
Int32
¿$¿$J O
IdConcurrencia
¿$¿$P ^
,
¿$¿$^ _
Int32
¿$¿$` e

Idanalisis
¿$¿$f p
)
¿$¿$p q
{
À$À$ 	
return
Á$Á$ 
DACConsulta
Á$Á$ 
.
Á$Á$ !
ReporteEventosSalud
Á$Á$ 2
(
Á$Á$2 3
IdConcurrencia
Á$Á$3 A
,
Á$Á$A B

Idanalisis
Á$Á$C M
)
Á$Á$M N
;
Á$Á$N O
}
Â$Â$ 	
public
Ä$Ä$ 
List
Ä$Ä$ 
<
Ä$Ä$ '
vw_tablero_analisis_casos
Ä$Ä$ -
>
Ä$Ä$- .*
ConsultaTableroAnalisisCasos
Ä$Ä$/ K
(
Ä$Ä$K L
ref
Ä$Ä$L O 
MessageResponseOBJ
Ä$Ä$P b
MsgRes
Ä$Ä$c i
)
Ä$Ä$i j
{
Å$Å$ 	
return
Æ$Æ$ 
DACConsulta
Æ$Æ$ 
.
Æ$Æ$ *
ConsultaTableroAnalisisCasos
Æ$Æ$ ;
(
Æ$Æ$; <
ref
Æ$Æ$< ?
MsgRes
Æ$Æ$@ F
)
Æ$Æ$F G
;
Æ$Æ$G H
}
Ç$Ç$ 	
public
É$É$ 
void
É$É$ +
Insertargestionanalisisdecaso
É$É$ 1
(
É$É$1 2$
GestionAnalisisDeCasos
É$É$2 H
Analisis
É$É$I Q
,
É$É$Q R
ref
É$É$S V 
MessageResponseOBJ
É$É$W i
MsgRes
É$É$j p
)
É$É$p q
{
Ê$Ê$ 	

DACInserta
Ë$Ë$ 
.
Ë$Ë$ +
Insertargestionanalisisdecaso
Ë$Ë$ 4
(
Ë$Ë$4 5
Analisis
Ë$Ë$5 =
,
Ë$Ë$= >
ref
Ë$Ë$? B
MsgRes
Ë$Ë$C I
)
Ë$Ë$I J
;
Ë$Ë$J K
}
Ì$Ì$ 	
public
Î$Î$ 
void
Î$Î$ -
Actualizargestionanalisisdecaso
Î$Î$ 3
(
Î$Î$3 4$
GestionAnalisisDeCasos
Î$Î$4 J
Analisis
Î$Î$K S
,
Î$Î$S T
ref
Î$Î$U X 
MessageResponseOBJ
Î$Î$Y k
MsgRes
Î$Î$l r
)
Î$Î$r s
{
Ï$Ï$ 	
DACActualiza
Ğ$Ğ$ 
.
Ğ$Ğ$ -
Actualizargestionanalisisdecaso
Ğ$Ğ$ 8
(
Ğ$Ğ$8 9
Analisis
Ğ$Ğ$9 A
,
Ğ$Ğ$A B
ref
Ğ$Ğ$C F
MsgRes
Ğ$Ğ$G M
)
Ğ$Ğ$M N
;
Ğ$Ğ$N O
}
Ñ$Ñ$ 	
public
Ó$Ó$ $
GestionAnalisisDeCasos
Ó$Ó$ % 
GetAnalisisGestion
Ó$Ó$& 8
(
Ó$Ó$8 9
Int32
Ó$Ó$9 >
?
Ó$Ó$> ?
idtipoanalisis
Ó$Ó$@ N
,
Ó$Ó$N O
Int32
Ó$Ó$P U
?
Ó$Ó$U V
	idanalsis
Ó$Ó$W `
,
Ó$Ó$` a
ref
Ó$Ó$b e 
MessageResponseOBJ
Ó$Ó$f x
MsgRes
Ó$Ó$y 
)Ó$Ó$ €
{
Ô$Ô$ 	
return
Õ$Õ$ 
DACConsulta
Õ$Õ$ 
.
Õ$Õ$  
GetAnalisisGestion
Õ$Õ$ 1
(
Õ$Õ$1 2
idtipoanalisis
Õ$Õ$2 @
,
Õ$Õ$@ A
	idanalsis
Õ$Õ$B K
,
Õ$Õ$K L
ref
Õ$Õ$M P
MsgRes
Õ$Õ$Q W
)
Õ$Õ$W X
;
Õ$Õ$X Y
}
Ö$Ö$ 	
public
Ø$Ø$ 
List
Ø$Ø$ 
<
Ø$Ø$ &
vw_analisis_caso_alertas
Ø$Ø$ ,
>
Ø$Ø$, -"
GetIdAnalisisAlertas
Ø$Ø$. B
(
Ø$Ø$B C
Int32
Ø$Ø$C H
id_concurrencia
Ø$Ø$I X
,
Ø$Ø$X Y
ref
Ø$Ø$Z ] 
MessageResponseOBJ
Ø$Ø$^ p
MsgRes
Ø$Ø$q w
)
Ø$Ø$w x
{
Ù$Ù$ 	
return
Ú$Ú$ 
DACConsulta
Ú$Ú$ 
.
Ú$Ú$ "
GetIdAnalisisAlertas
Ú$Ú$ 3
(
Ú$Ú$3 4
id_concurrencia
Ú$Ú$4 C
,
Ú$Ú$C D
ref
Ú$Ú$E H
MsgRes
Ú$Ú$I O
)
Ú$Ú$O P
;
Ú$Ú$P Q
}
Û$Û$ 	
public
İ$İ$ 
List
İ$İ$ 
<
İ$İ$ '
vw_analisis_caso_muestreo
İ$İ$ -
>
İ$İ$- .#
GetIdAnalisisMuestras
İ$İ$/ D
(
İ$İ$D E
Int32
İ$İ$E J
id_concurrencia
İ$İ$K Z
,
İ$İ$Z [
ref
İ$İ$\ _ 
MessageResponseOBJ
İ$İ$` r
MsgRes
İ$İ$s y
)
İ$İ$y z
{
Ş$Ş$ 	
return
ß$ß$ 
DACConsulta
ß$ß$ 
.
ß$ß$ #
GetIdAnalisisMuestras
ß$ß$ 4
(
ß$ß$4 5
id_concurrencia
ß$ß$5 D
,
ß$ß$D E
ref
ß$ß$F I
MsgRes
ß$ß$J P
)
ß$ß$P Q
;
ß$ß$Q R
}
à$à$ 	
public
å$å$ 
void
å$å$ 
InsertarUrgencias
å$å$ %
(
å$å$% &
List
å$å$& *
<
å$å$* + 
urg_cargue_base_ok
å$å$+ =
>
å$å$= >
ListUrgencias
å$å$? L
,
å$å$L M
ref
å$å$N Q 
MessageResponseOBJ
å$å$R d
MsgRes
å$å$e k
)
å$å$k l
{
æ$æ$ 	

DACInserta
ç$ç$ 
.
ç$ç$ 
InsertarUrgencias
ç$ç$ (
(
ç$ç$( )
ListUrgencias
ç$ç$) 6
,
ç$ç$6 7
ref
ç$ç$8 ;
MsgRes
ç$ç$< B
)
ç$ç$B C
;
ç$ç$C D
}
è$è$ 	
public
ê$ê$ 
void
ê$ê$ (
InsertarAuditoriaUrgencias
ê$ê$ .
(
ê$ê$. /%
urg_auditoria_urgencias
ê$ê$/ F
aud_urgencias
ê$ê$G T
,
ê$ê$T U
ref
ê$ê$V Y 
MessageResponseOBJ
ê$ê$Z l
MsgRes
ê$ê$m s
)
ê$ê$s t
{
ë$ë$ 	

DACInserta
ì$ì$ 
.
ì$ì$ (
InsertarAuditoriaUrgencias
ì$ì$ 1
(
ì$ì$1 2
aud_urgencias
ì$ì$2 ?
,
ì$ì$? @
ref
ì$ì$A D
MsgRes
ì$ì$E K
)
ì$ì$K L
;
ì$ì$L M
}
í$í$ 	
public
ï$ï$ 
List
ï$ï$ 
<
ï$ï$  
urg_cargue_base_ok
ï$ï$ &
>
ï$ï$& ' 
ConsultarUrgencias
ï$ï$( :
(
ï$ï$: ;
int
ï$ï$; >
?
ï$ï$> ?

idurgencia
ï$ï$@ J
,
ï$ï$J K
DateTime
ï$ï$L T
?
ï$ï$T U

fechadesde
ï$ï$V `
,
ï$ï$` a
DateTime
ï$ï$b j
?
ï$ï$j k

fechahasta
ï$ï$l v
,
ï$ï$v w
int
ï$ï$x {
?
ï$ï${ |
regionalï$ï$} …
,ï$ï$… †
intï$ï$‡ Š
?ï$ï$Š ‹
	idusuarioï$ï$Œ •
,ï$ï$• –
refï$ï$— š"
MessageResponseOBJï$ï$› ­
MsgResï$ï$® ´
)ï$ï$´ µ
{
ğ$ğ$ 	
return
ñ$ñ$ 
DACConsulta
ñ$ñ$ 
.
ñ$ñ$  
ConsultarUrgencias
ñ$ñ$ 1
(
ñ$ñ$1 2

idurgencia
ñ$ñ$2 <
,
ñ$ñ$< =

fechadesde
ñ$ñ$> H
,
ñ$ñ$H I

fechahasta
ñ$ñ$J T
,
ñ$ñ$T U
regional
ñ$ñ$V ^
,
ñ$ñ$^ _
	idusuario
ñ$ñ$` i
,
ñ$ñ$i j
ref
ñ$ñ$k n
MsgRes
ñ$ñ$o u
)
ñ$ñ$u v
;
ñ$ñ$v w
}
ò$ò$ 	
public
ô$ô$ 
List
ô$ô$ 
<
ô$ô$ 
Ref_tipo_egreso
ô$ô$ #
>
ô$ô$# $ 
ConsultaTipoEgreso
ô$ô$% 7
(
ô$ô$7 8
ref
ô$ô$8 ; 
MessageResponseOBJ
ô$ô$< N
MsgRes
ô$ô$O U
)
ô$ô$U V
{
õ$õ$ 	
return
ö$ö$ 
DACConsulta
ö$ö$ 
.
ö$ö$  
ConsultaTipoEgreso
ö$ö$ 1
(
ö$ö$1 2
ref
ö$ö$2 5
MsgRes
ö$ö$6 <
)
ö$ö$< =
;
ö$ö$= >
}
÷$÷$ 	
public
ù$ù$ 
List
ù$ù$ 
<
ù$ù$ &
ref_urg_destino_paciente
ù$ù$ ,
>
ù$ù$, -%
ConsultaDestinoPaciente
ù$ù$. E
(
ù$ù$E F
ref
ù$ù$F I 
MessageResponseOBJ
ù$ù$J \
MsgRes
ù$ù$] c
)
ù$ù$c d
{
ú$ú$ 	
return
û$û$ 
DACConsulta
û$û$ 
.
û$û$ %
ConsultaDestinoPaciente
û$û$ 6
(
û$û$6 7
ref
û$û$7 :
MsgRes
û$û$; A
)
û$û$A B
;
û$û$B C
}
ü$ü$ 	
public
ş$ş$ 
List
ş$ş$ 
<
ş$ş$ %
vw_tablero_urgencias_ok
ş$ş$ +
>
ş$ş$+ ,'
ConsultaTablerUrgenciasOk
ş$ş$- F
(
ş$ş$F G
)
ş$ş$G H
{
ÿ$ÿ$ 	
return
€%€% 
DACConsulta
€%€% 
.
€%€% '
ConsultaTablerUrgenciasOk
€%€% 8
(
€%€%8 9
)
€%€%9 :
;
€%€%: ;
}
%% 	
public
ˆ%ˆ% 
Int32
ˆ%ˆ% $
InsertarCierreContable
ˆ%ˆ% +
(
ˆ%ˆ%+ ,
cierre_contable
ˆ%ˆ%, ;
obj
ˆ%ˆ%< ?
,
ˆ%ˆ%? @
ref
ˆ%ˆ%A D 
MessageResponseOBJ
ˆ%ˆ%E W
MsgRes
ˆ%ˆ%X ^
)
ˆ%ˆ%^ _
{
‰%‰% 	
return
Š%Š% 

DACInserta
Š%Š% 
.
Š%Š% $
InsertarCierreContable
Š%Š% 4
(
Š%Š%4 5
obj
Š%Š%5 8
,
Š%Š%8 9
ref
Š%Š%: =
MsgRes
Š%Š%> D
)
Š%Š%D E
;
Š%Š%E F
}
‹%‹% 	
public
%% 
List
%% 
<
%% 
cierre_contable
%% #
>
%%# $#
GetListCierreContable
%%% :
(
%%: ;
ref
%%; > 
MessageResponseOBJ
%%? Q
MsgRes
%%R X
)
%%X Y
{
%% 	
return
%% 
DACConsulta
%% 
.
%% #
GetListCierreContable
%% 4
(
%%4 5
ref
%%5 8
MsgRes
%%9 ?
)
%%? @
;
%%@ A
}
%% 	
public
’%’% 
bool
’%’% )
InsertarFacturasMesInterior
’%’% /
(
’%’%/ 0
List
’%’%0 4
<
’%’%4 5&
cierre_cont_mes_anterior
’%’%5 M
>
’%’%M N
List
’%’%O S
,
’%’%S T
ref
’%’%U X 
MessageResponseOBJ
’%’%Y k
MsgRes
’%’%l r
)
’%’%r s
{
“%“% 	
return
”%”% 

DACInserta
”%”% 
.
”%”% )
InsertarFacturasMesInterior
”%”% 9
(
”%”%9 :
List
”%”%: >
,
”%”%> ?
ref
”%”%@ C
MsgRes
”%”%D J
)
”%”%J K
;
”%”%K L
}
•%•% 	
public
—%—% 
bool
—%—% &
InsertarFacturasRechazos
—%—% ,
(
—%—%, -
List
—%—%- 1
<
—%—%1 2"
cierre_cont_rechazos
—%—%2 F
>
—%—%F G
List
—%—%H L
,
—%—%L M
ref
—%—%N Q 
MessageResponseOBJ
—%—%R d
MsgRes
—%—%e k
)
—%—%k l
{
˜%˜% 	
return
™%™% 

DACInserta
™%™% 
.
™%™% &
InsertarFacturasRechazos
™%™% 6
(
™%™%6 7
List
™%™%7 ;
,
™%™%; <
ref
™%™%= @
MsgRes
™%™%A G
)
™%™%G H
;
™%™%H I
}
š%š% 	
public
œ%œ% 
bool
œ%œ% -
InsertarFacturasPendientesProcs
œ%œ% 3
(
œ%œ%3 4
List
œ%œ%4 8
<
œ%œ%8 9,
cierre_cont_pendiente_procesar
œ%œ%9 W
>
œ%œ%W X
List
œ%œ%Y ]
,
œ%œ%] ^
ref
œ%œ%_ b 
MessageResponseOBJ
œ%œ%c u
MsgRes
œ%œ%v |
)
œ%œ%| }
{
%% 	
return
%% 

DACInserta
%% 
.
%% -
InsertarFacturasPendientesProcs
%% =
(
%%= >
List
%%> B
,
%%B C
ref
%%D G
MsgRes
%%H N
)
%%N O
;
%%O P
}
Ÿ%Ÿ% 	
public
¡%¡% 
bool
¡%¡% *
InsertarFacturasdevoluciones
¡%¡% 0
(
¡%¡%0 1
List
¡%¡%1 5
<
¡%¡%5 6&
cierre_cont_devoluciones
¡%¡%6 N
>
¡%¡%N O
List
¡%¡%P T
,
¡%¡%T U
ref
¡%¡%V Y 
MessageResponseOBJ
¡%¡%Z l
MsgRes
¡%¡%m s
)
¡%¡%s t
{
¢%¢% 	
return
£%£% 

DACInserta
£%£% 
.
£%£% *
InsertarFacturasdevoluciones
£%£% :
(
£%£%: ;
List
£%£%; ?
,
£%£%? @
ref
£%£%A D
MsgRes
£%£%E K
)
£%£%K L
;
£%£%L M
}
¤%¤% 	
public
¦%¦% 
bool
¦%¦% &
InsertarFacturascausadas
¦%¦% ,
(
¦%¦%, -
List
¦%¦%- 1
<
¦%¦%1 2"
cierre_cont_causadas
¦%¦%2 F
>
¦%¦%F G
List
¦%¦%H L
,
¦%¦%L M
ref
¦%¦%N Q 
MessageResponseOBJ
¦%¦%R d
MsgRes
¦%¦%e k
)
¦%¦%k l
{
§%§% 	
return
¨%¨% 

DACInserta
¨%¨% 
.
¨%¨% &
InsertarFacturascausadas
¨%¨% 6
(
¨%¨%6 7
List
¨%¨%7 ;
,
¨%¨%; <
ref
¨%¨%= @
MsgRes
¨%¨%A G
)
¨%¨%G H
;
¨%¨%H I
}
©%©% 	
public
«%«% 
bool
«%«% '
InsertarFacturasradicadas
«%«% -
(
«%«%- .
List
«%«%. 2
<
«%«%2 3#
cierre_cont_radicadas
«%«%3 H
>
«%«%H I
List
«%«%J N
,
«%«%N O
ref
«%«%P S 
MessageResponseOBJ
«%«%T f
MsgRes
«%«%g m
)
«%«%m n
{
¬%¬% 	
return
­%­% 

DACInserta
­%­% 
.
­%­% '
InsertarFacturasradicadas
­%­% 7
(
­%­%7 8
List
­%­%8 <
,
­%­%< =
ref
­%­%> A
MsgRes
­%­%B H
)
­%­%H I
;
­%­%I J
}
®%®% 	
public
°%°% 
cierre_contable
°%°% 
GetCierreContable
°%°% 0
(
°%°%0 1
int
°%°%1 4
idcierre
°%°%5 =
,
°%°%= >
ref
°%°%? B 
MessageResponseOBJ
°%°%C U
MsgRes
°%°%V \
)
°%°%\ ]
{
±%±% 	
return
²%²% 
DACConsulta
²%²% 
.
²%²% 
GetCierreContable
²%²% 0
(
²%²%0 1
idcierre
²%²%1 9
,
²%²%9 :
ref
²%²%; >
MsgRes
²%²%? E
)
²%²%E F
;
²%²%F G
}
³%³% 	
public
µ%µ% 
List
µ%µ% 
<
µ%µ% (
vw_totales_cierre_contable
µ%µ% .
>
µ%µ%. /*
GetListTotalesCierreContable
µ%µ%0 L
(
µ%µ%L M
int
µ%µ%M P
idcierre
µ%µ%Q Y
,
µ%µ%Y Z
ref
µ%µ%[ ^ 
MessageResponseOBJ
µ%µ%_ q
MsgRes
µ%µ%r x
)
µ%µ%x y
{
¶%¶% 	
return
·%·% 
DACConsulta
·%·% 
.
·%·% *
GetListTotalesCierreContable
·%·% ;
(
·%·%; <
idcierre
·%·%< D
,
·%·%D E
ref
·%·%F I
MsgRes
·%·%J P
)
·%·%P Q
;
·%·%Q R
}
¸%¸% 	
public
º%º% 
List
º%º% 
<
º%º%  
vw_causas_facturas
º%º% &
>
º%º%& ')
GetListCausasCierreContable
º%º%( C
(
º%º%C D
int
º%º%D G
idcierre
º%º%H P
,
º%º%P Q
ref
º%º%R U 
MessageResponseOBJ
º%º%V h
MsgRes
º%º%i o
)
º%º%o p
{
»%»% 	
return
¼%¼% 
DACConsulta
¼%¼% 
.
¼%¼% )
GetListCausasCierreContable
¼%¼% :
(
¼%¼%: ;
idcierre
¼%¼%; C
,
¼%¼%C D
ref
¼%¼%E H
MsgRes
¼%¼%I O
)
¼%¼%O P
;
¼%¼%P Q
}
½%½% 	
public
À%À% )
cierre_contable_cargue_base
À%À% *!
traerCierreContable
À%À%+ >
(
À%À%> ?
int
À%À%? B
?
À%À%B C
mes
À%À%D G
,
À%À%G H
int
À%À%I L
?
À%À%L M
aÃ±o
À%À%N Q
,
À%À%Q R
int
À%À%S V
?
À%À%V W
regional
À%À%X `
)
À%À%` a
{
Á%Á% 	
return
Â%Â% 
DACConsulta
Â%Â% 
.
Â%Â% !
traerCierreContable
Â%Â% 2
(
Â%Â%2 3
mes
Â%Â%3 6
,
Â%Â%6 7
aÃ±o
Â%Â%8 ;
,
Â%Â%; <
regional
Â%Â%= E
)
Â%Â%E F
;
Â%Â%F G
}
Ã%Ã% 	
public
Å%Å% 
List
Å%Å% 
<
Å%Å% =
/management_cierre_contable_tableroControlResult
Å%Å% C
>
Å%Å%C D&
TraerDatosCierreContable
Å%Å%E ]
(
Å%Å%] ^
)
Å%Å%^ _
{
Æ%Æ% 	
return
Ç%Ç% 
DACConsulta
Ç%Ç% 
.
Ç%Ç% &
TraerDatosCierreContable
Ç%Ç% 7
(
Ç%Ç%7 8
)
Ç%Ç%8 9
;
Ç%Ç%9 :
}
È%È% 	
public
Ê%Ê% 
int
Ê%Ê% $
InsertarCierreContable
Ê%Ê% )
(
Ê%Ê%) *)
cierre_contable_cargue_base
Ê%Ê%* E
obj
Ê%Ê%F I
,
Ê%Ê%I J
ref
Ê%Ê%K N 
MessageResponseOBJ
Ê%Ê%O a
MsgRes
Ê%Ê%b h
)
Ê%Ê%h i
{
Ë%Ë% 	
return
Ì%Ì% 

DACInserta
Ì%Ì% 
.
Ì%Ì% $
InsertarCierreContable
Ì%Ì% 4
(
Ì%Ì%4 5
obj
Ì%Ì%5 8
,
Ì%Ì%8 9
ref
Ì%Ì%: =
MsgRes
Ì%Ì%> D
)
Ì%Ì%D E
;
Ì%Ì%E F
}
Í%Í% 	
public
Ï%Ï% 
void
Ï%Ï% +
InsertarCierreContableDetalle
Ï%Ï% 1
(
Ï%Ï%1 2
List
Ï%Ï%2 6
<
Ï%Ï%6 7,
cierre_contable_cargue_detalle
Ï%Ï%7 U
>
Ï%Ï%U V
dtll
Ï%Ï%W [
,
Ï%Ï%[ \
ref
Ï%Ï%] ` 
MessageResponseOBJ
Ï%Ï%a s
MsgRes
Ï%Ï%t z
)
Ï%Ï%z {
{
Ğ%Ğ% 	

DACInserta
Ñ%Ñ% 
.
Ñ%Ñ% +
InsertarCierreContableDetalle
Ñ%Ñ% 4
(
Ñ%Ñ%4 5
dtll
Ñ%Ñ%5 9
,
Ñ%Ñ%9 :
ref
Ñ%Ñ%; >
MsgRes
Ñ%Ñ%? E
)
Ñ%Ñ%E F
;
Ñ%Ñ%F G
}
Ò%Ò% 	
public
Ô%Ô% 
int
Ô%Ô% *
EliminarCargueCierreContable
Ô%Ô% /
(
Ô%Ô%/ 0
int
Ô%Ô%0 3
idCargue
Ô%Ô%4 <
)
Ô%Ô%< =
{
Õ%Õ% 	
return
Ö%Ö% 

DACElimina
Ö%Ö% 
.
Ö%Ö% *
EliminarCargueCierreContable
Ö%Ö% :
(
Ö%Ö%: ;
idCargue
Ö%Ö%; C
)
Ö%Ö%C D
;
Ö%Ö%D E
}
×%×% 	
public
Ù%Ù% 
int
Ù%Ù% /
!InsertarLogEliminarCierreContable
Ù%Ù% 4
(
Ù%Ù%4 54
&log_cierreContable_eliminarConsolidado
Ù%Ù%5 [
obj
Ù%Ù%\ _
)
Ù%Ù%_ `
{
Ú%Ú% 	
return
Û%Û% 

DACInserta
Û%Û% 
.
Û%Û% /
!InsertarLogEliminarCierreContable
Û%Û% ?
(
Û%Û%? @
obj
Û%Û%@ C
)
Û%Û%C D
;
Û%Û%D E
}
Ü%Ü% 	
public
â%â% 
List
â%â% 
<
â%â% 
ref_cohortes
â%â%  
>
â%â%  !
Get_refCohortes
â%â%" 1
(
â%â%1 2
)
â%â%2 3
{
ã%ã% 	
return
ä%ä% 
DACConsulta
ä%ä% 
.
ä%ä% 
Get_refCohortes
ä%ä% .
(
ä%ä%. /
)
ä%ä%/ 0
;
ä%ä%0 1
}
å%å% 	
public
ç%ç% 
List
ç%ç% 
<
ç%ç% 
ref_cohortes
ç%ç%  
>
ç%ç%  !"
Get_refCohortesSindh
ç%ç%" 6
(
ç%ç%6 7
)
ç%ç%7 8
{
è%è% 	
return
é%é% 
DACConsulta
é%é% 
.
é%é% "
Get_refCohortesSindh
é%é% 3
(
é%é%3 4
)
é%é%4 5
;
é%é%5 6
}
ê%ê% 	
public
ë%ë% 
List
ë%ë% 
<
ë%ë% *
ref_adh_modalidad_prestacion
ë%ë% 0
>
ë%ë%0 11
#Get_adherencia_modalidad_prestacion
ë%ë%2 U
(
ë%ë%U V
)
ë%ë%V W
{
ì%ì% 	
return
í%í% 
DACConsulta
í%í% 
.
í%í% 1
#Get_adherencia_modalidad_prestacion
í%í% B
(
í%í%B C
)
í%í%C D
;
í%í%D E
}
î%î% 	
public
ï%ï% 
int
ï%ï% !
InsertCohortesDatos
ï%ï% &
(
ï%ï%& '"
cohortes_cargue_base
ï%ï%' ;
obj
ï%ï%< ?
,
ï%ï%? @
List
ï%ï%A E
<
ï%ï%E F(
cohortes_detalle_cargue_OK
ï%ï%F `
>
ï%ï%` a
lista
ï%ï%b g
,
ï%ï%g h
ref
ï%ï%i l 
MessageResponseOBJ
ï%ï%m 
MsgResï%ï%€ †
)ï%ï%† ‡
{
ğ%ğ% 	
return
ñ%ñ% 

DACInserta
ñ%ñ% 
.
ñ%ñ% !
InsertCohortesDatos
ñ%ñ% 1
(
ñ%ñ%1 2
obj
ñ%ñ%2 5
,
ñ%ñ%5 6
lista
ñ%ñ%7 <
,
ñ%ñ%< =
ref
ñ%ñ%> A
MsgRes
ñ%ñ%B H
)
ñ%ñ%H I
;
ñ%ñ%I J
}
ò%ò% 	
public
ó%ó% 
int
ó%ó%  
InsertCohortesEPOC
ó%ó% %
(
ó%ó%% &"
cohortes_cargue_base
ó%ó%& :
obj
ó%ó%; >
,
ó%ó%> ?
List
ó%ó%@ D
<
ó%ó%D E(
cohortes_detalle_cargue_OK
ó%ó%E _
>
ó%ó%_ `
	listaepoc
ó%ó%a j
,
ó%ó%j k
ref
ó%ó%l o!
MessageResponseOBJó%ó%p ‚
MsgResó%ó%ƒ ‰
)ó%ó%‰ Š
{
ô%ô% 	
return
õ%õ% 

DACInserta
õ%õ% 
.
õ%õ%  
InsertCohortesEPOC
õ%õ% 0
(
õ%õ%0 1
obj
õ%õ%1 4
,
õ%õ%4 5
	listaepoc
õ%õ%6 ?
,
õ%õ%? @
ref
õ%õ%A D
MsgRes
õ%õ%E K
)
õ%õ%K L
;
õ%õ%L M
}
ö%ö% 	
public
ø%ø% 
void
ø%ø% 
InsertCohortesPAD
ø%ø% %
(
ø%ø%% &"
cohortes_cargue_base
ø%ø%& :
cargue
ø%ø%; A
,
ø%ø%A B
List
ø%ø%C G
<
ø%ø%G H(
cohortes_detalle_cargue_OK
ø%ø%H b
>
ø%ø%b c
listaPAD
ø%ø%d l
,
ø%ø%l m
ref
ø%ø%n q!
MessageResponseOBJø%ø%r „
MsgResø%ø%… ‹
)ø%ø%‹ Œ
{
ù%ù% 	

DACInserta
ú%ú% 
.
ú%ú% 
InsertCohortesPAD
ú%ú% (
(
ú%ú%( )
cargue
ú%ú%) /
,
ú%ú%/ 0
listaPAD
ú%ú%1 9
,
ú%ú%9 :
ref
ú%ú%; >
MsgRes
ú%ú%? E
)
ú%ú%E F
;
ú%ú%F G
}
û%û% 	
public
ı%ı% 
void
ı%ı% 
InsertCohortesRCV
ı%ı% %
(
ı%ı%% &"
cohortes_cargue_base
ı%ı%& :
cargue
ı%ı%; A
,
ı%ı%A B
List
ı%ı%C G
<
ı%ı%G H(
cohortes_detalle_cargue_OK
ı%ı%H b
>
ı%ı%b c
listaRCV
ı%ı%d l
,
ı%ı%l m
ref
ı%ı%n q!
MessageResponseOBJı%ı%r „
MsgResı%ı%… ‹
)ı%ı%‹ Œ
{
ş%ş% 	

DACInserta
ÿ%ÿ% 
.
ÿ%ÿ% 
InsertCohortesRCV
ÿ%ÿ% (
(
ÿ%ÿ%( )
cargue
ÿ%ÿ%) /
,
ÿ%ÿ%/ 0
listaRCV
ÿ%ÿ%1 9
,
ÿ%ÿ%9 :
ref
ÿ%ÿ%; >
MsgRes
ÿ%ÿ%? E
)
ÿ%ÿ%E F
;
ÿ%ÿ%F G
}
€&€& 	
public
‚&‚& 
void
‚&‚& %
InsertCohortesGESTANTES
‚&‚& +
(
‚&‚&+ ,"
cohortes_cargue_base
‚&‚&, @
cargue
‚&‚&A G
,
‚&‚&G H
List
‚&‚&I M
<
‚&‚&M N(
cohortes_detalle_cargue_OK
‚&‚&N h
>
‚&‚&h i
listaGestantes
‚&‚&j x
,
‚&‚&x y
ref
‚&‚&z }!
MessageResponseOBJ‚&‚&~ 
MsgRes‚&‚&‘ —
)‚&‚&— ˜
{
ƒ&ƒ& 	

DACInserta
„&„& 
.
„&„& %
InsertCohortesGESTANTES
„&„& .
(
„&„&. /
cargue
„&„&/ 5
,
„&„&5 6
listaGestantes
„&„&7 E
,
„&„&E F
ref
„&„&G J
MsgRes
„&„&K Q
)
„&„&Q R
;
„&„&R S
}
…&…& 	
public
‡&‡& 
List
‡&‡& 
<
‡&‡& 3
%management_cohortesBeneficiarioResult
‡&‡& 9
>
‡&‡&9 :%
GetCohortesBeneficiario
‡&‡&; R
(
‡&‡&R S
string
‡&‡&S Y
idDoc
‡&‡&Z _
)
‡&‡&_ `
{
ˆ&ˆ& 	
return
‰&‰& 
DACConsulta
‰&‰& 
.
‰&‰& %
GetCohortesBeneficiario
‰&‰& 6
(
‰&‰&6 7
idDoc
‰&‰&7 <
)
‰&‰&< =
;
‰&‰&= >
}
Š&Š& 	
public
‹&‹& 
List
‹&‹& 
<
‹&‹& ?
1management_HospitalizacionEvitable_cohortesResult
‹&‹& E
>
‹&‹&E F0
"HospitalizacionPrevenible_cohortes
‹&‹&G i
(
‹&‹&i j
string
‹&‹&j p
idDoc
‹&‹&q v
)
‹&‹&v w
{
Œ&Œ& 	
return
&& 
DACConsulta
&& 
.
&& 0
"HospitalizacionPrevenible_cohortes
&& A
(
&&A B
idDoc
&&B G
)
&&G H
;
&&H I
}
&& 	
public
&& 
List
&& 
<
&& @
2management_hospitalizacionPrevenible_TableroResult
&& F
>
&&F G*
GetHospitalizacionPrevenible
&&H d
(
&&d e
)
&&e f
{
&& 	
return
‘&‘& 
DACConsulta
‘&‘& 
.
‘&‘& *
GetHospitalizacionPrevenible
‘&‘& ;
(
‘&‘&; <
)
‘&‘&< =
;
‘&‘&= >
}
’&’& 	
public
“&“& @
2management_hospitalizacionPrevenible_detalleResult
“&“& A1
#GetHospitalizacionPrevenibleDetalle
“&“&B e
(
“&“&e f
int
“&“&f i
idHE
“&“&j n
)
“&“&n o
{
”&”& 	
return
•&•& 
DACConsulta
•&•& 
.
•&•& 1
#GetHospitalizacionPrevenibleDetalle
•&•& B
(
•&•&B C
idHE
•&•&C G
)
•&•&G H
;
•&•&H I
}
–&–& 	
public
˜&˜& 
int
˜&˜& /
!InsertarHospitalizacionPrevenible
˜&˜& 4
(
˜&˜&4 5-
ecop_hospitalizacion_prevenible
˜&˜&5 T
obj
˜&˜&U X
)
˜&˜&X Y
{
™&™& 	
return
š&š& 

DACInserta
š&š& 
.
š&š& /
!InsertarHospitalizacionPrevenible
š&š& ?
(
š&š&? @
obj
š&š&@ C
)
š&š&C D
;
š&š&D E
}
›&›& 	
public
&& 
List
&& 
<
&& (
ecop_directorioPPE_correos
&& .
>
&&. /+
GetEcop_DirectorioPPE_Correos
&&0 M
(
&&M N
string
&&N T
regional
&&U ]
)
&&] ^
{
&& 	
return
Ÿ&Ÿ& 
DACConsulta
Ÿ&Ÿ& 
.
Ÿ&Ÿ& +
GetEcop_DirectorioPPE_Correos
Ÿ&Ÿ& <
(
Ÿ&Ÿ&< =
regional
Ÿ&Ÿ&= E
)
Ÿ&Ÿ&E F
;
Ÿ&Ÿ&F G
}
 & & 	
public
¡&¡& 
List
¡&¡& 
<
¡&¡& (
ecop_directorioPPE_correos
¡&¡& .
>
¡&¡&. /4
&GetEcop_DirectorioPPE_CorreosDocumento
¡&¡&0 V
(
¡&¡&V W
string
¡&¡&W ]
	documento
¡&¡&^ g
)
¡&¡&g h
{
¢&¢& 	
return
£&£& 
DACConsulta
£&£& 
.
£&£& 4
&GetEcop_DirectorioPPE_CorreosDocumento
£&£& E
(
£&£&E F
	documento
£&£&F O
)
£&£&O P
;
£&£&P Q
}
¥&¥& 	
public
«&«& 
List
«&«& 
<
«&«& #
ref_adh_tipo_criterio
«&«& )
>
«&«&) *"
get_ref_TipoCriterio
«&«&+ ?
(
«&«&? @
)
«&«&@ A
{
¬&¬& 	
return
­&­& 
DACConsulta
­&­& 
.
­&­& "
get_ref_TipoCriterio
­&­& 3
(
­&­&3 4
)
­&­&4 5
;
­&­&5 6
}
®&®& 	
public
°&°& 
List
°&°& 
<
°&°& (
ref_adh_grupo_tipocriterio
°&°& .
>
°&°&. /'
get_ref_grupoTipoCriterio
°&°&0 I
(
°&°&I J
)
°&°&J K
{
±&±& 	
return
²&²& 
DACConsulta
²&²& 
.
²&²& '
get_ref_grupoTipoCriterio
²&²& 8
(
²&²&8 9
)
²&²&9 :
;
²&²&: ;
}
³&³& 	
public
µ&µ& 
List
µ&µ& 
<
µ&µ& 
adh_tipocriterio
µ&µ& $
>
µ&µ&$ %"
get_adh_tipocriterio
µ&µ&& :
(
µ&µ&: ;
int
µ&µ&; >
idadherencia
µ&µ&? K
)
µ&µ&K L
{
¶&¶& 	
return
·&·& 
DACConsulta
·&·& 
.
·&·& "
get_adh_tipocriterio
·&·& 3
(
·&·&3 4
idadherencia
·&·&4 @
)
·&·&@ A
;
·&·&A B
}
¸&¸& 	
public
º&º& 
List
º&º& 
<
º&º& (
ref_adh_grupo_tipocriterio
º&º& .
>
º&º&. /+
get_ref_adh_grupotipocriterio
º&º&0 M
(
º&º&M N
)
º&º&N O
{
»&»& 	
return
¼&¼& 
DACConsulta
¼&¼& 
.
¼&¼& +
get_ref_adh_grupotipocriterio
¼&¼& <
(
¼&¼&< =
)
¼&¼&= >
;
¼&¼&> ?
}
½&½& 	
public
¿&¿& 
List
¿&¿& 
<
¿&¿& #
ref_adh_tipo_criterio
¿&¿& )
>
¿&¿&) **
get_ref_TipoCriterio_cohorte
¿&¿&+ G
(
¿&¿&G H
int
¿&¿&H K
idtipocohorte
¿&¿&L Y
)
¿&¿&Y Z
{
À&À& 	
return
Á&Á& 
DACConsulta
Á&Á& 
.
Á&Á& *
get_ref_TipoCriterio_cohorte
Á&Á& ;
(
Á&Á&; <
idtipocohorte
Á&Á&< I
)
Á&Á&I J
;
Á&Á&J K
}
Â&Â& 	
public
Å&Å& 
List
Å&Å& 
<
Å&Å& 
adh_criterio
Å&Å&  
>
Å&Å&  !'
getcriteriosbytipocohorte
Å&Å&" ;
(
Å&Å&; <
int
Å&Å&< ?
tipocohorte
Å&Å&@ K
)
Å&Å&K L
{
Æ&Æ& 	
return
Ç&Ç& 
DACConsulta
Ç&Ç& 
.
Ç&Ç& '
getcriteriosbytipocohorte
Ç&Ç& 8
(
Ç&Ç&8 9
tipocohorte
Ç&Ç&9 D
)
Ç&Ç&D E
;
Ç&Ç&E F
}
È&È& 	
public
Ê&Ê& 
void
Ê&Ê& "
InsertarTipoCriterio
Ê&Ê& (
(
Ê&Ê&( )#
ref_adh_tipo_criterio
Ê&Ê&) >
obj
Ê&Ê&? B
,
Ê&Ê&B C
ref
Ê&Ê&D G 
MessageResponseOBJ
Ê&Ê&H Z
MsgRes
Ê&Ê&[ a
)
Ê&Ê&a b
{
Ë&Ë& 	

DACInserta
Ì&Ì& 
.
Ì&Ì& "
InsertarTipoCriterio
Ì&Ì& +
(
Ì&Ì&+ ,
obj
Ì&Ì&, /
,
Ì&Ì&/ 0
ref
Ì&Ì&1 4
MsgRes
Ì&Ì&5 ;
)
Ì&Ì&; <
;
Ì&Ì&< =
}
Í&Í& 	
public
Ï&Ï& 
void
Ï&Ï& 
InsertarCriterio
Ï&Ï& $
(
Ï&Ï&$ %
adh_criterio
Ï&Ï&% 1
criterio
Ï&Ï&2 :
,
Ï&Ï&: ;
ref
Ï&Ï&< ? 
MessageResponseOBJ
Ï&Ï&@ R
MsgRes
Ï&Ï&S Y
)
Ï&Ï&Y Z
{
Ğ&Ğ& 	

DACInserta
Ñ&Ñ& 
.
Ñ&Ñ& 
InsertarCriterio
Ñ&Ñ& '
(
Ñ&Ñ&' (
criterio
Ñ&Ñ&( 0
,
Ñ&Ñ&0 1
ref
Ñ&Ñ&2 5
MsgRes
Ñ&Ñ&6 <
)
Ñ&Ñ&< =
;
Ñ&Ñ&= >
}
Ò&Ò& 	
public
Ô&Ô& 
void
Ô&Ô& $
ActualizarTipoCriterio
Ô&Ô& *
(
Ô&Ô&* +#
ref_adh_tipo_criterio
Ô&Ô&+ @
obj
Ô&Ô&A D
,
Ô&Ô&D E
ref
Ô&Ô&F I 
MessageResponseOBJ
Ô&Ô&J \
MsgRes
Ô&Ô&] c
)
Ô&Ô&c d
{
Õ&Õ& 	
DACActualiza
Ö&Ö& 
.
Ö&Ö& $
ActualizarTipoCriterio
Ö&Ö& /
(
Ö&Ö&/ 0
obj
Ö&Ö&0 3
,
Ö&Ö&3 4
ref
Ö&Ö&5 8
MsgRes
Ö&Ö&9 ?
)
Ö&Ö&? @
;
Ö&Ö&@ A
}
×&×& 	
public
Ù&Ù& 
void
Ù&Ù&  
ActualizarCriterio
Ù&Ù& &
(
Ù&Ù&& '
adh_criterio
Ù&Ù&' 3
criterio
Ù&Ù&4 <
,
Ù&Ù&< =
ref
Ù&Ù&> A 
MessageResponseOBJ
Ù&Ù&B T
MsgRes
Ù&Ù&U [
)
Ù&Ù&[ \
{
Ú&Ú& 	
DACActualiza
Û&Û& 
.
Û&Û&  
ActualizarCriterio
Û&Û& +
(
Û&Û&+ ,
criterio
Û&Û&, 4
,
Û&Û&4 5
ref
Û&Û&6 9
MsgRes
Û&Û&: @
)
Û&Û&@ A
;
Û&Û&A B
}
Ü&Ü& 	
public
Ş&Ş& 
void
Ş&Ş& %
EliminarCriterioCohorte
Ş&Ş& +
(
Ş&Ş&+ ,
int
Ş&Ş&, /

idcriterio
Ş&Ş&0 :
,
Ş&Ş&: ;
ref
Ş&Ş&< ? 
MessageResponseOBJ
Ş&Ş&@ R
MsgRes
Ş&Ş&S Y
)
Ş&Ş&Y Z
{
ß&ß& 	

DACElimina
à&à& 
.
à&à& %
EliminarCriterioCohorte
à&à& .
(
à&à&. /

idcriterio
à&à&/ 9
,
à&à&9 :
ref
à&à&; >
MsgRes
à&à&? E
)
à&à&E F
;
à&à&F G
}
á&á& 	
public
ã&ã& 
void
ã&ã& "
EliminarTipoCriterio
ã&ã& (
(
ã&ã&( )
int
ã&ã&) ,
idtipocriterio
ã&ã&- ;
,
ã&ã&; <
ref
ã&ã&= @ 
MessageResponseOBJ
ã&ã&A S
MsgRes
ã&ã&T Z
)
ã&ã&Z [
{
ä&ä& 	

DACElimina
å&å& 
.
å&å& "
EliminarTipoCriterio
å&å& +
(
å&å&+ ,
idtipocriterio
å&å&, :
,
å&å&: ;
ref
å&å&< ?
MsgRes
å&å&@ F
)
å&å&F G
;
å&å&G H
}
æ&æ& 	
public
è&è& 
adh_criterio
è&è& #
ConsultarCriterioById
è&è& 1
(
è&è&1 2
int
è&è&2 5

idcriterio
è&è&6 @
)
è&è&@ A
{
é&é& 	
return
ê&ê& 
DACConsulta
ê&ê& 
.
ê&ê& #
ConsultarCriterioById
ê&ê& 4
(
ê&ê&4 5

idcriterio
ê&ê&5 ?
)
ê&ê&? @
;
ê&ê&@ A
}
ë&ë& 	
public
í&í& 
int
í&í&  
InsertarResultados
í&í& %
(
í&í&% &
adh_resultados
í&í&& 4

resultados
í&í&5 ?
,
í&í&? @
List
í&í&A E
<
í&í&E F
string
í&í&F L
>
í&í&L M
resultadoshc1
í&í&N [
,
í&í&[ \
ref
í&í&] ` 
MessageResponseOBJ
í&í&a s
Msg
í&í&t w
)
í&í&w x
{
î&î& 	
return
ï&ï& 

DACInserta
ï&ï& 
.
ï&ï&  
InsertarResultados
ï&ï& 0
(
ï&ï&0 1

resultados
ï&ï&1 ;
,
ï&ï&; <
resultadoshc1
ï&ï&= J
,
ï&ï&J K
ref
ï&ï&L O
Msg
ï&ï&P S
)
ï&ï&S T
;
ï&ï&T U
}
ğ&ğ& 	
public
ò&ò& 
List
ò&ò& 
<
ò&ò& 
adh_resultados
ò&ò& "
>
ò&ò&" #$
GetResultadosPrestador
ò&ò&$ :
(
ò&ò&: ;
int
ò&ò&; >
idprestador
ò&ò&? J
,
ò&ò&J K
int
ò&ò&L O
profesional
ò&ò&P [
,
ò&ò&[ \
int
ò&ò&] `
mes
ò&ò&a d
,
ò&ò&d e
int
ò&ò&f i
aÃ±o
ò&ò&j m
)
ò&ò&m n
{
ó&ó& 	
return
ô&ô& 
DACConsulta
ô&ô& 
.
ô&ô& &
GetResultadosPrestadorv2
ô&ô& 7
(
ô&ô&7 8
idprestador
ô&ô&8 C
,
ô&ô&C D
profesional
ô&ô&E P
,
ô&ô&P Q
mes
ô&ô&R U
,
ô&ô&U V
aÃ±o
ô&ô&W Z
)
ô&ô&Z [
;
ô&ô&[ \
}
õ&õ& 	
public
÷&÷& 
List
÷&÷& 
<
÷&÷& (
vw_rptResultadosAdherencia
÷&÷& .
>
÷&÷&. /$
GetResultadosPrestador
÷&÷&0 F
(
÷&÷&F G
Int32
÷&÷&G L
?
÷&÷&L M
idresultados
÷&÷&N Z
)
÷&÷&Z [
{
ø&ø& 	
return
ù&ù& 
DACConsulta
ù&ù& 
.
ù&ù& $
GetResultadosPrestador
ù&ù& 5
(
ù&ù&5 6
idresultados
ù&ù&6 B
)
ù&ù&B C
;
ù&ù&C D
}
ú&ú& 	
public
ü&ü& 
List
ü&ü& 
<
ü&ü& 8
*managmentReporteResultadosAdherenciaResult
ü&ü& >
>
ü&ü&> ?%
GetResultadosAdherencia
ü&ü&@ W
(
ü&ü&W X
Int32
ü&ü&X ]
idresultados
ü&ü&^ j
)
ü&ü&j k
{
ı&ı& 	
return
ş&ş& 
DACConsulta
ş&ş& 
.
ş&ş& %
GetResultadosAdherencia
ş&ş& 6
(
ş&ş&6 7
idresultados
ş&ş&7 C
)
ş&ş&C D
;
ş&ş&D E
}
ÿ&ÿ& 	
public
'' 
List
'' 
<
'' 9
+managmentReporteResultadosAdherencia2Result
'' ?
>
''? @&
GetResultadosAdherencia2
''A Y
(
''Y Z
Int32
''Z _
idresultados
''` l
)
''l m
{
‚'‚' 	
return
ƒ'ƒ' 
DACConsulta
ƒ'ƒ' 
.
ƒ'ƒ' &
GetResultadosAdherencia2
ƒ'ƒ' 7
(
ƒ'ƒ'7 8
idresultados
ƒ'ƒ'8 D
)
ƒ'ƒ'D E
;
ƒ'ƒ'E F
}
„'„' 	
public
†'†' 
List
†'†' 
<
†'†' <
.Management_adh_cantidad_resultados_grupoResult
†'†' B
>
†'†'B C*
GetResultadosGrupoAdherencia
†'†'D `
(
†'†'` a
Int32
†'†'a f
idresultados
†'†'g s
)
†'†'s t
{
‡'‡' 	
return
ˆ'ˆ' 
DACConsulta
ˆ'ˆ' 
.
ˆ'ˆ' *
GetResultadosGrupoAdherencia
ˆ'ˆ' ;
(
ˆ'ˆ'; <
idresultados
ˆ'ˆ'< H
)
ˆ'ˆ'H I
;
ˆ'ˆ'I J
}
‰'‰' 	
public
‹'‹' 
List
‹'‹' 
<
‹'‹' 
Ref_ips_cuentas
‹'‹' #
>
‹'‹'# $
getprestadores
‹'‹'% 3
(
‹'‹'3 4
)
‹'‹'4 5
{
Œ'Œ' 	
return
'' 
DACConsulta
'' 
.
'' 
getprestadores
'' -
(
''- .
)
''. /
;
''/ 0
}
'' 	
public
'' 
List
'' 
<
'' 5
'management_prestadoresHomologadosResult
'' ;
>
''; <'
getprestadoresHomologados
''= V
(
''V W
)
''W X
{
‘'‘' 	
return
’'’' 
DACConsulta
’'’' 
.
’'’' '
getprestadoresHomologados
’'’' 8
(
’'’'8 9
)
’'’'9 :
;
’'’': ;
}
“'“' 	
public
•'•' 
void
•'•' !
InsertarTipoCohorte
•'•' '
(
•'•'' (
ref_cohortes
•'•'( 4
obj
•'•'5 8
)
•'•'8 9
{
–'–' 	

DACInserta
—'—' 
.
—'—' !
InsertarTipoCohorte
—'—' *
(
—'—'* +
obj
—'—'+ .
)
—'—'. /
;
—'—'/ 0
}
˜'˜' 	
public
š'š' 
void
š'š' #
ActualizarTipoCohorte
š'š' )
(
š'š') *
ref_cohortes
š'š'* 6
obj
š'š'7 :
)
š'š': ;
{
›'›' 	
DACActualiza
œ'œ' 
.
œ'œ' #
ActualizarTipoCohorte
œ'œ' .
(
œ'œ'. /
obj
œ'œ'/ 2
)
œ'œ'2 3
;
œ'œ'3 4
}
'' 	
public
Ÿ'Ÿ' 
ref_cohortes
Ÿ'Ÿ'  
getTipoCohorteById
Ÿ'Ÿ' .
(
Ÿ'Ÿ'. /
int
Ÿ'Ÿ'/ 2
idtipocohorte
Ÿ'Ÿ'3 @
)
Ÿ'Ÿ'@ A
{
 ' ' 	
return
¡'¡' 
DACConsulta
¡'¡' 
.
¡'¡'  
getTipoCohorteById
¡'¡' 1
(
¡'¡'1 2
idtipocohorte
¡'¡'2 ?
)
¡'¡'? @
;
¡'¡'@ A
}
¢'¢' 	
public
¤'¤' 
void
¤'¤' $
InsertarAdminCriterios
¤'¤' *
(
¤'¤'* +
int
¤'¤'+ .
tipoadherencia
¤'¤'/ =
,
¤'¤'= >
List
¤'¤'? C
<
¤'¤'C D
int
¤'¤'D G
>
¤'¤'G H
seleccionados
¤'¤'I V
,
¤'¤'V W
List
¤'¤'X \
<
¤'¤'\ ]
int
¤'¤'] `
>
¤'¤'` a
seleccionados2
¤'¤'b p
,
¤'¤'p q
ref
¤'¤'r u!
MessageResponseOBJ¤'¤'v ˆ
MsgRes¤'¤'‰ 
)¤'¤' 
{
¥'¥' 	

DACInserta
¦'¦' 
.
¦'¦' $
InsertarAdminCriterios
¦'¦' -
(
¦'¦'- .
tipoadherencia
¦'¦'. <
,
¦'¦'< =
seleccionados
¦'¦'> K
,
¦'¦'K L
seleccionados2
¦'¦'M [
,
¦'¦'[ \
ref
¦'¦'] `
MsgRes
¦'¦'a g
)
¦'¦'g h
;
¦'¦'h i
}
§'§' 	
public
©'©' 
List
©'©' 
<
©'©' !
ref_adherencia_unis
©'©' '
>
©'©'' (
GetUnisByRegional
©'©') :
(
©'©': ;
int
©'©'; >

idregional
©'©'? I
)
©'©'I J
{
ª'ª' 	
return
«'«' 
DACConsulta
«'«' 
.
«'«' 
GetUnisByRegional
«'«' 0
(
«'«'0 1

idregional
«'«'1 ;
)
«'«'; <
;
«'«'< =
}
¬'¬' 	
public
®'®' 
List
®'®' 
<
®'®' #
ref_adherencia_ciudad
®'®' )
>
®'®') *
GetciudadByunis
®'®'+ :
(
®'®': ;
int
®'®'; >
idunis
®'®'? E
)
®'®'E F
{
¯'¯' 	
return
°'°' 
DACConsulta
°'°' 
.
°'°' 
GetciudadByunis
°'°' .
(
°'°'. /
idunis
°'°'/ 5
)
°'°'5 6
;
°'°'6 7
}
±'±' 	
public
³'³' 
List
³'³' 
<
³'³' -
ref_adherencia_prestador_ciudad
³'³' 3
>
³'³'3 4$
GetPrestadoresByciudad
³'³'5 K
(
³'³'K L
int
³'³'L O
idciudad
³'³'P X
)
³'³'X Y
{
´'´' 	
return
µ'µ' 
DACConsulta
µ'µ' 
.
µ'µ' $
GetPrestadoresByciudad
µ'µ' 5
(
µ'µ'5 6
idciudad
µ'µ'6 >
)
µ'µ'> ?
;
µ'µ'? @
}
¶'¶' 	
public
¸'¸' 
List
¸'¸' 
<
¸'¸' 2
$ref_adherencia_profesional_prestador
¸'¸' 8
>
¸'¸'8 9)
GetProfesionalesByprestador
¸'¸': U
(
¸'¸'U V
int
¸'¸'V Y
idprestador
¸'¸'Z e
)
¸'¸'e f
{
¹'¹' 	
return
º'º' 
DACConsulta
º'º' 
.
º'º' )
GetProfesionalesByprestador
º'º' :
(
º'º': ;
idprestador
º'º'; F
)
º'º'F G
;
º'º'G H
}
»'»' 	
public
¿'¿' 
List
¿'¿' 
<
¿'¿' (
Ref_odont_list_check_ortod
¿'¿' .
>
¿'¿'. /
getcheckOrtod
¿'¿'0 =
(
¿'¿'= >
)
¿'¿'> ?
{
À'À' 	
return
Á'Á' 
DACConsulta
Á'Á' 
.
Á'Á' 
getcheckOrtod
Á'Á' ,
(
Á'Á', -
)
Á'Á'- .
;
Á'Á'. /
}
Â'Â' 	
public
Ä'Ä' 
List
Ä'Ä' 
<
Ä'Ä' )
Ref_odont_check_porcentajes
Ä'Ä' /
>
Ä'Ä'/ 0 
getcheckPorcentaje
Ä'Ä'1 C
(
Ä'Ä'C D
)
Ä'Ä'D E
{
Å'Å' 	
return
Æ'Æ' 
DACConsulta
Æ'Æ' 
.
Æ'Æ'  
getcheckPorcentaje
Æ'Æ' 1
(
Æ'Æ'1 2
)
Æ'Æ'2 3
;
Æ'Æ'3 4
}
Ç'Ç' 	
public
É'É' 
List
É'É' 
<
É'É' '
Ref_odont_tipo_endodoncia
É'É' -
>
É'É'- .#
getListTipoEndodoncia
É'É'/ D
(
É'É'D E
)
É'É'E F
{
Ê'Ê' 	
return
Ë'Ë' 
DACConsulta
Ë'Ë' 
.
Ë'Ë' #
getListTipoEndodoncia
Ë'Ë' 4
(
Ë'Ë'4 5
)
Ë'Ë'5 6
;
Ë'Ë'6 7
}
Ì'Ì' 	
public
Î'Î' 
List
Î'Î' 
<
Î'Î' ,
Ref_odont_parametros_auditados
Î'Î' 2
>
Î'Î'2 3(
getListParametrosAuditados
Î'Î'4 N
(
Î'Î'N O
)
Î'Î'O P
{
Ï'Ï' 	
return
Ğ'Ğ' 
DACConsulta
Ğ'Ğ' 
.
Ğ'Ğ' (
getListParametrosAuditados
Ğ'Ğ' 9
(
Ğ'Ğ'9 :
)
Ğ'Ğ': ;
;
Ğ'Ğ'; <
}
Ñ'Ñ' 	
public
Ô'Ô' 
Int32
Ô'Ô' %
InsertarOdontOrtodoncia
Ô'Ô' ,
(
Ô'Ô', -*
odont_tratamiento_ortodoncia
Ô'Ô'- I
OBJ
Ô'Ô'J M
,
Ô'Ô'M N
ref
Ô'Ô'O R 
MessageResponseOBJ
Ô'Ô'S e
MsgRes
Ô'Ô'f l
)
Ô'Ô'l m
{
Õ'Õ' 	
return
Ö'Ö' 

DACInserta
Ö'Ö' 
.
Ö'Ö' %
InsertarOdontOrtodoncia
Ö'Ö' 5
(
Ö'Ö'5 6
OBJ
Ö'Ö'6 9
,
Ö'Ö'9 :
ref
Ö'Ö'; >
MsgRes
Ö'Ö'? E
)
Ö'Ö'E F
;
Ö'Ö'F G
}
×'×' 	
public
Ù'Ù' 
Int32
Ù'Ù' ,
InsertarOdontOrtodonciaDetalle
Ù'Ù' 3
(
Ù'Ù'3 42
$odont_tratamiento_ortodoncia_detalle
Ù'Ù'4 X
OBJ
Ù'Ù'Y \
,
Ù'Ù'\ ]
ref
Ù'Ù'^ a 
MessageResponseOBJ
Ù'Ù'b t
MsgRes
Ù'Ù'u {
)
Ù'Ù'{ |
{
Ú'Ú' 	
return
Û'Û' 

DACInserta
Û'Û' 
.
Û'Û' ,
InsertarOdontOrtodonciaDetalle
Û'Û' <
(
Û'Û'< =
OBJ
Û'Û'= @
,
Û'Û'@ A
ref
Û'Û'B E
MsgRes
Û'Û'F L
)
Û'Û'L M
;
Û'Û'M N
}
Ü'Ü' 	
public
Ş'Ş' 
Int32
Ş'Ş' %
InsertarOdontEndodoncia
Ş'Ş' ,
(
Ş'Ş', -*
odont_tratamiento_endodoncia
Ş'Ş'- I
OBJ
Ş'Ş'J M
,
Ş'Ş'M N
ref
Ş'Ş'O R 
MessageResponseOBJ
Ş'Ş'S e
MsgRes
Ş'Ş'f l
)
Ş'Ş'l m
{
ß'ß' 	
return
à'à' 

DACInserta
à'à' 
.
à'à' %
InsertarOdontEndodoncia
à'à' 5
(
à'à'5 6
OBJ
à'à'6 9
,
à'à'9 :
ref
à'à'; >
MsgRes
à'à'? E
)
à'à'E F
;
à'à'F G
}
á'á' 	
public
ã'ã' 
Int32
ã'ã' 
InsertarOdontFija
ã'ã' &
(
ã'ã'& '5
'odont_rehabilitacion_oral_protesis_fija
ã'ã'' N
OBJ
ã'ã'O R
,
ã'ã'R S
ref
ã'ã'T W 
MessageResponseOBJ
ã'ã'X j
MsgRes
ã'ã'k q
)
ã'ã'q r
{
ä'ä' 	
return
å'å' 

DACInserta
å'å' 
.
å'å' 
InsertarOdontFija
å'å' /
(
å'å'/ 0
OBJ
å'å'0 3
,
å'å'3 4
ref
å'å'5 8
MsgRes
å'å'9 ?
)
å'å'? @
;
å'å'@ A
}
æ'æ' 	
public
è'è' 
void
è'è' #
InsertarOdontFijaDtll
è'è' )
(
è'è') *
List
è'è'* .
<
è'è'. /:
,odont_rehabilitacion_oral_protesis_fija_dtll
è'è'/ [
>
è'è'[ \
OBJ
è'è'] `
,
è'è'` a
ref
è'è'b e 
MessageResponseOBJ
è'è'f x
MsgRes
è'è'y 
)è'è' €
{
é'é' 	

DACInserta
ê'ê' 
.
ê'ê' #
InsertarOdontFijaDtll
ê'ê' ,
(
ê'ê', -
OBJ
ê'ê'- 0
,
ê'ê'0 1
ref
ê'ê'2 5
MsgRes
ê'ê'6 <
)
ê'ê'< =
;
ê'ê'= >
}
ë'ë' 	
public
í'í' 
Int32
í'í' $
InsertarOdontRemovible
í'í' +
(
í'í'+ ,;
-odont_rehabilitacion_oral_protesis_removibles
í'í', Y
OBJ
í'í'Z ]
,
í'í'] ^
ref
í'í'_ b 
MessageResponseOBJ
í'í'c u
MsgRes
í'í'v |
)
í'í'| }
{
î'î' 	
return
ï'ï' 

DACInserta
ï'ï' 
.
ï'ï' $
InsertarOdontRemovible
ï'ï' 4
(
ï'ï'4 5
OBJ
ï'ï'5 8
,
ï'ï'8 9
ref
ï'ï': =
MsgRes
ï'ï'> D
)
ï'ï'D E
;
ï'ï'E F
}
ğ'ğ' 	
public
ò'ò' 
List
ò'ò' 
<
ò'ò' (
vw_odont_ortodoncia_report
ò'ò' .
>
ò'ò'. /)
ConsultaIdReporteOrtodoncia
ò'ò'0 K
(
ò'ò'K L
Int32
ò'ò'L Q'
id_tratamiento_ortodoncia
ò'ò'R k
,
ò'ò'k l
ref
ò'ò'm p!
MessageResponseOBJò'ò'q ƒ
MsgResò'ò'„ Š
)ò'ò'Š ‹
{
ó'ó' 	
return
ô'ô' 
DACConsulta
ô'ô' 
.
ô'ô' )
ConsultaIdReporteOrtodoncia
ô'ô' :
(
ô'ô': ;'
id_tratamiento_ortodoncia
ô'ô'; T
,
ô'ô'T U
ref
ô'ô'V Y
MsgRes
ô'ô'Z `
)
ô'ô'` a
;
ô'ô'a b
}
õ'õ' 	
public
ö'ö' 
Int32
ö'ö' (
InsertarOdontRemovibledtll
ö'ö' /
(
ö'ö'/ 0@
2odont_rehabilitacion_oral_protesis_removibles_dtll
ö'ö'0 b
OBJ
ö'ö'c f
,
ö'ö'f g
ref
ö'ö'h k 
MessageResponseOBJ
ö'ö'l ~
MsgResö'ö' …
)ö'ö'… †
{
÷'÷' 	
return
ø'ø' 

DACInserta
ø'ø' 
.
ø'ø' (
InsertarOdontRemovibledtll
ø'ø' 8
(
ø'ø'8 9
OBJ
ø'ø'9 <
,
ø'ø'< =
ref
ø'ø'> A
MsgRes
ø'ø'B H
)
ø'ø'H I
;
ø'ø'I J
}
ù'ù' 	
public
û'û' 
Int32
û'û' )
InsertarOdontEndodonciadtll
û'û' 0
(
û'û'0 1/
!odont_tratamiento_endodoncia_dtll
û'û'1 R
OBJ
û'û'S V
,
û'û'V W
ref
û'û'X [ 
MessageResponseOBJ
û'û'\ n
MsgRes
û'û'o u
)
û'û'u v
{
ü'ü' 	
return
ı'ı' 

DACInserta
ı'ı' 
.
ı'ı' )
InsertarOdontEndodonciadtll
ı'ı' 9
(
ı'ı'9 :
OBJ
ı'ı': =
,
ı'ı'= >
ref
ı'ı'? B
MsgRes
ı'ı'C I
)
ı'ı'I J
;
ı'ı'J K
}
ş'ş' 	
public
€(€( 
List
€(€( 
<
€(€( '
vw_odont_removible_report
€(€( -
>
€(€(- .(
ConsultaIdReporteRemovible
€(€(/ I
(
€(€(I J
Int32
€(€(J O8
*id_rehabilitacion_oral_protesis_removibles
€(€(P z
,
€(€(z {
ref
€(€(| "
MessageResponseOBJ€(€(€ ’
MsgRes€(€(“ ™
)€(€(™ š
{
(( 	
return
‚(‚( 
DACConsulta
‚(‚( 
.
‚(‚( (
ConsultaIdReporteRemovible
‚(‚( 9
(
‚(‚(9 :8
*id_rehabilitacion_oral_protesis_removibles
‚(‚(: d
,
‚(‚(d e
ref
‚(‚(f i
MsgRes
‚(‚(j p
)
‚(‚(p q
;
‚(‚(q r
}
ƒ(ƒ( 	
public
„(„( 
List
„(„( 
<
„(„( (
vw_odont_endodoncia_report
„(„( .
>
„(„(. /)
ConsultaIdReporteEndodoncia
„(„(0 K
(
„(„(K L
Int32
„(„(L Q'
id_tratamiento_endodoncia
„(„(R k
,
„(„(k l
ref
„(„(m p!
MessageResponseOBJ„(„(q ƒ
MsgRes„(„(„ Š
)„(„(Š ‹
{
…(…( 	
return
†(†( 
DACConsulta
†(†( 
.
†(†( )
ConsultaIdReporteEndodoncia
†(†( :
(
†(†(: ;'
id_tratamiento_endodoncia
†(†(; T
,
†(†(T U
ref
†(†(V Y
MsgRes
†(†(Z `
)
†(†(` a
;
†(†(a b
}
‡(‡( 	
public
‰(‰( 
List
‰(‰( 
<
‰(‰( "
vw_odont_fija_report
‰(‰( (
>
‰(‰(( )+
ConsultaIdReporteProtesisFija
‰(‰(* G
(
‰(‰(G H
Int32
‰(‰(H M*
id_tratamiento_Protesis_Fija
‰(‰(N j
,
‰(‰(j k
ref
‰(‰(l o!
MessageResponseOBJ‰(‰(p ‚
MsgRes‰(‰(ƒ ‰
)‰(‰(‰ Š
{
Š(Š( 	
return
‹(‹( 
DACConsulta
‹(‹( 
.
‹(‹( +
ConsultaIdReporteProtesisFija
‹(‹( <
(
‹(‹(< =*
id_tratamiento_Protesis_Fija
‹(‹(= Y
,
‹(‹(Y Z
ref
‹(‹([ ^
MsgRes
‹(‹(_ e
)
‹(‹(e f
;
‹(‹(f g
}
Œ(Œ( 	
public
(( 
List
(( 
<
(( :
,odont_rehabilitacion_oral_protesis_fija_dtll
(( @
>
((@ A/
!ConsultaIdReporteProtesisFijaDtll
((B c
(
((c d
Int32
((d i+
id_tratamiento_Protesis_Fija((j †
,((† ‡
ref((ˆ ‹"
MessageResponseOBJ((Œ 
MsgRes((Ÿ ¥
)((¥ ¦
{
(( 	
return
(( 
DACConsulta
(( 
.
(( /
!ConsultaIdReporteProtesisFijaDtll
(( @
(
((@ A*
id_tratamiento_Protesis_Fija
((A ]
,
((] ^
ref
((_ b
MsgRes
((c i
)
((i j
;
((j k
}
‘(‘( 	
public
“(“( 
List
“(“( 
<
“(“( )
vw_odont_porcentaje_d1_fija
“(“( /
>
“(“(/ 0#
Getporcentaje_d1_fija
“(“(1 F
(
“(“(F G
Int32
“(“(G L
id_protesis_fija
“(“(M ]
,
“(“(] ^
ref
“(“(_ b 
MessageResponseOBJ
“(“(c u
MsgRes
“(“(v |
)
“(“(| }
{
”(”( 	
return
•(•( 
DACConsulta
•(•( 
.
•(•( #
Getporcentaje_d1_fija
•(•( 4
(
•(•(4 5
id_protesis_fija
•(•(5 E
,
•(•(E F
ref
•(•(G J
MsgRes
•(•(K Q
)
•(•(Q R
;
•(•(R S
}
–(–( 	
public
˜(˜( 
List
˜(˜( 
<
˜(˜( )
vw_odont_porcentaje_d2_fija
˜(˜( /
>
˜(˜(/ 0#
Getporcentaje_d2_fija
˜(˜(1 F
(
˜(˜(F G
Int32
˜(˜(G L
id_protesis_fija
˜(˜(M ]
,
˜(˜(] ^
ref
˜(˜(_ b 
MessageResponseOBJ
˜(˜(c u
MsgRes
˜(˜(v |
)
˜(˜(| }
{
™(™( 	
return
š(š( 
DACConsulta
š(š( 
.
š(š( #
Getporcentaje_d2_fija
š(š( 4
(
š(š(4 5
id_protesis_fija
š(š(5 E
,
š(š(E F
ref
š(š(G J
MsgRes
š(š(K Q
)
š(š(Q R
;
š(š(R S
}
›(›( 	
public
(( 
List
(( 
<
(( -
vw_odont_reporte_removible_dtll
(( 3
>
((3 44
&ConsultaIdReporteProtesisRemovibleDtll
((5 [
(
(([ \
Int32
((\ a9
*id_rehabilitacion_oral_protesis_removibles((b Œ
,((Œ 
ref(( ‘"
MessageResponseOBJ((’ ¤
MsgRes((¥ «
)((« ¬
{
Ÿ(Ÿ( 	
return
 ( ( 
DACConsulta
 ( ( 
.
 ( ( 4
&ConsultaIdReporteProtesisRemovibleDtll
 ( ( E
(
 ( (E F8
*id_rehabilitacion_oral_protesis_removibles
 ( (F p
,
 ( (p q
ref
 ( (r u
MsgRes
 ( (v |
)
 ( (| }
;
 ( (} ~
}
¡(¡( 	
public
£(£( 
List
£(£( 
<
£(£( *
vw_odont_tableros_ortodoncia
£(£( 0
>
£(£(0 1*
ConsultaListadoTTOsOrodoncia
£(£(2 N
(
£(£(N O
ref
£(£(O R 
MessageResponseOBJ
£(£(S e
MsgRes
£(£(f l
)
£(£(l m
{
¤(¤( 	
return
¥(¥( 
DACConsulta
¥(¥( 
.
¥(¥( *
ConsultaListadoTTOsOrodoncia
¥(¥( ;
(
¥(¥(; <
ref
¥(¥(< ?
MsgRes
¥(¥(@ F
)
¥(¥(F G
;
¥(¥(G H
}
¦(¦( 	
public
¨(¨( 
List
¨(¨( 
<
¨(¨( /
!vw_odont_tableros_ortodoncia_prof
¨(¨( 5
>
¨(¨(5 6.
 ConsultaListadoTTOsOrodonciaProf
¨(¨(7 W
(
¨(¨(W X
ref
¨(¨(X [ 
MessageResponseOBJ
¨(¨(\ n
MsgRes
¨(¨(o u
)
¨(¨(u v
{
©(©( 	
return
ª(ª( 
DACConsulta
ª(ª( 
.
ª(ª( .
 ConsultaListadoTTOsOrodonciaProf
ª(ª( ?
(
ª(ª(? @
ref
ª(ª(@ C
MsgRes
ª(ª(D J
)
ª(ª(J K
;
ª(ª(K L
}
«(«( 	
public
­(­( 
List
­(­( 
<
­(­( ,
vw_odont_tableros_ProtesisFija
­(­( 2
>
­(­(2 3$
ConsultaListadoTTOsPPF
­(­(4 J
(
­(­(J K
ref
­(­(K N 
MessageResponseOBJ
­(­(O a
MsgRes
­(­(b h
)
­(­(h i
{
®(®( 	
return
¯(¯( 
DACConsulta
¯(¯( 
.
¯(¯( $
ConsultaListadoTTOsPPF
¯(¯( 5
(
¯(¯(5 6
ref
¯(¯(6 9
MsgRes
¯(¯(: @
)
¯(¯(@ A
;
¯(¯(A B
}
°(°( 	
public
²(²( 
List
²(²( 
<
²(²( 1
#vw_odont_tableros_ProtesisFija_prof
²(²( 7
>
²(²(7 8%
ConsultaListadoTTOsProf
²(²(9 P
(
²(²(P Q
ref
²(²(Q T 
MessageResponseOBJ
²(²(U g
MsgRes
²(²(h n
)
²(²(n o
{
³(³( 	
return
´(´( 
DACConsulta
´(´( 
.
´(´( %
ConsultaListadoTTOsProf
´(´( 6
(
´(´(6 7
ref
´(´(7 :
MsgRes
´(´(; A
)
´(´(A B
;
´(´(B C
}
µ(µ( 	
public
¸(¸( 
List
¸(¸( 
<
¸(¸( -
vw_odont_tableros_ProtesisRemov
¸(¸( 3
>
¸(¸(3 4*
ConsultaListadoTTOsRemovible
¸(¸(5 Q
(
¸(¸(Q R
ref
¸(¸(R U 
MessageResponseOBJ
¸(¸(V h
MsgRes
¸(¸(i o
)
¸(¸(o p
{
¹(¹( 	
return
º(º( 
DACConsulta
º(º( 
.
º(º( *
ConsultaListadoTTOsRemovible
º(º( ;
(
º(º(; <
ref
º(º(< ?
MsgRes
º(º(@ F
)
º(º(F G
;
º(º(G H
}
»(»( 	
public
½(½( 
List
½(½( 
<
½(½( 2
$vw_odont_tableros_ProtesisRemov_prof
½(½( 8
>
½(½(8 9/
!ConsultaListadoTTOsRemoviblesProf
½(½(: [
(
½(½([ \
ref
½(½(\ _ 
MessageResponseOBJ
½(½(` r
MsgRes
½(½(s y
)
½(½(y z
{
¾(¾( 	
return
¿(¿( 
DACConsulta
¿(¿( 
.
¿(¿( /
!ConsultaListadoTTOsRemoviblesProf
¿(¿( @
(
¿(¿(@ A
ref
¿(¿(A D
MsgRes
¿(¿(E K
)
¿(¿(K L
;
¿(¿(L M
}
À(À( 	
public
Â(Â( 
List
Â(Â( 
<
Â(Â( *
vw_odont_tableros_endodoncia
Â(Â( 0
>
Â(Â(0 1+
ConsultaListadoTTOsEndodoncia
Â(Â(2 O
(
Â(Â(O P
ref
Â(Â(P S 
MessageResponseOBJ
Â(Â(T f
MsgRes
Â(Â(g m
)
Â(Â(m n
{
Ã(Ã( 	
return
Ä(Ä( 
DACConsulta
Ä(Ä( 
.
Ä(Ä( +
ConsultaListadoTTOsEndodoncia
Ä(Ä( <
(
Ä(Ä(< =
ref
Ä(Ä(= @
MsgRes
Ä(Ä(A G
)
Ä(Ä(G H
;
Ä(Ä(H I
}
Å(Å( 	
public
Ç(Ç( 
List
Ç(Ç( 
<
Ç(Ç( /
!vw_odont_tableros_endodoncia_prof
Ç(Ç( 5
>
Ç(Ç(5 6+
ConsultaListadoEndodonciaProf
Ç(Ç(7 T
(
Ç(Ç(T U
ref
Ç(Ç(U X 
MessageResponseOBJ
Ç(Ç(Y k
MsgRes
Ç(Ç(l r
)
Ç(Ç(r s
{
È(È( 	
return
É(É( 
DACConsulta
É(É( 
.
É(É( +
ConsultaListadoEndodonciaProf
É(É( <
(
É(É(< =
ref
É(É(= @
MsgRes
É(É(A G
)
É(É(G H
;
É(É(H I
}
Ê(Ê( 	
public
Ì(Ì( 
List
Ì(Ì( 
<
Ì(Ì( '
Ref_odont_acciones_mejora
Ì(Ì( -
>
Ì(Ì(- .#
GetListAccionesMejora
Ì(Ì(/ D
(
Ì(Ì(D E
)
Ì(Ì(E F
{
Í(Í( 	
return
Î(Î( 
DACConsulta
Î(Î( 
.
Î(Î( #
GetListAccionesMejora
Î(Î( 4
(
Î(Î(4 5
)
Î(Î(5 6
;
Î(Î(6 7
}
Ï(Ï( 	
public
Ñ(Ñ( 
List
Ñ(Ñ( 
<
Ñ(Ñ( %
Ref_odont_estado_accion
Ñ(Ñ( +
>
Ñ(Ñ(+ ,*
GetListEstadosAccionesMejora
Ñ(Ñ(- I
(
Ñ(Ñ(I J
)
Ñ(Ñ(J K
{
Ò(Ò( 	
return
Ó(Ó( 
DACConsulta
Ó(Ó( 
.
Ó(Ó( *
GetListEstadosAccionesMejora
Ó(Ó( ;
(
Ó(Ó(; <
)
Ó(Ó(< =
;
Ó(Ó(= >
}
Ô(Ô( 	
public
Ö(Ö( 
List
Ö(Ö( 
<
Ö(Ö( &
vw_odont_tbl_prestadores
Ö(Ö( ,
>
Ö(Ö(, -&
GetprestadoresPlanMejora
Ö(Ö(. F
(
Ö(Ö(F G
)
Ö(Ö(G H
{
×(×( 	
return
Ø(Ø( 
DACConsulta
Ø(Ø( 
.
Ø(Ø( &
GetprestadoresPlanMejora
Ø(Ø( 7
(
Ø(Ø(7 8
)
Ø(Ø(8 9
;
Ø(Ø(9 :
}
Ù(Ù( 	
public
Û(Û( 
List
Û(Û( 
<
Û(Û( $
vw_odont_planes_mejora
Û(Û( *
>
Û(Û(* +
GetPlanesMejora
Û(Û(, ;
(
Û(Û(; <
)
Û(Û(< =
{
Ü(Ü( 	
return
İ(İ( 
DACConsulta
İ(İ( 
.
İ(İ( 
GetPlanesMejora
İ(İ( .
(
İ(İ(. /
)
İ(İ(/ 0
;
İ(İ(0 1
}
Ş(Ş( 	
public
à(à( 
Int32
à(à( +
InsertarPlanMejoraTratamiento
à(à( 2
(
à(à(2 3+
odont_plan_mejora_tratamiento
à(à(3 P
OBJ
à(à(Q T
,
à(à(T U
ref
à(à(V Y 
MessageResponseOBJ
à(à(Z l
MsgRes
à(à(m s
)
à(à(s t
{
á(á( 	
return
â(â( 

DACInserta
â(â( 
.
â(â( +
InsertarPlanMejoraTratamiento
â(â( ;
(
â(â(; <
OBJ
â(â(< ?
,
â(â(? @
ref
â(â(A D
MsgRes
â(â(E K
)
â(â(K L
;
â(â(L M
}
ã(ã( 	
public
å(å( 
Int32
å(å( 2
$InsertarPlanMejoraTratamientodetalle
å(å( 9
(
å(å(9 :0
"odont_plan_mejora_tratamiento_dtll
å(å(: \
OBJ
å(å(] `
,
å(å(` a
ref
å(å(b e 
MessageResponseOBJ
å(å(f x
MsgRes
å(å(y 
)å(å( €
{
æ(æ( 	
return
ç(ç( 

DACInserta
ç(ç( 
.
ç(ç( 2
$InsertarPlanMejoraTratamientodetalle
ç(ç( B
(
ç(ç(B C
OBJ
ç(ç(C F
,
ç(ç(F G
ref
ç(ç(H K
MsgRes
ç(ç(L R
)
ç(ç(R S
;
ç(ç(S T
}
è(è( 	
public
ê(ê( 
Int32
ê(ê( ,
InsertarPlanMejoraBeneficiario
ê(ê( 3
(
ê(ê(3 4,
odont_plan_mejora_beneficiario
ê(ê(4 R
OBJ
ê(ê(S V
,
ê(ê(V W
ref
ê(ê(X [ 
MessageResponseOBJ
ê(ê(\ n
MsgRes
ê(ê(o u
)
ê(ê(u v
{
ë(ë( 	
return
ì(ì( 

DACInserta
ì(ì( 
.
ì(ì( ,
InsertarPlanMejoraBeneficiario
ì(ì( <
(
ì(ì(< =
OBJ
ì(ì(= @
,
ì(ì(@ A
ref
ì(ì(B E
MsgRes
ì(ì(F L
)
ì(ì(L M
;
ì(ì(M N
}
í(í( 	
public
ï(ï( 
Int32
ï(ï( 3
%InsertarPlanMejoraBeneficiariodetalle
ï(ï( :
(
ï(ï(: ;1
#odont_plan_mejora_beneficiario_dtll
ï(ï(; ^
OBJ
ï(ï(_ b
,
ï(ï(b c
ref
ï(ï(d g 
MessageResponseOBJ
ï(ï(h z
MsgResï(ï({ 
)ï(ï( ‚
{
ğ(ğ( 	
return
ñ(ñ( 

DACInserta
ñ(ñ( 
.
ñ(ñ( 3
%InsertarPlanMejoraBeneficiariodetalle
ñ(ñ( C
(
ñ(ñ(C D
OBJ
ñ(ñ(D G
,
ñ(ñ(G H
ref
ñ(ñ(I L
MsgRes
ñ(ñ(M S
)
ñ(ñ(S T
;
ñ(ñ(T U
}
ò(ò( 	
public
ô(ô( 
void
ô(ô( '
ActualizarOdontPlanMejora
ô(ô( -
(
ô(ô(- .$
odont_plan_mejora_dtll
ô(ô(. D
obj2
ô(ô(E I
,
ô(ô(I J
ref
ô(ô(K N 
MessageResponseOBJ
ô(ô(O a
MsgRes
ô(ô(b h
)
ô(ô(h i
{
õ(õ( 	
DACActualiza
ö(ö( 
.
ö(ö( '
ActualizarOdontPlanMejora
ö(ö( 2
(
ö(ö(2 3
obj2
ö(ö(3 7
,
ö(ö(7 8
ref
ö(ö(9 <
MsgRes
ö(ö(= C
)
ö(ö(C D
;
ö(ö(D E
}
÷(÷( 	
public
ù(ù( 
void
ù(ù( 3
%ActualizarOdontPlanMejoraBeneficiario
ù(ù( 9
(
ù(ù(9 :1
#odont_plan_mejora_beneficiario_dtll
ù(ù(: ]
obj2
ù(ù(^ b
,
ù(ù(b c
ref
ù(ù(d g 
MessageResponseOBJ
ù(ù(h z
MsgResù(ù({ 
)ù(ù( ‚
{
ú(ú( 	
DACActualiza
û(û( 
.
û(û( 3
%ActualizarOdontPlanMejoraBeneficiario
û(û( >
(
û(û(> ?
obj2
û(û(? C
,
û(û(C D
ref
û(û(E H
MsgRes
û(û(I O
)
û(û(O P
;
û(û(P Q
}
ü(ü( 	
public
ş(ş( 
List
ş(ş( 
<
ş(ş( 
odont_plan_mejora
ş(ş( %
>
ş(ş(% &
GetPlanMejoraTra
ş(ş(' 7
(
ş(ş(7 8
)
ş(ş(8 9
{
ÿ(ÿ( 	
return
€)€) 
DACConsulta
€)€) 
.
€)€) 
GetPlanMejoraTra
€)€) /
(
€)€)/ 0
)
€)€)0 1
;
€)€)1 2
}
)) 	
public
ƒ)ƒ) 
List
ƒ)ƒ) 
<
ƒ)ƒ) ,
odont_plan_mejora_beneficiario
ƒ)ƒ) 2
>
ƒ)ƒ)2 3
GetPlanMejoraBen
ƒ)ƒ)4 D
(
ƒ)ƒ)D E
)
ƒ)ƒ)E F
{
„)„) 	
return
…)…) 
DACConsulta
…)…) 
.
…)…) 
GetPlanMejoraBen
…)…) /
(
…)…)/ 0
)
…)…)0 1
;
…)…)1 2
}
†)†) 	
public
ˆ)ˆ) 
List
ˆ)ˆ) 
<
ˆ)ˆ) $
vw_odont_planes_mejora
ˆ)ˆ) *
>
ˆ)ˆ)* +"
GetPlanMejoraTradtll
ˆ)ˆ), @
(
ˆ)ˆ)@ A
Int32
ˆ)ˆ)A F"
id_odont_plan_mejora
ˆ)ˆ)G [
,
ˆ)ˆ)[ \
ref
ˆ)ˆ)] ` 
MessageResponseOBJ
ˆ)ˆ)a s
MsgRes
ˆ)ˆ)t z
)
ˆ)ˆ)z {
{
‰)‰) 	
return
Š)Š) 
DACConsulta
Š)Š) 
.
Š)Š) "
GetPlanMejoraTradtll
Š)Š) 3
(
Š)Š)3 4"
id_odont_plan_mejora
Š)Š)4 H
,
Š)Š)H I
ref
Š)Š)J M
MsgRes
Š)Š)N T
)
Š)Š)T U
;
Š)Š)U V
}
‹)‹) 	
public
)) 
List
)) 
<
)) (
vw_odont_planes_mejora_ben
)) .
>
)). /"
GetPlanMejoraBendtll
))0 D
(
))D E
Int32
))E J/
!id_odont_plan_mejora_beneficiario
))K l
,
))l m
ref
))n q!
MessageResponseOBJ))r „
MsgRes))… ‹
)))‹ Œ
{
)) 	
return
)) 
DACConsulta
)) 
.
)) "
GetPlanMejoraBendtll
)) 3
(
))3 4/
!id_odont_plan_mejora_beneficiario
))4 U
,
))U V
ref
))W Z
MsgRes
))[ a
)
))a b
;
))b c
}
)) 	
public
’)’) 
void
’)’) 5
'ActualizarOdontPlanMejoraObsTratamiento
’)’) ;
(
’)’); <
odont_plan_mejora
’)’)< M
obj2
’)’)N R
,
’)’)R S
ref
’)’)T W 
MessageResponseOBJ
’)’)X j
MsgRes
’)’)k q
)
’)’)q r
{
“)“) 	
DACActualiza
”)”) 
.
”)”) 5
'ActualizarOdontPlanMejoraObsTratamiento
”)”) @
(
”)”)@ A
obj2
”)”)A E
,
”)”)E F
ref
”)”)G J
MsgRes
”)”)K Q
)
”)”)Q R
;
”)”)R S
}
•)•) 	
public
—)—) 
void
—)—) 6
(ActualizarOdontPlanMejoraObsBeneficiario
—)—) <
(
—)—)< =,
odont_plan_mejora_beneficiario
—)—)= [
obj2
—)—)\ `
,
—)—)` a
ref
—)—)b e 
MessageResponseOBJ
—)—)f x
MsgRes
—)—)y 
)—)—) €
{
˜)˜) 	
DACActualiza
™)™) 
.
™)™) 6
(ActualizarOdontPlanMejoraObsBeneficiario
™)™) A
(
™)™)A B
obj2
™)™)B F
,
™)™)F G
ref
™)™)H K
MsgRes
™)™)L R
)
™)™)R S
;
™)™)S T
}
š)š) 	
public
œ)œ) 
List
œ)œ) 
<
œ)œ) (
vw_tablero_plan_mejora_ben
œ)œ) .
>
œ)œ). /$
ConsultaTableroPlanBen
œ)œ)0 F
(
œ)œ)F G
)
œ)œ)G H
{
)) 	
return
)) 
DACConsulta
)) 
.
)) $
ConsultaTableroPlanBen
)) 5
(
))5 6
)
))6 7
;
))7 8
}
Ÿ)Ÿ) 	
public
¡)¡) 
void
¡)¡) 1
#ActualizarOdontPlanMejoraCierreTrat
¡)¡) 7
(
¡)¡)7 8
odont_plan_mejora
¡)¡)8 I
obj2
¡)¡)J N
,
¡)¡)N O
ref
¡)¡)P S 
MessageResponseOBJ
¡)¡)T f
MsgRes
¡)¡)g m
)
¡)¡)m n
{
¢)¢) 	
DACActualiza
£)£) 
.
£)£) 1
#ActualizarOdontPlanMejoraCierreTrat
£)£) <
(
£)£)< =
obj2
£)£)= A
,
£)£)A B
ref
£)£)C F
MsgRes
£)£)G M
)
£)£)M N
;
£)£)N O
}
¤)¤) 	
public
¥)¥) 
void
¥)¥) 0
"ActualizarOdontPlanMejoraCierreBen
¥)¥) 6
(
¥)¥)6 7,
odont_plan_mejora_beneficiario
¥)¥)7 U
obj2
¥)¥)V Z
,
¥)¥)Z [
ref
¥)¥)\ _ 
MessageResponseOBJ
¥)¥)` r
MsgRes
¥)¥)s y
)
¥)¥)y z
{
¦)¦) 	
DACActualiza
§)§) 
.
§)§) 0
"ActualizarOdontPlanMejoraCierreBen
§)§) ;
(
§)§); <
obj2
§)§)< @
,
§)§)@ A
ref
§)§)B E
MsgRes
§)§)F L
)
§)§)L M
;
§)§)M N
}
¨)¨) 	
public
ª)ª) 
Int32
ª)ª) %
InsertarHistoriaClinica
ª)ª) ,
(
ª)ª), -$
odont_historia_clinica
ª)ª)- C
OBJ
ª)ª)D G
,
ª)ª)G H
ref
ª)ª)I L 
MessageResponseOBJ
ª)ª)M _
MsgRes
ª)ª)` f
)
ª)ª)f g
{
«)«) 	
return
¬)¬) 

DACInserta
¬)¬) 
.
¬)¬) %
InsertarHistoriaClinica
¬)¬) 5
(
¬)¬)5 6
OBJ
¬)¬)6 9
,
¬)¬)9 :
ref
¬)¬); >
MsgRes
¬)¬)? E
)
¬)¬)E F
;
¬)¬)F G
}
­)­) 	
public
®)®) 
Int32
®)®) -
InsertarHistoriaClinicaPaciente
®)®) 4
(
®)®)4 5-
odont_historia_clinica_paciente
®)®)5 T
OBJ
®)®)U X
,
®)®)X Y
ref
®)®)Z ] 
MessageResponseOBJ
®)®)^ p
MsgRes
®)®)q w
)
®)®)w x
{
¯)¯) 	
return
°)°) 

DACInserta
°)°) 
.
°)°) -
InsertarHistoriaClinicaPaciente
°)°) =
(
°)°)= >
OBJ
°)°)> A
,
°)°)A B
ref
°)°)C F
MsgRes
°)°)G M
)
°)°)M N
;
°)°)N O
}
±)±) 	
public
²)²) 
Int32
²)²) ,
InsertarHistoriaClinicaDetalle
²)²) 3
(
²)²)3 44
&odont_historia_clinica_detalle_calidad
²)²)4 Z
OBJ
²)²)[ ^
,
²)²)^ _
ref
²)²)` c 
MessageResponseOBJ
²)²)d v
MsgRes
²)²)w }
)
²)²)} ~
{
³)³) 	
return
´)´) 

DACInserta
´)´) 
.
´)´) ,
InsertarHistoriaClinicaDetalle
´)´) <
(
´)´)< =
OBJ
´)´)= @
,
´)´)@ A
ref
´)´)B E
MsgRes
´)´)F L
)
´)´)L M
;
´)´)M N
}
µ)µ) 	
public
¶)¶) 
Int32
¶)¶) 1
#InsertarHistoriaClinicaDetalleConte
¶)¶) 8
(
¶)¶)8 96
(odont_historia_clinica_detalle_contenido
¶)¶)9 a
OBJ
¶)¶)b e
,
¶)¶)e f
ref
¶)¶)g j 
MessageResponseOBJ
¶)¶)k }
MsgRes¶)¶)~ „
)¶)¶)„ …
{
·)·) 	
return
¸)¸) 

DACInserta
¸)¸) 
.
¸)¸) 1
#InsertarHistoriaClinicaDetalleConte
¸)¸) A
(
¸)¸)A B
OBJ
¸)¸)B E
,
¸)¸)E F
ref
¸)¸)G J
MsgRes
¸)¸)K Q
)
¸)¸)Q R
;
¸)¸)R S
}
¹)¹) 	
public
º)º) 
List
º)º) 
<
º)º) $
odont_historia_clinica
º)º) *
>
º)º)* + 
GetHistoriaClinica
º)º), >
(
º)º)> ?
)
º)º)? @
{
»)») 	
return
¼)¼) 
DACConsulta
¼)¼) 
.
¼)¼)  
GetHistoriaClinica
¼)¼) 1
(
¼)¼)1 2
)
¼)¼)2 3
;
¼)¼)3 4
}
½)½) 	
public
¿)¿) 
List
¿)¿) 
<
¿)¿) -
odont_historia_clinica_paciente
¿)¿) 3
>
¿)¿)3 4(
GetHistoriaClinicaPaciente
¿)¿)5 O
(
¿)¿)O P
Int32
¿)¿)P U'
id_odont_historia_clinica
¿)¿)V o
,
¿)¿)o p
ref
¿)¿)q t!
MessageResponseOBJ¿)¿)u ‡
MsgRes¿)¿)ˆ 
)¿)¿) 
{
À)À) 	
return
Á)Á) 
DACConsulta
Á)Á) 
.
Á)Á) (
GetHistoriaClinicaPaciente
Á)Á) 9
(
Á)Á)9 :'
id_odont_historia_clinica
Á)Á): S
,
Á)Á)S T
ref
Á)Á)U X
MsgRes
Á)Á)Y _
)
Á)Á)_ `
;
Á)Á)` a
}
Â)Â) 	
public
Ã)Ã) 
List
Ã)Ã) 
<
Ã)Ã) /
!vw_odont_historia_clinica_detalle
Ã)Ã) 5
>
Ã)Ã)5 6)
GetVWHistoriaClinicaDetalle
Ã)Ã)7 R
(
Ã)Ã)R S
Int32
Ã)Ã)S X0
"id_odont_historia_clinica_paciente
Ã)Ã)Y {
,
Ã)Ã){ |
refÃ)Ã)} €"
MessageResponseOBJÃ)Ã) “
MsgResÃ)Ã)” š
)Ã)Ã)š ›
{
Ä)Ä) 	
return
Å)Å) 
DACConsulta
Å)Å) 
.
Å)Å) )
GetVWHistoriaClinicaDetalle
Å)Å) :
(
Å)Å): ;0
"id_odont_historia_clinica_paciente
Å)Å); ]
,
Å)Å)] ^
ref
Å)Å)_ b
MsgRes
Å)Å)c i
)
Å)Å)i j
;
Å)Å)j k
}
Æ)Æ) 	
public
Ç)Ç) 
List
Ç)Ç) 
<
Ç)Ç) 9
+vw_odont_historia_clinica_detalle_contenido
Ç)Ç) ?
>
Ç)Ç)? @/
!GetVWHistoriaClinicaDetalleConten
Ç)Ç)A b
(
Ç)Ç)b c
Int32
Ç)Ç)c h1
"id_odont_historia_clinica_pacienteÇ)Ç)i ‹
,Ç)Ç)‹ Œ
refÇ)Ç) "
MessageResponseOBJÇ)Ç)‘ £
MsgResÇ)Ç)¤ ª
)Ç)Ç)ª «
{
È)È) 	
return
É)É) 
DACConsulta
É)É) 
.
É)É) /
!GetVWHistoriaClinicaDetalleConten
É)É) @
(
É)É)@ A0
"id_odont_historia_clinica_paciente
É)É)A c
,
É)É)c d
ref
É)É)e h
MsgRes
É)É)i o
)
É)É)o p
;
É)É)p q
}
Ê)Ê) 	
public
Ì)Ì) 
List
Ì)Ì) 
<
Ì)Ì) )
Ref_odont_hc_calidad_formal
Ì)Ì) /
>
Ì)Ì)/ 00
"GetHistoriaClinicaRefCalidadFormal
Ì)Ì)1 S
(
Ì)Ì)S T
)
Ì)Ì)T U
{
Í)Í) 	
return
Î)Î) 
DACConsulta
Î)Î) 
.
Î)Î) 0
"GetHistoriaClinicaRefCalidadFormal
Î)Î) A
(
Î)Î)A B
)
Î)Î)B C
;
Î)Î)C D
}
Ï)Ï) 	
public
Ñ)Ñ) 
List
Ñ)Ñ) 
<
Ñ)Ñ) ,
Ref_odont_hc_calidad_contenido
Ñ)Ñ) 2
>
Ñ)Ñ)2 3,
GetHistoriaClinicaRefContenido
Ñ)Ñ)4 R
(
Ñ)Ñ)R S
)
Ñ)Ñ)S T
{
Ò)Ò) 	
return
Ó)Ó) 
DACConsulta
Ó)Ó) 
.
Ó)Ó) ,
GetHistoriaClinicaRefContenido
Ó)Ó) =
(
Ó)Ó)= >
)
Ó)Ó)> ?
;
Ó)Ó)? @
}
Ô)Ô) 	
public
Ö)Ö) 
void
Ö)Ö) $
ActualizarOdontHCdtll1
Ö)Ö) *
(
Ö)Ö)* +4
&odont_historia_clinica_detalle_calidad
Ö)Ö)+ Q
obj2
Ö)Ö)R V
,
Ö)Ö)V W
ref
Ö)Ö)X [ 
MessageResponseOBJ
Ö)Ö)\ n
MsgRes
Ö)Ö)o u
)
Ö)Ö)u v
{
×)×) 	
DACActualiza
Ø)Ø) 
.
Ø)Ø) $
ActualizarOdontHCdtll1
Ø)Ø) /
(
Ø)Ø)/ 0
obj2
Ø)Ø)0 4
,
Ø)Ø)4 5
ref
Ø)Ø)6 9
MsgRes
Ø)Ø): @
)
Ø)Ø)@ A
;
Ø)Ø)A B
}
Ú)Ú) 	
public
Ü)Ü) 
void
Ü)Ü) $
ActualizarOdontHCdtll2
Ü)Ü) *
(
Ü)Ü)* +6
(odont_historia_clinica_detalle_contenido
Ü)Ü)+ S
obj2
Ü)Ü)T X
,
Ü)Ü)X Y
ref
Ü)Ü)Z ] 
MessageResponseOBJ
Ü)Ü)^ p
MsgRes
Ü)Ü)q w
)
Ü)Ü)w x
{
İ)İ) 	
DACActualiza
Ş)Ş) 
.
Ş)Ş) $
ActualizarOdontHCdtll2
Ş)Ş) /
(
Ş)Ş)/ 0
obj2
Ş)Ş)0 4
,
Ş)Ş)4 5
ref
Ş)Ş)6 9
MsgRes
Ş)Ş): @
)
Ş)Ş)@ A
;
Ş)Ş)A B
}
ß)ß) 	
public
á)á) 
void
á)á) (
ActualizarOdontHCdtllFinal
á)á) .
(
á)á). /-
odont_historia_clinica_paciente
á)á)/ N
obj2
á)á)O S
,
á)á)S T
ref
á)á)U X 
MessageResponseOBJ
á)á)Y k
MsgRes
á)á)l r
)
á)á)r s
{
â)â) 	
DACActualiza
ã)ã) 
.
ã)ã) (
ActualizarOdontHCdtllFinal
ã)ã) 3
(
ã)ã)3 4
obj2
ã)ã)4 8
,
ã)ã)8 9
ref
ã)ã): =
MsgRes
ã)ã)> D
)
ã)ã)D E
;
ã)ã)E F
}
å)å) 	
public
ç)ç) 
List
ç)ç) 
<
ç)ç) #
Ref_odont_prestadores
ç)ç) )
>
ç)ç)) *!
GetPrestadoresOdont
ç)ç)+ >
(
ç)ç)> ?
)
ç)ç)? @
{
è)è) 	
return
é)é) 
DACConsulta
é)é) 
.
é)é) !
GetPrestadoresOdont
é)é) 2
(
é)é)2 3
)
é)é)3 4
;
é)é)4 5
}
ê)ê) 	
public
ì)ì) 
Int32
ì)ì)  
InsertarPlanMejora
ì)ì) '
(
ì)ì)' (
odont_plan_mejora
ì)ì)( 9
OBJ
ì)ì): =
,
ì)ì)= >
ref
ì)ì)? B 
MessageResponseOBJ
ì)ì)C U
MsgRes
ì)ì)V \
)
ì)ì)\ ]
{
í)í) 	
return
î)î) 

DACInserta
î)î) 
.
î)î)  
InsertarPlanMejora
î)î) 0
(
î)î)0 1
OBJ
î)î)1 4
,
î)î)4 5
ref
î)î)6 9
MsgRes
î)î): @
)
î)î)@ A
;
î)î)A B
}
ï)ï) 	
public
ñ)ñ) 
Int32
ñ)ñ) '
InsertarPlanMejoradetalle
ñ)ñ) .
(
ñ)ñ). /$
odont_plan_mejora_dtll
ñ)ñ)/ E
OBJ
ñ)ñ)F I
,
ñ)ñ)I J
ref
ñ)ñ)K N 
MessageResponseOBJ
ñ)ñ)O a
MsgRes
ñ)ñ)b h
)
ñ)ñ)h i
{
ò)ò) 	
return
ó)ó) 

DACInserta
ó)ó) 
.
ó)ó) '
InsertarPlanMejoradetalle
ó)ó) 7
(
ó)ó)7 8
OBJ
ó)ó)8 ;
,
ó)ó); <
ref
ó)ó)= @
MsgRes
ó)ó)A G
)
ó)ó)G H
;
ó)ó)H I
}
ô)ô) 	
public
ö)ö) 
List
ö)ö) 
<
ö)ö) !
vw_odont_totales_hc
ö)ö) '
>
ö)ö)' (
GetOdontogTotales
ö)ö)) :
(
ö)ö): ;
Int32
ö)ö); @'
id_odont_historia_clinica
ö)ö)A Z
,
ö)ö)Z [
ref
ö)ö)\ _ 
MessageResponseOBJ
ö)ö)` r
MsgRes
ö)ö)s y
)
ö)ö)y z
{
÷)÷) 	
return
ø)ø) 
DACConsulta
ø)ø) 
.
ø)ø) 
GetOdontogTotales
ø)ø) 0
(
ø)ø)0 1'
id_odont_historia_clinica
ø)ø)1 J
,
ø)ø)J K
ref
ø)ø)L O
MsgRes
ø)ø)P V
)
ø)ø)V W
;
ø)ø)W X
}
ù)ù) 	
public
û)û) 
List
û)û) 
<
û)û) *
vw_odont_detalle_plan_mejora
û)û) 0
>
û)û)0 1)
GetOdontogdetallePlanMejora
û)û)2 M
(
û)û)M N
)
û)û)N O
{
ü)ü) 	
return
ı)ı) 
DACConsulta
ı)ı) 
.
ı)ı) )
GetOdontogdetallePlanMejora
ı)ı) :
(
ı)ı): ;
)
ı)ı); <
;
ı)ı)< =
}
ş)ş) 	
public
€*€* 
List
€*€* 
<
€*€* *
vw_odont_tableros_ortodoncia
€*€* 0
>
€*€*0 1*
GetOdontogTablerosOrtodoncia
€*€*2 N
(
€*€*N O
)
€*€*O P
{
** 	
return
‚*‚* 
DACConsulta
‚*‚* 
.
‚*‚* *
GetOdontogTablerosOrtodoncia
‚*‚* ;
(
‚*‚*; <
)
‚*‚*< =
;
‚*‚*= >
}
ƒ*ƒ* 	
public
…*…* 
List
…*…* 
<
…*…* ,
vw_odont_tableros_ProtesisFija
…*…* 2
>
…*…*2 3"
GetOdontogTablerosPT
…*…*4 H
(
…*…*H I
)
…*…*I J
{
†*†* 	
return
‡*‡* 
DACConsulta
‡*‡* 
.
‡*‡* "
GetOdontogTablerosPT
‡*‡* 3
(
‡*‡*3 4
)
‡*‡*4 5
;
‡*‡*5 6
}
ˆ*ˆ* 	
public
Š*Š* 
List
Š*Š* 
<
Š*Š* -
vw_odont_tableros_ProtesisRemov
Š*Š* 3
>
Š*Š*3 4"
GetOdontogTablerosPR
Š*Š*5 I
(
Š*Š*I J
)
Š*Š*J K
{
‹*‹* 	
return
Œ*Œ* 
DACConsulta
Œ*Œ* 
.
Œ*Œ* "
GetOdontogTablerosPR
Œ*Œ* 3
(
Œ*Œ*3 4
)
Œ*Œ*4 5
;
Œ*Œ*5 6
}
** 	
public
** 
List
** 
<
** *
vw_odont_tableros_endodoncia
** 0
>
**0 1*
GetOdontogTablerosEndodoncia
**2 N
(
**N O
)
**O P
{
** 	
return
‘*‘* 
DACConsulta
‘*‘* 
.
‘*‘* *
GetOdontogTablerosEndodoncia
‘*‘* ;
(
‘*‘*; <
)
‘*‘*< =
;
‘*‘*= >
}
’*’* 	
public
”*”* 
List
”*”* 
<
”*”* 0
"Ref_odont_parametros_auditados_rem
”*”* 6
>
”*”*6 7
GetparametrosRem
”*”*8 H
(
”*”*H I
)
”*”*I J
{
•*•* 	
return
–*–* 
DACConsulta
–*–* 
.
–*–* 
GetparametrosRem
–*–* /
(
–*–*/ 0
)
–*–*0 1
;
–*–*1 2
}
—*—* 	
public
™*™* 
List
™*™* 
<
™*™* '
Ref_odont_tratamiento_rem
™*™* -
>
™*™*- . 
GetTratamientosRem
™*™*/ A
(
™*™*A B
)
™*™*B C
{
š*š* 	
return
›*›* 
DACConsulta
›*›* 
.
›*›*  
GetTratamientosRem
›*›* 1
(
›*›*1 2
)
›*›*2 3
;
›*›*3 4
}
œ*œ* 	
public
** 
List
** 
<
** +
vw_odont_tableros_plan_mejora
** 1
>
**1 2*
GetOdontogTablerosPlanMejora
**3 O
(
**O P
)
**P Q
{
Ÿ*Ÿ* 	
return
 * * 
DACConsulta
 * * 
.
 * * *
GetOdontogTablerosPlanMejora
 * * ;
(
 * *; <
)
 * *< =
;
 * *= >
}
¢*¢* 	
public
¤*¤* 
List
¤*¤* 
<
¤*¤* %
vw_odont_consolidado_hc
¤*¤* +
>
¤*¤*+ ,
GetConsolidadoHc
¤*¤*- =
(
¤*¤*= >
ref
¤*¤*> A 
MessageResponseOBJ
¤*¤*B T
MsgRes
¤*¤*U [
)
¤*¤*[ \
{
¥*¥* 	
return
¦*¦* 
DACConsulta
¦*¦* 
.
¦*¦* 
GetConsolidadoHc
¦*¦* /
(
¦*¦*/ 0
ref
¦*¦*0 3
MsgRes
¦*¦*4 :
)
¦*¦*: ;
;
¦*¦*; <
}
§*§* 	
public
©*©* 
List
©*©* 
<
©*©* /
!vw_odont_consolidado_hc_prestador
©*©* 5
>
©*©*5 6*
GetConsolidadoHcporprestador
©*©*7 S
(
©*©*S T
ref
©*©*T W 
MessageResponseOBJ
©*©*X j
MsgRes
©*©*k q
)
©*©*q r
{
ª*ª* 	
return
«*«* 
DACConsulta
«*«* 
.
«*«* *
GetConsolidadoHcporprestador
«*«* ;
(
«*«*; <
ref
«*«*< ?
MsgRes
«*«*@ F
)
«*«*F G
;
«*«*G H
}
¬*¬* 	
public
®*®* 
List
®*®* 
<
®*®* *
vw_odont_count_planes_mejora
®*®* 0
>
®*®*0 1&
GetListCountPlanesMejora
®*®*2 J
(
®*®*J K
int
®*®*K N

idregional
®*®*O Y
)
®*®*Y Z
{
¯*¯* 	
return
°*°* 
DACConsulta
°*°* 
.
°*°* &
GetListCountPlanesMejora
°*°* 7
(
°*°*7 8

idregional
°*°*8 B
)
°*°*B C
;
°*°*C D
}
±*±* 	
public
³*³* 
List
³*³* 
<
³*³* ,
vw_odont_count_acciones_mejora
³*³* 2
>
³*³*2 3(
GetListCountAccionesMejora
³*³*4 N
(
³*³*N O
int
³*³*O R

idregional
³*³*S ]
)
³*³*] ^
{
´*´* 	
return
µ*µ* 
DACConsulta
µ*µ* 
.
µ*µ* (
GetListCountAccionesMejora
µ*µ* 9
(
µ*µ*9 :

idregional
µ*µ*: D
)
µ*µ*D E
;
µ*µ*E F
}
¶*¶* 	
public
¸*¸* 
void
¸*¸* .
 InsertarRemisionesEspecialidades
¸*¸* 4
(
¸*¸*4 5-
odont_remisiones_especialidades
¸*¸*5 T
obj
¸*¸*U X
,
¸*¸*X Y
ref
¸*¸*Z ] 
MessageResponseOBJ
¸*¸*^ p
MsgRes
¸*¸*q w
)
¸*¸*w x
{
¹*¹* 	

DACInserta
º*º* 
.
º*º* .
 InsertarRemisionesEspecialidades
º*º* 7
(
º*º*7 8
obj
º*º*8 ;
,
º*º*; <
ref
º*º*= @
MsgRes
º*º*A G
)
º*º*G H
;
º*º*H I
}
»*»* 	
public
½*½* 
List
½*½* 
<
½*½* 0
"vw_odont_remisiones_especialidades
½*½* 6
>
½*½*6 7
GetRemisiones
½*½*8 E
(
½*½*E F
ref
½*½*F I 
MessageResponseOBJ
½*½*J \
MsgRes
½*½*] c
)
½*½*c d
{
¾*¾* 	
return
¿*¿* 
DACConsulta
¿*¿* 
.
¿*¿* 
GetRemisiones
¿*¿* ,
(
¿*¿*, -
ref
¿*¿*- 0
MsgRes
¿*¿*1 7
)
¿*¿*7 8
;
¿*¿*8 9
}
À*À* 	
public
Â*Â* 
void
Â*Â* +
InsertarRemisionesVerificadas
Â*Â* 1
(
Â*Â*1 2*
odont_remisiones_verificadas
Â*Â*2 N
obj
Â*Â*O R
,
Â*Â*R S
ref
Â*Â*T W 
MessageResponseOBJ
Â*Â*X j
MsgRes
Â*Â*k q
)
Â*Â*q r
{
Ã*Ã* 	

DACInserta
Ä*Ä* 
.
Ä*Ä* +
InsertarRemisionesVerificadas
Ä*Ä* 4
(
Ä*Ä*4 5
obj
Ä*Ä*5 8
,
Ä*Ä*8 9
ref
Ä*Ä*: =
MsgRes
Ä*Ä*> D
)
Ä*Ä*D E
;
Ä*Ä*E F
}
Å*Å* 	
public
Ç*Ç* 
List
Ç*Ç* 
<
Ç*Ç* -
vw_odont_remisiones_verificadas
Ç*Ç* 3
>
Ç*Ç*3 4&
GetRemisionesVerificadas
Ç*Ç*5 M
(
Ç*Ç*M N
ref
Ç*Ç*N Q 
MessageResponseOBJ
Ç*Ç*R d
MsgRes
Ç*Ç*e k
)
Ç*Ç*k l
{
È*È* 	
return
É*É* 
DACConsulta
É*É* 
.
É*É* &
GetRemisionesVerificadas
É*É* 7
(
É*É*7 8
ref
É*É*8 ;
MsgRes
É*É*< B
)
É*É*B C
;
É*É*C D
}
Ê*Ê* 	
public
Ë*Ë* 
List
Ë*Ë* 
<
Ë*Ë* 
vw_totales_odont
Ë*Ë* $
>
Ë*Ë*$ %
GetTotalPaciente
Ë*Ë*& 6
(
Ë*Ë*6 7
Int32
Ë*Ë*7 <'
id_odont_historia_clinica
Ë*Ë*= V
,
Ë*Ë*V W
ref
Ë*Ë*X [ 
MessageResponseOBJ
Ë*Ë*\ n
MsgRes
Ë*Ë*o u
)
Ë*Ë*u v
{
Ì*Ì* 	
return
Í*Í* 
DACConsulta
Í*Í* 
.
Í*Í* 
GetTotalPaciente
Í*Í* /
(
Í*Í*/ 0'
id_odont_historia_clinica
Í*Í*0 I
,
Í*Í*I J
ref
Í*Í*K N
MsgRes
Í*Í*O U
)
Í*Í*U V
;
Í*Í*V W
}
Î*Î* 	
public
Ğ*Ğ* 
List
Ğ*Ğ* 
<
Ğ*Ğ* /
!vw_reportesTratamientosUnificados
Ğ*Ğ* 5
>
Ğ*Ğ*5 6-
GetReportTratamientosUnificados
Ğ*Ğ*7 V
(
Ğ*Ğ*V W
ref
Ğ*Ğ*W Z 
MessageResponseOBJ
Ğ*Ğ*[ m
MsgRes
Ğ*Ğ*n t
)
Ğ*Ğ*t u
{
Ñ*Ñ* 	
return
Ò*Ò* 
DACConsulta
Ò*Ò* 
.
Ò*Ò* -
GetReportTratamientosUnificados
Ò*Ò* >
(
Ò*Ò*> ?
ref
Ò*Ò*? B
MsgRes
Ò*Ò*C I
)
Ò*Ò*I J
;
Ò*Ò*J K
}
Ó*Ó* 	
public
Õ*Õ* 
void
Õ*Õ* *
InsertarprestadorOdontologia
Õ*Õ* 0
(
Õ*Õ*0 1#
Ref_odont_prestadores
Õ*Õ*1 F
obj
Õ*Õ*G J
,
Õ*Õ*J K
ref
Õ*Õ*L O 
MessageResponseOBJ
Õ*Õ*P b
MsgRes
Õ*Õ*c i
)
Õ*Õ*i j
{
Ö*Ö* 	

DACInserta
×*×* 
.
×*×* *
InsertarprestadorOdontologia
×*×* 3
(
×*×*3 4
obj
×*×*4 7
,
×*×*7 8
ref
×*×*9 <
MsgRes
×*×*= C
)
×*×*C D
;
×*×*D E
}
Ø*Ø* 	
public
Ù*Ù* 
List
Ù*Ù* 
<
Ù*Ù* 1
#vw_prestadores_odontologiaUnificado
Ù*Ù* 7
>
Ù*Ù*7 8*
GetPrestadoresOdonUnificados
Ù*Ù*9 U
(
Ù*Ù*U V
ref
Ù*Ù*V Y 
MessageResponseOBJ
Ù*Ù*Z l
MsgRes
Ù*Ù*m s
)
Ù*Ù*s t
{
Ú*Ú* 	
return
Û*Û* 
DACConsulta
Û*Û* 
.
Û*Û* *
GetPrestadoresOdonUnificados
Û*Û* ;
(
Û*Û*; <
ref
Û*Û*< ?
MsgRes
Û*Û*@ F
)
Û*Û*F G
;
Û*Û*G H
}
Ü*Ü* 	
public
â*â* 
Int32
â*â* $
InsertarFFMMRadicacion
â*â* +
(
â*â*+ ,%
ffmm_Cuentas_radicacion
â*â*, C
OBJ
â*â*D G
,
â*â*G H
ref
â*â*I L 
MessageResponseOBJ
â*â*M _
MsgRes
â*â*` f
)
â*â*f g
{
ã*ã* 	
return
ä*ä* 

DACInserta
ä*ä* 
.
ä*ä* $
InsertarFFMMRadicacion
ä*ä* 4
(
ä*ä*4 5
OBJ
ä*ä*5 8
,
ä*ä*8 9
ref
ä*ä*: =
MsgRes
ä*ä*> D
)
ä*ä*D E
;
ä*ä*E F
}
å*å* 	
public
ç*ç* 
List
ç*ç* 
<
ç*ç* -
vw_ffmm_consulta_radicacion_ips
ç*ç* 3
>
ç*ç*3 4%
GetOdontogRadicacionIPS
ç*ç*5 L
(
ç*ç*L M
)
ç*ç*M N
{
è*è* 	
return
é*é* 
DACConsulta
é*é* 
.
é*é* %
GetOdontogRadicacionIPS
é*é* 6
(
é*é*6 7
)
é*é*7 8
;
é*é*8 9
}
ê*ê* 	
public
ì*ì* 
List
ì*ì* 
<
ì*ì* %
ffmm_Cuentas_radicacion
ì*ì* +
>
ì*ì*+ ,&
GetRadicacionIPSFacturas
ì*ì*- E
(
ì*ì*E F
Int32
ì*ì*F K
id_proveedor
ì*ì*L X
,
ì*ì*X Y
Int32
ì*ì*Z _

id_factura
ì*ì*` j
,
ì*ì*j k
string
ì*ì*l r
prefijo
ì*ì*s z
,
ì*ì*z {
ref
ì*ì*| "
MessageResponseOBJì*ì*€ ’
MsgResì*ì*“ ™
)ì*ì*™ š
{
í*í* 	
return
î*î* 
DACConsulta
î*î* 
.
î*î* &
GetRadicacionIPSFacturas
î*î* 7
(
î*î*7 8
id_proveedor
î*î*8 D
,
î*î*D E

id_factura
î*î*F P
,
î*î*P Q
prefijo
î*î*R Y
,
î*î*Y Z
ref
î*î*[ ^
MsgRes
î*î*_ e
)
î*î*e f
;
î*î*f g
}
ï*ï* 	
public
ñ*ñ* 
Int32
ñ*ñ* #
InsertarFFMMAuditoria
ñ*ñ* *
(
ñ*ñ** +$
ffmm_Cuentas_auditoria
ñ*ñ*+ A
OBJ
ñ*ñ*B E
,
ñ*ñ*E F
List
ñ*ñ*G K
<
ñ*ñ*K L'
ffmm_cuentas_medicas_cups
ñ*ñ*L e
>
ñ*ñ*e f
obj2
ñ*ñ*g k
,
ñ*ñ*k l
List
ñ*ñ*m q
<
ñ*ñ*q r*
ffmm_cuentas_medicas_glosasñ*ñ*r 
>ñ*ñ* 
obj3ñ*ñ* “
,ñ*ñ*“ ”
refñ*ñ*• ˜"
MessageResponseOBJñ*ñ*™ «
MsgResñ*ñ*¬ ²
)ñ*ñ*² ³
{
ò*ò* 	
return
ó*ó* 

DACInserta
ó*ó* 
.
ó*ó* #
InsertarFFMMAuditoria
ó*ó* 3
(
ó*ó*3 4
OBJ
ó*ó*4 7
,
ó*ó*7 8
obj2
ó*ó*9 =
,
ó*ó*= >
obj3
ó*ó*? C
,
ó*ó*C D
ref
ó*ó*E H
MsgRes
ó*ó*I O
)
ó*ó*O P
;
ó*ó*P Q
}
ô*ô* 	
public
ö*ö* 
List
ö*ö* 
<
ö*ö* ,
management_CupsAuditoriaResult
ö*ö* 2
>
ö*ö*2 3 
ListaCupsAuditoria
ö*ö*4 F
(
ö*ö*F G
)
ö*ö*G H
{
÷*÷* 	
return
ø*ø* 
DACConsulta
ø*ø* 
.
ø*ø*  
ListaCupsAuditoria
ø*ø* 1
(
ø*ø*1 2
)
ø*ø*2 3
;
ø*ø*3 4
}
ù*ù* 	
public
û*û* 
void
û*û* (
ActualizarEstadoRadicacion
û*û* .
(
û*û*. /%
ffmm_Cuentas_radicacion
û*û*/ F
obj2
û*û*G K
,
û*û*K L
ref
û*û*M P 
MessageResponseOBJ
û*û*Q c
MsgRes
û*û*d j
)
û*û*j k
{
ü*ü* 	
DACActualiza
ı*ı* 
.
ı*ı* (
ActualizarEstadoRadicacion
ı*ı* 3
(
ı*ı*3 4
obj2
ı*ı*4 8
,
ı*ı*8 9
ref
ı*ı*: =
MsgRes
ı*ı*> D
)
ı*ı*D E
;
ı*ı*E F
}
ş*ş* 	
public
ÿ*ÿ* 
List
ÿ*ÿ* 
<
ÿ*ÿ* $
ffmm_Cuentas_auditoria
ÿ*ÿ* *
>
ÿ*ÿ** +
GetIPSTotal
ÿ*ÿ*, 7
(
ÿ*ÿ*7 8
Int32
ÿ*ÿ*8 =,
id_ref_ffmm_radicacion_Cuentas
ÿ*ÿ*> \
,
ÿ*ÿ*\ ]
ref
ÿ*ÿ*^ a 
MessageResponseOBJ
ÿ*ÿ*b t
MsgRes
ÿ*ÿ*u {
)
ÿ*ÿ*{ |
{
€+€+ 	
return
++ 
DACConsulta
++ 
.
++ 
GetIPSTotal
++ *
(
++* +,
id_ref_ffmm_radicacion_Cuentas
+++ I
,
++I J
ref
++K N
MsgRes
++O U
)
++U V
;
++V W
}
‚+‚+ 	
public
„+„+ 
Int32
„+„+ )
InsertarFFMMAuditoriaGlosas
„+„+ 0
(
„+„+0 1!
ffmm_cuentas_glosas
„+„+1 D
OBJ
„+„+E H
,
„+„+H I
ref
„+„+J M 
MessageResponseOBJ
„+„+N `
MsgRes
„+„+a g
)
„+„+g h
{
…+…+ 	
return
†+†+ 

DACInserta
†+†+ 
.
†+†+ )
InsertarFFMMAuditoriaGlosas
†+†+ 9
(
†+†+9 :
OBJ
†+†+: =
,
†+†+= >
ref
†+†+? B
MsgRes
†+†+C I
)
†+†+I J
;
†+†+J K
}
‡+‡+ 	
public
ˆ+ˆ+ 
List
ˆ+ˆ+ 
<
ˆ+ˆ+ !
ffmm_cuentas_glosas
ˆ+ˆ+ '
>
ˆ+ˆ+' (
GetIPSTotalGlosas
ˆ+ˆ+) :
(
ˆ+ˆ+: ;
Int32
ˆ+ˆ+; @,
id_ref_ffmm_radicacion_Cuentas
ˆ+ˆ+A _
,
ˆ+ˆ+_ `
ref
ˆ+ˆ+a d 
MessageResponseOBJ
ˆ+ˆ+e w
MsgRes
ˆ+ˆ+x ~
)
ˆ+ˆ+~ 
{
‰+‰+ 	
return
Š+Š+ 
DACConsulta
Š+Š+ 
.
Š+Š+ 
GetIPSTotalGlosas
Š+Š+ 0
(
Š+Š+0 1,
id_ref_ffmm_radicacion_Cuentas
Š+Š+1 O
,
Š+Š+O P
ref
Š+Š+Q T
MsgRes
Š+Š+U [
)
Š+Š+[ \
;
Š+Š+\ ]
}
‹+‹+ 	
public
++ 
void
++ #
ActualizarEstadoGlosa
++ )
(
++) *!
ffmm_cuentas_glosas
++* =
obj2
++> B
,
++B C
ref
++D G 
MessageResponseOBJ
++H Z
MsgRes
++[ a
)
++a b
{
++ 	
DACActualiza
++ 
.
++ #
ActualizarEstadoGlosa
++ .
(
++. /
obj2
++/ 3
,
++3 4
ref
++5 8
MsgRes
++9 ?
)
++? @
;
++@ A
}
++ 	
public
’+’+ 
void
’+’+ /
!ActualizarEstadoGlosaSegundaConci
’+’+ 5
(
’+’+5 6!
ffmm_cuentas_glosas
’+’+6 I
obj2
’+’+J N
,
’+’+N O
ref
’+’+P S 
MessageResponseOBJ
’+’+T f
MsgRes
’+’+g m
)
’+’+m n
{
“+“+ 	
DACActualiza
”+”+ 
.
”+”+ /
!ActualizarEstadoGlosaSegundaConci
”+”+ :
(
”+”+: ;
obj2
”+”+; ?
,
”+”+? @
ref
”+”+A D
MsgRes
”+”+E K
)
”+”+K L
;
”+”+L M
}
•+•+ 	
public
–+–+ 
Int32
–+–+ '
InsertarFFMMref_proveedor
–+–+ .
(
–+–+. /,
Ref_ffmm_prestadores_proveedor
–+–+/ M
OBJ
–+–+N Q
,
–+–+Q R
ref
–+–+S V 
MessageResponseOBJ
–+–+W i
MsgRes
–+–+j p
)
–+–+p q
{
—+—+ 	
return
˜+˜+ 

DACInserta
˜+˜+ 
.
˜+˜+ '
InsertarFFMMref_proveedor
˜+˜+ 7
(
˜+˜+7 8
OBJ
˜+˜+8 ;
,
˜+˜+; <
ref
˜+˜+= @
MsgRes
˜+˜+A G
)
˜+˜+G H
;
˜+˜+H I
}
™+™+ 	
public
›+›+ 
Int32
›+›+ (
InsertarFFMMref_paliativos
›+›+ /
(
›+›+/ 0&
ffmm_cuidados_paliativos
›+›+0 H
OBJ
›+›+I L
,
›+›+L M
ref
›+›+N Q 
MessageResponseOBJ
›+›+R d
MsgRes
›+›+e k
)
›+›+k l
{
œ+œ+ 	
return
++ 

DACInserta
++ 
.
++ (
InsertarFFMMref_paliativos
++ 8
(
++8 9
OBJ
++9 <
,
++< =
ref
++> A
MsgRes
++B H
)
++H I
;
++I J
}
++ 	
public
¡+¡+ 
int
¡+¡+ #
InsertarContratosFFMM
¡+¡+ (
(
¡+¡+( )
ffmm_contratos
¡+¡+) 7
obj
¡+¡+8 ;
)
¡+¡+; <
{
¢+¢+ 	
return
£+£+ 

DACInserta
£+£+ 
.
£+£+ #
InsertarContratosFFMM
£+£+ 3
(
£+£+3 4
obj
£+£+4 7
)
£+£+7 8
;
£+£+8 9
}
¤+¤+ 	
public
¦+¦+ 
int
¦+¦+ -
InsertarCargueMasivoPagoFactura
¦+¦+ 2
(
¦+¦+2 3
List
¦+¦+3 7
<
¦+¦+7 8'
ffmm_cargue_facturas_pago
¦+¦+8 Q
>
¦+¦+Q R
List
¦+¦+S W
,
¦+¦+W X
ref
¦+¦+Y \ 
MessageResponseOBJ
¦+¦+] o
MsgRes
¦+¦+p v
)
¦+¦+v w
{
§+§+ 	
return
¨+¨+ 

DACInserta
¨+¨+ 
.
¨+¨+ -
InsertarCargueMasivoPagoFactura
¨+¨+ =
(
¨+¨+= >
List
¨+¨+> B
,
¨+¨+B C
ref
¨+¨+D G
MsgRes
¨+¨+H N
)
¨+¨+N O
;
¨+¨+O P
}
©+©+ 	
public
ª+ª+ 
List
ª+ª+ 
<
ª+ª+ -
management_facturas_pagosResult
ª+ª+ 3
>
ª+ª+3 4
ListFacturasPago
ª+ª+5 E
(
ª+ª+E F
)
ª+ª+F G
{
«+«+ 	
return
¬+¬+ 
DACConsulta
¬+¬+ 
.
¬+¬+ 
ListFacturasPago
¬+¬+ /
(
¬+¬+/ 0
)
¬+¬+0 1
;
¬+¬+1 2
}
­+­+ 	
public
µ+µ+ 
sis_configuracion
µ+µ+  
GetParametro
µ+µ+! -
(
µ+µ+- .
string
µ+µ+. 4
	parametro
µ+µ+5 >
)
µ+µ+> ?
{
¶+¶+ 	
return
·+·+ 
DACConsulta
·+·+ 
.
·+·+ 
GetParametro
·+·+ +
(
·+·++ ,
	parametro
·+·+, 5
)
·+·+5 6
;
·+·+6 7
}
¸+¸+ 	
public
¾+¾+ 
Int32
¾+¾+ /
!InsertarSeguimientoDetalleCovid19
¾+¾+ 6
(
¾+¾+6 7
List
¾+¾+7 ;
<
¾+¾+; <0
"cargue_seguimiento_covid19_detalle
¾+¾+< ^
>
¾+¾+^ _
OBJ
¾+¾+` c
,
¾+¾+c d
ref
¾+¾+e h 
MessageResponseOBJ
¾+¾+i {
MsgRes¾+¾+| ‚
)¾+¾+‚ ƒ
{
¿+¿+ 	
return
À+À+ 

DACInserta
À+À+ 
.
À+À+ /
!InsertarSeguimientoDetalleCovid19
À+À+ ?
(
À+À+? @
OBJ
À+À+@ C
,
À+À+C D
ref
À+À+E H
MsgRes
À+À+I O
)
À+À+O P
;
À+À+P Q
}
Á+Á+ 	
public
Ã+Ã+ 
Int32
Ã+Ã+ 3
%InsertarConsolidadoSeguimientoCovid19
Ã+Ã+ :
(
Ã+Ã+: ;
List
Ã+Ã+; ?
<
Ã+Ã+? @(
cargue_seguimiento_covid19
Ã+Ã+@ Z
>
Ã+Ã+Z [
OBJ
Ã+Ã+\ _
,
Ã+Ã+_ `
ref
Ã+Ã+a d 
MessageResponseOBJ
Ã+Ã+e w
MsgRes
Ã+Ã+x ~
)
Ã+Ã+~ 
{
Ä+Ä+ 	
return
Å+Å+ 

DACInserta
Å+Å+ 
.
Å+Å+ 3
%InsertarConsolidadoSeguimientoCovid19
Å+Å+ C
(
Å+Å+C D
OBJ
Å+Å+D G
,
Å+Å+G H
ref
Å+Å+I L
MsgRes
Å+Å+M S
)
Å+Å+S T
;
Å+Å+T U
}
Æ+Æ+ 	
public
É+É+ 
Int32
É+É+ /
!InsertarSeguimientoCovid19Detalle
É+É+ 6
(
É+É+6 70
"cargue_seguimiento_covid19_detalle
É+É+7 Y
OBJ
É+É+Z ]
,
É+É+] ^
ref
É+É+_ b 
MessageResponseOBJ
É+É+c u
MsgRes
É+É+v |
)
É+É+| }
{
Ê+Ê+ 	
return
Ë+Ë+ 

DACInserta
Ë+Ë+ 
.
Ë+Ë+ /
!InsertarSeguimientoCovid19Detalle
Ë+Ë+ ?
(
Ë+Ë+? @
OBJ
Ë+Ë+@ C
,
Ë+Ë+C D
ref
Ë+Ë+E H
MsgRes
Ë+Ë+I O
)
Ë+Ë+O P
;
Ë+Ë+P Q
}
Ì+Ì+ 	
public
Ï+Ï+ 
List
Ï+Ï+ 
<
Ï+Ï+ (
cargue_seguimiento_covid19
Ï+Ï+ .
>
Ï+Ï+. /*
ConsultaIdSeguimientoCovid19
Ï+Ï+0 L
(
Ï+Ï+L M
Int32
Ï+Ï+M R
ID
Ï+Ï+S U
,
Ï+Ï+U V
ref
Ï+Ï+W Z 
MessageResponseOBJ
Ï+Ï+[ m
MsgRes
Ï+Ï+n t
)
Ï+Ï+t u
{
Ğ+Ğ+ 	
return
Ñ+Ñ+ 
DACConsulta
Ñ+Ñ+ 
.
Ñ+Ñ+ *
ConsultaIdSeguimientoCovid19
Ñ+Ñ+ ;
(
Ñ+Ñ+; <
ID
Ñ+Ñ+< >
,
Ñ+Ñ+> ?
ref
Ñ+Ñ+@ C
MsgRes
Ñ+Ñ+D J
)
Ñ+Ñ+J K
;
Ñ+Ñ+K L
}
Ò+Ò+ 	
public
Ô+Ô+ 
List
Ô+Ô+ 
<
Ô+Ô+ ,
vw_seguimiento_covid19_detalle
Ô+Ô+ 2
>
Ô+Ô+2 31
#ConsultaIdSeguimientoCovid19Detalle
Ô+Ô+4 W
(
Ô+Ô+W X
Int32
Ô+Ô+X ]
ID
Ô+Ô+^ `
,
Ô+Ô+` a
ref
Ô+Ô+b e 
MessageResponseOBJ
Ô+Ô+f x
MsgRes
Ô+Ô+y 
)Ô+Ô+ €
{
Õ+Õ+ 	
return
Ö+Ö+ 
DACConsulta
Ö+Ö+ 
.
Ö+Ö+ 1
#ConsultaIdSeguimientoCovid19Detalle
Ö+Ö+ B
(
Ö+Ö+B C
ID
Ö+Ö+C E
,
Ö+Ö+E F
ref
Ö+Ö+G J
MsgRes
Ö+Ö+K Q
)
Ö+Ö+Q R
;
Ö+Ö+R S
}
×+×+ 	
public
Ù+Ù+ 
List
Ù+Ù+ 
<
Ù+Ù+ 3
%vw_seguimiento_covid19_ultimo_detalle
Ù+Ù+ 9
>
Ù+Ù+9 :7
)ConsultaIdSeguimientoCovid19DetalleUltimo
Ù+Ù+; d
(
Ù+Ù+d e
Int32
Ù+Ù+e j
ID
Ù+Ù+k m
,
Ù+Ù+m n
ref
Ù+Ù+o r!
MessageResponseOBJÙ+Ù+s …
MsgResÙ+Ù+† Œ
)Ù+Ù+Œ 
{
Ú+Ú+ 	
return
Û+Û+ 
DACConsulta
Û+Û+ 
.
Û+Û+ 7
)ConsultaIdSeguimientoCovid19DetalleUltimo
Û+Û+ H
(
Û+Û+H I
ID
Û+Û+I K
,
Û+Û+K L
ref
Û+Û+M P
MsgRes
Û+Û+Q W
)
Û+Û+W X
;
Û+Û+X Y
}
Ü+Ü+ 	
public
ß+ß+ 
List
ß+ß+ 
<
ß+ß+ (
cargue_seguimiento_covid19
ß+ß+ .
>
ß+ß+. /.
 ConsultaDocumentoPacienteCovid19
ß+ß+0 P
(
ß+ß+P Q
String
ß+ß+Q W
ID
ß+ß+X Z
)
ß+ß+Z [
{
à+à+ 	
return
á+á+ 
DACConsulta
á+á+ 
.
á+á+ .
 ConsultaDocumentoPacienteCovid19
á+á+ ?
(
á+á+? @
ID
á+á+@ B
)
á+á+B C
;
á+á+C D
}
â+â+ 	
public
ã+ã+ 
List
ã+ã+ 
<
ã+ã+ (
cargue_seguimiento_covid19
ã+ã+ .
>
ã+ã+. /#
ConsultaCargueCovid19
ã+ã+0 E
(
ã+ã+E F
ref
ã+ã+F I 
MessageResponseOBJ
ã+ã+J \
MsgRes
ã+ã+] c
)
ã+ã+c d
{
ä+ä+ 	
return
å+å+ 
DACConsulta
å+å+ 
.
å+å+ #
ConsultaCargueCovid19
å+å+ 4
(
å+å+4 5
ref
å+å+5 8
MsgRes
å+å+9 ?
)
å+å+? @
;
å+å+@ A
}
æ+æ+ 	
public
è+è+ 
List
è+è+ 
<
è+è+ 0
"cargue_seguimiento_covid19_detalle
è+è+ 6
>
è+è+6 7/
!ConsultaDetalleSeguimientoCovid19
è+è+8 Y
(
è+è+Y Z
Int32
è+è+Z _
	id_cargue
è+è+` i
,
è+è+i j
ref
è+è+k n!
MessageResponseOBJè+è+o 
MsgResè+è+‚ ˆ
)è+è+ˆ ‰
{
é+é+ 	
return
ê+ê+ 
DACConsulta
ê+ê+ 
.
ê+ê+ /
!ConsultaDetalleSeguimientoCovid19
ê+ê+ @
(
ê+ê+@ A
	id_cargue
ê+ê+A J
,
ê+ê+J K
ref
ê+ê+L O
MsgRes
ê+ê+P V
)
ê+ê+V W
;
ê+ê+W X
}
ë+ë+ 	
public
í+í+ 
List
í+í+ 
<
í+í+ +
vw_seguimiento_covid19_diario
í+í+ 1
>
í+í+1 2/
!ConsultaListadoseguimientoCovid19
í+í+3 T
(
í+í+T U
)
í+í+U V
{
î+î+ 	
return
ï+ï+ 
DACConsulta
ï+ï+ 
.
ï+ï+ /
!ConsultaListadoseguimientoCovid19
ï+ï+ @
(
ï+ï+@ A
)
ï+ï+A B
;
ï+ï+B C
}
ğ+ğ+ 	
public
ò+ò+ 
List
ò+ò+ 
<
ò+ò+ 0
"vw_seguimiento_covid19_interdiario
ò+ò+ 6
>
ò+ò+6 7:
,ConsultaListadoseguimientoInterdiarioCovid19
ò+ò+8 d
(
ò+ò+d e
)
ò+ò+e f
{
ó+ó+ 	
return
ô+ô+ 
DACConsulta
ô+ô+ 
.
ô+ô+ :
,ConsultaListadoseguimientoInterdiarioCovid19
ô+ô+ K
(
ô+ô+K L
)
ô+ô+L M
;
ô+ô+M N
}
õ+õ+ 	
public
÷+÷+ 
List
÷+÷+ 
<
÷+÷+ 3
%vw_seguimiento_covid19_casos_cerrados
÷+÷+ 9
>
÷+÷+9 :7
)ConsultaListadoseguimientoCerradosCovid19
÷+÷+; d
(
÷+÷+d e
)
÷+÷+e f
{
ø+ø+ 	
return
ù+ù+ 
DACConsulta
ù+ù+ 
.
ù+ù+ 7
)ConsultaListadoseguimientoCerradosCovid19
ù+ù+ H
(
ù+ù+H I
)
ù+ù+I J
;
ù+ù+J K
}
ú+ú+ 	
public
ü+ü+ 
List
ü+ü+ 
<
ü+ü+ &
ref_covid19_tipificacion
ü+ü+ ,
>
ü+ü+, -.
 ConsultaListadoTipicacionCovid19
ü+ü+. N
(
ü+ü+N O
)
ü+ü+O P
{
ı+ı+ 	
return
ş+ş+ 
DACConsulta
ş+ş+ 
.
ş+ş+ .
 ConsultaListadoTipicacionCovid19
ş+ş+ ?
(
ş+ş+? @
)
ş+ş+@ A
;
ş+ş+A B
}
ÿ+ÿ+ 	
public
,, 
List
,, 
<
,, /
!ref_covid19_tipificacion7_detalle
,, 5
>
,,5 6/
!ConsultaListadoTipicacion7Covid19
,,7 X
(
,,X Y
)
,,Y Z
{
‚,‚, 	
return
ƒ,ƒ, 
DACConsulta
ƒ,ƒ, 
.
ƒ,ƒ, /
!ConsultaListadoTipicacion7Covid19
ƒ,ƒ, @
(
ƒ,ƒ,@ A
)
ƒ,ƒ,A B
;
ƒ,ƒ,B C
}
„,„, 	
public
‡,‡, 
void
‡,‡, 0
"ActualizarEstadoSeguimientoCovid19
‡,‡, 6
(
‡,‡,6 7(
cargue_seguimiento_covid19
‡,‡,7 Q
OBJ2
‡,‡,R V
,
‡,‡,V W
ref
‡,‡,X [ 
MessageResponseOBJ
‡,‡,\ n
MsgRes
‡,‡,o u
)
‡,‡,u v
{
ˆ,ˆ, 	
DACActualiza
‰,‰, 
.
‰,‰, 0
"ActualizarEstadoSeguimientoCovid19
‰,‰, ;
(
‰,‰,; <
OBJ2
‰,‰,< @
,
‰,‰,@ A
ref
‰,‰,B E
MsgRes
‰,‰,F L
)
‰,‰,L M
;
‰,‰,M N
}
Š,Š, 	
public
,, 
void
,, (
Actualizacasocarguecovid19
,, .
(
,,. /(
cargue_seguimiento_covid19
,,/ I
OBJ
,,J M
,
,,M N
ref
,,O R 
MessageResponseOBJ
,,S e
MsgRes
,,f l
)
,,l m
{
,, 	
DACActualiza
,, 
.
,, (
Actualizacasocarguecovid19
,, 3
(
,,3 4
OBJ
,,4 7
,
,,7 8
ref
,,9 <
MsgRes
,,= C
)
,,C D
;
,,D E
}
,, 	
public
“,“, 
List
“,“, 
<
“,“, '
ref_covid19_estado_asalud
“,“, -
>
“,“,- .)
Consultaestadoasaludcovid19
“,“,/ J
(
“,“,J K
)
“,“,K L
{
”,”, 	
return
•,•, 
DACConsulta
•,•, 
.
•,•, )
Consultaestadoasaludcovid19
•,•, :
(
•,•,: ;
)
•,•,; <
;
•,•,< =
}
–,–, 	
public
š,š, 
List
š,š, 
<
š,š, 4
&vw_seguimiento_covid19_descarga_diaria
š,š, :
>
š,š,: ;7
)ConsultaListadoseguimientodescargaCovid19
š,š,< e
(
š,š,e f
)
š,š,f g
{
›,›, 	
return
œ,œ, 
DACConsulta
œ,œ, 
.
œ,œ, 7
)ConsultaListadoseguimientodescargaCovid19
œ,œ, H
(
œ,œ,H I
)
œ,œ,I J
;
œ,œ,J K
}
,, 	
public
 , , 
List
 , , 
<
 , , 9
+vw_seguimiento_covid19_descarga_interdiaria
 , , ?
>
 , ,? @B
4ConsultaListadoseguimientointerdiariodescargaCovid19
 , ,A u
(
 , ,u v
)
 , ,v w
{
¡,¡, 	
return
¢,¢, 
DACConsulta
¢,¢, 
.
¢,¢, B
4ConsultaListadoseguimientointerdiariodescargaCovid19
¢,¢, S
(
¢,¢,S T
)
¢,¢,T U
;
¢,¢,U V
}
£,£, 	
public
¥,¥, 
List
¥,¥, 
<
¥,¥, <
.vw_seguimiento_covid19_descarga_casos_cerrados
¥,¥, B
>
¥,¥,B CD
6ConsultaListadoseguimientoCasosCerradosdescargaCovid19
¥,¥,D z
(
¥,¥,z {
)
¥,¥,{ |
{
¦,¦, 	
return
§,§, 
DACConsulta
§,§, 
.
§,§, D
6ConsultaListadoseguimientoCasosCerradosdescargaCovid19
§,§, U
(
§,§,U V
)
§,§,V W
;
§,§,W X
}
¨,¨, 	
public
«,«, 
List
«,«, 
<
«,«, 4
&vw_seguimiento_covid19_general_detalle
«,«, :
>
«,«,: ;4
&Consultageneraldetalleseguimientocovid
«,«,< b
(
«,«,b c
)
«,«,c d
{
¬,¬, 	
return
­,­, 
DACConsulta
­,­, 
.
­,­, 4
&Consultageneraldetalleseguimientocovid
­,­, E
(
­,­,E F
)
­,­,F G
;
­,­,G H
}
®,®, 	
public
·,·, 
List
·,·, 
<
·,·, 1
#vw_md_tablero_interventoria_general
·,·, 7
>
·,·,7 8$
Getinterventoriagneral
·,·,9 O
(
·,·,O P
)
·,·,P Q
{
¸,¸, 	
return
¹,¹, 
DACConsulta
¹,¹, 
.
¹,¹, $
Getinterventoriagneral
¹,¹, 5
(
¹,¹,5 6
)
¹,¹,6 7
;
¹,¹,7 8
}
º,º, 	
public
¼,¼, 
List
¼,¼, 
<
¼,¼, :
,vw_md_tablero_intenventoria_general_detalle1
¼,¼, @
>
¼,¼,@ A#
Detalleinterventoria1
¼,¼,B W
(
¼,¼,W X
Int32
¼,¼,X ]
ID
¼,¼,^ `
)
¼,¼,` a
{
½,½, 	
return
¾,¾, 
DACConsulta
¾,¾, 
.
¾,¾, #
Detalleinterventoria1
¾,¾, 4
(
¾,¾,4 5
ID
¾,¾,5 7
)
¾,¾,7 8
;
¾,¾,8 9
}
¿,¿, 	
public
Á,Á, 
List
Á,Á, 
<
Á,Á, :
,vw_md_tablero_interventoria_general_detalle2
Á,Á, @
>
Á,Á,@ A#
Detalleinterventoria2
Á,Á,B W
(
Á,Á,W X
Int32
Á,Á,X ]
ID
Á,Á,^ `
)
Á,Á,` a
{
Â,Â, 	
return
Ã,Ã, 
DACConsulta
Ã,Ã, 
.
Ã,Ã, #
Detalleinterventoria2
Ã,Ã, 4
(
Ã,Ã,4 5
ID
Ã,Ã,5 7
)
Ã,Ã,7 8
;
Ã,Ã,8 9
}
Ä,Ä, 	
public
Å,Å, 
List
Å,Å, 
<
Å,Å, :
,vw_md_tablero_interventoria_general_detalle3
Å,Å, @
>
Å,Å,@ A#
Detalleinterventoria3
Å,Å,B W
(
Å,Å,W X
Int32
Å,Å,X ]
ID
Å,Å,^ `
)
Å,Å,` a
{
Æ,Æ, 	
return
Ç,Ç, 
DACConsulta
Ç,Ç, 
.
Ç,Ç, #
Detalleinterventoria3
Ç,Ç, 4
(
Ç,Ç,4 5
ID
Ç,Ç,5 7
)
Ç,Ç,7 8
;
Ç,Ç,8 9
}
È,È, 	
public
É,É, 
List
É,É, 
<
É,É, :
,vw_md_tablero_interventoria_general_detalle4
É,É, @
>
É,É,@ A#
Detalleinterventoria4
É,É,B W
(
É,É,W X
Int32
É,É,X ]
ID
É,É,^ `
)
É,É,` a
{
Ê,Ê, 	
return
Ë,Ë, 
DACConsulta
Ë,Ë, 
.
Ë,Ë, #
Detalleinterventoria4
Ë,Ë, 4
(
Ë,Ë,4 5
ID
Ë,Ë,5 7
)
Ë,Ë,7 8
;
Ë,Ë,8 9
}
Ì,Ì, 	
public
Î,Î, 
int
Î,Î, 3
%InsertarHospitalizacionPrevenibleDtll
Î,Î, 8
(
Î,Î,8 92
$ecop_hospitalizacion_prevenible_dtll
Î,Î,9 ]
obj
Î,Î,^ a
)
Î,Î,a b
{
Ï,Ï, 	
return
Ğ,Ğ, 

DACInserta
Ğ,Ğ, 
.
Ğ,Ğ, 3
%InsertarHospitalizacionPrevenibleDtll
Ğ,Ğ, C
(
Ğ,Ğ,C D
obj
Ğ,Ğ,D G
)
Ğ,Ğ,G H
;
Ğ,Ğ,H I
}
Ñ,Ñ, 	
public
Ò,Ò, 
List
Ò,Ò, 
<
Ò,Ò, @
2management_vigilancia_epidemiologica_tableroResult
Ò,Ò, F
>
Ò,Ò,F G)
GetVigilanciaEpidemiologica
Ò,Ò,H c
(
Ò,Ò,c d
)
Ò,Ò,d e
{
Ó,Ó, 	
return
Ô,Ô, 
DACConsulta
Ô,Ô, 
.
Ô,Ô, )
GetVigilanciaEpidemiologica
Ô,Ô, :
(
Ô,Ô,: ;
)
Ô,Ô,; <
;
Ô,Ô,< =
}
Õ,Õ, 	
public
Ö,Ö, 
int
Ö,Ö, 7
)InsertarVigilanciaEpidemiologica_archivos
Ö,Ö, <
(
Ö,Ö,< =(
ecop_VE_gestion_documental
Ö,Ö,= W
obj
Ö,Ö,X [
)
Ö,Ö,[ \
{
×,×, 	
return
Ø,Ø, 

DACInserta
Ø,Ø, 
.
Ø,Ø, 7
)InsertarVigilanciaEpidemiologica_archivos
Ø,Ø, G
(
Ø,Ø,G H
obj
Ø,Ø,H K
)
Ø,Ø,K L
;
Ø,Ø,L M
}
Ù,Ù, 	
public
Ú,Ú, 
int
Ú,Ú, .
 InsertarVigilanciaEpidemiologica
Ú,Ú, 3
(
Ú,Ú,3 4,
ecop_vigilancia_epidemiologica
Ú,Ú,4 R
obj
Ú,Ú,S V
)
Ú,Ú,V W
{
Û,Û, 	
return
Ü,Ü, 

DACInserta
Ü,Ü, 
.
Ü,Ü, .
 InsertarVigilanciaEpidemiologica
Ü,Ü, >
(
Ü,Ü,> ?
obj
Ü,Ü,? B
)
Ü,Ü,B C
;
Ü,Ü,C D
}
İ,İ, 	
public
Ş,Ş, 
Int32
Ş,Ş, 4
&InsertarArchivoHospitalziacionEvitable
Ş,Ş, ;
(
Ş,Ş,; <(
ecop_HE_gestion_documental
Ş,Ş,< V
obj
Ş,Ş,W Z
)
Ş,Ş,Z [
{
ß,ß, 	
return
à,à, 

DACInserta
à,à, 
.
à,à, 4
&InsertarArchivoHospitalziacionEvitable
à,à, D
(
à,à,D E
obj
à,à,E H
)
à,à,H I
;
à,à,I J
}
á,á, 	
public
â,â, 
List
â,â, 
<
â,â, !
ref_he_analisisCaso
â,â, '
>
â,â,' ( 
ListAnalisisCasoHE
â,â,) ;
(
â,â,; <
)
â,â,< =
{
ã,ã, 	
return
ä,ä, 
DACConsulta
ä,ä, 
.
ä,ä,  
ListAnalisisCasoHE
ä,ä, 1
(
ä,ä,1 2
)
ä,ä,2 3
;
ä,ä,3 4
}
å,å, 	
public
æ,æ, 
List
æ,æ, 
<
æ,æ, $
ref_he_analisisCaso_si
æ,æ, *
>
æ,æ,* +"
ListAnalisisCasoHESi
æ,æ,, @
(
æ,æ,@ A
)
æ,æ,A B
{
ç,ç, 	
return
è,è, 
DACConsulta
è,è, 
.
è,è, "
ListAnalisisCasoHESi
è,è, 3
(
è,è,3 4
)
è,è,4 5
;
è,è,5 6
}
é,é, 	
public
ê,ê, 
List
ê,ê, 
<
ê,ê, $
ref_he_analisisCaso_no
ê,ê, *
>
ê,ê,* +"
ListAnalisisCasoHENo
ê,ê,, @
(
ê,ê,@ A
)
ê,ê,A B
{
ë,ë, 	
return
ì,ì, 
DACConsulta
ì,ì, 
.
ì,ì, "
ListAnalisisCasoHENo
ì,ì, 3
(
ì,ì,3 4
)
ì,ì,4 5
;
ì,ì,5 6
}
í,í, 	
public
î,î, 
List
î,î, 
<
î,î, H
:management_hospitalizacionPrevenible_detalle_gestionResult
î,î, N
>
î,î,N O9
+GetHospitalizacionPrevenibleDetalle_gestion
î,î,P {
(
î,î,{ |
int
î,î,| 
idHEî,î,€ „
)î,î,„ …
{
ï,ï, 	
return
ğ,ğ, 
DACConsulta
ğ,ğ, 
.
ğ,ğ, 9
+GetHospitalizacionPrevenibleDetalle_gestion
ğ,ğ, J
(
ğ,ğ,J K
idHE
ğ,ğ,K O
)
ğ,ğ,O P
;
ğ,ğ,P Q
}
ñ,ñ, 	
public
ò,ò, (
ecop_HE_gestion_documental
ò,ò, )!
buscarArchivoHEDtll
ò,ò,* =
(
ò,ò,= >
int
ò,ò,> A
HEDtll
ò,ò,B H
)
ò,ò,H I
{
ó,ó, 	
return
ô,ô, 
DACConsulta
ô,ô, 
.
ô,ô, !
buscarArchivoHEDtll
ô,ô, 2
(
ô,ô,2 3
HEDtll
ô,ô,3 9
)
ô,ô,9 :
;
ô,ô,: ;
}
õ,õ, 	
public
ö,ö, (
ecop_VE_gestion_documental
ö,ö, )
buscarArchivoVE
ö,ö,* 9
(
ö,ö,9 :
int
ö,ö,: =
idVE
ö,ö,> B
,
ö,ö,B C
int
ö,ö,D G
tipo
ö,ö,H L
)
ö,ö,L M
{
÷,÷, 	
return
ø,ø, 
DACConsulta
ø,ø, 
.
ø,ø, 
buscarArchivoVE
ø,ø, .
(
ø,ø,. /
idVE
ø,ø,/ 3
,
ø,ø,3 4
tipo
ø,ø,5 9
)
ø,ø,9 :
;
ø,ø,: ;
}
ù,ù, 	
public
ú,ú, 
List
ú,ú, 
<
ú,ú, @
2management_hospitalizacionPrevenible_reporteResult
ú,ú, F
>
ú,ú,F G2
$GetHospitalizacionPrevenible_Reporte
ú,ú,H l
(
ú,ú,l m
)
ú,ú,m n
{
û,û, 	
return
ü,ü, 
DACConsulta
ü,ü, 
.
ü,ü, 2
$GetHospitalizacionPrevenible_Reporte
ü,ü, C
(
ü,ü,C D
)
ü,ü,D E
;
ü,ü,E F
}
ı,ı, 	
public
ş,ş, 
List
ş,ş, 
<
ş,ş, $
vw_md_datos_comunicado
ş,ş, *
>
ş,ş,* + 
GetDatosComunicado
ş,ş,, >
(
ş,ş,> ?
)
ş,ş,? @
{
ÿ,ÿ, 	
return
€-€- 
DACConsulta
€-€- 
.
€-€-  
GetDatosComunicado
€-€- 1
(
€-€-1 2
)
€-€-2 3
;
€-€-3 4
}
-- 	
public
ƒ-ƒ- 
List
ƒ-ƒ- 
<
ƒ-ƒ- 
md_comunicaciones
ƒ-ƒ- %
>
ƒ-ƒ-% &'
TraerdocumentoComunicados
ƒ-ƒ-' @
(
ƒ-ƒ-@ A
Int32
ƒ-ƒ-A F
ID
ƒ-ƒ-G I
)
ƒ-ƒ-I J
{
„-„- 	
return
…-…- 
DACConsulta
…-…- 
.
…-…- '
TraerdocumentoComunicados
…-…- 8
(
…-…-8 9
ID
…-…-9 ;
)
…-…-; <
;
…-…-< =
}
†-†- 	
public
‰-‰- .
 GestionDocumentalMedicamentosCad
‰-‰- /$
Traerimagenindicacioes
‰-‰-0 F
(
‰-‰-F G
Int32
‰-‰-G L
ID
‰-‰-M O
)
‰-‰-O P
{
Š-Š- 	
return
‹-‹- 
DACConsulta
‹-‹- 
.
‹-‹- $
Traerimagenindicacioes
‹-‹- 5
(
‹-‹-5 6
ID
‹-‹-6 8
)
‹-‹-8 9
;
‹-‹-9 :
}
Œ-Œ- 	
public
-- 
List
-- 
<
-- +
vw_md_consolidado_sin_auditor
-- 1
>
--1 2!
Getfactursinauditor
--3 F
(
--F G
)
--G H
{
-- 	
return
-- 
DACConsulta
-- 
.
-- !
Getfactursinauditor
-- 2
(
--2 3
)
--3 4
;
--4 5
}
‘-‘- 	
public
”-”- 0
"ManagmentIngresarIncapacidadResult
”-”- 1*
GetAsignarAuditorConsolidado
”-”-2 N
(
”-”-N O
String
”-”-O U
factura
”-”-V ]
,
”-”-] ^
ref
”-”-_ b 
MessageResponseOBJ
”-”-c u
MsgRes
”-”-v |
)
”-”-| }
{
•-•- 	
return
–-–- 
DACConsulta
–-–- 
.
–-–- *
GetAsignarAuditorConsolidado
–-–- ;
(
–-–-; <
factura
–-–-< C
,
–-–-C D
ref
–-–-E H
MsgRes
–-–-I O
)
–-–-O P
;
–-–-P Q
}
—-—- 	
public
š-š- 
List
š-š- 
<
š-š- :
,managmentprestadoresFacturasRechazadasResult
š-š- @
>
š-š-@ A#
GetFacturasRechazadas
š-š-B W
(
š-š-W X
string
š-š-X ^
factura
š-š-_ f
,
š-š-f g
ref
š-š-h k 
MessageResponseOBJ
š-š-l ~
MsgResš-š- …
)š-š-… †
{
›-›- 	
return
œ-œ- 
DACConsulta
œ-œ- 
.
œ-œ- #
GetFacturasRechazadas
œ-œ- 4
(
œ-œ-4 5
factura
œ-œ-5 <
,
œ-œ-< =
ref
œ-œ-> A
MsgRes
œ-œ-B H
)
œ-œ-H I
;
œ-œ-I J
}
-- 	
public
¢-¢- 
void
¢-¢- (
InsertarCargueContratacion
¢-¢- .
(
¢-¢-. /&
contratacion_cargue_base
¢-¢-/ G
obj
¢-¢-H K
,
¢-¢-K L
List
¢-¢-M Q
<
¢-¢-Q R&
contratacion_cargue_dtll
¢-¢-R j
>
¢-¢-j k
ListaContratacion
¢-¢-l }
,
¢-¢-} ~
ref¢-¢- ‚"
MessageResponseOBJ¢-¢-ƒ •
MsgRes¢-¢-– œ
)¢-¢-œ 
{
£-£- 	

DACInserta
¤-¤- 
.
¤-¤- (
InsertarCargueContratacion
¤-¤- 1
(
¤-¤-1 2
obj
¤-¤-2 5
,
¤-¤-5 6
ListaContratacion
¤-¤-7 H
,
¤-¤-H I
ref
¤-¤-J M
MsgRes
¤-¤-N T
)
¤-¤-T U
;
¤-¤-U V
}
¥-¥- 	
public
§-§- &
contratacion_cargue_base
§-§- '#
getcarguecontratacion
§-§-( =
(
§-§-= >
int
§-§-> A
mes
§-§-B E
,
§-§-E F
int
§-§-G J
aÃ±o
§-§-K N
)
§-§-N O
{
¨-¨- 	
return
©-©- 
DACConsulta
©-©- 
.
©-©- #
getcarguecontratacion
©-©- 4
(
©-©-4 5
mes
©-©-5 8
,
©-©-8 9
aÃ±o
©-©-: =
)
©-©-= >
;
©-©-> ?
}
ª-ª- 	
public
¬-¬- -
ecop_gestion_facturas_aprobadas
¬-¬- ."
GetFacturasAprobadas
¬-¬-/ C
(
¬-¬-C D
int
¬-¬-D G
id_cargue_dtll
¬-¬-H V
)
¬-¬-V W
{
­-­- 	
return
®-®- 
DACConsulta
®-®- 
.
®-®- "
GetFacturasAprobadas
®-®- 3
(
®-®-3 4
id_cargue_dtll
®-®-4 B
)
®-®-B C
;
®-®-C D
}
¯-¯- 	
public
±-±- 
List
±-±- 
<
±-±- 5
'managementFacturasanalistas_lotesResult
±-±- ;
>
±-±-; <!
GetFacturaAnalistas
±-±-= P
(
±-±-P Q
String
±-±-Q W
usuario
±-±-X _
,
±-±-_ `
ref
±-±-a d 
MessageResponseOBJ
±-±-e w
MsgRes
±-±-x ~
)
±-±-~ 
{
²-²- 	
return
³-³- 
DACConsulta
³-³- 
.
³-³- !
GetFacturaAnalistas
³-³- 2
(
³-³-2 3
usuario
³-³-3 :
,
³-³-: ;
ref
³-³-< ?
MsgRes
³-³-@ F
)
³-³-F G
;
³-³-G H
}
´-´- 	
public
¶-¶- 
List
¶-¶- 
<
¶-¶- 8
*managementFacturasanalistas_lotes_okResult
¶-¶- >
>
¶-¶-> ?#
GetFacturaAnalistasok
¶-¶-@ U
(
¶-¶-U V
ref
¶-¶-V Y 
MessageResponseOBJ
¶-¶-Z l
MsgRes
¶-¶-m s
)
¶-¶-s t
{
·-·- 	
return
¸-¸- 
DACConsulta
¸-¸- 
.
¸-¸- #
GetFacturaAnalistasok
¸-¸- 4
(
¸-¸-4 5
ref
¸-¸-5 8
MsgRes
¸-¸-9 ?
)
¸-¸-? @
;
¸-¸-@ A
}
¹-¹- 	
public
º-º- 
List
º-º- 
<
º-º- 9
+Management_Lotes_totales_con_analistaResult
º-º- ?
>
º-º-? @#
GetLotesAnalistaTotal
º-º-A V
(
º-º-V W
DateTime
º-º-W _
fecha_inicio
º-º-` l
,
º-º-l m
DateTime
º-º-n v
	fecha_finº-º-w €
,º-º-€ 
refº-º-‚ …"
MessageResponseOBJº-º-† ˜
MsgResº-º-™ Ÿ
)º-º-Ÿ  
{
»-»- 	
return
¼-¼- 
DACConsulta
¼-¼- 
.
¼-¼- #
GetLotesAnalistaTotal
¼-¼- 4
(
¼-¼-4 5
fecha_inicio
¼-¼-5 A
,
¼-¼-A B
	fecha_fin
¼-¼-C L
,
¼-¼-L M
ref
¼-¼-N Q
MsgRes
¼-¼-R X
)
¼-¼-X Y
;
¼-¼-Y Z
}
½-½- 	
public
À-À- 
List
À-À- 
<
À-À- =
/Management_Lotes_totales_con_analistaDtllResult
À-À- C
>
À-À-C D'
GetLotesAnalistaTotalDtll
À-À-E ^
(
À-À-^ _
Int32
À-À-_ d
Id
À-À-e g
,
À-À-g h
ref
À-À-i l 
MessageResponseOBJ
À-À-m 
MsgResÀ-À-€ †
)À-À-† ‡
{
Á-Á- 	
return
Â-Â- 
DACConsulta
Â-Â- 
.
Â-Â- '
GetLotesAnalistaTotalDtll
Â-Â- 8
(
Â-Â-8 9
Id
Â-Â-9 ;
,
Â-Â-; <
ref
Â-Â-= @
MsgRes
Â-Â-A G
)
Â-Â-G H
;
Â-Â-H I
}
Ã-Ã- 	
public
Æ-Æ- 
Int32
Æ-Æ- "
InsertarFirmadigital
Æ-Æ- )
(
Æ-Æ-) *+
ecop_firma_digital_cod_barras
Æ-Æ-* G
obj
Æ-Æ-H K
,
Æ-Æ-K L
ref
Æ-Æ-M P 
MessageResponseOBJ
Æ-Æ-Q c
MsgRes
Æ-Æ-d j
)
Æ-Æ-j k
{
Ç-Ç- 	
return
È-È- 

DACInserta
È-È- 
.
È-È- "
InsertarFirmadigital
È-È- 2
(
È-È-2 3
obj
È-È-3 6
,
È-È-6 7
ref
È-È-8 ;
MsgRes
È-È-< B
)
È-È-B C
;
È-È-C D
}
É-É- 	
public
Ê-Ê- +
ecop_firma_digital_cod_barras
Ê-Ê- ,
GetDtll_codBarras
Ê-Ê-- >
(
Ê-Ê-> ?
Int32
Ê-Ê-? D
?
Ê-Ê-D E
	idDetalle
Ê-Ê-F O
)
Ê-Ê-O P
{
Ë-Ë- 	
return
Ì-Ì- 
DACConsulta
Ì-Ì- 
.
Ì-Ì- 
GetDtll_codBarras
Ì-Ì- 0
(
Ì-Ì-0 1
	idDetalle
Ì-Ì-1 :
)
Ì-Ì-: ;
;
Ì-Ì-; <
}
Í-Í- 	
public
Î-Î- *
Management_consulta_QRResult
Î-Î- +
GetConsultaQr
Î-Î-, 9
(
Î-Î-9 :
Int32
Î-Î-: ?
?
Î-Î-? @
	idDetalle
Î-Î-A J
)
Î-Î-J K
{
Ï-Ï- 	
return
Ğ-Ğ- 
DACConsulta
Ğ-Ğ- 
.
Ğ-Ğ- 
GetConsultaQr
Ğ-Ğ- ,
(
Ğ-Ğ-, -
	idDetalle
Ğ-Ğ-- 6
)
Ğ-Ğ-6 7
;
Ğ-Ğ-7 8
}
Ñ-Ñ- 	
public
Ò-Ò- 
Int32
Ò-Ò- &
InsertarFirmadigitalsami
Ò-Ò- -
(
Ò-Ò-- .%
ecop_firma_digital_sami
Ò-Ò-. E
obj
Ò-Ò-F I
,
Ò-Ò-I J
ref
Ò-Ò-K N 
MessageResponseOBJ
Ò-Ò-O a
MsgRes
Ò-Ò-b h
)
Ò-Ò-h i
{
Ó-Ó- 	
return
Ô-Ô- 

DACInserta
Ô-Ô- 
.
Ô-Ô- &
InsertarFirmadigitalsami
Ô-Ô- 6
(
Ô-Ô-6 7
obj
Ô-Ô-7 :
,
Ô-Ô-: ;
ref
Ô-Ô-< ?
MsgRes
Ô-Ô-@ F
)
Ô-Ô-F G
;
Ô-Ô-G H
}
Õ-Õ- 	
public
Ö-Ö- 
List
Ö-Ö- 
<
Ö-Ö- 7
)vw_odontologia_detallado_historia_clinica
Ö-Ö- =
>
Ö-Ö-= >'
getdetallehistoriaclinica
Ö-Ö-? X
(
Ö-Ö-X Y
)
Ö-Ö-Y Z
{
×-×- 	
return
Ø-Ø- 
DACConsulta
Ø-Ø- 
.
Ø-Ø- '
getdetallehistoriaclinica
Ø-Ø- 8
(
Ø-Ø-8 9
)
Ø-Ø-9 :
;
Ø-Ø-: ;
}
Ù-Ù- 	
public
Û-Û- 
Int32
Û-Û- +
InsertarGestionFacturadigital
Û-Û- 2
(
Û-Û-2 3*
ecop_gestion_factura_digital
Û-Û-3 O
obj
Û-Û-P S
,
Û-Û-S T
ref
Û-Û-U X 
MessageResponseOBJ
Û-Û-Y k
MsgRes
Û-Û-l r
)
Û-Û-r s
{
Ü-Ü- 	
return
İ-İ- 

DACInserta
İ-İ- 
.
İ-İ- +
InsertarGestionFacturadigital
İ-İ- ;
(
İ-İ-; <
obj
İ-İ-< ?
,
İ-İ-? @
ref
İ-İ-A D
MsgRes
İ-İ-E K
)
İ-İ-K L
;
İ-İ-L M
}
Ş-Ş- 	
public
ß-ß- 
Int32
ß-ß- 0
"InsertarGestionFacturadigitalGasto
ß-ß- 7
(
ß-ß-7 80
"ecop_gestion_factura_digital_gasto
ß-ß-8 Z
obj
ß-ß-[ ^
,
ß-ß-^ _
ref
ß-ß-` c 
MessageResponseOBJ
ß-ß-d v
MsgRes
ß-ß-w }
)
ß-ß-} ~
{
à-à- 	
return
á-á- 

DACInserta
á-á- 
.
á-á- 0
"InsertarGestionFacturadigitalGasto
á-á- @
(
á-á-@ A
obj
á-á-A D
,
á-á-D E
ref
á-á-F I
MsgRes
á-á-J P
)
á-á-P Q
;
á-á-Q R
}
â-â- 	
public
ä-ä- 
void
ä-ä- 7
)insertarListadoGestionFacturadigitalGasto
ä-ä- =
(
ä-ä-= >
List
ä-ä-> B
<
ä-ä-B C0
"ecop_gestion_factura_digital_gasto
ä-ä-C e
>
ä-ä-e f
obj
ä-ä-g j
,
ä-ä-j k
ref
ä-ä-l o!
MessageResponseOBJä-ä-p ‚
MsgResä-ä-ƒ ‰
)ä-ä-‰ Š
{
å-å- 	

DACInserta
æ-æ- 
.
æ-æ- 7
)insertarListadoGestionFacturadigitalGasto
æ-æ- @
(
æ-æ-@ A
obj
æ-æ-A D
,
æ-æ-D E
ref
æ-æ-F I
MsgRes
æ-æ-J P
)
æ-æ-P Q
;
æ-æ-Q R
}
ç-ç- 	
public
é-é- 
void
é-é- 2
$ActualizarGestionFacturadigitalGasto
é-é- 8
(
é-é-8 9,
vw_factura_digital_gasto_total
é-é-9 W
obj
é-é-X [
,
é-é-[ \
ref
é-é-] ` 
MessageResponseOBJ
é-é-a s
MsgRes
é-é-t z
)
é-é-z {
{
ê-ê- 	
DACActualiza
ë-ë- 
.
ë-ë- 2
$ActualizarGestionFacturadigitalGasto
ë-ë- =
(
ë-ë-= >
obj
ë-ë-> A
,
ë-ë-A B
ref
ë-ë-C F
MsgRes
ë-ë-G M
)
ë-ë-M N
;
ë-ë-N O
}
ì-ì- 	
public
î-î- 
List
î-î- 
<
î-î- %
ref_tipo_gasto_facturas
î-î- +
>
î-î-+ ,(
Getref_tipo_gasto_facturas
î-î-- G
(
î-î-G H
ref
î-î-H K 
MessageResponseOBJ
î-î-L ^
MsgRes
î-î-_ e
)
î-î-e f
{
ï-ï- 	
return
ğ-ğ- 
DACConsulta
ğ-ğ- 
.
ğ-ğ- (
Getref_tipo_gasto_facturas
ğ-ğ- 9
(
ğ-ğ-9 :
ref
ğ-ğ-: =
MsgRes
ğ-ğ-> D
)
ğ-ğ-D E
;
ğ-ğ-E F
}
ñ-ñ- 	
public
ó-ó- %
ecop_firma_digital_sami
ó-ó- &
	GetFirmas
ó-ó-' 0
(
ó-ó-0 1
Int32
ó-ó-1 6
?
ó-ó-6 7
	idusuario
ó-ó-8 A
)
ó-ó-A B
{
ô-ô- 	
return
õ-õ- 
DACComonClass
õ-õ-  
.
õ-õ-  !
	GetFirmas
õ-õ-! *
(
õ-õ-* +
	idusuario
õ-õ-+ 4
)
õ-õ-4 5
;
õ-õ-5 6
}
ö-ö- 	
public
ø-ø- 6
(management_ecop_firma_digital_samiResult
ø-ø- 7
GetFirmasId
ø-ø-8 C
(
ø-ø-C D
int
ø-ø-D G
?
ø-ø-G H
	idUsuario
ø-ø-I R
)
ø-ø-R S
{
ù-ù- 	
return
ú-ú- 
DACComonClass
ú-ú-  
.
ú-ú-  !
GetFirmasId
ú-ú-! ,
(
ú-ú-, -
	idUsuario
ú-ú-- 6
)
ú-ú-6 7
;
ú-ú-7 8
}
û-û- 	
public
ı-ı- %
ecop_firma_digital_sami
ı-ı- &
traerDatosFirma
ı-ı-' 6
(
ı-ı-6 7
int
ı-ı-7 :
?
ı-ı-: ;
	idUsuario
ı-ı-< E
)
ı-ı-E F
{
ş-ş- 	
return
ÿ-ÿ- 
DACConsulta
ÿ-ÿ- 
.
ÿ-ÿ- 
traerDatosFirma
ÿ-ÿ- .
(
ÿ-ÿ-. /
	idUsuario
ÿ-ÿ-/ 8
)
ÿ-ÿ-8 9
;
ÿ-ÿ-9 :
}
€.€. 	
public
‚.‚. 
List
‚.‚. 
<
‚.‚. ;
-management_ecop_firma_digital_sami_todoResult
‚.‚. A
>
‚.‚.A B"
ListadoFirmasSinRuta
‚.‚.C W
(
‚.‚.W X
)
‚.‚.X Y
{
ƒ.ƒ. 	
return
„.„. 
DACConsulta
„.„. 
.
„.„. "
ListadoFirmasSinRuta
„.„. 3
(
„.„.3 4
)
„.„.4 5
;
„.„.5 6
}
….…. 	
public
†.†. 
List
†.†. 
<
†.†. %
ecop_firma_digital_sami
†.†. +
>
†.†.+ ,!
listaFirmasUsuarios
†.†.- @
(
†.†.@ A
)
†.†.A B
{
‡.‡. 	
return
ˆ.ˆ. 
DACComonClass
ˆ.ˆ.  
.
ˆ.ˆ.  !!
listaFirmasUsuarios
ˆ.ˆ.! 4
(
ˆ.ˆ.4 5
)
ˆ.ˆ.5 6
;
ˆ.ˆ.6 7
}
‰.‰. 	
public
‹.‹. 
void
‹.‹. )
ActualizarRutaFirmasDigital
‹.‹. /
(
‹.‹./ 0
string
‹.‹.0 6
ruta
‹.‹.7 ;
,
‹.‹.; <
int
‹.‹.= @
?
‹.‹.@ A
idFirma
‹.‹.B I
)
‹.‹.I J
{
Œ.Œ. 	
DACActualiza
.. 
.
.. )
ActualizarRutaFirmasDigital
.. 4
(
..4 5
ruta
..5 9
,
..9 :
idFirma
..; B
)
..B C
;
..C D
}
.. 	
public
.. 
int
.. 
cantidaddias
.. 
(
..  
int
..  #
idconcurrencia
..$ 2
)
..2 3
{
‘.‘. 	
return
’.’. 
DACConsulta
’.’. 
.
’.’. 
cantidaddias
’.’. +
(
’.’.+ ,
idconcurrencia
’.’., :
)
’.’.: ;
;
’.’.; <
}
“.“. 	
public
–.–. 
void
–.–. +
ActualizarAuditorConcurrencia
–.–. 1
(
–.–.1 2
ecop_concurrencia
–.–.2 C
OBJ
–.–.D G
,
–.–.G H
ref
–.–.I L 
MessageResponseOBJ
–.–.M _
MsgRes
–.–.` f
)
–.–.f g
{
—.—. 	
DACActualiza
˜.˜. 
.
˜.˜. +
ActualizarAuditorConcurrencia
˜.˜. 6
(
˜.˜.6 7
OBJ
˜.˜.7 :
,
˜.˜.: ;
ref
˜.˜.< ?
MsgRes
˜.˜.@ F
)
˜.˜.F G
;
˜.˜.G H
}
™.™. 	
public
›.›. 
void
›.›. $
ActualizarAuditorCenso
›.›. *
(
›.›.* +

ecop_censo
›.›.+ 5
OBJ
›.›.6 9
,
›.›.9 :
ref
›.›.; > 
MessageResponseOBJ
›.›.? Q
MsgRes
›.›.R X
)
›.›.X Y
{
œ.œ. 	
DACActualiza
.. 
.
.. $
ActualizarAuditorCenso
.. /
(
../ 0
OBJ
..0 3
,
..3 4
ref
..5 8
MsgRes
..9 ?
)
..? @
;
..@ A
}
.. 	
public
 . . 
List
 . . 
<
 . . 5
'ManagmentDetalleFacturasDevueltasResult
 . . ;
>
 . .; <(
GetDetalleFacturadevuletas
 . .= W
(
 . .W X
int
 . .X [

id_detalle
 . .\ f
)
 . .f g
{
¡.¡. 	
return
¢.¢. 
DACConsulta
¢.¢. 
.
¢.¢. (
GetDetalleFacturadevuletas
¢.¢. 9
(
¢.¢.9 :

id_detalle
¢.¢.: D
)
¢.¢.D E
;
¢.¢.E F
}
£.£. 	
public
¥.¥. 
List
¥.¥. 
<
¥.¥. &
view_ref_estado_facturas
¥.¥. ,
>
¥.¥., -
GetEstadoFacturas
¥.¥.. ?
(
¥.¥.? @
)
¥.¥.@ A
{
¦.¦. 	
return
§.§. 
DACConsulta
§.§. 
.
§.§. 
GetEstadoFacturas
§.§. 0
(
§.§.0 1
)
§.§.1 2
;
§.§.2 3
}
¨.¨. 	
public
ª.ª. 
Int32
ª.ª. 2
$InsertarLogCambiosGetionHospitalaria
ª.ª. 9
(
ª.ª.9 :.
 log_cambios_gestion_hospitalaria
ª.ª.: Z
obj
ª.ª.[ ^
,
ª.ª.^ _
ref
ª.ª.` c 
MessageResponseOBJ
ª.ª.d v
MsgRes
ª.ª.w }
)
ª.ª.} ~
{
«.«. 	
return
¬.¬. 

DACInserta
¬.¬. 
.
¬.¬. 2
$InsertarLogCambiosGetionHospitalaria
¬.¬. B
(
¬.¬.B C
obj
¬.¬.C F
,
¬.¬.F G
ref
¬.¬.H K
MsgRes
¬.¬.L R
)
¬.¬.R S
;
¬.¬.S T
}
­.­. 	
public
°.°. 
void
°.°. ,
ActualizarCambiosPacienteCenso
°.°. 2
(
°.°.2 3

ecop_censo
°.°.3 =
OBJ
°.°.> A
,
°.°.A B
String
°.°.C I

tipocambio
°.°.J T
,
°.°.T U
ref
°.°.V Y 
MessageResponseOBJ
°.°.Z l
MsgRes
°.°.m s
)
°.°.s t
{
±.±. 	
DACActualiza
².². 
.
².². ,
ActualizarCambiosPacienteCenso
².². 7
(
².².7 8
OBJ
².².8 ;
,
².².; <

tipocambio
².².= G
,
².².G H
ref
².².I L
MsgRes
².².M S
)
².².S T
;
².².T U
}
³.³. 	
public
µ.µ. 
void
µ.µ. ,
ActualizarCambiosPacienteConcu
µ.µ. 2
(
µ.µ.2 3
ecop_concurrencia
µ.µ.3 D
OBJ
µ.µ.E H
,
µ.µ.H I
String
µ.µ.J P

tipocambio
µ.µ.Q [
,
µ.µ.[ \
ref
µ.µ.] ` 
MessageResponseOBJ
µ.µ.a s
MsgRes
µ.µ.t z
)
µ.µ.z {
{
¶.¶. 	
DACActualiza
·.·. 
.
·.·. ,
ActualizarCambiosPacienteConcu
·.·. 7
(
·.·.7 8
OBJ
·.·.8 ;
,
·.·.; <

tipocambio
·.·.= G
,
·.·.G H
ref
·.·.I L
MsgRes
·.·.M S
)
·.·.S T
;
·.·.T U
}
¸.¸. 	
public
º.º. 
List
º.º. 
<
º.º. 5
'management_egresos_categorizacionResult
º.º. ;
>
º.º.; <*
listadoEgresosCategorizacion
º.º.= Y
(
º.º.Y Z
int
º.º.Z ]
idConcurrencia
º.º.^ l
)
º.º.l m
{
».». 	
return
¼.¼. 
DACConsulta
¼.¼. 
.
¼.¼. *
listadoEgresosCategorizacion
¼.¼. ;
(
¼.¼.; <
idConcurrencia
¼.¼.< J
)
¼.¼.J K
;
¼.¼.K L
}
½.½. 	
public
¾.¾. 
Int32
¾.¾. &
InsertarFacturaAprobadas
¾.¾. -
(
¾.¾.- .-
ecop_gestion_facturas_aprobadas
¾.¾.. M
obj
¾.¾.N Q
,
¾.¾.Q R
ref
¾.¾.S V 
MessageResponseOBJ
¾.¾.W i
MsgRes
¾.¾.j p
)
¾.¾.p q
{
¿.¿. 	
return
À.À. 

DACInserta
À.À. 
.
À.À. &
InsertarFacturaAprobadas
À.À. 6
(
À.À.6 7
obj
À.À.7 :
,
À.À.: ;
ref
À.À.< ?
MsgRes
À.À.@ F
)
À.À.F G
;
À.À.G H
}
Á.Á. 	
public
Ä.Ä. 
List
Ä.Ä. 
<
Ä.Ä. $
vw_analistas_recepcion
Ä.Ä. *
>
Ä.Ä.* +
GetListAnalistas
Ä.Ä., <
(
Ä.Ä.< =
)
Ä.Ä.= >
{
Å.Å. 	
return
Æ.Æ. 
DACConsulta
Æ.Æ. 
.
Æ.Æ. 
GetListAnalistas
Æ.Æ. /
(
Æ.Æ./ 0
)
Æ.Æ.0 1
;
Æ.Æ.1 2
}
Ç.Ç. 	
public
Ê.Ê. 
void
Ê.Ê. "
Insertaranalistalote
Ê.Ê. (
(
Ê.Ê.( )
ref_analista_lote
Ê.Ê.) :
obj
Ê.Ê.; >
,
Ê.Ê.> ?
ref
Ê.Ê.@ C 
MessageResponseOBJ
Ê.Ê.D V
MsgRes
Ê.Ê.W ]
)
Ê.Ê.] ^
{
Ë.Ë. 	

DACInserta
Ì.Ì. 
.
Ì.Ì. "
Insertaranalistalote
Ì.Ì. +
(
Ì.Ì.+ ,
obj
Ì.Ì., /
,
Ì.Ì./ 0
ref
Ì.Ì.1 4
MsgRes
Ì.Ì.5 ;
)
Ì.Ì.; <
;
Ì.Ì.< =
}
Í.Í. 	
public
Î.Î. 
List
Î.Î. 
<
Î.Î. 3
%managmentprestadoresFacturasOBSResult
Î.Î. 9
>
Î.Î.9 :#
GetConsultaObsFactura
Î.Î.; P
(
Î.Î.P Q
Int32
Î.Î.Q V
?
Î.Î.V W
id_af
Î.Î.X ]
)
Î.Î.] ^
{
Ï.Ï. 	
return
Ğ.Ğ. 
DACConsulta
Ğ.Ğ. 
.
Ğ.Ğ. #
GetConsultaObsFactura
Ğ.Ğ. 4
(
Ğ.Ğ.4 5
id_af
Ğ.Ğ.5 :
)
Ğ.Ğ.: ;
;
Ğ.Ğ.; <
}
Ñ.Ñ. 	
public
Ò.Ò. 
List
Ò.Ò. 
<
Ò.Ò. 8
*managmentprestadoresfacturasDEV_RECHResult
Ò.Ò. >
>
Ò.Ò.> ?'
GetConsultaRechDevFactura
Ò.Ò.@ Y
(
Ò.Ò.Y Z
)
Ò.Ò.Z [
{
Ó.Ó. 	
return
Ô.Ô. 
DACConsulta
Ô.Ô. 
.
Ô.Ô. '
GetConsultaRechDevFactura
Ô.Ô. 8
(
Ô.Ô.8 9
)
Ô.Ô.9 :
;
Ô.Ô.: ;
}
Õ.Õ. 	
public
×.×. 
List
×.×. 
<
×.×. :
,managmentprestadoresfacturasDEV_RECHV2Result
×.×. @
>
×.×.@ A)
GetConsultaRechDevFacturaV2
×.×.B ]
(
×.×.] ^
int
×.×.^ a
?
×.×.a b
	idfactura
×.×.c l
)
×.×.l m
{
Ø.Ø. 	
return
Ù.Ù. 
DACConsulta
Ù.Ù. 
.
Ù.Ù. )
GetConsultaRechDevFacturaV2
Ù.Ù. :
(
Ù.Ù.: ;
	idfactura
Ù.Ù.; D
)
Ù.Ù.D E
;
Ù.Ù.E F
}
Ú.Ú. 	
public
Ü.Ü. 
List
Ü.Ü. 
<
Ü.Ü. 6
(getfacturabynumfactura_idprestadorResult
Ü.Ü. <
>
Ü.Ü.< =&
ValidarEvistenciaFactura
Ü.Ü.> V
(
Ü.Ü.V W
int
Ü.Ü.W Z
	idfactura
Ü.Ü.[ d
,
Ü.Ü.d e
string
Ü.Ü.f l
numnuevofactura
Ü.Ü.m |
,
Ü.Ü.| }
stringÜ.Ü.~ „
numidprestadorÜ.Ü.… “
)Ü.Ü.“ ”
{
İ.İ. 	
return
Ş.Ş. 
DACConsulta
Ş.Ş. 
.
Ş.Ş. &
ValidarEvistenciaFactura
Ş.Ş. 7
(
Ş.Ş.7 8
	idfactura
Ş.Ş.8 A
,
Ş.Ş.A B
numnuevofactura
Ş.Ş.C R
,
Ş.Ş.R S
numidprestador
Ş.Ş.T b
)
Ş.Ş.b c
;
Ş.Ş.c d
}
ß.ß. 	
public
á.á. 
List
á.á. 
<
á.á. *
ecop_gestion_factura_digital
á.á. 0
>
á.á.0 1'
GetConsultaGestionFactura
á.á.2 K
(
á.á.K L
Int32
á.á.L Q
?
á.á.Q R
	idDetalle
á.á.S \
)
á.á.\ ]
{
â.â. 	
return
ã.ã. 
DACConsulta
ã.ã. 
.
ã.ã. '
GetConsultaGestionFactura
ã.ã. 8
(
ã.ã.8 9
	idDetalle
ã.ã.9 B
)
ã.ã.B C
;
ã.ã.C D
}
ä.ä. 	
public
å.å. 
List
å.å. 
<
å.å.  
factura_devolucion
å.å. &
>
å.å.& '*
GetConsultaGestionDevolucion
å.å.( D
(
å.å.D E
Int32
å.å.E J
?
å.å.J K
	idDetalle
å.å.L U
)
å.å.U V
{
æ.æ. 	
return
ç.ç. 
DACConsulta
ç.ç. 
.
ç.ç. *
GetConsultaGestionDevolucion
ç.ç. ;
(
ç.ç.; <
	idDetalle
ç.ç.< E
)
ç.ç.E F
;
ç.ç.F G
}
è.è. 	
public
é.é. 
List
é.é. 
<
é.é. 9
+managmentprestadoresfacturasACEP_ASIGResult
é.é. ?
>
é.é.? @)
GetConsultaAcep_AsigFactura
é.é.A \
(
é.é.\ ]
)
é.é.] ^
{
ê.ê. 	
return
ë.ë. 
DACConsulta
ë.ë. 
.
ë.ë. )
GetConsultaAcep_AsigFactura
ë.ë. :
(
ë.ë.: ;
)
ë.ë.; <
;
ë.ë.< =
}
ì.ì. 	
public
í.í. 
List
í.í. 
<
í.í. 5
'managmentprestadoresNumeroFacturaResult
í.í. ;
>
í.í.; <&
GetConsultaNumeroFactura
í.í.= U
(
í.í.U V
String
í.í.V \
	numeroFac
í.í.] f
)
í.í.f g
{
î.î. 	
return
ï.ï. 
DACConsulta
ï.ï. 
.
ï.ï. &
GetConsultaNumeroFactura
ï.ï. 7
(
ï.ï.7 8
	numeroFac
ï.ï.8 A
)
ï.ï.A B
;
ï.ï.B C
}
ğ.ğ. 	
public
ò.ò. 
List
ò.ò. 
<
ò.ò.  
factura_devolucion
ò.ò. &
>
ò.ò.& '-
GetfacturadevolucionByIdFactura
ò.ò.( G
(
ò.ò.G H
int
ò.ò.H K
	idfactura
ò.ò.L U
)
ò.ò.U V
{
ó.ó. 	
return
ô.ô. 
DACConsulta
ô.ô. 
.
ô.ô. -
GetfacturadevolucionByIdFactura
ô.ô. >
(
ô.ô.> ?
	idfactura
ô.ô.? H
)
ô.ô.H I
;
ô.ô.I J
}
õ.õ. 	
public
÷.÷. 
Int32
÷.÷. .
 InsertarFacturacionContabilizado
÷.÷. 5
(
÷.÷.5 6
List
÷.÷.6 :
<
÷.÷.: ;9
+ecop_gestion_factura_digital_contabilizados
÷.÷.; f
>
÷.÷.f g

OBJDetalle
÷.÷.h r
,
÷.÷.r s
ref
÷.÷.t w!
MessageResponseOBJ÷.÷.x Š
MsgRes÷.÷.‹ ‘
)÷.÷.‘ ’
{
ø.ø. 	
return
ù.ù. 

DACInserta
ù.ù. 
.
ù.ù. .
 InsertarFacturacionContabilizado
ù.ù. >
(
ù.ù.> ?

OBJDetalle
ù.ù.? I
,
ù.ù.I J
ref
ù.ù.K N
MsgRes
ù.ù.O U
)
ù.ù.U V
;
ù.ù.V W
}
ú.ú. 	
public
û.û. 
Int32
û.û. $
InsertarControlCambios
û.û. +
(
û.û.+ ,:
,ecop_gestion_factura_digital_control_cambios
û.û., X
OBJ
û.û.Y \
,
û.û.\ ]
ref
û.û.^ a 
MessageResponseOBJ
û.û.b t
MsgRes
û.û.u {
)
û.û.{ |
{
ü.ü. 	
return
ı.ı. 

DACInserta
ı.ı. 
.
ı.ı. $
InsertarControlCambios
ı.ı. 4
(
ı.ı.4 5
OBJ
ı.ı.5 8
,
ı.ı.8 9
ref
ı.ı.: =
MsgRes
ı.ı.> D
)
ı.ı.D E
;
ı.ı.E F
}
ş.ş. 	
public
ÿ.ÿ. 
int
ÿ.ÿ. '
ActualizarEstado_Facturas
ÿ.ÿ. ,
(
ÿ.ÿ., -
int
ÿ.ÿ.- 0
idFac
ÿ.ÿ.1 6
,
ÿ.ÿ.6 7
int
ÿ.ÿ.8 ;
estadoAntiguo
ÿ.ÿ.< I
,
ÿ.ÿ.I J
int
ÿ.ÿ.K N
estadoNuevo
ÿ.ÿ.O Z
)
ÿ.ÿ.Z [
{
€/€/ 	
return
// 
DACActualiza
// 
.
//  '
ActualizarEstado_Facturas
//  9
(
//9 :
idFac
//: ?
,
//? @
estadoAntiguo
//A N
,
//N O
estadoNuevo
//P [
)
//[ \
;
//\ ]
}
‚/‚/ 	
public
„/„/ 
List
„/„/ 
<
„/„/ $
md_prefacturas_detalle
„/„/ *
>
„/„/* +$
GetPrefacturasByIdLote
„/„/, B
(
„/„/B C
int
„/„/C F
lote
„/„/G K
)
„/„/K L
{
…/…/ 	
return
†/†/ 
DACConsulta
†/†/ 
.
†/†/ $
GetPrefacturasByIdLote
†/†/ 5
(
†/†/5 6
lote
†/†/6 :
)
†/†/: ;
;
†/†/; <
}
‡/‡/ 	
public
‰/‰/ =
/management_prefacturas_existeBeneficiarioResult
‰/‰/ >+
PrefacturasExisteBeneficiario
‰/‰/? \
(
‰/‰/\ ]
string
‰/‰/] c 
numeroBeneficiario
‰/‰/d v
,
‰/‰/v w
DateTime‰/‰/x €$
fechaDespachoFormula‰/‰/ •
)‰/‰/• –
{
Š/Š/ 	
return
‹/‹/ 
DACConsulta
‹/‹/ 
.
‹/‹/ +
PrefacturasExisteBeneficiario
‹/‹/ <
(
‹/‹/< = 
numeroBeneficiario
‹/‹/= O
,
‹/‹/O P"
fechaDespachoFormula
‹/‹/Q e
)
‹/‹/e f
;
‹/‹/f g
}
Œ/Œ/ 	
public
// 
string
// &
PrefacturasExisteFactura
// .
(
//. /
string
/// 5 
numeroBeneficiario
//6 H
,
//H I
int
//J M
numeroUnidades
//N \
,
//\ ]
DateTime
//^ f"
fechaDespachoFormula
//g {
,
//{ |
decimal//} „
vlrUnidades//… 
,
// 
string
// 
cum
// 
,
// 
string
//  
nombreComercial
//! 0
)
//0 1
{
// 	
return
‘/‘/ 
DACConsulta
‘/‘/ 
.
‘/‘/ &
PrefacturasExisteFactura
‘/‘/ 7
(
‘/‘/7 8 
numeroBeneficiario
‘/‘/8 J
,
‘/‘/J K
numeroUnidades
‘/‘/L Z
,
‘/‘/Z ["
fechaDespachoFormula
‘/‘/\ p
,
‘/‘/p q
vlrUnidades
‘/‘/r }
,
‘/‘/} ~
cum‘/‘/ ‚
,‘/‘/‚ ƒ
nombreComercial‘/‘/„ “
)‘/‘/“ ”
;‘/‘/” •
}
’/’/ 	
public
”/”/ ?
1management_prefacturas_regionalBeneficiarioResult
”/”/ @-
PrefacturasRegionalBeneficiario
”/”/A `
(
”/”/` a
string
”/”/a g 
numeroBeneficiario
”/”/h z
,
”/”/z {
DateTime”/”/| „$
fechaDespachoFormula”/”/… ™
,”/”/™ š
string”/”/› ¡
nombreEspecial”/”/¢ °
)”/”/° ±
{
•/•/ 	
return
–/–/ 
DACConsulta
–/–/ 
.
–/–/ -
PrefacturasRegionalBeneficiario
–/–/ >
(
–/–/> ? 
numeroBeneficiario
–/–/? Q
,
–/–/Q R"
fechaDespachoFormula
–/–/S g
,
–/–/g h
nombreEspecial
–/–/i w
)
–/–/w x
;
–/–/x y
}
—/—/ 	
public
™/™/ 
void
™/™/ (
ActualizarPrefacturaCargue
™/™/ .
(
™/™/. /
int
™/™// 2
?
™/™/2 3

cargueBase
™/™/4 >
,
™/™/> ?
int
™/™/@ C
?
™/™/C D
fase
™/™/E I
,
™/™/I J
string
™/™/K Q
usuario
™/™/R Y
,
™/™/Y Z
int
™/™/[ ^
?
™/™/^ _
	terminado
™/™/` i
)
™/™/i j
{
š/š/ 	
DACActualiza
›/›/ 
.
›/›/ (
ActualizarPrefacturaCargue
›/›/ 3
(
›/›/3 4

cargueBase
›/›/4 >
,
›/›/> ?
fase
›/›/@ D
,
›/›/D E
usuario
›/›/F M
,
›/›/M N
	terminado
›/›/O X
)
›/›/X Y
;
›/›/Y Z
}
œ/œ/ 	
public
// 
void
// ,
ActualizarPrefacturaCargueFase
// 2
(
//2 3
int
//3 6
?
//6 7

cargueBase
//8 B
,
//B C
int
//D G
?
//G H
fase
//I M
,
//M N
string
//O U
usuario
//V ]
)
//] ^
{
// 	
DACActualiza
Ÿ/Ÿ/ 
.
Ÿ/Ÿ/ ,
ActualizarPrefacturaCargueFase
Ÿ/Ÿ/ 7
(
Ÿ/Ÿ/7 8

cargueBase
Ÿ/Ÿ/8 B
,
Ÿ/Ÿ/B C
fase
Ÿ/Ÿ/D H
,
Ÿ/Ÿ/H I
usuario
Ÿ/Ÿ/J Q
)
Ÿ/Ÿ/Q R
;
Ÿ/Ÿ/R S
}
 / / 	
public
¢/¢/ 
int
¢/¢/ 4
&ActualizarPrefacturaCargueFaseDevolver
¢/¢/ 9
(
¢/¢/9 :
int
¢/¢/: =
?
¢/¢/= >

cargueBase
¢/¢/? I
)
¢/¢/I J
{
£/£/ 	
return
¤/¤/ 
DACActualiza
¤/¤/ 
.
¤/¤/  4
&ActualizarPrefacturaCargueFaseDevolver
¤/¤/  F
(
¤/¤/F G

cargueBase
¤/¤/G Q
)
¤/¤/Q R
;
¤/¤/R S
}
¥/¥/ 	
public
¨/¨/ 
int
¨/¨/ -
ActualizarConteo_Facturas_fase1
¨/¨/ 2
(
¨/¨/2 3
int
¨/¨/3 6
idCargue
¨/¨/7 ?
,
¨/¨/? @
string
¨/¨/A G
usuarioDigita
¨/¨/H U
,
¨/¨/U V
ref
¨/¨/W Z 
MessageResponseOBJ
¨/¨/[ m
MsgRes
¨/¨/n t
)
¨/¨/t u
{
©/©/ 	
return
ª/ª/ 
DACActualiza
ª/ª/ 
.
ª/ª/  -
ActualizarConteo_Facturas_fase1
ª/ª/  ?
(
ª/ª/? @
idCargue
ª/ª/@ H
,
ª/ª/H I
usuarioDigita
ª/ª/J W
,
ª/ª/W X
ref
ª/ª/Y \
MsgRes
ª/ª/] c
)
ª/ª/c d
;
ª/ª/d e
}
«/«/ 	
public
­/­/ 
int
­/­/ -
ActualizarConteo_Facturas_fase2
­/­/ 2
(
­/­/2 3
int
­/­/3 6
idCargue
­/­/7 ?
,
­/­/? @
string
­/­/A G
usuarioDigita
­/­/H U
,
­/­/U V
ref
­/­/W Z 
MessageResponseOBJ
­/­/[ m
MsgRes
­/­/n t
)
­/­/t u
{
®/®/ 	
return
¯/¯/ 
DACActualiza
¯/¯/ 
.
¯/¯/  -
ActualizarConteo_Facturas_fase2
¯/¯/  ?
(
¯/¯/? @
idCargue
¯/¯/@ H
,
¯/¯/H I
usuarioDigita
¯/¯/J W
,
¯/¯/W X
ref
¯/¯/Y \
MsgRes
¯/¯/] c
)
¯/¯/c d
;
¯/¯/d e
}
°/°/ 	
public
²/²/ 
int
²/²/ /
!ActualizarConteo_Facturas_fase2_2
²/²/ 4
(
²/²/4 5
int
²/²/5 8
idCargue
²/²/9 A
,
²/²/A B
string
²/²/C I
usuarioDigita
²/²/J W
,
²/²/W X
ref
²/²/Y \ 
MessageResponseOBJ
²/²/] o
MsgRes
²/²/p v
)
²/²/v w
{
³/³/ 	
return
´/´/ 
DACActualiza
´/´/ 
.
´/´/  /
!ActualizarConteo_Facturas_fase2_2
´/´/  A
(
´/´/A B
idCargue
´/´/B J
,
´/´/J K
usuarioDigita
´/´/L Y
,
´/´/Y Z
ref
´/´/[ ^
MsgRes
´/´/_ e
)
´/´/e f
;
´/´/f g
}
µ/µ/ 	
public
¼/¼/ 
int
¼/¼/ -
ActualizarConteo_Facturas_fase3
¼/¼/ 2
(
¼/¼/2 3
int
¼/¼/3 6
idCargue
¼/¼/7 ?
,
¼/¼/? @
string
¼/¼/A G
usuarioDigita
¼/¼/H U
,
¼/¼/U V
ref
¼/¼/W Z 
MessageResponseOBJ
¼/¼/[ m
MsgRes
¼/¼/n t
)
¼/¼/t u
{
½/½/ 	
return
¾/¾/ 
DACActualiza
¾/¾/ 
.
¾/¾/  -
ActualizarConteo_Facturas_fase3
¾/¾/  ?
(
¾/¾/? @
idCargue
¾/¾/@ H
,
¾/¾/H I
usuarioDigita
¾/¾/J W
,
¾/¾/W X
ref
¾/¾/Y \
MsgRes
¾/¾/] c
)
¾/¾/c d
;
¾/¾/d e
}
¿/¿/ 	
public
Â/Â/ 
int
Â/Â/ .
 ActualizarConteo_FacturasInicial
Â/Â/ 3
(
Â/Â/3 4
int
Â/Â/4 7
idCargue
Â/Â/8 @
,
Â/Â/@ A
ref
Â/Â/B E 
MessageResponseOBJ
Â/Â/F X
MsgRes
Â/Â/Y _
)
Â/Â/_ `
{
Ã/Ã/ 	
return
Ä/Ä/ 
DACActualiza
Ä/Ä/ 
.
Ä/Ä/  .
 ActualizarConteo_FacturasInicial
Ä/Ä/  @
(
Ä/Ä/@ A
idCargue
Ä/Ä/A I
,
Ä/Ä/I J
ref
Ä/Ä/K N
MsgRes
Ä/Ä/O U
)
Ä/Ä/U V
;
Ä/Ä/V W
}
Å/Å/ 	
public
Ç/Ç/ 
int
Ç/Ç/ *
ActualizarConteo_FacturasUno
Ç/Ç/ /
(
Ç/Ç// 0
int
Ç/Ç/0 3
idCargue
Ç/Ç/4 <
,
Ç/Ç/< =
string
Ç/Ç/> D
usuarioDigita
Ç/Ç/E R
,
Ç/Ç/R S
ref
Ç/Ç/T W 
MessageResponseOBJ
Ç/Ç/X j
MsgRes
Ç/Ç/k q
)
Ç/Ç/q r
{
È/È/ 	
return
É/É/ 
DACActualiza
É/É/ 
.
É/É/  *
ActualizarConteo_FacturasUno
É/É/  <
(
É/É/< =
idCargue
É/É/= E
,
É/É/E F
usuarioDigita
É/É/G T
,
É/É/T U
ref
É/É/V Y
MsgRes
É/É/Z `
)
É/É/` a
;
É/É/a b
}
Ê/Ê/ 	
public
Ì/Ì/ 
int
Ì/Ì/ '
ActualizarConteo_Facturas
Ì/Ì/ ,
(
Ì/Ì/, -
int
Ì/Ì/- 0
idCargue
Ì/Ì/1 9
,
Ì/Ì/9 :
string
Ì/Ì/; A
usuarioDigita
Ì/Ì/B O
,
Ì/Ì/O P
int
Ì/Ì/Q T
?
Ì/Ì/T U
tipo
Ì/Ì/V Z
,
Ì/Ì/Z [
ref
Ì/Ì/\ _ 
MessageResponseOBJ
Ì/Ì/` r
MsgRes
Ì/Ì/s y
)
Ì/Ì/y z
{
Í/Í/ 	
return
Î/Î/ 
DACActualiza
Î/Î/ 
.
Î/Î/  '
ActualizarConteo_Facturas
Î/Î/  9
(
Î/Î/9 :
idCargue
Î/Î/: B
,
Î/Î/B C
usuarioDigita
Î/Î/D Q
,
Î/Î/Q R
tipo
Î/Î/S W
,
Î/Î/W X
ref
Î/Î/Y \
MsgRes
Î/Î/] c
)
Î/Î/c d
;
Î/Î/d e
}
Ï/Ï/ 	
public
Ñ/Ñ/ 
List
Ñ/Ñ/ 
<
Ñ/Ñ/ 8
*management_prefacturas_reporteCierreResult
Ñ/Ñ/ >
>
Ñ/Ñ/> ?(
ReportePrefacturasCerradas
Ñ/Ñ/@ Z
(
Ñ/Ñ/Z [
int
Ñ/Ñ/[ ^
?
Ñ/Ñ/^ _
idCargue
Ñ/Ñ/` h
)
Ñ/Ñ/h i
{
Ò/Ò/ 	
return
Ó/Ó/ 
DACConsulta
Ó/Ó/ 
.
Ó/Ó/ (
ReportePrefacturasCerradas
Ó/Ó/ 9
(
Ó/Ó/9 :
idCargue
Ó/Ó/: B
)
Ó/Ó/B C
;
Ó/Ó/C D
}
Ô/Ô/ 	
public
Ö/Ö/ 
int
Ö/Ö/ (
ActualizarConteo_Facturas2
Ö/Ö/ -
(
Ö/Ö/- .
int
Ö/Ö/. 1
idCargue
Ö/Ö/2 :
,
Ö/Ö/: ;
string
Ö/Ö/< B
usuario
Ö/Ö/C J
,
Ö/Ö/J K
ref
Ö/Ö/L O 
MessageResponseOBJ
Ö/Ö/P b
MsgRes
Ö/Ö/c i
)
Ö/Ö/i j
{
×/×/ 	
return
Ø/Ø/ 
DACActualiza
Ø/Ø/ 
.
Ø/Ø/  (
ActualizarConteo_Facturas2
Ø/Ø/  :
(
Ø/Ø/: ;
idCargue
Ø/Ø/; C
,
Ø/Ø/C D
usuario
Ø/Ø/E L
,
Ø/Ø/L M
ref
Ø/Ø/N Q
MsgRes
Ø/Ø/R X
)
Ø/Ø/X Y
;
Ø/Ø/Y Z
}
Ù/Ù/ 	
public
Û/Û/ 
int
Û/Û/ (
ActualizarConteo_Facturas3
Û/Û/ -
(
Û/Û/- .
int
Û/Û/. 1
idCargue
Û/Û/2 :
,
Û/Û/: ;
string
Û/Û/< B
usuarioValida
Û/Û/C P
,
Û/Û/P Q
ref
Û/Û/R U 
MessageResponseOBJ
Û/Û/V h
MsgRes
Û/Û/i o
)
Û/Û/o p
{
Ü/Ü/ 	
return
İ/İ/ 
DACActualiza
İ/İ/ 
.
İ/İ/  (
ActualizarConteo_Facturas3
İ/İ/  :
(
İ/İ/: ;
idCargue
İ/İ/; C
,
İ/İ/C D
usuarioValida
İ/İ/E R
,
İ/İ/R S
ref
İ/İ/T W
MsgRes
İ/İ/X ^
)
İ/İ/^ _
;
İ/İ/_ `
}
Ş/Ş/ 	
public
à/à/ 
int
à/à/ (
ActualizarConteo_Facturas4
à/à/ -
(
à/à/- .
int
à/à/. 1
idCargue
à/à/2 :
,
à/à/: ;
ref
à/à/< ? 
MessageResponseOBJ
à/à/@ R
MsgRes
à/à/S Y
)
à/à/Y Z
{
á/á/ 	
return
â/â/ 
DACActualiza
â/â/ 
.
â/â/  (
ActualizarConteo_Facturas4
â/â/  :
(
â/â/: ;
idCargue
â/â/; C
,
â/â/C D
ref
â/â/E H
MsgRes
â/â/I O
)
â/â/O P
;
â/â/P Q
}
ã/ã/ 	
public
å/å/ 
int
å/å/ (
ActualizarConteo_Facturas5
å/å/ -
(
å/å/- .
int
å/å/. 1
idCargue
å/å/2 :
,
å/å/: ;
ref
å/å/< ? 
MessageResponseOBJ
å/å/@ R
MsgRes
å/å/S Y
)
å/å/Y Z
{
æ/æ/ 	
return
ç/ç/ 
DACActualiza
ç/ç/ 
.
ç/ç/  (
ActualizarConteo_Facturas5
ç/ç/  :
(
ç/ç/: ;
idCargue
ç/ç/; C
,
ç/ç/C D
ref
ç/ç/E H
MsgRes
ç/ç/I O
)
ç/ç/O P
;
ç/ç/P Q
}
è/è/ 	
public
ê/ê/ G
9management_prefacturas_buscarEnHistoricoPrefacturasResult
ê/ê/ H(
BuscarHistoricoPrefacturas
ê/ê/I c
(
ê/ê/c d
string
ê/ê/d j)
num_documento_beneficiarioê/ê/k …
,ê/ê/… †
stringê/ê/‡ 
cumê/ê/ ‘
,ê/ê/‘ ’
string
ë/ë/ +
nombre_comercial_medicacmento
ë/ë/ ,
,
ë/ë/, -
string
ë/ë/. 4%
num_unidades_entregadas
ë/ë/5 L
,
ë/ë/L M
DateTime
ë/ë/N V$
fecha_despacho_formula
ë/ë/W m
,
ë/ë/m n
string
ë/ë/o u)
vlr_unitario_und_entregadaë/ë/v 
)ë/ë/ ‘
{
ì/ì/ 	
return
í/í/ 
DACConsulta
í/í/ 
.
í/í/ (
BuscarHistoricoPrefacturas
í/í/ 9
(
í/í/9 :(
num_documento_beneficiario
í/í/: T
,
í/í/T U
cum
í/í/V Y
,
í/í/Y Z+
nombre_comercial_medicacmento
í/í/[ x
,
í/í/x y&
num_unidades_entregadasí/í/z ‘
,í/í/‘ ’&
fecha_despacho_formulaí/í/“ ©
,í/í/© ª*
vlr_unitario_und_entregadaí/í/« Å
)í/í/Å Æ
;í/í/Æ Ç
}
î/î/ 	
public
ğ/ğ/ $
md_prefactura_contador
ğ/ğ/ %+
TraerDatosContadorPrefacturas
ğ/ğ/& C
(
ğ/ğ/C D
int
ğ/ğ/D G!
idDetallePrefactura
ğ/ğ/H [
)
ğ/ğ/[ \
{
ñ/ñ/ 	
return
ò/ò/ 
DACConsulta
ò/ò/ 
.
ò/ò/ +
TraerDatosContadorPrefacturas
ò/ò/ <
(
ò/ò/< =!
idDetallePrefactura
ò/ò/= P
)
ò/ò/P Q
;
ò/ò/Q R
}
ó/ó/ 	
public
ô/ô/ 
List
ô/ô/ 
<
ô/ô/ 5
'management_Validador_datosCorreosResult
ô/ô/ ;
>
ô/ô/; <(
ListadoCorreosValidadorOPL
ô/ô/= W
(
ô/ô/W X
int
ô/ô/X [
?
ô/ô/[ \

idRegional
ô/ô/] g
)
ô/ô/g h
{
õ/õ/ 	
return
ö/ö/ 
DACConsulta
ö/ö/ 
.
ö/ö/ (
ListadoCorreosValidadorOPL
ö/ö/ 9
(
ö/ö/9 :

idRegional
ö/ö/: D
)
ö/ö/D E
;
ö/ö/E F
}
÷/÷/ 	
public
ø/ø/ 
List
ø/ø/ 
<
ø/ø/ 3
%management_prestadores_regionalResult
ø/ø/ 9
>
ø/ø/9 :$
GetPrestadoresRegional
ø/ø/; Q
(
ø/ø/Q R
int
ø/ø/R U

idRegional
ø/ø/V `
)
ø/ø/` a
{
ù/ù/ 	
return
ú/ú/ 
DACConsulta
ú/ú/ 
.
ú/ú/ $
GetPrestadoresRegional
ú/ú/ 5
(
ú/ú/5 6

idRegional
ú/ú/6 @
)
ú/ú/@ A
;
ú/ú/A B
}
û/û/ 	
public
ş/ş/ 
List
ş/ş/ 
<
ş/ş/ ,
vw_factura_digital_gasto_total
ş/ş/ 2
>
ş/ş/2 3
GetGatosFactura
ş/ş/4 C
(
ş/ş/C D
int
ş/ş/D G
id
ş/ş/H J
)
ş/ş/J K
{
ÿ/ÿ/ 	
return
€0€0 
DACConsulta
€0€0 
.
€0€0 
GetGatosFactura
€0€0 .
(
€0€0. /
id
€0€0/ 1
)
€0€01 2
;
€0€02 3
}
00 	
public
ƒ0ƒ0 
List
ƒ0ƒ0 
<
ƒ0ƒ0 9
+managementprestadores_alertas_activasResult
ƒ0ƒ0 ?
>
ƒ0ƒ0? @'
GetConsultaAlertasactivas
ƒ0ƒ0A Z
(
ƒ0ƒ0Z [
)
ƒ0ƒ0[ \
{
„0„0 	
return
…0…0 
DACConsulta
…0…0 
.
…0…0 '
GetConsultaAlertasactivas
…0…0 8
(
…0…08 9
)
…0…09 :
;
…0…0: ;
}
†0†0 	
public
ˆ0ˆ0 
List
ˆ0ˆ0 
<
ˆ0ˆ0 ?
1managmentprestadoresfacturasgestionadasdtllResult
ˆ0ˆ0 E
>
ˆ0ˆ0E F)
GetListFacturasByNumFactura
ˆ0ˆ0G b
(
ˆ0ˆ0b c
string
ˆ0ˆ0c i

numfactura
ˆ0ˆ0j t
)
ˆ0ˆ0t u
{
‰0‰0 	
return
Š0Š0 
DACConsulta
Š0Š0 
.
Š0Š0 )
GetListFacturasByNumFactura
Š0Š0 :
(
Š0Š0: ;

numfactura
Š0Š0; E
)
Š0Š0E F
;
Š0Š0F G
}
‹0‹0 	
public
Œ0Œ0 
List
Œ0Œ0 
<
Œ0Œ0 G
9managmentprestadoresfacturasgestionadasdtllCompletaResult
Œ0Œ0 M
>
Œ0Œ0M N1
#GetListFacturasByNumFacturaCompleta
Œ0Œ0O r
(
Œ0Œ0r s
String
Œ0Œ0s y
numFacŒ0Œ0z €
,Œ0Œ0€ 
StringŒ0Œ0‚ ˆ
nitŒ0Œ0‰ Œ
,Œ0Œ0Œ 
StringŒ0Œ0 ”
	prestadorŒ0Œ0• 
,Œ0Œ0 Ÿ
StringŒ0Œ0  ¦
sapŒ0Œ0§ ª
)Œ0Œ0ª «
{
00 	
return
00 
DACConsulta
00 
.
00 1
#GetListFacturasByNumFacturaCompleta
00 B
(
00B C
numFac
00C I
,
00I J
nit
00K N
,
00N O
	prestador
00P Y
,
00Y Z
sap
00[ ^
)
00^ _
;
00_ `
}
00 	
public
’0’0 9
+ManagementPrestadoresFacturasByIdDtllResult
’0’0 : 
GetInfoFacturaById
’0’0; M
(
’0’0M N
int
’0’0N Q
idcarguedtll
’0’0R ^
)
’0’0^ _
{
“0“0 	
return
”0”0 
DACConsulta
”0”0 
.
”0”0  
GetInfoFacturaById
”0”0 1
(
”0”01 2
idcarguedtll
”0”02 >
)
”0”0> ?
;
”0”0? @
}
•0•0 	
public
—0—0 
List
—0—0 
<
—0—0 :
,managmentprestadoresFacturas_analistasResult
—0—0 @
>
—0—0@ A+
prestadoresFacturas_analistas
—0—0B _
(
—0—0_ `
)
—0—0` a
{
˜0˜0 	
return
™0™0 
DACConsulta
™0™0 
.
™0™0 +
prestadoresFacturas_analistas
™0™0 <
(
™0™0< =
)
™0™0= >
;
™0™0> ?
}
š0š0 	
public
œ0œ0 
List
œ0œ0 
<
œ0œ0 :
,managmentprestadoresFacturas_auditoresResult
œ0œ0 @
>
œ0œ0@ A+
prestadoresFacturas_auditores
œ0œ0B _
(
œ0œ0_ `
)
œ0œ0` a
{
00 	
return
00 
DACConsulta
00 
.
00 +
prestadoresFacturas_auditores
00 <
(
00< =
)
00= >
;
00> ?
}
Ÿ0Ÿ0 	
public
¢0¢0 
Int32
¢0¢0 %
InsertarGestionAnalista
¢0¢0 ,
(
¢0¢0, -*
ref_cuentas_medicas_analista
¢0¢0- I
OBJ
¢0¢0J M
,
¢0¢0M N
ref
¢0¢0O R 
MessageResponseOBJ
¢0¢0S e
MsgRes
¢0¢0f l
)
¢0¢0l m
{
£0£0 	
return
¤0¤0 

DACInserta
¤0¤0 
.
¤0¤0 %
InsertarGestionAnalista
¤0¤0 5
(
¤0¤05 6
OBJ
¤0¤06 9
,
¤0¤09 :
ref
¤0¤0; >
MsgRes
¤0¤0? E
)
¤0¤0E F
;
¤0¤0F G
}
¥0¥0 	
public
§0§0 
List
§0§0 
<
§0§0 +
vw_recep_facturas_cargue_base
§0§0 1
>
§0§01 2(
GetHistoricoRadicacionById
§0§03 M
(
§0§0M N
int
§0§0N Q
idcargue
§0§0R Z
)
§0§0Z [
{
¨0¨0 	
return
©0©0 
DACConsulta
©0©0 
.
©0©0 (
GetHistoricoRadicacionById
©0©0 9
(
©0©09 :
idcargue
©0©0: B
)
©0©0B C
;
©0©0C D
}
ª0ª0 	
public
¬0¬0 
List
¬0¬0 
<
¬0¬0 '
ManagmentFacturasV2Result
¬0¬0 -
>
¬0¬0- .&
GetFacturasByRecepcionV2
¬0¬0/ G
(
¬0¬0G H
int
¬0¬0H K
idrecepcion
¬0¬0L W
)
¬0¬0W X
{
­0­0 	
return
®0®0 
DACConsulta
®0®0 
.
®0®0 &
GetFacturasByRecepcionV2
®0®0 7
(
®0®07 8
idrecepcion
®0®08 C
)
®0®0C D
;
®0®0D E
}
¯0¯0 	
public
±0±0 
Int32
±0±0 $
InsertarGestionAuditor
±0±0 +
(
±0±0+ ,+
ref_cuentas_medicas_auditores
±0±0, I
OBJ
±0±0J M
,
±0±0M N
ref
±0±0O R 
MessageResponseOBJ
±0±0S e
MsgRes
±0±0f l
)
±0±0l m
{
²0²0 	
return
³0³0 

DACInserta
³0³0 
.
³0³0 $
InsertarGestionAuditor
³0³0 4
(
³0³04 5
OBJ
³0³05 8
,
³0³08 9
ref
³0³0: =
MsgRes
³0³0> D
)
³0³0D E
;
³0³0E F
}
µ0µ0 	
public
·0·0 
void
·0·0 '
ActualizaAnalistaAsignado
·0·0 -
(
·0·0- .*
ref_cuentas_medicas_analista
·0·0. J
ObjAudi
·0·0K R
,
·0·0R S
ref
·0·0T W 
MessageResponseOBJ
·0·0X j
MsgRes
·0·0k q
)
·0·0q r
{
¸0¸0 	
DACActualiza
¹0¹0 
.
¹0¹0 '
ActualizaAnalistaAsignado
¹0¹0 2
(
¹0¹02 3
ObjAudi
¹0¹03 :
,
¹0¹0: ;
ref
¹0¹0< ?
MsgRes
¹0¹0@ F
)
¹0¹0F G
;
¹0¹0G H
}
º0º0 	
public
¼0¼0 
void
¼0¼0 &
ActualizaAuditorAsignado
¼0¼0 ,
(
¼0¼0, -+
ref_cuentas_medicas_auditores
¼0¼0- J
ObjAudi
¼0¼0K R
,
¼0¼0R S
ref
¼0¼0T W 
MessageResponseOBJ
¼0¼0X j
MsgRes
¼0¼0k q
)
¼0¼0q r
{
½0½0 	
DACActualiza
¾0¾0 
.
¾0¾0 &
ActualizaAuditorAsignado
¾0¾0 1
(
¾0¾01 2
ObjAudi
¾0¾02 9
,
¾0¾09 :
ref
¾0¾0; >
MsgRes
¾0¾0? E
)
¾0¾0E F
;
¾0¾0F G
}
¿0¿0 	
public
Ç0Ç0 
void
Ç0Ç0 )
InsertarLogBusquedaTableros
Ç0Ç0 /
(
Ç0Ç0/ 0-
log_busquedas_tableros_facturas
Ç0Ç00 O
obj
Ç0Ç0P S
,
Ç0Ç0S T
ref
Ç0Ç0U X 
MessageResponseOBJ
Ç0Ç0Y k
MsgRes
Ç0Ç0l r
)
Ç0Ç0r s
{
È0È0 	

DACInserta
É0É0 
.
É0É0 )
InsertarLogBusquedaTableros
É0É0 2
(
É0É02 3
obj
É0É03 6
,
É0É06 7
ref
É0É08 ;
MsgRes
É0É0< B
)
É0É0B C
;
É0É0C D
}
Ê0Ê0 	
public
Ñ0Ñ0 
List
Ñ0Ñ0 
<
Ñ0Ñ0 !
ref_gestion_interna
Ñ0Ñ0 '
>
Ñ0Ñ0' (#
GetGestionInternaList
Ñ0Ñ0) >
(
Ñ0Ñ0> ?
)
Ñ0Ñ0? @
{
Ò0Ò0 	
return
Ó0Ó0 
DACConsulta
Ó0Ó0 
.
Ó0Ó0 #
GetGestionInternaList
Ó0Ó0 4
(
Ó0Ó04 5
)
Ó0Ó05 6
;
Ó0Ó06 7
}
Ô0Ô0 	
public
Ö0Ö0 !
ref_gestion_interna
Ö0Ö0 "#
GetGestionInternaById
Ö0Ö0# 8
(
Ö0Ö08 9
int
Ö0Ö09 <
	idgestion
Ö0Ö0= F
)
Ö0Ö0F G
{
×0×0 	
return
Ø0Ø0 
DACConsulta
Ø0Ø0 
.
Ø0Ø0 #
GetGestionInternaById
Ø0Ø0 4
(
Ø0Ø04 5
	idgestion
Ø0Ø05 >
)
Ø0Ø0> ?
;
Ø0Ø0? @
}
Ù0Ù0 	
public
Û0Û0 
List
Û0Û0 
<
Û0Û0 '
vw_odont_historia_clinica
Û0Û0 -
>
Û0Û0- .!
ListHistoriaClinica
Û0Û0/ B
(
Û0Û0B C
int
Û0Û0C F
id_historia
Û0Û0G R
)
Û0Û0R S
{
Ü0Ü0 	
return
İ0İ0 
DACConsulta
İ0İ0 
.
İ0İ0 !
ListHistoriaClinica
İ0İ0 2
(
İ0İ02 3
id_historia
İ0İ03 >
)
İ0İ0> ?
;
İ0İ0? @
}
Ş0Ş0 	
public
à0à0 
List
à0à0 
<
à0à0 '
vw_odont_historia_clinica
à0à0 -
>
à0à0- ./
!GetListHistoriaClinicaXOdontologo
à0à0/ P
(
à0à0P Q
string
à0à0Q W
nomodontologo
à0à0X e
)
à0à0e f
{
á0á0 	
return
â0â0 
DACConsulta
â0â0 
.
â0â0 /
!GetListHistoriaClinicaXOdontologo
â0â0 @
(
â0â0@ A
nomodontologo
â0â0A N
)
â0â0N O
;
â0â0O P
}
ã0ã0 	
public
å0å0 
void
å0å0 %
EliminarHistoriaclinica
å0å0 +
(
å0å0+ ,
int
å0å0, /
id_hc_paciente
å0å00 >
,
å0å0> ?<
.log_eliminacion_historias_clinicas_odontologia
å0å0@ n
obj
å0å0o r
,
å0å0r s
ref
å0å0t w!
MessageResponseOBJå0å0x Š
MsgReså0å0‹ ‘
)å0å0‘ ’
{
æ0æ0 	

DACElimina
ç0ç0 
.
ç0ç0 %
EliminarHistoriaclinica
ç0ç0 .
(
ç0ç0. /
id_hc_paciente
ç0ç0/ =
,
ç0ç0= >
obj
ç0ç0? B
,
ç0ç0B C
ref
ç0ç0D G
MsgRes
ç0ç0H N
)
ç0ç0N O
;
ç0ç0O P
}
è0è0 	
public
ê0ê0 
void
ê0ê0 1
#InsertarLogActualizacionFechaEgreso
ê0ê0 7
(
ê0ê07 8%
log_update_fecha_egreso
ê0ê08 O
log
ê0ê0P S
,
ê0ê0S T
ref
ê0ê0U X 
MessageResponseOBJ
ê0ê0Y k
MsgRes
ê0ê0l r
)
ê0ê0r s
{
ë0ë0 	

DACInserta
ì0ì0 
.
ì0ì0 1
#InsertarLogActualizacionFechaEgreso
ì0ì0 :
(
ì0ì0: ;
log
ì0ì0; >
,
ì0ì0> ?
ref
ì0ì0@ C
MsgRes
ì0ì0D J
)
ì0ì0J K
;
ì0ì0K L
}
í0í0 	
public
ó0ó0 
int
ó0ó0 '
InsertarGastosPorServicio
ó0ó0 ,
(
ó0ó0, -,
gasto_por_servicio_cargue_base
ó0ó0- K
obj
ó0ó0L O
,
ó0ó0O P
ref
ó0ó0Q T 
MessageResponseOBJ
ó0ó0U g
MsgRes
ó0ó0h n
)
ó0ó0n o
{
ô0ô0 	
return
õ0õ0 

DACInserta
õ0õ0 
.
õ0õ0 '
InsertarGastosPorServicio
õ0õ0 7
(
õ0õ07 8
obj
õ0õ08 ;
,
õ0õ0; <
ref
õ0õ0= @
MsgRes
õ0õ0A G
)
õ0õ0G H
;
õ0õ0H I
}
ö0ö0 	
public
ø0ø0 
void
ø0ø0 +
InsertarGastosPorServicioDtll
ø0ø0 1
(
ø0ø01 2
List
ø0ø02 6
<
ø0ø06 7,
gasto_por_Servicio_cargue_dtll
ø0ø07 U
>
ø0ø0U V
dtll
ø0ø0W [
,
ø0ø0[ \
ref
ø0ø0] ` 
MessageResponseOBJ
ø0ø0a s
MsgRes
ø0ø0t z
)
ø0ø0z {
{
ù0ù0 	

DACInserta
ú0ú0 
.
ú0ú0 +
InsertarGastosPorServicioDtll
ú0ú0 4
(
ú0ú04 5
dtll
ú0ú05 9
,
ú0ú09 :
ref
ú0ú0; >
MsgRes
ú0ú0? E
)
ú0ú0E F
;
ú0ú0F G
}
û0û0 	
public
ı0ı0 ,
gasto_por_servicio_cargue_base
ı0ı0 -
getcarguebase
ı0ı0. ;
(
ı0ı0; <
int
ı0ı0< ?
mes
ı0ı0@ C
,
ı0ı0C D
int
ı0ı0E H
aÃ±o
ı0ı0I L
,
ı0ı0L M
string
ı0ı0N T
regional
ı0ı0U ]
)
ı0ı0] ^
{
ş0ş0 	
return
ÿ0ÿ0 
DACConsulta
ÿ0ÿ0 
.
ÿ0ÿ0 
getcarguebase
ÿ0ÿ0 ,
(
ÿ0ÿ0, -
mes
ÿ0ÿ0- 0
,
ÿ0ÿ00 1
aÃ±o
ÿ0ÿ02 5
,
ÿ0ÿ05 6
regional
ÿ0ÿ07 ?
)
ÿ0ÿ0? @
;
ÿ0ÿ0@ A
}
€1€1 	
public
ˆ1ˆ1 
List
ˆ1ˆ1 
<
ˆ1ˆ1 ,
vw_consulta_gasto_por_servicio
ˆ1ˆ1 2
>
ˆ1ˆ12 33
%ObtenerListadoCarguesGastoPorServicio
ˆ1ˆ14 Y
(
ˆ1ˆ1Y Z
)
ˆ1ˆ1Z [
{
‰1‰1 	
return
Š1Š1 
DACConsulta
Š1Š1 
.
Š1Š1 3
%ObtenerListadoCarguesGastoPorServicio
Š1Š1 D
(
Š1Š1D E
)
Š1Š1E F
;
Š1Š1F G
}
‹1‹1 	
public
•1•1 
List
•1•1 
<
•1•1 ;
-Management_gasto_x_servicio_consolidadoResult
•1•1 A
>
•1•1A B?
1ObtenerConsolidadoGastoPorServicioPorIdCargueBase
•1•1C t
(
•1•1t u
int
•1•1u x
idCargueBase•1•1y …
)•1•1… †
{
–1–1 	
return
—1—1 
DACConsulta
—1—1 
.
—1—1 ?
1ObtenerConsolidadoGastoPorServicioPorIdCargueBase
—1—1 P
(
—1—1P Q
idCargueBase
—1—1Q ]
)
—1—1] ^
;
—1—1^ _
}
˜1˜1 	
public
11 
List
11 
<
11 6
(management_gastoxservicio_consultaResult
11 <
>
11< =-
ObtenerGastoPorsercicioConsulta
11> ]
(
11] ^
DateTime
11^ f
?
11f g
fechaInicio
11h s
,
11s t
DateTime
11u }
?
11} ~
fechaFin11 ‡
,11‡ ˆ
string11‰ 
factura11 —
,11— ˜
int11™ œ
cedula11 £
,11£ ¤
string11¥ «
servicio11¬ ´
,11´ µ
string11¶ ¼
tiga11½ Á
,11Á Â
DateTime11Ã Ë
?11Ë Ì
fechaInicioPre11Í Û
,11Û Ü
DateTime11İ å
?11å æ
fechaFinPre11ç ò
)11ò ó
{
Ÿ1Ÿ1 	
return
 1 1 
DACConsulta
 1 1 
.
 1 1 -
ObtenerGastoPorsercicioConsulta
 1 1 >
(
 1 1> ?
fechaInicio
 1 1? J
,
 1 1J K
fechaFin
 1 1L T
,
 1 1T U
factura
 1 1V ]
,
 1 1] ^
cedula
 1 1_ e
,
 1 1e f
servicio
 1 1g o
,
 1 1o p
tiga
 1 1q u
,
 1 1u v
fechaInicioPre 1 1w …
, 1 1… †
fechaFinPre 1 1‡ ’
) 1 1’ “
; 1 1“ ”
}
¡1¡1 	
public
¨1¨1 
int
¨1¨1 ,
EliminarCargueGastoPorServicio
¨1¨1 1
(
¨1¨11 2
int
¨1¨12 5
idCargue
¨1¨16 >
)
¨1¨1> ?
{
©1©1 	
return
ª1ª1 

DACElimina
ª1ª1 
.
ª1ª1 ,
EliminarCargueGastoPorServicio
ª1ª1 <
(
ª1ª1< =
idCargue
ª1ª1= E
)
ª1ª1E F
;
ª1ª1F G
}
«1«1 	
public
­1­1 
int
­1­1 /
!InsertarLogEliminarGastoxServicio
­1­1 4
(
­1­14 54
&log_gastoxServicio_eliminarConsolidado
­1­15 [
obj
­1­1\ _
)
­1­1_ `
{
®1®1 	
return
¯1¯1 

DACInserta
¯1¯1 
.
¯1¯1 /
!InsertarLogEliminarGastoxServicio
¯1¯1 ?
(
¯1¯1? @
obj
¯1¯1@ C
)
¯1¯1C D
;
¯1¯1D E
}
°1°1 	
public
¶1¶1 
List
¶1¶1 
<
¶1¶1 -
seguimiento_entregables_periodo
¶1¶1 3
>
¶1¶13 4'
GetListEntregablesPeriodo
¶1¶15 N
(
¶1¶1N O
int
¶1¶1O R
id_seg_entregable
¶1¶1S d
)
¶1¶1d e
{
·1·1 	
return
¸1¸1 
DACConsulta
¸1¸1 
.
¸1¸1 '
GetListEntregablesPeriodo
¸1¸1 8
(
¸1¸18 9
id_seg_entregable
¸1¸19 J
)
¸1¸1J K
;
¸1¸1K L
}
¹1¹1 	
public
»1»1 -
seguimiento_entregables_periodo
»1»1 .&
GetEntregablePeriodoById
»1»1/ G
(
»1»1G H
int
»1»1H K
id_ent_periodo
»1»1L Z
)
»1»1Z [
{
¼1¼1 	
return
½1½1 
DACConsulta
½1½1 
.
½1½1 &
GetEntregablePeriodoById
½1½1 7
(
½1½17 8
id_ent_periodo
½1½18 F
)
½1½1F G
;
½1½1G H
}
¾1¾1 	
public
À1À1 
List
À1À1 
<
À1À1 *
ref_periodicidad_entregables
À1À1 0
>
À1À10 1,
GetListPeriodicidadEntregables
À1À12 P
(
À1À1P Q
)
À1À1Q R
{
Á1Á1 	
return
Â1Â1 
DACConsulta
Â1Â1 
.
Â1Â1 ,
GetListPeriodicidadEntregables
Â1Â1 =
(
Â1Â1= >
)
Â1Â1> ?
;
Â1Â1? @
}
Ã1Ã1 	
public
Å1Å1 
void
Å1Å1 6
(InsertarOActualizarSeguimientoEntregable
Å1Å1 <
(
Å1Å1< =%
seguimiento_entregables
Å1Å1= T
obj
Å1Å1U X
,
Å1Å1X Y
ref
Å1Å1Z ] 
MessageResponseOBJ
Å1Å1^ p
MsgRes
Å1Å1q w
)
Å1Å1w x
{
Æ1Æ1 	

DACInserta
Ç1Ç1 
.
Ç1Ç1 6
(InsertarOActualizarSeguimientoEntregable
Ç1Ç1 ?
(
Ç1Ç1? @
obj
Ç1Ç1@ C
,
Ç1Ç1C D
ref
Ç1Ç1E H
MsgRes
Ç1Ç1I O
)
Ç1Ç1O P
;
Ç1Ç1P Q
}
È1È1 	
public
Ê1Ê1 
void
Ê1Ê1 /
!InsertarSeguimientoEntregableDTLL
Ê1Ê1 5
(
Ê1Ê15 6
int
Ê1Ê16 9
id_seg_entregable
Ê1Ê1: K
,
Ê1Ê1K L&
seguimiento_dtll_entrega
Ê1Ê1M e
obj
Ê1Ê1f i
,
Ê1Ê1i j
List
Ê1Ê1k o
<
Ê1Ê1o p1
"seguimiento_entregables_documentosÊ1Ê1p ’
>Ê1Ê1’ “
ObjdocumentosÊ1Ê1” ¡
,Ê1Ê1¡ ¢
refÊ1Ê1£ ¦"
MessageResponseOBJÊ1Ê1§ ¹
MsgResÊ1Ê1º À
)Ê1Ê1À Á
{
Ë1Ë1 	

DACInserta
Ì1Ì1 
.
Ì1Ì1 /
!InsertarSeguimientoEntregableDTLL
Ì1Ì1 8
(
Ì1Ì18 9
id_seg_entregable
Ì1Ì19 J
,
Ì1Ì1J K
obj
Ì1Ì1L O
,
Ì1Ì1O P
Objdocumentos
Ì1Ì1Q ^
,
Ì1Ì1^ _
ref
Ì1Ì1` c
MsgRes
Ì1Ì1d j
)
Ì1Ì1j k
;
Ì1Ì1k l
}
Í1Í1 	
public
Ï1Ï1 
Int32
Ï1Ï1 0
"InsertarSeguimientoEntregableDTLL1
Ï1Ï1 7
(
Ï1Ï17 8
int
Ï1Ï18 ;
id_seg_entregable
Ï1Ï1< M
,
Ï1Ï1M N&
seguimiento_dtll_entrega
Ï1Ï1O g
obj
Ï1Ï1h k
,
Ï1Ï1k l
ref
Ï1Ï1m p!
MessageResponseOBJÏ1Ï1q ƒ
MsgResÏ1Ï1„ Š
)Ï1Ï1Š ‹
{
Ğ1Ğ1 	
return
Ñ1Ñ1 

DACInserta
Ñ1Ñ1 
.
Ñ1Ñ1 0
"InsertarSeguimientoEntregableDTLL1
Ñ1Ñ1 @
(
Ñ1Ñ1@ A
id_seg_entregable
Ñ1Ñ1A R
,
Ñ1Ñ1R S
obj
Ñ1Ñ1T W
,
Ñ1Ñ1W X
ref
Ñ1Ñ1Y \
MsgRes
Ñ1Ñ1] c
)
Ñ1Ñ1c d
;
Ñ1Ñ1d e
}
Ò1Ò1 	
public
Ô1Ô1 
void
Ô1Ô1 0
"InsertarSeguimientoEntregableDTLL2
Ô1Ô1 6
(
Ô1Ô16 7
List
Ô1Ô17 ;
<
Ô1Ô1; <0
"seguimiento_entregables_documentos
Ô1Ô1< ^
>
Ô1Ô1^ _
lista
Ô1Ô1` e
,
Ô1Ô1e f
ref
Ô1Ô1g j 
MessageResponseOBJ
Ô1Ô1k }
MsgResÔ1Ô1~ „
)Ô1Ô1„ …
{
Õ1Õ1 	

DACInserta
Ö1Ö1 
.
Ö1Ö1 0
"InsertarSeguimientoEntregableDTLL2
Ö1Ö1 9
(
Ö1Ö19 :
lista
Ö1Ö1: ?
,
Ö1Ö1? @
ref
Ö1Ö1A D
MsgRes
Ö1Ö1E K
)
Ö1Ö1K L
;
Ö1Ö1L M
}
×1×1 	
public
Ù1Ù1 
List
Ù1Ù1 
<
Ù1Ù1 (
vw_seguimiento_entregables
Ù1Ù1 .
>
Ù1Ù1. / 
GetListEntregables
Ù1Ù10 B
(
Ù1Ù1B C
int
Ù1Ù1C F
?
Ù1Ù1F G
periodicidad
Ù1Ù1H T
)
Ù1Ù1T U
{
Ú1Ú1 	
return
Û1Û1 
DACConsulta
Û1Û1 
.
Û1Û1  
GetListEntregables
Û1Û1 1
(
Û1Û11 2
periodicidad
Û1Û12 >
)
Û1Û1> ?
;
Û1Û1? @
}
Ü1Ü1 	
public
Ş1Ş1 &
seguimiento_dtll_entrega
Ş1Ş1 ''
GetseguimientoDtllEntrega
Ş1Ş1( A
(
Ş1Ş1A B
int
Ş1Ş1B E
id_dtll
Ş1Ş1F M
)
Ş1Ş1M N
{
ß1ß1 	
return
à1à1 
DACConsulta
à1à1 
.
à1à1 '
GetseguimientoDtllEntrega
à1à1 8
(
à1à18 9
id_dtll
à1à19 @
)
à1à1@ A
;
à1à1A B
}
á1á1 	
public
ã1ã1 &
seguimiento_dtll_entrega
ã1ã1 '1
#GetseguimientoDtllEntregaPresentado
ã1ã1( K
(
ã1ã1K L
int
ã1ã1L O
?
ã1ã1O P
id_dtll
ã1ã1Q X
)
ã1ã1X Y
{
ä1ä1 	
return
å1å1 
DACConsulta
å1å1 
.
å1å1 1
#GetseguimientoDtllEntregaPresentado
å1å1 B
(
å1å1B C
id_dtll
å1å1C J
)
å1å1J K
;
å1å1K L
}
æ1æ1 	
public
è1è1 
List
è1è1 
<
è1è1 &
seguimiento_dtll_entrega
è1è1 ,
>
è1è1, -+
GetListseguimientoDtllEntrega
è1è1. K
(
è1è1K L
int
è1è1L O
id_seg_periodo
è1è1P ^
)
è1è1^ _
{
é1é1 	
return
ê1ê1 
DACConsulta
ê1ê1 
.
ê1ê1 +
GetListseguimientoDtllEntrega
ê1ê1 <
(
ê1ê1< =
id_seg_periodo
ê1ê1= K
)
ê1ê1K L
;
ê1ê1L M
}
ë1ë1 	
public
í1í1 
List
í1í1 
<
í1í1 0
"seguimiento_entregables_documentos
í1í1 6
>
í1í16 7*
GetSeguimientoEntregableDocs
í1í18 T
(
í1í1T U
int
í1í1U X
id
í1í1Y [
)
í1í1[ \
{
î1î1 	
return
ï1ï1 
DACConsulta
ï1ï1 
.
ï1ï1 *
GetSeguimientoEntregableDocs
ï1ï1 ;
(
ï1ï1; <
id
ï1ï1< >
)
ï1ï1> ?
;
ï1ï1? @
}
ğ1ğ1 	
public
ñ1ñ1 0
"seguimiento_entregables_documentos
ñ1ñ1 1(
traerDocumentoEntregableId
ñ1ñ12 L
(
ñ1ñ1L M
int
ñ1ñ1M P
idDocumento
ñ1ñ1Q \
)
ñ1ñ1\ ]
{
ò1ò1 	
return
ó1ó1 
DACConsulta
ó1ó1 
.
ó1ó1 (
traerDocumentoEntregableId
ó1ó1 9
(
ó1ó19 :
idDocumento
ó1ó1: E
)
ó1ó1E F
;
ó1ó1F G
}
ô1ô1 	
public
ö1ö1 
List
ö1ö1 
<
ö1ö1 ?
1managmentSeguimiento_entregables_documentosResult
ö1ö1 E
>
ö1ö1E F+
GetSeguimientoEntregableDocs2
ö1ö1G d
(
ö1ö1d e
ref
ö1ö1e h 
MessageResponseOBJ
ö1ö1i {
MsgResö1ö1| ‚
)ö1ö1‚ ƒ
{
÷1÷1 	
return
ø1ø1 
DACConsulta
ø1ø1 
.
ø1ø1 +
GetSeguimientoEntregableDocs2
ø1ø1 <
(
ø1ø1< =
ref
ø1ø1= @
MsgRes
ø1ø1A G
)
ø1ø1G H
;
ø1ø1H I
}
ù1ù1 	
public
û1û1 %
seguimiento_entregables
û1û1 &&
GetSeguimientoEntregable
û1û1' ?
(
û1û1? @
int
û1û1@ C
id
û1û1D F
)
û1û1F G
{
ü1ü1 	
return
ı1ı1 
DACConsulta
ı1ı1 
.
ı1ı1 &
GetSeguimientoEntregable
ı1ı1 7
(
ı1ı17 8
id
ı1ı18 :
)
ı1ı1: ;
;
ı1ı1; <
}
ş1ş1 	
public
€2€2 
void
€2€2 2
$InsertarSeguimientoEntregablePeriodo
€2€2 8
(
€2€28 9-
seguimiento_entregables_periodo
€2€29 X
obj
€2€2Y \
,
€2€2\ ]
ref
€2€2^ a 
MessageResponseOBJ
€2€2b t
MsgRes
€2€2u {
)
€2€2{ |
{
22 	

DACInserta
‚2‚2 
.
‚2‚2 2
$InsertarSeguimientoEntregablePeriodo
‚2‚2 ;
(
‚2‚2; <
obj
‚2‚2< ?
,
‚2‚2? @
ref
‚2‚2A D
MsgRes
‚2‚2E K
)
‚2‚2K L
;
‚2‚2L M
}
ƒ2ƒ2 	
public
…2…2 
void
…2…2 '
InsertarGestionEntregable
…2…2 -
(
…2…2- .
int
…2…2. 1'
id_seg_entregable_periodo
…2…22 K
,
…2…2K L&
seguimiento_dtll_entrega
…2…2M e
obj
…2…2f i
,
…2…2i j
ref
…2…2k n!
MessageResponseOBJ…2…2o 
MsgRes…2…2‚ ˆ
)…2…2ˆ ‰
{
†2†2 	

DACInserta
‡2‡2 
.
‡2‡2 '
InsertarGestionEntregable
‡2‡2 0
(
‡2‡20 1'
id_seg_entregable_periodo
‡2‡21 J
,
‡2‡2J K
obj
‡2‡2L O
,
‡2‡2O P
ref
‡2‡2Q T
MsgRes
‡2‡2U [
)
‡2‡2[ \
;
‡2‡2\ ]
}
ˆ2ˆ2 	
public
Š2Š2 
List
Š2Š2 
<
Š2Š2 2
$ref_cobertura_seguimiento_entregable
Š2Š2 8
>
Š2Š28 9'
GetCoberturaSegEntregable
Š2Š2: S
(
Š2Š2S T
)
Š2Š2T U
{
‹2‹2 	
return
Œ2Œ2 
DACConsulta
Œ2Œ2 
.
Œ2Œ2 '
GetCoberturaSegEntregable
Œ2Œ2 8
(
Œ2Œ28 9
)
Œ2Œ29 :
;
Œ2Œ2: ;
}
22 	
public
22 
void
22 "
ActualizarEntregable
22 (
(
22( )
int
22) ,'
id_seg_entregable_periodo
22- F
,
22F G&
seguimiento_dtll_entrega
22H `
obj
22a d
,
22d e
ref
22f i 
MessageResponseOBJ
22j |
MsgRes22} ƒ
)22ƒ „
{
22 	
DACActualiza
‘2‘2 
.
‘2‘2 "
ActualizarEntregable
‘2‘2 -
(
‘2‘2- .'
id_seg_entregable_periodo
‘2‘2. G
,
‘2‘2G H
obj
‘2‘2I L
,
‘2‘2L M
ref
‘2‘2N Q
MsgRes
‘2‘2R X
)
‘2‘2X Y
;
‘2‘2Y Z
}
’2’2 	
public
–2–2 
void
–2–2 +
GuardarRespuestaObservaciones
–2–2 1
(
–2–21 2&
seguimiento_dtll_entrega
–2–22 J
obj
–2–2K N
,
–2–2N O
ref
–2–2P S 
MessageResponseOBJ
–2–2T f
MsgRes
–2–2g m
)
–2–2m n
{
—2—2 	
DACActualiza
˜2˜2 
.
˜2˜2 +
GuardarRespuestaObservaciones
˜2˜2 6
(
˜2˜26 7
obj
˜2˜27 :
,
˜2˜2: ;
ref
˜2˜2< ?
MsgRes
˜2˜2@ F
)
˜2˜2F G
;
˜2˜2G H
}
™2™2 	
public
›2›2 
List
›2›2 
<
›2›2 8
*ref_seguimiento_entregable_usuario_gestion
›2›2 >
>
›2›2> ?#
GetUsuariosSegGestion
›2›2@ U
(
›2›2U V
)
›2›2V W
{
œ2œ2 	
return
22 
DACConsulta
22 
.
22 #
GetUsuariosSegGestion
22 4
(
224 5
)
225 6
;
226 7
}
22 	
public
 2 2 
int
 2 2 '
InsertarPeriodoEntregable
 2 2 ,
(
 2 2, --
seguimiento_entregables_periodo
 2 2- L
obj
 2 2M P
,
 2 2P Q
ref
 2 2R U 
MessageResponseOBJ
 2 2V h
MsgRes
 2 2i o
)
 2 2o p
{
¡2¡2 	
return
¢2¢2 

DACInserta
¢2¢2 
.
¢2¢2 '
InsertarPeriodoEntregable
¢2¢2 7
(
¢2¢27 8
obj
¢2¢28 ;
,
¢2¢2; <
ref
¢2¢2= @
MsgRes
¢2¢2A G
)
¢2¢2G H
;
¢2¢2H I
}
£2£2 	
public
¥2¥2 
int
¥2¥2 )
ActualizarEntregablePeriodo
¥2¥2 .
(
¥2¥2. /-
seguimiento_entregables_periodo
¥2¥2/ N
obj
¥2¥2O R
,
¥2¥2R S
ref
¥2¥2T W 
MessageResponseOBJ
¥2¥2X j
MsgRes
¥2¥2k q
)
¥2¥2q r
{
¦2¦2 	
return
§2§2 
DACActualiza
§2§2 
.
§2§2  )
ActualizarEntregablePeriodo
§2§2  ;
(
§2§2; <
obj
§2§2< ?
,
§2§2? @
ref
§2§2A D
MsgRes
§2§2E K
)
§2§2K L
;
§2§2L M
}
¨2¨2 	
public
ª2ª2 
List
ª2ª2 
<
ª2ª2 5
'vw_seguimiento_entregables_competencias
ª2ª2 ;
>
ª2ª2; <3
%GetSeguimientoEntregablesCompetencias
ª2ª2= b
(
ª2ª2b c
)
ª2ª2c d
{
«2«2 	
return
¬2¬2 
DACConsulta
¬2¬2 
.
¬2¬2 3
%GetSeguimientoEntregablesCompetencias
¬2¬2 D
(
¬2¬2D E
)
¬2¬2E F
;
¬2¬2F G
}
­2­2 	
public
¯2¯2 
List
¯2¯2 
<
¯2¯2 $
ref_proceso_entregable
¯2¯2 *
>
¯2¯2* +"
Getprocesoentregable
¯2¯2, @
(
¯2¯2@ A
)
¯2¯2A B
{
°2°2 	
return
±2±2 
DACConsulta
±2±2 
.
±2±2 "
Getprocesoentregable
±2±2 3
(
±2±23 4
)
±2±24 5
;
±2±25 6
}
²2²2 	
public
º2º2 
List
º2º2 
<
º2º2 9
+ref_seguimiento_entregables_tipo_entregable
º2º2 ?
>
º2º2? @#
GetListTipoEntregable
º2º2A V
(
º2º2V W
)
º2º2W X
{
»2»2 	
return
¼2¼2 
DACConsulta
¼2¼2 
.
¼2¼2 #
GetListTipoEntregable
¼2¼2 4
(
¼2¼24 5
)
¼2¼25 6
;
¼2¼26 7
}
½2½2 	
public
Å2Å2 
List
Å2Å2 
<
Å2Å2 #
ref_estado_entregable
Å2Å2 )
>
Å2Å2) *%
GetListEstadoEntregable
Å2Å2+ B
(
Å2Å2B C
)
Å2Å2C D
{
Æ2Æ2 	
return
Ç2Ç2 
DACConsulta
Ç2Ç2 
.
Ç2Ç2 %
GetListEstadoEntregable
Ç2Ç2 6
(
Ç2Ç26 7
)
Ç2Ç27 8
;
Ç2Ç28 9
}
È2È2 	
public
Ñ2Ñ2 
List
Ñ2Ñ2 
<
Ñ2Ñ2 3
%seguimiento_entregables_alerta_diaria
Ñ2Ñ2 9
>
Ñ2Ñ29 :A
3GetListNotificacionesEnviadasSeguimientoEntregables
Ñ2Ñ2; n
(
Ñ2Ñ2n o
DateTime
Ñ2Ñ2o w
?
Ñ2Ñ2w x
fecha
Ñ2Ñ2y ~
)
Ñ2Ñ2~ 
{
Ò2Ò2 	
return
Ó2Ó2 
DACConsulta
Ó2Ó2 
.
Ó2Ó2 A
3GetListNotificacionesEnviadasSeguimientoEntregables
Ó2Ó2 R
(
Ó2Ó2R S
fecha
Ó2Ó2S X
)
Ó2Ó2X Y
;
Ó2Ó2Y Z
}
Ô2Ô2 	
public
Ş2Ş2 
List
Ş2Ş2 
<
Ş2Ş2 >
0Management_seguimiento_entregables_gestionResult
Ş2Ş2 D
>
Ş2Ş2D E1
#GetListSeguimientoEntregableGestion
Ş2Ş2F i
(
Ş2Ş2i j
int
Ş2Ş2j m
?
Ş2Ş2m n
periodicidad
Ş2Ş2o {
,
Ş2Ş2{ |
intŞ2Ş2} €
?Ş2Ş2€ 
tipoEntregableŞ2Ş2‚ 
)Ş2Ş2 ‘
{
ß2ß2 	
return
à2à2 
DACConsulta
à2à2 
.
à2à2 1
#GetListSeguimientoEntregableGestion
à2à2 B
(
à2à2B C
periodicidad
à2à2C O
,
à2à2O P
tipoEntregable
à2à2Q _
)
à2à2_ `
;
à2à2` a
}
á2á2 	
public
ê2ê2 
List
ê2ê2 
<
ê2ê2 (
vw_seguimiento_entregables
ê2ê2 .
>
ê2ê2. //
!GetListEntregablesPorIdEntregable
ê2ê20 Q
(
ê2ê2Q R
int
ê2ê2R U
?
ê2ê2U V%
idSeguimientoEntregable
ê2ê2W n
)
ê2ê2n o
{
ë2ë2 	
return
ì2ì2 
DACConsulta
ì2ì2 
.
ì2ì2 /
!GetListEntregablesPorIdEntregable
ì2ì2 @
(
ì2ì2@ A%
idSeguimientoEntregable
ì2ì2A X
)
ì2ì2X Y
;
ì2ì2Y Z
}
í2í2 	
public
ö2ö2 
void
ö2ö2 2
$GuardarDatosEvalCalidadSegEntregable
ö2ö2 8
(
ö2ö28 9:
,seguimiento_entregables_periodo_eval_calidad
ö2ö29 e
obj
ö2ö2f i
,
ö2ö2i j
ref
ö2ö2k n!
MessageResponseOBJö2ö2o 
MsgResö2ö2‚ ˆ
)ö2ö2ˆ ‰
{
÷2÷2 	

DACInserta
ø2ø2 
.
ø2ø2 2
$GuardarDatosEvalCalidadSegEntregable
ø2ø2 ;
(
ø2ø2; <
obj
ø2ø2< ?
,
ø2ø2? @
ref
ø2ø2A D
MsgRes
ø2ø2E K
)
ø2ø2K L
;
ø2ø2L M
}
ù2ù2 	
public
‚3‚3 :
,seguimiento_entregables_periodo_eval_calidad
‚3‚3 ;:
,ConsultarEvaluacionPorIdPeriodoSegEntregable
‚3‚3< h
(
‚3‚3h i
int
‚3‚3i l
id
‚3‚3m o
)
‚3‚3o p
{
ƒ3ƒ3 	
return
„3„3 
DACConsulta
„3„3 
.
„3„3 :
,ConsultarEvaluacionPorIdPeriodoSegEntregable
„3„3 K
(
„3„3K L
id
„3„3L N
)
„3„3N O
;
„3„3O P
}
…3…3 	
public
‘3‘3 
List
‘3‘3 
<
‘3‘3 B
4Management_seguimiento_entregables_indicadoresResult
‘3‘3 H
>
‘3‘3H I9
+GetListadoOportunidadSeguimientoEntregables
‘3‘3J u
(
‘3‘3u v
string
‘3‘3v |!
personaResponsable‘3‘3} 
,‘3‘3 
int‘3‘3‘ ”
?‘3‘3” •
tipoEntregable‘3‘3– ¤
,‘3‘3¤ ¥
int‘3‘3¦ ©
?‘3‘3© ª
periodicidad‘3‘3« ·
,‘3‘3· ¸
int‘3‘3¹ ¼
?‘3‘3¼ ½
aÃ±o‘3‘3¾ Á
)‘3‘3Á Â
{
’3’3 	
return
“3“3 
DACConsulta
“3“3 
.
“3“3 9
+GetListadoOportunidadSeguimientoEntregables
“3“3 J
(
“3“3J K 
personaResponsable
“3“3K ]
,
“3“3] ^
tipoEntregable
“3“3_ m
,
“3“3m n
periodicidad
“3“3o {
,
“3“3{ |
aÃ±o“3“3} €
)“3“3€ 
;“3“3 ‚
}
”3”3 	
public
33 
List
33 
<
33 G
9Management_SeguimientoEntregables_IndicadorXPersonaResult
33 M
>
33M NB
3GetListadoIndicadoresXPersonaSeguimientoEntregables33O ‚
(33‚ ƒ
int33ƒ †

mesInicial33‡ ‘
,33‘ ’
int33“ –
mesFinal33— Ÿ
,33Ÿ  
int33¡ ¤
aÃ±o33¥ ¨
,33¨ ©
string33ª °
responsable33± ¼
)33¼ ½
{
Ÿ3Ÿ3 	
return
 3 3 
DACConsulta
 3 3 
.
 3 3 A
3GetListadoIndicadoresXPersonaSeguimientoEntregables
 3 3 R
(
 3 3R S

mesInicial
 3 3S ]
,
 3 3] ^
mesFinal
 3 3_ g
,
 3 3g h
aÃ±o
 3 3i l
,
 3 3l m
responsable
 3 3n y
)
 3 3y z
;
 3 3z {
}
¡3¡3 	
public
¬3¬3 
List
¬3¬3 
<
¬3¬3 J
<Management_SeguimientoEntregables_IndicadorXComponenteResult
¬3¬3 P
>
¬3¬3P QE
6GetListadoIndicadoresXComponenteSeguimientoEntregables¬3¬3R ˆ
(¬3¬3ˆ ‰
int¬3¬3‰ Œ

mesInicial¬3¬3 —
,¬3¬3— ˜
int¬3¬3™ œ
mesFinal¬3¬3 ¥
,¬3¬3¥ ¦
int¬3¬3§ ª
aÃ±o¬3¬3« ®
,¬3¬3® ¯
int¬3¬3° ³
?¬3¬3³ ´
	idProceso¬3¬3µ ¾
)¬3¬3¾ ¿
{
­3­3 	
return
®3®3 
DACConsulta
®3®3 
.
®3®3 D
6GetListadoIndicadoresXComponenteSeguimientoEntregables
®3®3 U
(
®3®3U V

mesInicial
®3®3V `
,
®3®3` a
mesFinal
®3®3b j
,
®3®3j k
aÃ±o
®3®3l o
,
®3®3o p
	idProceso
®3®3q z
)
®3®3z {
;
®3®3{ |
}
¯3¯3 	
public
º3º3 
List
º3º3 
<
º3º3 P
BManagement_SeguimientoEntregables_IndicadorXCompyPeridicidadResult
º3º3 V
>
º3º3V WL
=GetListadoIndicadoresXCompYPeriodicidadSeguimientoEntregablesº3º3X •
(º3º3• –
intº3º3– ™

mesInicialº3º3š ¤
,º3º3¤ ¥
intº3º3¦ ©
mesFinalº3º3ª ²
,º3º3² ³
intº3º3´ ·
aÃ±oº3º3¸ »
,º3º3» ¼
intº3º3½ À
?º3º3À Á
	idProcesoº3º3Â Ë
,º3º3Ë Ì
intº3º3Í Ğ
?º3º3Ğ Ñ
idPeriodicidadº3º3Ò à
)º3º3à á
{
»3»3 	
return
¼3¼3 
DACConsulta
¼3¼3 
.
¼3¼3 K
=GetListadoIndicadoresXCompYPeriodicidadSeguimientoEntregables
¼3¼3 \
(
¼3¼3\ ]

mesInicial
¼3¼3] g
,
¼3¼3g h
mesFinal
¼3¼3i q
,
¼3¼3q r
aÃ±o
¼3¼3s v
,
¼3¼3v w
	idProceso¼3¼3x 
,¼3¼3 ‚
idPeriodicidad¼3¼3ƒ ‘
)¼3¼3‘ ’
;¼3¼3’ “
}
½3½3 	
public
Ç3Ç3 
List
Ç3Ç3 
<
Ç3Ç3 J
<Management_SeguimientoEntregables_IndicadorVencimientoResult
Ç3Ç3 P
>
Ç3Ç3P Q6
(GetIndicadorDiasVencimientSegEntregables
Ç3Ç3R z
(
Ç3Ç3z {
stringÇ3Ç3{ 
responsableÇ3Ç3‚ 
,Ç3Ç3 
intÇ3Ç3 ’
?Ç3Ç3’ “
	idProcesoÇ3Ç3” 
,Ç3Ç3 
intÇ3Ç3Ÿ ¢
?Ç3Ç3¢ £
aÃ±oÇ3Ç3¤ §
)Ç3Ç3§ ¨
{
È3È3 	
return
É3É3 
DACConsulta
É3É3 
.
É3É3 6
(GetIndicadorDiasVencimientSegEntregables
É3É3 G
(
É3É3G H
responsable
É3É3H S
,
É3É3S T
	idProceso
É3É3U ^
,
É3É3^ _
aÃ±o
É3É3` c
)
É3É3c d
;
É3É3d e
}
Ê3Ê3 	
public
Ë3Ë3 
int
Ë3Ë3 ,
eliminarEvaluacioEntregablesID
Ë3Ë3 1
(
Ë3Ë31 2
int
Ë3Ë32 5
?
Ë3Ë35 6
	idPeriodo
Ë3Ë37 @
)
Ë3Ë3@ A
{
Ì3Ì3 	
return
Í3Í3 

DACElimina
Í3Í3 
.
Í3Í3 ,
eliminarEvaluacioEntregablesID
Í3Í3 <
(
Í3Í3< =
	idPeriodo
Í3Í3= F
)
Í3Í3F G
;
Í3Í3G H
}
Î3Î3 	
public
Ï3Ï3 
int
Ï3Ï3 1
#eliminarFelicitacionesEntregablesID
Ï3Ï3 6
(
Ï3Ï36 7
int
Ï3Ï37 :
?
Ï3Ï3: ;
	idPeriodo
Ï3Ï3< E
)
Ï3Ï3E F
{
Ğ3Ğ3 	
return
Ñ3Ñ3 

DACElimina
Ñ3Ñ3 
.
Ñ3Ñ3 1
#eliminarFelicitacionesEntregablesID
Ñ3Ñ3 A
(
Ñ3Ñ3A B
	idPeriodo
Ñ3Ñ3B K
)
Ñ3Ñ3K L
;
Ñ3Ñ3L M
}
Ò3Ò3 	
public
Ø3Ø3 
List
Ø3Ø3 
<
Ø3Ø3 0
"ref_contact_clasificacion_contacto
Ø3Ø3 6
>
Ø3Ø36 7*
GetListClasificacionContacto
Ø3Ø38 T
(
Ø3Ø3T U
)
Ø3Ø3U V
{
Ù3Ù3 	
return
Ú3Ú3 
DACConsulta
Ú3Ú3 
.
Ú3Ú3 *
GetListClasificacionContacto
Ú3Ú3 ;
(
Ú3Ú3; <
)
Ú3Ú3< =
;
Ú3Ú3= >
}
Û3Û3 	
public
İ3İ3 
List
İ3İ3 
<
İ3İ3 &
ref_contact_tipificacion
İ3İ3 ,
>
İ3İ3, -!
GetListTipificacion
İ3İ3. A
(
İ3İ3A B
)
İ3İ3B C
{
Ş3Ş3 	
return
ß3ß3 
DACConsulta
ß3ß3 
.
ß3ß3 !
GetListTipificacion
ß3ß3 2
(
ß3ß32 3
)
ß3ß33 4
;
ß3ß34 5
}
à3à3 	
public
â3â3 
List
â3â3 
<
â3â3 '
ref_contact_tipo_servicio
â3â3 -
>
â3â3- .!
GetListTipoServicio
â3â3/ B
(
â3â3B C
)
â3â3C D
{
ã3ã3 	
return
ä3ä3 
DACConsulta
ä3ä3 
.
ä3ä3 !
GetListTipoServicio
ä3ä3 2
(
ä3ä32 3
)
ä3ä33 4
;
ä3ä34 5
}
å3å3 	
public
ç3ç3 
List
ç3ç3 
<
ç3ç3 (
ref_contact_tipo_solicitud
ç3ç3 .
>
ç3ç3. /"
GetListTipoSolicitud
ç3ç30 D
(
ç3ç3D E
)
ç3ç3E F
{
è3è3 	
return
é3é3 
DACConsulta
é3é3 
.
é3é3 "
GetListTipoSolicitud
é3é3 3
(
é3é33 4
)
é3é34 5
;
é3é35 6
}
ê3ê3 	
public
ì3ì3 
List
ì3ì3 
<
ì3ì3 /
!ref_contact_tipoSolicitudBitacora
ì3ì3 5
>
ì3ì35 6*
GetListTipoSolicitudBitacora
ì3ì37 S
(
ì3ì3S T
)
ì3ì3T U
{
í3í3 	
return
î3î3 
DACConsulta
î3î3 
.
î3î3 *
GetListTipoSolicitudBitacora
î3î3 ;
(
î3î3; <
)
î3î3< =
;
î3î3= >
}
ï3ï3 	
public
ğ3ğ3 
List
ğ3ğ3 
<
ğ3ğ3 
	Ref_cie10
ğ3ğ3 
>
ğ3ğ3 
GetCie10Bycodigo
ğ3ğ3 /
(
ğ3ğ3/ 0
string
ğ3ğ30 6
term
ğ3ğ37 ;
)
ğ3ğ3; <
{
ñ3ñ3 	
return
ò3ò3 
DACConsulta
ò3ò3 
.
ò3ò3 
GetCie10Bycodigo
ò3ò3 /
(
ò3ò3/ 0
term
ò3ò30 4
)
ò3ò34 5
;
ò3ò35 6
}
ó3ó3 	
public
õ3õ3 
List
õ3õ3 
<
õ3õ3 
ref_cie10_mortNat
õ3õ3 %
>
õ3õ3% &$
GetCie10MorNatBycodigo
õ3õ3' =
(
õ3õ3= >
string
õ3õ3> D
term
õ3õ3E I
)
õ3õ3I J
{
ö3ö3 	
return
÷3÷3 
DACConsulta
÷3÷3 
.
÷3÷3 $
GetCie10MorNatBycodigo
÷3÷3 5
(
÷3÷35 6
term
÷3÷36 :
)
÷3÷3: ;
;
÷3÷3; <
}
ø3ø3 	
public
ú3ú3 
List
ú3ú3 
<
ú3ú3 *
ref_contact_estado_solicitud
ú3ú3 0
>
ú3ú30 1$
GetListEstadoSolicitud
ú3ú32 H
(
ú3ú3H I
)
ú3ú3I J
{
û3û3 	
return
ü3ü3 
DACConsulta
ü3ü3 
.
ü3ü3 $
GetListEstadoSolicitud
ü3ü3 5
(
ü3ü35 6
)
ü3ü36 7
;
ü3ü37 8
}
ı3ı3 	
public
ÿ3ÿ3 
List
ÿ3ÿ3 
<
ÿ3ÿ3 ,
ref_contact_medio_notificacion
ÿ3ÿ3 2
>
ÿ3ÿ32 3'
GetListMediosNotificacion
ÿ3ÿ34 M
(
ÿ3ÿ3M N
)
ÿ3ÿ3N O
{
€4€4 	
return
44 
DACConsulta
44 
.
44 '
GetListMediosNotificacion
44 8
(
448 9
)
449 :
;
44: ;
}
‚4‚4 	
public
„4„4 
int
„4„4 *
InsertarIngresoContactCenter
„4„4 /
(
„4„4/ 0
contact_center
„4„40 >
obj
„4„4? B
,
„4„4B C
ref
„4„4D G 
MessageResponseOBJ
„4„4H Z
MsgRes
„4„4[ a
)
„4„4a b
{
…4…4 	
return
†4†4 

DACInserta
†4†4 
.
†4†4 *
InsertarIngresoContactCenter
†4†4 :
(
†4†4: ;
obj
†4†4; >
,
†4†4> ?
ref
†4†4@ C
MsgRes
†4†4D J
)
†4†4J K
;
†4†4K L
}
‡4‡4 	
public
‰4‰4 
void
‰4‰4 (
InsertarBitacoraCallCenter
‰4‰4 .
(
‰4‰4. /
List
‰4‰4/ 3
<
‰4‰43 4!
contact_center_dtll
‰4‰44 G
>
‰4‰4G H
List
‰4‰4I M
,
‰4‰4M N
int
‰4‰4O R
id_contact_center
‰4‰4S d
,
‰4‰4d e
string
‰4‰4f l
usuario
‰4‰4m t
)
‰4‰4t u
{
Š4Š4 	

DACInserta
‹4‹4 
.
‹4‹4 (
InsertarBitacoraCallCenter
‹4‹4 1
(
‹4‹41 2
List
‹4‹42 6
,
‹4‹46 7
id_contact_center
‹4‹48 I
,
‹4‹4I J
usuario
‹4‹4K R
)
‹4‹4R S
;
‹4‹4S T
}
Œ4Œ4 	
public
44 
int
44 +
InsertarBitacoraContactCenter
44 0
(
440 1!
contact_center_dtll
441 D
obj
44E H
)
44H I
{
44 	
return
44 

DACInserta
44 
.
44 +
InsertarBitacoraContactCenter
44 ;
(
44; <
obj
44< ?
)
44? @
;
44@ A
}
44 	
public
’4’4 
contact_center
’4’4 "
GetContactCenterById
’4’4 2
(
’4’42 3
int
’4’43 6
id
’4’47 9
)
’4’49 :
{
“4“4 	
return
”4”4 
DACConsulta
”4”4 
.
”4”4 "
GetContactCenterById
”4”4 3
(
”4”43 4
id
”4”44 6
)
”4”46 7
;
”4”47 8
}
•4•4 	
public
—4—4 
List
—4—4 
<
—4—4 !
contact_center_dtll
—4—4 '
>
—4—4' (&
GetListBitacoraByIngreso
—4—4) A
(
—4—4A B
int
—4—4B E
id_contact_center
—4—4F W
,
—4—4W X
int
—4—4Y \
?
—4—4\ ]
censo
—4—4^ c
,
—4—4c d
int
—4—4e h
?
—4—4h i
idConcurrencia
—4—4j x
)
—4—4x y
{
˜4˜4 	
return
™4™4 
DACConsulta
™4™4 
.
™4™4 &
GetListBitacoraByIngreso
™4™4 7
(
™4™47 8
id_contact_center
™4™48 I
,
™4™4I J
censo
™4™4K P
,
™4™4P Q
idConcurrencia
™4™4R `
)
™4™4` a
;
™4™4a b
}
š4š4 	
public
œ4œ4 
int
œ4œ4 .
 ActualizarContactCenterPrincipal
œ4œ4 3
(
œ4œ43 4
int
œ4œ44 7
?
œ4œ47 8
	idContact
œ4œ49 B
)
œ4œ4B C
{
44 	
return
44 
DACActualiza
44 
.
44  .
 ActualizarContactCenterPrincipal
44  @
(
44@ A
	idContact
44A J
)
44J K
;
44K L
}
Ÿ4Ÿ4 	
public
 4 4 
List
 4 4 
<
 4 4 
vw_contact_center
 4 4 %
>
 4 4% &"
GetListContactCenter
 4 4' ;
(
 4 4; <
int
 4 4< ?
?
 4 4? @
estado
 4 4A G
)
 4 4G H
{
¡4¡4 	
return
¢4¢4 
DACConsulta
¢4¢4 
.
¢4¢4 "
GetListContactCenter
¢4¢4 3
(
¢4¢43 4
estado
¢4¢44 :
)
¢4¢4: ;
;
¢4¢4; <
}
£4£4 	
public
¤4¤4 
List
¤4¤4 
<
¤4¤4 -
management_contact_centerResult
¤4¤4 3
>
¤4¤43 4'
ListaTableroContactCenter
¤4¤45 N
(
¤4¤4N O
DateTime
¤4¤4O W
?
¤4¤4W X
fechaIni
¤4¤4Y a
,
¤4¤4a b
DateTime
¤4¤4c k
?
¤4¤4k l
fechaFin
¤4¤4m u
)
¤4¤4u v
{
¥4¥4 	
return
¦4¦4 
DACConsulta
¦4¦4 
.
¦4¦4 '
ListaTableroContactCenter
¦4¦4 8
(
¦4¦48 9
fechaIni
¦4¦49 A
,
¦4¦4A B
fechaFin
¦4¦4C K
)
¦4¦4K L
;
¦4¦4L M
}
§4§4 	
public
¨4¨4 -
management_contact_centerResult
¨4¨4 .,
GetContactCenterCensoIdContact
¨4¨4/ M
(
¨4¨4M N
int
¨4¨4N Q
id
¨4¨4R T
)
¨4¨4T U
{
©4©4 	
return
ª4ª4 
DACConsulta
ª4ª4 
.
ª4ª4 ,
GetContactCenterCensoIdContact
ª4ª4 =
(
ª4ª4= >
id
ª4ª4> @
)
ª4ª4@ A
;
ª4ª4A B
}
«4«4 	
public
­4­4 -
management_contact_centerResult
­4­4 .*
GetContactCenterCensoIdCenso
­4­4/ K
(
­4­4K L
int
­4­4L O
id
­4­4P R
)
­4­4R S
{
®4®4 	
return
¯4¯4 
DACConsulta
¯4¯4 
.
¯4¯4 *
GetContactCenterCensoIdCenso
¯4¯4 ;
(
¯4¯4; <
id
¯4¯4< >
)
¯4¯4> ?
;
¯4¯4? @
}
°4°4 	
public
²4²4 -
management_contact_centerResult
²4²4 .1
#GetContactCenterCensoIdConcurrencia
²4²4/ R
(
²4²4R S
int
²4²4S V
id
²4²4W Y
)
²4²4Y Z
{
³4³4 	
return
´4´4 
DACConsulta
´4´4 
.
´4´4 1
#GetContactCenterCensoIdConcurrencia
´4´4 B
(
´4´4B C
id
´4´4C E
)
´4´4E F
;
´4´4F G
}
µ4µ4 	
public
·4·4 
int
·4·4 3
%ActualizarEnContactCenterConcurrencia
·4·4 8
(
·4·48 9
int
·4·49 <
?
·4·4< =
idConcurrencia
·4·4> L
,
·4·4L M
ref
·4·4N Q 
MessageResponseOBJ
·4·4R d
MsgRes
·4·4e k
)
·4·4k l
{
¸4¸4 	
return
¹4¹4 
DACActualiza
¹4¹4 
.
¹4¹4  3
%ActualizarEnContactCenterConcurrencia
¹4¹4  E
(
¹4¹4E F
idConcurrencia
¹4¹4F T
,
¹4¹4T U
ref
¹4¹4V Y
MsgRes
¹4¹4Z `
)
¹4¹4` a
;
¹4¹4a b
}
º4º4 	
public
¼4¼4 
int
¼4¼4 ,
ActualizarEnContactCenterCenso
¼4¼4 1
(
¼4¼41 2
int
¼4¼42 5
?
¼4¼45 6
idCenso
¼4¼47 >
,
¼4¼4> ?
ref
¼4¼4@ C 
MessageResponseOBJ
¼4¼4D V
MsgRes
¼4¼4W ]
)
¼4¼4] ^
{
½4½4 	
return
¾4¾4 
DACActualiza
¾4¾4 
.
¾4¾4  ,
ActualizarEnContactCenterCenso
¾4¾4  >
(
¾4¾4> ?
idCenso
¾4¾4? F
,
¾4¾4F G
ref
¾4¾4H K
MsgRes
¾4¾4L R
)
¾4¾4R S
;
¾4¾4S T
}
¿4¿4 	
public
Á4Á4 
void
Á4Á4 6
(InsertarLogConcurrenciaEnviadaCallCenter
Á4Á4 <
(
Á4Á4< =
List
Á4Á4= A
<
Á4Á4A B3
%log_concurrenciaEnviada_contactCenter
Á4Á4B g
>
Á4Á4g h
log
Á4Á4i l
,
Á4Á4l m
ref
Á4Á4n q!
MessageResponseOBJÁ4Á4r „
MsgResÁ4Á4… ‹
)Á4Á4‹ Œ
{
Â4Â4 	

DACInserta
Ã4Ã4 
.
Ã4Ã4 6
(InsertarLogConcurrenciaEnviadaCallCenter
Ã4Ã4 ?
(
Ã4Ã4? @
log
Ã4Ã4@ C
,
Ã4Ã4C D
ref
Ã4Ã4E H
MsgRes
Ã4Ã4I O
)
Ã4Ã4O P
;
Ã4Ã4P Q
}
Ä4Ä4 	
public
Æ4Æ4 
void
Æ4Æ4 @
2InsertarLogindividualConcurrenciaEnviadaCallCenter
Æ4Æ4 F
(
Æ4Æ4F G3
%log_concurrenciaEnviada_contactCenter
Æ4Æ4G l
log
Æ4Æ4m p
,
Æ4Æ4p q
ref
Æ4Æ4r u!
MessageResponseOBJÆ4Æ4v ˆ
MsgResÆ4Æ4‰ 
)Æ4Æ4 
{
Ç4Ç4 	

DACInserta
È4È4 
.
È4È4 @
2InsertarLogindividualConcurrenciaEnviadaCallCenter
È4È4 I
(
È4È4I J
log
È4È4J M
,
È4È4M N
ref
È4È4O R
MsgRes
È4È4S Y
)
È4È4Y Z
;
È4È4Z [
}
É4É4 	
public
Ê4Ê4 
void
Ê4Ê4 "
ActualizarImagenCaso
Ê4Ê4 (
(
Ê4Ê4( )
string
Ê4Ê4) /

rutaImagen
Ê4Ê40 :
,
Ê4Ê4: ;
string
Ê4Ê4< B
tipo
Ê4Ê4C G
,
Ê4Ê4G H
int
Ê4Ê4I L
contactcenter
Ê4Ê4M Z
)
Ê4Ê4Z [
{
Ë4Ë4 	
DACActualiza
Ì4Ì4 
.
Ì4Ì4 "
ActualizarImagenCaso
Ì4Ì4 -
(
Ì4Ì4- .

rutaImagen
Ì4Ì4. 8
,
Ì4Ì48 9
tipo
Ì4Ì4: >
,
Ì4Ì4> ?
contactcenter
Ì4Ì4@ M
)
Ì4Ì4M N
;
Ì4Ì4N O
}
Í4Í4 	
public
Ï4Ï4 
List
Ï4Ï4 
<
Ï4Ï4 %
ref_contact_solicitante
Ï4Ï4 +
>
Ï4Ï4+ ,'
GetlistSolicitantesbytipo
Ï4Ï4- F
(
Ï4Ï4F G
string
Ï4Ï4G M
term
Ï4Ï4N R
,
Ï4Ï4R S
int
Ï4Ï4T W
tipo
Ï4Ï4X \
)
Ï4Ï4\ ]
{
Ğ4Ğ4 	
return
Ñ4Ñ4 
DACConsulta
Ñ4Ñ4 
.
Ñ4Ñ4 '
GetlistSolicitantesbytipo
Ñ4Ñ4 8
(
Ñ4Ñ48 9
term
Ñ4Ñ49 =
,
Ñ4Ñ4= >
tipo
Ñ4Ñ4? C
)
Ñ4Ñ4C D
;
Ñ4Ñ4D E
}
Ò4Ò4 	
public
Ô4Ô4 
List
Ô4Ô4 
<
Ô4Ô4 @
2management_contact_center_camposObligatoriosResult
Ô4Ô4 F
>
Ô4Ô4F G'
ListaCamposObligatoriosCC
Ô4Ô4H a
(
Ô4Ô4a b
int
Ô4Ô4b e
?
Ô4Ô4e f
	idContact
Ô4Ô4g p
,
Ô4Ô4p q
int
Ô4Ô4r u
?
Ô4Ô4u v
idConcurrenciaÔ4Ô4w …
,Ô4Ô4… †
intÔ4Ô4‡ Š
?Ô4Ô4Š ‹
idCensoÔ4Ô4Œ “
)Ô4Ô4“ ”
{
Õ4Õ4 	
return
Ö4Ö4 
DACConsulta
Ö4Ö4 
.
Ö4Ö4 '
ListaCamposObligatoriosCC
Ö4Ö4 8
(
Ö4Ö48 9
	idContact
Ö4Ö49 B
,
Ö4Ö4B C
idConcurrencia
Ö4Ö4D R
,
Ö4Ö4R S
idCenso
Ö4Ö4T [
)
Ö4Ö4[ \
;
Ö4Ö4\ ]
}
×4×4 	
public
Ù4Ù4 
List
Ù4Ù4 
<
Ù4Ù4 9
+management_contact_center_seguimientoResult
Ù4Ù4 ?
>
Ù4Ù4? @2
$ListaTableroContactCenterSeguimiento
Ù4Ù4A e
(
Ù4Ù4e f
DateTime
Ù4Ù4f n
?
Ù4Ù4n o
fechaIni
Ù4Ù4p x
,
Ù4Ù4x y
DateTimeÙ4Ù4z ‚
?Ù4Ù4‚ ƒ
fechaFinÙ4Ù4„ Œ
)Ù4Ù4Œ 
{
Ú4Ú4 	
return
Û4Û4 
DACConsulta
Û4Û4 
.
Û4Û4 2
$ListaTableroContactCenterSeguimiento
Û4Û4 C
(
Û4Û4C D
fechaIni
Û4Û4D L
,
Û4Û4L M
fechaFin
Û4Û4N V
)
Û4Û4V W
;
Û4Û4W X
}
Ü4Ü4 	
public
ß4ß4 
int
ß4ß4 +
ActualizarContactObligatorios
ß4ß4 0
(
ß4ß40 1
contact_center
ß4ß41 ?
obj
ß4ß4@ C
)
ß4ß4C D
{
à4à4 	
return
á4á4 
DACActualiza
á4á4 
.
á4á4  +
ActualizarContactObligatorios
á4á4  =
(
á4á4= >
obj
á4á4> A
)
á4á4A B
;
á4á4B C
}
â4â4 	
public
è4è4 
bool
è4è4 ,
ValidarExistenciaQuejasValidas
è4è4 2
(
è4è42 3
int
è4è43 6
mes
è4è47 :
,
è4è4: ;
int
è4è4< ?
aÃ±o
è4è4@ C
)
è4è4C D
{
é4é4 	
return
ê4ê4 
DACConsulta
ê4ê4 
.
ê4ê4 ,
ValidarExistenciaQuejasValidas
ê4ê4 =
(
ê4ê4= >
mes
ê4ê4> A
,
ê4ê4A B
aÃ±o
ê4ê4C F
)
ê4ê4F G
;
ê4ê4G H
}
ë4ë4 	
public
í4í4 
void
í4í4 '
InsertarQuejasValidasDtll
í4í4 -
(
í4í4- .
List
í4í4. 2
<
í4í42 3)
calidad_quejas_validas_dtll
í4í43 N
>
í4í4N O
List
í4í4P T
,
í4í4T U$
calidad_quejas_validas
í4í4V l
objbase
í4í4m t
,
í4í4t u
ref
í4í4v y!
MessageResponseOBJí4í4z Œ
MsgResí4í4 “
)í4í4“ ”
{
î4î4 	

DACInserta
ï4ï4 
.
ï4ï4 '
InsertarQuejasValidasDtll
ï4ï4 0
(
ï4ï40 1
List
ï4ï41 5
,
ï4ï45 6
objbase
ï4ï47 >
,
ï4ï4> ?
ref
ï4ï4@ C
MsgRes
ï4ï4D J
)
ï4ï4J K
;
ï4ï4K L
}
ğ4ğ4 	
public
ò4ò4 
List
ò4ò4 
<
ò4ò4 '
vw_calidad_quejas_validas
ò4ò4 -
>
ò4ò4- .)
GetListCalidadQuejasValidas
ò4ò4/ J
(
ò4ò4J K
)
ò4ò4K L
{
ó4ó4 	
return
ô4ô4 
DACConsulta
ô4ô4 
.
ô4ô4 )
GetListCalidadQuejasValidas
ô4ô4 :
(
ô4ô4: ;
)
ô4ô4; <
;
ô4ô4< =
}
õ4õ4 	
public
÷4÷4 
List
÷4÷4 
<
÷4÷4 -
calidad_quejas_validas_base_zip
÷4÷4 3
>
÷4÷43 4/
!GetListBasesCargadasQuejasValidas
÷4÷45 V
(
÷4÷4V W
)
÷4÷4W X
{
ø4ø4 	
return
ù4ù4 
DACConsulta
ù4ù4 
.
ù4ù4 /
!GetListBasesCargadasQuejasValidas
ù4ù4 @
(
ù4ù4@ A
)
ù4ù4A B
;
ù4ù4B C
}
ú4ú4 	
public
ü4ü4 -
calidad_quejas_validas_base_zip
ü4ü4 .
GetArchivoById
ü4ü4/ =
(
ü4ü4= >
int
ü4ü4> A
id
ü4ü4B D
)
ü4ü4D E
{
ı4ı4 	
return
ş4ş4 
DACConsulta
ş4ş4 
.
ş4ş4 
GetArchivoById
ş4ş4 -
(
ş4ş4- .
id
ş4ş4. 0
)
ş4ş40 1
;
ş4ş41 2
}
ÿ4ÿ4 	
public
55 
void
55 -
EliminarArchivoZipQuejasValidas
55 3
(
553 4-
calidad_quejas_validas_base_zip
554 S
obj
55T W
)
55W X
{
‚5‚5 	

DACElimina
ƒ5ƒ5 
.
ƒ5ƒ5 -
EliminarArchivoZipQuejasValidas
ƒ5ƒ5 6
(
ƒ5ƒ56 7
obj
ƒ5ƒ57 :
)
ƒ5ƒ5: ;
;
ƒ5ƒ5; <
}
„5„5 	
public
†5†5 
void
†5†5 *
InsertarArchivoQuejasValidas
†5†5 0
(
†5†50 1-
calidad_quejas_validas_base_zip
†5†51 P
obj
†5†5Q T
,
†5†5T U
ref
†5†5V Y 
MessageResponseOBJ
†5†5Z l
MsgRes
†5†5m s
)
†5†5s t
{
‡5‡5 	

DACInserta
ˆ5ˆ5 
.
ˆ5ˆ5 *
InsertarArchivoQuejasValidas
ˆ5ˆ5 3
(
ˆ5ˆ53 4
obj
ˆ5ˆ54 7
,
ˆ5ˆ57 8
ref
ˆ5ˆ59 <
MsgRes
ˆ5ˆ5= C
)
ˆ5ˆ5C D
;
ˆ5ˆ5D E
}
‰5‰5 	
public
‹5‹5 
bool
‹5‹5 .
 ValidarExistenciaOportunidadRIPS
‹5‹5 4
(
‹5‹54 5
int
‹5‹55 8
mes
‹5‹59 <
,
‹5‹5< =
int
‹5‹5> A
aÃ±o
‹5‹5B E
)
‹5‹5E F
{
Œ5Œ5 	
return
55 
DACConsulta
55 
.
55 .
 ValidarExistenciaOportunidadRIPS
55 ?
(
55? @
mes
55@ C
,
55C D
aÃ±o
55E H
)
55H I
;
55I J
}
55 	
public
55 
void
55 %
InsertarOportunidadRips
55 +
(
55+ ,
List
55, 0
<
550 1+
calidad_oportunidad_rips_dtll
551 N
>
55N O
List
55P T
,
55T U&
calidad_oportunidad_rips
55V n
objbase
55o v
,
55v w
ref
55x {!
MessageResponseOBJ55| 
MsgRes55 •
)55• –
{
‘5‘5 	

DACInserta
’5’5 
.
’5’5 %
InsertarOportunidadRips
’5’5 .
(
’5’5. /
List
’5’5/ 3
,
’5’53 4
objbase
’5’55 <
,
’5’5< =
ref
’5’5> A
MsgRes
’5’5B H
)
’5’5H I
;
’5’5I J
}
“5“5 	
public
•5•5 
List
•5•5 
<
•5•5 3
%vw_calidad_oportunidad_rips_indicador
•5•5 9
>
•5•59 :+
GetListCalidadOportunidadRips
•5•5; X
(
•5•5X Y
)
•5•5Y Z
{
–5–5 	
return
—5—5 
DACConsulta
—5—5 
.
—5—5 +
GetListCalidadOportunidadRips
—5—5 <
(
—5—5< =
)
—5—5= >
;
—5—5> ?
}
˜5˜5 	
public
š5š5 
void
š5š5 !
InsertarCalidadRips
š5š5 '
(
š5š5' (
List
š5š5( ,
<
š5š5, -"
calidad_de_rips_dtll
š5š5- A
>
š5š5A B
List
š5š5C G
,
š5š5G H
calidad_de_rips
š5š5I X
objbase
š5š5Y `
,
š5š5` a
ref
š5š5b e 
MessageResponseOBJ
š5š5f x
MsgRes
š5š5y 
)š5š5 €
{
›5›5 	

DACInserta
œ5œ5 
.
œ5œ5 !
InsertarCalidadRips
œ5œ5 *
(
œ5œ5* +
List
œ5œ5+ /
,
œ5œ5/ 0
objbase
œ5œ51 8
,
œ5œ58 9
ref
œ5œ5: =
MsgRes
œ5œ5> D
)
œ5œ5D E
;
œ5œ5E F
}
55 	
public
Ÿ5Ÿ5 
List
Ÿ5Ÿ5 
<
Ÿ5Ÿ5 *
vw_calidad_de_rips_indicador
Ÿ5Ÿ5 0
>
Ÿ5Ÿ50 1'
GetListCalidadCalidadRips
Ÿ5Ÿ52 K
(
Ÿ5Ÿ5K L
)
Ÿ5Ÿ5L M
{
 5 5 	
return
¡5¡5 
DACConsulta
¡5¡5 
.
¡5¡5 '
GetListCalidadCalidadRips
¡5¡5 8
(
¡5¡58 9
)
¡5¡59 :
;
¡5¡5: ;
}
¢5¢5 	
public
¤5¤5 
void
¤5¤5 -
InsertarOportunidadCitasMedicas
¤5¤5 3
(
¤5¤53 4
List
¤5¤54 8
<
¤5¤58 9;
-calidad_oportunidad_citas_medicina_gnral_dtll
¤5¤59 f
>
¤5¤5f g
List
¤5¤5h l
,
¤5¤5l m7
(calidad_oportunidad_citas_medicina_gnral¤5¤5n –
objbase¤5¤5— 
,¤5¤5 Ÿ
ref¤5¤5  £"
MessageResponseOBJ¤5¤5¤ ¶
MsgRes¤5¤5· ½
)¤5¤5½ ¾
{
¥5¥5 	

DACInserta
¦5¦5 
.
¦5¦5 -
InsertarOportunidadCitasMedicas
¦5¦5 6
(
¦5¦56 7
List
¦5¦57 ;
,
¦5¦5; <
objbase
¦5¦5= D
,
¦5¦5D E
ref
¦5¦5F I
MsgRes
¦5¦5J P
)
¦5¦5P Q
;
¦5¦5Q R
}
§5§5 	
public
©5©5 
void
©5©5 +
InsertarCalidadCitasCumplidas
©5©5 1
(
©5©51 2
List
©5©52 6
<
©5©56 7*
calidad_citas_cumplidas_dtll
©5©57 S
>
©5©5S T
List
©5©5U Y
,
©5©5Y Z%
calidad_citas_cumplidas
©5©5[ r
objbase
©5©5s z
,
©5©5z {
ref
©5©5| "
MessageResponseOBJ©5©5€ ’
MsgRes©5©5“ ™
)©5©5™ š
{
ª5ª5 	

DACInserta
«5«5 
.
«5«5 +
InsertarCalidadCitasCumplidas
«5«5 4
(
«5«54 5
List
«5«55 9
,
«5«59 :
objbase
«5«5; B
,
«5«5B C
ref
«5«5D G
MsgRes
«5«5H N
)
«5«5N O
;
«5«5O P
}
¬5¬5 	
public
®5®5 
List
®5®5 
<
®5®5 C
5vw_calidad_oportunidad_citas_medicina_gnral_indicador
®5®5 I
>
®5®5I J,
GetListCalidadOporCitasMedicas
®5®5K i
(
®5®5i j
)
®5®5j k
{
¯5¯5 	
return
°5°5 
DACConsulta
°5°5 
.
°5°5 ,
GetListCalidadOporCitasMedicas
°5°5 =
(
°5°5= >
)
°5°5> ?
;
°5°5? @
}
±5±5 	
public
´5´5 
void
´5´5 ,
InsertarOportunidadOdontologia
´5´5 2
(
´5´52 3
List
´5´53 7
<
´5´57 88
*calidad_oportunidad_odontologia_gnral_dtll
´5´58 b
>
´5´5b c
List
´5´5d h
,
´5´5h i4
%calidad_oportunidad_odontologia_gnral´5´5j 
objbase´5´5 —
,´5´5— ˜
ref´5´5™ œ"
MessageResponseOBJ´5´5 ¯
MsgRes´5´5° ¶
)´5´5¶ ·
{
µ5µ5 	

DACInserta
¶5¶5 
.
¶5¶5 ,
InsertarOportunidadOdontologia
¶5¶5 5
(
¶5¶55 6
List
¶5¶56 :
,
¶5¶5: ;
objbase
¶5¶5< C
,
¶5¶5C D
ref
¶5¶5E H
MsgRes
¶5¶5I O
)
¶5¶5O P
;
¶5¶5P Q
}
·5·5 	
public
º5º5 
List
º5º5 
<
º5º5 @
2vw_calidad_oportunidad_odontologia_gnral_indicador
º5º5 F
>
º5º5F G+
GetListCalidadOporOdontologia
º5º5H e
(
º5º5e f
)
º5º5f g
{
»5»5 	
return
¼5¼5 
DACConsulta
¼5¼5 
.
¼5¼5 +
GetListCalidadOporOdontologia
¼5¼5 <
(
¼5¼5< =
)
¼5¼5= >
;
¼5¼5> ?
}
½5½5 	
public
¿5¿5 
List
¿5¿5 
<
¿5¿5 2
$vw_calidad_citas_cumplidas_indicador
¿5¿5 8
>
¿5¿58 9*
GetListCalidadCitasCumplidas
¿5¿5: V
(
¿5¿5V W
)
¿5¿5W X
{
À5À5 	
return
Á5Á5 
DACConsulta
Á5Á5 
.
Á5Á5 *
GetListCalidadCitasCumplidas
Á5Á5 ;
(
Á5Á5; <
)
Á5Á5< =
;
Á5Á5= >
}
Â5Â5 	
public
Ä5Ä5 
void
Ä5Ä5 %
InsertarEventosAdversos
Ä5Ä5 +
(
Ä5Ä5+ ,
List
Ä5Ä5, 0
<
Ä5Ä50 1$
calidad_evento_adverso
Ä5Ä51 G
>
Ä5Ä5G H
List
Ä5Ä5I M
,
Ä5Ä5M N
ref
Ä5Ä5O R 
MessageResponseOBJ
Ä5Ä5S e
MsgRes
Ä5Ä5f l
)
Ä5Ä5l m
{
Å5Å5 	

DACInserta
Æ5Æ5 
.
Æ5Æ5 %
InsertarEventosAdversos
Æ5Æ5 .
(
Æ5Æ5. /
List
Æ5Æ5/ 3
,
Æ5Æ53 4
ref
Æ5Æ55 8
MsgRes
Æ5Æ59 ?
)
Æ5Æ5? @
;
Æ5Æ5@ A
}
Ç5Ç5 	
public
É5É5 
List
É5É5 
<
É5É5 $
calidad_evento_adverso
É5É5 *
>
É5É5* +)
GetListCalidadEventoAdverso
É5É5, G
(
É5É5G H
)
É5É5H I
{
Ê5Ê5 	
return
Ë5Ë5 
DACConsulta
Ë5Ë5 
.
Ë5Ë5 )
GetListCalidadEventoAdverso
Ë5Ë5 :
(
Ë5Ë5: ;
)
Ë5Ë5; <
;
Ë5Ë5< =
}
Ì5Ì5 	
public
Î5Î5 
void
Î5Î5 %
InsertarDocumentoInsumo
Î5Î5 +
(
Î5Î5+ ,/
!calidad_gestor_documental_insumos
Î5Î5, M
obj
Î5Î5N Q
,
Î5Î5Q R
ref
Î5Î5S V 
MessageResponseOBJ
Î5Î5W i
MsgRes
Î5Î5j p
)
Î5Î5p q
{
Ï5Ï5 	

DACInserta
Ğ5Ğ5 
.
Ğ5Ğ5 %
InsertarDocumentoInsumo
Ğ5Ğ5 .
(
Ğ5Ğ5. /
obj
Ğ5Ğ5/ 2
,
Ğ5Ğ52 3
ref
Ğ5Ğ54 7
MsgRes
Ğ5Ğ58 >
)
Ğ5Ğ5> ?
;
Ğ5Ğ5? @
}
Ñ5Ñ5 	
public
Ó5Ó5 
List
Ó5Ó5 
<
Ó5Ó5 /
!calidad_gestor_documental_insumos
Ó5Ó5 5
>
Ó5Ó55 6,
GetListGestorDocumentalInsumos
Ó5Ó57 U
(
Ó5Ó5U V
)
Ó5Ó5V W
{
Ô5Ô5 	
return
Õ5Õ5 
DACConsulta
Õ5Õ5 
.
Õ5Õ5 ,
GetListGestorDocumentalInsumos
Õ5Õ5 =
(
Õ5Õ5= >
)
Õ5Õ5> ?
;
Õ5Õ5? @
}
Ö5Ö5 	
public
Ø5Ø5 /
!calidad_gestor_documental_insumos
Ø5Ø5 0
GetDocumentoById
Ø5Ø51 A
(
Ø5Ø5A B
int
Ø5Ø5B E
id
Ø5Ø5F H
)
Ø5Ø5H I
{
Ù5Ù5 	
return
Ú5Ú5 
DACConsulta
Ú5Ú5 
.
Ú5Ú5 
GetDocumentoById
Ú5Ú5 /
(
Ú5Ú5/ 0
id
Ú5Ú50 2
)
Ú5Ú52 3
;
Ú5Ú53 4
}
Û5Û5 	
public
Ü5Ü5 2
$vw_calidad_gestor_documental_insumos
Ü5Ü5 3 
VwGetDocumentoById
Ü5Ü54 F
(
Ü5Ü5F G
int
Ü5Ü5G J
id
Ü5Ü5K M
)
Ü5Ü5M N
{
İ5İ5 	
return
Ş5Ş5 
DACConsulta
Ş5Ş5 
.
Ş5Ş5  
VwGetDocumentoById
Ş5Ş5 1
(
Ş5Ş51 2
id
Ş5Ş52 4
)
Ş5Ş54 5
;
Ş5Ş55 6
}
ß5ß5 	
public
à5à5 2
$vw_calidad_gestor_documental_insumos
à5à5 3#
TarerArchivoInsumosId
à5à54 I
(
à5à5I J
int
à5à5J M
id
à5à5N P
)
à5à5P Q
{
á5á5 	
return
â5â5 
DACConsulta
â5â5 
.
â5â5 #
TarerArchivoInsumosId
â5â5 4
(
â5â54 5
id
â5â55 7
)
â5â57 8
;
â5â58 9
}
ã5ã5 	
public
æ5æ5 
void
æ5æ5 
EliminarDocumento
æ5æ5 %
(
æ5æ5% &/
!calidad_gestor_documental_insumos
æ5æ5& G
obj
æ5æ5H K
)
æ5æ5K L
{
ç5ç5 	

DACElimina
è5è5 
.
è5è5 
EliminarDocumento
è5è5 (
(
è5è5( )
obj
è5è5) ,
)
è5è5, -
;
è5è5- .
}
é5é5 	
public
ë5ë5 
List
ë5ë5 
<
ë5ë5 1
#ref_calidad_insumos_tipo_documental
ë5ë5 7
>
ë5ë57 8)
GetListInsumoTipoDocumental
ë5ë59 T
(
ë5ë5T U
)
ë5ë5U V
{
ì5ì5 	
return
í5í5 
DACConsulta
í5í5 
.
í5í5 )
GetListInsumoTipoDocumental
í5í5 :
(
í5í5: ;
)
í5í5; <
;
í5í5< =
}
î5î5 	
public
ğ5ğ5 
List
ğ5ğ5 
<
ğ5ğ5 3
%vw_calidad_quejas_validas_prestadores
ğ5ğ5 9
>
ğ5ğ59 :)
GetPrestadoresQuejasValidas
ğ5ğ5; V
(
ğ5ğ5V W
string
ğ5ğ5W ]
term
ğ5ğ5^ b
,
ğ5ğ5b c
ref
ğ5ğ5d g 
MessageResponseOBJ
ğ5ğ5h z
MsgResğ5ğ5{ 
)ğ5ğ5 ‚
{
ñ5ñ5 	
return
ò5ò5 
DACConsulta
ò5ò5 
.
ò5ò5 )
GetPrestadoresQuejasValidas
ò5ò5 :
(
ò5ò5: ;
term
ò5ò5; ?
,
ò5ò5? @
ref
ò5ò5A D
MsgRes
ò5ò5E K
)
ò5ò5K L
;
ò5ò5L M
}
ó5ó5 	
public
õ5õ5 
List
õ5õ5 
<
õ5õ5 G
9vw_calidad_oportunidad_calidad_rips_indicador_prestadores
õ5õ5 M
>
õ5õ5M N+
GetPrestadoresOportunidadRips
õ5õ5O l
(
õ5õ5l m
string
õ5õ5m s
term
õ5õ5t x
,
õ5õ5x y
ref
õ5õ5z }!
MessageResponseOBJõ5õ5~ 
MsgResõ5õ5‘ —
)õ5õ5— ˜
{
ö5ö5 	
return
÷5÷5 
DACConsulta
÷5÷5 
.
÷5÷5 +
GetPrestadoresOportunidadRips
÷5÷5 <
(
÷5÷5< =
term
÷5÷5= A
,
÷5÷5A B
ref
÷5÷5C F
MsgRes
÷5÷5G M
)
÷5÷5M N
;
÷5÷5N O
}
ø5ø5 	
public
ú5ú5 
List
ú5ú5 
<
ú5ú5 G
9vw_calidad_oportunidad_calidad_rips_indicador_prestadores
ú5ú5 M
>
ú5ú5M N.
 GetCodPrestadoresOportunidadRips
ú5ú5O o
(
ú5ú5o p
string
ú5ú5p v
term
ú5ú5w {
,
ú5ú5{ |
refú5ú5} €"
MessageResponseOBJú5ú5 “
MsgResú5ú5” š
)ú5ú5š ›
{
û5û5 	
return
ü5ü5 
DACConsulta
ü5ü5 
.
ü5ü5 .
 GetCodPrestadoresOportunidadRips
ü5ü5 ?
(
ü5ü5? @
term
ü5ü5@ D
,
ü5ü5D E
ref
ü5ü5F I
MsgRes
ü5ü5J P
)
ü5ü5P Q
;
ü5ü5Q R
}
ı5ı5 	
public
ÿ5ÿ5 
List
ÿ5ÿ5 
<
ÿ5ÿ5 7
)vw_calidad_opor_citas_y_odont_prestadores
ÿ5ÿ5 =
>
ÿ5ÿ5= >4
&GetPrestadoresCitasmedicasyodontologia
ÿ5ÿ5? e
(
ÿ5ÿ5e f
string
ÿ5ÿ5f l
term
ÿ5ÿ5m q
,
ÿ5ÿ5q r
ref
ÿ5ÿ5s v!
MessageResponseOBJÿ5ÿ5w ‰
MsgResÿ5ÿ5Š 
)ÿ5ÿ5 ‘
{
€6€6 	
return
66 
DACConsulta
66 
.
66 4
&GetPrestadoresCitasmedicasyodontologia
66 E
(
66E F
term
66F J
,
66J K
ref
66L O
MsgRes
66P V
)
66V W
;
66W X
}
‚6‚6 	
public
„6„6 
List
„6„6 
<
„6„6 8
*vw_calidad_opor_citas_y_odon_profesionales
„6„6 >
>
„6„6> ?6
(GetProfesionalesCitasmedicasyodontologia
„6„6@ h
(
„6„6h i
string
„6„6i o
term
„6„6p t
,
„6„6t u
ref
„6„6v y!
MessageResponseOBJ„6„6z Œ
MsgRes„6„6 “
)„6„6“ ”
{
…6…6 	
return
†6†6 
DACConsulta
†6†6 
.
†6†6 6
(GetProfesionalesCitasmedicasyodontologia
†6†6 G
(
†6†6G H
term
†6†6H L
,
†6†6L M
ref
†6†6N Q
MsgRes
†6†6R X
)
†6†6X Y
;
†6†6Y Z
}
‡6‡6 	
public
Š6Š6 
List
Š6Š6 
<
Š6Š6 5
'vw_calidad_eventos_adversos_prestadores
Š6Š6 ;
>
Š6Š6; <+
GetprestadoresEventosAdversos
Š6Š6= Z
(
Š6Š6Z [
string
Š6Š6[ a
term
Š6Š6b f
,
Š6Š6f g
ref
Š6Š6h k 
MessageResponseOBJ
Š6Š6l ~
MsgResŠ6Š6 …
)Š6Š6… †
{
‹6‹6 	
return
Œ6Œ6 
DACConsulta
Œ6Œ6 
.
Œ6Œ6 +
GetprestadoresEventosAdversos
Œ6Œ6 <
(
Œ6Œ6< =
term
Œ6Œ6= A
,
Œ6Œ6A B
ref
Œ6Œ6C F
MsgRes
Œ6Œ6G M
)
Œ6Œ6M N
;
Œ6Œ6N O
}
66 	
public
66 
List
66 
<
66 6
(vw_calidad_citas_cumplidas_profesionales
66 <
>
66< =,
GetProfesionalesCitasCumplidas
66> \
(
66\ ]
string
66] c
term
66d h
,
66h i
ref
66j m!
MessageResponseOBJ66n €
MsgRes66 ‡
)66‡ ˆ
{
66 	
return
‘6‘6 
DACConsulta
‘6‘6 
.
‘6‘6 ,
GetProfesionalesCitasCumplidas
‘6‘6 =
(
‘6‘6= >
term
‘6‘6> B
,
‘6‘6B C
ref
‘6‘6D G
MsgRes
‘6‘6H N
)
‘6‘6N O
;
‘6‘6O P
}
’6’6 	
public
”6”6 
List
”6”6 
<
”6”6 A
3management_insumos_capacidad_resolutiva_listaResult
”6”6 G
>
”6”6G H-
ListaInsumosCapacidadResolutiva
”6”6I h
(
”6”6h i
)
”6”6i j
{
•6•6 	
return
–6–6 
DACConsulta
–6–6 
.
–6–6 -
ListaInsumosCapacidadResolutiva
–6–6 >
(
–6–6> ?
)
–6–6? @
;
–6–6@ A
}
—6—6 	
public
™6™6 
bool
™6™6 /
!ValidarExistenciaIndicadorCalidad
™6™6 5
(
™6™65 6
int
™6™66 9
mes
™6™6: =
,
™6™6= >
int
™6™6? B
aÃ±o
™6™6C F
)
™6™6F G
{
š6š6 	
return
›6›6 
DACConsulta
›6›6 
.
›6›6 /
!ValidarExistenciaIndicadorCalidad
›6›6 @
(
›6›6@ A
mes
›6›6A D
,
›6›6D E
aÃ±o
›6›6F I
)
›6›6I J
;
›6›6J K
}
œ6œ6 	
public
66 
void
66 ,
InsertarIndicadoresCalidadDtll
66 2
(
662 3
List
663 7
<
667 8/
!insumos_capacidad_resolutiva_dtll
668 Y
>
66Y Z
List
66[ _
,
66_ `*
insumos_capacidad_resolutiva
66a }
objbase66~ …
,66… †
ref66‡ Š"
MessageResponseOBJ66‹ 
MsgRes66 ¤
)66¤ ¥
{
Ÿ6Ÿ6 	

DACInserta
 6 6 
.
 6 6 ,
InsertarIndicadoresCalidadDtll
 6 6 5
(
 6 65 6
List
 6 66 :
,
 6 6: ;
objbase
 6 6< C
,
 6 6C D
ref
 6 6E H
MsgRes
 6 6I O
)
 6 6O P
;
 6 6P Q
}
¡6¡6 	
public
£6£6 
List
£6£6 
<
£6£6 &
calidad_ref_especialidad
£6£6 ,
>
£6£6, -
GetEspecialidades
£6£6. ?
(
£6£6? @
)
£6£6@ A
{
¤6¤6 	
return
¥6¥6 
DACComonClass
¥6¥6  
.
¥6¥6  !
GetEspecialidades
¥6¥6! 2
(
¥6¥62 3
)
¥6¥63 4
;
¥6¥64 5
}
¦6¦6 	
public
¨6¨6 
int
¨6¨6 -
InsertarBaseBeneficiariosMasivo
¨6¨6 2
(
¨6¨62 3
List
¨6¨63 7
<
¨6¨67 8 
base_beneficiarios
¨6¨68 J
>
¨6¨6J K
List
¨6¨6L P
,
¨6¨6P Q
ref
¨6¨6R U 
MessageResponseOBJ
¨6¨6V h
MsgRes
¨6¨6i o
)
¨6¨6o p
{
©6©6 	
return
ª6ª6 

DACInserta
ª6ª6 
.
ª6ª6 -
InsertarBaseBeneficiariosMasivo
ª6ª6 =
(
ª6ª6= >
List
ª6ª6> B
,
ª6ª6B C
ref
ª6ª6D G
MsgRes
ª6ª6H N
)
ª6ª6N O
;
ª6ª6O P
}
«6«6 	
public
¬6¬6 
int
¬6¬6 *
InsertarLogBaseBeneficiarios
¬6¬6 /
(
¬6¬6/ 0+
log_cargue_base_beneficiarios
¬6¬60 M
obj
¬6¬6N Q
,
¬6¬6Q R
ref
¬6¬6S V 
MessageResponseOBJ
¬6¬6W i
MsgRes
¬6¬6j p
)
¬6¬6p q
{
­6­6 	
return
®6®6 

DACInserta
®6®6 
.
®6®6 *
InsertarLogBaseBeneficiarios
®6®6 :
(
®6®6: ;
obj
®6®6; >
,
®6®6> ?
ref
®6®6@ C
MsgRes
®6®6D J
)
®6®6J K
;
®6®6K L
}
¯6¯6 	
public
±6±6 
void
±6±6 *
EliminarBaseBeneficiariosEco
±6±6 0
(
±6±60 1
ref
±6±61 4 
MessageResponseOBJ
±6±65 G
MsgRes
±6±6H N
)
±6±6N O
{
²6²6 	

DACElimina
³6³6 
.
³6³6 *
EliminarBaseBeneficiariosEco
³6³6 3
(
³6³63 4
ref
³6³64 7
MsgRes
³6³68 >
)
³6³6> ?
;
³6³6? @
}
´6´6 	
public
¶6¶6  
base_beneficiarios
¶6¶6 !+
getUltimoPeriodoBeneficiarios
¶6¶6" ?
(
¶6¶6? @
)
¶6¶6@ A
{
·6·6 	 
base_beneficiarios
¸6¸6 
list
¸6¸6 #
=
¸6¸6$ %
DACConsulta
¸6¸6& 1
.
¸6¸61 2+
getUltimoPeriodoBeneficiarios
¸6¸62 O
(
¸6¸6O P
)
¸6¸6P Q
;
¸6¸6Q R
return
¹6¹6 
list
¹6¹6 
;
¹6¹6 
}
º6º6 	
public
¼6¼6 
List
¼6¼6 
<
¼6¼6 #
ref_adherencia_ciudad
¼6¼6 )
>
¼6¼6) *
	GetCiudad
¼6¼6+ 4
(
¼6¼64 5
)
¼6¼65 6
{
½6½6 	
return
¾6¾6 
DACConsulta
¾6¾6 
.
¾6¾6 
	GetCiudad
¾6¾6 (
(
¾6¾6( )
)
¾6¾6) *
;
¾6¾6* +
}
¿6¿6 	
public
Â6Â6 
int
Â6Â6 
insertarPrestador
Â6Â6 $
(
Â6Â6$ %&
ref_adherencia_prestador
Â6Â6% =
obj
Â6Â6> A
,
Â6Â6A B
List
Â6Â6C G
<
Â6Â6G H(
ref_adherencia_profesional
Â6Â6H b
>
Â6Â6b c
lista
Â6Â6d i
,
Â6Â6i j
int
Â6Â6k n
creado
Â6Â6o u
)
Â6Â6u v
{
Ã6Ã6 	
return
Ä6Ä6 

DACInserta
Ä6Ä6 
.
Ä6Ä6 
insertarPrestador
Ä6Ä6 /
(
Ä6Ä6/ 0
obj
Ä6Ä60 3
,
Ä6Ä63 4
lista
Ä6Ä65 :
,
Ä6Ä6: ;
creado
Ä6Ä6< B
)
Ä6Ä6B C
;
Ä6Ä6C D
}
Å6Å6 	
public
Ç6Ç6 
int
Ç6Ç6 %
insertarPrestadorCiudad
Ç6Ç6 *
(
Ç6Ç6* +-
ref_adherencia_prestador_ciudad
Ç6Ç6+ J
obj
Ç6Ç6K N
)
Ç6Ç6N O
{
È6È6 	
return
É6É6 

DACInserta
É6É6 
.
É6É6 %
insertarPrestadorCiudad
É6É6 5
(
É6É65 6
obj
É6É66 9
)
É6É69 :
;
É6É6: ;
}
Ê6Ê6 	
public
Ì6Ì6 
List
Ì6Ì6 
<
Ì6Ì6 &
ref_adherencia_prestador
Ì6Ì6 ,
>
Ì6Ì6, -
traerPrestadores
Ì6Ì6. >
(
Ì6Ì6> ?
)
Ì6Ì6? @
{
Í6Í6 	
return
Î6Î6 
DACConsulta
Î6Î6 
.
Î6Î6 
traerPrestadores
Î6Î6 /
(
Î6Î6/ 0
)
Î6Î60 1
;
Î6Î61 2
}
Ï6Ï6 	
public
Ñ6Ñ6 
List
Ñ6Ñ6 
<
Ñ6Ñ6 /
!management_traerPrestadoresResult
Ñ6Ñ6 5
>
Ñ6Ñ65 6 
traerPrestadoresId
Ñ6Ñ67 I
(
Ñ6Ñ6I J
string
Ñ6Ñ6J P
id
Ñ6Ñ6Q S
)
Ñ6Ñ6S T
{
Ò6Ò6 	
return
Ó6Ó6 
DACConsulta
Ó6Ó6 
.
Ó6Ó6  
traerPrestadoresId
Ó6Ó6 1
(
Ó6Ó61 2
id
Ó6Ó62 4
)
Ó6Ó64 5
;
Ó6Ó65 6
}
Ô6Ô6 	
public
Ö6Ö6 
List
Ö6Ö6 
<
Ö6Ö6 =
/management_baseBeneficiariosPeriodoValidoResult
Ö6Ö6 C
>
Ö6Ö6C D*
GetBeneficiariosPerodoValido
Ö6Ö6E a
(
Ö6Ö6a b
int
Ö6Ö6b e
mes
Ö6Ö6f i
,
Ö6Ö6i j
int
Ö6Ö6k n
aÃ±o
Ö6Ö6o r
)
Ö6Ö6r s
{
×6×6 	
return
Ø6Ø6 
DACConsulta
Ø6Ø6 
.
Ø6Ø6 *
GetBeneficiariosPerodoValido
Ø6Ø6 ;
(
Ø6Ø6; <
mes
Ø6Ø6< ?
,
Ø6Ø6? @
aÃ±o
Ø6Ø6A D
)
Ø6Ø6D E
;
Ø6Ø6E F
}
Ù6Ù6 	
public
Û6Û6 
List
Û6Û6 
<
Û6Û6 #
ref_adherencia_ciudad
Û6Û6 )
>
Û6Û6) *
getCiudadesUnis
Û6Û6+ :
(
Û6Û6: ;
int
Û6Û6; >
idUnis
Û6Û6? E
)
Û6Û6E F
{
Ü6Ü6 	
return
İ6İ6 
DACConsulta
İ6İ6 
.
İ6İ6 
getCiudadesUnis
İ6İ6 .
(
İ6İ6. /
idUnis
İ6İ6/ 5
)
İ6İ65 6
;
İ6İ66 7
}
Ş6Ş6 	
public
ä6ä6 
List
ä6ä6 
<
ä6ä6 "
ref_ver_tipoCriterio
ä6ä6 (
>
ä6ä6( )!
Get_refTipoCriterio
ä6ä6* =
(
ä6ä6= >
)
ä6ä6> ?
{
å6å6 	
return
æ6æ6 
DACConsulta
æ6æ6 
.
æ6æ6 !
Get_refTipoCriterio
æ6æ6 2
(
æ6æ62 3
)
æ6æ63 4
;
æ6æ64 5
}
ç6ç6 	
public
è6è6 
List
è6è6 
<
è6è6 +
ref_verificacion_farmaceutico
è6è6 1
>
è6è61 2-
Get_refVerificacionFarmaceutita
è6è63 R
(
è6è6R S
)
è6è6S T
{
é6é6 	
return
ê6ê6 
DACConsulta
ê6ê6 
.
ê6ê6 -
Get_refVerificacionFarmaceutita
ê6ê6 >
(
ê6ê6> ?
)
ê6ê6? @
;
ê6ê6@ A
}
ë6ë6 	
public
í6í6 
List
í6í6 
<
í6í6 0
"management_verificacionListaResult
í6í6 6
>
í6í66 7
getTipoCriterioId
í6í68 I
(
í6í6I J
int
í6í6J M
idTipo
í6í6N T
)
í6í6T U
{
î6î6 	
return
ï6ï6 
DACConsulta
ï6ï6 
.
ï6ï6 
getTipoCriterioId
ï6ï6 0
(
ï6ï60 1
idTipo
ï6ï61 7
)
ï6ï67 8
;
ï6ï68 9
}
ğ6ğ6 	
public
ñ6ñ6 
List
ñ6ñ6 
<
ñ6ñ6 0
"management_verificacionListaResult
ñ6ñ6 6
>
ñ6ñ66 7!
getTotalDatosDispen
ñ6ñ68 K
(
ñ6ñ6K L
)
ñ6ñ6L M
{
ò6ò6 	
return
ó6ó6 
DACConsulta
ó6ó6 
.
ó6ó6 !
getTotalDatosDispen
ó6ó6 2
(
ó6ó62 3
)
ó6ó63 4
;
ó6ó64 5
}
ô6ô6 	
public
ö6ö6 +
ref_verificacion_farmaceutico
ö6ö6 ,1
#Get_refVerificacionFarmaceutitaById
ö6ö6- P
(
ö6ö6P Q
int
ö6ö6Q T
	idTipoVer
ö6ö6U ^
)
ö6ö6^ _
{
÷6÷6 	
return
ø6ø6 
DACConsulta
ø6ø6 
.
ø6ø6 1
#Get_refVerificacionFarmaceutitaById
ø6ø6 B
(
ø6ø6B C
	idTipoVer
ø6ø6C L
)
ø6ø6L M
;
ø6ø6M N
}
ù6ù6 	
public
û6û6 
void
û6û6 "
InsertarVerificacion
û6û6 (
(
û6û6( )+
ref_verificacion_farmaceutico
û6û6) F
obj
û6û6G J
,
û6û6J K
ref
û6û6L O 
MessageResponseOBJ
û6û6P b
MsgRes
û6û6c i
)
û6û6i j
{
ü6ü6 	

DACInserta
ı6ı6 
.
ı6ı6 "
InsertarVerificacion
ı6ı6 +
(
ı6ı6+ ,
obj
ı6ı6, /
,
ı6ı6/ 0
ref
ı6ı61 4
MsgRes
ı6ı65 ;
)
ı6ı6; <
;
ı6ı6< =
}
ş6ş6 	
public
ÿ6ÿ6 
void
ÿ6ÿ6 $
ActualizarVerificacion
ÿ6ÿ6 *
(
ÿ6ÿ6* ++
ref_verificacion_farmaceutico
ÿ6ÿ6+ H
obj
ÿ6ÿ6I L
,
ÿ6ÿ6L M
ref
ÿ6ÿ6N Q 
MessageResponseOBJ
ÿ6ÿ6R d
MsgRes
ÿ6ÿ6e k
)
ÿ6ÿ6k l
{
€7€7 	
DACActualiza
77 
.
77 $
ActualizarVerificacion
77 /
(
77/ 0
obj
770 3
,
773 4
ref
775 8
MsgRes
779 ?
)
77? @
;
77@ A
}
‚7‚7 	
public
„7„7 
void
„7„7 %
InsertarTipoCriteriover
„7„7 +
(
„7„7+ ,"
ref_ver_tipoCriterio
„7„7, @
obj
„7„7A D
,
„7„7D E
ref
„7„7F I 
MessageResponseOBJ
„7„7J \
MsgRes
„7„7] c
)
„7„7c d
{
…7…7 	

DACInserta
†7†7 
.
†7†7 %
InsertarTipoCriteriover
†7†7 .
(
†7†7. /
obj
†7†7/ 2
,
†7†72 3
ref
†7†74 7
MsgRes
†7†78 >
)
†7†7> ?
;
†7†7? @
}
‡7‡7 	
public
‰7‰7 
void
‰7‰7 '
ActualizarTipoCriteriover
‰7‰7 -
(
‰7‰7- ."
ref_ver_tipoCriterio
‰7‰7. B
obj
‰7‰7C F
,
‰7‰7F G
ref
‰7‰7H K 
MessageResponseOBJ
‰7‰7L ^
MsgRes
‰7‰7_ e
)
‰7‰7e f
{
Š7Š7 	
DACActualiza
‹7‹7 
.
‹7‹7 '
ActualizarTipoCriteriover
‹7‹7 2
(
‹7‹72 3
obj
‹7‹73 6
,
‹7‹76 7
ref
‹7‹78 ;
MsgRes
‹7‹7< B
)
‹7‹7B C
;
‹7‹7C D
}
Œ7Œ7 	
public
77 
List
77 
<
77 
ver_tipocriterio
77 $
>
77$ %"
get_ref_tipoCriterio
77& :
(
77: ;
int
77; >
idVerificacion
77? M
)
77M N
{
77 	
return
‘7‘7 
DACConsulta
‘7‘7 
.
‘7‘7 "
get_ref_tipoCriterio
‘7‘7 3
(
‘7‘73 4
idVerificacion
‘7‘74 B
)
‘7‘7B C
;
‘7‘7C D
}
’7’7 	
public
”7”7 
List
”7”7 
<
”7”7 '
ref_ver_grupo_tpocriterio
”7”7 -
>
”7”7- .&
get_ver_grupoTipoCritero
”7”7/ G
(
”7”7G H
)
”7”7H I
{
•7•7 	
return
–7–7 
DACConsulta
–7–7 
.
–7–7 &
get_ver_grupoTipoCritero
–7–7 7
(
–7–77 8
)
–7–78 9
;
–7–79 :
}
—7—7 	
public
™7™7 
void
™7™7 '
InsertarAdminCriteriosver
™7™7 -
(
™7™7- .
int
™7™7. 1
tipoVerificacion
™7™72 B
,
™7™7B C
List
™7™7D H
<
™7™7H I
int
™7™7I L
>
™7™7L M
seleccionados
™7™7N [
,
™7™7[ \
List
™7™7] a
<
™7™7a b
int
™7™7b e
>
™7™7e f
seleccionados2
™7™7g u
,
™7™7u v
string
™7™7w }
usuario™7™7~ …
,™7™7… †
ref™7™7‡ Š"
MessageResponseOBJ™7™7‹ 
MsgRes™7™7 ¤
)™7™7¤ ¥
{
š7š7 	

DACInserta
›7›7 
.
›7›7 '
InsertarAdminCriteriosver
›7›7 0
(
›7›70 1
tipoVerificacion
›7›71 A
,
›7›7A B
seleccionados
›7›7C P
,
›7›7P Q
seleccionados2
›7›7R `
,
›7›7` a
usuario
›7›7b i
,
›7›7i j
ref
›7›7k n
MsgRes
›7›7o u
)
›7›7u v
;
›7›7v w
}
œ7œ7 	
public
77 
void
77 %
EliminarTipoCriteriover
77 +
(
77+ ,
int
77, /
idtipocriterio
770 >
,
77> ?
ref
77@ C 
MessageResponseOBJ
77D V
MsgRes
77W ]
)
77] ^
{
Ÿ7Ÿ7 	

DACElimina
 7 7 
.
 7 7 %
EliminarTipoCriteriover
 7 7 .
(
 7 7. /
idtipocriterio
 7 7/ =
,
 7 7= >
ref
 7 7? B
MsgRes
 7 7C I
)
 7 7I J
;
 7 7J K
}
¡7¡7 	
public
£7£7 
List
£7£7 
<
£7£7 
ver_criterio
£7£7  
>
£7£7  !,
getcriteriosbytipoverificacion
£7£7" @
(
£7£7@ A
int
£7£7A D
tipoverificacion
£7£7E U
)
£7£7U V
{
¤7¤7 	
return
¥7¥7 
DACConsulta
¥7¥7 
.
¥7¥7 ,
getcriteriosbytipoverificacion
¥7¥7 =
(
¥7¥7= >
tipoverificacion
¥7¥7> N
)
¥7¥7N O
;
¥7¥7O P
}
¦7¦7 	
public
¨7¨7 
ver_criterio
¨7¨7 $
ConsultarCriterioById2
¨7¨7 2
(
¨7¨72 3
int
¨7¨73 6

idcriterio
¨7¨77 A
)
¨7¨7A B
{
©7©7 	
return
ª7ª7 
DACConsulta
ª7ª7 
.
ª7ª7 $
ConsultarCriterioById2
ª7ª7 5
(
ª7ª75 6

idcriterio
ª7ª76 @
)
ª7ª7@ A
;
ª7ª7A B
}
«7«7 	
public
­7­7 
void
­7­7 !
InsertarCriteriover
­7­7 '
(
­7­7' (
ver_criterio
­7­7( 4
criterio
­7­75 =
,
­7­7= >
ref
­7­7? B 
MessageResponseOBJ
­7­7C U
MsgRes
­7­7V \
)
­7­7\ ]
{
®7®7 	

DACInserta
¯7¯7 
.
¯7¯7 !
InsertarCriteriover
¯7¯7 *
(
¯7¯7* +
criterio
¯7¯7+ 3
,
¯7¯73 4
ref
¯7¯75 8
MsgRes
¯7¯79 ?
)
¯7¯7? @
;
¯7¯7@ A
}
°7°7 	
public
²7²7 
void
²7²7 #
ActualizarCriteriover
²7²7 )
(
²7²7) *
ver_criterio
²7²7* 6
criterio
²7²77 ?
,
²7²7? @
ref
²7²7A D 
MessageResponseOBJ
²7²7E W
MsgRes
²7²7X ^
)
²7²7^ _
{
³7³7 	
DACActualiza
´7´7 
.
´7´7 #
ActualizarCriteriover
´7´7 .
(
´7´7. /
criterio
´7´7/ 7
,
´7´77 8
ref
´7´79 <
MsgRes
´7´7= C
)
´7´7C D
;
´7´7D E
}
µ7µ7 	
public
·7·7 
List
·7·7 
<
·7·7 0
"ref_verificacionFarmaceutica_tipos
·7·7 6
>
·7·76 7"
getTiposVerificacion
·7·78 L
(
·7·7L M
)
·7·7M N
{
¸7¸7 	
return
¹7¹7 
DACConsulta
¹7¹7 
.
¹7¹7 "
getTiposVerificacion
¹7¹7 3
(
¹7¹73 4
)
¹7¹74 5
;
¹7¹75 6
}
º7º7 	
public
¼7¼7 
void
¼7¼7 *
EliminarCriterioVerificacion
¼7¼7 0
(
¼7¼70 1
int
¼7¼71 4

idcriterio
¼7¼75 ?
,
¼7¼7? @
ref
¼7¼7A D 
MessageResponseOBJ
¼7¼7E W
MsgRes
¼7¼7X ^
)
¼7¼7^ _
{
½7½7 	

DACElimina
¾7¾7 
.
¾7¾7 *
EliminarCriterioVerificacion
¾7¾7 3
(
¾7¾73 4

idcriterio
¾7¾74 >
,
¾7¾7> ?
ref
¾7¾7@ C
MsgRes
¾7¾7D J
)
¾7¾7J K
;
¾7¾7K L
}
¿7¿7 	
public
Á7Á7 
void
Á7Á7 -
InsertarCarguePuntoDispensacion
Á7Á7 3
(
Á7Á73 4
List
Á7Á74 8
<
Á7Á78 9#
ver_puntoDispensacion
Á7Á79 N
>
Á7Á7N O
List
Á7Á7P T
,
Á7Á7T U
ref
Á7Á7V Y 
MessageResponseOBJ
Á7Á7Z l
MsgRes
Á7Á7m s
)
Á7Á7s t
{
Â7Â7 	

DACInserta
Ã7Ã7 
.
Ã7Ã7 -
InsertarCarguePuntoDispensacion
Ã7Ã7 6
(
Ã7Ã76 7
List
Ã7Ã77 ;
,
Ã7Ã7; <
ref
Ã7Ã7= @
MsgRes
Ã7Ã7A G
)
Ã7Ã7G H
;
Ã7Ã7H I
}
Ä7Ä7 	
public
Æ7Æ7 
List
Æ7Æ7 
<
Æ7Æ7 #
ver_puntoDispensacion
Æ7Æ7 )
>
Æ7Æ7) *&
getPuntoDispensacionList
Æ7Æ7+ C
(
Æ7Æ7C D
)
Æ7Æ7D E
{
Ç7Ç7 	
return
È7È7 
DACConsulta
È7È7 
.
È7È7 &
getPuntoDispensacionList
È7È7 7
(
È7È77 8
)
È7È78 9
;
È7È79 :
}
É7É7 	
public
Ê7Ê7 
List
Ê7Ê7 
<
Ê7Ê7 ?
1management_dispensacion_archivosRepositorioResult
Ê7Ê7 E
>
Ê7Ê7E F0
"MostrarArchivosEvaluacionVisitasMD
Ê7Ê7G i
(
Ê7Ê7i j
int
Ê7Ê7j m
?
Ê7Ê7m n
idEvaluacion
Ê7Ê7o {
)
Ê7Ê7{ |
{
Ë7Ë7 	
return
Ì7Ì7 
DACConsulta
Ì7Ì7 
.
Ì7Ì7 0
"MostrarArchivosEvaluacionVisitasMD
Ì7Ì7 A
(
Ì7Ì7A B
idEvaluacion
Ì7Ì7B N
)
Ì7Ì7N O
;
Ì7Ì7O P
}
Í7Í7 	
public
Î7Î7 
int
Î7Î7 )
ActualizarPuntoDispensacion
Î7Î7 .
(
Î7Î7. /#
ver_puntoDispensacion
Î7Î7/ D
obj
Î7Î7E H
)
Î7Î7H I
{
Ï7Ï7 	
return
Ğ7Ğ7 
DACActualiza
Ğ7Ğ7 
.
Ğ7Ğ7  )
ActualizarPuntoDispensacion
Ğ7Ğ7  ;
(
Ğ7Ğ7; <
obj
Ğ7Ğ7< ?
)
Ğ7Ğ7? @
;
Ğ7Ğ7@ A
}
Ñ7Ñ7 	
public
Ò7Ò7 
int
Ò7Ò7 3
%ActualizarAuditadoVisitasDispensacion
Ò7Ò7 8
(
Ò7Ò78 9#
ver_puntoDispensacion
Ò7Ò79 N
obj
Ò7Ò7O R
)
Ò7Ò7R S
{
Ó7Ó7 	
return
Ô7Ô7 
DACActualiza
Ô7Ô7 
.
Ô7Ô7  3
%ActualizarAuditadoVisitasDispensacion
Ô7Ô7  E
(
Ô7Ô7E F
obj
Ô7Ô7F I
)
Ô7Ô7I J
;
Ô7Ô7J K
}
Õ7Õ7 	
public
Ö7Ö7 
List
Ö7Ö7 
<
Ö7Ö7 %
ver_evaluacion_archivos
Ö7Ö7 +
>
Ö7Ö7+ ,.
 TraerArchivosVisitasDispensacion
Ö7Ö7- M
(
Ö7Ö7M N
int
Ö7Ö7N Q
idEvaluacion
Ö7Ö7R ^
)
Ö7Ö7^ _
{
×7×7 	
return
Ø7Ø7 
DACConsulta
Ø7Ø7 
.
Ø7Ø7 .
 TraerArchivosVisitasDispensacion
Ø7Ø7 ?
(
Ø7Ø7? @
idEvaluacion
Ø7Ø7@ L
)
Ø7Ø7L M
;
Ø7Ø7M N
}
Ù7Ù7 	
public
Ú7Ú7 
int
Ú7Ú7 ,
InsertarEvaluacionDispensacion
Ú7Ú7 1
(
Ú7Ú71 2#
ver_dispen_evaluacion
Ú7Ú72 G
obj
Ú7Ú7H K
)
Ú7Ú7K L
{
Û7Û7 	
return
Ü7Ü7 

DACInserta
Ü7Ü7 
.
Ü7Ü7 ,
InsertarEvaluacionDispensacion
Ü7Ü7 <
(
Ü7Ü7< =
obj
Ü7Ü7= @
)
Ü7Ü7@ A
;
Ü7Ü7A B
}
İ7İ7 	
public
ß7ß7 
int
ß7ß7 1
#InsertarEvaluacionDispensacionTotal
ß7ß7 6
(
ß7ß76 7
List
ß7ß77 ;
<
ß7ß7; <)
ver_dispen_evaluacion_total
ß7ß7< W
>
ß7ß7W X
List
ß7ß7Y ]
)
ß7ß7] ^
{
à7à7 	
return
á7á7 

DACInserta
á7á7 
.
á7á7 1
#InsertarEvaluacionDispensacionTotal
á7á7 A
(
á7á7A B
List
á7á7B F
)
á7á7F G
;
á7á7G H
}
â7â7 	
public
ã7ã7 
List
ã7ã7 
<
ã7ã7 >
0management_dispensacion_evaluacionRelacionResult
ã7ã7 D
>
ã7ã7D E'
getDispensacionEvaluacion
ã7ã7F _
(
ã7ã7_ `
)
ã7ã7` a
{
ä7ä7 	
return
å7å7 
DACConsulta
å7å7 
.
å7å7 (
getDispensacionEvaluacionl
å7å7 9
(
å7å79 :
)
å7å7: ;
;
å7å7; <
}
æ7æ7 	
public
è7è7 
List
è7è7 
<
è7è7 D
6management_dispensacion_evaluacionRelacion_totalResult
è7è7 J
>
è7è7J K,
getDispensacionEvaluacionTotal
è7è7L j
(
è7è7j k
)
è7è7k l
{
é7é7 	
return
ê7ê7 
DACConsulta
ê7ê7 
.
ê7ê7 ,
getDispensacionEvaluacionTotal
ê7ê7 =
(
ê7ê7= >
)
ê7ê7> ?
;
ê7ê7? @
}
ë7ë7 	
public
í7í7 
List
í7í7 
<
í7í7 >
0management_dispensacion_evaluacionRelacionResult
í7í7 D
>
í7í7D E)
getDispensacionEvaluacionId
í7í7F a
(
í7í7a b
int
í7í7b e
Id
í7í7f h
)
í7í7h i
{
î7î7 	
return
ï7ï7 
DACConsulta
ï7ï7 
.
ï7ï7 )
getDispensacionEvaluacionId
ï7ï7 :
(
ï7ï7: ;
Id
ï7ï7; =
)
ï7ï7= >
;
ï7ï7> ?
}
ğ7ğ7 	
public
ò7ò7 
List
ò7ò7 
<
ò7ò7 D
6management_dispensacion_evaluacionRelacion_totalResult
ò7ò7 J
>
ò7ò7J K.
 getDispensacionEvaluacionTotalId
ò7ò7L l
(
ò7ò7l m
int
ò7ò7m p
id
ò7ò7q s
)
ò7ò7s t
{
ó7ó7 	
return
ô7ô7 
DACConsulta
ô7ô7 
.
ô7ô7 .
 getDispensacionEvaluacionTotalId
ô7ô7 ?
(
ô7ô7? @
id
ô7ô7@ B
)
ô7ô7B C
;
ô7ô7C D
}
õ7õ7 	
public
ø7ø7 
int
ø7ø7 (
InsertarArchivosEvaluacion
ø7ø7 -
(
ø7ø7- .%
ver_evaluacion_archivos
ø7ø7. E
obj
ø7ø7F I
)
ø7ø7I J
{
ù7ù7 	
return
ú7ú7 

DACInserta
ú7ú7 
.
ú7ú7 (
InsertarArchivosEvaluacion
ú7ú7 8
(
ú7ú78 9
obj
ú7ú79 <
)
ú7ú7< =
;
ú7ú7= >
}
û7û7 	
public
ü7ü7 
int
ü7ü7 ,
InsertarArchivosEvaluacionPDFS
ü7ü7 1
(
ü7ü71 2!
ver_evaluacion_pdfs
ü7ü72 E
obj
ü7ü7F I
)
ü7ü7I J
{
ı7ı7 	
return
ş7ş7 

DACInserta
ş7ş7 
.
ş7ş7 ,
InsertarArchivosEvaluacionPDFS
ş7ş7 <
(
ş7ş7< =
obj
ş7ş7= @
)
ş7ş7@ A
;
ş7ş7A B
}
ÿ7ÿ7 	
public
€8€8 !
ver_evaluacion_pdfs
€8€8 "'
TraerPDFEvaluacionVisitas
€8€8# <
(
€8€8< =
int
€8€8= @
idEvaluacion
€8€8A M
)
€8€8M N
{
88 	
return
‚8‚8 
DACConsulta
‚8‚8 
.
‚8‚8 '
TraerPDFEvaluacionVisitas
‚8‚8 8
(
‚8‚88 9
idEvaluacion
‚8‚89 E
)
‚8‚8E F
;
‚8‚8F G
}
ƒ8ƒ8 	
public
„8„8 
int
„8„8 7
)EliminarArchivosPDFevaluacionDispensacion
„8„8 <
(
„8„8< =
int
„8„8= @
idEvaluacion
„8„8A M
)
„8„8M N
{
…8…8 	
return
†8†8 

DACElimina
†8†8 
.
†8†8 7
)EliminarArchivosPDFevaluacionDispensacion
†8†8 G
(
†8†8G H
idEvaluacion
†8†8H T
)
†8†8T U
;
†8†8U V
}
‡8‡8 	
public
ˆ8ˆ8 
int
ˆ8ˆ8 /
!EliminarArchivosEvaluacionVisitas
ˆ8ˆ8 4
(
ˆ8ˆ84 5
int
ˆ8ˆ85 8
idEvaluacion
ˆ8ˆ89 E
,
ˆ8ˆ8E F
int
ˆ8ˆ8G J
	idArchivo
ˆ8ˆ8K T
)
ˆ8ˆ8T U
{
‰8‰8 	
return
Š8Š8 

DACElimina
Š8Š8 
.
Š8Š8 /
!EliminarArchivosEvaluacionVisitas
Š8Š8 ?
(
Š8Š8? @
idEvaluacion
Š8Š8@ L
,
Š8Š8L M
	idArchivo
Š8Š8N W
)
Š8Š8W X
;
Š8Š8X Y
}
‹8‹8 	
public
Œ8Œ8 %
ver_evaluacion_archivos
Œ8Œ8 &/
!DescargarArchivoEvaluacionVisitas
Œ8Œ8' H
(
Œ8Œ8H I
int
Œ8Œ8I L
	idArchivo
Œ8Œ8M V
)
Œ8Œ8V W
{
88 	
return
88 
DACConsulta
88 
.
88 /
!DescargarArchivoEvaluacionVisitas
88 @
(
88@ A
	idArchivo
88A J
)
88J K
;
88K L
}
88 	
public
88 
List
88 
<
88 G
9management_dispensacion_evaluacionRelacion_hallazgoResult
88 M
>
88M N#
getEvolucionHallazgos
88O d
(
88d e
)
88e f
{
‘8‘8 	
return
’8’8 
DACConsulta
’8’8 
.
’8’8 #
getEvolucionHallazgos
’8’8 4
(
’8’84 5
)
’8’85 6
;
’8’86 7
}
“8“8 	
public
•8•8 
List
•8•8 
<
•8•8 M
?management_dispensacion_evaluacionRelacion_total_hallazgoResult
•8•8 S
>
•8•8S T8
*getDispensacionEvaluacionTotalIdHallazgoId
•8•8U 
(•8•8 €
int•8•8€ ƒ
id•8•8„ †
)•8•8† ‡
{
–8–8 	
return
—8—8 
DACConsulta
—8—8 
.
—8—8 8
*getDispensacionEvaluacionTotalIdHallazgoId
—8—8 I
(
—8—8I J
id
—8—8J L
)
—8—8L M
;
—8—8M N
}
˜8˜8 	
public
›8›8 
List
›8›8 
<
›8›8 (
ref_evaluacion_estadoTotal
›8›8 .
>
›8›8. /+
getEstadosEvaluacionHallazgos
›8›80 M
(
›8›8M N
)
›8›8N O
{
œ8œ8 	
return
88 
DACConsulta
88 
.
88 +
getEstadosEvaluacionHallazgos
88 <
(
88< =
)
88= >
;
88> ?
}
88 	
public
 8 8 
List
 8 8 
<
 8 8 )
ref_evaluacion_tipoHallazgo
 8 8 /
>
 8 8/ 0'
getTipoHallazgoEvaluacion
 8 81 J
(
 8 8J K
)
 8 8K L
{
¡8¡8 	
return
¢8¢8 
DACConsulta
¢8¢8 
.
¢8¢8 '
getTipoHallazgoEvaluacion
¢8¢8 8
(
¢8¢88 9
)
¢8¢89 :
;
¢8¢8: ;
}
£8£8 	
public
¥8¥8 
List
¥8¥8 
<
¥8¥8 )
ref_evaluacion_cumplimiento
¥8¥8 /
>
¥8¥8/ 0'
getCumplimientoEvaluacion
¥8¥81 J
(
¥8¥8J K
)
¥8¥8K L
{
¦8¦8 	
return
§8§8 
DACConsulta
§8§8 
.
§8§8 '
getCumplimientoEvaluacion
§8§8 8
(
§8§88 9
)
§8§89 :
;
§8§8: ;
}
¨8¨8 	
public
©8©8 
List
©8©8 
<
©8©8 (
ref_evaluacion_tipoSoporte
©8©8 .
>
©8©8. /&
getTipoSoporteEvaluacion
©8©80 H
(
©8©8H I
)
©8©8I J
{
ª8ª8 	
return
«8«8 
DACConsulta
«8«8 
.
«8«8 &
getTipoSoporteEvaluacion
«8«8 7
(
«8«87 8
)
«8«88 9
;
«8«89 :
}
¬8¬8 	
public
®8®8 
int
®8®8 ,
insertarHallazgoItemEvaluacion
®8®8 1
(
®8®81 2%
ver_evaluacion_hallazgo
®8®82 I
obj
®8®8J M
)
®8®8M N
{
¯8¯8 	
return
°8°8 

DACInserta
°8°8 
.
°8°8 ,
insertarHallazgoItemEvaluacion
°8°8 <
(
°8°8< =
obj
°8°8= @
)
°8°8@ A
;
°8°8A B
}
±8±8 	
public
³8³8 
List
³8³8 
<
³8³8 %
ver_evaluacion_hallazgo
³8³8 +
>
³8³8+ ,%
ExisteHallazgoSubsanado
³8³8- D
(
³8³8D E
int
³8³8E H
idTotal
³8³8I P
,
³8³8P Q
int
³8³8R U
id_tipoCriterio
³8³8V e
)
³8³8e f
{
´8´8 	
return
µ8µ8 
DACConsulta
µ8µ8 
.
µ8µ8 %
ExisteHallazgoSubsanado
µ8µ8 6
(
µ8µ86 7
idTotal
µ8µ87 >
,
µ8µ8> ?
id_tipoCriterio
µ8µ8@ O
)
µ8µ8O P
;
µ8µ8P Q
}
¶8¶8 	
public
¸8¸8 
int
¸8¸8 '
ActualizarHallazgoVisitas
¸8¸8 ,
(
¸8¸8, -%
ver_evaluacion_hallazgo
¸8¸8- D
obj
¸8¸8E H
)
¸8¸8H I
{
¹8¹8 	
return
º8º8 
DACActualiza
º8º8 
.
º8º8  '
ActualizarHallazgoVisitas
º8º8  9
(
º8º89 :
obj
º8º8: =
)
º8º8= >
;
º8º8> ?
}
»8»8 	
public
½8½8 
int
½8½8 4
&insertarHallazgoItemEvaluacionArchivos
½8½8 9
(
½8½89 :.
 ver_evaluacion_hallazgo_archivos
½8½8: Z
obj
½8½8[ ^
)
½8½8^ _
{
¾8¾8 	
return
¿8¿8 

DACInserta
¿8¿8 
.
¿8¿8 4
&insertarHallazgoItemEvaluacionArchivos
¿8¿8 D
(
¿8¿8D E
obj
¿8¿8E H
)
¿8¿8H I
;
¿8¿8I J
}
À8À8 	
public
Â8Â8 
List
Â8Â8 
<
Â8Â8 M
?management_dispensacion_evaluacionRelacion_total_hallazgoResult
Â8Â8 S
>
Â8Â8S T4
&getDispensacionEvaluacionTotalHallazgo
Â8Â8U {
(
Â8Â8{ |
)
Â8Â8| }
{
Ã8Ã8 	
return
Ä8Ä8 
DACConsulta
Ä8Ä8 
.
Ä8Ä8 4
&getDispensacionEvaluacionTotalHallazgo
Ä8Ä8 E
(
Ä8Ä8E F
)
Ä8Ä8F G
;
Ä8Ä8G H
}
Å8Å8 	
public
Ë8Ë8 
int
Ë8Ë8 $
SaveCuidadosPaliativos
Ë8Ë8 )
(
Ë8Ë8) *&
ffmm_cuidados_paliativos
Ë8Ë8* B
objeto
Ë8Ë8C I
,
Ë8Ë8I J
ref
Ë8Ë8K N 
MessageResponseOBJ
Ë8Ë8O a
MsgRes
Ë8Ë8b h
)
Ë8Ë8h i
{
Ì8Ì8 	
return
Í8Í8 

DACInserta
Í8Í8 
.
Í8Í8 $
SaveCuidadosPaliativos
Í8Í8 4
(
Í8Í84 5
objeto
Í8Í85 ;
,
Í8Í8; <
ref
Í8Í8= @
MsgRes
Í8Í8A G
)
Í8Í8G H
;
Í8Í8H I
}
Î8Î8 	
public
Ñ8Ñ8 
List
Ñ8Ñ8 
<
Ñ8Ñ8 &
Ref_ffmm_unidad_satelite
Ñ8Ñ8 ,
>
Ñ8Ñ8, -
GetUnidadSatelite
Ñ8Ñ8. ?
(
Ñ8Ñ8? @
ref
Ñ8Ñ8@ C 
MessageResponseOBJ
Ñ8Ñ8D V
MsgRes
Ñ8Ñ8W ]
)
Ñ8Ñ8] ^
{
Ò8Ò8 	
return
Ó8Ó8 
DACConsulta
Ó8Ó8 
.
Ó8Ó8 
GetUnidadSatelite
Ó8Ó8 0
(
Ó8Ó80 1
ref
Ó8Ó81 4
MsgRes
Ó8Ó85 ;
)
Ó8Ó8; <
;
Ó8Ó8< =
}
Ô8Ô8 	
public
Õ8Õ8 
List
Õ8Õ8 
<
Õ8Õ8  
Ref_ffmm_unidad_cp
Õ8Õ8 &
>
Õ8Õ8& '
	GetUnidad
Õ8Õ8( 1
(
Õ8Õ81 2
ref
Õ8Õ82 5 
MessageResponseOBJ
Õ8Õ86 H
MsgRes
Õ8Õ8I O
)
Õ8Õ8O P
{
Ö8Ö8 	
return
×8×8 
DACConsulta
×8×8 
.
×8×8 
	GetUnidad
×8×8 (
(
×8×8( )
ref
×8×8) ,
MsgRes
×8×8- 3
)
×8×83 4
;
×8×84 5
}
Ø8Ø8 	
public
Ù8Ù8 
List
Ù8Ù8 
<
Ù8Ù8 "
vw_ffmm_departamento
Ù8Ù8 (
>
Ù8Ù8( )
GetDepartamentos
Ù8Ù8* :
(
Ù8Ù8: ;
ref
Ù8Ù8; > 
MessageResponseOBJ
Ù8Ù8? Q
MsgRes
Ù8Ù8R X
)
Ù8Ù8X Y
{
Ú8Ú8 	
return
Û8Û8 
DACConsulta
Û8Û8 
.
Û8Û8 
GetDepartamentos
Û8Û8 /
(
Û8Û8/ 0
ref
Û8Û80 3
MsgRes
Û8Û84 :
)
Û8Û8: ;
;
Û8Û8; <
}
Ü8Ü8 	
public
İ8İ8 
List
İ8İ8 
<
İ8İ8 
vw_ffmm_municipio
İ8İ8 %
>
İ8İ8% &
GetMunicipios
İ8İ8' 4
(
İ8İ84 5
ref
İ8İ85 8 
MessageResponseOBJ
İ8İ89 K
MsgRes
İ8İ8L R
)
İ8İ8R S
{
Ş8Ş8 	
return
ß8ß8 
DACConsulta
ß8ß8 
.
ß8ß8 
GetMunicipios
ß8ß8 ,
(
ß8ß8, -
ref
ß8ß8- 0
MsgRes
ß8ß81 7
)
ß8ß87 8
;
ß8ß88 9
}
à8à8 	
public
á8á8 
List
á8á8 
<
á8á8 
vw_ffmm_municipio
á8á8 %
>
á8á8% &
GetMunicipiosFM
á8á8' 6
(
á8á86 7
int
á8á87 :
idDepartamento
á8á8; I
,
á8á8I J
ref
á8á8K N 
MessageResponseOBJ
á8á8O a
MsgRes
á8á8b h
)
á8á8h i
{
â8â8 	
return
ã8ã8 
DACConsulta
ã8ã8 
.
ã8ã8 
GetMunicipiosFM
ã8ã8 .
(
ã8ã8. /
idDepartamento
ã8ã8/ =
,
ã8ã8= >
ref
ã8ã8? B
MsgRes
ã8ã8C I
)
ã8ã8I J
;
ã8ã8J K
}
ä8ä8 	
public
å8å8 
List
å8å8 
<
å8å8 %
Ref_ffmm_tipo_visita_cp
å8å8 +
>
å8å8+ ,
GetTipoVisita
å8å8- :
(
å8å8: ;
ref
å8å8; > 
MessageResponseOBJ
å8å8? Q
MsgRes
å8å8R X
)
å8å8X Y
{
æ8æ8 	
return
ç8ç8 
DACConsulta
ç8ç8 
.
ç8ç8 
GetTipoVisita
ç8ç8 ,
(
ç8ç8, -
ref
ç8ç8- 0
MsgRes
ç8ç81 7
)
ç8ç87 8
;
ç8ç88 9
}
è8è8 	
public
é8é8 
List
é8é8 
<
é8é8 
vw_ffmm_ips
é8é8 
>
é8é8  
GetIPS
é8é8! '
(
é8é8' (
ref
é8é8( + 
MessageResponseOBJ
é8é8, >
MsgRes
é8é8? E
)
é8é8E F
{
ê8ê8 	
return
ë8ë8 
DACConsulta
ë8ë8 
.
ë8ë8 
GetIPS
ë8ë8 %
(
ë8ë8% &
ref
ë8ë8& )
MsgRes
ë8ë8* 0
)
ë8ë80 1
;
ë8ë81 2
}
ì8ì8 	
public
í8í8 
List
í8í8 
<
í8í8 !
Ref_tipo_documental
í8í8 '
>
í8í8' (#
GetTipoIdentificacion
í8í8) >
(
í8í8> ?
ref
í8í8? B 
MessageResponseOBJ
í8í8C U
MsgRes
í8í8V \
)
í8í8\ ]
{
î8î8 	
return
ï8ï8 
DACConsulta
ï8ï8 
.
ï8ï8 #
GetTipoIdentificacion
ï8ï8 4
(
ï8ï84 5
ref
ï8ï85 8
MsgRes
ï8ï89 ?
)
ï8ï8? @
;
ï8ï8@ A
}
ğ8ğ8 	
public
ñ8ñ8 
ref_solucionador
ñ8ñ8 #
UltimaRegionalUsuario
ñ8ñ8  5
(
ñ8ñ85 6
string
ñ8ñ86 <
nombre
ñ8ñ8= C
)
ñ8ñ8C D
{
ò8ò8 	
return
ó8ó8 
DACConsulta
ó8ó8 
.
ó8ó8 #
UltimaRegionalUsuario
ó8ó8 4
(
ó8ó84 5
nombre
ó8ó85 ;
)
ó8ó8; <
;
ó8ó8< =
}
ô8ô8 	
public
õ8õ8 
List
õ8õ8 
<
õ8õ8 
Ref_ffmm_sexo
õ8õ8 !
>
õ8õ8! "
GetSexo
õ8õ8# *
(
õ8õ8* +
ref
õ8õ8+ . 
MessageResponseOBJ
õ8õ8/ A
MsgRes
õ8õ8B H
)
õ8õ8H I
{
ö8ö8 	
return
÷8÷8 
DACConsulta
÷8÷8 
.
÷8÷8 
GetSexo
÷8÷8 &
(
÷8÷8& '
ref
÷8÷8' *
MsgRes
÷8÷8+ 1
)
÷8÷81 2
;
÷8÷82 3
}
ø8ø8 	
public
ù8ù8 
List
ù8ù8 
<
ù8ù8  
Ref_ffmm_unidad_cp
ù8ù8 &
>
ù8ù8& '!
GetSitioAdscripcion
ù8ù8( ;
(
ù8ù8; <
ref
ù8ù8< ? 
MessageResponseOBJ
ù8ù8@ R
MsgRes
ù8ù8S Y
)
ù8ù8Y Z
{
ú8ú8 	
return
û8û8 
DACConsulta
û8û8 
.
û8û8 !
GetSitioAdscripcion
û8û8 2
(
û8û82 3
ref
û8û83 6
MsgRes
û8û87 =
)
û8û8= >
;
û8û8> ?
}
ü8ü8 	
public
ı8ı8 
List
ı8ı8 
<
ı8ı8 &
Ref_ffmm_tipo_afiliacion
ı8ı8 ,
>
ı8ı8, -
GetTipoAfiliacion
ı8ı8. ?
(
ı8ı8? @
ref
ı8ı8@ C 
MessageResponseOBJ
ı8ı8D V
MsgRes
ı8ı8W ]
)
ı8ı8] ^
{
ş8ş8 	
return
ÿ8ÿ8 
DACConsulta
ÿ8ÿ8 
.
ÿ8ÿ8 
GetTipoAfiliacion
ÿ8ÿ8 0
(
ÿ8ÿ80 1
ref
ÿ8ÿ81 4
MsgRes
ÿ8ÿ85 ;
)
ÿ8ÿ8; <
;
ÿ8ÿ8< =
}
€9€9 	
public
‚9‚9 
List
‚9‚9 
<
‚9‚9 
Ref_ffmm_estado
‚9‚9 #
>
‚9‚9# $
	GetEstado
‚9‚9% .
(
‚9‚9. /
ref
‚9‚9/ 2 
MessageResponseOBJ
‚9‚93 E
MsgRes
‚9‚9F L
)
‚9‚9L M
{
ƒ9ƒ9 	
return
„9„9 
DACConsulta
„9„9 
.
„9„9 
	GetEstado
„9„9 (
(
„9„9( )
ref
„9„9) ,
MsgRes
„9„9- 3
)
„9„93 4
;
„9„94 5
}
…9…9 	
public
†9†9 
List
†9†9 
<
†9†9 
Ref_ffmm_fuerza
†9†9 #
>
†9†9# $
	GetFuerza
†9†9% .
(
†9†9. /
ref
†9†9/ 2 
MessageResponseOBJ
†9†93 E
MsgRes
†9†9F L
)
†9†9L M
{
‡9‡9 	
return
ˆ9ˆ9 
DACConsulta
ˆ9ˆ9 
.
ˆ9ˆ9 
	GetFuerza
ˆ9ˆ9 (
(
ˆ9ˆ9( )
ref
ˆ9ˆ9) ,
MsgRes
ˆ9ˆ9- 3
)
ˆ9ˆ93 4
;
ˆ9ˆ94 5
}
‰9‰9 	
public
Š9Š9 
List
Š9Š9 
<
Š9Š9 
Ref_ffmm_sino
Š9Š9 !
>
Š9Š9! "
GetSiNo
Š9Š9# *
(
Š9Š9* +
ref
Š9Š9+ . 
MessageResponseOBJ
Š9Š9/ A
MsgRes
Š9Š9B H
)
Š9Š9H I
{
‹9‹9 	
return
Œ9Œ9 
DACConsulta
Œ9Œ9 
.
Œ9Œ9 
GetSiNo
Œ9Œ9 &
(
Œ9Œ9& '
ref
Œ9Œ9' *
MsgRes
Œ9Œ9+ 1
)
Œ9Œ91 2
;
Œ9Œ92 3
}
99 	
public
99 
List
99 
<
99 
vw_cie10
99 
>
99 
GetCie10
99 &
(
99& '
ref
99' * 
MessageResponseOBJ
99+ =
MsgRes
99> D
)
99D E
{
99 	
return
‘9‘9 
DACConsulta
‘9‘9 
.
‘9‘9 
GetCie10
‘9‘9 '
(
‘9‘9' (
ref
‘9‘9( +
MsgRes
‘9‘9, 2
)
‘9‘92 3
;
‘9‘93 4
}
’9’9 	
public
”9”9 
List
”9”9 
<
”9”9 
vw_ffmm_glosas
”9”9 "
>
”9”9" #
GetFFMMGlosas
”9”9$ 1
(
”9”91 2
ref
”9”92 5 
MessageResponseOBJ
”9”96 H
MsgRes
”9”9I O
)
”9”9O P
{
•9•9 	
return
–9–9 
DACConsulta
–9–9 
.
–9–9 
GetFFMMGlosas
–9–9 ,
(
–9–9, -
ref
–9–9- 0
MsgRes
–9–91 7
)
–9–97 8
;
–9–98 9
}
—9—9 	
public
99 
int
99 
CargueCorreosPPE
99 #
(
99# $
List
99$ (
<
99( )(
ecop_directorioPPE_correos
99) C
>
99C D
listadoCargue
99E R
,
99R S
ref
99T W 
MessageResponseOBJ
99X j
MsgRes
99k q
)
99q r
{
99 	
return
Ÿ9Ÿ9 

DACInserta
Ÿ9Ÿ9 
.
Ÿ9Ÿ9 
CargueCorreosPPE
Ÿ9Ÿ9 .
(
Ÿ9Ÿ9. /
listadoCargue
Ÿ9Ÿ9/ <
,
Ÿ9Ÿ9< =
ref
Ÿ9Ÿ9> A
MsgRes
Ÿ9Ÿ9B H
)
Ÿ9Ÿ9H I
;
Ÿ9Ÿ9I J
}
 9 9 	
public
¢9¢9 
int
¢9¢9  
eliminarCorreosPPE
¢9¢9 %
(
¢9¢9% &
ref
¢9¢9& ) 
MessageResponseOBJ
¢9¢9* <
MsgRes
¢9¢9= C
)
¢9¢9C D
{
£9£9 	
return
¤9¤9 

DACElimina
¤9¤9 
.
¤9¤9  
eliminarCorreosPPE
¤9¤9 0
(
¤9¤90 1
ref
¤9¤91 4
MsgRes
¤9¤95 ;
)
¤9¤9; <
;
¤9¤9< =
}
¥9¥9 	
public
ª9ª9 
Int32
ª9ª9 &
SaveProgramarVisitaGlosa
ª9ª9 -
(
ª9ª9- .
ffmm_glosas
ª9ª9. 9
objeto
ª9ª9: @
,
ª9ª9@ A
ref
ª9ª9B E 
MessageResponseOBJ
ª9ª9F X
MsgRes
ª9ª9Y _
)
ª9ª9_ `
{
«9«9 	
return
¬9¬9 

DACInserta
¬9¬9 
.
¬9¬9 &
SaveProgramarVisitaGlosa
¬9¬9 6
(
¬9¬96 7
objeto
¬9¬97 =
,
¬9¬9= >
ref
¬9¬9? B
MsgRes
¬9¬9C I
)
¬9¬9I J
;
¬9¬9J K
}
®9®9 	
public
±9±9 
Int32
±9±9 
UpdateGlosa
±9±9  
(
±9±9  !
ffmm_glosas
±9±9! ,
objeto
±9±9- 3
,
±9±93 4
string
±9±95 ;
caso
±9±9< @
,
±9±9@ A
ref
±9±9B E 
MessageResponseOBJ
±9±9F X
MsgRes
±9±9Y _
)
±9±9_ `
{
²9²9 	
return
´9´9 
DACActualiza
´9´9 
.
´9´9  
UpdateGlosa
´9´9  +
(
´9´9+ ,
objeto
´9´9, 2
,
´9´92 3
caso
´9´94 8
,
´9´98 9
ref
´9´9: =
MsgRes
´9´9> D
)
´9´9D E
;
´9´9E F
}
¶9¶9 	
public
¼9¼9 
List
¼9¼9 
<
¼9¼9 $
ffmm_Cuentas_auditoria
¼9¼9 *
>
¼9¼9* +!
GetCuentasAuditoria
¼9¼9, ?
(
¼9¼9? @
ref
¼9¼9@ C 
MessageResponseOBJ
¼9¼9D V
MsgRes
¼9¼9W ]
)
¼9¼9] ^
{
½9½9 	
return
¾9¾9 
DACConsulta
¾9¾9 
.
¾9¾9 !
GetCuentasAuditoria
¾9¾9 2
(
¾9¾92 3
ref
¾9¾93 6
MsgRes
¾9¾97 =
)
¾9¾9= >
;
¾9¾9> ?
}
À9À9 	
public
Â9Â9 
Int32
Â9Â9 (
UpdateProgramarVisitaGlosa
Â9Â9 /
(
Â9Â9/ 0
ffmm_glosas
Â9Â90 ;
objeto
Â9Â9< B
,
Â9Â9B C
ref
Â9Â9D G 
MessageResponseOBJ
Â9Â9H Z
MsgRes
Â9Â9[ a
)
Â9Â9a b
{
Ã9Ã9 	
return
Ä9Ä9 
DACActualiza
Ä9Ä9 
.
Ä9Ä9  (
UpdateProgramarVisitaGlosa
Ä9Ä9  :
(
Ä9Ä9: ;
objeto
Ä9Ä9; A
,
Ä9Ä9A B
ref
Ä9Ä9C F
MsgRes
Ä9Ä9G M
)
Ä9Ä9M N
;
Ä9Ä9N O
}
Æ9Æ9 	
public
È9È9 !
ffmm_cuentas_glosas
È9È9 "
GetCuentasGlosas
È9È9# 3
(
È9È93 4
int
È9È94 7
id_glosa
È9È98 @
,
È9È9@ A
ref
È9È9B E 
MessageResponseOBJ
È9È9F X
MsgRes
È9È9Y _
)
È9È9_ `
{
É9É9 	
return
Ê9Ê9 
DACConsulta
Ê9Ê9 
.
Ê9Ê9 
GetCuentasGlosas
Ê9Ê9 /
(
Ê9Ê9/ 0
id_glosa
Ê9Ê90 8
,
Ê9Ê98 9
ref
Ê9Ê9: =
MsgRes
Ê9Ê9> D
)
Ê9Ê9D E
;
Ê9Ê9E F
}
Ë9Ë9 	
public
Î9Î9 
ffmm_glosas
Î9Î9 
	GetGlosas
Î9Î9 $
(
Î9Î9$ %
int
Î9Î9% (
id_glosa
Î9Î9) 1
,
Î9Î91 2
ref
Î9Î93 6 
MessageResponseOBJ
Î9Î97 I
MsgRes
Î9Î9J P
)
Î9Î9P Q
{
Ï9Ï9 	
return
Ğ9Ğ9 
DACConsulta
Ğ9Ğ9 
.
Ğ9Ğ9 
	GetGlosas
Ğ9Ğ9 (
(
Ğ9Ğ9( )
id_glosa
Ğ9Ğ9) 1
,
Ğ9Ğ91 2
ref
Ğ9Ğ93 6
MsgRes
Ğ9Ğ97 =
)
Ğ9Ğ9= >
;
Ğ9Ğ9> ?
}
Ñ9Ñ9 	
public
Ô9Ô9 $
ffmm_Cuentas_auditoria
Ô9Ô9 %%
ultimoPagoyConciliacion
Ô9Ô9& =
(
Ô9Ô9= >
)
Ô9Ô9> ?
{
Õ9Õ9 	
return
Ö9Ö9 
DACConsulta
Ö9Ö9 
.
Ö9Ö9 %
ultimoPagoyConciliacion
Ö9Ö9 6
(
Ö9Ö96 7
)
Ö9Ö97 8
;
Ö9Ö98 9
}
×9×9 	
public
İ9İ9 
List
İ9İ9 
<
İ9İ9 1
#management_unionFuerzasgradosResult
İ9İ9 7
>
İ9İ97 8
getUnionFuerzas
İ9İ99 H
(
İ9İ9H I
int
İ9İ9I L
idFuerza
İ9İ9M U
)
İ9İ9U V
{
Ş9Ş9 	
return
ß9ß9 
DACConsulta
ß9ß9 
.
ß9ß9 
getUnionFuerzas
ß9ß9 .
(
ß9ß9. /
idFuerza
ß9ß9/ 7
)
ß9ß97 8
;
ß9ß98 9
}
à9à9 	
public
æ9æ9 
int
æ9æ9 4
&InsertarDispensacionMedicamentosCargue
æ9æ9 9
(
æ9æ99 :(
medicamentos_dispen_cargue
æ9æ9: T
objbase
æ9æ9U \
,
æ9æ9\ ]
ref
æ9æ9^ a 
MessageResponseOBJ
æ9æ9b t
MsgRes
æ9æ9u {
)
æ9æ9{ |
{
ç9ç9 	
return
è9è9 

DACInserta
è9è9 
.
è9è9 4
&InsertarDispensacionMedicamentosCargue
è9è9 D
(
è9è9D E
objbase
è9è9E L
,
è9è9L M
ref
è9è9N Q
MsgRes
è9è9R X
)
è9è9X Y
;
è9è9Y Z
}
é9é9 	
public
ë9ë9 
void
ë9ë9 "
EliminarCargueDispen
ë9ë9 (
(
ë9ë9( )
int
ë9ë9) ,
idCargue
ë9ë9- 5
,
ë9ë95 6
ref
ë9ë97 : 
MessageResponseOBJ
ë9ë9; M
MsgRes
ë9ë9N T
)
ë9ë9T U
{
ì9ì9 	

DACElimina
í9í9 
.
í9í9 "
EliminarCargueDispen
í9í9 +
(
í9í9+ ,
idCargue
í9í9, 4
,
í9í94 5
ref
í9í96 9
MsgRes
í9í9: @
)
í9í9@ A
;
í9í9A B
}
î9î9 	
public
ğ9ğ9 
void
ğ9ğ9 &
EliminarCargueDispendtll
ğ9ğ9 ,
(
ğ9ğ9, -
int
ğ9ğ9- 0
idCargue
ğ9ğ91 9
,
ğ9ğ99 :
ref
ğ9ğ9; > 
MessageResponseOBJ
ğ9ğ9? Q
MsgRes
ğ9ğ9R X
)
ğ9ğ9X Y
{
ñ9ñ9 	

DACElimina
ò9ò9 
.
ò9ò9 &
EliminarCargueDispendtll
ò9ò9 /
(
ò9ò9/ 0
idCargue
ò9ò90 8
,
ò9ò98 9
ref
ò9ò9: =
MsgRes
ò9ò9> D
)
ò9ò9D E
;
ò9ò9E F
}
ó9ó9 	
public
ö9ö9 
int
ö9ö9 5
'InsertarDispensacionMedicamentosCalidad
ö9ö9 :
(
ö9ö9: ;
List
ö9ö9; ?
<
ö9ö9? @)
medicamentos_dispen_calidad
ö9ö9@ [
>
ö9ö9[ \
List
ö9ö9] a
,
ö9ö9a b
Int32
ö9ö9c h
	id_cargue
ö9ö9i r
,
ö9ö9r s
ref
ö9ö9t w!
MessageResponseOBJö9ö9x Š
MsgResö9ö9‹ ‘
)ö9ö9‘ ’
{
÷9÷9 	
return
ø9ø9 

DACInserta
ø9ø9 
.
ø9ø9 5
'InsertarDispensacionMedicamentosCalidad
ø9ø9 E
(
ø9ø9E F
List
ø9ø9F J
,
ø9ø9J K
	id_cargue
ø9ø9L U
,
ø9ø9U V
ref
ø9ø9W Z
MsgRes
ø9ø9[ a
)
ø9ø9a b
;
ø9ø9b c
}
ù9ù9 	
public
ú9ú9 
List
ú9ú9 
<
ú9ú9 6
(management_medicamentosDispen_listResult
ú9ú9 <
>
ú9ú9< =+
ListaMedicamentosDispensacion
ú9ú9> [
(
ú9ú9[ \
)
ú9ú9\ ]
{
û9û9 	
return
ü9ü9 
DACConsulta
ü9ü9 
.
ü9ü9 +
ListaMedicamentosDispensacion
ü9ü9 <
(
ü9ü9< =
)
ü9ü9= >
;
ü9ü9> ?
}
ı9ı9 	
public
ş9ş9 
List
ş9ş9 
<
ş9ş9 9
+management_medicamentosDispen_reporteResult
ş9ş9 ?
>
ş9ş9? @2
$ListaMedicamentosDispensacionReporte
ş9ş9A e
(
ş9ş9e f
int
ş9ş9f i
idCargue
ş9ş9j r
)
ş9ş9r s
{
ÿ9ÿ9 	
return
€:€: 
DACConsulta
€:€: 
.
€:€: 2
$ListaMedicamentosDispensacionReporte
€:€: C
(
€:€:C D
idCargue
€:€:D L
)
€:€:L M
;
€:€:M N
}
:: 	
public
ƒ:ƒ: 
List
ƒ:ƒ: 
<
ƒ:ƒ: 4
&management_listaMedicDspensacionResult
ƒ:ƒ: :
>
ƒ:ƒ:: ;6
(ListaMedicamentosDispensacionPrestadores
ƒ:ƒ:< d
(
ƒ:ƒ:d e
int
ƒ:ƒ:e h
mes
ƒ:ƒ:i l
,
ƒ:ƒ:l m
int
ƒ:ƒ:n q
aÃ±o
ƒ:ƒ:r u
)
ƒ:ƒ:u v
{
„:„: 	
return
…:…: 
DACConsulta
…:…: 
.
…:…: 6
(ListaMedicamentosDispensacionPrestadores
…:…: G
(
…:…:G H
mes
…:…:H K
,
…:…:K L
aÃ±o
…:…:M P
)
…:…:P Q
;
…:…:Q R
}
†:†: 	
public
ˆ:ˆ: 
List
ˆ:ˆ: 
<
ˆ:ˆ: ?
1management_listaMedicDspensacion_agrupacionResult
ˆ:ˆ: E
>
ˆ:ˆ:E F@
2ListaMedicamentosDispensacionPrestadoresAgrupacion
ˆ:ˆ:G y
(
ˆ:ˆ:y z
int
ˆ:ˆ:z }
mesˆ:ˆ:~ 
,ˆ:ˆ: ‚
intˆ:ˆ:ƒ †
aÃ±oˆ:ˆ:‡ Š
)ˆ:ˆ:Š ‹
{
‰:‰: 	
return
Š:Š: 
DACConsulta
Š:Š: 
.
Š:Š: @
2ListaMedicamentosDispensacionPrestadoresAgrupacion
Š:Š: Q
(
Š:Š:Q R
mes
Š:Š:R U
,
Š:Š:U V
aÃ±o
Š:Š:W Z
)
Š:Š:Z [
;
Š:Š:[ \
}
‹:‹: 	
public
:: 
List
:: 
<
:: :
,management_medicamentosDispen_consultaResult
:: @
>
::@ A-
ListaMedicamentosDispenConsulta
::B a
(
::a b
DateTime
::b j
fechaIni
::k s
,
::s t
DateTime
::u }
fechaFin::~ †
)::† ‡
{
:: 	
return
:: 
DACConsulta
:: 
.
:: -
ListaMedicamentosDispenConsulta
:: >
(
::> ?
fechaIni
::? G
,
::G H
fechaFin
::I Q
)
::Q R
;
::R S
}
:: 	
public
‘:‘: 
List
‘:‘: 
<
‘:‘: A
3management_medicamentosDispen_consulta_armadaResult
‘:‘: G
>
‘:‘:G H3
%ListaMedicamentosDispenConsultaArmada
‘:‘:I n
(
‘:‘:n o
DateTime
‘:‘:o w
fechaIni‘:‘:x €
,‘:‘:€ 
DateTime‘:‘:‚ Š
fechaFin‘:‘:‹ “
,‘:‘:“ ”
string‘:‘:• ›
	documento‘:‘:œ ¥
,‘:‘:¥ ¦
string‘:‘:§ ­
familiar‘:‘:® ¶
,‘:‘:¶ ·
string‘:‘:¸ ¾
formula‘:‘:¿ Æ
)‘:‘:Æ Ç
{
’:’: 	
return
“:“: 
DACConsulta
“:“: 
.
“:“: 3
%ListaMedicamentosDispenConsultaArmada
“:“: D
(
“:“:D E
fechaIni
“:“:E M
,
“:“:M N
fechaFin
“:“:O W
,
“:“:W X
	documento
“:“:Y b
,
“:“:b c
familiar
“:“:d l
,
“:“:l m
formula
“:“:n u
)
“:“:u v
;
“:“:v w
}
”:”: 	
public
•:•: 
List
•:•: 
<
•:•: F
8management_medicamentosDispen_consulta_filtros_docResult
•:•: L
>
•:•:L M6
(ListaMedicamentosDispenConsultaFiltroDoc
•:•:N v
(
•:•:v w
string
•:•:w }
	documento•:•:~ ‡
)•:•:‡ ˆ
{
–:–: 	
return
—:—: 
DACConsulta
—:—: 
.
—:—: 6
(ListaMedicamentosDispenConsultaFiltroDoc
—:—: G
(
—:—:G H
	documento
—:—:H Q
)
—:—:Q R
;
—:—:R S
}
˜:˜: 	
public
š:š: 
List
š:š: 
<
š:š: K
=management_medicamentosDispen_consulta_filtros_familiarResult
š:š: Q
>
š:š:Q R<
-ListaMedicamentosDispenConsultaFiltroFamiliarš:š:S €
(š:š:€ 
stringš:š: ‡
familiaš:š:ˆ 
)š:š: 
{
›:›: 	
return
œ:œ: 
DACConsulta
œ:œ: 
.
œ:œ: ;
-ListaMedicamentosDispenConsultaFiltroFamiliar
œ:œ: L
(
œ:œ:L M
familia
œ:œ:M T
)
œ:œ:T U
;
œ:œ:U V
}
:: 	
public
Ÿ:Ÿ: 
List
Ÿ:Ÿ: 
<
Ÿ:Ÿ: J
<management_medicamentosDispen_consulta_filtros_formulaResult
Ÿ:Ÿ: P
>
Ÿ:Ÿ:P Q8
*ListaMedicamentosDispenConsultaFiltroFormu
Ÿ:Ÿ:R |
(
Ÿ:Ÿ:| }
stringŸ:Ÿ:} ƒ
	documentoŸ:Ÿ:„ 
)Ÿ:Ÿ: 
{
 : : 	
return
¡:¡: 
DACConsulta
¡:¡: 
.
¡:¡: 8
*ListaMedicamentosDispenConsultaFiltroFormu
¡:¡: I
(
¡:¡:I J
	documento
¡:¡:J S
)
¡:¡:S T
;
¡:¡:T U
}
¢:¢: 	
public
¦:¦: 
int
¦:¦: "
ValidaExisteAnalista
¦:¦: '
(
¦:¦:' (
int
¦:¦:( +
regional
¦:¦:, 4
,
¦:¦:4 5
int
¦:¦:6 9
unis
¦:¦:: >
,
¦:¦:> ?
int
¦:¦:@ C
analista
¦:¦:D L
)
¦:¦:L M
{
§:§: 	
return
¨:¨: 
DACConsulta
¨:¨: 
.
¨:¨: "
ValidaExisteAnalista
¨:¨: 3
(
¨:¨:3 4
regional
¨:¨:4 <
,
¨:¨:< =
unis
¨:¨:> B
,
¨:¨:B C
analista
¨:¨:D L
)
¨:¨:L M
;
¨:¨:M N
}
©:©: 	
public
«:«: *
ref_cuentas_medicas_analista
«:«: +&
TraerAnalistasIngresados
«:«:, D
(
«:«:D E*
ref_cuentas_medicas_analista
«:«:E a
obj
«:«:b e
)
«:«:e f
{
¬:¬: 	
return
­:­: 
DACConsulta
­:­: 
.
­:­: &
TraerAnalistasIngresados
­:­: 7
(
­:­:7 8
obj
­:­:8 ;
)
­:­:; <
;
­:­:< =
}
®:®: 	
public
°:°: $
vw_analistas_recepcion
°:°: %
TraerAnalista
°:°:& 3
(
°:°:3 4
int
°:°:4 7
	idUsuario
°:°:8 A
)
°:°:A B
{
±:±: 	
return
²:²: 
DACConsulta
²:²: 
.
²:²: 
TraerAnalista
²:²: ,
(
²:²:, -
	idUsuario
²:²:- 6
)
²:²:6 7
;
²:²:7 8
}
³:³: 	
public
´:´: 
int
´:´: 
InsertarAnalistas
´:´: $
(
´:´:$ %
List
´:´:% )
<
´:´:) **
ref_cuentas_medicas_analista
´:´:* F
>
´:´:F G
obj
´:´:H K
)
´:´:K L
{
µ:µ: 	
return
¶:¶: 

DACInserta
¶:¶: 
.
¶:¶: 
InsertarAnalistas
¶:¶: /
(
¶:¶:/ 0
obj
¶:¶:0 3
)
¶:¶:3 4
;
¶:¶:4 5
}
·:·: 	
public
º:º: 
List
º:º: 
<
º:º: 0
"ManagmentRipsHomologacionFacResult
º:º: 6
>
º:º:6 7%
ConsultaHomologacionFac
º:º:8 O
(
º:º:O P
String
º:º:P V
num_factura
º:º:W b
,
º:º:b c
String
º:º:d j
tipo_id_prestador
º:º:k |
,
º:º:| }
Stringº:º:~ „ 
num_id_prestadorº:º:… •
)º:º:• –
{
»:»: 	
return
¼:¼: 
DACConsulta
¼:¼: 
.
¼:¼: %
ConsultaHomologacionFac
¼:¼: 6
(
¼:¼:6 7
num_factura
¼:¼:7 B
,
¼:¼:B C
tipo_id_prestador
¼:¼:D U
,
¼:¼:U V
num_id_prestador
¼:¼:W g
)
¼:¼:g h
;
¼:¼:h i
}
½:½: 	
public
¿:¿: 
List
¿:¿: 
<
¿:¿: 4
&ManagmentRipsHomologacionFacDTLLResult
¿:¿: :
>
¿:¿:: ;)
ConsultaHomologacionFacdtll
¿:¿:< W
(
¿:¿:W X
String
¿:¿:X ^
num_factura
¿:¿:_ j
,
¿:¿:j k
String
¿:¿:l r 
tipo_id_prestador¿:¿:s „
,¿:¿:„ …
String¿:¿:† Œ 
num_id_prestador¿:¿: 
,¿:¿: 
Int32¿:¿:Ÿ ¤
id_rips¿:¿:¥ ¬
)¿:¿:¬ ­
{
À:À: 	
return
Á:Á: 
DACConsulta
Á:Á: 
.
Á:Á: )
ConsultaHomologacionFacdtll
Á:Á: :
(
Á:Á:: ;
num_factura
Á:Á:; F
,
Á:Á:F G
tipo_id_prestador
Á:Á:H Y
,
Á:Á:Y Z
num_id_prestador
Á:Á:[ k
,
Á:Á:k l
id_rips
Á:Á:m t
)
Á:Á:t u
;
Á:Á:u v
}
Â:Â: 	
public
Ä:Ä: 
int
Ä:Ä: (
Insertar_rips_homologacion
Ä:Ä: -
(
Ä:Ä:- .
rips_homologacion
Ä:Ä:. ?
objbase
Ä:Ä:@ G
,
Ä:Ä:G H
ref
Ä:Ä:I L 
MessageResponseOBJ
Ä:Ä:M _
MsgRes
Ä:Ä:` f
)
Ä:Ä:f g
{
Å:Å: 	
return
Æ:Æ: 

DACInserta
Æ:Æ: 
.
Æ:Æ: (
Insertar_rips_homologacion
Æ:Æ: 8
(
Æ:Æ:8 9
objbase
Æ:Æ:9 @
,
Æ:Æ:@ A
ref
Æ:Æ:B E
MsgRes
Æ:Æ:F L
)
Æ:Æ:L M
;
Æ:Æ:M N
}
Ç:Ç: 	
public
È:È: 
int
È:È: -
Insertar_rips_homologacion_dtll
È:È: 2
(
È:È:2 3
List
È:È:3 7
<
È:È:7 8$
rips_homologacion_dtll
È:È:8 N
>
È:È:N O
objbase
È:È:P W
,
È:È:W X
ref
È:È:Y \ 
MessageResponseOBJ
È:È:] o
MsgRes
È:È:p v
)
È:È:v w
{
É:É: 	
return
Ê:Ê: 

DACInserta
Ê:Ê: 
.
Ê:Ê: -
Insertar_rips_homologacion_dtll
Ê:Ê: =
(
Ê:Ê:= >
objbase
Ê:Ê:> E
,
Ê:Ê:E F
ref
Ê:Ê:G J
MsgRes
Ê:Ê:K Q
)
Ê:Ê:Q R
;
Ê:Ê:R S
}
Ë:Ë: 	
public
Ì:Ì: 
List
Ì:Ì: 
<
Ì:Ì: 
rips_homologacion
Ì:Ì: %
>
Ì:Ì:% &$
Traerhomologacion_dtll
Ì:Ì:' =
(
Ì:Ì:= >
rips_homologacion
Ì:Ì:> O
obj
Ì:Ì:P S
)
Ì:Ì:S T
{
Í:Í: 	
return
Î:Î: 
DACConsulta
Î:Î: 
.
Î:Î: $
Traerhomologacion_dtll
Î:Î: 5
(
Î:Î:5 6
obj
Î:Î:6 9
)
Î:Î:9 :
;
Î:Î:: ;
}
Ï:Ï: 	
public
Ğ:Ğ: 
List
Ğ:Ğ: 
<
Ğ:Ğ: 9
+ManagmentRipsHomologacionFacDTLLFinalResult
Ğ:Ğ: ?
>
Ğ:Ğ:? @.
 ConsultaHomologacionFacdtllFinal
Ğ:Ğ:A a
(
Ğ:Ğ:a b
String
Ğ:Ğ:b h
num_factura
Ğ:Ğ:i t
,
Ğ:Ğ:t u
Int32
Ğ:Ğ:v {
id_ripsĞ:Ğ:| ƒ
)Ğ:Ğ:ƒ „
{
Ñ:Ñ: 	
return
Ò:Ò: 
DACConsulta
Ò:Ò: 
.
Ò:Ò: .
 ConsultaHomologacionFacdtllFinal
Ò:Ò: ?
(
Ò:Ò:? @
num_factura
Ò:Ò:@ K
,
Ò:Ò:K L
id_rips
Ò:Ò:M T
)
Ò:Ò:T U
;
Ò:Ò:U V
}
Ó:Ó: 	
public
Õ:Õ: 
List
Õ:Õ: 
<
Õ:Õ: ,
vw_rips_homologacion_tarifario
Õ:Õ: 2
>
Õ:Õ:2 3
TarifarioRips
Õ:Õ:4 A
(
Õ:Õ:A B
)
Õ:Õ:B C
{
Ö:Ö: 	
return
×:×: 
DACComonClass
×:×:  
.
×:×:  !
TarifarioRips
×:×:! .
(
×:×:. /
)
×:×:/ 0
;
×:×:0 1
}
Ø:Ø: 	
public
Ú:Ú: 
int
Ú:Ú: .
 Actualizar_md_Lupe_cargue_base_H
Ú:Ú: 3
(
Ú:Ú:3 4$
rips_homologacion_dtll
Ú:Ú:4 J
obj
Ú:Ú:K N
,
Ú:Ú:N O
ref
Ú:Ú:P S 
MessageResponseOBJ
Ú:Ú:T f
MsgRes
Ú:Ú:g m
)
Ú:Ú:m n
{
Û:Û: 	
return
Ü:Ü: 
DACActualiza
Ü:Ü: 
.
Ü:Ü:  .
 Actualizar_md_Lupe_cargue_base_H
Ü:Ü:  @
(
Ü:Ü:@ A
obj
Ü:Ü:A D
,
Ü:Ü:D E
ref
Ü:Ü:F I
MsgRes
Ü:Ü:J P
)
Ü:Ü:P Q
;
Ü:Ü:Q R
}
İ:İ: 	
public
Ş:Ş: 
int
Ş:Ş: .
 Actualizar_md_Lupe_cargue_base_G
Ş:Ş: 3
(
Ş:Ş:3 4$
rips_homologacion_dtll
Ş:Ş:4 J
obj
Ş:Ş:K N
,
Ş:Ş:N O
ref
Ş:Ş:P S 
MessageResponseOBJ
Ş:Ş:T f
MsgRes
Ş:Ş:g m
)
Ş:Ş:m n
{
ß:ß: 	
return
à:à: 
DACActualiza
à:à: 
.
à:à:  .
 Actualizar_md_Lupe_cargue_base_G
à:à:  @
(
à:à:@ A
obj
à:à:A D
,
à:à:D E
ref
à:à:F I
MsgRes
à:à:J P
)
à:à:P Q
;
à:à:Q R
}
á:á: 	
public
ã:ã: 
List
ã:ã: 
<
ã:ã: 5
'ManagmentRipsHomologacionFacFinalResult
ã:ã: ;
>
ã:ã:; <*
ConsultaHomologacionFacFinal
ã:ã:= Y
(
ã:ã:Y Z
String
ã:ã:Z `
num_factura
ã:ã:a l
,
ã:ã:l m
String
ã:ã:n t 
tipo_id_prestadorã:ã:u †
,ã:ã:† ‡
Stringã:ã:ˆ  
num_id_prestadorã:ã: Ÿ
,ã:ã:Ÿ  
Int32ã:ã:¡ ¦
id_ripsã:ã:§ ®
)ã:ã:® ¯
{
ä:ä: 	
return
å:å: 
DACConsulta
å:å: 
.
å:å: *
ConsultaHomologacionFacFinal
å:å: ;
(
å:å:; <
num_factura
å:å:< G
,
å:å:G H
tipo_id_prestador
å:å:I Z
,
å:å:Z [
num_id_prestador
å:å:\ l
,
å:å:l m
id_rips
å:å:n u
)
å:å:u v
;
å:å:v w
}
æ:æ: 	
public
ç:ç: 
int
ç:ç: *
Actualizar_rips_homologacion
ç:ç: /
(
ç:ç:/ 0
rips_homologacion
ç:ç:0 A
obj
ç:ç:B E
,
ç:ç:E F
ref
ç:ç:G J 
MessageResponseOBJ
ç:ç:K ]
MsgRes
ç:ç:^ d
)
ç:ç:d e
{
è:è: 	
return
é:é: 
DACActualiza
é:é: 
.
é:é:  *
Actualizar_rips_homologacion
é:é:  <
(
é:é:< =
obj
é:é:= @
,
é:é:@ A
ref
é:é:B E
MsgRes
é:é:F L
)
é:é:L M
;
é:é:M N
}
ê:ê: 	
public
ë:ë: 
void
ë:ë: ,
ActualizarFacturas_automaticas
ë:ë: 2
(
ë:ë:2 3
int
ë:ë:3 6
idBaseFactura
ë:ë:7 D
)
ë:ë:D E
{
ì:ì: 	
DACActualiza
í:í: 
.
í:í: ,
ActualizarFacturas_automaticas
í:í: 7
(
í:í:7 8
idBaseFactura
í:í:8 E
)
í:í:E F
;
í:í:F G
}
î:î: 	
public
ï:ï: 
List
ï:ï: 
<
ï:ï: ;
-management_gastoServicio_nombreServicioResult
ï:ï: A
>
ï:ï:A B(
ConsultarNombreServicioGXS
ï:ï:C ]
(
ï:ï:] ^
string
ï:ï:^ d
nombre
ï:ï:e k
)
ï:ï:k l
{
ğ:ğ: 	
return
ñ:ñ: 
DACConsulta
ñ:ñ: 
.
ñ:ñ: (
ConsultarNombreServicioGXS
ñ:ñ: 9
(
ñ:ñ:9 :
nombre
ñ:ñ:: @
)
ñ:ñ:@ A
;
ñ:ñ:A B
}
ò:ò: 	
public
ó:ó: 
int
ó:ó: %
EliminarTotalEvaluacion
ó:ó: *
(
ó:ó:* +
int
ó:ó:+ .
idEvaluacion
ó:ó:/ ;
)
ó:ó:; <
{
ô:ô: 	
return
õ:õ: 

DACElimina
õ:õ: 
.
õ:õ: %
EliminarTotalEvaluacion
õ:õ: 5
(
õ:õ:5 6
idEvaluacion
õ:õ:6 B
)
õ:õ:B C
;
õ:õ:C D
}
ö:ö: 	
public
÷:÷: 
int
÷:÷: *
ActualizarVisitaDispensacion
÷:÷: /
(
÷:÷:/ 0#
ver_dispen_evaluacion
÷:÷:0 E
obj
÷:÷:F I
)
÷:÷:I J
{
ø:ø: 	
return
ù:ù: 
DACActualiza
ù:ù: 
.
ù:ù:  *
ActualizarVisitaDispensacion
ù:ù:  <
(
ù:ù:< =
obj
ù:ù:= @
)
ù:ù:@ A
;
ù:ù:A B
}
ú:ú: 	
public
û:û: 
int
û:û: 3
%EliminarEvaluacionVisitasAutoguardado
û:û: 8
(
û:û:8 9
int
û:û:9 <
idEvaluacion
û:û:= I
)
û:û:I J
{
ü:ü: 	
return
ı:ı: 

DACElimina
ı:ı: 
.
ı:ı: 3
%EliminarEvaluacionVisitasAutoguardado
ı:ı: C
(
ı:ı:C D
idEvaluacion
ı:ı:D P
)
ı:ı:P Q
;
ı:ı:Q R
}
ş:ş: 	
public
ÿ:ÿ: 
List
ÿ:ÿ: 
<
ÿ:ÿ: >
0management_informacionUsuarios_prestadoresResult
ÿ:ÿ: D
>
ÿ:ÿ:D E!
UsuariosPrestadores
ÿ:ÿ:F Y
(
ÿ:ÿ:Y Z
string
ÿ:ÿ:Z `
nit
ÿ:ÿ:a d
,
ÿ:ÿ:d e
string
ÿ:ÿ:f l
nombre
ÿ:ÿ:m s
,
ÿ:ÿ:s t
string
ÿ:ÿ:u {
cedulaÿ:ÿ:| ‚
)ÿ:ÿ:‚ ƒ
{
€;€; 	
return
;; 
DACConsulta
;; 
.
;; !
UsuariosPrestadores
;; 2
(
;;2 3
nit
;;3 6
,
;;6 7
nombre
;;8 >
,
;;> ?
cedula
;;@ F
)
;;F G
;
;;G H
}
‚;‚; 	
public
„;„; 
List
„;„; 
<
„;„; E
7management_informacionUsuarios_prestadoresDetalleResult
„;„; K
>
„;„;K L(
UsuariosPrestadoresDetalle
„;„;M g
(
„;„;g h
int
„;„;h k
	idUsuario
„;„;l u
)
„;„;u v
{
…;…; 	
return
†;†; 
DACConsulta
†;†; 
.
†;†; (
UsuariosPrestadoresDetalle
†;†; 9
(
†;†;9 :
	idUsuario
†;†;: C
)
†;†;C D
;
†;†;D E
}
‡;‡; 	
public
‰;‰; 
int
‰;‰; "
EliminarLoteFacturas
‰;‰; '
(
‰;‰;' (
int
‰;‰;( +
id
‰;‰;, .
)
‰;‰;. /
{
Š;Š; 	
return
‹;‹; 

DACElimina
‹;‹; 
.
‹;‹; "
EliminarLoteFacturas
‹;‹; 2
(
‹;‹;2 3
id
‹;‹;3 5
)
‹;‹;5 6
;
‹;‹;6 7
}
Œ;Œ; 	
public
;; 
sis_usuario
;; 
datosUsuarioId
;; )
(
;;) *
int
;;* -
	idUsuario
;;. 7
)
;;7 8
{
;; 	
return
;; 
DACConsulta
;; 
.
;; 
datosUsuarioId
;; -
(
;;- .
	idUsuario
;;. 7
)
;;7 8
;
;;8 9
}
;; 	
public
’;’; 
sis_usuario
’;’; 
datosUsuarioRol
’;’; *
(
’;’;* +
int
’;’;+ .
?
’;’;. /
idRol
’;’;0 5
)
’;’;5 6
{
“;“; 	
return
”;”; 
DACConsulta
”;”; 
.
”;”; 
datosUsuarioRol
”;”; .
(
”;”;. /
idRol
”;”;/ 4
)
”;”;4 5
;
”;”;5 6
}
•;•; 	
public
—;—; 
sis_usuario
—;—; 
datosUsuarioUser
—;—; +
(
—;—;+ ,
string
—;—;, 2
usuario
—;—;3 :
)
—;—;: ;
{
˜;˜; 	
return
™;™; 
DACConsulta
™;™; 
.
™;™; 
datosUsuarioUser
™;™; /
(
™;™;/ 0
usuario
™;™;0 7
)
™;™;7 8
;
™;™;8 9
}
š;š; 	
public
œ;œ; 
sis_usuario
œ;œ;  
datosUsuarioNombre
œ;œ; -
(
œ;œ;- .
string
œ;œ;. 4
nombre
œ;œ;5 ;
)
œ;œ;; <
{
;; 	
return
;; 
DACConsulta
;; 
.
;;  
datosUsuarioNombre
;; 1
(
;;1 2
nombre
;;2 8
)
;;8 9
;
;;9 :
}
Ÿ;Ÿ; 	
public
¡;¡; 
List
¡;¡; 
<
¡;¡; 9
+management_existeLoteAsignado_FacturaResult
¡;¡; ?
>
¡;¡;? @ 
ExisteLoteAsignado
¡;¡;A S
(
¡;¡;S T
int
¡;¡;T W
idFac
¡;¡;X ]
)
¡;¡;] ^
{
¢;¢; 	
return
£;£; 
DACConsulta
£;£; 
.
£;£;  
ExisteLoteAsignado
£;£; 1
(
£;£;1 2
idFac
£;£;2 7
)
£;£;7 8
;
£;£;8 9
}
¤;¤; 	
public
¥;¥; 
List
¥;¥; 
<
¥;¥; 
Ref_ips_cuentas
¥;¥; #
>
¥;¥;# $$
getprestadoresEspecial
¥;¥;% ;
(
¥;¥;; <
string
¥;¥;< B
nit
¥;¥;C F
,
¥;¥;F G
string
¥;¥;H N
	prestador
¥;¥;O X
)
¥;¥;X Y
{
¦;¦; 	
return
§;§; 
DACConsulta
§;§; 
.
§;§; $
getprestadoresEspecial
§;§; 5
(
§;§;5 6
nit
§;§;6 9
,
§;§;9 :
	prestador
§;§;; D
)
§;§;D E
;
§;§;E F
}
¨;¨; 	
public
©;©; 6
(management_prestadoresRegionalIdPrResult
©;©; 7
PrestadorRegional
©;©;8 I
(
©;©;I J
int
©;©;J M
idPrestador
©;©;N Y
)
©;©;Y Z
{
ª;ª; 	
return
«;«; 
DACConsulta
«;«; 
.
«;«; 
PrestadorRegional
«;«; 0
(
«;«;0 1
idPrestador
«;«;1 <
)
«;«;< =
;
«;«;= >
}
¬;¬; 	
public
­;­; 
List
­;­; 
<
­;­; %
vw_sis_auditor_regional
­;­; +
>
­;­;+ ,
UsuariosxRegional
­;­;- >
(
­;­;> ?
int
­;­;? B

idRegional
­;­;C M
)
­;­;M N
{
®;®; 	
return
¯;¯; 
DACConsulta
¯;¯; 
.
¯;¯; 
UsuariosxRegional
¯;¯; 0
(
¯;¯;0 1

idRegional
¯;¯;1 ;
)
¯;¯;; <
;
¯;¯;< =
}
°;°; 	
public
±;±; 
List
±;±; 
<
±;±; *
ref_cuentas_medicas_analista
±;±; 0
>
±;±;0 1,
getAnalistasAsignadosprestador
±;±;2 P
(
±;±;P Q
int
±;±;Q T
idPrestador
±;±;U `
)
±;±;` a
{
²;²; 	
return
³;³; 
DACConsulta
³;³; 
.
³;³; ,
getAnalistasAsignadosprestador
³;³; =
(
³;³;= >
idPrestador
³;³;> I
)
³;³;I J
;
³;³;J K
}
´;´; 	
public
µ;µ; 
int
µ;µ; -
ActualizarAsignacion_automatica
µ;µ; 2
(
µ;µ;2 3
int
µ;µ;3 6
idPrestador
µ;µ;7 B
)
µ;µ;B C
{
¶;¶; 	
return
·;·; 
DACActualiza
·;·; 
.
·;·;  -
ActualizarAsignacion_automatica
·;·;  ?
(
·;·;? @
idPrestador
·;·;@ K
)
·;·;K L
;
·;·;L M
}
¸;¸; 	
public
¹;¹; *
ref_cuentas_medicas_analista
¹;¹; +(
ListadoActualizarAnalistas
¹;¹;, F
(
¹;¹;F G
int
¹;¹;G J
idPrestador
¹;¹;K V
,
¹;¹;V W
int
¹;¹;X [

idAnalista
¹;¹;\ f
)
¹;¹;f g
{
º;º; 	
return
»;»; 
DACConsulta
»;»; 
.
»;»; (
ListadoActualizarAnalistas
»;»; 9
(
»;»;9 :
idPrestador
»;»;: E
,
»;»;E F

idAnalista
»;»;G Q
)
»;»;Q R
;
»;»;R S
}
¼;¼; 	
public
½;½; 
int
½;½; ,
ActualizarAsignacionAutomatica
½;½; 1
(
½;½;1 2*
ref_cuentas_medicas_analista
½;½;2 N
obj
½;½;O R
)
½;½;R S
{
¾;¾; 	
return
¿;¿; 
DACActualiza
¿;¿; 
.
¿;¿;  ,
ActualizarAsignacionAutomatica
¿;¿;  >
(
¿;¿;> ?
obj
¿;¿;? B
)
¿;¿;B C
;
¿;¿;C D
}
À;À; 	
public
Á;Á; 
int
Á;Á; :
,InsertarNuevosAnalistas_asignacionAutomatica
Á;Á; ?
(
Á;Á;? @
List
Á;Á;@ D
<
Á;Á;D E*
ref_cuentas_medicas_analista
Á;Á;E a
>
Á;Á;a b
obj
Á;Á;c f
)
Á;Á;f g
{
Â;Â; 	
return
Ã;Ã; 

DACInserta
Ã;Ã; 
.
Ã;Ã; :
,InsertarNuevosAnalistas_asignacionAutomatica
Ã;Ã; J
(
Ã;Ã;J K
obj
Ã;Ã;K N
)
Ã;Ã;N O
;
Ã;Ã;O P
}
Ä;Ä; 	
public
Å;Å; 
List
Å;Å; 
<
Å;Å; 9
+management_facturacion_tableroControlResult
Å;Å; ?
>
Å;Å;? @)
ListadoMedicamentosFacturas
Å;Å;A \
(
Å;Å;\ ]
DateTime
Å;Å;] e
fechaInicio
Å;Å;f q
,
Å;Å;q r
DateTime
Å;Å;s {
fechaFinÅ;Å;| „
,Å;Å;„ …
stringÅ;Å;† Œ
identificacionÅ;Å; ›
)Å;Å;› œ
{
Æ;Æ; 	
return
Ç;Ç; 
DACConsulta
Ç;Ç; 
.
Ç;Ç; )
ListadoMedicamentosFacturas
Ç;Ç; :
(
Ç;Ç;: ;
fechaInicio
Ç;Ç;; F
,
Ç;Ç;F G
fechaFin
Ç;Ç;H P
,
Ç;Ç;P Q
identificacion
Ç;Ç;R `
)
Ç;Ç;` a
;
Ç;Ç;a b
}
È;È; 	
public
Ê;Ê; 
List
Ê;Ê; 
<
Ê;Ê; <
.management_facturacion_consolidado_listaResult
Ê;Ê; B
>
Ê;Ê;B C9
+ListadoMedicamentosFacturasConsolidadoLista
Ê;Ê;D o
(
Ê;Ê;o p
DateTime
Ê;Ê;p x
fechaIniÊ;Ê;y 
,Ê;Ê; ‚
DateTimeÊ;Ê;ƒ ‹
fechaFinÊ;Ê;Œ ”
)Ê;Ê;” •
{
Ë;Ë; 	
return
Ì;Ì; 
DACConsulta
Ì;Ì; 
.
Ì;Ì; 9
+ListadoMedicamentosFacturasConsolidadoLista
Ì;Ì; J
(
Ì;Ì;J K
fechaIni
Ì;Ì;K S
,
Ì;Ì;S T
fechaFin
Ì;Ì;U ]
)
Ì;Ì;] ^
;
Ì;Ì;^ _
}
Í;Í; 	
public
Î;Î; >
0managemenet_prestadores_traerDatosFacturasResult
Î;Î; ?!
ListadoFacturasIdAf
Î;Î;@ S
(
Î;Î;S T
int
Î;Î;T W
id_af
Î;Î;X ]
)
Î;Î;] ^
{
Ï;Ï; 	
return
Ğ;Ğ; 
DACConsulta
Ğ;Ğ; 
.
Ğ;Ğ; !
ListadoFacturasIdAf
Ğ;Ğ; 2
(
Ğ;Ğ;2 3
id_af
Ğ;Ğ;3 8
)
Ğ;Ğ;8 9
;
Ğ;Ğ;9 :
}
Ñ;Ñ; 	
public
Ò;Ò; 
List
Ò;Ò; 
<
Ò;Ò; )
ref_componente_hospitalario
Ò;Ò; /
>
Ò;Ò;/ 0)
GetComponentesHospitalarios
Ò;Ò;1 L
(
Ò;Ò;L M
)
Ò;Ò;M N
{
Ó;Ó; 	
return
Ô;Ô; 
DACConsulta
Ô;Ô; 
.
Ô;Ô; )
GetComponentesHospitalarios
Ô;Ô; :
(
Ô;Ô;: ;
)
Ô;Ô;; <
;
Ô;Ô;< =
}
Õ;Õ; 	
public
Ö;Ö; 
int
Ö;Ö; /
!EliminarCarguePrefacturasCompleto
Ö;Ö; 4
(
Ö;Ö;4 5
int
Ö;Ö;5 8
idCargue
Ö;Ö;9 A
)
Ö;Ö;A B
{
×;×; 	
return
Ø;Ø; 

DACElimina
Ø;Ø; 
.
Ø;Ø; /
!EliminarCarguePrefacturasCompleto
Ø;Ø; ?
(
Ø;Ø;? @
idCargue
Ø;Ø;@ H
)
Ø;Ø;H I
;
Ø;Ø;I J
}
Ù;Ù; 	
public
Û;Û; 
int
Û;Û; .
 GuardarLogEliminacionPrefacturas
Û;Û; 3
(
Û;Û;3 4-
log_prefacturas_eliminarCargues
Û;Û;4 S
obj
Û;Û;T W
)
Û;Û;W X
{
Ü;Ü; 	
return
İ;İ; 

DACInserta
İ;İ; 
.
İ;İ; .
 GuardarLogEliminacionPrefacturas
İ;İ; >
(
İ;İ;> ?
obj
İ;İ;? B
)
İ;İ;B C
;
İ;İ;C D
}
Ş;Ş; 	
public
à;à; 
List
à;à; 
<
à;à; 8
*management_prefacturas_tableroCierreResult
à;à; >
>
à;à;> ?1
#TableroInformacionCierrePrefacturas
à;à;@ c
(
à;à;c d
DateTime
à;à;d l
?
à;à;l m
fechaInicio
à;à;n y
,
à;à;y z
DateTimeà;à;{ ƒ
?à;à;ƒ „
fechaFinà;à;… 
)à;à; 
{
á;á; 	
return
â;â; 
DACConsulta
â;â; 
.
â;â; 1
#TableroInformacionCierrePrefacturas
â;â; B
(
â;â;B C
fechaInicio
â;â;C N
,
â;â;N O
fechaFin
â;â;P X
)
â;â;X Y
;
â;â;Y Z
}
ã;ã; 	
public
å;å; 
List
å;å; 
<
å;å; 8
*management_prefacturas_tableroAhorroResult
å;å; >
>
å;å;> ?1
#TableroInformacionAhorroPrefacturas
å;å;@ c
(
å;å;c d
DateTime
å;å;d l
?
å;å;l m
fechaInicio
å;å;n y
,
å;å;y z
DateTimeå;å;{ ƒ
?å;å;ƒ „
fechaFinå;å;… 
)å;å; 
{
æ;æ; 	
return
ç;ç; 
DACConsulta
ç;ç; 
.
ç;ç; 1
#TableroInformacionAhorroPrefacturas
ç;ç; B
(
ç;ç;B C
fechaInicio
ç;ç;C N
,
ç;ç;N O
fechaFin
ç;ç;P X
)
ç;ç;X Y
;
ç;ç;Y Z
}
è;è; 	
public
ê;ê; 
List
ê;ê; 
<
ê;ê; 
ref_analista_lote
ê;ê; %
>
ê;ê;% &(
TraerAnalistaLoteExistente
ê;ê;' A
(
ê;ê;A B
int
ê;ê;B E
idlote
ê;ê;F L
)
ê;ê;L M
{
ë;ë; 	
return
ì;ì; 
DACConsulta
ì;ì; 
.
ì;ì; (
TraerAnalistaLoteExistente
ì;ì; 9
(
ì;ì;9 :
idlote
ì;ì;: @
)
ì;ì;@ A
;
ì;ì;A B
}
í;í; 	
public
î;î; 
int
î;î; $
ActualizarLoteAnalista
î;î; )
(
î;î;) *
ref_analista_lote
î;î;* ;
obj
î;î;< ?
,
î;î;? @
ref
î;î;A D 
MessageResponseOBJ
î;î;E W
MsgRes
î;î;X ^
)
î;î;^ _
{
ï;ï; 	
return
ğ;ğ; 
DACActualiza
ğ;ğ; 
.
ğ;ğ;  $
ActualizarLoteAnalista
ğ;ğ;  6
(
ğ;ğ;6 7
obj
ğ;ğ;7 :
,
ğ;ğ;: ;
ref
ğ;ğ;< ?
MsgRes
ğ;ğ;@ F
)
ğ;ğ;F G
;
ğ;ğ;G H
}
ñ;ñ; 	
public
ş;ş; 
Int32
ş;ş; @
2InsertarInventarioFacturasContabilizadasCargueBase
ş;ş; G
(
ş;ş;G H;
-inventario_facturas_contabilizadas_carguebase
ş;ş;H u
obj
ş;ş;v y
,
ş;ş;y z
ref
ş;ş;{ ~!
MessageResponseOBJş;ş; ‘
MsgResş;ş;’ ˜
)ş;ş;˜ ™
{
ÿ;ÿ; 	
return
€<€< 

DACInserta
€<€< 
.
€<€< @
2InsertarInventarioFacturasContabilizadasCargueBase
€<€< P
(
€<€<P Q
obj
€<€<Q T
,
€<€<T U
ref
€<€<V Y
MsgRes
€<€<Z `
)
€<€<` a
;
€<€<a b
}
<< 	
public
Š<Š< 
void
Š<Š< @
2InsertarInventarioFacturasContabilizadasCargueDtll
Š<Š< F
(
Š<Š<F G
List
Š<Š<G K
<
Š<Š<K L;
-inventario_facturas_contabilizadas_carguedtll
Š<Š<L y
>
Š<Š<y z
dtll
Š<Š<{ 
,Š<Š< €
refŠ<Š< „"
MessageResponseOBJŠ<Š<… —
MsgResŠ<Š<˜ 
)Š<Š< Ÿ
{
‹<‹< 	

DACInserta
Œ<Œ< 
.
Œ<Œ< @
2InsertarInventarioFacturasContabilizadasCargueDtll
Œ<Œ< I
(
Œ<Œ<I J
dtll
Œ<Œ<J N
,
Œ<Œ<N O
ref
Œ<Œ<P S
MsgRes
Œ<Œ<T Z
)
Œ<Œ<Z [
;
Œ<Œ<[ \
}
<< 	
public
—<—< 
List
—<—< 
<
—<—< A
3Management_inventario_facturas_contabilizadasResult
—<—< G
>
—<—<G H2
$ConsultarInventarioFacturasPorEstado
—<—<I m
(
—<—<m n
int
—<—<n q
idEstado
—<—<r z
,
—<—<z {
DateTime—<—<| „
?—<—<„ …
fechainicio—<—<† ‘
,—<—<‘ ’
DateTime—<—<“ ›
?—<—<› œ

fechafinal—<—< §
,—<—<§ ¨
int—<—<© ¬
?—<—<¬ ­
regional—<—<® ¶
,—<—<¶ ·
ref—<—<¸ »"
MessageResponseOBJ—<—<¼ Î
MsgRes—<—<Ï Õ
)—<—<Õ Ö
{
˜<˜< 	
return
™<™< 
DACConsulta
™<™< 
.
™<™< 2
$ConsultarInventarioFacturasPorEstado
™<™< C
(
™<™<C D
idEstado
™<™<D L
,
™<™<L M
fechainicio
™<™<N Y
,
™<™<Y Z

fechafinal
™<™<[ e
,
™<™<e f
regional
™<™<g o
,
™<™<o p
ref
™<™<q t
MsgRes
™<™<u {
)
™<™<{ |
;
™<™<| }
}
š<š< 	
public
£<£< 
void
£<£< 9
+GuardarGestionInvetarioFacturaContabilizada
£<£< ?
(
£<£<? @8
*inventario_facturas_contabilizadas_gestion
£<£<@ j
obj
£<£<k n
,
£<£<n o
ref
£<£<p s!
MessageResponseOBJ£<£<t †
MsgRes£<£<‡ 
)£<£< 
{
¤<¤< 	

DACInserta
¥<¥< 
.
¥<¥< 9
+GuardarGestionInvetarioFacturaContabilizada
¥<¥< B
(
¥<¥<B C
obj
¥<¥<C F
,
¥<¥<F G
ref
¥<¥<H K
MsgRes
¥<¥<L R
)
¥<¥<R S
;
¥<¥<S T
}
¦<¦< 	
public
³<³< 
List
³<³< 
<
³<³< M
?Management_inventario_facturas_contabilizadas_cordinacionResult
³<³< S
>
³<³<S T4
&ConsultarInventarioFacturasCordinacion
³<³<U {
(
³<³<{ |
int
³<³<| 
mes³<³<€ ƒ
,³<³<ƒ „
int³<³<… ˆ
aÃ±o³<³<‰ Œ
,³<³<Œ 
int³<³< ‘
regional³<³<’ š
,³<³<š ›
ref³<³<œ Ÿ"
MessageResponseOBJ³<³<  ²
MsgRes³<³<³ ¹
)³<³<¹ º
{
´<´< 	
return
µ<µ< 
DACConsulta
µ<µ< 
.
µ<µ< 4
&ConsultarInventarioFacturasCordinacion
µ<µ< E
(
µ<µ<E F
mes
µ<µ<F I
,
µ<µ<I J
aÃ±o
µ<µ<K N
,
µ<µ<N O
regional
µ<µ<P X
,
µ<µ<X Y
ref
µ<µ<Z ]
MsgRes
µ<µ<^ d
)
µ<µ<d e
;
µ<µ<e f
}
¶<¶< 	
public
¾<¾< 
List
¾<¾< 
<
¾<¾< M
?Management_inventario_facturas_contabilizadas_consolidadoResult
¾<¾< S
>
¾<¾<S T4
&ConsultarInventarioFacturasConsolidado
¾<¾<U {
(
¾<¾<{ |
)
¾<¾<| }
{
¿<¿< 	
return
À<À< 
DACConsulta
À<À< 
.
À<À< 4
&ConsultarInventarioFacturasConsolidado
À<À< E
(
À<À<E F
)
À<À<F G
;
À<À<G H
}
Á<Á< 	
public
Ê<Ê< 8
*inventario_facturas_contabilizadas_gestion
Ê<Ê< 9>
0consultarGestionFacturaContabilizadaporIdDetalle
Ê<Ê<: j
(
Ê<Ê<j k
int
Ê<Ê<k n
	idDetalle
Ê<Ê<o x
)
Ê<Ê<x y
{
Ë<Ë< 	
return
Ì<Ì< 
DACConsulta
Ì<Ì< 
.
Ì<Ì< >
0consultarGestionFacturaContabilizadaporIdDetalle
Ì<Ì< O
(
Ì<Ì<O P
	idDetalle
Ì<Ì<P Y
)
Ì<Ì<Y Z
;
Ì<Ì<Z [
}
Í<Í< 	
public
Ï<Ï< 8
*inventario_facturas_contabilizadas_gestion
Ï<Ï< 9>
0consultarGestionFacturaContabilizadaporIdGestion
Ï<Ï<: j
(
Ï<Ï<j k
int
Ï<Ï<k n
	idGestion
Ï<Ï<o x
)
Ï<Ï<x y
{
Ğ<Ğ< 	
return
Ñ<Ñ< 
DACConsulta
Ñ<Ñ< 
.
Ñ<Ñ< >
0consultarGestionFacturaContabilizadaporIdGestion
Ñ<Ñ< O
(
Ñ<Ñ<O P
	idGestion
Ñ<Ñ<P Y
)
Ñ<Ñ<Y Z
;
Ñ<Ñ<Z [
}
Ò<Ò< 	
public
Û<Û< 
void
Û<Û< O
AinsertarDatosArchivoCargadoInventarioFacContabilizadasConsolidado
Û<Û< U
(
Û<Û<U VC
4inventario_facturas_contabilizadas_gestor_documentalÛ<Û<V Š
docÛ<Û<‹ 
,Û<Û< 
refÛ<Û< “"
MessageResponseOBJÛ<Û<” ¦
MsgResÛ<Û<§ ­
)Û<Û<­ ®
{
Ü<Ü< 	

DACInserta
İ<İ< 
.
İ<İ< O
AinsertarDatosArchivoCargadoInventarioFacContabilizadasConsolidado
İ<İ< X
(
İ<İ<X Y
doc
İ<İ<Y \
,
İ<İ<\ ]
ref
İ<İ<^ a
MsgRes
İ<İ<b h
)
İ<İ<h i
;
İ<İ<i j
}
Ş<Ş< 	
public
ç<ç< B
4inventario_facturas_contabilizadas_gestor_documental
ç<ç< C2
$ConsultarRegistroArchivoCargadoPorId
ç<ç<D h
(
ç<ç<h i
int
ç<ç<i l
	idArchivo
ç<ç<m v
,
ç<ç<v w
ref
ç<ç<x {!
MessageResponseOBJç<ç<| 
MsgResç<ç< •
)ç<ç<• –
{
è<è< 	
return
é<é< 
DACConsulta
é<é< 
.
é<é< 2
$ConsultarRegistroArchivoCargadoPorId
é<é< C
(
é<é<C D
	idArchivo
é<é<D M
,
é<é<M N
ref
é<é<O R
MsgRes
é<é<S Y
)
é<é<Y Z
;
é<é<Z [
}
ê<ê< 	
public
ì<ì< 
List
ì<ì< 
<
ì<ì< B
4inventario_facturas_contabilizadas_gestor_documental
ì<ì< H
>
ì<ì<H I7
)ConsultarRegistroArchivoCargadoPorIdLista
ì<ì<J s
(
ì<ì<s t
int
ì<ì<t w
?
ì<ì<w x
mes
ì<ì<y |
,
ì<ì<| }
intì<ì<~ 
?ì<ì< ‚
aÃ±oì<ì<ƒ †
,ì<ì<† ‡
intì<ì<ˆ ‹
?ì<ì<‹ Œ
regionalì<ì< •
,ì<ì<• –
refì<ì<— š"
MessageResponseOBJì<ì<› ­
MsgResì<ì<® ´
)ì<ì<´ µ
{
í<í< 	
return
î<î< 
DACConsulta
î<î< 
.
î<î< 7
)ConsultarRegistroArchivoCargadoPorIdLista
î<î< H
(
î<î<H I
mes
î<î<I L
,
î<î<L M
aÃ±o
î<î<N Q
,
î<î<Q R
regional
î<î<S [
,
î<î<[ \
ref
î<î<] `
MsgRes
î<î<a g
)
î<î<g h
;
î<î<h i
}
ï<ï< 	
public
ú<ú< ;
-inventario_facturas_contabilizadas_carguebase
ú<ú< </
!ConsultarExistenciaPeriodoCargado
ú<ú<= ^
(
ú<ú<^ _
int
ú<ú<_ b
mes
ú<ú<c f
,
ú<ú<f g
int
ú<ú<h k
aÃ±o
ú<ú<l o
,
ú<ú<o p
int
ú<ú<q t
regional
ú<ú<u }
)
ú<ú<} ~
{
û<û< 	
return
ü<ü< 
DACConsulta
ü<ü< 
.
ü<ü< /
!ConsultarExistenciaPeriodoCargado
ü<ü< @
(
ü<ü<@ A
mes
ü<ü<A D
,
ü<ü<D E
aÃ±o
ü<ü<F I
,
ü<ü<I J
regional
ü<ü<K S
)
ü<ü<S T
;
ü<ü<T U
}
ı<ı< 	
public
ş<ş< 
List
ş<ş< 
<
ş<ş< I
;management_inventario_facturas_contabilizadas_reporteResult
ş<ş< O
>
ş<ş<O P-
ReporteInventarioContabilizadas
ş<ş<Q p
(
ş<ş<p q
int
ş<ş<q t
estado
ş<ş<u {
)
ş<ş<{ |
{
ÿ<ÿ< 	
return
€=€= 
DACConsulta
€=€= 
.
€=€= -
ReporteInventarioContabilizadas
€=€= >
(
€=€=> ?
estado
€=€=? E
)
€=€=E F
;
€=€=F G
}
== 	
public
†=†= 
int
†=†= &
insercionMasivaAltoCosto
†=†= +
(
†=†=+ ,-
inventario_altoCosto_carguebase
†=†=, K
obj
†=†=L O
,
†=†=O P
List
†=†=Q U
<
†=†=U V*
inventario_altoCosto_detalle
†=†=V r
>
†=†=r s
dtl
†=†=t w
,
†=†=w x
ref
†=†=y |!
MessageResponseOBJ†=†=} 
MsgRes†=†= –
)†=†=– —
{
‡=‡= 	
return
ˆ=ˆ= 

DACInserta
ˆ=ˆ= 
.
ˆ=ˆ= &
insercionMasivaAltoCosto
ˆ=ˆ= 6
(
ˆ=ˆ=6 7
obj
ˆ=ˆ=7 :
,
ˆ=ˆ=: ;
dtl
ˆ=ˆ=< ?
,
ˆ=ˆ=? @
ref
ˆ=ˆ=A D
MsgRes
ˆ=ˆ=E K
)
ˆ=ˆ=K L
;
ˆ=ˆ=L M
}
‰=‰= 	
public
Š=Š= 
List
Š=Š= 
<
Š=Š= ;
-management_inventario_altoCosto_tableroResult
Š=Š= A
>
Š=Š=A B(
ListadoInventarioAltoCosto
Š=Š=C ]
(
Š=Š=] ^
)
Š=Š=^ _
{
‹=‹= 	
return
Œ=Œ= 
DACConsulta
Œ=Œ= 
.
Œ=Œ= (
ListadoInventarioAltoCosto
Œ=Œ= 9
(
Œ=Œ=9 :
)
Œ=Œ=: ;
;
Œ=Œ=; <
}
== 	
public
== 
int
== (
insercionGestionInventario
== -
(
==- .,
inventario_altoCosto_gestiones
==. L
obj
==M P
,
==P Q
ref
==R U 
MessageResponseOBJ
==V h
MsgRes
==i o
)
==o p
{
== 	
return
‘=‘= 

DACInserta
‘=‘= 
.
‘=‘= (
insercionGestionInventario
‘=‘= 8
(
‘=‘=8 9
obj
‘=‘=9 <
,
‘=‘=< =
ref
‘=‘=> A
MsgRes
‘=‘=B H
)
‘=‘=H I
;
‘=‘=I J
}
’=’= 	
public
•=•= 
List
•=•= 
<
•=•= 0
"ref_inventario_altoCostoCancer_atc
•=•= 6
>
•=•=6 7"
listadoInventarioATC
•=•=8 L
(
•=•=L M
)
•=•=M N
{
–=–= 	
return
—=—= 
DACConsulta
—=—= 
.
—=—= "
listadoInventarioATC
—=—= 3
(
—=—=3 4
)
—=—=4 5
;
—=—=5 6
}
˜=˜= 	
public
š=š= 
List
š=š= 
<
š=š= ,
inventario_altoCosto_gestiones
š=š= 2
>
š=š=2 3*
listaInvAltoCostoGestionadas
š=š=4 P
(
š=š=P Q
int
š=š=Q T
?
š=š=T U
	idDetalle
š=š=V _
)
š=š=_ `
{
›=›= 	
return
œ=œ= 
DACConsulta
œ=œ= 
.
œ=œ= *
listaInvAltoCostoGestionadas
œ=œ= ;
(
œ=œ=; <
	idDetalle
œ=œ=< E
)
œ=œ=E F
;
œ=œ=F G
}
== 	
public
Ÿ=Ÿ= ,
inventario_altoCosto_gestiones
Ÿ=Ÿ= -'
DatoInvAltoCostoGestionID
Ÿ=Ÿ=. G
(
Ÿ=Ÿ=G H
int
Ÿ=Ÿ=H K
?
Ÿ=Ÿ=K L
	idGestion
Ÿ=Ÿ=M V
)
Ÿ=Ÿ=V W
{
 = = 	
return
¡=¡= 
DACConsulta
¡=¡= 
.
¡=¡= '
DatoInvAltoCostoGestionID
¡=¡= 8
(
¡=¡=8 9
	idGestion
¡=¡=9 B
)
¡=¡=B C
;
¡=¡=C D
}
¢=¢= 	
public
¤=¤= ,
inventario_altoCosto_gestiones
¤=¤= --
DatoUltimoInvAltoCostoGestionID
¤=¤=. M
(
¤=¤=M N
int
¤=¤=N Q
?
¤=¤=Q R
	idDetalle
¤=¤=S \
)
¤=¤=\ ]
{
¥=¥= 	
return
¦=¦= 
DACConsulta
¦=¦= 
.
¦=¦= -
DatoUltimoInvAltoCostoGestionID
¦=¦= >
(
¦=¦=> ?
	idDetalle
¦=¦=? H
)
¦=¦=H I
;
¦=¦=I J
}
§=§= 	
public
©=©= 
Int32
©=©= .
 InsertarArchivoisAltoCostoCancer
©=©= 5
(
©=©=5 6
List
©=©=6 :
<
©=©=: ;+
inventario_altoCosto_archivos
©=©=; X
>
©=©=X Y
archivos
©=©=Z b
,
©=©=b c
ref
©=©=d g 
MessageResponseOBJ
©=©=h z
MsgRes©=©={ 
)©=©= ‚
{
ª=ª= 	
return
«=«= 

DACInserta
«=«= 
.
«=«= .
 InsertarArchivoisAltoCostoCancer
«=«= >
(
«=«=> ?
archivos
«=«=? G
,
«=«=G H
ref
«=«=I L
MsgRes
«=«=M S
)
«=«=S T
;
«=«=T U
}
¬=¬= 	
public
®=®= 
List
®=®= 
<
®=®= ?
1management_inventario_altoCosto_verArchivosResult
®=®= E
>
®=®=E F(
ListadoArchivosGestionados
®=®=G a
(
®=®=a b
int
®=®=b e
?
®=®=e f
	idGestion
®=®=g p
)
®=®=p q
{
¯=¯= 	
return
°=°= 
DACConsulta
°=°= 
.
°=°= (
ListadoArchivosGestionados
°=°= 9
(
°=°=9 :
	idGestion
°=°=: C
)
°=°=C D
;
°=°=D E
}
±=±= 	
public
³=³= +
inventario_altoCosto_archivos
³=³= ,,
traerArchivoAltoCostoIdArchivo
³=³=- K
(
³=³=K L
int
³=³=L O
?
³=³=O P
	idArchivo
³=³=Q Z
)
³=³=Z [
{
´=´= 	
return
µ=µ= 
DACConsulta
µ=µ= 
.
µ=µ= ,
traerArchivoAltoCostoIdArchivo
µ=µ= =
(
µ=µ== >
	idArchivo
µ=µ=> G
)
µ=µ=G H
;
µ=µ=H I
}
¶=¶= 	
public
·=·= 
int
·=·= 5
'eliminarArchivoAltoCostoCanceridArchivo
·=·= :
(
·=·=: ;
int
·=·=; >
	idArchivo
·=·=? H
)
·=·=H I
{
¸=¸= 	
return
¹=¹= 

DACElimina
¹=¹= 
.
¹=¹= 5
'eliminarArchivoAltoCostoCanceridArchivo
¹=¹= E
(
¹=¹=E F
	idArchivo
¹=¹=F O
)
¹=¹=O P
;
¹=¹=P Q
}
º=º= 	
public
»=»= 
int
»=»= 4
&InsertarLogEliminacionArchivoAltoCosto
»=»= 9
(
»=»=9 ::
,log_inventario_altoCosto_eliminacionArchivos
»=»=: f
obj
»=»=g j
)
»=»=j k
{
¼=¼= 	
return
½=½= 

DACInserta
½=½= 
.
½=½= 4
&InsertarLogEliminacionArchivoAltoCosto
½=½= D
(
½=½=D E
obj
½=½=E H
)
½=½=H I
;
½=½=I J
}
¾=¾= 	
public
À=À= 
List
À=À= 
<
À=À= D
6management_inventario_altoCosto_tableroGestionesResult
À=À= J
>
À=À=J K%
ListaAltoCostoGestiones
À=À=L c
(
À=À=c d
int
À=À=d g
?
À=À=g h
	idDetalle
À=À=i r
)
À=À=r s
{
Á=Á= 	
return
Â=Â= 
DACConsulta
Â=Â= 
.
Â=Â= %
ListaAltoCostoGestiones
Â=Â= 6
(
Â=Â=6 7
	idDetalle
Â=Â=7 @
)
Â=Â=@ A
;
Â=Â=A B
}
Ã=Ã= 	
public
Å=Å= 
List
Å=Å= 
<
Å=Å= *
ref_cargue_cuentas_altoCosto
Å=Å= 0
>
Å=Å=0 1%
listadoCargueGsdRastreo
Å=Å=2 I
(
Å=Å=I J
)
Å=Å=J K
{
Æ=Æ= 	
return
Ç=Ç= 
DACConsulta
Ç=Ç= 
.
Ç=Ç= %
listadoCargueGsdRastreo
Ç=Ç= 6
(
Ç=Ç=6 7
)
Ç=Ç=7 8
;
Ç=Ç=8 9
}
È=È= 	
public
Ê=Ê= 
List
Ê=Ê= 
<
Ê=Ê= 2
$ref_cargue_cuentas_altoCosto_estados
Ê=Ê= 8
>
Ê=Ê=8 9+
listadoEstadosCuentaAltoCosto
Ê=Ê=: W
(
Ê=Ê=W X
)
Ê=Ê=X Y
{
Ë=Ë= 	
return
Ì=Ì= 
DACConsulta
Ì=Ì= 
.
Ì=Ì= +
listadoEstadosCuentaAltoCosto
Ì=Ì= <
(
Ì=Ì=< =
)
Ì=Ì== >
;
Ì=Ì=> ?
}
Í=Í= 	
public
Ï=Ï= 
int
Ï=Ï= +
eliminarDatosCuentasAltoCosto
Ï=Ï= 0
(
Ï=Ï=0 1
int
Ï=Ï=1 4
idCargue
Ï=Ï=5 =
,
Ï=Ï== >
int
Ï=Ï=? B
?
Ï=Ï=B C
tipo
Ï=Ï=D H
)
Ï=Ï=H I
{
Ğ=Ğ= 	
return
Ñ=Ñ= 

DACElimina
Ñ=Ñ= 
.
Ñ=Ñ= +
eliminarDatosCuentasAltoCosto
Ñ=Ñ= ;
(
Ñ=Ñ=; <
idCargue
Ñ=Ñ=< D
,
Ñ=Ñ=D E
tipo
Ñ=Ñ=F J
)
Ñ=Ñ=J K
;
Ñ=Ñ=K L
}
Ò=Ò= 	
public
Ô=Ô= 
int
Ô=Ô= &
cargue_cuentas_altoCosto
Ô=Ô= +
(
Ô=Ô=+ ,&
cargue_cuentas_altoCosto
Ô=Ô=, D
obj
Ô=Ô=E H
,
Ô=Ô=H I
ref
Ô=Ô=J M 
MessageResponseOBJ
Ô=Ô=N `
MsgRes
Ô=Ô=a g
)
Ô=Ô=g h
{
Õ=Õ= 	
return
Ö=Ö= 

DACInserta
Ö=Ö= 
.
Ö=Ö= &
cargue_cuentas_altoCosto
Ö=Ö= 6
(
Ö=Ö=6 7
obj
Ö=Ö=7 :
,
Ö=Ö=: ;
ref
Ö=Ö=< ?
MsgRes
Ö=Ö=@ F
)
Ö=Ö=F G
;
Ö=Ö=G H
}
×=×= 	
public
Ù=Ù= 
int
Ù=Ù= 1
#InsertarCuentasAltoCostoConfirmnada
Ù=Ù= 6
(
Ù=Ù=6 7
List
Ù=Ù=7 ;
<
Ù=Ù=; <1
#cargue_cuentas_altoCosto_confirmada
Ù=Ù=< _
>
Ù=Ù=_ `
List
Ù=Ù=a e
,
Ù=Ù=e f
ref
Ù=Ù=g j 
MessageResponseOBJ
Ù=Ù=k }
MsgResÙ=Ù=~ „
)Ù=Ù=„ …
{
Ú=Ú= 	
return
Û=Û= 

DACInserta
Û=Û= 
.
Û=Û= 1
#InsertarCuentasAltoCostoConfirmnada
Û=Û= A
(
Û=Û=A B
List
Û=Û=B F
,
Û=Û=F G
ref
Û=Û=H K
MsgRes
Û=Û=L R
)
Û=Û=R S
;
Û=Û=S T
}
Ü=Ü= 	
public
Ş=Ş= 
int
Ş=Ş= ,
InsertarCuentasAltoCostoCancer
Ş=Ş= 1
(
Ş=Ş=1 2
List
Ş=Ş=2 6
<
Ş=Ş=6 7-
cargue_cuentas_altoCosto_cancer
Ş=Ş=7 V
>
Ş=Ş=V W
List
Ş=Ş=X \
,
Ş=Ş=\ ]
ref
Ş=Ş=^ a 
MessageResponseOBJ
Ş=Ş=b t
MsgRes
Ş=Ş=u {
)
Ş=Ş={ |
{
ß=ß= 	
return
à=à= 

DACInserta
à=à= 
.
à=à= ,
InsertarCuentasAltoCostoCancer
à=à= <
(
à=à=< =
List
à=à== A
,
à=à=A B
ref
à=à=C F
MsgRes
à=à=G M
)
à=à=M N
;
à=à=N O
}
á=á= 	
public
ã=ã= 
int
ã=ã= ,
GuardarGestionCuentasAltoCosto
ã=ã= 1
(
ã=ã=1 20
"cargue_cuentas_altoCosto_gestiones
ã=ã=2 T
obj
ã=ã=U X
,
ã=ã=X Y
ref
ã=ã=Z ] 
MessageResponseOBJ
ã=ã=^ p
MsgRes
ã=ã=q w
)
ã=ã=w x
{
ä=ä= 	
return
å=å= 

DACInserta
å=å= 
.
å=å= ,
GuardarGestionCuentasAltoCosto
å=å= <
(
å=å=< =
obj
å=å== @
,
å=å=@ A
ref
å=å=B E
MsgRes
å=å=F L
)
å=å=L M
;
å=å=M N
}
æ=æ= 	
public
è=è= 
List
è=è= 
<
è=è= 9
+management_cuentasAltoCosto_gestionesResult
è=è= ?
>
è=è=? @'
listadoGestionesAltoCosto
è=è=A Z
(
è=è=Z [
int
è=è=[ ^
?
è=è=^ _

idRegistro
è=è=` j
,
è=è=j k
int
è=è=l o
?
è=è=o p
tipo
è=è=q u
)
è=è=u v
{
é=é= 	
return
ê=ê= 
DACConsulta
ê=ê= 
.
ê=ê= '
listadoGestionesAltoCosto
ê=ê= 8
(
ê=ê=8 9

idRegistro
ê=ê=9 C
,
ê=ê=C D
tipo
ê=ê=E I
)
ê=ê=I J
;
ê=ê=J K
}
ë=ë= 	
public
í=í= 
int
í=í= /
!InsertarCuentasAltoCostoHemofilia
í=í= 4
(
í=í=4 5
List
í=í=5 9
<
í=í=9 :0
"cargue_cuentas_altoCosto_hemofilia
í=í=: \
>
í=í=\ ]
List
í=í=^ b
,
í=í=b c
ref
í=í=d g 
MessageResponseOBJ
í=í=h z
MsgResí=í={ 
)í=í= ‚
{
î=î= 	
return
ï=ï= 

DACInserta
ï=ï= 
.
ï=ï= /
!InsertarCuentasAltoCostoHemofilia
ï=ï= ?
(
ï=ï=? @
List
ï=ï=@ D
,
ï=ï=D E
ref
ï=ï=F I
MsgRes
ï=ï=J P
)
ï=ï=P Q
;
ï=ï=Q R
}
ğ=ğ= 	
public
ò=ò= 
int
ò=ò= .
 InsertarCuentasAltoCostoArtritis
ò=ò= 3
(
ò=ò=3 4
List
ò=ò=4 8
<
ò=ò=8 9/
!cargue_cuentas_altoCosto_artritis
ò=ò=9 Z
>
ò=ò=Z [
List
ò=ò=\ `
,
ò=ò=` a
ref
ò=ò=b e 
MessageResponseOBJ
ò=ò=f x
MsgRes
ò=ò=y 
)ò=ò= €
{
ó=ó= 	
return
ô=ô= 

DACInserta
ô=ô= 
.
ô=ô= .
 InsertarCuentasAltoCostoArtritis
ô=ô= >
(
ô=ô=> ?
List
ô=ô=? C
,
ô=ô=C D
ref
ô=ô=E H
MsgRes
ô=ô=I O
)
ô=ô=O P
;
ô=ô=P Q
}
õ=õ= 	
public
÷=÷= 
int
÷=÷= )
InsertarCuentasAltoCostoVIH
÷=÷= .
(
÷=÷=. /
List
÷=÷=/ 3
<
÷=÷=3 4*
cargue_cuentas_altoCosto_vih
÷=÷=4 P
>
÷=÷=P Q
List
÷=÷=R V
,
÷=÷=V W
ref
÷=÷=X [ 
MessageResponseOBJ
÷=÷=\ n
MsgRes
÷=÷=o u
)
÷=÷=u v
{
ø=ø= 	
return
ù=ù= 

DACInserta
ù=ù= 
.
ù=ù= )
InsertarCuentasAltoCostoVIH
ù=ù= 9
(
ù=ù=9 :
List
ù=ù=: >
,
ù=ù=> ?
ref
ù=ù=@ C
MsgRes
ù=ù=D J
)
ù=ù=J K
;
ù=ù=K L
}
ú=ú= 	
public
ü=ü= 
List
ü=ü= 
<
ü=ü= 8
*management_cuentasAltoCosto_rastreosResult
ü=ü= >
>
ü=ü=> ?&
ListadoDatosRastreoTotal
ü=ü=@ X
(
ü=ü=X Y
int
ü=ü=Y \
?
ü=ü=\ ]
tipo
ü=ü=^ b
)
ü=ü=b c
{
ı=ı= 	
return
ş=ş= 
DACConsulta
ş=ş= 
.
ş=ş= &
ListadoDatosRastreoTotal
ş=ş= 7
(
ş=ş=7 8
tipo
ş=ş=8 <
)
ş=ş=< =
;
ş=ş== >
}
ÿ=ÿ= 	
public
>> 
List
>> 
<
>> C
5management_cuentasAltoCosto_rastreosConfirmadosResult
>> I
>
>>I J5
'ListadoDatosCuentasAltoCostoConfirmados
>>K r
(
>>r s
int
>>s v
?
>>v w
tipo
>>x |
)
>>| }
{
‚>‚> 	
return
ƒ>ƒ> 
DACConsulta
ƒ>ƒ> 
.
ƒ>ƒ> 5
'ListadoDatosCuentasAltoCostoConfirmados
ƒ>ƒ> F
(
ƒ>ƒ>F G
tipo
ƒ>ƒ>G K
)
ƒ>ƒ>K L
;
ƒ>ƒ>L M
}
„>„> 	
public
ˆ>ˆ> 
List
ˆ>ˆ> 
<
ˆ>ˆ> ;
-management_cuentasAltoCosto_repositorioResult
ˆ>ˆ> A
>
ˆ>ˆ>A B)
CuentasAltoCostoRepositorio
ˆ>ˆ>C ^
(
ˆ>ˆ>^ _
int
ˆ>ˆ>_ b
?
ˆ>ˆ>b c

idRegistro
ˆ>ˆ>d n
,
ˆ>ˆ>n o
int
ˆ>ˆ>p s
?
ˆ>ˆ>s t
tipo
ˆ>ˆ>u y
)
ˆ>ˆ>y z
{
‰>‰> 	
return
Š>Š> 
DACConsulta
Š>Š> 
.
Š>Š> )
CuentasAltoCostoRepositorio
Š>Š> :
(
Š>Š>: ;

idRegistro
Š>Š>; E
,
Š>Š>E F
tipo
Š>Š>G K
)
Š>Š>K L
;
Š>Š>L M
}
‹>‹> 	
public
>> 
List
>> 
<
>> /
!ref_cuentas_altocosto_tipoSoporte
>> 5
>
>>5 6
tipoSoporteCAC
>>7 E
(
>>E F
)
>>F G
{
>> 	
return
>> 
DACConsulta
>> 
.
>> 
tipoSoporteCAC
>> -
(
>>- .
)
>>. /
;
>>/ 0
}
>> 	
public
’>’> /
!cargue_cuentas_altoCosto_archivos
’>’> 0%
TraerArchivoRepositorio
’>’>1 H
(
’>’>H I
int
’>’>I L
?
’>’>L M
	idArchivo
’>’>N W
)
’>’>W X
{
“>“> 	
return
”>”> 
DACConsulta
”>”> 
.
”>”> %
TraerArchivoRepositorio
”>”> 6
(
”>”>6 7
	idArchivo
”>”>7 @
)
”>”>@ A
;
”>”>A B
}
•>•> 	
public
—>—> 
Int32
—>—> +
InsertarArchivoReposAltoCosto
—>—> 2
(
—>—>2 3/
!cargue_cuentas_altoCosto_archivos
—>—>3 T
OBJ
—>—>U X
,
—>—>X Y
ref
—>—>Z ] 
MessageResponseOBJ
—>—>^ p
MsgRes
—>—>q w
)
—>—>w x
{
˜>˜> 	
return
™>™> 

DACInserta
™>™> 
.
™>™> +
InsertarArchivoReposAltoCosto
™>™> ;
(
™>™>; <
OBJ
™>™>< ?
,
™>™>? @
ref
™>™>A D
MsgRes
™>™>E K
)
™>™>K L
;
™>™>L M
}
š>š> 	
public
œ>œ> 
int
œ>œ> 1
#eliminarArchivoRepositorioAltoCosto
œ>œ> 6
(
œ>œ>6 7
int
œ>œ>7 :
id
œ>œ>; =
)
œ>œ>= >
{
>> 	
return
>> 

DACElimina
>> 
.
>> 1
#eliminarArchivoRepositorioAltoCosto
>> A
(
>>A B
id
>>B D
)
>>D E
;
>>E F
}
Ÿ>Ÿ> 	
public
¡>¡> 
Int32
¡>¡> &
LogArchivoReposAltoCosto
¡>¡> -
(
¡>¡>- .3
%log_cargue_cuentas_altoCosto_archivos
¡>¡>. S
OBJ
¡>¡>T W
,
¡>¡>W X
ref
¡>¡>Y \ 
MessageResponseOBJ
¡>¡>] o
MsgRes
¡>¡>p v
)
¡>¡>v w
{
¢>¢> 	
return
£>£> 

DACInserta
£>£> 
.
£>£> &
LogArchivoReposAltoCosto
£>£> 6
(
£>£>6 7
OBJ
£>£>7 :
,
£>£>: ;
ref
£>£>< ?
MsgRes
£>£>@ F
)
£>£>F G
;
£>£>G H
}
¤>¤> 	
public
¦>¦> 
List
¦>¦> 
<
¦>¦> N
@management_cuentasAltoCosto_rastreosConfirmados_conArchivoResult
¦>¦> T
>
¦>¦>T UA
2ListadoDatosCuentasAltoCostoConfirmadosConArchivos¦>¦>V ˆ
(¦>¦>ˆ ‰
)¦>¦>‰ Š
{
§>§> 	
return
¨>¨> 
DACConsulta
¨>¨> 
.
¨>¨> @
2ListadoDatosCuentasAltoCostoConfirmadosConArchivos
¨>¨> Q
(
¨>¨>Q R
)
¨>¨>R S
;
¨>¨>S T
}
©>©> 	
public
«>«> 
List
«>«> 
<
«>«> V
Hmanagement_cuentasAltoCosto_rastreosConfirmados_conArchivoCompletaResult
«>«> \
>
«>«>\ ]J
;ListadoDatosCuentasAltoCostoConfirmadosConArchivosDetallada«>«>^ ™
(«>«>™ š
)«>«>š ›
{
¬>¬> 	
return
­>­> 
DACConsulta
­>­> 
.
­>­> I
;ListadoDatosCuentasAltoCostoConfirmadosConArchivosDetallada
­>­> Z
(
­>­>Z [
)
­>­>[ \
;
­>­>\ ]
}
®>®> 	
public
°>°> 
List
°>°> 
<
°>°> Q
Cmanagement_cuentasAltoCosto_rastreosConfirmados_observacionesResult
°>°> W
>
°>°>W X>
/ListadoObservacionesCuentasAltoCostoGestionadas°>°>Y ˆ
(°>°>ˆ ‰
int°>°>‰ Œ
?°>°>Œ 

idRegistro°>°> ˜
,°>°>˜ ™
int°>°>š 
?°>°> 
tipo°>°>Ÿ £
)°>°>£ ¤
{
±>±> 	
return
²>²> 
DACConsulta
²>²> 
.
²>²> =
/ListadoObservacionesCuentasAltoCostoGestionadas
²>²> N
(
²>²>N O

idRegistro
²>²>O Y
,
²>²>Y Z
tipo
²>²>[ _
)
²>²>_ `
;
²>²>` a
}
³>³> 	
public
µ>µ> 
Int32
µ>µ> 2
$GuardarObservacionesCuentasAltoCosto
µ>µ> 9
(
µ>µ>9 :4
&cargue_cuentas_altoCosto_observaciones
µ>µ>: `
OBJ
µ>µ>a d
,
µ>µ>d e
ref
µ>µ>f i 
MessageResponseOBJ
µ>µ>j |
MsgResµ>µ>} ƒ
)µ>µ>ƒ „
{
¶>¶> 	
return
·>·> 

DACInserta
·>·> 
.
·>·> 2
$GuardarObservacionesCuentasAltoCosto
·>·> B
(
·>·>B C
OBJ
·>·>C F
,
·>·>F G
ref
·>·>H K
MsgRes
·>·>L R
)
·>·>R S
;
·>·>S T
}
¸>¸> 	
public
º>º> 
int
º>º> *
eliminarObservacionAltoCosto
º>º> /
(
º>º>/ 0
int
º>º>0 3
id
º>º>4 6
)
º>º>6 7
{
»>»> 	
return
¼>¼> 

DACElimina
¼>¼> 
.
¼>¼> *
eliminarObservacionAltoCosto
¼>¼> :
(
¼>¼>: ;
id
¼>¼>; =
)
¼>¼>= >
;
¼>¼>> ?
}
½>½> 	
public
¿>¿> 
List
¿>¿> 
<
¿>¿> C
5management_cuentasAltoCosto_consolidadoArchivosResult
¿>¿> I
>
¿>¿>I J*
ListaArchivosPorDocumentoCAC
¿>¿>K g
(
¿>¿>g h
string
¿>¿>h n
	documento
¿>¿>o x
,
¿>¿>x y
int
¿>¿>z }
?
¿>¿>} ~
tipo¿>¿> ƒ
)¿>¿>ƒ „
{
À>À> 	
return
Á>Á> 
DACConsulta
Á>Á> 
.
Á>Á> *
ListaArchivosPorDocumentoCAC
Á>Á> ;
(
Á>Á>; <
	documento
Á>Á>< E
,
Á>Á>E F
tipo
Á>Á>G K
)
Á>Á>K L
;
Á>Á>L M
}
Â>Â> 	
public
Ä>Ä> 
List
Ä>Ä> 
<
Ä>Ä> B
4management_cuentasAltoCosto_documentosArchivosResult
Ä>Ä> H
>
Ä>Ä>H I#
DocumentosConArchivos
Ä>Ä>J _
(
Ä>Ä>_ `
int
Ä>Ä>` c
?
Ä>Ä>c d
tipo
Ä>Ä>e i
)
Ä>Ä>i j
{
Å>Å> 	
return
Æ>Æ> 
DACConsulta
Æ>Æ> 
.
Æ>Æ> #
DocumentosConArchivos
Æ>Æ> 4
(
Æ>Æ>4 5
tipo
Æ>Æ>5 9
)
Æ>Æ>9 :
;
Æ>Æ>: ;
}
Ç>Ç> 	
public
Í>Í> 
int
Í>Í> #
CargueMasivoContratos
Í>Í> (
(
Í>Í>( )
contratos_cargue
Í>Í>) 9
obj
Í>Í>: =
,
Í>Í>= >
List
Í>Í>? C
<
Í>Í>C D
contratos_detalle
Í>Í>D U
>
Í>Í>U V
detalle
Í>Í>W ^
,
Í>Í>^ _
ref
Í>Í>` c 
MessageResponseOBJ
Í>Í>d v
MsgRes
Í>Í>w }
)
Í>Í>} ~
{
Î>Î> 	
return
Ï>Ï> 

DACInserta
Ï>Ï> 
.
Ï>Ï> #
CargueMasivoContratos
Ï>Ï> 3
(
Ï>Ï>3 4
obj
Ï>Ï>4 7
,
Ï>Ï>7 8
detalle
Ï>Ï>9 @
,
Ï>Ï>@ A
ref
Ï>Ï>B E
MsgRes
Ï>Ï>F L
)
Ï>Ï>L M
;
Ï>Ï>M N
}
Ğ>Ğ> 	
public
Ò>Ò> 
List
Ò>Ò> 
<
Ò>Ò> 0
"management_contratos_listadoResult
Ò>Ò> 6
>
Ò>Ò>6 7
listadoContratos
Ò>Ò>8 H
(
Ò>Ò>H I
)
Ò>Ò>I J
{
Ó>Ó> 	
return
Ô>Ô> 
DACConsulta
Ô>Ô> 
.
Ô>Ô> 
listadoContratos
Ô>Ô> /
(
Ô>Ô>/ 0
)
Ô>Ô>0 1
;
Ô>Ô>1 2
}
Õ>Õ> 	
public
Ö>Ö> 
contratos_detalle
Ö>Ö>  $
MostrarDatosContratoId
Ö>Ö>! 7
(
Ö>Ö>7 8
int
Ö>Ö>8 ;
?
Ö>Ö>; <

idContrato
Ö>Ö>= G
)
Ö>Ö>G H
{
×>×> 	
return
Ø>Ø> 
DACConsulta
Ø>Ø> 
.
Ø>Ø> $
MostrarDatosContratoId
Ø>Ø> 5
(
Ø>Ø>5 6

idContrato
Ø>Ø>6 @
)
Ø>Ø>@ A
;
Ø>Ø>A B
}
Ù>Ù> 	
public
Ú>Ú> 
int
Ú>Ú>  
ActualizarContrato
Ú>Ú> %
(
Ú>Ú>% &
contratos_detalle
Ú>Ú>& 7
obj
Ú>Ú>8 ;
,
Ú>Ú>; <
ref
Ú>Ú>= @ 
MessageResponseOBJ
Ú>Ú>A S
MsgRes
Ú>Ú>T Z
)
Ú>Ú>Z [
{
Û>Û> 	
return
Ü>Ü> 
DACActualiza
Ü>Ü> 
.
Ü>Ü>   
ActualizarContrato
Ü>Ü>  2
(
Ü>Ü>2 3
obj
Ü>Ü>3 6
,
Ü>Ü>6 7
ref
Ü>Ü>8 ;
MsgRes
Ü>Ü>< B
)
Ü>Ü>B C
;
Ü>Ü>C D
}
İ>İ> 	
public
ß>ß> 
contratos_detalle
ß>ß>  %
MostrarDetallePContrato
ß>ß>! 8
(
ß>ß>8 9
string
ß>ß>9 ?
sap
ß>ß>@ C
)
ß>ß>C D
{
à>à> 	
return
á>á> 
DACConsulta
á>á> 
.
á>á> %
MostrarDetallePContrato
á>á> 6
(
á>á>6 7
sap
á>á>7 :
)
á>á>: ;
;
á>á>; <
}
â>â> 	
public
ä>ä> 
int
ä>ä> ,
InsertarContratoNuevoPrestador
ä>ä> 1
(
ä>ä>1 2
contratos_detalle
ä>ä>2 C
obj
ä>ä>D G
)
ä>ä>G H
{
å>å> 	
return
æ>æ> 

DACInserta
æ>æ> 
.
æ>æ> ,
InsertarContratoNuevoPrestador
æ>æ> <
(
æ>æ>< =
obj
æ>æ>= @
)
æ>æ>@ A
;
æ>æ>A B
}
ç>ç> 	
public
ì>ì> 
List
ì>ì> 
<
ì>ì> 0
"management_usuarios_regionalResult
ì>ì> 6
>
ì>ì>6 7$
ListadoRegionalUsuario
ì>ì>8 N
(
ì>ì>N O
)
ì>ì>O P
{
í>í> 	
return
î>î> 
DACComonClass
î>î>  
.
î>î>  !$
ListadoRegionalUsuario
î>î>! 7
(
î>î>7 8
)
î>î>8 9
;
î>î>9 :
}
ï>ï> 	
public
ü>ü> '
rips_depurados_carguebase
ü>ü> (.
 ConsultarCargueBaseRipsDepurados
ü>ü>) I
(
ü>ü>I J
string
ü>ü>J P
tipoRips
ü>ü>Q Y
,
ü>ü>Y Z
int
ü>ü>[ ^
mes
ü>ü>_ b
,
ü>ü>b c
int
ü>ü>d g
aÃ±o
ü>ü>h k
)
ü>ü>k l
{
ı>ı> 	
return
ş>ş> 
DACConsulta
ş>ş> 
.
ş>ş> .
 ConsultarCargueBaseRipsDepurados
ş>ş> ?
(
ş>ş>? @
tipoRips
ş>ş>@ H
,
ş>ş>H I
mes
ş>ş>J M
,
ş>ş>M N
aÃ±o
ş>ş>O R
)
ş>ş>R S
;
ş>ş>S T
}
ÿ>ÿ> 	
public
‰?‰? 
int
‰?‰? ,
GuardarCargueBaseRipsDepurados
‰?‰? 1
(
‰?‰?1 2'
rips_depurados_carguebase
‰?‰?2 K
cb
‰?‰?L N
,
‰?‰?N O
ref
‰?‰?P S 
MessageResponseOBJ
‰?‰?T f
MsgRes
‰?‰?g m
)
‰?‰?m n
{
Š?Š? 	
return
‹?‹? 

DACInserta
‹?‹? 
.
‹?‹? ,
GuardarCargueBaseRipsDepurados
‹?‹? <
(
‹?‹?< =
cb
‹?‹?= ?
,
‹?‹?? @
ref
‹?‹?A D
MsgRes
‹?‹?E K
)
‹?‹?K L
;
‹?‹?L M
}
Œ?Œ? 	
public
•?•? 
void
•?•? 1
#InsertarCargueMasivoRipsDepuradosAC
•?•? 7
(
•?•?7 8
List
•?•?8 <
<
•?•?< =*
rips_depurados_ac_carguedtll
•?•?= Y
>
•?•?Y Z
cdtll
•?•?[ `
,
•?•?` a
ref
•?•?b e 
MessageResponseOBJ
•?•?f x
MsgRes
•?•?y 
)•?•? €
{
–?–? 	

DACInserta
—?—? 
.
—?—? 1
#InsertarCargueMasivoRipsDepuradosAC
—?—? :
(
—?—?: ;
cdtll
—?—?; @
,
—?—?@ A
ref
—?—?B E
MsgRes
—?—?F L
)
—?—?L M
;
—?—?M N
}
˜?˜? 	
public
¡?¡? 
void
¡?¡? 1
#InsertarCargueMasivoRipsDepuradosAP
¡?¡? 7
(
¡?¡?7 8
List
¡?¡?8 <
<
¡?¡?< =*
rips_depurados_ap_carguedtll
¡?¡?= Y
>
¡?¡?Y Z
cdtll
¡?¡?[ `
,
¡?¡?` a
ref
¡?¡?b e 
MessageResponseOBJ
¡?¡?f x
MsgRes
¡?¡?y 
)¡?¡? €
{
¢?¢? 	

DACInserta
£?£? 
.
£?£? 1
#InsertarCargueMasivoRipsDepuradosAP
£?£? :
(
£?£?: ;
cdtll
£?£?; @
,
£?£?@ A
ref
£?£?B E
MsgRes
£?£?F L
)
£?£?L M
;
£?£?M N
}
¤?¤? 	
public
­?­? 
void
­?­? 1
#InsertarCargueMasivoRipsDepuradosAU
­?­? 7
(
­?­?7 8
List
­?­?8 <
<
­?­?< =*
rips_depurados_au_carguedtll
­?­?= Y
>
­?­?Y Z
cdtll
­?­?[ `
,
­?­?` a
ref
­?­?b e 
MessageResponseOBJ
­?­?f x
MsgRes
­?­?y 
)­?­? €
{
®?®? 	

DACInserta
¯?¯? 
.
¯?¯? 1
#InsertarCargueMasivoRipsDepuradosAU
¯?¯? :
(
¯?¯?: ;
cdtll
¯?¯?; @
,
¯?¯?@ A
ref
¯?¯?B E
MsgRes
¯?¯?F L
)
¯?¯?L M
;
¯?¯?M N
}
°?°? 	
public
¹?¹? 
void
¹?¹? 1
#InsertarCargueMasivoRipsDepuradosAM
¹?¹? 7
(
¹?¹?7 8
List
¹?¹?8 <
<
¹?¹?< =*
rips_depurados_am_carguedtll
¹?¹?= Y
>
¹?¹?Y Z
cdtll
¹?¹?[ `
,
¹?¹?` a
ref
¹?¹?b e 
MessageResponseOBJ
¹?¹?f x
MsgRes
¹?¹?y 
)¹?¹? €
{
º?º? 	

DACInserta
»?»? 
.
»?»? 1
#InsertarCargueMasivoRipsDepuradosAM
»?»? :
(
»?»?: ;
cdtll
»?»?; @
,
»?»?@ A
ref
»?»?B E
MsgRes
»?»?F L
)
»?»?L M
;
»?»?M N
}
¼?¼? 	
public
Å?Å? 
void
Å?Å? 1
#InsertarCargueMasivoRipsDepuradosAN
Å?Å? 7
(
Å?Å?7 8
List
Å?Å?8 <
<
Å?Å?< =*
rips_depurados_an_carguedtll
Å?Å?= Y
>
Å?Å?Y Z
cdtll
Å?Å?[ `
,
Å?Å?` a
ref
Å?Å?b e 
MessageResponseOBJ
Å?Å?f x
MsgRes
Å?Å?y 
)Å?Å? €
{
Æ?Æ? 	

DACInserta
Ç?Ç? 
.
Ç?Ç? 1
#InsertarCargueMasivoRipsDepuradosAN
Ç?Ç? :
(
Ç?Ç?: ;
cdtll
Ç?Ç?; @
,
Ç?Ç?@ A
ref
Ç?Ç?B E
MsgRes
Ç?Ç?F L
)
Ç?Ç?L M
;
Ç?Ç?M N
}
È?È? 	
public
Ê?Ê? 
int
Ê?Ê? #
InsertarPrestadorRIPS
Ê?Ê? (
(
Ê?Ê?( )"
Ref_RIPS_Prestadores
Ê?Ê?) =
obj
Ê?Ê?> A
)
Ê?Ê?A B
{
Ë?Ë? 	
return
Ì?Ì? 

DACInserta
Ì?Ì? 
.
Ì?Ì? #
InsertarPrestadorRIPS
Ì?Ì? 3
(
Ì?Ì?3 4
obj
Ì?Ì?4 7
)
Ì?Ì?7 8
;
Ì?Ì?8 9
}
Í?Í? 	
public
Ğ?Ğ? 
int
Ğ?Ğ? $
InsertarPrestadorRIPS2
Ğ?Ğ? )
(
Ğ?Ğ?) *"
Ref_RIPS_Prestadores
Ğ?Ğ?* >
obj
Ğ?Ğ?? B
)
Ğ?Ğ?B C
{
Ñ?Ñ? 	
return
Ò?Ò? 

DACInserta
Ò?Ò? 
.
Ò?Ò? $
InsertarPrestadorRIPS2
Ò?Ò? 4
(
Ò?Ò?4 5
obj
Ò?Ò?5 8
)
Ò?Ò?8 9
;
Ò?Ò?9 :
}
Ó?Ó? 	
public
Õ?Õ? 
List
Õ?Õ? 
<
Õ?Õ? "
Ref_RIPS_Prestadores
Õ?Õ? (
>
Õ?Õ?( )(
ConsultaPrestadoresRipsNit
Õ?Õ?* D
(
Õ?Õ?D E
double
Õ?Õ?E K
nit
Õ?Õ?L O
,
Õ?Õ?O P
ref
Õ?Õ?Q T 
MessageResponseOBJ
Õ?Õ?U g
MsgRes
Õ?Õ?h n
)
Õ?Õ?n o
{
Ö?Ö? 	
return
×?×? 
DACConsulta
×?×? 
.
×?×? (
ConsultaPrestadoresRipsNit
×?×? 9
(
×?×?9 :
nit
×?×?: =
,
×?×?= >
ref
×?×?? B
MsgRes
×?×?C I
)
×?×?I J
;
×?×?J K
}
Ø?Ø? 	
public
Ú?Ú? 
List
Ú?Ú? 
<
Ú?Ú? "
Ref_RIPS_Prestadores
Ú?Ú? (
>
Ú?Ú?( )0
"ConsultaPrestadoresRipsIdPrestador
Ú?Ú?* L
(
Ú?Ú?L M
string
Ú?Ú?M S
IDPrestador
Ú?Ú?T _
,
Ú?Ú?_ `
ref
Ú?Ú?a d 
MessageResponseOBJ
Ú?Ú?e w
MsgRes
Ú?Ú?x ~
)
Ú?Ú?~ 
{
Û?Û? 	
return
Ü?Ü? 
DACConsulta
Ü?Ü? 
.
Ü?Ü? 0
"ConsultaPrestadoresRipsIdPrestador
Ü?Ü? A
(
Ü?Ü?A B
IDPrestador
Ü?Ü?B M
,
Ü?Ü?M N
ref
Ü?Ü?O R
MsgRes
Ü?Ü?S Y
)
Ü?Ü?Y Z
;
Ü?Ü?Z [
}
İ?İ? 	
public
ç?ç? 
void
ç?ç? 1
#InsertarCargueMasivoRipsDepuradosAH
ç?ç? 7
(
ç?ç?7 8
List
ç?ç?8 <
<
ç?ç?< =*
rips_depurados_ah_carguedtll
ç?ç?= Y
>
ç?ç?Y Z
cdtll
ç?ç?[ `
,
ç?ç?` a
ref
ç?ç?b e 
MessageResponseOBJ
ç?ç?f x
MsgRes
ç?ç?y 
)ç?ç? €
{
è?è? 	

DACInserta
é?é? 
.
é?é? 1
#InsertarCargueMasivoRipsDepuradosAH
é?é? :
(
é?é?: ;
cdtll
é?é?; @
,
é?é?@ A
ref
é?é?B E
MsgRes
é?é?F L
)
é?é?L M
;
é?é?M N
}
ê?ê? 	
public
ó?ó? 
void
ó?ó? 2
$EliminarRipsDepuradosCargueBasePorId
ó?ó? 8
(
ó?ó?8 9
int
ó?ó?9 <
idCargueBase
ó?ó?= I
)
ó?ó?I J
{
ô?ô? 	

DACElimina
õ?õ? 
.
õ?õ? 2
$EliminarRipsDepuradosCargueBasePorId
õ?õ? ;
(
õ?õ?; <
idCargueBase
õ?õ?< H
)
õ?õ?H I
;
õ?õ?I J
}
ö?ö? 	
public
ı?ı? 
int
ı?ı? 
InsertarRembolso
ı?ı? #
(
ı?ı?# $ 
cuentas_reembolsos
ı?ı?$ 6
obj
ı?ı?7 :
)
ı?ı?: ;
{
ş?ş? 	
return
ÿ?ÿ? 

DACInserta
ÿ?ÿ? 
.
ÿ?ÿ? 
InsertarRembolso
ÿ?ÿ? .
(
ÿ?ÿ?. /
obj
ÿ?ÿ?/ 2
)
ÿ?ÿ?2 3
;
ÿ?ÿ?3 4
}
€@€@ 	
public
‚@‚@ 
int
‚@‚@ %
InsertarRembolsoDetalle
‚@‚@ *
(
‚@‚@* +'
cuentas_reembolso_detalle
‚@‚@+ D
obj
‚@‚@E H
)
‚@‚@H I
{
ƒ@ƒ@ 	
return
„@„@ 

DACInserta
„@„@ 
.
„@„@ %
InsertarRembolsoDetalle
„@„@ 5
(
„@„@5 6
obj
„@„@6 9
)
„@„@9 :
;
„@„@: ;
}
…@…@ 	
public
‡@‡@ 
int
‡@‡@ &
InsertarRembolsoArchivos
‡@‡@ +
(
‡@‡@+ ,)
cuentas_reembolsos_archivos
‡@‡@, G
obj
‡@‡@H K
)
‡@‡@K L
{
ˆ@ˆ@ 	
return
‰@‰@ 

DACInserta
‰@‰@ 
.
‰@‰@ &
InsertarRembolsoArchivos
‰@‰@ 6
(
‰@‰@6 7
obj
‰@‰@7 :
)
‰@‰@: ;
;
‰@‰@; <
}
Š@Š@ 	
public
‹@‹@ 
List
‹@‹@ 
<
‹@‹@ 
ref_tipo_moneda
‹@‹@ #
>
‹@‹@# $

TipoMoneda
‹@‹@% /
(
‹@‹@/ 0
)
‹@‹@0 1
{
Œ@Œ@ 	
return
@@ 
DACComonClass
@@  
.
@@  !

TipoMoneda
@@! +
(
@@+ ,
)
@@, -
;
@@- .
}
@@ 	
public
@@ 
List
@@ 
<
@@ "
ref_estado_reembolso
@@ (
>
@@( )
EstadoReembolso
@@* 9
(
@@9 :
)
@@: ;
{
‘@‘@ 	
return
’@’@ 
DACComonClass
’@’@  
.
’@’@  !
EstadoReembolso
’@’@! 0
(
’@’@0 1
)
’@’@1 2
;
’@’@2 3
}
“@“@ 	
public
•@•@ 
List
•@•@ 
<
•@•@  
ref_tipo_reembolso
•@•@ &
>
•@•@& '
TipoReembolso
•@•@( 5
(
•@•@5 6
)
•@•@6 7
{
–@–@ 	
return
—@—@ 
DACComonClass
—@—@  
.
—@—@  !
TipoReembolso
—@—@! .
(
—@—@. /
)
—@—@/ 0
;
—@—@0 1
}
˜@˜@ 	
public
š@š@ 
List
š@š@ 
<
š@š@ 1
#management_reembolsos_tableroResult
š@š@ 7
>
š@š@7 8)
listadoReembolsosIngresados
š@š@9 T
(
š@š@T U
int
š@š@U X
?
š@š@X Y

idRegional
š@š@Z d
)
š@š@d e
{
›@›@ 	
return
œ@œ@ 
DACConsulta
œ@œ@ 
.
œ@œ@ )
listadoReembolsosIngresados
œ@œ@ :
(
œ@œ@: ;

idRegional
œ@œ@; E
)
œ@œ@E F
;
œ@œ@F G
}
@@ 	
public
Ÿ@Ÿ@ 
List
Ÿ@Ÿ@ 
<
Ÿ@Ÿ@ =
/management_reembolsos_tablero_gestionadosResult
Ÿ@Ÿ@ C
>
Ÿ@Ÿ@C D*
listadoReembolsosGestionados
Ÿ@Ÿ@E a
(
Ÿ@Ÿ@a b
int
Ÿ@Ÿ@b e
?
Ÿ@Ÿ@e f

idRegional
Ÿ@Ÿ@g q
)
Ÿ@Ÿ@q r
{
 @ @ 	
return
¡@¡@ 
DACConsulta
¡@¡@ 
.
¡@¡@ *
listadoReembolsosGestionados
¡@¡@ ;
(
¡@¡@; <

idRegional
¡@¡@< F
)
¡@¡@F G
;
¡@¡@G H
}
¢@¢@ 	
public
¤@¤@ 
List
¤@¤@ 
<
¤@¤@ !
ref_unis_reembolsos
¤@¤@ '
>
¤@¤@' (
UnisReembolsos
¤@¤@) 7
(
¤@¤@7 8
)
¤@¤@8 9
{
¥@¥@ 	
return
¦@¦@ 
DACConsulta
¦@¦@ 
.
¦@¦@ 
UnisReembolsos
¦@¦@ -
(
¦@¦@- .
)
¦@¦@. /
;
¦@¦@/ 0
}
§@§@ 	
public
©@©@ 
List
©@©@ 
<
©@©@ 1
#management_reembolsos_gestionResult
©@©@ 7
>
©@©@7 82
$listadoReembolsosIngresadosGestiones
©@©@9 ]
(
©@©@] ^
int
©@©@^ a
?
©@©@a b
idReembolso
©@©@c n
)
©@©@n o
{
ª@ª@ 	
return
«@«@ 
DACConsulta
«@«@ 
.
«@«@ 2
$listadoReembolsosIngresadosGestiones
«@«@ C
(
«@«@C D
idReembolso
«@«@D O
)
«@«@O P
;
«@«@P Q
}
¬@¬@ 	
public
®@®@ 
List
®@®@ 
<
®@®@ 9
+management_cuentas_reembolso_ArchivosResult
®@®@ ?
>
®@®@? @'
listadoReembolsosArchivos
®@®@A Z
(
®@®@Z [
int
®@®@[ ^
?
®@®@^ _
idReembolso
®@®@` k
)
®@®@k l
{
¯@¯@ 	
return
°@°@ 
DACConsulta
°@°@ 
.
°@°@ '
listadoReembolsosArchivos
°@°@ 8
(
°@°@8 9
idReembolso
°@°@9 D
)
°@°@D E
;
°@°@E F
}
±@±@ 	
public
²@²@ 
int
²@²@ '
ActualizarEstadoReembolso
²@²@ ,
(
²@²@, - 
cuentas_reembolsos
²@²@- ?
reem
²@²@@ D
)
²@²@D E
{
³@³@ 	
return
´@´@ 
DACActualiza
´@´@ 
.
´@´@  '
ActualizarEstadoReembolso
´@´@  9
(
´@´@9 :
reem
´@´@: >
)
´@´@> ?
;
´@´@? @
}
µ@µ@ 	
public
·@·@ 
int
·@·@ &
ActualizarDatosReembolso
·@·@ +
(
·@·@+ , 
cuentas_reembolsos
·@·@, >
reem
·@·@? C
)
·@·@C D
{
¸@¸@ 	
return
¹@¹@ 
DACActualiza
¹@¹@ 
.
¹@¹@  &
ActualizarDatosReembolso
¹@¹@  8
(
¹@¹@8 9
reem
¹@¹@9 =
)
¹@¹@= >
;
¹@¹@> ?
}
º@º@ 	
public
¼@¼@ 
int
¼@¼@ '
EliminarArchivoReembolsos
¼@¼@ ,
(
¼@¼@, -
int
¼@¼@- 0
?
¼@¼@0 1
	idArchivo
¼@¼@2 ;
)
¼@¼@; <
{
½@½@ 	
return
¾@¾@ 

DACElimina
¾@¾@ 
.
¾@¾@ '
EliminarArchivoReembolsos
¾@¾@ 7
(
¾@¾@7 8
	idArchivo
¾@¾@8 A
)
¾@¾@A B
;
¾@¾@B C
}
¿@¿@ 	
public
Á@Á@  
cuentas_reembolsos
Á@Á@ !!
TraerDatosReembolso
Á@Á@" 5
(
Á@Á@5 6
int
Á@Á@6 9
?
Á@Á@9 :
idReembolso
Á@Á@; F
)
Á@Á@F G
{
Â@Â@ 	
return
Ã@Ã@ 
DACConsulta
Ã@Ã@ 
.
Ã@Ã@ !
TraerDatosReembolso
Ã@Ã@ 2
(
Ã@Ã@2 3
idReembolso
Ã@Ã@3 >
)
Ã@Ã@> ?
;
Ã@Ã@? @
}
Ä@Ä@ 	
public
Æ@Æ@ )
cuentas_reembolsos_archivos
Æ@Æ@ *"
TrarArchivoReembolso
Æ@Æ@+ ?
(
Æ@Æ@? @
int
Æ@Æ@@ C
?
Æ@Æ@C D
	idArchivo
Æ@Æ@E N
)
Æ@Æ@N O
{
Ç@Ç@ 	
return
È@È@ 
DACConsulta
È@È@ 
.
È@È@ "
TrarArchivoReembolso
È@È@ 3
(
È@È@3 4
	idArchivo
È@È@4 =
)
È@È@= >
;
È@È@> ?
}
É@É@ 	
public
Ï@Ï@ 
int
Ï@Ï@ 
InsertarNoRips
Ï@Ï@ !
(
Ï@Ï@! "$
cuentas_medicas_norips
Ï@Ï@" 8
obj
Ï@Ï@9 <
,
Ï@Ï@< =
ref
Ï@Ï@> A 
MessageResponseOBJ
Ï@Ï@B T
MsgRes
Ï@Ï@U [
)
Ï@Ï@[ \
{
Ğ@Ğ@ 	
return
Ñ@Ñ@ 

DACInserta
Ñ@Ñ@ 
.
Ñ@Ñ@ 
InsertarNoRips
Ñ@Ñ@ ,
(
Ñ@Ñ@, -
obj
Ñ@Ñ@- 0
,
Ñ@Ñ@0 1
ref
Ñ@Ñ@2 5
MsgRes
Ñ@Ñ@6 <
)
Ñ@Ñ@< =
;
Ñ@Ñ@= >
}
Ò@Ò@ 	
public
Ô@Ô@ 
int
Ô@Ô@  
EliminarCasoNoRips
Ô@Ô@ %
(
Ô@Ô@% &
int
Ô@Ô@& )
?
Ô@Ô@) *
idMed
Ô@Ô@+ 0
)
Ô@Ô@0 1
{
Õ@Õ@ 	
return
Ö@Ö@ 

DACElimina
Ö@Ö@ 
.
Ö@Ö@  
EliminarCasoNoRips
Ö@Ö@ 0
(
Ö@Ö@0 1
idMed
Ö@Ö@1 6
)
Ö@Ö@6 7
;
Ö@Ö@7 8
}
×@×@ 	
public
Ù@Ù@ 
List
Ù@Ù@ 
<
Ù@Ù@ 7
)management_usuariosAnalistas_noripsResult
Ù@Ù@ =
>
Ù@Ù@= >
ListadoAnalistas
Ù@Ù@? O
(
Ù@Ù@O P
)
Ù@Ù@P Q
{
Ú@Ú@ 	
return
Û@Û@ 
DACConsulta
Û@Û@ 
.
Û@Û@ 
ListadoAnalistas
Û@Û@ /
(
Û@Û@/ 0
)
Û@Û@0 1
;
Û@Û@1 2
}
Ü@Ü@ 	
public
Ş@Ş@ 
List
Ş@Ş@ 
<
Ş@Ş@  
Total_Departamento
Ş@Ş@ &
>
Ş@Ş@& ' 
TraerDepartamentos
Ş@Ş@( :
(
Ş@Ş@: ;
)
Ş@Ş@; <
{
ß@ß@ 	
return
à@à@ 
DACConsulta
à@à@ 
.
à@à@  
TraerDepartamentos
à@à@ 1
(
à@à@1 2
)
à@à@2 3
;
à@à@3 4
}
á@á@ 	
public
â@â@  
Total_Departamento
â@â@ !!
TraerDepartamentoId
â@â@" 5
(
â@â@5 6
int
â@â@6 9
?
â@â@9 :
id
â@â@; =
)
â@â@= >
{
ã@ã@ 	
return
ä@ä@ 
DACConsulta
ä@ä@ 
.
ä@ä@ !
TraerDepartamentoId
ä@ä@ 2
(
ä@ä@2 3
id
ä@ä@3 5
)
ä@ä@5 6
;
ä@ä@6 7
}
å@å@ 	
public
ç@ç@ 
List
ç@ç@ 
<
ç@ç@ 2
$management_total_departamentosResult
ç@ç@ 8
>
ç@ç@8 9(
TraerDepartamentosRegional
ç@ç@: T
(
ç@ç@T U
int
ç@ç@U X
?
ç@ç@X Y
regional
ç@ç@Z b
)
ç@ç@b c
{
è@è@ 	
return
é@é@ 
DACConsulta
é@é@ 
.
é@é@ (
TraerDepartamentosRegional
é@é@ 9
(
é@é@9 :
regional
é@é@: B
)
é@é@B C
;
é@é@C D
}
ê@ê@ 	
public
ì@ì@ 
List
ì@ì@ 
<
ì@ì@ /
!ref_cuentasmedicas_notips_motivos
ì@ì@ 5
>
ì@ì@5 6 
ListaMotivosNoRips
ì@ì@7 I
(
ì@ì@I J
)
ì@ì@J K
{
í@í@ 	
return
î@î@ 
DACConsulta
î@î@ 
.
î@î@  
ListaMotivosNoRips
î@î@ 1
(
î@î@1 2
)
î@î@2 3
;
î@î@3 4
}
ï@ï@ 	
public
ğ@ğ@ 
Int32
ğ@ğ@ )
IngresoArchivosRipsNoEsalud
ğ@ğ@ 0
(
ğ@ğ@0 1-
cuentas_medicas_norips_Archivos
ğ@ğ@1 P
OBJ
ğ@ğ@Q T
,
ğ@ğ@T U
ref
ğ@ğ@V Y 
MessageResponseOBJ
ğ@ğ@Z l
MsgRes
ğ@ğ@m s
)
ğ@ğ@s t
{
ñ@ñ@ 	
return
ò@ò@ 

DACInserta
ò@ò@ 
.
ò@ò@ )
IngresoArchivosRipsNoEsalud
ò@ò@ 9
(
ò@ò@9 :
OBJ
ò@ò@: =
,
ò@ò@= >
ref
ò@ò@? B
MsgRes
ò@ò@C I
)
ò@ò@I J
;
ò@ò@J K
}
ó@ó@ 	
public
õ@õ@ 
List
õ@õ@ 
<
õ@õ@ :
,management_cuentasMedicas_ripsNoEsaludResult
õ@õ@ @
>
õ@õ@@ A!
TableroRipsNoEsalud
õ@õ@B U
(
õ@õ@U V
DateTime
õ@õ@V ^
?
õ@õ@^ _
fechaInicio
õ@õ@` k
,
õ@õ@k l
DateTime
õ@õ@m u
?
õ@õ@u v
fechaFin
õ@õ@w 
,õ@õ@ €
intõ@õ@ „
?õ@õ@„ …
regionalõ@õ@† 
)õ@õ@ 
{
ö@ö@ 	
return
÷@÷@ 
DACConsulta
÷@÷@ 
.
÷@÷@ !
TableroRipsNoEsalud
÷@÷@ 2
(
÷@÷@2 3
fechaInicio
÷@÷@3 >
,
÷@÷@> ?
fechaFin
÷@÷@@ H
,
÷@÷@H I
regional
÷@÷@J R
)
÷@÷@R S
;
÷@÷@S T
}
ø@ø@ 	
public
ú@ú@ 
List
ú@ú@ 
<
ú@ú@ C
5management_cuentasMedicas_ripsNoEsalud_archivosResult
ú@ú@ I
>
ú@ú@I J,
TableroRepositorioRipsNoEsalud
ú@ú@K i
(
ú@ú@i j
int
ú@ú@j m
?
ú@ú@m n
idMed
ú@ú@o t
)
ú@ú@t u
{
û@û@ 	
return
ü@ü@ 
DACConsulta
ü@ü@ 
.
ü@ü@ ,
TableroRepositorioRipsNoEsalud
ü@ü@ =
(
ü@ü@= >
idMed
ü@ü@> C
)
ü@ü@C D
;
ü@ü@D E
}
ı@ı@ 	
public
ÿ@ÿ@ -
cuentas_medicas_norips_Archivos
ÿ@ÿ@ . 
traerArchivoNoRips
ÿ@ÿ@/ A
(
ÿ@ÿ@A B
int
ÿ@ÿ@B E
	idArchivo
ÿ@ÿ@F O
)
ÿ@ÿ@O P
{
€A€A 	
return
AA 
DACConsulta
AA 
.
AA  
traerArchivoNoRips
AA 1
(
AA1 2
	idArchivo
AA2 ;
)
AA; <
;
AA< =
}
‚A‚A 	
public
„A„A 
List
„A„A 
<
„A„A H
:management_cuentasMedicas_ripsNoEsalud_TodosArchivosResult
„A„A N
>
„A„AN O/
!TraerTodosLosArchivosRipsNoEsalud
„A„AP q
(
„A„Aq r
DateTime
„A„Ar z
?
„A„Az {
fechaInicio„A„A| ‡
,„A„A‡ ˆ
DateTime„A„A‰ ‘
?„A„A‘ ’
fechaFin„A„A“ ›
,„A„A› œ
int„A„A  
?„A„A  ¡
regional„A„A¢ ª
)„A„Aª «
{
…A…A 	
return
†A†A 
DACConsulta
†A†A 
.
†A†A /
!TraerTodosLosArchivosRipsNoEsalud
†A†A @
(
†A†A@ A
fechaInicio
†A†AA L
,
†A†AL M
fechaFin
†A†AN V
,
†A†AV W
regional
†A†AX `
)
†A†A` a
;
†A†Aa b
}
‡A‡A 	
public
ˆAˆA 
List
ˆAˆA 
<
ˆAˆA ;
-management_baseBeneficiarios_xDocumentoResult
ˆAˆA A
>
ˆAˆAA B(
BusquedaBeneficiarioCedula
ˆAˆAC ]
(
ˆAˆA] ^
string
ˆAˆA^ d
	documento
ˆAˆAe n
)
ˆAˆAn o
{
‰A‰A 	
return
ŠAŠA 
DACConsulta
ŠAŠA 
.
ŠAŠA (
BusquedaBeneficiarioCedula
ŠAŠA 9
(
ŠAŠA9 :
	documento
ŠAŠA: C
)
ŠAŠAC D
;
ŠAŠAD E
}
‹A‹A 	
public
AA 
int
AA &
InsertarCreacionCampanas
AA +
(
AA+ ,
creacion_campana
AA, <
obj
AA= @
)
AA@ A
{
AA 	
return
‘A‘A 

DACInserta
‘A‘A 
.
‘A‘A &
InsertarCreacionCampanas
‘A‘A 6
(
‘A‘A6 7
obj
‘A‘A7 :
)
‘A‘A: ;
;
‘A‘A; <
}
’A’A 	
public
”A”A 
int
”A”A -
InsertarCreacionCampanasDetalle
”A”A 2
(
”A”A2 3&
creacion_campana_detalle
”A”A3 K
obj
”A”AL O
)
”A”AO P
{
•A•A 	
return
–A–A 

DACInserta
–A–A 
.
–A–A -
InsertarCreacionCampanasDetalle
–A–A =
(
–A–A= >
obj
–A–A> A
)
–A–AA B
;
–A–AB C
}
—A—A 	
public
™A™A 
int
™A™A 5
'InsertarCreacionCampanasDetalleListados
™A™A :
(
™A™A: ;
List
™A™A; ?
<
™A™A? @%
creacion_campana_listas
™A™A@ W
>
™A™AW X
listas
™A™AY _
,
™A™A_ `
List
™A™Aa e
<
™A™Ae f-
creacion_campana_camposSimples™A™Af „
>™A™A„ …
simples™A™A† 
)™A™A 
{
šAšA 	
return
›A›A 

DACInserta
›A›A 
.
›A›A 5
'InsertarCreacionCampanasDetalleListados
›A›A E
(
›A›AE F
listas
›A›AF L
,
›A›AL M
simples
›A›AN U
)
›A›AU V
;
›A›AV W
}
œAœA 	
public
AA 
List
AA 
<
AA 5
'management_campana_tableroControlResult
AA ;
>
AA; <
ListadoCampanas
AA= L
(
AAL M
)
AAM N
{
ŸAŸA 	
return
 A A 
DACConsulta
 A A 
.
 A A 
ListadoCampanas
 A A .
(
 A A. /
)
 A A/ 0
;
 A A0 1
}
¡A¡A 	
public
£A£A *
ref_campanas_permisosEdicion
£A£A +)
traerPermisosEdicionCampana
£A£A, G
(
£A£AG H
int
£A£AH K
?
£A£AK L
	idUsuario
£A£AM V
)
£A£AV W
{
¤A¤A 	
return
¥A¥A 
DACConsulta
¥A¥A 
.
¥A¥A )
traerPermisosEdicionCampana
¥A¥A :
(
¥A¥A: ;
	idUsuario
¥A¥A; D
)
¥A¥AD E
;
¥A¥AE F
}
¦A¦A 	
public
¨A¨A 
creacion_campana
¨A¨A #
TraerCampanaGeneralId
¨A¨A  5
(
¨A¨A5 6
int
¨A¨A6 9
?
¨A¨A9 :
id
¨A¨A; =
)
¨A¨A= >
{
©A©A 	
return
ªAªA 
DACConsulta
ªAªA 
.
ªAªA #
TraerCampanaGeneralId
ªAªA 4
(
ªAªA4 5
id
ªAªA5 7
)
ªAªA7 8
;
ªAªA8 9
}
«A«A 	
public
®A®A 
creacion_campana
®A®A *
TraerCampanaVersionGeneralId
®A®A  <
(
®A®A< =
int
®A®A= @
?
®A®A@ A
id
®A®AB D
)
®A®AD E
{
¯A¯A 	
return
°A°A 
DACConsulta
°A°A 
.
°A°A *
TraerCampanaVersionGeneralId
°A°A ;
(
°A°A; <
id
°A°A< >
)
°A°A> ?
;
°A°A? @
}
±A±A 	
public
³A³A 
List
³A³A 
<
³A³A &
creacion_campana_detalle
³A³A ,
>
³A³A, -*
TraerCampanaGeneraDetallelId
³A³A. J
(
³A³AJ K
int
³A³AK N
?
³A³AN O
id
³A³AP R
)
³A³AR S
{
´A´A 	
return
µAµA 
DACConsulta
µAµA 
.
µAµA *
TraerCampanaGeneraDetallelId
µAµA ;
(
µAµA; <
id
µAµA< >
)
µAµA> ?
;
µAµA? @
}
¶A¶A 	
public
¸A¸A 
List
¸A¸A 
<
¸A¸A &
ref_campana_tipoPregunta
¸A¸A ,
>
¸A¸A, -'
TraerTipoPreguntaCampaÃ±a
¸A¸A. F
(
¸A¸AF G
)
¸A¸AG H
{
¹A¹A 	
return
ºAºA 
DACConsulta
ºAºA 
.
ºAºA '
TraerTipoPreguntaCampaÃ±a
ºAºA 7
(
ºAºA7 8
)
ºAºA8 9
;
ºAºA9 :
}
»A»A 	
public
¼A¼A 
List
¼A¼A 
<
¼A¼A ,
creacion_campana_camposSimples
¼A¼A 2
>
¼A¼A2 30
"TraerCampanaCamposSimplesIdCampana
¼A¼A4 V
(
¼A¼AV W
int
¼A¼AW Z
?
¼A¼AZ [
id
¼A¼A\ ^
)
¼A¼A^ _
{
½A½A 	
return
¾A¾A 
DACConsulta
¾A¾A 
.
¾A¾A 0
"TraerCampanaCamposSimplesIdCampana
¾A¾A A
(
¾A¾AA B
id
¾A¾AB D
)
¾A¾AD E
;
¾A¾AE F
}
¿A¿A 	
public
ÁAÁA 
List
ÁAÁA 
<
ÁAÁA %
creacion_campana_listas
ÁAÁA +
>
ÁAÁA+ ,.
 TraerCampanaCamposListaIdCampana
ÁAÁA- M
(
ÁAÁAM N
int
ÁAÁAN Q
?
ÁAÁAQ R
id
ÁAÁAS U
)
ÁAÁAU V
{
ÂAÂA 	
return
ÃAÃA 
DACConsulta
ÃAÃA 
.
ÃAÃA .
 TraerCampanaCamposListaIdCampana
ÃAÃA ?
(
ÃAÃA? @
id
ÃAÃA@ B
)
ÃAÃAB C
;
ÃAÃAC D
}
ÄAÄA 	
public
ÆAÆA 
int
ÆAÆA (
insertarRespuestasCamapana
ÆAÆA -
(
ÆAÆA- .
List
ÆAÆA. 2
<
ÆAÆA2 3 
campana_respuestas
ÆAÆA3 E
>
ÆAÆAE F
listaPreguntas
ÆAÆAG U
,
ÆAÆAU V
ref
ÆAÆAW Z 
MessageResponseOBJ
ÆAÆA[ m
MsgRes
ÆAÆAn t
)
ÆAÆAt u
{
ÇAÇA 	
return
ÈAÈA 

DACInserta
ÈAÈA 
.
ÈAÈA (
insertarRespuestasCamapana
ÈAÈA 8
(
ÈAÈA8 9
listaPreguntas
ÈAÈA9 G
,
ÈAÈAG H
ref
ÈAÈAI L
MsgRes
ÈAÈAM S
)
ÈAÈAS T
;
ÈAÈAT U
}
ÉAÉA 	
public
ËAËA 
int
ËAËA <
.IngresarRespuestaCampaÃ±aVerificacion_Archivos
ËAËA @
(
ËAËA@ A 
campana_respuestas
ËAËAA S
obj
ËAËAT W
)
ËAËAW X
{
ÌAÌA 	
return
ÍAÍA 

DACInserta
ÍAÍA 
.
ÍAÍA <
.IngresarRespuestaCampaÃ±aVerificacion_Archivos
ÍAÍA K
(
ÍAÍAK L
obj
ÍAÍAL O
)
ÍAÍAO P
;
ÍAÍAP Q
}
ÎAÎA 	
public
ĞAĞA 
int
ĞAĞA 5
'insertarRespuestasCampanaVerificaciones
ĞAĞA :
(
ĞAĞA: ;
List
ĞAĞA; ?
<
ĞAĞA? @3
%campana_respuestas_tipoVerificaciones
ĞAĞA@ e
>
ĞAĞAe f!
listaVerificaciones
ĞAĞAg z
,
ĞAĞAz {
ref
ĞAĞA| "
MessageResponseOBJĞAĞA€ ’
MsgResĞAĞA“ ™
)ĞAĞA™ š
{
ÑAÑA 	
return
ÒAÒA 

DACInserta
ÒAÒA 
.
ÒAÒA 5
'insertarRespuestasCampanaVerificaciones
ÒAÒA E
(
ÒAÒAE F!
listaVerificaciones
ÒAÒAF Y
,
ÒAÒAY Z
ref
ÒAÒA[ ^
MsgRes
ÒAÒA_ e
)
ÒAÒAe f
;
ÒAÒAf g
}
ÓAÓA 	
public
ÕAÕA 
int
ÕAÕA /
!insertarRespuestasCampanaArchivos
ÕAÕA 4
(
ÕAÕA4 5
List
ÕAÕA5 9
<
ÕAÕA9 :,
campana_respuestas_tipoArchivo
ÕAÕA: X
>
ÕAÕAX Y
listaArchivos
ÕAÕAZ g
,
ÕAÕAg h
ref
ÕAÕAi l 
MessageResponseOBJ
ÕAÕAm 
MsgResÕAÕA€ †
)ÕAÕA† ‡
{
ÖAÖA 	
return
×A×A 

DACInserta
×A×A 
.
×A×A /
!insertarRespuestasCampanaArchivos
×A×A ?
(
×A×A? @
listaArchivos
×A×A@ M
,
×A×AM N
ref
×A×AO R
MsgRes
×A×AS Y
)
×A×AY Z
;
×A×AZ [
}
ØAØA 	
public
ÚAÚA 
int
ÚAÚA '
DesactivarActivarCampaÃ±a
ÚAÚA +
(
ÚAÚA+ ,
int
ÚAÚA, /
?
ÚAÚA/ 0
	idCampana
ÚAÚA1 :
,
ÚAÚA: ;
int
ÚAÚA< ?
?
ÚAÚA? @
estado
ÚAÚAA G
)
ÚAÚAG H
{
ÛAÛA 	
return
ÜAÜA 
DACActualiza
ÜAÜA 
.
ÜAÜA  '
DesactivarActivarCampaÃ±a
ÜAÜA  8
(
ÜAÜA8 9
	idCampana
ÜAÜA9 B
,
ÜAÜAB C
estado
ÜAÜAD J
)
ÜAÜAJ K
;
ÜAÜAK L
}
İAİA 	
public
àAàA ,
creacion_campana_camposSimples
àAàA -0
"TraerCampanaCamposSimplesIdDetalle
àAàA. P
(
àAàAP Q
int
àAàAQ T
?
àAàAT U
id
àAàAV X
)
àAàAX Y
{
áAáA 	
return
âAâA 
DACConsulta
âAâA 
.
âAâA 0
"TraerCampanaCamposSimplesIdDetalle
âAâA A
(
âAâAA B
id
âAâAB D
)
âAâAD E
;
âAâAE F
}
ãAãA 	
public
åAåA 
List
åAåA 
<
åAåA %
creacion_campana_listas
åAåA +
>
åAåA+ ,.
 TraerCampanaCamposListaIdDetalle
åAåA- M
(
åAåAM N
int
åAåAN Q
?
åAåAQ R
id
åAåAS U
)
åAåAU V
{
æAæA 	
return
çAçA 
DACConsulta
çAçA 
.
çAçA .
 TraerCampanaCamposListaIdDetalle
çAçA ?
(
çAçA? @
id
çAçA@ B
)
çAçAB C
;
çAçAC D
}
èAèA 	
public
êAêA &
creacion_campana_detalle
êAêA ''
TraerDatosPreguntaCampana
êAêA( A
(
êAêAA B
int
êAêAB E
?
êAêAE F
id
êAêAG I
)
êAêAI J
{
ëAëA 	
return
ìAìA 
DACConsulta
ìAìA 
.
ìAìA '
TraerDatosPreguntaCampana
ìAìA 8
(
ìAìA8 9
id
ìAìA9 ;
)
ìAìA; <
;
ìAìA< =
}
íAíA 	
public
ğAğA 
List
ğAğA 
<
ğAğA &
creacion_campana_detalle
ğAğA ,
>
ğAğA, -*
ConsultaDtllPreguntasCampana
ğAğA. J
(
ğAğAJ K
int
ğAğAK N
?
ğAğAN O
	idcampana
ğAğAP Y
)
ğAğAY Z
{
ñAñA 	
return
òAòA 
DACConsulta
òAòA 
.
òAòA *
ConsultaDtllPreguntasCampana
òAòA ;
(
òAòA; <
	idcampana
òAòA< E
)
òAòAE F
;
òAòAF G
}
óAóA 	
public
õAõA 
int
õAõA '
ActualizarVersionCampaÃ±a
õAõA +
(
õAõA+ ,
creacion_campana
õAõA, <
cam
õAõA= @
)
õAõA@ A
{
öAöA 	
return
÷A÷A 
DACActualiza
÷A÷A 
.
÷A÷A  '
ActualizarVersionCampaÃ±a
÷A÷A  8
(
÷A÷A8 9
cam
÷A÷A9 <
)
÷A÷A< =
;
÷A÷A= >
}
øAøA 	
public
úAúA 
int
úAúA -
ActualizarDatosCampaÃ±aPregunta
úAúA 1
(
úAúA1 2&
creacion_campana_detalle
úAúA2 J
cam
úAúAK N
)
úAúAN O
{
ûAûA 	
return
üAüA 
DACActualiza
üAüA 
.
üAüA  -
ActualizarDatosCampaÃ±aPregunta
üAüA  >
(
üAüA> ?
cam
üAüA? B
)
üAüAB C
;
üAüAC D
}
ıAıA 	
public
BB 
void
BB !
ActualizarInactivas
BB '
(
BB' (
List
BB( ,
<
BB, -&
creacion_campana_detalle
BB- E
>
BBE F!
ActualizarInactivas
BBG Z
,
BBZ [
ref
BB\ _ 
MessageResponseOBJ
BB` r
msg
BBs v
)
BBv w
{
‚B‚B 	
DACActualiza
ƒBƒB 
.
ƒBƒB !
ActualizarInactivas
ƒBƒB ,
(
ƒBƒB, -!
ActualizarInactivas
ƒBƒB- @
,
ƒBƒB@ A
ref
ƒBƒBB E
msg
ƒBƒBF I
)
ƒBƒBI J
;
ƒBƒBJ K
}
„B„B 	
public
‡B‡B 
int
‡B‡B "
InsertarLoteCampaÃ±a
‡B‡B &
(
‡B‡B& '%
campana_respuestas_lote
‡B‡B' >
lote
‡B‡B? C
)
‡B‡BC D
{
ˆBˆB 	
return
‰B‰B 

DACInserta
‰B‰B 
.
‰B‰B "
InsertarLoteCampaÃ±a
‰B‰B 1
(
‰B‰B1 2
lote
‰B‰B2 6
)
‰B‰B6 7
;
‰B‰B7 8
}
ŠBŠB 	
public
ŒBŒB 
int
ŒBŒB '
ActualizarCamposPreguntas
ŒBŒB ,
(
ŒBŒB, -
int
ŒBŒB- 0
?
ŒBŒB0 1

idPregunta
ŒBŒB2 <
)
ŒBŒB< =
{
BB 	
return
BB 
DACActualiza
BB 
.
BB  '
ActualizarCamposPreguntas
BB  9
(
BB9 :

idPregunta
BB: D
)
BBD E
;
BBE F
}
BB 	
public
“B“B 
int
“B“B '
InsertarLogCarguesMasivos
“B“B ,
(
“B“B, -!
log_cargues_masivos
“B“B- @
obj
“B“BA D
)
“B“BD E
{
”B”B 	
return
•B•B 

DACInserta
•B•B 
.
•B•B '
InsertarLogCarguesMasivos
•B•B 7
(
•B•B7 8
obj
•B•B8 ;
)
•B•B; <
;
•B•B< =
}
–B–B 	
public
™B™B 
int
™B™B '
CargueAlertasDispensacion
™B™B ,
(
™B™B, -"
alertas_dispensacion
™B™B- A
obj
™B™BB E
,
™B™BE F
List
™B™BG K
<
™B™BK L,
alertas_dispensacion_registros
™B™BL j
>
™B™Bj k
detalle
™B™Bl s
,
™B™Bs t
ref
™B™Bu x!
MessageResponseOBJ™B™By ‹
MsgRes™B™BŒ ’
)™B™B’ “
{
šBšB 	
return
›B›B 

DACInserta
›B›B 
.
›B›B '
CargueAlertasDispensacion
›B›B 7
(
›B›B7 8
obj
›B›B8 ;
,
›B›B; <
detalle
›B›B= D
,
›B›BD E
ref
›B›BF I
MsgRes
›B›BJ P
)
›B›BP Q
;
›B›BQ R
}
œBœB 	
public
BB 
List
BB 
<
BB A
3management_alertasDispensacion_tableroControlResult
BB G
>
BBG H(
ListadoAlertasDispensacion
BBI c
(
BBc d
DateTime
BBd l
?
BBl m!
fecha_prescripcionBBn €
,BB€ 
stringBB‚ ˆ
numero_formulaBB‰ —
,BB— ˜
stringBB™ Ÿ&
documento_beneficiarioBB  ¶
,BB¶ ·
stringBB¸ ¾
id_prescriptorBB¿ Í
,BBÍ Î
stringBBÏ Õ 
nombre_comercialBBÖ æ
)BBæ ç
{
ŸBŸB 	
return
 B B 
DACConsulta
 B B 
.
 B B (
ListadoAlertasDispensacion
 B B 9
(
 B B9 : 
fecha_prescripcion
 B B: L
,
 B BL M
numero_formula
 B BN \
,
 B B\ ]$
documento_beneficiario
 B B^ t
,
 B Bt u
id_prescriptor B Bv „
, B B„ … 
nombre_comercial B B† –
) B B– —
; B B— ˜
}
¡B¡B 	
public
£B£B 
List
£B£B 
<
£B£B D
6management_alertasDispensacion_tableroControl_idResult
£B£B J
>
£B£BJ K!
TraerRegistroAlerta
£B£BL _
(
£B£B_ `
int
£B£B` c
?
£B£Bc d

idRegistro
£B£Be o
)
£B£Bo p
{
¤B¤B 	
return
¥B¥B 
DACConsulta
¥B¥B 
.
¥B¥B !
TraerRegistroAlerta
¥B¥B 2
(
¥B¥B2 3

idRegistro
¥B¥B3 =
)
¥B¥B= >
;
¥B¥B> ?
}
¦B¦B 	
public
¨B¨B 
List
¨B¨B 
<
¨B¨B H
:management_alertasDispensacion_buscarNombreComercialResult
¨B¨B N
>
¨B¨BN O"
TraerNombreComercial
¨B¨BP d
(
¨B¨Bd e
string
¨B¨Be k
nombre_comercial
¨B¨Bl |
)
¨B¨B| }
{
©B©B 	
return
ªBªB 
DACConsulta
ªBªB 
.
ªBªB "
TraerNombreComercial
ªBªB 3
(
ªBªB3 4
nombre_comercial
ªBªB4 D
)
ªBªBD E
;
ªBªBE F
}
«B«B 	
public
®B®B 
List
®B®B 
<
®B®B M
?management_alertasDispensacion_tableroControl_gestionadasResult
®B®B S
>
®B®BS T3
%ListadoAlertasDispensacionGestionadas
®B®BU z
(
®B®Bz {
)
®B®B{ |
{
¯B¯B 	
return
°B°B 
DACConsulta
°B°B 
.
°B°B 3
%ListadoAlertasDispensacionGestionadas
°B°B D
(
°B°BD E
)
°B°BE F
;
°B°BF G
}
±B±B 	
public
´B´B 
List
´B´B 
<
´B´B V
Hmanagement_alertasDispensacion_tableroControl_gestionadasDetalladaResult
´B´B \
>
´B´B\ ]=
.ListadoAlertasDispensacionGestionadasDetallada´B´B^ Œ
(´B´BŒ 
)´B´B 
{
µBµB 	
return
¶B¶B 
DACConsulta
¶B¶B 
.
¶B¶B <
.ListadoAlertasDispensacionGestionadasDetallada
¶B¶B M
(
¶B¶BM N
)
¶B¶BN O
;
¶B¶BO P
}
·B·B 	
public
¹B¹B 
List
¹B¹B 
<
¹B¹B U
Gmanagement_alertasDispensacion_tableroControl_gestionadasArchivosResult
¹B¹B [
>
¹B¹B[ \<
-ListadoAlertasDispensacionGestionadasArchivos¹B¹B] Š
(¹B¹BŠ ‹
int¹B¹B‹ 
?¹B¹B 
	idDetalle¹B¹B ™
)¹B¹B™ š
{
ºBºB 	
return
»B»B 
DACConsulta
»B»B 
.
»B»B ;
-ListadoAlertasDispensacionGestionadasArchivos
»B»B L
(
»B»BL M
	idDetalle
»B»BM V
)
»B»BV W
;
»B»BW X
}
¼B¼B 	
public
¾B¾B 
int
¾B¾B 0
"InsertarRespuestaAlertaDiepnsacion
¾B¾B 5
(
¾B¾B5 6*
alertas_dispensacion_detalle
¾B¾B6 R
obj
¾B¾BS V
)
¾B¾BV W
{
¿B¿B 	
return
ÀBÀB 

DACInserta
ÀBÀB 
.
ÀBÀB 0
"InsertarRespuestaAlertaDiepnsacion
ÀBÀB @
(
ÀBÀB@ A
obj
ÀBÀBA D
)
ÀBÀBD E
;
ÀBÀBE F
}
ÁBÁB 	
public
ÃBÃB 
int
ÃBÃB *
InsertarArchivoAlertasDispen
ÃBÃB /
(
ÃBÃB/ 03
%alertas_dispensacion_detalle_archivos
ÃBÃB0 U
obj
ÃBÃBV Y
)
ÃBÃBY Z
{
ÄBÄB 	
return
ÅBÅB 

DACInserta
ÅBÅB 
.
ÅBÅB *
InsertarArchivoAlertasDispen
ÅBÅB :
(
ÅBÅB: ;
obj
ÅBÅB; >
)
ÅBÅB> ?
;
ÅBÅB? @
}
ÆBÆB 	
public
ÈBÈB 3
%alertas_dispensacion_detalle_archivos
ÈBÈB 4'
TraerArchivoAlertasDispen
ÈBÈB5 N
(
ÈBÈBN O
int
ÈBÈBO R
?
ÈBÈBR S
	idArchivo
ÈBÈBT ]
)
ÈBÈB] ^
{
ÉBÉB 	
return
ÊBÊB 
DACConsulta
ÊBÊB 
.
ÊBÊB '
TraerArchivoAlertasDispen
ÊBÊB 8
(
ÊBÊB8 9
	idArchivo
ÊBÊB9 B
)
ÊBÊBB C
;
ÊBÊBC D
}
ËBËB 	
public
ÍBÍB 
List
ÍBÍB 
<
ÍBÍB L
>management_alertasDispensacion_tableroControl_respuestasResult
ÍBÍB R
>
ÍBÍBR S1
#ListadoAlertasDispensacionGestiones
ÍBÍBT w
(
ÍBÍBw x
int
ÍBÍBx {
?
ÍBÍB{ |
	idDetalleÍBÍB} †
)ÍBÍB† ‡
{
ÎBÎB 	
return
ÏBÏB 
DACConsulta
ÏBÏB 
.
ÏBÏB 1
#ListadoAlertasDispensacionGestiones
ÏBÏB B
(
ÏBÏBB C
	idDetalle
ÏBÏBC L
)
ÏBÏBL M
;
ÏBÏBM N
}
ĞBĞB 	
public
ÖBÖB 
int
ÖBÖB )
InsercionMortalidadRegistro
ÖBÖB .
(
ÖBÖB. /"
mortalidad_registros
ÖBÖB/ C
obj
ÖBÖBD G
)
ÖBÖBG H
{
×B×B 	
return
ØBØB 

DACInserta
ØBØB 
.
ØBØB )
InsercionMortalidadRegistro
ØBØB 9
(
ØBØB9 :
obj
ØBØB: =
)
ØBØB= >
;
ØBØB> ?
}
ÙBÙB 	
public
ÛBÛB 
List
ÛBÛB 
<
ÛBÛB 0
"management_tiposBeneficiarioResult
ÛBÛB 6
>
ÛBÛB6 7$
TraerTiposBeneficiario
ÛBÛB8 N
(
ÛBÛBN O
)
ÛBÛBO P
{
ÜBÜB 	
return
İBİB 
DACConsulta
İBİB 
.
İBİB $
TraerTiposBeneficiario
İBİB 5
(
İBİB5 6
)
İBİB6 7
;
İBİB7 8
}
ŞBŞB 	
public
àBàB "
mortalidad_registros
àBàB #"
TraerDatosMortalidad
àBàB$ 8
(
àBàB8 9
int
àBàB9 <
?
àBàB< =
idMortalidad
àBàB> J
)
àBàBJ K
{
áBáB 	
return
âBâB 
DACConsulta
âBâB 
.
âBâB "
TraerDatosMortalidad
âBâB 3
(
âBâB3 4
idMortalidad
âBâB4 @
)
âBâB@ A
;
âBâBA B
}
ãBãB 	
public
æBæB "
mortalidad_registros
æBæB #0
"TraerDatosMortalidadIdentificacion
æBæB$ F
(
æBæBF G
string
æBæBG M
identificacion
æBæBN \
)
æBæB\ ]
{
çBçB 	
return
èBèB 
DACConsulta
èBèB 
.
èBèB 0
"TraerDatosMortalidadIdentificacion
èBèB A
(
èBèBA B
identificacion
èBèBB P
)
èBèBP Q
;
èBèBQ R
}
éBéB 	
public
ëBëB 
List
ëBëB 
<
ëBëB 0
"management_TableroMortalidadResult
ëBëB 6
>
ëBëB6 7&
TraerMortalidadesTablero
ëBëB8 P
(
ëBëBP Q
int
ëBëBQ T
?
ëBëBT U
aÃ±o
ëBëBV Y
,
ëBëBY Z
int
ëBëB[ ^
?
ëBëB^ _
	trimestre
ëBëB` i
,
ëBëBi j
int
ëBëBk n
?
ëBëBn o
mes
ëBëBp s
,
ëBëBs t
int
ëBëBu x
?
ëBëBx y
unis
ëBëBz ~
,
ëBëB~ 
intëBëB€ ƒ
?ëBëBƒ „
regionalëBëB… 
,ëBëB 
stringëBëB •
	documentoëBëB– Ÿ
,ëBëBŸ  
DateTimeëBëB¡ ©
?ëBëB© ª
fechaFiltroëBëB« ¶
)ëBëB¶ ·
{
ìBìB 	
return
íBíB 
DACConsulta
íBíB 
.
íBíB &
TraerMortalidadesTablero
íBíB 7
(
íBíB7 8
aÃ±o
íBíB8 ;
,
íBíB; <
	trimestre
íBíB= F
,
íBíBF G
mes
íBíBH K
,
íBíBK L
unis
íBíBM Q
,
íBíBQ R
regional
íBíBS [
,
íBíB[ \
	documento
íBíB] f
,
íBíBf g
fechaFiltro
íBíBh s
)
íBíBs t
;
íBíBt u
}
îBîB 	
public
ïBïB 
List
ïBïB 
<
ïBïB /
!management_TableroNatalidadResult
ïBïB 5
>
ïBïB5 6%
TraerNatalidadesTablero
ïBïB7 N
(
ïBïBN O
int
ïBïBO R
?
ïBïBR S
aÃ±o
ïBïBT W
,
ïBïBW X
int
ïBïBY \
?
ïBïB\ ]
	trimestre
ïBïB^ g
,
ïBïBg h
int
ïBïBi l
?
ïBïBl m
mes
ïBïBn q
,
ïBïBq r
int
ïBïBs v
?
ïBïBv w
unis
ïBïBx |
,
ïBïB| }
intïBïB~ 
?ïBïB ‚
regionalïBïBƒ ‹
,ïBïB‹ Œ
stringïBïB “
	documentoïBïB” 
,ïBïB 
DateTimeïBïBŸ §
?ïBïB§ ¨
fechaFiltroïBïB© ´
)ïBïB´ µ
{
ğBğB 	
return
ñBñB 
DACConsulta
ñBñB 
.
ñBñB %
TraerNatalidadesTablero
ñBñB 6
(
ñBñB6 7
aÃ±o
ñBñB7 :
,
ñBñB: ;
	trimestre
ñBñB< E
,
ñBñBE F
mes
ñBñBG J
,
ñBñBJ K
unis
ñBñBL P
,
ñBñBP Q
regional
ñBñBR Z
,
ñBñBZ [
	documento
ñBñB\ e
,
ñBñBe f
fechaFiltro
ñBñBg r
)
ñBñBr s
;
ñBñBs t
}
òBòB 	
public
óBóB 
int
óBóB *
ActualizarRegistroMortalidad
óBóB /
(
óBóB/ 0"
mortalidad_registros
óBóB0 D
reg
óBóBE H
)
óBóBH I
{
ôBôB 	
return
õBõB 
DACActualiza
õBõB 
.
õBõB  *
ActualizarRegistroMortalidad
õBõB  <
(
õBõB< =
reg
õBõB= @
)
õBõB@ A
;
õBõBA B
}
öBöB 	
public
øBøB 
int
øBøB (
InsercionNatalidadRegistro
øBøB -
(
øBøB- .!
natalidad_registros
øBøB. A
obj
øBøBB E
)
øBøBE F
{
ùBùB 	
return
úBúB 

DACInserta
úBúB 
.
úBúB (
InsercionNatalidadRegistro
úBúB 8
(
úBúB8 9
obj
úBúB9 <
)
úBúB< =
;
úBúB= >
}
ûBûB 	
public
ıBıB 
int
ıBıB )
ActualizarRegistroNatalidad
ıBıB .
(
ıBıB. /!
natalidad_registros
ıBıB/ B
nat
ıBıBC F
)
ıBıBF G
{
şBşB 	
return
ÿBÿB 
DACActualiza
ÿBÿB 
.
ÿBÿB  )
ActualizarRegistroNatalidad
ÿBÿB  ;
(
ÿBÿB; <
nat
ÿBÿB< ?
)
ÿBÿB? @
;
ÿBÿB@ A
}
€C€C 	
public
‚C‚C !
natalidad_registros
‚C‚C "!
TraerDatosNatalidad
‚C‚C# 6
(
‚C‚C6 7
int
‚C‚C7 :
?
‚C‚C: ;
idNatalidad
‚C‚C< G
)
‚C‚CG H
{
ƒCƒC 	
return
„C„C 
DACConsulta
„C„C 
.
„C„C !
TraerDatosNatalidad
„C„C 2
(
„C„C2 3
idNatalidad
„C„C3 >
)
„C„C> ?
;
„C„C? @
}
…C…C 	
public
ŠCŠC 
int
ŠCŠC  
CargueEventosSalud
ŠCŠC %
(
ŠCŠC% &!
evento_salud_cargue
ŠCŠC& 9
obj
ŠCŠC: =
,
ŠCŠC= >
List
ŠCŠC? C
<
ŠCŠCC D%
eventos_salud_registros
ŠCŠCD [
>
ŠCŠC[ \
detalle
ŠCŠC] d
,
ŠCŠCd e
ref
ŠCŠCf i 
MessageResponseOBJ
ŠCŠCj |
MsgResŠCŠC} ƒ
)ŠCŠCƒ „
{
‹C‹C 	
return
ŒCŒC 

DACInserta
ŒCŒC 
.
ŒCŒC  
CargueEventosSalud
ŒCŒC 0
(
ŒCŒC0 1
obj
ŒCŒC1 4
,
ŒCŒC4 5
detalle
ŒCŒC6 =
,
ŒCŒC= >
ref
ŒCŒC? B
MsgRes
ŒCŒCC I
)
ŒCŒCI J
;
ŒCŒCJ K
}
CC 	
public
CC 
int
CC !
InsertarEventoSalud
CC &
(
CC& '%
eventos_salud_registros
CC' >
evento
CC? E
)
CCE F
{
CC 	
return
‘C‘C 

DACInserta
‘C‘C 
.
‘C‘C !
InsertarEventoSalud
‘C‘C 1
(
‘C‘C1 2
evento
‘C‘C2 8
)
‘C‘C8 9
;
‘C‘C9 :
}
’C’C 	
public
”C”C 
List
”C”C 
<
”C”C ,
ref_eventosSalud_fuenteReporte
”C”C 2
>
”C”C2 3,
ListaEventosSaludFuenteReporte
”C”C4 R
(
”C”CR S
)
”C”CS T
{
•C•C 	
return
–C–C 
DACConsulta
–C–C 
.
–C–C ,
ListaEventosSaludFuenteReporte
–C–C =
(
–C–C= >
)
–C–C> ?
;
–C–C? @
}
—C—C 	
public
™C™C 
List
™C™C 
<
™C™C /
!ref_eventosSalud_ambitoOcurrencia
™C™C 5
>
™C™C5 6/
!ListaEventosSaludAmbitoOcurrencia
™C™C7 X
(
™C™CX Y
)
™C™CY Z
{
šCšC 	
return
›C›C 
DACConsulta
›C›C 
.
›C›C /
!ListaEventosSaludAmbitoOcurrencia
›C›C @
(
›C›C@ A
)
›C›CA B
;
›C›CB C
}
œCœC 	
public
CC 
List
CC 
<
CC 1
#ref_eventosSalud_severidadDesenlace
CC 7
>
CC7 81
#ListaEventosSaludSeveridadDesenlace
CC9 \
(
CC\ ]
)
CC] ^
{
ŸCŸC 	
return
 C C 
DACConsulta
 C C 
.
 C C 1
#ListaEventosSaludSeveridadDesenlace
 C C B
(
 C CB C
)
 C CC D
;
 C CD E
}
¡C¡C 	
public
£C£C 
List
£C£C 
<
£C£C 5
'ref_eventosSalud_ProbabilidadRepeticion
£C£C ;
>
£C£C; <5
'ListaEventosSaludProbabilidadRepeticion
£C£C= d
(
£C£Cd e
)
£C£Ce f
{
¤C¤C 	
return
¥C¥C 
DACConsulta
¥C¥C 
.
¥C¥C 5
'ListaEventosSaludProbabilidadRepeticion
¥C¥C F
(
¥C¥CF G
)
¥C¥CG H
;
¥C¥CH I
}
¦C¦C 	
public
¨C¨C 
List
¨C¨C 
<
¨C¨C 0
"ref_eventosSalud_conceptoAuditoria
¨C¨C 6
>
¨C¨C6 70
"ListaEventosSaludConceptoAuditoria
¨C¨C8 Z
(
¨C¨CZ [
)
¨C¨C[ \
{
©C©C 	
return
ªCªC 
DACConsulta
ªCªC 
.
ªCªC 0
"ListaEventosSaludConceptoAuditoria
ªCªC A
(
ªCªCA B
)
ªCªCB C
;
ªCªCC D
}
«C«C 	
public
­C­C 
List
­C­C 
<
­C­C *
ref_eventosSalud_seguimiento
­C­C 0
>
­C­C0 1*
ListaEventosSaludSeguimiento
­C­C2 N
(
­C­CN O
)
­C­CO P
{
®C®C 	
return
¯C¯C 
DACConsulta
¯C¯C 
.
¯C¯C *
ListaEventosSaludSeguimiento
¯C¯C ;
(
¯C¯C; <
)
¯C¯C< =
;
¯C¯C= >
}
°C°C 	
public
±C±C 
List
±C±C 
<
±C±C .
 ref_eventosSalud_categoriaEvento
±C±C 4
>
±C±C4 5(
ListaEventosSaludCategoria
±C±C6 P
(
±C±CP Q
)
±C±CQ R
{
²C²C 	
return
³C³C 
DACConsulta
³C³C 
.
³C³C (
ListaEventosSaludCategoria
³C³C 9
(
³C³C9 :
)
³C³C: ;
;
³C³C; <
}
´C´C 	
public
µCµC 
List
µCµC 
<
µCµC 1
#ref_eventosSalud_subCategoriaEvento
µCµC 7
>
µCµC7 8+
ListaEventosSaludSubCategoria
µCµC9 V
(
µCµCV W
)
µCµCW X
{
¶C¶C 	
return
·C·C 
DACConsulta
·C·C 
.
·C·C +
ListaEventosSaludSubCategoria
·C·C <
(
·C·C< =
)
·C·C= >
;
·C·C> ?
}
¸C¸C 	
public
ºCºC 
List
ºCºC 
<
ºCºC 1
#ref_eventosSalud_subCategoriaEvento
ºCºC 7
>
ºCºC7 8-
EventosSaludTraerSubCategoriaId
ºCºC9 X
(
ºCºCX Y
int
ºCºCY \
?
ºCºC\ ]
idCategoria
ºCºC^ i
)
ºCºCi j
{
»C»C 	
return
¼C¼C 
DACConsulta
¼C¼C 
.
¼C¼C -
EventosSaludTraerSubCategoriaId
¼C¼C >
(
¼C¼C> ?
idCategoria
¼C¼C? J
)
¼C¼CJ K
;
¼C¼CK L
}
½C½C 	
public
¿C¿C 
List
¿C¿C 
<
¿C¿C 0
"ref_eventosSalud_resultadoNegativo
¿C¿C 6
>
¿C¿C6 7B
4ListaEventosSaludResNegativoIdCategoriaClasificacion
¿C¿C8 l
(
¿C¿Cl m
int
¿C¿Cm p
?
¿C¿Cp q
idCategoria
¿C¿Cr }
,
¿C¿C} ~
int¿C¿C ‚
?¿C¿C‚ ƒ
idClasificacin¿C¿C„ ’
)¿C¿C’ “
{
ÀCÀC 	
return
ÁCÁC 
DACConsulta
ÁCÁC 
.
ÁCÁC B
4ListaEventosSaludResNegativoIdCategoriaClasificacion
ÁCÁC S
(
ÁCÁCS T
idCategoria
ÁCÁCT _
,
ÁCÁC_ `
idClasificacin
ÁCÁCa o
)
ÁCÁCo p
;
ÁCÁCp q
}
ÂCÂC 	
public
ÄCÄC 
List
ÄCÄC 
<
ÄCÄC 0
"ref_eventosSalud_resultadoNegativo
ÄCÄC 6
>
ÄCÄC6 7*
ListaEventosSaludResNegativo
ÄCÄC8 T
(
ÄCÄCT U
)
ÄCÄCU V
{
ÅCÅC 	
return
ÆCÆC 
DACConsulta
ÆCÆC 
.
ÆCÆC *
ListaEventosSaludResNegativo
ÆCÆC ;
(
ÆCÆC; <
)
ÆCÆC< =
;
ÆCÆC= >
}
ÇCÇC 	
public
ÉCÉC 
List
ÉCÉC 
<
ÉCÉC 2
$ref_eventosSalud_clasificacionEvento
ÉCÉC 8
>
ÉCÉC8 9,
ListaEventosSaludClasificacion
ÉCÉC: X
(
ÉCÉCX Y
)
ÉCÉCY Z
{
ÊCÊC 	
return
ËCËC 
DACConsulta
ËCËC 
.
ËCËC ,
ListaEventosSaludClasificacion
ËCËC =
(
ËCËC= >
)
ËCËC> ?
;
ËCËC? @
}
ÌCÌC 	
public
ÎCÎC 
List
ÎCÎC 
<
ÎCÎC 3
%management_eventosSalud_tableroResult
ÎCÎC 9
>
ÎCÎC9 :*
ListadoEventosEnSaludTablero
ÎCÎC; W
(
ÎCÎCW X
int
ÎCÎCX [
?
ÎCÎC[ \
mes
ÎCÎC] `
,
ÎCÎC` a
int
ÎCÎCb e
?
ÎCÎCe f
aÃ±o
ÎCÎCg j
)
ÎCÎCj k
{
ÏCÏC 	
return
ĞCĞC 
DACConsulta
ĞCĞC 
.
ĞCĞC *
ListadoEventosEnSaludTablero
ĞCĞC ;
(
ĞCĞC; <
mes
ĞCĞC< ?
,
ĞCĞC? @
aÃ±o
ĞCĞCA D
)
ĞCĞCD E
;
ĞCĞCE F
}
ÑCÑC 	
public
ÔCÔC %
eventos_salud_registros
ÔCÔC &&
TraerDatosEventosSaludId
ÔCÔC' ?
(
ÔCÔC? @
int
ÔCÔC@ C
?
ÔCÔCC D
idEvento
ÔCÔCE M
)
ÔCÔCM N
{
ÕCÕC 	
return
ÖCÖC 
DACConsulta
ÖCÖC 
.
ÖCÖC &
TraerDatosEventosSaludId
ÖCÖC 7
(
ÖCÖC7 8
idEvento
ÖCÖC8 @
)
ÖCÖC@ A
;
ÖCÖCA B
}
×C×C 	
public
ÙCÙC 
Ref_ips_cuentas
ÙCÙC 
getprestadoresNit
ÙCÙC 0
(
ÙCÙC0 1
string
ÙCÙC1 7
nit
ÙCÙC8 ;
)
ÙCÙC; <
{
ÚCÚC 	
return
ÛCÛC 
DACConsulta
ÛCÛC 
.
ÛCÛC 
getprestadoresNit
ÛCÛC 0
(
ÛCÛC0 1
nit
ÛCÛC1 4
)
ÛCÛC4 5
;
ÛCÛC5 6
}
ÜCÜC 	
public
ŞCŞC 
int
ŞCŞC ,
ActualizarRegistroEventosSalud
ŞCŞC 1
(
ŞCŞC1 2%
eventos_salud_registros
ŞCŞC2 I
even
ŞCŞCJ N
)
ŞCŞCN O
{
ßCßC 	
return
àCàC 
DACActualiza
àCàC 
.
àCàC  ,
ActualizarRegistroEventosSalud
àCàC  >
(
àCàC> ?
even
àCàC? C
)
àCàCC D
;
àCàCD E
}
áCáC 	
public
ëCëC 
List
ëCëC 
<
ëCëC )
cronograma_visita_documento
ëCëC /
>
ëCëC/ 0-
ObtenerListadoDocumentosVisitas
ëCëC1 P
(
ëCëCP Q
)
ëCëCQ R
{
ìCìC 	
return
íCíC 
DACConsulta
íCíC 
.
íCíC -
ObtenerListadoDocumentosVisitas
íCíC >
(
íCíC> ?
)
íCíC? @
;
íCíC@ A
}
îCîC 	
public
ïCïC 
List
ïCïC 
<
ïCïC :
,management_cronograma_visita_traerByteResult
ïCïC @
>
ïCïC@ A4
&ObtenerListadoDocumentosVisitasSinRuta
ïCïCB h
(
ïCïCh i
)
ïCïCi j
{
ğCğC 	
return
ñCñC 
DACConsulta
ñCñC 
.
ñCñC 4
&ObtenerListadoDocumentosVisitasSinRuta
ñCñC E
(
ñCñCE F
)
ñCñCF G
;
ñCñCG H
}
òCòC 	
public
úCúC 
List
úCúC 
<
úCúC 4
&management_encuesta_tipoPreguntaResult
úCúC :
>
úCúC: ;
listaEncuestaSAMI
úCúC< M
(
úCúCM N
)
úCúCN O
{
ûCûC 	
return
üCüC 
DACConsulta
üCüC 
.
üCüC 
listaEncuestaSAMI
üCüC 0
(
üCüC0 1
)
üCüC1 2
;
üCüC2 3
}
ıCıC 	
public
ÿCÿC 
int
ÿCÿC #
InsertarRespuestaSAMI
ÿCÿC (
(
ÿCÿC( )
encuesta_sami
ÿCÿC) 6
dato
ÿCÿC7 ;
,
ÿCÿC; <
List
ÿCÿC= A
<
ÿCÿCA B&
encuesta_sami_respuestas
ÿCÿCB Z
>
ÿCÿCZ [
detalles
ÿCÿC\ d
,
ÿCÿCd e
ref
ÿCÿCf i 
MessageResponseOBJ
ÿCÿCj |
MsgResÿCÿC} ƒ
)ÿCÿCƒ „
{
€D€D 	
return
DD 

DACInserta
DD 
.
DD #
InsertarRespuestaSAMI
DD 3
(
DD3 4
dato
DD4 8
,
DD8 9
detalles
DD: B
,
DDB C
ref
DDD G
MsgRes
DDH N
)
DDN O
;
DDO P
}
‚D‚D 	
public
„D„D 
List
„D„D 
<
„D„D 2
$management_encuesta_sami_datosResult
„D„D 8
>
„D„D8 9!
listaRespuestasSAMI
„D„D: M
(
„D„DM N
)
„D„DN O
{
…D…D 	
return
†D†D 
DACConsulta
†D†D 
.
†D†D !
listaRespuestasSAMI
†D†D 2
(
†D†D2 3
)
†D†D3 4
;
†D†D4 5
}
‡D‡D 	
public
‰D‰D 
List
‰D‰D 
<
‰D‰D :
,management_encuesta_sami_datos_detalleResult
‰D‰D @
>
‰D‰D@ A(
listaRespuestasSAMIDetalle
‰D‰DB \
(
‰D‰D\ ]
)
‰D‰D] ^
{
ŠDŠD 	
return
‹D‹D 
DACConsulta
‹D‹D 
.
‹D‹D (
listaRespuestasSAMIDetalle
‹D‹D 9
(
‹D‹D9 :
)
‹D‹D: ;
;
‹D‹D; <
}
ŒDŒD 	
public
DD 
List
DD 
<
DD <
.management_encuesta_sami_datos_promediosResult
DD B
>
DDB C*
listaRespuestasSAMIPromedios
DDD `
(
DD` a
)
DDa b
{
DD 	
return
DD 
DACConsulta
DD 
.
DD *
listaRespuestasSAMIPromedios
DD ;
(
DD; <
)
DD< =
;
DD= >
}
‘D‘D 	
public
“D“D 
encuesta_sami
“D“D "
TraerEncuestaEsteMes
“D“D 1
(
“D“D1 2
)
“D“D2 3
{
”D”D 	
return
•D•D 
DACConsulta
•D•D 
.
•D•D "
TraerEncuestaEsteMes
•D•D 3
(
•D•D3 4
)
•D•D4 5
;
•D•D5 6
}
–D–D 	
public
DD -
management_fisPrestadoresResult
DD .
TraerPrestadorId
DD/ ?
(
DD? @
int
DD@ C
?
DDC D
idPrestador
DDE P
)
DDP Q
{
DD 	
return
ŸDŸD 
DACConsulta
ŸDŸD 
.
ŸDŸD 
TraerPrestadorId
ŸDŸD /
(
ŸDŸD/ 0
idPrestador
ŸDŸD0 ;
)
ŸDŸD; <
;
ŸDŸD< =
}
 D D 	
public
¢D¢D 
List
¢D¢D 
<
¢D¢D 3
%management_fisPrestadores_sedesResult
¢D¢D 9
>
¢D¢D9 :#
TraerPrestadorSedesId
¢D¢D; P
(
¢D¢DP Q
int
¢D¢DQ T
?
¢D¢DT U
idPrestador
¢D¢DV a
)
¢D¢Da b
{
£D£D 	
return
¤D¤D 
DACConsulta
¤D¤D 
.
¤D¤D #
TraerPrestadorSedesId
¤D¤D 4
(
¤D¤D4 5
idPrestador
¤D¤D5 @
)
¤D¤D@ A
;
¤D¤DA B
}
¥D¥D 	
public
§D§D 
int
§D§D "
InsertarPrestadorFis
§D§D '
(
§D§D' ("
fis_rips_prestadores
§D§D( <
	prestador
§D§D= F
)
§D§DF G
{
¨D¨D 	
return
©D©D 

DACInserta
©D©D 
.
©D©D "
InsertarPrestadorFis
©D©D 2
(
©D©D2 3
	prestador
©D©D3 <
)
©D©D< =
;
©D©D= >
}
ªDªD 	
public
¬D¬D 
int
¬D¬D &
InsertarSedePrestadorFis
¬D¬D +
(
¬D¬D+ ,(
fis_rips_prestadores_sedes
¬D¬D, F
sede
¬D¬DG K
)
¬D¬DK L
{
­D­D 	
return
®D®D 

DACInserta
®D®D 
.
®D®D &
InsertarSedePrestadorFis
®D®D 6
(
®D®D6 7
sede
®D®D7 ;
)
®D®D; <
;
®D®D< =
}
¯D¯D 	
public
±D±D 
int
±D±D ,
ActualizarEstadoPrestadoresFIS
±D±D 1
(
±D±D1 2
int
±D±D2 5
?
±D±D5 6
idPrestador
±D±D7 B
)
±D±DB C
{
²D²D 	
return
³D³D 
DACActualiza
³D³D 
.
³D³D  ,
ActualizarEstadoPrestadoresFIS
³D³D  >
(
³D³D> ?
idPrestador
³D³D? J
)
³D³DJ K
;
³D³DK L
}
´D´D 	
public
¶D¶D 
int
¶D¶D %
EliminarSedePrestadores
¶D¶D *
(
¶D¶D* +
int
¶D¶D+ .
?
¶D¶D. /
idSede
¶D¶D0 6
)
¶D¶D6 7
{
·D·D 	
return
¸D¸D 

DACElimina
¸D¸D 
.
¸D¸D %
EliminarSedePrestadores
¸D¸D 5
(
¸D¸D5 6
idSede
¸D¸D6 <
)
¸D¸D< =
;
¸D¸D= >
}
¹D¹D 	
public
»D»D 
int
»D»D &
ActualizarDatosPrestador
»D»D +
(
»D»D+ ,"
fis_rips_prestadores
»D»D, @
pre
»D»DA D
)
»D»DD E
{
¼D¼D 	
return
½D½D 
DACActualiza
½D½D 
.
½D½D  &
ActualizarDatosPrestador
½D½D  8
(
½D½D8 9
pre
½D½D9 <
)
½D½D< =
;
½D½D= >
}
¾D¾D 	
public
ÀDÀD 
List
ÀDÀD 
<
ÀDÀD /
!management_regional_usuarioResult
ÀDÀD 5
>
ÀDÀD5 6(
ListadoRegionalesUsuarioId
ÀDÀD7 Q
(
ÀDÀDQ R
int
ÀDÀDR U
?
ÀDÀDU V
	idUsuario
ÀDÀDW `
)
ÀDÀD` a
{
ÁDÁD 	
return
ÂDÂD 
DACConsulta
ÂDÂD 
.
ÂDÂD (
ListadoRegionalesUsuarioId
ÂDÂD 9
(
ÂDÂD9 :
	idUsuario
ÂDÂD: C
)
ÂDÂDC D
;
ÂDÂDD E
}
ÃDÃD 	
public
ÅDÅD 
int
ÅDÅD &
GuardarArchivosPrestador
ÅDÅD +
(
ÅDÅD+ ,+
fis_rips_prestadores_archivos
ÅDÅD, I
archivo
ÅDÅDJ Q
)
ÅDÅDQ R
{
ÆDÆD 	
return
ÇDÇD 

DACInserta
ÇDÇD 
.
ÇDÇD &
GuardarArchivosPrestador
ÇDÇD 6
(
ÇDÇD6 7
archivo
ÇDÇD7 >
)
ÇDÇD> ?
;
ÇDÇD? @
}
ÈDÈD 	
public
ÊDÊD 
List
ÊDÊD 
<
ÊDÊD <
.management_fisPrestadores_tableroControlResult
ÊDÊD B
>
ÊDÊDB C'
TableroControlPrestadores
ÊDÊDD ]
(
ÊDÊD] ^
string
ÊDÊD^ d
nit
ÊDÊDe h
,
ÊDÊDh i
string
ÊDÊDj p
sap
ÊDÊDq t
)
ÊDÊDt u
{
ËDËD 	
return
ÌDÌD 
DACConsulta
ÌDÌD 
.
ÌDÌD '
TableroControlPrestadores
ÌDÌD 8
(
ÌDÌD8 9
nit
ÌDÌD9 <
,
ÌDÌD< =
sap
ÌDÌD> A
)
ÌDÌDA B
;
ÌDÌDB C
}
ÍDÍD 	
public
ÏDÏD 
List
ÏDÏD 
<
ÏDÏD F
8management_fisPrestadores_tableroControl_detalladoResult
ÏDÏD L
>
ÏDÏDL M0
"TableroControlPrestadoresDetallado
ÏDÏDN p
(
ÏDÏDp q
string
ÏDÏDq w
nit
ÏDÏDx {
,
ÏDÏD{ |
stringÏDÏD} ƒ
sapÏDÏD„ ‡
)ÏDÏD‡ ˆ
{
ĞDĞD 	
return
ÑDÑD 
DACConsulta
ÑDÑD 
.
ÑDÑD 0
"TableroControlPrestadoresDetallado
ÑDÑD A
(
ÑDÑDA B
nit
ÑDÑDB E
,
ÑDÑDE F
sap
ÑDÑDG J
)
ÑDÑDJ K
;
ÑDÑDK L
}
ÒDÒD 	
public
ÔDÔD 
List
ÔDÔD 
<
ÔDÔD E
7management_fisPrestadores_tableroControl_archivosResult
ÔDÔD K
>
ÔDÔDK L&
TraerArchivosPrestadorId
ÔDÔDM e
(
ÔDÔDe f
int
ÔDÔDf i
?
ÔDÔDi j
idPrestador
ÔDÔDk v
)
ÔDÔDv w
{
ÕDÕD 	
return
ÖDÖD 
DACConsulta
ÖDÖD 
.
ÖDÖD &
TraerArchivosPrestadorId
ÖDÖD 7
(
ÖDÖD7 8
idPrestador
ÖDÖD8 C
)
ÖDÖDC D
;
ÖDÖDD E
}
×D×D 	
public
ÙDÙD +
fis_rips_prestadores_archivos
ÙDÙD , 
ArchivoPrestadorId
ÙDÙD- ?
(
ÙDÙD? @
int
ÙDÙD@ C
?
ÙDÙDC D
	idArchivo
ÙDÙDE N
)
ÙDÙDN O
{
ÚDÚD 	
return
ÛDÛD 
DACConsulta
ÛDÛD 
.
ÛDÛD  
ArchivoPrestadorId
ÛDÛD 1
(
ÛDÛD1 2
	idArchivo
ÛDÛD2 ;
)
ÛDÛD; <
;
ÛDÛD< =
}
ÜDÜD 	
public
ŞDŞD 
int
ŞDŞD &
EliminarArchivoPrestador
ŞDŞD +
(
ŞDŞD+ ,
int
ŞDŞD, /
?
ŞDŞD/ 0
	idArchivo
ŞDŞD1 :
)
ŞDŞD: ;
{
ßDßD 	
return
àDàD 

DACElimina
àDàD 
.
àDàD &
EliminarArchivoPrestador
àDàD 6
(
àDàD6 7
	idArchivo
àDàD7 @
)
àDàD@ A
;
àDàDA B
}
áDáD 	
public
ãDãD 
List
ãDãD 
<
ãDãD "
fis_rips_prestadores
ãDãD (
>
ãDãD( )$
ConsultaPrestadoresFis
ãDãD* @
(
ãDãD@ A
decimal
ãDãDA H
?
ãDãDH I
nit
ãDãDJ M
,
ãDãDM N
ref
ãDãDO R 
MessageResponseOBJ
ãDãDS e
MsgRes
ãDãDf l
)
ãDãDl m
{
äDäD 	
return
åDåD 
DACConsulta
åDåD 
.
åDåD $
ConsultaPrestadoresFis
åDåD 5
(
åDåD5 6
nit
åDåD6 9
,
åDåD9 :
ref
åDåD; >
MsgRes
åDåD? E
)
åDåDE F
;
åDåDF G
}
æDæD 	
public
èDèD 
List
èDèD 
<
èDèD ,
fis_rips_prestadores_contratos
èDèD 2
>
èDèD2 3,
ConsultaContratoPrestadoresFis
èDèD4 R
(
èDèDR S
string
èDèDS Y
numContrato
èDèDZ e
,
èDèDe f
ref
èDèDg j 
MessageResponseOBJ
èDèDk }
MsgResèDèD~ „
)èDèD„ …
{
éDéD 	
return
êDêD 
DACConsulta
êDêD 
.
êDêD ,
ConsultaContratoPrestadoresFis
êDêD =
(
êDêD= >
numContrato
êDêD> I
,
êDêDI J
ref
êDêDK N
MsgRes
êDêDO U
)
êDêDU V
;
êDêDV W
}
ëDëD 	
public
íDíD 
List
íDíD 
<
íDíD "
fis_rips_prestadores
íDíD (
>
íDíD( )'
ConsultaPrestadoresFisSAP
íDíD* C
(
íDíDC D
decimal
íDíDD K
sap
íDíDL O
,
íDíDO P
ref
íDíDQ T 
MessageResponseOBJ
íDíDU g
MsgRes
íDíDh n
)
íDíDn o
{
îDîD 	
return
ïDïD 
DACConsulta
ïDïD 
.
ïDïD '
ConsultaPrestadoresFisSAP
ïDïD 8
(
ïDïD8 9
sap
ïDïD9 <
,
ïDïD< =
ref
ïDïD> A
MsgRes
ïDïDB H
)
ïDïDH I
;
ïDïDI J
}
ğDğD 	
public
òDòD 
List
òDòD 
<
òDòD 
fis_rips_tigas
òDòD "
>
òDòD" #

TraerTigas
òDòD$ .
(
òDòD. /
)
òDòD/ 0
{
óDóD 	
return
ôDôD 
DACConsulta
ôDôD 
.
ôDôD 

TraerTigas
ôDôD )
(
ôDôD) *
)
ôDôD* +
;
ôDôD+ ,
}
õDõD 	
public
÷D÷D 7
)management_fisPrestadores_contratosResult
÷D÷D 8 
TraerDatosContrato
÷D÷D9 K
(
÷D÷DK L
int
÷D÷DL O
?
÷D÷DO P

idCOntrato
÷D÷DQ [
)
÷D÷D[ \
{
øDøD 	
return
ùDùD 
DACConsulta
ùDùD 
.
ùDùD  
TraerDatosContrato
ùDùD 1
(
ùDùD1 2

idCOntrato
ùDùD2 <
)
ùDùD< =
;
ùDùD= >
}
úDúD 	
public
üDüD 
List
üDüD 
<
üDüD =
/management_fisPrestadores_contratos_tigasResult
üDüD C
>
üDüDC D%
TraerDatosContratoTigas
üDüDE \
(
üDüD\ ]
int
üDüD] `
?
üDüD` a

idCOntrato
üDüDb l
)
üDüDl m
{
ıDıD 	
return
şDşD 
DACConsulta
şDşD 
.
şDşD %
TraerDatosContratoTigas
şDşD 6
(
şDşD6 7

idCOntrato
şDşD7 A
)
şDşDA B
;
şDşDB C
}
ÿDÿD 	
public
EE 
int
EE &
GuardarContratoPrestador
EE +
(
EE+ ,,
fis_rips_prestadores_contratos
EE, J
contrato
EEK S
)
EES T
{
‚E‚E 	
return
ƒEƒE 

DACInserta
ƒEƒE 
.
ƒEƒE &
GuardarContratoPrestador
ƒEƒE 6
(
ƒEƒE6 7
contrato
ƒEƒE7 ?
)
ƒEƒE? @
;
ƒEƒE@ A
}
„E„E 	
public
…E…E 
int
…E…E .
 ActualizarDatosContratoPrestador
…E…E 3
(
…E…E3 4,
fis_rips_prestadores_contratos
…E…E4 R
contra
…E…ES Y
)
…E…EY Z
{
†E†E 	
return
‡E‡E 
DACActualiza
‡E‡E 
.
‡E‡E  .
 ActualizarDatosContratoPrestador
‡E‡E  @
(
‡E‡E@ A
contra
‡E‡EA G
)
‡E‡EG H
;
‡E‡EH I
}
ˆEˆE 	
public
ŠEŠE 
int
ŠEŠE *
GuardarTigaContratoPrestador
ŠEŠE /
(
ŠEŠE/ 01
#fis_rips_prestadores_contrato_tigas
ŠEŠE0 S
tiga
ŠEŠET X
)
ŠEŠEX Y
{
‹E‹E 	
return
ŒEŒE 

DACInserta
ŒEŒE 
.
ŒEŒE *
GuardarTigaContratoPrestador
ŒEŒE :
(
ŒEŒE: ;
tiga
ŒEŒE; ?
)
ŒEŒE? @
;
ŒEŒE@ A
}
EE 	
public
EE 
int
EE .
 EliminarTigaContratosPrestadores
EE 3
(
EE3 4
int
EE4 7
?
EE7 8
idTiga
EE9 ?
)
EE? @
{
EE 	
return
‘E‘E 

DACElimina
‘E‘E 
.
‘E‘E .
 EliminarTigaContratosPrestadores
‘E‘E >
(
‘E‘E> ?
idTiga
‘E‘E? E
)
‘E‘EE F
;
‘E‘EF G
}
’E’E 	
public
•E•E 
int
•E•E +
ActualizarEstadoTigasContrato
•E•E 0
(
•E•E0 1
int
•E•E1 4
?
•E•E4 5

idContrato
•E•E6 @
)
•E•E@ A
{
–E–E 	
return
—E—E 
DACActualiza
—E—E 
.
—E—E  +
ActualizarEstadoTigasContrato
—E—E  =
(
—E—E= >

idContrato
—E—E> H
)
—E—EH I
;
—E—EI J
}
˜E˜E 	
public
šEšE 
List
šEšE 
<
šEšE 
fis_rips_regional
šEšE %
>
šEšE% & 
TraerregionalesFis
šEšE' 9
(
šEšE9 :
)
šEšE: ;
{
›E›E 	
return
œEœE 
DACConsulta
œEœE 
.
œEœE  
TraerregionalesFis
œEœE 1
(
œEœE1 2
)
œEœE2 3
;
œEœE3 4
}
EE 	
public
EE 
List
EE 
<
EE 8
*management_fis_departamento_regionalResult
EE >
>
EE> ?#
TraerDepartamentosFis
EE@ U
(
EEU V
int
EEV Y
?
EEY Z

idRegional
EE[ e
)
EEe f
{
ŸEŸE 	
return
 E E 
DACConsulta
 E E 
.
 E E #
TraerDepartamentosFis
 E E 4
(
 E E4 5

idRegional
 E E5 ?
)
 E E? @
;
 E E@ A
}
¡E¡E 	
public
£E£E 
List
£E£E 
<
£E£E 
fis_rips_ciudad
£E£E #
>
£E£E# $
TraerCiudadesFis
£E£E% 5
(
£E£E5 6
int
£E£E6 9
?
£E£E9 :
idDepartamento
£E£E; I
)
£E£EI J
{
¤E¤E 	
return
¥E¥E 
DACConsulta
¥E¥E 
.
¥E¥E 
TraerCiudadesFis
¥E¥E /
(
¥E¥E/ 0
idDepartamento
¥E¥E0 >
)
¥E¥E> ?
;
¥E¥E? @
}
¦E¦E 	
public
¨E¨E 
void
¨E¨E #
insertarCargueTarifas
¨E¨E )
(
¨E¨E) *4
&fis_rips_prestadores_contratos_tarifas
¨E¨E* P
obj
¨E¨EQ T
,
¨E¨ET U
List
¨E¨EV Z
<
¨E¨EZ [?
0fis_rips_prestadores_contratos_tarifas_registros¨E¨E[ ‹
>¨E¨E‹ Œ
lista¨E¨E ’
,¨E¨E’ “
ref¨E¨E” —"
MessageResponseOBJ¨E¨E˜ ª
MsgRes¨E¨E« ±
)¨E¨E± ²
{
©E©E 	

DACInserta
ªEªE 
.
ªEªE #
insertarCargueTarifas
ªEªE ,
(
ªEªE, -
obj
ªEªE- 0
,
ªEªE0 1
lista
ªEªE2 7
,
ªEªE7 8
ref
ªEªE9 <
MsgRes
ªEªE= C
)
ªEªEC D
;
ªEªED E
}
«E«E 	
public
­E­E 
fis_rips_cups
­E­E 
TraerCupsCodigo
­E­E ,
(
­E­E, -
string
­E­E- 3
codigo
­E­E4 :
)
­E­E: ;
{
®E®E 	
return
¯E¯E 
DACConsulta
¯E¯E 
.
¯E¯E 
TraerCupsCodigo
¯E¯E .
(
¯E¯E. /
codigo
¯E¯E/ 5
)
¯E¯E5 6
;
¯E¯E6 7
}
°E°E 	
public
²E²E 
int
²E²E %
ActualizarEstadoTarifas
²E²E *
(
²E²E* +
int
²E²E+ .
?
²E²E. /

idContrato
²E²E0 :
)
²E²E: ;
{
³E³E 	
return
´E´E 
DACActualiza
´E´E 
.
´E´E  %
ActualizarEstadoTarifas
´E´E  7
(
´E´E7 8

idContrato
´E´E8 B
)
´E´EB C
;
´E´EC D
}
µEµE 	
public
·E·E 
List
·E·E 
<
·E·E F
8management_fisPrestadores_contratos_tableroControlResult
·E·E L
>
·E·EL M*
DatosTableroControlContratos
·E·EN j
(
·E·Ej k
)
·E·Ek l
{
¸E¸E 	
return
¹E¹E 
DACConsulta
¹E¹E 
.
¹E¹E *
DatosTableroControlContratos
¹E¹E ;
(
¹E¹E; <
)
¹E¹E< =
;
¹E¹E= >
}
ºEºE 	
public
¼E¼E 
int
¼E¼E 
	CrearCups
¼E¼E 
(
¼E¼E 
fis_rips_cups
¼E¼E *
obj
¼E¼E+ .
)
¼E¼E. /
{
½E½E 	
return
¾E¾E 

DACInserta
¾E¾E 
.
¾E¾E 
	CrearCups
¾E¾E '
(
¾E¾E' (
obj
¾E¾E( +
)
¾E¾E+ ,
;
¾E¾E, -
}
¿E¿E 	
public
ÁEÁE 
int
ÁEÁE 
ActualizarCupsFis
ÁEÁE $
(
ÁEÁE$ %
fis_rips_cups
ÁEÁE% 2
cups
ÁEÁE3 7
)
ÁEÁE7 8
{
ÂEÂE 	
return
ÃEÃE 
DACActualiza
ÃEÃE 
.
ÃEÃE  
ActualizarCupsFis
ÃEÃE  1
(
ÃEÃE1 2
cups
ÃEÃE2 6
)
ÃEÃE6 7
;
ÃEÃE7 8
}
ÄEÄE 	
public
ÆEÆE 
List
ÆEÆE 
<
ÆEÆE '
management_fis_cupsResult
ÆEÆE -
>
ÆEÆE- .
TraerCUpsTablero
ÆEÆE/ ?
(
ÆEÆE? @
)
ÆEÆE@ A
{
ÇEÇE 	
return
ÈEÈE 
DACConsulta
ÈEÈE 
.
ÈEÈE 
TraerCUpsTablero
ÈEÈE /
(
ÈEÈE/ 0
)
ÈEÈE0 1
;
ÈEÈE1 2
}
ÉEÉE 	
public
ÊEÊE 
fis_rips_cups
ÊEÊE 
TraerCupsId
ÊEÊE (
(
ÊEÊE( )
int
ÊEÊE) ,
?
ÊEÊE, -
idCups
ÊEÊE. 4
)
ÊEÊE4 5
{
ËEËE 	
return
ÌEÌE 
DACConsulta
ÌEÌE 
.
ÌEÌE 
TraerCupsId
ÌEÌE *
(
ÌEÌE* +
idCups
ÌEÌE+ 1
)
ÌEÌE1 2
;
ÌEÌE2 3
}
ÍEÍE 	
public
ÏEÏE 
List
ÏEÏE 
<
ÏEÏE 3
%management_fis_refTipoConsultasResult
ÏEÏE 9
>
ÏEÏE9 :%
ListadoTipoConsultaJson
ÏEÏE; R
(
ÏEÏER S
)
ÏEÏES T
{
ĞEĞE 	
return
ÑEÑE 
DACConsulta
ÑEÑE 
.
ÑEÑE %
ListadoTipoConsultaJson
ÑEÑE 6
(
ÑEÑE6 7
)
ÑEÑE7 8
;
ÑEÑE8 9
}
ÒEÒE 	
public
ÔEÔE $
chatbot_ref_respuestas
ÔEÔE %
TraerRespuestaId
ÔEÔE& 6
(
ÔEÔE6 7
int
ÔEÔE7 :
?
ÔEÔE: ;
idRespuesta
ÔEÔE< G
)
ÔEÔEG H
{
ÕEÕE 	
return
ÖEÖE 
DACConsulta
ÖEÖE 
.
ÖEÖE 
TraerRespuestaId
ÖEÖE /
(
ÖEÖE/ 0
idRespuesta
ÖEÖE0 ;
)
ÖEÖE; <
;
ÖEÖE< =
}
×E×E 	
public
ØEØE #
chatbot_ref_preguntas
ØEØE $
TraerPreguntaId
ØEØE% 4
(
ØEØE4 5
int
ØEØE5 8
?
ØEØE8 9

idPregunta
ØEØE: D
)
ØEØED E
{
ÙEÙE 	
return
ÚEÚE 
DACConsulta
ÚEÚE 
.
ÚEÚE 
TraerPreguntaId
ÚEÚE .
(
ÚEÚE. /

idPregunta
ÚEÚE/ 9
)
ÚEÚE9 :
;
ÚEÚE: ;
}
ÛEÛE 	
public
ÜEÜE %
chatbot_ref_subprocesos
ÜEÜE &
TraerSubProcesoId
ÜEÜE' 8
(
ÜEÜE8 9
int
ÜEÜE9 <
?
ÜEÜE< =
idSub
ÜEÜE> C
)
ÜEÜEC D
{
İEİE 	
return
ŞEŞE 
DACConsulta
ŞEŞE 
.
ŞEŞE 
TraerSubProcesoId
ŞEŞE 0
(
ŞEŞE0 1
idSub
ŞEŞE1 6
)
ŞEŞE6 7
;
ŞEŞE7 8
}
ßEßE 	
public
àEàE "
chatbot_ref_procesos
àEàE #
TraerProcesoId
àEàE$ 2
(
àEàE2 3
int
àEàE3 6
?
àEàE6 7
	idProceso
àEàE8 A
)
àEàEA B
{
áEáE 	
return
âEâE 
DACConsulta
âEâE 
.
âEâE 
TraerProcesoId
âEâE -
(
âEâE- .
	idProceso
âEâE. 7
)
âEâE7 8
;
âEâE8 9
}
ãEãE 	
public
äEäE #
chatbot_ref_proyectos
äEäE $
TraerProyectoId
äEäE% 4
(
äEäE4 5
int
äEäE5 8
?
äEäE8 9

idProyecto
äEäE: D
)
äEäED E
{
åEåE 	
return
æEæE 
DACConsulta
æEæE 
.
æEæE 
TraerProyectoId
æEæE .
(
æEæE. /

idProyecto
æEæE/ 9
)
æEæE9 :
;
æEæE: ;
}
çEçE 	
public
èEèE 
List
èEèE 
<
èEèE +
fis_rips_cargue_LoteConsultas
èEèE 1
>
èEèE1 2
ConsultaCUVFIS
èEèE3 A
(
èEèEA B
string
èEèEB H
codCuv
èEèEI O
,
èEèEO P
ref
èEèEQ T 
MessageResponseOBJ
èEèEU g
MsgRes
èEèEh n
)
èEèEn o
{
éEéE 	
return
êEêE 
DACConsulta
êEêE 
.
êEêE 
ConsultaCUVFIS
êEêE -
(
êEêE- .
codCuv
êEêE. 4
,
êEêE4 5
ref
êEêE6 9
MsgRes
êEêE: @
)
êEêE@ A
;
êEêEA B
}
ëEëE 	
public
íEíE 
List
íEíE 
<
íEíE 6
(management_fis_cargueRips_consultaResult
íEíE <
>
íEíE< =!
ListadoRipsConsulta
íEíE> Q
(
íEíEQ R
string
íEíER X
codCuv
íEíEY _
)
íEíE_ `
{
îEîE 	
return
ïEïE 
DACConsulta
ïEïE 
.
ïEïE !
ListadoRipsConsulta
ïEïE 2
(
ïEïE2 3
codCuv
ïEïE3 9
)
ïEïE9 :
;
ïEïE: ;
}
ğEğE 	
public
óEóE 
List
óEóE 
<
óEóE =
/management_fis_cargueRips_hospitalizacionResult
óEóE C
>
óEóEC D(
ListadoRipsHospitalizacion
óEóEE _
(
óEóE_ `
string
óEóE` f
codCuv
óEóEg m
)
óEóEm n
{
ôEôE 	
return
õEõE 
DACConsulta
õEõE 
.
õEõE (
ListadoRipsHospitalizacion
õEõE 9
(
õEõE9 :
codCuv
õEõE: @
)
õEõE@ A
;
õEõEA B
}
öEöE 	
public
øEøE 
List
øEøE 
<
øEøE :
,management_fis_cargueRips_medicamentosResult
øEøE @
>
øEøE@ A%
ListadoRipsMedicamentos
øEøEB Y
(
øEøEY Z
string
øEøEZ `
codCuv
øEøEa g
)
øEøEg h
{
ùEùE 	
return
úEúE 
DACConsulta
úEúE 
.
úEúE %
ListadoRipsMedicamentos
úEúE 6
(
úEúE6 7
codCuv
úEúE7 =
)
úEúE= >
;
úEúE> ?
}
ûEûE 	
public
ıEıE 
List
ıEıE 
<
ıEıE <
.management_fis_cargueRips_otrosServiciosResult
ıEıE B
>
ıEıEB C'
ListadoRipsOtrosServicios
ıEıED ]
(
ıEıE] ^
string
ıEıE^ d
codCuv
ıEıEe k
)
ıEıEk l
{
şEşE 	
return
ÿEÿE 
DACConsulta
ÿEÿE 
.
ÿEÿE '
ListadoRipsOtrosServicios
ÿEÿE 8
(
ÿEÿE8 9
codCuv
ÿEÿE9 ?
)
ÿEÿE? @
;
ÿEÿE@ A
}
€F€F 	
public
‚F‚F 
List
‚F‚F 
<
‚F‚F <
.management_fis_cargueRips_procedimientosResult
‚F‚F B
>
‚F‚FB C'
ListadoRipsProcedimientos
‚F‚FD ]
(
‚F‚F] ^
string
‚F‚F^ d
codCuv
‚F‚Fe k
)
‚F‚Fk l
{
ƒFƒF 	
return
„F„F 
DACConsulta
„F„F 
.
„F„F '
ListadoRipsProcedimientos
„F„F 8
(
„F„F8 9
codCuv
„F„F9 ?
)
„F„F? @
;
„F„F@ A
}
…F…F 	
public
‡F‡F 
List
‡F‡F 
<
‡F‡F :
,management_fis_cargueRips_recienNacidoResult
‡F‡F @
>
‡F‡F@ A%
ListadoRipsRecienNacido
‡F‡FB Y
(
‡F‡FY Z
string
‡F‡FZ `
codCuv
‡F‡Fa g
)
‡F‡Fg h
{
ˆFˆF 	
return
‰F‰F 
DACConsulta
‰F‰F 
.
‰F‰F %
ListadoRipsRecienNacido
‰F‰F 6
(
‰F‰F6 7
codCuv
‰F‰F7 =
)
‰F‰F= >
;
‰F‰F> ?
}
ŠFŠF 	
public
ŒFŒF 
List
ŒFŒF 
<
ŒFŒF 9
+management_fis_cargueRips_transaccionResult
ŒFŒF ?
>
ŒFŒF? @$
ListadoRipsTransaccion
ŒFŒFA W
(
ŒFŒFW X
string
ŒFŒFX ^
codCuv
ŒFŒF_ e
)
ŒFŒFe f
{
FF 	
return
FF 
DACConsulta
FF 
.
FF $
ListadoRipsTransaccion
FF 5
(
FF5 6
codCuv
FF6 <
)
FF< =
;
FF= >
}
FF 	
public
‘F‘F 
List
‘F‘F 
<
‘F‘F 7
)management_fis_cargueRips_urgenciasResult
‘F‘F =
>
‘F‘F= >"
ListadoRipsUrgencias
‘F‘F? S
(
‘F‘FS T
string
‘F‘FT Z
codCuv
‘F‘F[ a
)
‘F‘Fa b
{
’F’F 	
return
“F“F 
DACConsulta
“F“F 
.
“F“F "
ListadoRipsUrgencias
“F“F 3
(
“F“F3 4
codCuv
“F“F4 :
)
“F“F: ;
;
“F“F; <
}
”F”F 	
public
–F–F 
List
–F–F 
<
–F–F 6
(management_fis_cargueRips_usuariosResult
–F–F <
>
–F–F< =!
ListadoRipsUsuarios
–F–F> Q
(
–F–FQ R
string
–F–FR X
codCuv
–F–FY _
)
–F–F_ `
{
—F—F 	
return
˜F˜F 
DACConsulta
˜F˜F 
.
˜F˜F !
ListadoRipsUsuarios
˜F˜F 2
(
˜F˜F2 3
codCuv
˜F˜F3 9
)
˜F˜F9 :
;
˜F˜F: ;
}
™F™F 	
public
 F F 
List
 F F 
<
 F F 4
&Management_chatbot_ref_proyectosResult
 F F :
>
 F F: ;
ChatBotProyectos
 F F< L
(
 F FL M
)
 F FM N
{
¡F¡F 	
return
¢F¢F 
DACConsulta
¢F¢F 
.
¢F¢F 
ChatBotProyectos
¢F¢F /
(
¢F¢F/ 0
)
¢F¢F0 1
;
¢F¢F1 2
}
£F£F 	
public
¥F¥F 
List
¥F¥F 
<
¥F¥F 3
%Management_chatbot_ref_procesosResult
¥F¥F 9
>
¥F¥F9 :
ChatBotProcesos
¥F¥F; J
(
¥F¥FJ K
int
¥F¥FK N
?
¥F¥FN O

idProyecto
¥F¥FP Z
)
¥F¥FZ [
{
¦F¦F 	
return
§F§F 
DACConsulta
§F§F 
.
§F§F 
ChatBotProcesos
§F§F .
(
§F§F. /

idProyecto
§F§F/ 9
)
§F§F9 :
;
§F§F: ;
}
¨F¨F 	
public
ªFªF 
List
ªFªF 
<
ªFªF 6
(Management_chatbot_ref_subprocesosResult
ªFªF <
>
ªFªF< = 
ChatBotSubProcesos
ªFªF> P
(
ªFªFP Q
int
ªFªFQ T
?
ªFªFT U
	idProceso
ªFªFV _
)
ªFªF_ `
{
«F«F 	
return
¬F¬F 
DACConsulta
¬F¬F 
.
¬F¬F  
ChatBotSubProcesos
¬F¬F 1
(
¬F¬F1 2
	idProceso
¬F¬F2 ;
)
¬F¬F; <
;
¬F¬F< =
}
­F­F 	
public
¯F¯F 
List
¯F¯F 
<
¯F¯F 4
&Management_chatbot_ref_preguntasResult
¯F¯F :
>
¯F¯F: ;
ChatBotPreguntas
¯F¯F< L
(
¯F¯FL M
int
¯F¯FM P
?
¯F¯FP Q
idSubProceso
¯F¯FR ^
)
¯F¯F^ _
{
°F°F 	
return
±F±F 
DACConsulta
±F±F 
.
±F±F 
ChatBotPreguntas
±F±F /
(
±F±F/ 0
idSubProceso
±F±F0 <
)
±F±F< =
;
±F±F= >
}
²F²F 	
public
´F´F 
List
´F´F 
<
´F´F 5
'Management_chatbot_ref_respuestasResult
´F´F ;
>
´F´F; <
ChatBotRespuestas
´F´F= N
(
´F´FN O
int
´F´FO R
?
´F´FR S

idPregunta
´F´FT ^
)
´F´F^ _
{
µFµF 	
return
¶F¶F 
DACConsulta
¶F¶F 
.
¶F¶F 
ChatBotRespuestas
¶F¶F 0
(
¶F¶F0 1

idPregunta
¶F¶F1 ;
)
¶F¶F; <
;
¶F¶F< =
}
·F·F 	
public
¹F¹F 
int
¹F¹F $
ChatBotInsertarProceso
¹F¹F )
(
¹F¹F) *"
chatbot_ref_procesos
¹F¹F* >
obj
¹F¹F? B
)
¹F¹FB C
{
ºFºF 	
return
»F»F 

DACInserta
»F»F 
.
»F»F $
ChatBotInsertarProceso
»F»F 4
(
»F»F4 5
obj
»F»F5 8
)
»F»F8 9
;
»F»F9 :
}
¼F¼F 	
public
¾F¾F 
int
¾F¾F '
ChatBotInsertarSubProceso
¾F¾F ,
(
¾F¾F, -%
chatbot_ref_subprocesos
¾F¾F- D
obj
¾F¾FE H
)
¾F¾FH I
{
¿F¿F 	
return
ÀFÀF 

DACInserta
ÀFÀF 
.
ÀFÀF '
ChatBotInsertarSubProceso
ÀFÀF 7
(
ÀFÀF7 8
obj
ÀFÀF8 ;
)
ÀFÀF; <
;
ÀFÀF< =
}
ÁFÁF 	
public
ÃFÃF 
int
ÃFÃF &
ChatBotInsertarPreguntas
ÃFÃF +
(
ÃFÃF+ ,#
chatbot_ref_preguntas
ÃFÃF, A
obj
ÃFÃFB E
)
ÃFÃFE F
{
ÄFÄF 	
return
ÅFÅF 

DACInserta
ÅFÅF 
.
ÅFÅF &
ChatBotInsertarPreguntas
ÅFÅF 6
(
ÅFÅF6 7
obj
ÅFÅF7 :
)
ÅFÅF: ;
;
ÅFÅF; <
}
ÆFÆF 	
public
ÈFÈF 
int
ÈFÈF '
ChatBotInsertarRespuestas
ÈFÈF ,
(
ÈFÈF, -$
chatbot_ref_respuestas
ÈFÈF- C
obj
ÈFÈFD G
)
ÈFÈFG H
{
ÉFÉF 	
return
ÊFÊF 

DACInserta
ÊFÊF 
.
ÊFÊF '
ChatBotInsertarRespuestas
ÊFÊF 7
(
ÊFÊF7 8
obj
ÊFÊF8 ;
)
ÊFÊF; <
;
ÊFÊF< =
}
ËFËF 	
public
ÍFÍF 
int
ÍFÍF /
!ChatBotInsertarRespuestasArchivos
ÍFÍF 4
(
ÍFÍF4 5-
chatbot_ref_respuestas_archivos
ÍFÍF5 T
obj
ÍFÍFU X
)
ÍFÍFX Y
{
ÎFÎF 	
return
ÏFÏF 

DACInserta
ÏFÏF 
.
ÏFÏF /
!ChatBotInsertarRespuestasArchivos
ÏFÏF ?
(
ÏFÏF? @
obj
ÏFÏF@ C
)
ÏFÏFC D
;
ÏFÏFD E
}
ĞFĞF 	
public
ÒFÒF 
List
ÒFÒF 
<
ÒFÒF >
0Management_chatbot_ref_respuestas_archivosResult
ÒFÒF D
>
ÒFÒFD E'
ChatBotRespuestasArchivos
ÒFÒFF _
(
ÒFÒF_ `
int
ÒFÒF` c
?
ÒFÒFc d
idRespuesta
ÒFÒFe p
)
ÒFÒFp q
{
ÓFÓF 	
return
ÔFÔF 
DACConsulta
ÔFÔF 
.
ÔFÔF '
ChatBotRespuestasArchivos
ÔFÔF 8
(
ÔFÔF8 9
idRespuesta
ÔFÔF9 D
)
ÔFÔFD E
;
ÔFÔFE F
}
ÕFÕF 	
public
×F×F -
chatbot_ref_respuestas_archivos
×F×F .!
TraerArchivoChatBot
×F×F/ B
(
×F×FB C
int
×F×FC F
?
×F×FF G
	idArchivo
×F×FH Q
)
×F×FQ R
{
ØFØF 	
return
ÙFÙF 
DACConsulta
ÙFÙF 
.
ÙFÙF !
TraerArchivoChatBot
ÙFÙF 2
(
ÙFÙF2 3
	idArchivo
ÙFÙF3 <
)
ÙFÙF< =
;
ÙFÙF= >
}
ÚFÚF 	
public
àFàF 
int
àFàF (
GuardarCargueJsonConsultas
àFàF -
(
àFàF- .+
fis_rips_cargue_LoteConsultas
àFàF. K
lote
àFàFL P
,
àFàFP Q
List
àFàFR V
<
àFàFV W&
fis_rips_cargue_consulta
àFàFW o
>
àFàFo p
lista
àFàFq v
)
àFàFv w
{
áFáF 	
return
âFâF 

DACInserta
âFâF 
.
âFâF (
GuardarCargueJsonConsultas
âFâF 8
(
âFâF8 9
lote
âFâF9 =
,
âFâF= >
lista
âFâF? D
)
âFâFD E
;
âFâFE F
}
ãFãF 	
public
åFåF 
int
åFåF .
 GuardarCargueJsonHospitalizacion
åFåF 3
(
åFåF3 4+
fis_rips_cargue_LoteConsultas
åFåF4 Q
lote
åFåFR V
,
åFåFV W
List
åFåFX \
<
åFåF\ ]-
fis_rips_cargue_hospitalizacion
åFåF] |
>
åFåF| }
listaåFåF~ ƒ
)åFåFƒ „
{
æFæF 	
return
çFçF 

DACInserta
çFçF 
.
çFçF .
 GuardarCargueJsonHospitalizacion
çFçF >
(
çFçF> ?
lote
çFçF? C
,
çFçFC D
lista
çFçFE J
)
çFçFJ K
;
çFçFK L
}
èFèF 	
public
êFêF 
int
êFêF +
GuardarCargueJsonMedicamentos
êFêF 0
(
êFêF0 1+
fis_rips_cargue_LoteConsultas
êFêF1 N
lote
êFêFO S
,
êFêFS T
List
êFêFU Y
<
êFêFY Z*
fis_rips_cargue_medicamentos
êFêFZ v
>
êFêFv w
lista
êFêFx }
)
êFêF} ~
{
ëFëF 	
return
ìFìF 

DACInserta
ìFìF 
.
ìFìF +
GuardarCargueJsonMedicamentos
ìFìF ;
(
ìFìF; <
lote
ìFìF< @
,
ìFìF@ A
lista
ìFìFB G
)
ìFìFG H
;
ìFìFH I
}
íFíF 	
public
ïFïF 
int
ïFïF -
GuardarCargueJsonotrosServicios
ïFïF 2
(
ïFïF2 3+
fis_rips_cargue_LoteConsultas
ïFïF3 P
lote
ïFïFQ U
,
ïFïFU V
List
ïFïFW [
<
ïFïF[ \-
fis_rips_cargue_otros_servicios
ïFïF\ {
>
ïFïF{ |
listaïFïF} ‚
)ïFïF‚ ƒ
{
ğFğF 	
return
ñFñF 

DACInserta
ñFñF 
.
ñFñF -
GuardarCargueJsonotrosServicios
ñFñF =
(
ñFñF= >
lote
ñFñF> B
,
ñFñFB C
lista
ñFñFD I
)
ñFñFI J
;
ñFñFJ K
}
òFòF 	
public
ôFôF 
int
ôFôF -
GuardarCargueJsonProcedimientos
ôFôF 2
(
ôFôF2 3+
fis_rips_cargue_LoteConsultas
ôFôF3 P
lote
ôFôFQ U
,
ôFôFU V
List
ôFôFW [
<
ôFôF[ \,
fis_rips_cargue_procedimientos
ôFôF\ z
>
ôFôFz {
listaôFôF| 
)ôFôF ‚
{
õFõF 	
return
öFöF 

DACInserta
öFöF 
.
öFöF -
GuardarCargueJsonProcedimientos
öFöF =
(
öFöF= >
lote
öFöF> B
,
öFöFB C
lista
öFöFD I
)
öFöFI J
;
öFöFJ K
}
÷F÷F 	
public
ùFùF 
int
ùFùF +
GuardarCargueJsonRecienNacido
ùFùF 0
(
ùFùF0 1+
fis_rips_cargue_LoteConsultas
ùFùF1 N
lote
ùFùFO S
,
ùFùFS T
List
ùFùFU Y
<
ùFùFY Z*
fis_rips_cargue_reciennacido
ùFùFZ v
>
ùFùFv w
lista
ùFùFx }
)
ùFùF} ~
{
úFúF 	
return
ûFûF 

DACInserta
ûFûF 
.
ûFûF +
GuardarCargueJsonRecienNacido
ûFûF ;
(
ûFûF; <
lote
ûFûF< @
,
ûFûF@ A
lista
ûFûFB G
)
ûFûFG H
;
ûFûFH I
}
üFüF 	
public
şFşF 
int
şFşF *
GuardarCargueJsonTransaccion
şFşF /
(
şFşF/ 0+
fis_rips_cargue_LoteConsultas
şFşF0 M
lote
şFşFN R
,
şFşFR S
List
şFşFT X
<
şFşFX Y)
fis_rips_cargue_transaccion
şFşFY t
>
şFşFt u
lista
şFşFv {
)
şFşF{ |
{
ÿFÿF 	
return
€G€G 

DACInserta
€G€G 
.
€G€G *
GuardarCargueJsonTransaccion
€G€G :
(
€G€G: ;
lote
€G€G; ?
,
€G€G? @
lista
€G€GA F
)
€G€GF G
;
€G€GG H
}
GG 	
public
ƒGƒG 
int
ƒGƒG (
GuardarCargueJsonUrgencias
ƒGƒG -
(
ƒGƒG- .+
fis_rips_cargue_LoteConsultas
ƒGƒG. K
lote
ƒGƒGL P
,
ƒGƒGP Q
List
ƒGƒGR V
<
ƒGƒGV W'
fis_rips_cargue_urgencias
ƒGƒGW p
>
ƒGƒGp q
lista
ƒGƒGr w
)
ƒGƒGw x
{
„G„G 	
return
…G…G 

DACInserta
…G…G 
.
…G…G (
GuardarCargueJsonUrgencias
…G…G 8
(
…G…G8 9
lote
…G…G9 =
,
…G…G= >
lista
…G…G? D
)
…G…GD E
;
…G…GE F
}
†G†G 	
public
ˆGˆG 
int
ˆGˆG '
GuardarCargueJsonUsuarios
ˆGˆG ,
(
ˆGˆG, -+
fis_rips_cargue_LoteConsultas
ˆGˆG- J
lote
ˆGˆGK O
,
ˆGˆGO P
List
ˆGˆGQ U
<
ˆGˆGU V&
fis_rips_cargue_usuarios
ˆGˆGV n
>
ˆGˆGn o
lista
ˆGˆGp u
)
ˆGˆGu v
{
‰G‰G 	
return
ŠGŠG 

DACInserta
ŠGŠG 
.
ŠGŠG '
GuardarCargueJsonUsuarios
ŠGŠG 7
(
ŠGŠG7 8
lote
ŠGŠG8 <
,
ŠGŠG< =
lista
ŠGŠG> C
)
ŠGŠGC D
;
ŠGŠGD E
}
‹G‹G 	
}
‘G‘G 
}’G’G 