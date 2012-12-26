﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Z.Util
{
    /// <summary>
    /// 拼音字典
    /// </summary>
    public class PT
    {
        #region 首字母数组

        /// <summary>
        /// 首字母数组
        /// </summary>
        static string[][] PtArray = new string[][]{
	        new string[]{"@",""},
	        new string[]{"A","啊阿嗄锕錒爱矮挨哎碍癌艾唉哀蔼隘埃皑嗌嫒瑷暧捱砹嗳锿霭伌僾凒叆啀嘊噯塧壒娾嬡嵦愛懓敱昹曖欸毐溰溾濭璦皚皧瞹硋礙薆藹譪譺賹躷鎄鑀靄馤鱫鴱按安暗岸俺案鞍氨胺庵揞犴铵桉谙鹌埯黯侒儑唵啽垵堓婩媕峖晻洝痷盦盫罯腤荌萻葊蓭誝諳豻貋銨錌闇隌雸鞌韽馣鮟鵪鶕昂肮盎岇昻枊醠骯袄凹傲奥熬懊敖翱澳媪廒骜嗷坳遨聱螯獒鏊鳌鏖岙厫嗸垇奡奧媼嫯岰嶅嶴慠扷抝摮擙柪滶爊獓璈磝翶翺芺蔜襖謷謸軪镺隞驁鰲鷔鼇八"},
	        new string[]{"B","把吧爸拔罢跋巴芭扒坝霸叭靶笆疤捌粑茇岜鲅魃菝灞仈伯叐哵坺垻墢壩夿妭弝抜抪捭朳柭欛炦犮玐癹矲紦罷羓胈萆蚆覇詙豝軷釛釟颰魞鮁鮊鲃鲌鼥百白摆败柏拜佰稗呗掰唄庍拝挀擺敗栢竡粨粺絔薜薭襬贁鞁鞴韛半办班般拌搬版斑板伴扳扮瓣颁绊癍坂钣舨阪瘢並坢姅岅彬怑攽斒昄湴瓪秚粄絆蝂覂豳跘辦辧辨辩辬辯鈑鉡闆靽頒魬帮棒绑磅镑邦榜蚌傍梆谤浜蒡垹埲塝幇幚幫挷捠棓牓玤硥稖綁縍艕蜯謗邫鎊鞤包抱报饱保暴薄宝爆剥豹雹褒堡苞胞鲍龅孢煲褓鸨趵葆佨儤剝勹勽堢報媬嫑寚寳寶忁怉曓珤窇笣簿緥菢蕔藵虣袌襃賲鉋鑤铇闁靌靤飹飽駂骲髱鮑鳵鴇齙宀萡被北倍杯背悲备碑卑贝辈钡焙狈惫臂褙悖蓓鹎鐾邶孛陂碚俻俾偝偹備僃愂憊揹昁桮梖波牬犕狽珼琲痺盃禙糒苝藣蛽誖貝軰輩鄁鉳鋇鐴骳鵯本奔苯笨锛贲畚坌倴喯夲奙捹撪桳楍泍渀犇翉賁輽逩錛蹦绷甭崩迸泵甏嘣伻傰塴奟嵭琣琫痭絣綳繃菶跰逬鏰镚閍鞛比笔闭鼻碧必避逼毕彼鄙壁蓖币弊蔽毙庇敝陛毖痹秕荸匕裨畀嬖狴筚箅篦舭荜襞庳铋跸吡愎滗濞璧哔髀弼妣婢佊佖偪咇啚嗶坒堛夶奰妼娝嬶屄幣幤廦弻彃怭悂愊斃朼枈柀柲梐楅檗毴沘湢滭潷煏熚獘獙珌畁畢疕疪皀皕禆稫筆箆篳粃粊紴綼縪繴罼聛腷苾萞蓽蘗蜌袐襅襣觱詖诐豍貏貱贔赑跛蹕躃躄邲鄨鄪鈚鉍鎞鏎閇閉閟鞞鞸韠飶饆馝駜驆髲魓魮鮅鮩鰏鲾鵖鷝鷩鸊鼊髟边变便遍编扁贬鞭卞辫忭砭匾汴碥蝙褊鳊笾苄窆弁缏煸変峅徧惼抃揙昪汳炞牑猵獱疺稨箯籩糄編緶艑萹藊覍變貶辡辮邉邊釆鍽閞鞕頨鯾鯿鴘鶣表标彪膘婊飑飙鳔瘭飚镳裱骠镖俵儦墂幖摽標檦淲滮瀌熛爂猋穮脿臕蔈藨褾諘謤贆錶鏢鑣颮颷飆飇飈飊驃驫骉鰾麃别憋鳖瘪蹩別彆徶癟莂虌蛂蟞襒鱉鼈龞宾濒摈斌滨膑殡缤髌傧槟鬓镔玢儐擯椕殯氞汃濱濵瀕瑸璸砏繽臏虨豩賓賔邠鑌霦顮髕髩鬂鬢并病兵冰丙饼秉柄炳摒禀邴仌併倂偋傡垪寎幷庰怲抦掤昞昺栟栤梹棅檳氷癛癝眪稟窉竝苪蛃誁鈵鉼鋲陃靐鞆餅餠疒拨播泊博驳玻勃菠钵搏脖帛舶渤铂箔膊卜礴亳鹁踣啵簸钹饽擘仢侼僠僰壆孹嶓愽懪挬撥桲欂浡淿湐煿牔犦犻狛猼瓝瓟癶盋砵碆磻礡秡箥簙糪缽胉艊苩葧蔔袚袯襏襮譒豰蹳郣鈸鉑鉢鋍鎛鑮镈餑餺馎馛馞駁駮驋髆髉鱍鵓不步补布部捕哺埠怖瓿逋晡钸钚醭卟佈勏吥咘埗峬庯廍悑捗柨歨歩篰荹蔀補誧踄轐郶鈈鈽餔餢鳪鵏鸔"},
	        new string[]{"C","擦礤嚓傪攃礸蔡遪才菜采材财裁猜踩睬彩倸啋埰婇寀採棌綵縩纔財跴蚕残参惨惭餐灿骖璨黪粲儏參叄叅喰嬠嵾慘慙慚憯殘湌澯燦爘薒蝅蠶蠺謲飡飱驂黲藏仓沧舱苍伧仺倉凔匨嵢欌滄濸獊艙蒼蔵螥賶鑶鶬鸧草操曹槽糙嘈艚螬漕傮嶆愺慒撡曺肏艸艹蓸褿襙鏪騲册侧策测厕恻側冊厠墄廁惻憡拺敇測笧筞箣粣荝萗萴蓛刂岑涔梣笒曾层蹭噌層嶒竲驓插叉馇杈锸偛嗏扠扱挿揷肞臿艖芆訍銟仒丷玣榌磦巭毝溊曽厂穪拆磣掺孱摲傖罉屮硶膥查茶差岔搽察茬碴刹诧槎镲衩汊檫姹侘垞奼嵖摖査猹疀秅紁褨詧詫釵鍤鎈鑔钗靫餷柴豺瘥虿侪儕喍囆搓犲祡茈蠆袃产缠搀阐颤铲谗蝉馋觇婵蒇谄冁廛蟾羼忏潺禅骣躔澶丳僝儃儳刬剗剷劖啴嚵囅壥嬋嵼巉幝幨懴懺摻攙旵梴棎欃毚浐湹滻潹瀍瀺灛煘燀獑產産硟磛禪簅緾繟繵纏纒艬蕆蟬裧襌襜覘誗諂譂讇讒讖谶辿鄽酁醦鋋鋓鏟鑱镵閳闡韂顫饞唱常场尝肠畅昌敞倡偿猖鲳氅菖惝嫦徜鬯阊怅伥昶苌娼仧倀僘償兏厰嘗嚐場塲廠悵晿暢棖椙淐琩瑺瓺甞畼腸膓萇蟐裮誯鋹鋿錩鏛锠長镸閶韔鯧鱨鲿鼚朝抄超吵潮巢炒嘲绰钞怊焯耖晁仦仯巐巣弨樔欩漅煼牊眧窲綽繛罺觘訬謿趠轈鄛鈔麨鼂鼌车撤扯掣彻尺澈坼砗伡偖勶唓奲徹摰撦斥池烢烲爡瞮硨硩聅莗蛼車迠頙趁称辰臣尘晨沉陈衬橙忱郴榇抻谌碜宸龀嗔琛儭嚫塵墋夦愖揨敐曟棽樄櫬煁疢瘎瞋稱綝茞莀莐蔯薼螴襯訦諃諶謓賝贂趂趻踸軙迧鈂闖闯陳霃鷐麎齓齔秤成乘撑城程呈诚惩逞骋澄承塍柽埕铖酲裎枨蛏丞瞠乗侱偁堘塖娍宬峸庱徎悜憆憕懲挰掁摚撐朾棦椉橕檉檙泟洆浾溗澂瀓爯牚珵珹琤畻睈碀窚竀筬絾緽脀脭荿蟶觕誠赪赬郕鋮鏳鏿阷靗頳饓騁騬鯎吃迟翅痴赤齿耻持侈弛驰炽踟茌墀饬媸豉褫敕哧瘛蚩啻鸱眵螭篪魑叱彳笞嗤傺侙勅卶叺呎呮喫噄垑妛岻彨彲恜恥慗憏懘抶拸搋摛摴攡杘欼歯湁漦灻烾熾瓻痓痸癡瞝竾筂粚絺翄翤翨耛肔胣腟蚇蚳袲袳裭訵謘貾赿趍趐趩跅跮迣遅遟遫遲鉓鉹銐雴飭饎馳鴟鵄鶒鷘麶黐齒齝冲虫充宠崇艟忡舂铳憧茺喠嘃寵崈徸憃揰摏沖浺漴爞珫痋緟罿翀蝩蟲衝褈蹖銃隀抽愁臭仇丑稠绸酬筹踌畴瞅惆俦帱瘳雠丒侴偢儔吜嬦幬懤搊杽栦椆殠燽犨犫疇皗矁篘籌紬絒綢臰菗薵裯詶讎讐诪躊遚酧醔醜醻雔魗出处初锄除触橱楚础储畜滁矗搐躇厨雏楮杵刍怵绌亍憷蹰黜蜍樗俶傗儊儲処嘼埱媰岀幮廚拀斶椘橻檚櫉櫥欪歜滀濋犓珿琡璴礎竌竐篨絀耡臅芻蒢蒭處蟵褚觸諔豖豠貙趎踀躕鄐鋤閦雛鶵齣齭齼撮欻揣膪啜踹腄膗穿船传串川喘椽氚遄钏舡舛巛傳僢剶圌堾暷歂汌猭玔瑏篅舩荈賗踳輲釧镩窗床创疮怆傸刅刱剏剙創噇囪囱愴摐牀牎牕瘡磢窓窻葱蔥吹垂炊锤捶槌棰陲倕埀惙搥桘箠菙錘鎚顀龡春唇纯蠢醇淳椿蝽莼鹑偆媋惷旾暙杶槆橁櫄浱湻滣漘犉瑃睶箺純脣萅萶蒓賰輴醕錞陙鯙鰆鶉鶞戳踔龊辍促嚽娕娖婥擉歠涰磭簇蔟輟辵逴酫鑡齪齱次此词瓷慈雌磁辞刺茨疵赐呲鹚祠糍佌佽偨刾垐堲嬨嵯嵳庛措朿柌栨泚濨玼珁甆皉礠絘縒茦莿薋蛓螆蠀詞賜趀跐辝辤辭鈶飺餈骴髊鴜鶿鷀齹从丛匆聪琮枞淙璁骢苁叢婃孮従徖從忩怱悤悰憁暰棇楤樅樬樷欉漎漗潀潈潨灇焧熜爜瑽瞛篵緫繱聡聦聰茐蓯藂蟌誴謥賨賩鏦騘驄凑楱辏腠湊輳粗醋徂猝蹙殂蹴噈媨憱捽瘄瘯縬蔖誎趗踧蹵錯错顣麁麄麤鼀窜蹿篡汆爨撺巑攅攛櫕欑殩熶竄簒躥鑹催脆摧翠崔淬瘁粹璀啐悴萃毳榱伜凗啛墔嶉忰慛椊槯漼濢焠獕琗疩皠磪竁翆脃膬膵臎襊趡鏙顇村寸存忖皴侟刌吋拵洊澊竴籿踆邨挫磋厝鹾脞痤蹉锉矬剉剒夎棤瑳睉莝莡蒫蓌虘逪遳醝銼鹺咑殦仩伬鐣"},
	        new string[]{"D","呆肑仛蔕单亶僤単單憚掸撣多夛瘨眈铛僜盯虰鐺黨坻剟哆坘慸柢跢踶軧鍉擣眣鮘鬌楯沌踱蹲大答达打搭瘩笪耷哒褡疸怛靼妲嗒鞑亣剳匒呾噠垯墶撘汏溚炟燵畣眔繨羍胆荙薘蟽詚跶躂迏迖迭逹達鎝鐽韃龖龘带代戴待袋逮歹贷怠傣殆玳迨岱甙黛绐埭侢叇嘚垈帒帯帶廗懛曃柋棣毒瀻獃瑇簤紿緿艜蚮蝳螮襶貸蹛軑軩轪遞遰霴靆鴏黱但蛋担弹淡丹耽旦氮诞郸惮澹瘅萏殚聃箪儋啖刐勯匰啗啿嘾噉嚪妉媅帎彈憺擔柦殫沊泹澸狚玬瓭甔疍癉癚砃禫窞簞紞耼聸腅膽蜑衴褝觛訑誕贉躭鄲酖霮頕餤饏馾駳髧鴠黕卩亻当党挡档荡谠宕菪凼裆砀儅噹圵垱壋婸愓擋攩檔欓氹澢灙珰璗璫瓽當盪瞊碭礑筜簜簹艡蕩蘯蟷襠譡讜趤逿闣雼到道倒刀岛盗稻捣悼导蹈祷纛忉氘叨噵壔宲導島嶋嶌嶹捯搗朷椡槝檤瓙盜禂禱稲翢翿舠菿虭衜衟軇釖陦隝隯魛鱽的地得德底锝徳恴悳惪淂登鍀哋揼扥扽等灯邓瞪凳蹬磴镫噔嶝戥簦墱嬁櫈燈璒竳艠覴豋鄧鐙隥第低敌抵滴帝递嫡弟缔堤涤笛迪狄蒂觌邸谛诋嘀骶羝氐睇娣荻碲镝籴砥仾俤偙僀厎呧唙啇啲嚁坔埊埞墑墬奃媂嵽嶳廸弔弚弤彽怟扚拞掋揥摕敵旳杕枤梊梑楴滌焍牴玓珶甋眱磾祶禘篴糴締聜腣苖菂菧蔋蔐藋藡袛觝詆諦豴趆逓釱鉪鏑阺隄靮鞮頔馰髢魡鯳鸐嗲点电店殿淀掂颠垫碘惦奠典佃靛滇甸踮钿坫阽癫簟玷巅癜傎厧墊壂奌婝婰嵮巓巔扂攧敁敟椣槙橂澱琔癲磹蒧蕇蜔蹎鈿電顚顛驔點齻丶掉钓叼吊雕刁碉凋铞铫鲷貂伄刟奝屌弴彫汈琱瘹瞗窎窵竨簓蛁訋調釣鈟銱鋽錭鑃雿颩鮉鯛鳭鵰鼦爹跌叠碟蝶谍牒堞瓞揲蹀耋鲽垤喋啑峌幉怢恎惵戜挕曡殜氎牃畳疂疉疊絰绖耊胅臷艓苵蜨褋褺詄諜趃镻鮙鰈嚸顶定订叮丁钉鼎锭町玎铤腚碇疔仃耵酊啶奵嵿帄忊椗甼矴碠磸聢萣薡訂釘鋌錠鐤靪頂顁飣饤丢铥丟銩动东懂洞冻冬董栋侗恫峒鸫垌胨胴硐氡岽咚倲働凍動埬墥姛娻嬞峝崠崬戙挏昸東棟氭涷湩笗箽腖苳菄蕫蝀諌迵霘駧鮗鯟鶇鶫鼕都斗豆逗陡抖痘兜读蚪窦篼蔸乧侸兠凟剅吺唗斣枓梪橷毭浢渎瀆竇脰艔荳讀郖酘鋀钭閗闘阧餖饾鬥鬦鬪鬬鬭度渡堵独肚镀赌睹杜督犊妒顿蠹笃嘟椟牍黩髑芏剢剫匵厾妬嬻帾暏櫝殬殰涜牘犢獨琽瓄皾秺篤荰螙蠧裻襩覩読讟豄賭贕醏錖鍍鑟闍阇靯韇韣韥頓騳黷段短断端锻缎椴煅簖偳剬塅媏斷毈瑖碫籪緞腶葮褍躖鍛鍴对队堆兑敦镦碓怼憝兊兌垖塠夺奪対對嵟憞懟濧瀩痽磓祋綐薱譈譵鐓鐜陮隊頧鴭吨墩钝盾遁趸盹礅炖砘伅噸墪壿惇撉撴橔潡燉犜獤碷腞蜳踲蹾躉逇遯鈍驐朵舵剁垛跺惰堕掇躲咄铎裰哚缍亸凙刴喥嚉嚲垜埵墮墯媠嫷尮崜嶞憜挅挆敓敚敠敪朶柮桗椯毲痥綞茤趓跥躱鈬鐸陊陏飿饳鮵鵽妸亇徚蓞蝊縀罀鼑迚"},
	        new string[]{"E","呃呝阨阸咹頞誀饿额鹅蛾扼俄讹遏峨娥恶厄鄂锇谔垩锷阏萼苊轭婀莪鳄颚腭愕噩鹗屙偔僫匎卾吪咢噁囮堊堮妿姶娿屵岋峉峩崿廅悪惡戹搹擜枙櫮歞歺涐湂珴琧皒睋砈砐砨硆磀蕚蘁蚅蝁覨訛詻誐諤譌讍豟軛軶迗遌遻鈋鋨鍔鑩頟額顎餓餩騀魤鰐鱷鵈鵝鵞鶚齶恩摁蒽嗯奀峎煾鞥而二耳儿饵尔贰洱珥鲕鸸佴迩铒侕児兒刵咡唲尒尓峏弍弐栭栮樲毦洏爾粫耏聏胹荋薾衈袻貮貳趰輀轜邇鉺陑隭餌駬髵鮞鴯发鈪诶"},
	        new string[]{"F","笩茷分朌籓肦鳻紡纺蚄枹怫杮茀萯蜚炃燌獖蟦鐼俸唪埄漨祊佛复幅拂服畐胇費费踾馥鶝封份訜枋蕃噃帗榑番發艴襎尃拊捬秿輹附岎鲋畗埅法罚伐乏筏阀珐垡砝佱傠姂廢彂栰橃汎沷泛灋琺発瞂罰罸蕟藅醗鍅閥髪髮反饭翻犯凡帆返繁烦贩范樊藩矾钒燔蘩畈蹯梵幡仮凢凣勫匥墦奿婏嬎嬏嬔忛憣払旙旛杋柉棥楓橎氾渢滼瀪瀿煩犿璠盕礬笲笵範籵緐繙羳膰舤舧薠蠜訉販軓軬轓辺釩鐇颿飜飯飰鱕鷭攵犭放房防芳方访仿坊妨肪钫邡舫鲂倣匚堏旊昉昘汸淓牥瓬眆訪趽鈁錺髣魴鴋鶭非飞肥肺废匪吠沸菲诽啡篚腓扉妃斐狒芾悱镄霏翡榧淝鲱绯痱俷剕厞奜婓婔屝廃昲暃曊朏棐橨櫠渄濷猆疿癈砩祓笰紼緋绋胐萉蕜蕡蜰裵裶誹鐨陫靅靟飛飝餥馡騑騛髴鯡鼣芬粉坟奋愤纷忿粪酚焚吩氛汾棼瀵鲼偾鼢僨坆坋墳奮妢帉幩弅憤昐朆枌梤棻燓瞓秎竕糞紛羒羵翂膹蒶蚠蚡衯豮豶躮轒鈖隫雰餴饙馚馩魵鱝黂黺鼖风逢缝蜂丰枫疯冯奉讽凤峰锋烽砜酆葑沣仹偑僼凨凬凮堸夆妦寷峯崶捀摓桻檒沨浲湗溄灃炐焨煈犎猦琒甮瘋盽碸篈綘縫肨舽艂蘴諷豐賵赗鄷鋒鎽鏠靊風飌馮鳯鳳鴌麷覅仏坲梻否缶垺妚紑缹缻芣雬鴀副扶浮富福负伏付俯斧赴缚夫父符孵敷赋辅府腐腹妇抚覆辐肤氟俘傅讣弗涪袱甫釜腑阜咐黼苻趺跗蚨幞茯滏蜉菔蝠鳆蝮绂赙罘稃匐麸凫桴莩孚驸呋郛芙黻乀伕俌偩冨冹刜咈哹坿垘妋姇娐婦媍岪峊巿弣彿復怤懯撫旉枎柎柫栿棴椨椱沕泭洑澓炥烰焤玞玸琈甶畉癁盙砆祔禣稪竎筟箙簠粰糐紨紱絥綍綒緮縛罦翇胕膚艀荂荴葍蕧虙蚥蚹蛗蝜衭袝複褔襆襥覄訃詂諨豧負賦賻軵輔輻邞郙鄜酜酻釡鈇鉘鉜鍑鍢陚韍韨颫駙鬴鮄鮒鮲鰒鳧鳬鳺鴔鵩麩麬麱旮蠭"},
	        new string[]{"G","絠隑广干玵棍崗綆绠汵焻箉睪膭佮輵鬲鰪噶胳嘎钆伽尬尕尜呷嘠玍錷魀该改盖概钙溉戤垓丐陔赅乢侅匃匄姟峐忋摡晐杚槩槪漑瓂畡祴絯荄葢蓋該豥賅賌郂鈣鎅赶感敢竿甘肝柑杆赣秆旰酐矸疳泔苷擀绀橄澉淦尴坩个乹亁仠倝凎凲尲尶尷幹忓扞攼桿榦檊漧灨玕皯盰稈笴筸篢簳粓紺芉虷衦詌贑贛趕迀骭魐鱤鳡鳱刚钢纲港缸岗杠冈肛筻罡戆冮剛堈堽岡掆棡槓溝焵牨犅疘矼碙綱罁罓釭鋼鎠高搞告稿膏篙羔糕镐皋郜诰杲缟睾槔锆槁藁勂吿夰峼暠槀槹橰檺櫜滜獔皐祮祰禞稁稾筶縞羙臯菒藳誥鋯鎬韟餻髙鷎鷱鼛各歌割哥搁格阁隔革咯葛戈鸽疙铬硌骼袼塥虼圪镉舸嗝膈搿纥哿個匌呄彁愅戓戨扢挌擱敋槅櫊滆滒牫牱犵獦箇肐臈臵茖蛒裓觡諽謌轕鉻鎘鎶铪閣閤鞈鞷韐韚騔鮯鴚鴿给跟根哏茛亘艮揯搄更耕梗耿庚羹埂赓鲠哽亙刯堩峺挭椩浭焿畊絚緪縆羮莄菮賡郠骾鯁鶊鹒嗰工公功共弓攻宫供恭拱贡躬巩汞龚肱觥珙蚣匑匔厷咣唝塨宮幊廾愩慐拲杛栱熕碽糼羾觵貢躳輁鞏髸龏龔够沟狗钩勾购构苟垢岣彀枸鞲觏缑笱诟遘媾篝佝傋冓坸夠姤抅搆撀構泃煹玽簼緱耇耈耉茩蚼袧褠覯訽詬豿購鈎鉤雊韝古股鼓谷故孤箍姑顾固雇估咕骨辜沽蛊菇梏鸪汩轱崮菰鹘钴臌酤呱鲴诂牯瞽毂锢牿痼觚蛄罟嘏傦僱凅唂唃啒嗗堌夃嫴尳崓怘愲抇柧棝榖榾橭泒淈濲瀔焸皷盬祻稒穀笟箛篐糓縎罛羖胍脵苽蓇薣蛌蠱詁軱軲轂逧鈲鈷錮頋顧餶馉鮕鯝鴣鶻鼔挂刮瓜寡剐褂卦鸹诖冎剮劀叧啩坬掛歄煱絓緺罣罫詿銽颪騧鴰怪拐乖掴噲恠枴柺关管官观馆惯罐灌冠贯棺盥莞掼涫鳏鹳倌丱卝婠悹悺慣摜果桄樌毌泴淉潅爟琯瓘痯瘝癏矔礶祼窤筦罆舘萖蒄覌観觀貫躀輨遦錧鏆鑵関闗關雚館鰥鱞鱹鳤鸛光逛犷胱侊俇僙垙姯広廣挄撗欟洸灮炗炚炛烡獷珖硄臦臩茪輄銧黆归贵鬼跪轨规硅桂柜龟诡闺瑰圭刽癸庋宄桧刿鳜鲑皈匦妫晷簋炅亀佹劊劌匭厬垝姽媯嫢嬀嶲巂帰庪恑摫撌攰攱昋椝椢槶槻槼櫃櫷歸氿湀溎珪璝瓌瞡瞶祪禬窐筀簂胿茥蓕蛫螝蟡袿襘規觤詭貴軌邽郌閨陒鞼騩鬶鬹鮭鱖鱥龜滚辊鲧衮磙绲惃滾璭睔睴緄緷蓘蔉袞裷謴輥鮌鯀过国裹锅郭埚椁聒馘猓崞帼呙虢蜾蝈啯嘓囯囶囻圀國墎幗惈慖摑楇槨漍濄瘑粿綶聝腂腘菓蔮蟈褁輠過鈛鍋鐹餜馃哈給芶皼焹竃"},
	        new string[]{"H","呵烠閡阂餲厈垾屽盒頇顸礉夯閈闬灬俿捇箎摢蚝毼憾屶隓啈曷齃怀还核胲骸佄咁捍汗浛豃釬頏颃浩澔獋蒿蛤合颌佫秴紇菏詥鉿頜魺鲄恆絙红嗊渱紅虹銾魟鹄嗀櫎滑瓠磆鵠咶諣趏懽鵍恍横潢趪檜櫰絵繪绘浑混渾划唬活膕海害氦孩骇亥嗨醢咴嗐嚡塰烸還酼餀饚駭駴嘿喊含寒汉旱酣韩焊涵函憨翰罕撼悍邯邗菡撖瀚蚶焓颔晗鼾傼凾哻唅圅娢嫨崡嵅晘暵梒浫涆淊漢澏澣熯爳猂琀甝皔睅筨肣莟蔊蛿蜬蜭螒譀谽銲鋎鋡闞雗韓頷顄馠馯駻鬫魽鶾行航杭沆绗珩垳斻桁笐筕絎苀蚢貥迒魧好号嚎壕郝毫豪耗昊颢灏嚆嗥皓濠薅傐儫呺哠嘷噑恏悎昦晧暤暭曍椃淏滈灝獆皜皞皡皥秏竓籇翯聕薃號虠蠔諕譹鄗顥鰝和喝河禾何荷贺赫褐鹤涸嗬劾盍翮阖壑诃呼咊哬啝嗃垎姀寉峆惒抲柇楁欱渮澕焃煂熆熇燺爀狢癋皬盇盉碋篕籺粭萂藿蠚覈訶訸貈賀鉌鑉闔霍靍靎靏鞨饸鶡鶮鶴鸖鹖麧齕龁龢黑嬒潶黒很狠恨痕佷拫詪鞎恒哼衡亨蘅堼姮悙橫涥烆狟胻脝訇鑅鴴鵆鸻乊轰哄洪宏烘鸿弘讧蕻闳薨黉荭泓仜叿吰吽嚝垬妅娂宖屸彋揈撔晎汯浤渹潂澋澒灴焢玒玜硔硡竑竤篊粠紘紭綋纮翃翝耾舼苰葒葓訌谹谼谾軣輷轟鈜鉷鋐鍧閎閧闀闂霐霟鞃鬨鴻黌后厚吼喉侯候猴鲎篌堠後逅糇骺瘊垕帿洉犼睺矦翭葔豞郈鄇銗鍭餱鮜鯸鱟鲘齁湖户虎壶互胡护糊弧忽狐蝴葫沪乎瑚鹕冱怙鹱笏戽扈浒祜醐琥囫烀轷煳斛猢惚岵滹觳唿槲乕冴匢匫喖嘑嘝嚛垀壷壺婟媩嫭嫮寣帍幠弖恗戶戸搰擭昈昒曶枑楜槴歑汻沍泘淴滬滸濩瀫焀熩瓳穫箶簄粐絗綔縠膴芔苸萀蔛蔰虍虖虝螜衚許謼護軤鄠鋘錿鍙鍸雐雽韄頀頶餬鬍魱鯱鰗鱯鳠鳸鴩鶘鶦鸌话花化画华哗猾豁铧桦骅砉劃劐嘩埖姡婲婳嫿嬅崋摦撶杹椛槬樺檴浍澅澮畫畵硴糀繣舙芲華蕐蘤蘳螖觟話誮諙譮錵鏵驊鷨黊坏淮槐徊踝佪壊壞懐懷瀤耲蘹蘾褢褱换唤环患缓欢幻宦涣焕豢桓痪漶獾擐逭鲩鬟寰奂锾圜洹萑缳浣喚嚾圂堚奐寏峘嵈愌懁換攌梙槵欥歓歡渙澴烉煥瑍環瓛瘓睆瞏糫綄緩繯羦肒荁萈藧讙豲貆貛輐轘酄鍰镮闤阛雈驩鯇鯶鰀鴅鹮黄慌晃荒簧凰皇谎惶蝗磺煌幌隍肓篁徨鳇遑癀湟蟥璜偟兤喤堭塃墴奛媓宺崲巟怳愰揘晄曂楻榥滉炾熀熿獚瑝皝皩穔縨艎葟衁詤諻謊鍠鎤鐄锽韹餭騜鰉鱑鷬黃回会灰挥汇辉毁悔惠晦徽恢秽慧贿蛔讳卉烩诲彗珲蕙喙恚哕晖隳麾诙蟪茴洄虺荟缋僡儶匯嘒噅噕噦嚖囘囬圚婎媈孈寭屷幑廻廽彙彚徻恛恵憓拻揮暉暳會楎槥橞檅檓櫘毀毇泋洃湏滙潓瀈灳烣煇燬燴獩琿璤璯痐瘣睳瞺禈穢篲繢翙翚翬翽蔧薈薉藱蛕蜖袆詯詼誨諱譓譭譿豗賄輝迴逥鏸鐬闠阓靧韢顪餯鮰鰴昏荤婚魂阍馄溷诨俒倱忶惛惽慁掍昬棔殙涽焝睧睯繉葷觨諢閽餛鼲或火伙货获祸惑嚯镬耠攉锪蠖钬夥佸俰剨吙咟嚄嚿奯掝旤曤沎湱漷瀖獲癨眓矆矐禍秮秳耯臛艧蒦謋貨邩鈥鍃鑊閄雘靃騞夻丌殨颒硘毜呍"},
	        new string[]{"J","鯦浇澆覵戔畟筴簎硛捷接苴斺渐漸螹剿劋勦摷焣俥烥跈净淨瀞卙噭茧吷萕鮆怚脨鋑浚庴丼嘄覿俊伋仅夹芥奸鰔咎介吤鉀钾鴐颈掶頸喼句拘鮈贾角賈颳叏夬矜菅嶡槣觖赽趹蹶鴂鴃棞妎兯椷靬膠揭繳缴餄几及急既即机鸡积记级极计挤己季寄纪基激吉脊际汲肌嫉姬绩缉饥迹棘蓟技冀辑伎祭剂悸济籍寂忌妓继集击圾箕讥畸稽疾墼洎鲚屐齑戟鲫嵇矶稷戢虮诘笈暨笄剞叽蒺跻嵴掎跽霁唧畿瘠玑羁偈芨佶赍楫髻咭蕺觊麂骥殛岌亟犄乩芰哜丮亼偮僟兾刉刏剤劑勣卽叝喞嗘嘰嚌坖垍塉墍妀姞尐居峜嵆嶯幾廭彐彑彶徛忣惎愱憿懻揤撃撠擊擠攲敧旡旣暩曁枅梞楖極槉樭機橶檕檝檵櫅櫭毄泲洁済湒漃漈潗濈濟瀱焏犱狤璣璾痵癠癪皍磯禝禨秸稘稩穄穊積穖穧筓箿簊糭紀紒級結継緝縘績繫繼结罽羇羈耤耭膌臮艥茍葪蔇蕀薊藉蘎蘮蘻虀蝍螏蟣裚襀襋覉覊覬觙觭計訐記誋諅譏譤讦谻賫賷趌跡踖踦蹐蹟躋躤躸輯轚郆鄿銈銡錤鍓鏶鐖鑇鑙際隮雞雦雧霵霽鞊鞿韲飢饑驥魝魢鯚鯽鰶鰿鱀鱭鱾鳮鵋鶏鶺鷄鷑鸄鹡齌齎齏家加假价架甲佳嘉驾嫁枷荚颊稼铗葭迦戛浃镓痂恝岬跏胛笳珈瘕郏袈蛱傢價叚唊圿埉夾婽宊幏徦忦戞扴抸拁拮挟斚斝梜椵榎榢槚檟毠泇浹犌猳玾糘耞脥腵莢蛺袷裌豭貑跲郟鉫鋏鎵頬頰颉駕駱骱鴶鵊麚见件减尖间键贱肩兼建检箭煎简剪歼监坚健艰荐剑溅涧鉴践捡柬笺俭碱硷拣舰缄饯翦鞯戋谏牮枧腱趼缣搛戬毽鲣笕谫楗囝蹇裥踺睑謇鹣蒹僭锏湔俴倹傔儉冿剣剱劍劎劒劔囏堅堿墹姦姧寋帴幵弿彅徤惤戩挸揀揃揵撿擶旔暕栫梘検椾榗樫橌橺檢櫼殱殲減湕澗濺瀐瀳瀸瀽熞熸牋犍猏珔瑊瑐監睷瞯瞼碊磵礀礆礛筧箋篯簡籛糋絸緘縑繝繭聻臶艦艱菺葌葏葥蔪蕑蕳薦藆虃蠒袸襇襉襺見覸詃諓諫謭譼譾豣賎賤趝踐轞釼鋻錽鍳鍵鏩鐗鐧鑑鑒鑬鑯鑳間鞬韀韉餞餰馢騫骞鬋鰎鰹鳒鵳鹸鹹鹻鹼麉将讲江奖降浆僵姜酱蒋疆匠桨豇礓缰犟耩绛茳糨洚匞壃夅奨奬將嵹弜弶彊摪摾杢槳橿櫤殭滰漿獎畕畺疅糡絳繮翞膙葁蔣薑螀螿袶講謽醤醬韁顜鱂鳉叫脚交教较觉焦胶娇绞搅骄狡矫郊嚼蕉轿窖椒礁饺铰酵徼艽僬蛟敫跤姣皎茭鹪噍醮佼鲛挢僥儌勪呌嘂嘦嬌嬓孂峧嶕嶣恔憍憢挍捁撟撹攪敎敽敿斠晈暞曒櫵湬滘漖潐灚烄焳煍燋獥珓璬皦皭矯穚窌笅筊簥糾絞纐纠腳膲臫芁茮藠蟜蟭覐覚覺訆譑譥賋趭踋較轇轎釂鉸鐎餃驕骹鮫鱎鵤鷦鷮纟节街借皆截解界届姐戒阶劫竭疥桔杰诫睫桀喈羯蚧嗟鲒婕碣孑疖丯偼傑刦刧刼劼卪堦堺媎媘媫嫅屆岊岕崨嵥嶻巀幯庎徣悈掲擑擮擳昅桝椄楐楬楶榤檞毑湝滐潔煯犗玠琾畍疌痎癤砎礍稭節絜繲脻莭菨蓵蛣蛶蜐蝔蠘蠞蠽衱衸袓袺褯觧詰誡誱踕迼鉣鍻階鞂飷魪鮚鶛进近今紧金斤尽劲禁浸锦晋筋津谨巾襟烬靳廑瑾馑槿衿堇荩噤缙卺妗赆觐伒侭僅僸儘兓凚劤勁厪嚍埐堻墐壗嫤嬧寖嶜巹惍慬搢晉枃歏殣浕溍漌濅濜煡燼珒琎琻瑨璡璶盡砛祲紟緊縉荕菫蓳藎覲觔謹賮贐進釒錦钅饉鹶黅齽竟静井惊经镜京敬精景警竞境径荆晶鲸粳兢茎睛痉靖肼獍阱腈弪刭憬婧胫菁儆旌迳泾亰仱俓倞傹凈剄坓坕妌婙婛宑巠幜弳徑憼擏旍暻曔桱梷橸殑汫汬浄涇燝猄璄璟璥痙秔稉穽竧竫競竸経經聙脛荊莖葝蟼誩踁逕鏡陉陘靜頚驚鯨鵛鶁麖麠鼱窘炯扃迥侰僒冋冏囧坰埛宭扄泂浻澃烱煚煛熲燛絅綗蘏蘔褧逈顈颎駉駫就九酒旧久揪救舅究韭厩臼玖灸疚赳鹫僦柩桕鬏鸠阄啾丩乆乣倃勼匓匛匶奺廄廏廐慦捄揂揫摎朻杦柾樛殧汣牞畂糺紤舊舏萛镹韮鬮鳩鷲麔齨欍举巨局具距锯剧聚菊矩沮拒惧鞠狙驹据俱咀疽踞炬倨醵裾屦犋窭飓锔椐苣琚掬榘龃莒雎遽橘踽榉鞫钜讵侷倶冣凥劇勮匊圧坥埧埾壉姖娵婅婮寠屨岠崌巈弆怇愳懅懼拠挙挶據擧昛梮椇椈檋櫸欅歫毩毱泦洰涺淗湨澽焗爠犑狊痀眗砠秬窶筥簴粔粷罝耟聥腒臄舉艍蒟蓻蘜虡蚷蜛襷詎諊豦貗趉趜跔跙跼踘蹫躆躹輂邭郹鉅鋦鋸鐻閰陱颶駏駒駶驧鮔鴡鵙鵴鶋鶪鼰鼳齟卷倦鹃捐娟眷绢鄄锩蠲镌狷桊涓隽劵勌勬呟埍奆姢帣弮悁慻捲梋淃焆獧瓹睊睠絭絹罥羂脧臇菤蔨裐鋗錈鎸鐫雋鞙飬餋鵑决绝爵掘诀撅倔抉攫桷橛劂爝矍镢獗珏崛蕨噘谲孓厥亅傕刔妜孒屩屫嶥彏憠憰戄挗捔撧斍橜欔欮殌氒決泬潏灍熦爑爴玃玦玨瑴璚疦瘚矡砄絕絶芵蕝虳蚗蟨蟩觼訣譎貜蹷躩逫鈌鐍鐝钁镼駃鶌鷢龣军君均菌峻竣骏钧郡筠麇皲捃儁呁埈寯懏攈晙桾汮焌燇珺畯皸皹碅箘箟莙葰蚐蜠袀覠軍鈞銁銞鍕陖餕馂駿鮶鲪鵔鵕鵘麏麕攟矝巻亽燞"},
	        new string[]{"K","咔剀剴欬匼峇堪媿窾欿謉侉搕咖咳扛亢伉抗犺阬可嘅枯苦栝括哙扩擴傀匮匱瞆蘬錕锟彉彍矌錁锞頦颏阚吭妔忼炕邟薧嗑渇渴苛蚵袔姱楛殻獪稞喟嘳巜爌溃婫捆昆梡梱礊萿睽瞉揩槛檻楷嵑鍇锴粇怐崫狂騤骙卡喀胩佧垰衉裃鉲开凯慨垲锎铠忾恺蒈凱勓塏奒愷愾暟溘炌炏烗輆鎎鎧鐦開闓闿颽看砍刊坎勘龛戡侃瞰莰偘冚埳塪墈崁嵁惂栞歁矙磡竷衎輡轁轗顑龕糠康慷钪闶匟囥坑嫝嵻摃槺漮砊穅躿鈧鏮閌鱇靠考烤拷栲犒尻铐丂嵪攷洘焅銬髛鮳鯌鲓克棵科颗刻课客壳柯磕坷恪岢蝌缂轲窠钶氪瞌珂髁疴骒剋勀勊堁娔尅嵙嶱愙揢敤榼樖炣牁犐痾碦礚緙翗胢萪薖課趷軻醘鈳顆騍肯啃恳垦裉墾懇掯硍肎肻褃豤貇錹铿劥挳牼硁硜硻誙銵鍞鏗空孔控恐倥崆箜埪悾涳硿躻錓鞚鵼口扣抠寇蔻芤眍筘叩冦剾劶宼彄挎摳敂滱瞘窛竘簆蔲釦鷇哭库裤窟酷刳骷喾堀绔俈嚳圐庫扝桍狜瘔矻秙絝袴褲跍跨郀鮬垮夸胯咵晇舿誇銙顝骻快块筷侩蒯郐狯脍儈凷圦塊墤廥擓旝糩膾蒉蕢鄶鬠魁鱠鲙宽款髋寛寬欵歀窽鑧髖矿筐框况旷匡眶诳邝纩夼诓圹贶哐儣劻匩壙岲恇懬懭抂昿曠況洭狅眖砿礦穬筺絋絖纊誆誑貺軖軠軦軭邼鄺鉱鋛鑛鵟黋亏愧奎窥葵馈盔岿愦揆跬聩篑喹逵暌悝馗蝰夔刲卼嬇尯巋巙憒戣晆楏楑樻櫆欳潰煃磈窺簣籄聧聭聵藈蘷虁虧蹞躨鄈鍨鍷鐀鑎闚頄頍頯餽饋困坤鲲髡琨醌阃悃堃堒壸壼尡崐崑晜涃潉焜熴猑瑻睏硱祵稇稛綑臗菎蜫裈裍裩褌閫閸騉髠髨鯤鵾鶤鹍阔廓蛞噋懖拡桰濶筈葀闊霩鞟鞹韕頢髺垃哢煀"},
	        new string[]{"L","熝鏕蛖虑鴓臱膔冫燷皪扐荖勑厘裣襝唠嘮帘踜樆离莉誺邌離驪骊鵣櫖硫蔍逯隶狫氀樚玀愍敃錀豊俛呒嘸宓璷鮥鵅纶沦淪綸蠃嚂歛蘫狼貉輅辂芦朚芒茫沬湣緍緡缗脔艻苙蜡骆滥濫稴錬鍊嵺竻肋靓靚剹穋繆缪篓簍蒌蔞砢卵拉啦辣腊喇蓝落瘌邋砬剌旯儠嚹揦揧搚擸攋柆楋櫴溂爉瓎癩磖翋臘菈藞蝋蝲蠟辢鑞镴鞡鬎鯻鱲癞来赖莱濑赉崃涞铼籁徕睐來俫倈厲唻婡崍庲徠懶梾棶淶瀨瀬猍琜睞筙箂籟萊藾襰賚賴逨郲釐錸頼顂騋鯠鶆麳黧兰烂拦篮懒栏揽缆阑谰婪澜览榄岚褴镧斓罱漤儖厱啉囒壈壏嬾孄孏嵐幱廩廪惏懔懢擥攔攬斕欄欖欗浨涟湅漣瀾灆灠灡炼煉燗燣爁爛爤爦璼瓓礷籃籣糷繿纜葻藍蘭襕襤襴襽覧覽譋讕躝醂鑭钄闌韊顲浪廊郎朗榔琅稂螂莨啷锒阆蒗俍勆哴唥埌塱嫏崀悢朖朤桹樃樠欴烺瑯硠筤脼艆蓈蓢蜋誏踉躴郒郞鋃鎯閬駺老捞牢劳烙涝姥酪络佬潦耢铹醪铑栳崂痨僗僚労勞咾哰嗠嫪嶗恅憥憦撈撩朥橑橯浶澇獠珯癆硓磱窂簩粩絡耮蓼蛯蟧軂轑銠鐒顟髝鮱了乐勒鳓仂叻泐嘞忇氻玏砳簕阞韷餎饹鰳类累泪雷垒擂蕾镭儡磊缧诔耒酹羸嫘檑傫儽卢厽咧塁壘壨攂樏櫐櫑欙洡涙淚漯灅瓃畾癗盧矋磥礌礧礨禷絫縲纇纍纝罍脷蔂蕌藟蘱蘲蘽虆蠝誄讄轠銇錑鐳鑘鑸靁頛頪類颣鱩鸓鼺里力立李例哩理利梨礼历丽吏砾漓傈荔俐痢狸粒沥栗璃鲤厉励犁黎篱郦鹂笠坜苈鳢缡跞蜊锂澧粝蓠枥蠡呖砺嫠篥疠疬猁藜溧鲡戾栎唳醴轹詈罹逦俪喱雳莅俚蛎娌儮儷凓刕列剓剺劙勵厤厯叓唎嚟嚦囄囇塛壢娳婯孋孷屴岦峛峲巁廲悡悧悷慄捩搮擽攊攦攭斄暦曆曞朸栃栛栵梸棃棙檪櫔櫟櫪欐欚歴歷氂沴沵浬涖濼濿瀝灕爄爏犂犛犡珕珞琍瑮瓅瓈瓑瓥癘癧盠盭睝矖砅磿礪礫礰禮禲秝穲竰筣籬粴糎糲綟縭纚艃茘荲菞蒚蒞蔾藶蘺蚸蛠蜧蟍蟸蠇蠣蠫裏裡褵觻謧讈貍赲轢轣邐酈醨鉝鋫鋰錅鏫鑗隷隸霾靂靋鬁鯉鯏鯬鱧鱱鱳鱺鳨鴗鵹鷅鸝麗麜俩倆连联练莲恋脸链敛怜廉镰蠊琏殓蔹鲢奁潋臁裢濂楝亷令僆劆匲匳嗹噒堜奩娈媡嫾嬚孌慩憐戀挛摙攣斂梿槤櫣殮浰溓澰濓瀲熑燫瑓璉瞵磏簾籢籨練縺纞羷翴聨聫聮聯膦臉苓萰蓮薕蘝蘞螊褳覝謰譧蹥連鄻鎌鏈鐮零鬑鰊鰱两亮辆凉粮梁量良晾谅粱墚椋魉両兩唡啢喨掚樑涼湸煷簗糧綡緉蜽裲諒輌輛輬辌鍄魎料聊撂疗廖燎辽寥镣钌尥寮缭鹩嘹僇嫽尞尦屪嶚嶛廫憀憭敹暸漻炓爎爒璙療瞭窷竂簝繚膋膫蟉蟟豂賿蹘蹽遼鄝釕鏐鐐镠镽飂飉髎鷯裂猎劣烈埒鬣趔躐冽洌劦劽哷埓姴峢巤挒挘毟浖烮煭犣猟獵睙聗脟茢蛚迾颲鬛鮤鴷林临淋邻磷鳞赁吝拎琳霖凛遴嶙蔺粼麟躏辚檩亃僯凜厸壣崊恡悋懍撛斴晽暽橉檁潾澟瀶焛燐獜璘甐疄痳碄箖粦繗翷臨菻藺賃蹸躙躪轔轥鄰鏻閵隣驎魿鱗麐另领铃玲灵岭龄凌陵菱伶羚棱翎蛉绫瓴酃呤泠棂柃鲮聆囹倰冷刢坽夌姈婈孁岺崚嶺彾掕昤朎櫺欞淩澪瀮炩燯爧狑琌皊砱祾秢竛笭紷綾舲蓤蔆蕶蘦衑袊裬詅跉軨輘醽鈴錂閝阾霊霗霛霝靇靈領駖鯪鴒鸰鹷麢齡齢龗六流留刘柳溜瘤榴琉馏碌陆绺锍鎏镏浏骝旒鹨熘遛偻僂劉嚠塯媹嬼嵧廇懰抡旈栁桞桺橊橮沠泖澑瀏熮珋瑠瑬璢畄畱疁癅磂磟綹罶羀翏膢蒥蓅藰裗蹓鉚鋶鎦鐂铆陸雡霤飀飅飗餾駠駵騮驑鬸鰡鶹鷚鹠麍囖龙拢笼聋隆垄咙窿陇垅胧珑茏泷栊癃砻儱嚨壟壠嶐巃巄徿攏昽曨朧梇槞櫳湰漋瀧爖瓏眬矓硦礱礲竉竜篭籠聾蕯蘢蠪蠬衖襱豅贚躘鏧鑨隴霳驡鸗龍龒龓楼搂漏陋露娄蝼镂耧髅喽瘘嵝嘍塿婁屚嶁廔慺摟樓溇漊熡甊瘺瘻瞜耬艛螻謱軁遱鏤鞻髏路录鹿炉鲁卤颅庐掳绿虏赂戮潞禄麓鲈栌渌泸轳氇簏橹垆胪噜镥辘漉撸璐鸬鹭舻侓勎勠嚕嚧圥坴塶塷壚娽峍廘廬彔挔捋捛摝擄擼攎枦椂樐櫓櫨氌淕淥滤滷澛濾瀂瀘爐獹玈琭瓐甪盝睩矑硉硵磠祿稑箓簬簵簶籙籚粶緑纑罏膟臚舮艣艪艫菉蓾蕗蘆虂虜螰蠦觮賂趢踛蹗轆轤醁鈩錄録錴鏀鏴鐪鑥鑪顱騄騼髗魯魲鯥鱸鴼鵦鵱鷺鸕鹵黸乱滦峦孪栾銮鸾亂圝圞奱孿巒曫欒灓灤癴癵羉臠薍虊覶釠鑾鵉鸞略掠锊剠圙寽率畧稤鋝鋢论轮伦仑囵侖倫圇埨婨崘崙惀掄棆溣碖磮稐耣腀菕蜦論踚輪陯鯩罗锣裸骡箩螺萝洛逻荦雒倮椤脶瘰摞泺镙猡儸剆啰囉峈攞曪欏犖癳笿籮纙羅腡臝蓏蘿覼躶邏鏍鑼頱饠騾驘鸁铝驴旅屡吕律氯缕侣履膂榈闾褛稆侶儢勴卛呂垏屢嵂慮曥梠櫚焒爈祣穞穭箻絽綠縷繂膐葎藘褸郘鋁鑢閭馿驢鷜"},
	        new string[]{"M","襔秘泌呣吗袹妈马嘛麻骂抹码玛蚂摩唛蟆犸嬷杩么傌嗎嘜媽嫲嬤尛榪溤犘獁瑪痲睰碼礣祃禡罵蓦蔴螞蟇貊遤鎷閁靡馬駡驀鬕鰢鷌麼麽买卖迈埋麦脉劢荬佅勱咪嘪売脈蕒薶衇買賣邁霡霢鷶麥满慢瞒漫蛮蔓曼馒谩幔鳗墁螨镘颟鞔缦熳僈姏嫚屘悗慲摱槾満滿澫澷獌睌瞞矕絻縵蔄蘰蟎蠻謾鄤鏋鏝顢饅鬗鬘鰻忙盲莽氓硭邙蟒漭厖吂哤壾娏尨庬恾朦杗杧汒浝牤牻狵甿痝瞢笀茻莾蘉蠎釯鋩铓駹鸏鹲毛冒帽猫矛卯貌茂贸锚茅耄茆瑁蝥髦懋昴牦瞀峁袤蟊旄侔冃冇冐堥夘媢嵍愗戼描暓枆楙毷渵牟獏皃眊笷緢罞芼蓩蛑蝐覒貓貿軞鄚鄮酕鉾錨霿髳鶜嚒嚜嚰孭庅濹癦没每煤镁美酶妹枚霉玫眉梅寐昧媒糜媚谜沫嵋猸袂湄浼鹛莓魅镅楣凂堳塺墨媄媺嬍嵄徾抺挴攗某栂楳槑櫗毎沒渼湈煝燘珻瑂痗眛睂睸矀祙禖篃脄脢腜苺葿蘪蝞跊躾郿鋂鎂鎇韎鬽鶥黣黴门们闷懑扪钔焖們怋悶懣捫暪椚燜玟璊穈菛虋鍆門閅猛梦蒙锰孟盟檬萌礞蜢勐懵甍蠓虻艋艨儚冡夢夣嫇幪懜懞掹擝明曚橗氋溕濛獴瓾瞑矇矒莔蕄蝱鄳鄸錳霥靀顭饛鯍鯭黽黾鼆踎米密迷眯蜜觅弥幂醚蘼縻汨麋祢猕弭谧芈脒敉嘧糸侎冞冪劘塓孊宻峚幎幦彌戂擟擵攠榓樒檷櫁洣淧渳溟滵漞濔濗瀰灖熐爢獼眫眽瞇瞴祕禰簚粎羃羋葞蒾蓂蔝蔤藌袮覓覔覛詸謎謐醾醿釄銤鑖镾鸍麊麛鼏面棉免绵眠缅勉冕娩腼湎眄沔渑丏偭冥勔喕婂媔嬵愐檰櫋汅泯矈矊矏糆綿緜緬芇蝒蠠靣鮸麪麫麵麺秒苗庙妙瞄藐渺眇缈淼喵杪鹋邈媌嫹庿廟玅竗篎緲鱙鶓灭蔑咩篾蠛吀哶幭懱搣滅瀎眜薎衊覕鱴民抿敏闽皿悯珉闵苠鳘岷僶冧冺刡勄呡垊姄崏慜憫捪敯旻旼暋渂潣琘琝瑉痻盿碈笢笽簢罠賯鈱錉鍲閔閩鰵鴖名命鸣铭螟暝茗酩佲凕姳慏掵朙榠洺猽眀眳覭詺鄍銘鳴谬謬摸磨末膜莫默魔模摹漠陌蘑寞秣瘼殁镆嫫谟貘茉馍耱劰嗼嚤嚩圽塻妺嫼帓帞怽懡昩暯枺橅歾歿爅皌眿瞐瞙砞礳粖糢絈縸纆莈藦蛨蟔謨謩貃銆鏌靺饃饝髍魩魹麿黙谋眸鍪哞劺恈桙洠蟱謀鴾麰毪氁闑杣斏洜耂藔躼烕謢募幕毣萺坶艒鞪木母亩目墓牧穆暮牡拇慕睦姆钼沐仫苜凩娒峔幙慔楘樢炑牳狇畆畒畝畞畮砪胟莯蚞踇鉧鉬雮霂乸拏"},
	        new string[]{"N","乃懝拗噢埿蘖矃柅迡扭杻鈕钮柠淖鳥鸟唸埝拈貀嬭郍妞鎒鐞湼屰袦弄臡呐氼籋乜那拿哪纳钠娜南衲捺镎肭內内吶呶嗱妠抐淰笝納蒘蒳詉誽豽蹃軜鈉鎿雫靹魶耐奶奈氖萘艿柰鼐倷妳孻廼掜摨渿熋疓能腉螚褦迺釢錼难男赧囡蝻楠喃腩侽娚婻戁抩揇暔枏湳煵畘莮萳諵遖難囊馕曩囔攮乪儾哝噥嚢憹擃欜灢蠰譨饢鬞齉闹脑恼挠孬铙瑙垴蛲猱硇匘堖夒嫐峱嶩巎怓悩惱撓檂獶獿碯脳腦鐃閙鬧呢讷眲訥馁氝腇餒鮾鯘焾嫩恁嫰銰你泥拟腻逆溺倪尼匿妮霓铌昵坭猊伲怩鲵睨旎伱儞堄婗嫟嬺孴屔惄愵抳擬晲暱棿淣濘狔痆眤秜籾縌胒膩苨薿蚭蜺觬貎跜輗郳鈮鉨鑈隬馜鯢麑齯年念捻撵碾蔫廿黏辇鲇鲶卄哖姩撚攆秊秥簐艌躎輦鮎鯰鵇尿袅茑脲嬲嫋嬝蔦裊褭捏镍聂孽涅镊啮陧嗫臬蹑颞噛嚙囁囓圼孼嵲嶭巕帇惗揑敜枿槷櫱篞糱糵聶肀臲苶菍蠥讘踂踗踙躡鉩錜鎳鑷钀隉顳齧您囜拰脌拧凝宁狞泞佞甯咛聍侫儜嚀嬣寍寕寗寜寧擰橣檸澝獰聹薴鑏鬡鸋牛纽狃忸汼沑炄牜紐莥靵浓农脓侬儂挊挵欁濃癑禯秾穠繷膿蕽襛農辳醲齈耨啂搙槈檽羺譳怒努奴孥胬驽弩伖伮傉砮笯駑暖奻渜煗餪虐疟硸黁燶挪诺懦糯喏傩锘搦儺堧懧掿搻梛榒橠稬穤糑糥諾逽鍩女衄钕恧朒籹衂釹瘧倿嶿"},
	        new string[]{"O","哦蓲毆鏂筽偶呕欧藕鸥沤殴怄瓯讴耦吘嘔塸慪櫙歐漚熰甌腢膒蕅藲謳鴎鷗齵妑帊"},
	        new string[]{"P","耙钯哱杷湃皅跁鈀排派猈搫柈螌褩膀嗙嫎彭徬搒旁螃騯髈刨炮瀑嚗砲蚫袍裒埤柸棑椑菩葡諀錍堋平抨熢錋辟芘仳媲崥庀怶旇枇殍瞥笓箄紕纰翍肶脾腗螕陴拚甂僄剽嫖徱漂篻撆撇蠙頻频屏拼琕魄怕拍擗檘泼潑潘蒲蔢蚾跑埔僕婄撲擈溥獛箁陠鯆揊汖醱畨蟠袢彷雱鰟鳑裴喷噴歕濆盼葐捧莑蘕逄鵬鹏炰衃脯抙捊掊纀莆蜅錇锫哌奤縏舗坯頮鮍鲏蹒蹣厐庞龐萠冖彯蜱帕爬趴啪琶筢葩掱潖舥袙牌迫徘俳蒎犤猅簰輫鎃盘判攀畔叛磐胖襻泮爿冸媻幋槃沜溿瀊炍片牉皤盤眅蒰詊踫鄱鋬鎜鑻鞶頖鵥耪乓滂沗篣胮膖覫霶龎抛泡咆狍匏庖疱脬垉奅拋爮犥皰礟礮萢褜謈軳鞄麅麭陪配赔呸胚佩培沛旆帔醅霈辔伂俖姵嶏怌抷斾昢毰浿淠珮肧蓜賠轡阫馷駍盆湓呠瓫翸碰棚砰蓬朋烹硼膨澎篷怦蟛嘭倗剻匉塜塳弸恲憉掽梈椖椪槰樥泙淎淜漰皏硑磞稝竼纄胓芃苹荓蟚軯軿輣輧鑝閛韸韼髼鬅鬔批皮披匹劈屁僻疲痞霹琵毗啤譬砒貔丕圮癖郫甓睥鼙邳铍罴噼蚍伓伾噽嚭壀嫓岯憵扑朇毘毞渒潎澼炋焷狉狓疈睤磇礔礕秛秠篺簲羆耚脴膍苉苤蚽螷蠯豼豾鈹鉟銔銢錃闢阰隦頗颇駓髬魾鴄鵧鷿篇骗偏翩犏骈胼蹁谝囨媥楄楩腁覑諚諞貵賆駢騈騗騙骿魸票飘瓢朴螵瞟缥嘌勡慓旚皫縹翲薸醥闝顠飃飄魒氕丿嫳暼鐅品贫聘嫔榀姘牝颦嚬娉嬪朩玭矉礗穦薲蘋貧顰馪驞凭瓶评乒萍坪鲆枰俜凴呯塀娦屛岼帡帲幈慿憑洴涄焩玶甁甹砯竮箳簈缾聠艵蓱蚲蛢評郱頩鮃破坡婆粕笸钋攴叵珀钷嘙尀岥岶廹敀櫇洦溌烞砶蒪酦釙鉕鏺駊剖咅抔犃铺谱仆圃浦普镨噗匍濮氆蹼璞镤圑圤墣巬暜樸檏潽炇烳痡瞨穙舖菐蒱諩譜贌酺鋪鏷鐠曝七鍂"},
	        new string[]{"Q","鎆奇鉗钳墽磽嬱朁淺瑲篬鼜鉆趫呛嗆抢搶摤槍鎗祇芪荎耝詘诎戧朐胊輇辁荃醛荠薺趋趣趨且戚趥冄矵晵礘魥汱釓汽乾髂蛩区區軥鴝鸜鸲峠權炔朹祈嵌欦鈐钤愒洽隺翵礐搳孉蠸搉期其齐帺懠撽棋淁璂瞿萁蕲蘄蟿褀趞跂踑騎骑鬾齊茄挈浅偂前堑塹銭錢鐱钱鰜鳽鶼黚强勥強蔃峤湫乔侨僑却卻喬嫶嶠橋萩蕎蹻釥倢契洯雃婜斳菳鋟锓儬檠濪箐蜻靑青靘鶄蝤趄娶忂渠籧蘧蛆螶郥圈圏埢惓棬腃蜷踡韏匷啳埆屈繑蠼誳闋闕阕阙鞒鞽呿豈凵訄愘硞頎颀摼殸宆椌穹羫腔缺頃顷泣槏蛪敺起气器妻欺漆启柒岂砌弃祁凄企乞歧栖畦脐崎迄沏讫旗祺骐屺岐蹊桤憩萋芑汔鳍槭嘁蛴綦亓琪麒琦蜞圻杞葺碛淇耆绮亝倛傶僛切剘勤呇咠唘唭啓啔啟噐埼夡娸婍岓嵜忯恓悽愭慼慽憇捿掑斉斊旂暣朞栔桼棄棊棨棲榿檱櫀欫気氣淒湆湇濝炁猉玂玘甈疧盀盵碁碕碶磜磧磩禥竒簯簱籏粸紪綥綨綮綺緀緕纃缼罊肵臍艩芞藄蚑蚔蚚蜝螧蠐裿褄訖諆諬諿軝迉邔郪釮鏚锜闙霋騏騹鬐鬿魌魕鮨鯕鰭鲯鵸鶀鶈麡恰掐葜佉冾圶帢拤殎硈跒酠鞐千牵签欠铅钎迁谴谦潜歉扦遣黔仟岍褰箝掮搴倩慊悭愆虔芡缱佥芊阡肷茜椠僉儙刋圱圲墘壍奷媊孯岒嵰忴悓慳扲拑拪掔撁攐攑攓杄棈榩槧橬檶櫏歬汘汧潛濳灊牽皘竏箞篏篟簽籖籤粁綪縴繾羟羥羬臤茾蒨蔳蕁蚈蚙蜸諐謙譴谸軡輤遷釺鈆鉛鏲鑓韆顅騚騝鬜鬝鰬鵮鹐枪墙羌蔷蜣跄戗襁戕炝镪锖锵樯嫱唴啌嗴墏墻嬙嶈庆廧慶斨檣溬漒熗牄牆猐獇玱琷繈繦羗羻艢薔蘠親謒跫蹌蹡錆鏘鏹桥瞧敲巧翘锹鞘撬悄俏窍雀峭橇樵荞跷硗憔谯愀缲诮劁僺嘺塙墝墧帩幧槗橾殼毃燆犞癄硚碻礄竅翹荍藮誚譙趬踃踍蹺躈郻鄡鄥鍫鍬鐈陗鞩韒頝顦髚髜怯窃郄惬锲妾箧匧厒悏愜朅癿穕竊笡篋籡緁聺苆藒踥鍥鐑鯜亲琴侵擒寝秦芹沁禽钦吣衾芩溱嗪螓噙揿檎吢唚坅埁媇嫀寑寢寴嵚嶔庈懃懄抋捦搇撳昑梫澿瀙珡琹瘽笉綅耹菣菦藽螼蠄誛赺赾鈙雂靲顉駸骎鮼鳹请轻清情晴氢倾擎卿氰圊謦苘黥罄鲭磬傾凊勍啨埥夝寈庼廎掅暒棾樈檾櫦氫淸漀甠碃請軽輕郬鑋鯖穷琼邛茕銎筇儝卭惸憌桏橩焪焭煢熍瓊瓗睘窮竆笻藑藭蛬赹求球秋丘泅邱囚酋楸蚯裘糗巯逑俅虬赇鳅犰鼽遒丠厹叴唒坵媝崷巰恘扏搝梂殏毬汓浗渞湭煪玌璆皳盚秌穐篍紌絿緧肍莍蘒虯蛷蝵蟗蠤觓觩訅賕逎釚釻銶鞦鞧鮂鯄鰌鰍鰽鵭鶖鹙龝去取曲驱躯龋祛蕖磲劬阒麴癯衢黢璩氍觑蛐岖伹佢刞匤厺岴嶇憈戵抾斪欋浀淭灈璖竬筁粬紶絇翑胠臞菃葋蝺蟝蠷衐袪覰覷覻詓躣軀迲鑺閴闃阹駆駈驅髷魼鰸鱋麮麯麹鼁鼩齲全权劝拳犬泉券颧痊铨筌绻诠畎鬈悛佺勧勸姾婘峑巏恮搼椦楾権洤湶烇牶牷犈瑔甽硂絟綣縓葲虇觠詮跧銓鐉闎顴駩騡鰁鳈齤确瘸鹊榷悫崅愨慤燩皵碏確礭舃蒛鵲群裙逡囷夋峮帬羣裠輑呥鈫奍"},
	        new string[]{"R","糹肜婼冉腍銳鋭锐杒渪濡臑輭陾髶邚釰蕊蕋坈任挐箬篛柟搑瀼橈腝蝚蟯媆枘涊蹨攘鬤嬬擩獳褥愞耎染燃然髯苒蚺嘫姌媣橪珃繎肰舑蒅蚦衻袇袡髥让嚷瓤壤穰禳壌孃懹爙獽穣譲讓躟饶绕扰荛桡娆嬈擾犪繞襓遶隢饒热若惹偌渃熱人忍认刃仁韧妊纫壬饪轫仞荏衽稔仭刄姙屻忈忎扨朲栠栣梕棯牣秂秹紉紝絍綛纴肕芢荵袵訒認讱躵軔鈓銋靭靱韌飪餁魜鵀日鈤馹驲容绒融溶熔荣戎蓉冗茸榕狨嵘蝾傇傛媶嫆嬫宂峵嵤嶸搈曧栄榮榵毧氄瀜烿爃瑢穁穃絨縙縟缛羢茙螎蠑褣鎔镕駥肉揉柔糅蹂鞣媃宍楺渘煣瑈瓇禸粈腬葇輮鍒韖騥鰇鶔如入汝儒茹乳辱蠕孺蓐襦铷嚅薷颥溽洳侞偄嗕媷帤扖曘杁桇燸筎繻肗蕠袽鄏醹銣顬鱬鳰鴑鴽软阮朊壖撋瑌瓀碝礝緛蝡軟瑞睿芮蚋蕤叡壡惢桵橤汭甤緌繠蘂蘃蜹润闰橍潤閏閠弱叒嵶楉焫爇蒻鄀鰙鰯鶸仨祍"},
	        new string[]{"S","騃杓偲揌嘇穇篸鏒鯵鰺鲹慅懆鄵鐰赦僧剎墠嬗摌脠苫蟺裳尙尚粆紹綤绍喢沈瀋胂盛晟嵊匙蛇飾餝饰埫跾敊淑鸀漺伺司枱栜薮藪衰熣粋縗繀缞脺澨石伔抌膻贍赡受勺芍諟適崼涉渉樞襡杸隋柹市舌丨敆鉮懳眭睢蒐姼菽拾氏垧橚睃身狻圣适祱灑蝕釃鑠铄颯飒涁渗滲滝摅攄挼烁爍鱦摵罙澠紗纱谂繩绳厶丆娞浽滠灄摂摄攝諗姍姗审宷審疋俟梩洓示炶煔幓捎睄唼帹声胜拴獡葚囸摉頌颂綏绥苼撒洒萨挲卅脎摋桬櫒殺潵薩訯鏾钑隡靸馺塞腮鳃思赛噻僿嗮嘥愢毢毸簑簺賽顋鰓乷三散伞叁馓糁毵俕傘厁弎橵毶毿犙糂糝糣糤繖蔘閐饊鬖彡氵桑丧嗓颡磉搡喪桒槡褬鎟顙扫嫂搔骚梢埽鳋臊缫瘙哨掃掻氉溞矂縿繅颾騒騷髞鰠鱢色涩瑟啬铯穑嗇愬懎擌歮歰渋溹澀澁濇濏瀒璱瘷穡穯繬譅轖銫鎍鎩鏼铩雭飋森槮襂鬙杀沙啥傻砂莎厦煞杉鲨霎痧裟歃倽儍唦廈挱榝樧猀硰箑繺翜翣萐蔱賒賖赊閯閷魦鯊鯋晒筛酾曬篩簁簛籭術山闪衫善扇删煽珊膳陕汕擅缮蟮芟跚鄯潸鳝骟疝讪钐舢埏傓僐刪剼圸墡搧敾晱曑椫樿檆澘灗熌狦痁睒磰笘繕羴羶葠覢訕謆譱赸軕邖釤銏鐥閃閊陝饍騸鯅鱓鱔鱣上伤商赏晌墒熵觞绱殇丄傷恦慯殤滳漡緔蔏螪觴謪賞鑜鬺少烧稍邵韶劭潲艄蛸筲卲娋弰搜旓柖溲焼燒玿莦萔蕱袑輎颵髾鮹社射设舍慑奢厍畲猞麝佘厙弽慴懾捨檨欇涻舎蔎蛥蠂設輋韘騇伸深婶神甚肾申绅呻砷什娠慎莘诜矧椹渖蜃哂侁侺兟堔妽嬸屾峷弞愼扟昚曋柛椮榊氠燊珅甡甧瘆瘮眒眘瞫矤祳穼籶籸紳罧脤腎蓡薓裑覾訠訷詵讅谉邥鋠頣駪魫鯓鰰鵢省剩生升甥牲眚笙偗剰勝呏墭憴斘昇晠曻枡榺橳殅泩渻湦焺狌珄琞竔縄聖聲蕂譝貹賸鉎鍟阩陞陹鵿鼪是使十时事室师试史式识虱矢屎驶始似士世柿拭誓逝势嗜噬失仕侍释狮食恃蚀视实施湿诗尸豕莳埘铈舐鲥鲺贳轼蓍筮炻谥弑螫丗乨亊佀佦兘冟勢卋呞呩塒奭嬕実宩寔實寺屍峕嵵師弒徥恀揓斯旹昰時枾栻榁榯檡浉湜湤溡溮溼濕烒煶狧獅瑡眂眎眡睗礻祏秲竍笶笹箷篒簭籂絁舓葹蒒蒔蝨褷襫襹視觢試詩諡謚識貰軾辻遈遾邿釈釋鈰鉂鉃鉇鉐鉽銴鍦飠饣駛鮖鯴鰘鰣鰤鳲鳾鶳鸤鼫鼭齛手收首守瘦授兽售熟寿艏狩绶収垨壽夀涭獣獸痩綬膄鏉书树数输梳叔属束术述蜀黍鼠赎孰蔬疏戍竖墅庶薯漱恕枢暑殊抒曙署舒姝秫纾沭毹腧塾殳澍倏倐嗽婌尌尗屬庻怷捒掓數書朮柕樹毺涑潄潻濖瀭焂璹疎癙竪籔糬紓絉綀荗蒁薥虪蠴裋豎贖踈軗輸鄃鉥錰鏣陎鮛鱪鱰鵨鶐鼡忄刷耍唰唆涮誜摔甩帅蟀帥栓闩腨閂双霜爽孀傱塽孇慡樉欆灀礵縔艭鏯雙騻驦骦鷞鸘鹴水谁睡税说帨挩捝氺涗涚稅脽裞說説誰閖顺吮瞬舜橓瞚瞤蕣順鬊硕朔搠妁槊蒴哾嗍欶洬溯矟碩鎙四死丝撕私嘶肆饲嗣巳耜驷兕蛳厮汜锶泗笥咝鸶姒缌祀澌亖価俬儩凘噝娰媤孠廝恖柶楒榹泀泤洍涘瀃燍牭磃禗禠禩竢絲緦罒罳肂蕬蕼虒螄蟖蟴覗貄釲鈻鉰銯鍶鐁颸飔飤飼駟騦鷥鼶螦送松耸宋诵怂讼竦菘淞悚嵩凇崧忪倯娀嵷庺愯慫憽摗枀枩柗梥檧濍硹聳蜙訟誦鎹餸駷鬆艘擞嗾嗖飕叟锼馊瞍螋傁凁叜廀廋捜擻櫢潚獀瘶蓃謏鄋醙鎪颼餿騪素速诉塑俗苏肃粟酥缩僳愫簌觫稣夙嗉谡蔌傃囌埣塐嫊憟梀榡樎樕櫯殐泝溸潥玊珟璛甦穌窣粛縤縮肅膆藗蘇蘓訴謖趚蹜遡遬鋉餗驌骕鯂鱐鷫鹔酸算蒜匴痠祘笇筭岁随碎虽穗遂髓隧祟谇濉邃燧荽亗倠哸夊嬘嵗旞檖歲歳滖澻瀡煫璲瓍睟砕禭穂穟繐繸膸芕荾蓑襚誶譢賥遀鐆鐩隨雖鞖髄孙损笋榫荪飧狲隼孫損搎槂潠猻筍箰蓀蕵薞鎨鶽所锁琐索梭唢桫嗦娑羧傞嗩摍暛溑琑瑣簔莏蜶趖逤鎖鎻鏁髿鮻他贘敒愡"},
	        new string[]{"T","螁体殕土捈梌荼镡兎兔嘽團緂倘儻淌闛涛濤呫塡填捵樘橖趟她徲抬拕拖提沱箈菭蝭鶗鶙僮潼酮檮鯈涂朣橦膧魋芚蓴跿塔沓塌搨橽荅鎉呔骀軚馱駄駘驮赕倓坛壇弾惔撢潭醈偒嵣烫燙焘忑洮燾絩陶渧碮苐蓧蹄蹏題题鬄橝痶蜓调佻倜嬥挑粜糶蜩誂跳跕踢鐡鐵鰨鳎汀濎艼葶勭桐烔燑筒筩絧衕詷投酡鈄塗橐彖囤庉忳腯豚沲柁橢沰詑軃陀頫骰踻铦咍滩灘僣髫烃烴冂焞忐罤僋燤靦盷摊攤脮慝帑湪哣趿覃团氽槫檀潬顃汤湯踼苕笤搷媞忕惿痑褆鉈铊扌儵瑹稌透台謕鋖鷉它踏拓獭挞蹋溻榻遢闼傝咜嚃嚺墖太崉撻榙毾涾澾濌牠獺祂禢褟誻譶蹹躢遝錔闒闥阘鞜鞳态胎苔泰酞汰炱肽跆鲐钛薹邰儓冭囼坮夳嬯孡忲態擡旲檯溙炲珆籉粏臺舦鈦颱鮐谈叹探碳贪痰毯坦炭瘫谭坍袒钽郯锬昙嗿嘆埮墰墵壜婒怹憛憳憻擹暺曇榃歎湠璮癱罈罎舔舕菼藫襢談譚譠貚貪賧醓醰鉭鷤躺堂糖塘唐搪棠膛螳羰醣瑭镗傥饧溏耥铴螗傏劏啺嘡坣戃摥曭榶漟煻爣矘磄禟篖糃糛膅蓎薚蝪赯蹚鄌鎕鎲鏜鐋钂镋隚鞺餳餹饄鶶鼞套掏逃桃讨淘滔绦萄鼗啕饕韬仐匋咷嫍幍弢慆搯梼槄瑫祹絛綯縚縧绹蜪裪討詜謟迯醄鋾鞀鞉鞱韜飸饀駣騊疼腾藤誊滕儯幐漛籐籘縢膯虅螣謄邆霯駦騰驣鰧鼟替剃剔梯锑啼涕嚏惕屉醍鹈绨缇裼逖悌偍厗嗁嚔屜崹悐惖戻挮掦歒殢洟漽瑅瓋稊籊綈緹蕛褅趧趯躰軆逷遆銻鍗騠骵體髰鬀鮷鯷鳀鴺鵜鷈天田添甜恬腆掭阗忝殄畋倎兲吞呑唺娗婖屇悿晪沺淟湉琠瑱璳甛畑畠睓睼碵磌窴胋舚菾覥觍賟酟錪闐靔靝餂鴫鷆鷏黇条迢眺龆祧窕鲦宨岧岹庣恌斢旫晀朓條樤祒窱聎脁芀蓚蓨螩覜趒鋚鎥鞗鰷齠铁贴帖萜餮怗聑蛈貼鉄飻驖听停挺厅亭艇庭廷莛婷梃霆侹厛圢嵉庁廰廳楟榳涏渟烶珽筳綎耓聤聴聼聽脡蝏誔諪邒閮鞓頲颋鼮同通痛铜桶捅统童彤瞳茼仝砼恸佟嗵哃囲峂庝慟憅晍曈樋氃浵炵熥犝狪獞痌眮秱穜粡統綂蓪蚒赨鉖鉵銅餇鮦鲖头偷偸妵敨紏緰蘣鍮頭黈图吐秃突徒凸途屠酴钍菟堍凃唋図圖圗圡堗峹嵞嶀庩廜捸揬汢涋湥潳痜瘏禿筡莵葖蒤迌釷鈯鋵鍎馟駼鵌鵚鵵鶟鷋鷵鼵湍疃抟団圕慱摶檲漙煓猯畽篿糰褖貒鏄鷒鷻腿推退褪颓蜕煺忒侻俀僓娧尵穨脫脱蓷藬蘈蛻蹆蹪隤頹頺頽駾骽屯臀饨暾坉旽朜涒臋豘軘霕飩魨鲀黗托妥驼椭唾鸵柝跎乇坨佗庹鼍箨砣侂咃堶岮撱杔楕槖毤毻汑涶狏砤碢籜紽莌袉袥託讬迱陁飥饦馲駝駞騨驒驝魠鮀鰖鴕鵎鼉鼧闧屲"},
	        new string[]{"W","焥磑踓濻瀢伪偽僞硪蒍唔咼斡迋哇娃廆桅沩洼潙癐硊隗涡囗堝渦蝸為晥汪洿芴譁釫汍皖瞣脘撝椲濊煒违違韋韦鼿齀顐鋄鎫蛙趶完忹枉頑顽位捰蟃盳務呅味溦亹汶雺霚霧莬葂忞砇万无勿無袜譕婺敄毋墲婑錗餧喔昷握渥痦駇罖捼踒蜼亠聉吴屗挖瓦佤娲腽劸咓啘嗢媧徍搲攨污溛漥瓲畖砙穵窊窪膃襪邷韈韤鼃外歪崴喎竵顡晚碗玩弯挽湾丸腕宛婉烷豌惋蜿绾芄琬纨剜畹菀倇刓卍卐唍埦塆壪岏帵彎忨抏捖捥晩晼杤梚椀涴潫灣琓盌睕笂箢紈綩綰翫脕萬貦贃贎踠輓鋔骩骪骫望忘王往网亡旺妄辋魍惘罔亾仼兦尩尪尫彺徃暀朢棢瀇網莣菵蚟蛧蝄誷輞为未围喂胃微尾威伟卫危委魏唯维畏惟巍蔚谓尉潍纬慰萎苇渭葳帏艉鲔娓逶闱隈玮涠帷诿洧偎猥猬嵬軎韪炜煨圩薇痿倭偉儰叞唩喡喴圍墛壝媁媙媦寪峗峞崣嵔嶶幃徫愄愇懀捤揋揻斖暐梶椳楲洈浘渨湋溈潿濰烓煟熭燰爲犚犩琟瑋璏癓硙碨維緭緯縅罻腲芛苿荱菋葦葨蓶蔿薳藯蘶蜲蝛蝟螱衛衞褽覣覹詴諉謂讆讏躗躛轊鄬醀鍏鍡鏏闈阢隇霨霺韑韙韡頠颹餵饖鮇鮠鮪鰃鰄鳂鳚问文闻稳温吻蚊纹瘟紊阌刎雯璺呚問塭妏彣忟抆揾搵桽榅榲殟溫炆珳瑥瘒穏穩紋聞肳脗芠蚉螡蟁豱鎾閺閿闅闦饂馼魰鰛鰮鳁鳼鴍鼤翁嗡瓮蕹蓊勜塕奣嵡暡滃甕瞈罋聬螉鎓鶲鹟齆我窝卧挝沃蜗幄龌肟莴仴偓婐媉捾撾杌楃涹濣猧瓁瞃窩腛臒臥萵齷五屋物舞雾误捂悟钨武戊务呜伍午吾侮乌诬芜巫晤梧坞妩蜈牾寤兀怃邬忤骛鋈仵鹜迕焐庑鹉鼯浯圬乄伆俉倵儛剭吳呉啎嗚塢奦娪娬嫵屼岉嵨廡弙忢悞悮憮扤摀旿杇橆歍洖溩潕烏熃熓玝珷珸瑦璑甒矹碔祦禑窏窹箼粅茣莁蕪螐誈誣誤躌逜郚鄔錻鎢隖靰騖鯃鰞鴮鵐鵡鶩鷡鹀夕攚"},
	        new string[]{"X","塈娭諰嚣嚻囂郩峀嘐裦喺匂肸燹斜焎謵醒呬啸喜嘨嘯屟歗糦脪踅郗溴慉槒絮歘螅昕昔笚娊潒涎湺咥槢渫鞢閜諴錎猲暅邢郉呴銛雟虾吓獬蝦糮軒轩巷茠藃嚇蝎螛謞轄辖郃阋鬩噷掀哅瓨戏戯戱戲芐觷许学學郇眩鐶烜銊焄蔒轋系繋谿夏夓挾揳暇頡攕柙涀瞷纎纖纤醎鈃銒钘閒险險塂校鵁嶰嬐馸坙鋞銄揟泫讂噱穴姰旬濬欯莶薟輱餡馅硎兄蝷奊忚伈勖獯瓕訤蘘譊幸煖谑需盻嚊顖椺皛宿滊磎螇磍荨伣俔嗛孅廞杴燂膁藖削硝箾稧鈊鬵嬛琁櫹鱃戌栒灥舄儴勷忀欀纕鑲镶穘鑐泧躠鈒霰虩闟裇聓曏襳綃绡萷歙信姺孞冼姓箵嘘噓狶餙齥俆咰翛蒣选選巡廵恂眴徙杫析菥蜤吅撨僁卹碿巺畃跣些牺犧獻蕈錟錫锡脩綉绣稥辪鎼鞋苋莧邜觹觽觿限西洗细吸席稀溪熄膝息袭惜习嘻悉矽熙希檄晰媳硒铣烯隙汐犀蜥奚浠葸饩屣玺嬉禊兮翕穸禧僖淅蓰舾醯欷皙蟋羲隰唏曦樨粞熹觋鼷係俙傒凞匸卌卥厀唽噏嚱壐嬆屃屖屭嵠嶍巇徆徯忥怬怸恄悕惁慀憘憙扸晞晳暿枲桸椞榽橀橲歖氥渓漇漝潝潟澙焁焈焟焬煕熂熈熺熻燨爔犔犠琋璽瘜睎瞦磶礂窸細綌緆縰繥绤習翖肹胁脅脇脋莃葈蒠蒵蓆蕮薂蟢蠵衋襲覀覡覤訢誒謑譆豀豨豯貕赥赩趇趘蹝躧郋郤鄎酅釳釸鈢銑鎴鏭鑴隟隵霫霼飁餏餼饻騱騽驨鯑鰼鱚鳛鵗鸂黖下峡瞎霞狭匣侠狎黠硖罅遐瑕丅俠傄圷峽懗敮梺炠烚煆狹珨疜睱硤碬祫筪縖翈舝舺蕸谺赮鍜鎋鏬閕陜陿颬騢魻鰕鶷先线县现显闲献嫌陷鲜弦衔咸锨仙腺贤宪舷羡藓岘痫籼娴蚬猃祆跹酰暹氙鹇筅仚伭佡僊僩僲僴咞哯啣嘕垷奾妶姭娨娹婱嫺嫻尟尠屳峴崄嶮幰廯忺憪憲憸挦揱搟撊撏攇晛櫶毨澖瀗灦烍狝獫獮玁玹珗現甉癇癎県睍礥禒秈箲粯絃絤綫線縣繊缐羨胘臔臽苮蘚蚿蛝蜆衘褼誢誸譣豏賢贒赻蹮躚軐銜鋧鍁鍌閑陥険韅韯韱顕顯馦鮮鱻鶱鷳鷴鷼麲鼸想向象项响香乡相像箱享厢翔祥橡详湘襄飨鲞骧蟓庠芗饷缃葙亯佭勨嚮姠嶑廂晑栙楿珦瓖絴緗缿膷萫薌蚃蠁襐詳跭郷鄉鄊鄕鐌響項餉饗饟驤鮝鯗鱌鱜鱶麘小笑消销萧效宵晓肖孝淆霄哮魈骁枵哓崤筱潇逍枭箫侾俲傚効咲咻嘋嘵婋宯庨彇恷敩斅斆暁曉梟歊毊洨涍澩瀟灱灲烋焇熽猇獢痚痟皢硣窙筿篠簘簫膮蕭虈虓蟂蟏蟰蠨詨誟誵銷驍髇髐鴞鴵鷍鸮写歇血谢卸屑蟹泻懈泄楔邪协械谐携绁缬榭廨撷偕瀣亵榍邂薤躞燮勰伳偰冩協卨嗋噧垥塮夑娎媟寫屓屧峫徢恊愶拹擕擷攜旪暬洩澥瀉灺炧炨熁燲爕瑎祄禼糏紲絬綊緤緳纈缷翓膎薢藛蝑蝢蠍蠏衺褉褻襭諧謝讗鞵韰齂齘龤新心欣芯薪锌辛寻衅忻歆囟馨鑫伩俽兴噺妡嬜尋惞杺枔潃炘焮盺興舋襑訫邤釁鋅鐔阠馫性型形星腥刑杏猩惺悻擤荇侀倖哘垶娙婞嬹曐洐涬煋瑆皨睲篂緈臖莕蛵裄觪觲謃鉶铏騂骍鮏鯹胸雄凶熊汹匈芎兇夐忷恟敻昫洶胷訩詗詾讻诇修锈休羞嗅袖秀朽貅馐髹鸺庥岫俢樇滫烌煦珛琇璓糔綇繍繡脙臹苬螑褎褏銝銹鎀鏅鏥鏽飍饈髤鮴鵂齅须虚蓄续序叙婿徐旭绪吁酗恤墟糈栩蓿顼洫胥醑诩溆盱伵侐偦冔勗喣垿壻姁媭嬃幁怴敍敘旴暊朂楈欨欰歔殈汿沀湑漵潊烅烼獝珝珬疞盢盨瞁瞲稰稸窢続緒緖縃續聟蕦藚虗虛蛡訏訹詡諝譃谞賉鄦須頊驉鬚魆魖鱮悬旋玄宣喧绚癣暄楦儇渲漩铉璇煊碹镟炫揎萱痃谖咺塇媗嫙怰愃愋懸昍昡晅暶檈洵琄瑄璿癬睻矎禤箮絢縼繏翧翾萲蓒蔙蕿藼蘐蜁蝖蠉衒袨諠諼譞贙鉉鍹鏇颴駨駽鰚雪靴薛鳕泶乴坹岤峃嶨斈桖樰瀥燢狘疶膤艝茓蒆袕謔轌辥雤鞾鱈鷽鸴讯熏训循殉迅驯汛逊勋询巽鲟浔埙醺峋薰荀窨曛徇伨侚偱勛勲勳卂噀噚嚑坃塤壎壦奞愻揗攳杊桪樳殾毥潯燅燖燻爋狥珣璕矄稄紃纁臐薫蘍蟳訊訓訙詢賐迿遜鄩鑂顨馴鱏鱘丫鴬徔"},
	        new string[]{"Y","抣腌乂儗噫堨醷靉崖菴裺遃陰隂鴳仰卬醃墺梎泑澚眑蝹鴁鴢猰贇赟玚瑒槱浧郢佁呹扡沶狋眙移誃謻涌傭怞揄牰懨踰掾郵箹營詒诒唌檐蜒崵瘍儥浟約约莜軺轺佚昳楪泆軼轶甬窬逾斁鈗柂亚亜亞哑唖啞垭埡搤椏邑閼齾饐欕仸軋阣屹仡嘢鎑硬劷硲蜮顩喛巆謍羽璍釪垸援瑗蒝蚘褘眃餫越魊乁倚揖猗蟻押擖玪豜醶黬廴侥咬妖敥吟唫訡釿頴颕瀅熒鎣僪萭鬻萒乙矞繘勻匀鋆幆垠珢齦龈妪嫗臾楽樂纅躒阴斿游药藥玧幺蝆弇啱嶷喦嵒褹疑蚴鞰紆纡遇醧乑彦媐堷欹忔渏錡齮苂窯銚顤欽邺謜汋蕘氜月込噎剡挻掞烻禓燿抴畬葉虵吲媵埶戺洂液繹绎醳釶俞兪忬悆豫野隃鷸鹬以肄螔逘銉飴饴莤遗遺炎夵姚抭荑桋跃躍鮧畇艞銕桯恿硧婾媮愉褕諭谕余墿悇讉彵杝袘迆迤焑焽艈茒汙汚黳夭园夗妧惌蚖鋺魭尢尣厃有沇熨猚痏茟愠慍煴眼緼縕缊蕰蕴薀藴蘊褞輼轀辒韞韫壅枂馧於峿御扜杅鋙铻齬龉义吚咦羛義蜴呀岈疨矣綖麙洋羏鴹殽滧爻獟鷕叶偞枻絏耶鍱鐷頁页憖荥嫈滎研硏莠于亐伃惐淢籲芋芌藇謣雩魣弲楥蜎爓筼篔蟫压牙芽鸭涯雅衙鸦讶蚜砑琊桠睚娅痖氩伢迓揠俹倻劜厊厌厓厭圔圠堐壓婭孲崕庌庘挜掗枒椻氬浥漄犽玡瑘瘂稏窫笌聐蕥襾訝釾錏鐚铔顔颜鴉鴨鵶鼼齖烟沿盐言演严咽淹掩宴岩延堰验艳殷阉砚雁唁焰衍谚燕阎焉奄芫厣菸魇琰滟焱赝筵兖餍恹罨湮偃谳胭晏闫俨郾酽鄢妍鼹崦嫣乵偐偣傿儼兗円剦匽厳厴喭噞嚥嚴塩墕壛壧妟姲姸娫娮嬊嬮嬿孍嵃嵓嶖巌巖巗巘巚彥愝懕戭扊抁揅揜昖暥曕曮棪椼楌樮檿櫩殗氤洇淫渰渷漹灎灔灧灩焔煙熖燄牪狿猒珚琂甗硯硽碞礹篶簷縯臙艶艷莚葕蔅虤蝘褗覎觃觾訁訮詽諺讌讞讠豓豔贋贗躽軅郔酀酓醼釅閆閹閻阭隁隒靥靨顏饜騐験騴驗驠鬳魘鰋鳫鴈鶠鷃鷰鹽麣黡黤黫黭黶鼴齗齞齴龂龑样养羊扬秧氧痒杨漾阳殃央鸯佯疡炀恙徉鞅泱蛘烊怏佒傟咉坱垟姎岟崸慃懩抰揚攁敭旸昜映暘柍楊楧様樣氱瀁炴煬珜癢眏眻礢紻羕羪胦英詇諹軮輰鉠鍈鍚鐊钖阦陽雵霙霷颺飏養駚鰑鴦鸉要摇腰窑舀邀谣遥瑶耀尧钥珧鳐鹞吆崾肴曜徭杳窈倄偠傜喓嗂垚堯婹媱宎尭岆峣嶢嶤幼徺怮愮揺搖摿暚曣枖柼楆榚榣殀溔滛瀹烑熎狕猶猺瑤由矅磘祅穾窅窔窰筄繇苭葯葽蓔薬蘨袎覞訞詏謠謡讑遙鎐鑰闄靿颻飖餆餚騕鰩鷂齩也夜业爷掖腋冶曳椰谒晔烨揶铘亱僷吔嚈埜墷壄嶪嶫捓捙擛擨擪擫暍曄曅曗枼枽業歋漜潱澲燁爗爺皣瞱瞸礏蠮謁鄓鄴鋣鎁餘餣饁馀馌驜鵺鸈黦一已亿衣依易医仪亦椅益姨翼译伊胰沂宜异彝壹蚁谊铱翌艺抑役臆逸疫颐裔意毅忆夷溢诣议怿痍镒癔怡驿旖熠酏翊峄圯殪懿劓漪咿瘗羿弈苡佾贻钇缢刈悒黟翳弋奕埸挹薏呓镱舣亄伇伿侇俋偯儀億兿冝劮勚勩匇匜印呭唈囈圛坄垼壱夁妷嫕嫛嬄嬑嬟宐宧寱寲峓崺嶧嶬巸帟帠庡廙弌弬彛彜彞怈恞悘悥憶懌扅扆撎攺敡敼旑晹暆曀曎曵杙枍栘栧栺棭椬椸榏槸檍檥檹欭歝殔殹毉洢浂浳湙潩澺瀷炈焲熤熪熼燚燡燱獈玴瑿瓵畩異痬瘞瘱瞖礒祎禕秇稦穓竩笖簃籎縊繄繶羠耴肊膉艗艤芅苅苢萓萟蓺藙藝蘙虉蛜蛦螘螠衤衪衵袣裛襼觺訲訳詍詣誼譩譯議讛豙豛豷貖貤貽贀跇跠輢轙辷迻郼醫釔釴鈘鈠鉯銥鎰鏔鐿陭隿霬靾頉頤頥顊顗駅驛骮鯣鳦鶂鶃鶍鷁鷊鷖鷧鷾鸃鹝鹢鹥黓黝齸因引银音饮隐荫尹寅茵姻堙鄞喑夤胤狺霪蚓铟瘾茚乚侌冘凐噖噾嚚囙圁垔垽婣婬峾崟崯嶾廕愔慇慭憗懚斦朄栶檃檭檼櫽歅殥泿洕淾湚溵濥濦烎犾猌璌瘖癊癮碒磤禋秵筃粌絪緸荶蒑蔩蔭蘟螾裀訔訚誾諲讔趛酳鈏鈝銀銦闉阥陻隠隱霒霠靷鞇韾飮飲駰骃鮣鷣应影营迎蝇赢鹰颖莹盈婴樱缨荧萤萦楹蓥瘿茔鹦莺璎嘤撄瑛滢潆嬴罂瀛膺颍偀僌営噟嚶塋媖嬰孆孾巊廮応愥應摬攍攖暎朠桜梬櫻櫿渶溁溋潁濙濚濴瀠瀯瀴灐灜煐珱瑩瓔甇甖癭盁矨碤礯穎籝籯緓縈纓绬罃罌耺膡萾藀蘡蛍蝧蝿螢蠅蠳褮覮譍譻賏贏軈鐛鑍锳韺鶧鶯鷪鷹鸎鸚哟育唷喲用永拥蛹勇雍咏泳佣踊痈庸臃慵俑墉鳙邕喁饔镛勈嗈噰埇塎嫞嵱廱彮怺悀惥愑愹慂擁柡栐槦湧滽澭灉牅癕癰砽禜苚蒏詠踴郺鄘醟銿鏞雝顒颙鯒鰫鱅鲬鷛又右油优友铀忧尤犹诱悠邮酉佑釉幽疣攸蚰鱿卣莸猷宥牖囿柚蝣鼬铕呦侑丣亴偤優哊唀嚘姷孧峟峳庮怣憂懮栯梄楢櫌櫾沋湵滺瀀牗狖祐禉秞糿纋羐羑耰聈肬脜苃蕕蜏訧誘貁輏輶迶逌逰遊邎鄾酭鈾銪駀魷鮋鲉麀与欲鱼雨语愈狱玉渔予誉愚虞娱淤舆屿禹宇迂域郁盂喻峪渝榆隅浴寓裕预驭嵛阈鹆妤窳觎舁蓣煜钰谀竽瑜禺聿欤俣伛圄庾昱萸瘐圉瘀饫毓燠腴狳蝓俁俼偊傴匬唹喅喐喩噊噳圫堉堣堬娛娯媀嬩寙崳嵎嶎嶼庽彧慾懙戫扵挧敔斔斞旟棛棜棫楀楡楰櫲欎欝歈歟歶淯湡滪漁澞澦灪焴燏爩牏獄玗玙琙瑀璵畭瘉癒盓睮砡硢礇礖礜祤禦秗稢稶穥穻箊篽籅籞緎罭羭與艅苑茰萮蒮蓹蕍蕷薁蘌蘛虶蜟螸衧袬覦語諛譽貐軉輍輿轝迃逳遹邘鄅酑鈺鋊錥鍝鐭閾陓隩雓霱預飫饇馭騟驈骬髃鬰鬱魚鮽鯲鰅鱊鳿鴥鴧鴪鵒鷠鸆鸒麌远员元院圆原愿猿怨冤源缘袁渊垣鸳辕鼋橼媛爰眢鸢沅螈塬傆允剈厡厵員噮囦圎園圓妴媴嫄嬽寃杬棩榞榬櫞淵渁渆渕湲溒灁猨獂盶禐笎緣縁羱肙葾蒬薗蜵蝝蝯衏裫褑褤貟贠轅逺遠邍邧酛鈨鎱陨隕願駌騵鳶鴛鵷鶢鶰鹓黿鼘鼝阅岳悦曰粤钺刖龠樾嬳岄嶽彟彠恱悅戉抈捳曱爚玥矱礿禴篗籆籥籰粵蘥蚎蚏跀軏鈅鉞閱閲鸑鸙龥云运晕韵孕耘酝郧氲恽郓芸昀狁殒纭伝傊喗囩夽奫妘惲愪抎暈枟橒殞氳沄涢溳澐熅熉磒秐紜縜繧腪荺蒀蒕蒷蕓賱運鄆鄖醖醞雲霣韗韻頵馻齫齳帀吖"},
	        new string[]{"Z","敳碪箃稹彴穛囃橴戝噆囋蹔臧澡造嫧帻幘柵栅齰增橧繒缯硳岾猠乽楂蹅喳茝齜龇佔崭嶃嶄榐沾胀袩蹍长脹縐绉謅诌宅詀侲枕桭棧湛疹縝缜鍖埩徴徵撜氶浈奓歭治泜淔滞滯瘈胝胵騺鳷重种偅樁烛盅祌種蹱嚋圳妯婤掫畤盩譸助榋炪祝禇菆著蓫蕏藸蠩諸诸跦鉏嘬惴甎膞鶨幢椎肫孎斫綴缀腏辶鋜錣鏃镞兹呰啙姕滋澬粢胔茲赼趑嗭偬燪碂総縦縱總纵奏揍族卒酢卆皻踤麆攒僔攢濽灒穳篹襸乼倅崒崪紣綷隹墫最諎躜躦酂酇矺觰噡皽詹黮黵啁箌棏陟翟墆樀疐蝃蹢逐槇淍赵趙鵃鸼挃柣眰窒至螲褶掟騆夂噣竺笁鍺锗陼耑埻追杂澤硾囐乻旕胏阝轧甴飦戅戇閘闸乤乥瓡找叀湷篧隻吇汥瘵莋蒩郅鯯齍橏箴籈譖谮灂唶搩祖艐謯紾臸稵岨珇租菹葅足邹郰鄒鄹朘甄縳襈弡穱蕞雉乫扻窼簻揁怾廤泏窋燭爥穒摺叕鱄总乿孖旀椧搱枬莻孨粘輾辗銸鋷苧夞乯磗洀庄浌巼闏乶喸釽兺哛祗吱忮扺支枝渍漬衹寨撍葴鍼喿招繰菬蓁組组騶驺譔囕咮棁笍霅栍虄壭煰燥閪縇繌嶦鳣鞝召佋折磼蜄震殖峙厔唑汁禵肢胑跩馶魳朱杼紵藷蠾謶鐲镯專娷濯傂乺捴揔棷稡撰嗺簨璅侤襨擿穉薙鐟寘鎭鎮镇占乭飳剸塼嫥磚啍窀萚蘀阤曢朑桛歚毮烪癷聁聣蒊虲袰鐢霻鶑乛鍐迬欈枮鑦黹芧玆饌馔札这這黰羘亪拽煠择擇樴櫂襗众泈粥灹荢砸咋匝扎咱咂拶喒桚沞沯紥紮臜臢襍鉔雑雜雥韴在再灾载栽宰哉甾崽仔傤儎扗洅渽溨災烖睵縡菑賳載酨暂赞簪趱糌瓒昝錾偺儧儹兂寁揝暫瓉瓚禶簮讃讚賛贊趲鄼鏨鐕饡脏葬赃奘驵塟弉牂臓臟賍賘贓贜銺駔髒早遭糟灶枣凿躁藻皂噪蚤唣唕慥栆梍棗璪皁竈簉艁薻譟趮蹧醩鑿则责泽箦舴迮啧仄昃笮赜則啫嘖夨崱庂択捑昗柞樍歵汄沢泎溭皟瞔矠礋簀耫葃蔶蠌謫謮讁谪責賾飵鸅齚贼蠈賊鰂鱡鲗怎囎譛赠憎综罾甑锃増熷璔矰磳綜譄贈鄫鋥鬷鱛炸渣眨榨乍诈铡砟痄吒哳蚱揸咤齄偧劄厏宱怍抯拃挓搾摣柤樝溠牐皶箚苲蚻詐譇譗踷迊醡鍘鮓鮺鲊鲝齇摘窄债斋砦債厇夈抧捚斎榸粂鉙齋站战盏毡展栈蘸绽斩瞻谵搌旃偡嫸嶘惉戦戰斬旜栴桟氈氊琖盞綻菚薝虥虦蛅覱譫讝趈輚轏邅醆閚霑颭飐饘驏驙魙鸇鹯张章帐仗丈掌涨账樟杖彰漳瘴障仉嫜幛鄣璋嶂獐蟑傽墇帳幥張慞扙暲涱漲痮瘬瞕礃粀粻蔁賬遧餦騿鱆麞着照罩爪兆昭沼肇棹钊笊诏啅垗妱巶旐曌枛炤燳爫狣瑵盄瞾羄肁肈詔釗鉊鍣駋鮡者遮蛰哲蔗辙浙柘辄赭鹧磔蜇啠喆嗻嚞埑嫬悊慹晢晣樜歽淛潪砓籷粍虴蟄蟅襵詟謺讋輒輙轍陬鮿鷓鷙鸷真阵针振斟珍诊砧臻贞侦祯轸榛赈朕鸩胗桢畛偵塦姫嫃寊屒帪弫抮挋揕搸敶昣栕栚楨樼殝湞潧澵獉珎瑧眕眞眹禎禛紖絼縥纼聄萙蒖薽袗裖覙診誫貞賑軫轃辴遉酙針鉁鋴錱陣靕駗鬒鱵鴆帧正整睁争挣征怔证症郑拯蒸狰政峥钲铮筝诤佂凧塣姃媜崝崢幀徰愸抍掙晸止炡烝爭猙癥眐睜箏篜糽聇証諍證踭鄭鉦錚鬇鴊只之直知制指纸芝稚蜘质脂炙织职痔植执值侄址趾旨志挚掷致置帜智秩帙摭桎枳轵祉蛭膣觯栀彘芷咫絷踬骘轾痣踯埴贽卮酯豸跖栉俧倁値偫儨凪劕劧坁坧垁執墌姪娡嬂崻巵帋幟庢庤廌徏徝恉憄懥懫戠搘摯擲旘晊梔梽椥榰槜櫍櫛汦沚洔洷淽滍漐潌瀄熫犆狾猘瓆疷疻砋礩祑祬禃禔秓秖秪秷稙稺筫紙紩綕緻縶織翐聀職膱芖茋藢蘵蟙衼袟袠製襧覟觗觶訨誌豑豒貭質贄跱蹠躑躓軄軹輊釞銍鋕鑕铚锧阯隲馽駤騭驇鴙鴲鼅中钟肿终忠仲衷踵舯螽锺冢伀刣堹塚妐妕媑尰幒彸柊歱汷炂煄狆瘇眾筗籦終腫茽蔠蚛螤衆衳衶諥鈡鍾鐘鴤鼨周洲皱州轴舟昼骤宙肘帚咒胄纣荮碡籀酎伷侏侜僽冑呪啄喌徟晝晭注炿烐珘甃疛皺睭矪箒籒籕粙紂舳菷葤詋諏诹賙赒軸輈輖辀週郮銂霌駎駲驟鯞住主猪竹株煮筑贮铸嘱拄驻珠瞩蛛柱诛蛀潴洙伫瘃翥茱苎橥箸炷铢疰渚躅麈邾槠佇劅劚劯囑坾墸壴宔嵀斀斸曯柷樦櫡櫧櫫欘殶濐瀦灟炢煑眝矚砫硃祩秼竚笜筯築篫紸絑纻罜羜茁茿莇蝫蠋袾註詝誅豬貯跓軴鉒銖鋳鑄钃阻霔馵駐駯鮢鯺鱁鴸鼄抓檛膼髽转专砖赚篆颛啭僎囀堟専灷瑑瑼竱籑蒃蟤諯賺転轉鄟顓装撞壮桩状妆丬壯壵妝娤庒梉焋狀粧糚荘莊裝坠锥赘骓缒墜沝甀畷礈縋膇諈贅轛醊錐鑆餟騅鵻准谆凖宒準稕綧衠訰諄迍捉桌拙灼浊卓琢酌擢诼浞涿倬禚丵圴妰娺撯擆斮斱斲斵晫梲棳椓槕濁烵犳琸硺窡窧籗籱罬蠗蠿諁諑鐯鵫鷟字自子紫籽资姿滓咨孜淄笫秭恣谘缁梓鲻锱孳耔觜髭赀訾嵫眦姊辎倳剚嗞姉孶崰杍栥椔榟湽牸眥矷禌秄秶紎緇胾芓茊茡葘虸訿諮貲資趦輜輺鄑釨鈭錙鍿鎡镃頾頿鯔鰦鶅鼒咗唨宗棕踪鬃粽腙倊倧傯堫嵏嵕嵸惣惾搃摠昮朡椶熧猔猣疭瘲磫稯糉緃緵縂翪葼蓗蝬豵踨蹤錝鍯鏓鑁騌騣骔鬉鯮鯼走鲰棸緅赱鯐鯫黀齺诅俎哫爼箤詛踿錊鎺靻钻纂缵攥劗籫繤纉纘鑚鑽嘴醉罪厜噿嶊嶵晬枠栬樶檇檌璻祽穝絊纗蟕辠酔尊遵鳟撙樽噂嶟捘繜罇譐銌鐏鱒鶎鷷做作坐左座昨佐祚胙阼侳岝岞秨稓筰糳繓葄蓙袏鈼鏱"},
        };

        #endregion

        #region 首字母字典

        static Dictionary<char, char> PtDict = new Dictionary<char, char>();

        static void Init()
        {
            foreach (var ss in PtArray)
            {
                foreach (var c in ss[1])
                {
                    if (!PtDict.ContainsKey(c))
                    {
                        PtDict.Add(c, ss[0][0]);
                    }
                    else
                    {
                        Console.WriteLine(c);
                    }
                }
            }
        }

        static PT()
        {
            Init();
        }

        #endregion

        #region GetPT

        /// <summary>
        /// 获取传入字符串的第一个字母的拼音首字母
        /// </summary>
        /// <param name="s"></param>
        /// <param name="CharIndex"></param>
        /// <param name="Digit"></param>
        /// <param name="Other"></param>
        /// <returns></returns>
        public static Char GetPT(string s, int CharIndex = 0, Char Digit = '#', Char Other = '@')
        {
            if (string.IsNullOrEmpty(s))
            {
                return ' ';
            }

            char c = s[CharIndex];

            return GetPT(c, Digit, Other);
        }

        #endregion

        #region GetPT

        /// <summary>
        /// 获取传入字符的拼音首字母
        /// </summary>
        /// <param name="c"></param>
        /// <param name="Digit"></param>
        /// <param name="Other"></param>
        /// <returns></returns>
        public static Char GetPT(Char c, Char Digit = '#', Char Other = '@')
        {
            if (c >= 'A' && c <= 'Z')
                return c;

            if (c >= 'a' && c <= 'z')
                return (char)(c - 32);

            if (Char.IsDigit(c))
            {
                if (Digit == Char.MinValue)
                    return c;
                return Digit;
            }

            if (PtDict.ContainsKey(c))
                return PtDict[c];

            if (Other == Char.MinValue)
                return c;
            
            return Other;
        }

        #endregion

        #region GetPY

        /// <summary>
        /// 获取输入字符串的拼音缩写
        /// </summary>
        /// <param name="s"></param>
        /// <param name="Digit"></param>
        /// <param name="Other"></param>
        /// <returns></returns>
        public static string GetPY(string s, Char Digit = '#', Char Other = '@')
        {
            string r = string.Empty;

            foreach (char c in s)
            {
                r += GetPT(c, Char.MinValue, Char.MinValue);
            }

            return r;
        }

        #endregion
    }
}
